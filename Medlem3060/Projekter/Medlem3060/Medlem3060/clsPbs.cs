using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace nsPuls3060
{

    public struct betlintype
    {
        public object betid;
        public object pbssektionnr;
        public object pbstranskode;
        public object Nr;
        public object faknr;
        public object debitorkonto;
        public object aftalenr;
        public object betalingsdato;
        public object belob;
        public object indbetalingsdato;
        public object bogforingsdato;
        public object indbetalingsbelob;
        public object pbskortart;
        public object pbsgebyrbelob;
        public object pbsarkivnr;
    }

    public enum datefmtType
    {
        fdmmddyyyy = 0,
        fdmmddyyyyhhmmss = 1,
    }

    public struct lintype
    {
        //string advistekst;
        //double advisbelob;
    }

    public struct infolintype
    {
        //long getinfotekst;
    }

    class clsPbs
    {
        private DbData3060 dbData3060;

        public clsPbs()
        {
            dbData3060 = new DbData3060(@"C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\dbData3060.sdf");
        }

        public Boolean erMedlem(short pNr) { return erMedlem(pNr, DateTime.Now); }
        public Boolean erMedlem(short pNr, DateTime pDate)
        {
            var BetalingsFristiDageGamleMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageGamleMedlemmer;
            var BetalingsFristiDageNyeMedlemmer = global::nsPuls3060.Properties.Settings.Default.BetalingsFristiDageNyeMedlemmer;
            var indmeldelsesDato = DateTime.MinValue;
            var udmeldelsesDato = DateTime.MinValue;
            var kontingentTilbageførtDato = DateTime.MinValue;
            var kontingentTilDato31 = DateTime.MinValue;
            var kontingentBetaltDato31 = DateTime.MinValue;
            var kontingentBetaltDato = DateTime.MinValue; ;
            var kontingentTilDato = DateTime.MinValue; ;
            var restanceTilDatoGamleMedlemmer = DateTime.MinValue; ;
            var opkrævningsDato = DateTime.MinValue; ;
            var restanceTilDatoNyeMedlemmer = DateTime.MinValue; ;
            var b10 = false; // Seneste Indmelses dato fundet
            var b20 = false; // Seneste PBS opkrævnings dato fundet
            var b30 = false; // Seneste Kontingent betalt til dato fundet
            var b31 = false; // Næst seneste Kontingent betalt til dato fundet
            var b40 = false; // Seneste PBS betaling tilbageført fundet
            var b50 = false; // Udmeldelses dato fundet

            //Den query skal ændres til en union !!!!!!!!!!!!
            var MedlemLogs = from c in dbData3060.TblMedlemLog
                             where c.Nr == pNr && c.Logdato <= pDate
                             orderby c.Logdato descending
                             select c;

            foreach (var MedlemLog in MedlemLogs)
            {
                switch (MedlemLog.Akt_id)
                {
                    case 10: // Seneste Indmelses dato
                        if (!b10)
                        {
                            b10 = true;
                            indmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 20:  // Seneste PBS opkrævnings dato
                        if (!b20)
                        {
                            b20 = true;
                            opkrævningsDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 30:  // Kontingent betalt til dato
                        if ((b30) && (!b31)) // Næst seneste Kontingent betalt til dato
                        {
                            b31 = true;
                            kontingentBetaltDato31 = (DateTime)MedlemLog.Logdato;
                            kontingentTilDato31 = (DateTime)MedlemLog.Akt_dato;
                        }
                        if ((!b30) && (!b31)) // Seneste Kontingent betalt til dato
                        {
                            b30 = true;
                            kontingentBetaltDato = (DateTime)MedlemLog.Logdato;
                            kontingentTilDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 40:  // Seneste PBS betaling tilbageført
                        if (!b40)
                        {
                            b40 = true;
                            kontingentTilbageførtDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;

                    case 50:  // Udmeldelses dato
                        if (!b50)
                        {
                            b50 = true;
                            udmeldelsesDato = (DateTime)MedlemLog.Akt_dato;
                        }
                        break;
                }
            }

            //Undersøg vedr ind- og udmeldelse
            if (b10) //Findes der en indmeldelse
            {
                if (b50) //Findes der en udmeldelse
                {
                    if (udmeldelsesDato >= indmeldelsesDato) //Er udmeldelsen aktiv
                    {
                        if (udmeldelsesDato <= pDate) //Er udmeldelsen aktiv
                        {
                            return false;
                        }
                    }
                }
                else //Der findes ingen indmeldelse
                {
                    return false;
                }
            }

            //Find aktive betalingsrecord
            if (b40) //Findes der en kontingent tilbageført
            {
                if (kontingentTilbageførtDato >= kontingentBetaltDato) //Kontingenttilbageført er aktiv
                {
                    //''!!!Kontingent er tilbageført !!!!!!!!!
                    if (b31)
                    {
                        kontingentBetaltDato = kontingentBetaltDato31;
                        kontingentTilDato = kontingentTilDato31;
                    }
                    else
                    {
                        b30 = false;
                    }
                }
            }


            //Undersøg om der er betalt kontingent
            if (b30) //Findes der en betaling
            {
                restanceTilDatoGamleMedlemmer = kontingentTilDato.AddDays(BetalingsFristiDageGamleMedlemmer);
                if (restanceTilDatoGamleMedlemmer >= pDate) //Er kontingentTilDato aktiv
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            { //Der findes ingen betalinger. Nyt medlem?
                restanceTilDatoNyeMedlemmer = indmeldelsesDato.AddDays(BetalingsFristiDageNyeMedlemmer);
                if (restanceTilDatoNyeMedlemmer >= pDate)
                { //Er kontingentTilDato aktiv
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        private string write002(string datalevnr, string delsystem, string levtype, string levident, System.DateTime levdato)
        {
            string rec = null;

            rec = "BS002";
            rec += lpad(datalevnr, 8, '?');
            rec += lpad(delsystem, 3, '?');
            rec += lpad(levtype, 4, '?');
            rec += lpad(levident, 10, '0');
            rec += rpad("", 19, ' ');
            rec += lpad(String.Format("{0:ddMMyy}", levdato), 6, '?');
            return rec;
        }

        private string write012(string pbsnr, string sektionnr, string debgrpnr, string levident, System.DateTime levdato, string regnr, string kontonr, string h_linie)
        {
            string rec = null;

            rec = "BS012";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(sektionnr, 4, '?');
            //-- Sektionsnr.
            if (sektionnr == "0100")
            {
                rec += rpad("", 3, ' ');
                //-- Filler
                rec += lpad(debgrpnr, 5, '0');
                //-- Debitorgruppenr.: Debitorgruppenummer
                rec += rpad(levident, 15, ' ');
                //-- Leveranceidentifikation: Brugers identifikation hos dataleverandør
                rec += rpad("", 9, ' ');
                //-- Filler
                //-- Dato: 000000 eller leverancens dannelsesdato
                rec += lpad(String.Format("{0:ddMMyy}", levdato), 6, '?');
            }
            else if (sektionnr == "0112")
            {
                rec += rpad("", 5, ' ');
                //-- Filler
                rec += lpad(debgrpnr, 5, '0');
                //-- Debitorgruppenr.: Debitorgruppenummer
                rec += rpad(levident, 15, ' ');
                //-- Leveranceidentifikation: Brugers identifikation hos dataleverandør
                rec += rpad("", 4, ' ');
                //-- Filler
                //-- Dato: 00000000 eller leverancens dannelsesdato
                rec += lpad(String.Format("{0:ddMMyyyy}", levdato), 8, '?');
            }
            else if (sektionnr == "0117")
            {
                rec += rpad("", 5, ' ');
                //-- Filler
                rec += lpad(debgrpnr, 5, '0');
                //-- Debitorgruppenr.: Debitorgruppenummer
                rec += rpad(levident, 15, ' ');
                //-- Leveranceidentifikation: Brugers identifikation hos dataleverandør
                rec += rpad("", 4, ' ');
                //-- Filler
                //-- Dato: 00000000 eller leverancens dannelsesdato
                rec += lpad(String.Format("{0:ddMMyyyy}", levdato), 8, '?');
            }
            rec += lpad(regnr, 4, '0');
            //-- Reg.nr.: Overførselsregistreringsnummer
            rec += lpad(kontonr, 10, '0');
            //-- Kontonr.: Overførselskontonummer
            rec += h_linie;
            //-- H-linie: Tekst til hovedlinie på advis
            return rec;
        }

        private string write022(string sektionnr, string pbsnr, string transkode, long recordnr, string debgrpnr, string personid, long aftalenr, string Data)
        {
            string rec = null;

            rec = "BS022";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(transkode, 4, '?');
            //-- Transkode:
            if (sektionnr == "0112")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else if (sektionnr == "0117")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else
            {
                //-- Recordnr.
                rec += lpad(recordnr, 3, '0');
            }
            rec += lpad(debgrpnr, 5, '0');
            //-- Debitorgruppenr.: Debitorgruppenummer
            rec += lpad(personid, 15, '0');
            //-- Kundenr.: Debitors kundenummer hos kreditor
            rec += lpad(aftalenr, 9, '0');
            //-- Aftalenr.
            if (recordnr == 9)
            {
                rec += rpad("", 15, ' ');
                //-- Filler
                rec += rpad(Data, 4, ' ');
                //-- Debitor postnr
                //-- Debitor landekode
                rec += rpad("DK", 3, ' ');
            }
            else
            {
                //-- Debitor navn og adresse
                rec += Data;
            }

            return rec;
        }

        private string write042(string sektionnr, string pbsnr, string transkode, string debgrpnr, string medlemsnr, int aftalenr, System.DateTime betaldato, int fortegn, int belob, int faknr)
        {
            string rec = null;

            rec = "BS042";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(transkode, 4, '?');
            //-- Transkode:
            if (sektionnr == "0112")
            {
                //-- Recordnr.
                rec += lpad("", 5, '0');
            }
            else if (sektionnr == "0117")
            {
                //-- Recordnr.
                rec += lpad("", 5, '0');
            }
            else
            {
                //-- Recordnr.
                rec += lpad("", 3, '0');
            }
            rec += lpad(debgrpnr, 5, '0');
            //-- Debitorgruppenr.: Debitorgruppenummer
            rec += lpad(medlemsnr, 15, '0');
            //-- Kundenr.: Debitors kundenummer hos kreditor
            rec += lpad(aftalenr, 9, '0');
            //-- Aftalenr.
            if (sektionnr == "0112")
            {
                rec += lpad(String.Format("{0:ddMMyyyy}", betaldato), 8, '?');
            }
            else if (sektionnr == "0117")
            {
                rec += lpad(String.Format("{0:ddMMyyyy}", betaldato), 8, '?');
            }
            else
            {
                rec += lpad(String.Format("{0:ddMMyyyy}", betaldato), 6, '?');
            }
            rec += lpad(fortegn, 1, '0');
            rec += lpad(belob, 13, '0');
            rec += lpad(faknr, 9, '0');
            rec += rpad("", 21, ' ');
            if (sektionnr == "0112")
            {
                rec += rpad("", 2, '0');
            }
            else if (sektionnr == "0117")
            {
                rec += rpad("", 2, '0');
            }
            else
            {
                rec += rpad("", 6, '0');
            }

            return rec;
        }

        private string write052(string sektionnr, string pbsnr, string transkode, long recordnr, string debgrpnr, string medlemsnr, long aftalenr, string advistekst1, double advisbelob1, string advistekst2, double advisbelob2)
        {
            string rec = null;


            rec = "BS052";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(transkode, 4, '?');
            //-- Transkode:
            if (sektionnr == "0112")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else if (sektionnr == "0117")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else
            {
                //-- Recordnr.
                rec += lpad(recordnr, 3, '0');
            }
            rec += lpad(debgrpnr, 5, '0');
            //-- Debitorgruppenr.: Debitorgruppenummer
            rec += lpad(medlemsnr, 15, '0');
            //-- Kundenr.: Debitors kundenummer hos kreditor
            rec += lpad(aftalenr, 9, '0');
            //-- Aftalenr.
            rec += " ";
            if (sektionnr == "0112")
            {
                if (advisbelob1 != 0)
                {
                    rec += rpad(advistekst1, 38, ' ');
                    //-- Advistekst 1: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob1_formated = String.Format("{0:###0.00}", advisbelob1);
                    rec += rpad(advisbelob1_formated.Replace('.', ','), 9, ' ');
                }
                else
                {
                    //-- Advistekst 1: Tekstlinie på advis
                    rec += advistekst1;
                }
            }
            else if (sektionnr == "0117")
            {
                if (advisbelob1 != 0)
                {
                    rec += rpad(advistekst1, 38, ' ');
                    //-- Advistekst 1: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob1_formated = String.Format("{0:###0.00}", advisbelob1);
                    rec += rpad(advisbelob1_formated.Replace('.', ','), 9, ' ');
                }
                else
                {
                    //-- Advistekst 1: Tekstlinie på advis
                    rec += advistekst1;
                }
            }
            else
            {
                if (advisbelob1 != 0)
                {
                    rec += rpad(advistekst1, 29, ' ');
                    //-- Advistekst 1: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob1_formated = String.Format("{0:###0.00}", advisbelob1);
                    rec += rpad(advisbelob1_formated.Replace('.', ','), 9, ' ');
                }
                else
                {
                    //-- Advistekst 1: Tekstlinie på advis
                    rec += rpad(advistekst1, 38, ' ');
                }
                rec += '0';
                if (advisbelob2 != 0)
                {
                    rec += rpad(advistekst2, 29, ' ');
                    //-- Advistekst 2: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob2_formated = String.Format("{0:###0.00}", advisbelob2);
                    rec += rpad(advisbelob2_formated.Replace('.', ','), 9, ' ');
                }
                else if (advistekst2 != null)
                {
                    //-- Advistekst 2: Tekstlinie på advis
                    rec += advistekst2;
                }
            }
            return rec;
        }

        private string write092(string pbsnr, string sektionnr, string debgrpnr, int antal1, int belob1, int antal2, int antal3)
        {

            string rec = null;

            rec = "BS092";
            rec += lpad(pbsnr, 8, '?');
            rec += lpad(sektionnr, 4, '?');
            if (sektionnr == "0112")
            {
                rec += rpad("", 5, '0');
                rec += lpad(debgrpnr, 5, '0');
                rec += rpad("", 4, ' ');
            }
            else if (sektionnr == "0117")
            {
                rec += rpad("", 5, '0');
                rec += lpad(debgrpnr, 5, '0');
                rec += rpad("", 4, ' ');
            }
            else
            {
                rec += rpad("", 3, '0');
                rec += lpad(debgrpnr, 5, '0');
                rec += rpad("", 6, ' ');
            }
            rec += lpad(antal1, 11, '0');
            rec += lpad(belob1, 15, '0');
            rec += lpad(antal2, 11, '0');
            rec += rpad("", 15, ' ');
            rec += lpad(antal3, 11, '0');

            return rec;
        }
        private string write992(string datalevnr, string delsystem, string levtype, long antal1, long antal2, long belob2, long antal3, long antal4)
        {

            string rec = null;
            rec = "BS992";
            rec += lpad(datalevnr, 8, '?');
            rec += lpad(delsystem, 3, '?');
            rec += lpad(levtype, 4, '?');
            rec += lpad(antal1, 11, '0');
            rec += lpad(antal2, 11, '0');
            rec += lpad(belob2, 15, '0');
            rec += lpad(antal3, 11, '0');
            rec += lpad("", 15, '0');
            rec += lpad(antal4, 11, '0');
            rec += lpad("", 34, '0');

            return rec;
        }

        public object lpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadLeft(Length, PadChar);
        }

        public object rpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadRight(Length, PadChar);
        }

        public int nextval(string nrserienavn)
        {
            try
            {
                var rst = (from c in dbData3060.Tblnrserie
                           where c.Nrserienavn == nrserienavn
                           select c).First();

                if (rst.Sidstbrugtenr != null)
                {
                    rst.Sidstbrugtenr += 1;
                    return rst.Sidstbrugtenr.Value;
                }
                else
                {
                    rst.Sidstbrugtenr = 0;
                    return rst.Sidstbrugtenr.Value;
                }
            }
            catch (System.InvalidOperationException)
            {
                Tblnrserie rec_nrserie = new Tblnrserie
                {
                    Nrserienavn = nrserienavn,
                    Sidstbrugtenr = 0
                };
                dbData3060.Tblnrserie.InsertOnSubmit(rec_nrserie);
                return 0;
            }
        }

        public void faktura_601_action(int lobnr)
        {
            string rec;
            //lintype lin;
            //infolintype infolin;
            int recnr;
            int fortegn;
            int wleveranceid;
            int seq;
            // -- Betalingsoplysninger
            string h_linie;
            // -- Tekst til hovedlinie på advis
            int belobint;
            string advistekst;
            int advisbelob;
            // -- Tællere
            int antal042;
            // -- Antal 042: Antal foranstående 042 records
            int belob042;
            // -- Beløb: Nettobeløb i 042 records
            int antal052;
            // -- Antal 052: Antal foranstående 052 records
            int antal022;
            // -- Antal 022: Antal foranstående 022 records
            int antalsek;
            // -- Antal sektioner i leverancen
            int antal042tot;
            // -- Antal 042: Antal foranstående 042 records
            int belob042tot;
            // -- Beløb: Nettobeløb i 042 records
            int antal052tot;
            // -- Antal 052: Antal foranstående 052 records
            int antal022tot;
            // -- Antal 022: Antal foranstående 022 records
            // TODO: On Error GoTo Warning!!!: The statement is not translatable 
            // --lobnr = 275 '--debug
            h_linie = "LØBEKLUBBEN PULS 3060";
            seq = 0;
            antal042 = 0;
            belob042 = 0;
            antal052 = 0;
            antal022 = 0;
            antalsek = 0;
            antal042tot = 0;
            belob042tot = 0;
            antal052tot = 0;
            antal022tot = 0;

            {
                var antal = (from c in dbData3060.Tbltilpbs
                             where c.Id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in dbData3060.Tbltilpbs
                             where c.Id == lobnr && c.Pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            {
                var antal = (from c in dbData3060.Tblfak
                             where c.Tilpbsid == lobnr
                             select c).Count();
                // if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
                // næste linie skal fjernes efter test
                if (antal != 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in dbData3060.Tbltilpbs
                          where c.Id == lobnr
                          select c).First();
            if (rsttil.Udtrukket == null) { rsttil.Udtrukket = DateTime.Now; }
            if (rsttil.Bilagdato == null) { rsttil.Bilagdato = rsttil.Udtrukket; }
            if (rsttil.Delsystem == null) { rsttil.Delsystem = "BS1"; }
            if (rsttil.Leverancetype == null) { rsttil.Leverancetype = ""; }
            dbData3060.SubmitChanges();

            wleveranceid = nextval("leveranceid");

            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
            {
                Delsystem = rsttil.Delsystem,
                Leverancetype = rsttil.Leverancetype,
                Oprettetaf = "Fak",
                Oprettet = DateTime.Now,
                Leveranceid = wleveranceid
            };
            dbData3060.Tblpbsforsendelse.InsertOnSubmit(rec_pbsforsendelse);

            Tblpbsfiles rec_pbsfiles = new Tblpbsfiles();
            rec_pbsforsendelse.Tblpbsfiles.Add(rec_pbsfiles);

            var rstkrd = (from c in dbData3060.Tblkreditor
                          where c.Delsystem == rsttil.Delsystem
                          select c).First();


            // -- Leverance Start - 0601 Betalingsoplysninger
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - rsttil.Delsystem - Delsystem:  Dataleverandør delsystem
            // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
            // - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
            // - rsttil!udtrukket - Dato: 000000 eller leverancens dannelsesdato
            rec = write002(rstkrd.Datalevnr, rsttil.Delsystem, "0601", wleveranceid.ToString(), (DateTime)rsttil.Udtrukket);
            Tblpbsfile rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            // -- Sektion start - sektion 0112/0117
            // -  rstkrd.Pbsnr       - PBS-nr.: Kreditors PBS-nummer
            // -  rstkrd.Sektionnr   - Sektionsnr.: 0112/0117 (Betalinger med lang advistekst)
            // -  rstkrd.Debgrpnr    - Debitorgruppenr.: Debitorgruppenummer
            // -  rstkrd.Datalevnavn - Leveranceidentifikation: Brugers identifikation hos dataleverandør
            // -  rsttil.Udtrukket   - Dato: 000000 eller leverancens dannelsesdato
            // -  rstkrd.Regnr       - Reg.nr.: Overførselsregistreringsnummer
            // -  rstkrd.Kontonr     - Kontonr.: Overførselskontonummer
            // -  h_linie            - H-linie: Tekst til hovedlinie på advis
            rec = write012(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, rstkrd.Datalevnavn, (DateTime)rsttil.Udtrukket, rstkrd.Regnr, rstkrd.Kontonr, h_linie);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
            antalsek++;
            var rstdebs = from f in dbData3060.Tblfak
                          where f.Tilpbsid == lobnr && f.Nr != null
                          join m in dbData3060.TblMedlem on f.Nr equals m.Nr
                          orderby f.Nr
                          select new
                          {
                              f.Id,
                              m.Nr,
                              Kundenr = 32001610000000 + m.Nr,
                              m.Navn,
                              m.Adresse,
                              m.Postnr,
                              f.Faknr,
                              f.Betalingsdato,
                              f.Infotekst,
                              f.Tilpbsid,
                              f.Advistekst,
                              belob = f.Advisbelob
                          };
            foreach (var rstdeb in rstdebs)
            {
                // -- Debitornavn
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 1                - Recordnr.: 001
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Navn      - Navn: Debitors navn
                rec = write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 1, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Navn);
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // -- Debitoradresse 1/adresse 2
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 2                - Recordnr.: 002
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Adresse   - Adresse 1: Adresselinie 1
                rec = write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 2, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Adresse);
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // -- Debitorpostnummer
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 3                - Recordnr.: 003
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Postnr    - Postnr.: Postnummer
                rec = write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 9, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Postnr.ToString());
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // -- Forfald betaling
                if (rstdeb.belob > 0)
                {
                    fortegn = 1;
                    // -- Fortegnskode: 1 = træk
                    belobint = ((int)rstdeb.belob) * 100;
                    belob042 += belobint;
                    belob042tot += belobint;
                }
                else if (rstdeb.belob < 0)
                {
                    fortegn = 2;
                    // -- Fortegnskode: 2 = indsættelse
                    belobint = ((int)rstdeb.belob) * (-100);
                    belob042 -= belobint;
                    belob042tot -= belobint;
                }
                else
                {
                    fortegn = 0;  // -- Fortegnskode: 0 = 0-beløb
                    belobint = 0;
                }
                // - rstkrd.Sektionnr         -
                // - rstkrd.Pbsnr             - PBS-nr.: Kreditors PBS-nummer
                // - rstkrd.Transkodebetaling - Transkode: 0280/0285 (Betaling)
                // - rstkrd.Debgrpnr          - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr           - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                        - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Betalingsdato     -
                // - fortegn                  -
                // - belobint                 - Beløb: Beløb i øre uden fortegn
                // - rstdeb.Faknr             - faknr: Information vedrørende betalingen.
                rec = write042(rstkrd.Sektionnr, rstkrd.Pbsnr, rstkrd.Transkodebetaling, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, (DateTime)rstdeb.Betalingsdato, fortegn, belobint, (int)rstdeb.Faknr);
                antal042++;
                antal042tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                string[] arradvis;
                int n;
                string[] splitchars = { "\r\n" };
                arradvis = rstdeb.Advistekst.Split(splitchars, StringSplitOptions.None);
                recnr = 0;
                for (n = arradvis.GetLowerBound(0); n <= arradvis.GetUpperBound(0); n++)
                {
                    if (n == arradvis.GetLowerBound(0))
                    {
                        recnr++;
                        advistekst = arradvis[n];
                        advisbelob = (int)rstdeb.belob;
                    }
                    else
                    {
                        recnr++;
                        advistekst = arradvis[n];
                        advisbelob = 0;
                    }

                    // -- Tekst til advis
                    antal052++;
                    antal052tot++;
                    rec = write052(rstkrd.Sektionnr, rstkrd.Pbsnr, "0241", recnr, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, advistekst, advisbelob, "", 0);
                    rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                    rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
                }
                // '  FOR infolin IN SELECT * FROM pbs.getinfotekst(rstdeb!id, rstdeb!infotekstid)
                // '  LOOP  '--pbs.getinfotekst
                // '    recnr = recnr + 1
                // '    '-- Tekst til advis
                // '    antal052 = antal052 + 1
                // '    antal052tot = antal052tot + 1
                // '    selector = True
                // '
                // '    '- rstkrd!sektionnr     -
                // '    '- rstkrd!pbsnr         - PBS-nr.: Kreditors PBS-nummer
                // '    '- "0241"               - Transkode: 0241 (Tekstlinie)
                // '    '- recnr                - Recordnr.: 001-999
                // '    '- rstkrd!debgrpnr      - Debitorgruppenr.: Debitorgruppenummer
                // '    '- rstdeb!kundenr       - Kundenr.: Debitors kundenummer hos kreditor
                // '    '- 0                    - Aftalenr.: 000000000 eller 999999999
                // '    '- infolin.getinfotekst - Advistekst 1: Tekstlinie på advis
                // '    '- 0.0                  - Advisbeløb 1: Beløb på advis
                // '    '- ""                   - Advistekst 2: Tekstlinie på advis
                // '    '- 0.0                  - Advisbeløb 2: Beløb på advis
                // '    rec = write052(IfNull(rstkrd!sektionnr), _
                // '              IfNull(rstkrd!pbsnr), _
                // '              "0241", _
                // '              recnr, _
                // '              IfNull(rstkrd!debgrpnr), _
                // '              IfNull(rstdeb!kundenr), _
                // '              0, _
                // '              IfNull(infolin.getinfotekst), _
                // '              0, _
                // '              "", _
                // '              0)
                // '    seq = seq + 1
                // '    rstfile.AddNew
                // '    rstfile!pbsfilesid = wpbsfilesid
                // '    rstfile!Seqnr = seq
                // '    rstfile!data = rec
                // '    rstfile.Update
                // '    ts.WriteLine rec
                // '  END LOOP  '--pbs.getinfotekst

            } // -- End rstdebs

            // -- Sektion slut - sektion 0112/117
            // - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
            // - rstkrd!sektionnr - Sektionsnr.: 0112/0117 (Betalinger)
            // - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
            // - antal042         - Antal 042: Antal foranstående 042 records
            // - belob042         - Beløb: Nettobeløb i 042 records
            // - antal052         - Antal 052: Antal foranstående 052 records
            // - antal022         - Antal 022: Antal foranstående 022 records
            rec = write092(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, antal042, belob042, antal052, antal022);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            // -- Leverance slut  - 0601 Betalingsoplysninger
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - rstkrd.Delsystem - Delsystem:  Dataleverandør delsystem
            // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
            // - antalsek         - Antal sektioner: Antal sektioner i leverancen
            // - antal042tot      - Antal 042: Antal foranstående 042 records
            // - belob042tot      - Beløb: Nettobeløb i 042 records
            // - antal052tot      - Antal 052: Antal foranstående 052 records
            // - antal022tot      - Antal 022: Antal foranstående 022 records
            rec = write992(rstkrd.Datalevnr, rstkrd.Delsystem, "0601", antalsek, antal042tot, belob042tot, antal052tot, antal022tot);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            rsttil.Udtrukket = DateTime.Now;
            rsttil.Leverancespecifikation = wleveranceid.ToString();
            dbData3060.SubmitChanges();
        }

        public int kontingent_fakturer_bs1()
        {
            int lobnr;
            string wadvistekst;
            int wantalfakturaer;
            wantalfakturaer = 0;

            Tbltilpbs rec_tilpbs = new Tbltilpbs
            {
                Delsystem = "BS1",
                Leverancetype = "0601",
                Udtrukket = DateTime.Now
            };
            dbData3060.Tbltilpbs.InsertOnSubmit(rec_tilpbs);
            lobnr = rec_tilpbs.Id;

            var rstmedlems = from h in dbData3060.TempKontforslag
                             join l in dbData3060.TempKontforslaglinie on h.Id equals l.Kontforslagid
                             join m in dbData3060.TblMedlem on l.Nr equals m.Nr
                             select new
                             {
                                 m.Nr,
                                 m.Navn,
                                 h.Betalingsdato,
                                 l.Advisbelob,
                                 l.Fradato,
                                 h.Tildato
                             };

            foreach (var rstmedlem in rstmedlems)
            {

                wadvistekst = "Puls 3060 Medlemskontingent";
                wadvistekst += "\r\n" + "  for " + rstmedlem.Navn;
                wadvistekst += "\r\n" + "  perioden fra " + string.Format("{0:yyyy-MM-dd}", rstmedlem.Fradato) + " til " + string.Format("{0:yyyy-MM-dd}", rstmedlem.Tildato);
                wadvistekst += "\r\n" + "Besøg Puls 3060\"s hjemmeside på www.puls3060.dk";
                Tblfak rec_fak = new Tblfak
                {
                    Betalingsdato = rstmedlem.Betalingsdato,
                    Nr = rstmedlem.Nr,
                    Faknr = nextval("faknr"),
                    Advistekst = wadvistekst,
                    Advisbelob = rstmedlem.Advisbelob,
                    Infotekst = 0,
                    Bogfkonto = 1800,
                    Vnr = 1,
                    Fradato = rstmedlem.Fradato,
                    Tildato = rstmedlem.Tildato
                };
                rec_tilpbs.Tblfak.Add(rec_fak);
                wantalfakturaer++;
            }
            dbData3060.SubmitChanges();

            if (wantalfakturaer > 0) { faktura_601_action(lobnr); }
            return wantalfakturaer;

        }

        public betlintype read042(int frapbsid, string sektion, string transkode, string rec)
        {
            int fortegn;
            double belobmun;
            int belob;
            betlintype rcd;

            // --  pbssektionnr
            rcd.pbssektionnr = sektion;
            // --  pbstranskode
            // - transkode 0236, gennemf?rt automatisk betaling
            // - transkode 0237, afvist automatisk betaling
            // - transkode 0238, afmeldt automatisk betaling
            // - transkode 0239, tilbagef?rt betaling
            rcd.pbstranskode = transkode;
            // --  debitorkonto
            if ((sektion == "0211"))
            {
                rcd.Nr = rec.Substring(33, 7);
                rcd.debitorkonto = rec.Substring(25, 15);
            }
            else if ((sektion == "0215"))
            {
                rcd.Nr = rec.Substring(37, 7);
                rcd.debitorkonto = rec.Substring(29, 15);
            }
            else
            {
                rcd.Nr = null;
                rcd.debitorkonto = null;
            }
            // --  aftalenr
            if ((sektion == "0211"))
            {
                rcd.aftalenr = rec.Substring(40, 9);
            }
            else
            {
                rcd.aftalenr = null;
            }
            // --  pbskortart
            if ((sektion == "0215"))
            {
                rcd.pbskortart = rec.Substring(44, 2);
            }
            else
            {
                rcd.pbskortart = null;
            }
            // --  pbsgebyrbelob
            if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(46, 1));
                belob = int.Parse(rec.Substring(47, 5));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.pbsgebyrbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.pbsgebyrbelob = belobmun;
                }
                else
                {
                    rcd.pbsgebyrbelob = 0;
                }
            }
            else
            {
                rcd.pbsgebyrbelob = 0;
            }
            // --  betalingsdato
            if ((sektion == "0211"))
            {
                if ((rec.Substring(49, 6) != "000000"))
                {
                    rcd.betalingsdato = ("20"
                                + (rec.Substring(53, 2) + ("-"
                                + (rec.Substring(51, 2) + ("-" + rec.Substring(49, 2))))));
                }
                else
                {
                    rcd.betalingsdato = null;
                }
            }
            else if ((sektion == "0215"))
            {
                if ((rec.Substring(52, 6) != "000000"))
                {
                    rcd.betalingsdato = ("20"
                                + (rec.Substring(56, 2) + ("-"
                                + (rec.Substring(54, 2) + ("-" + rec.Substring(52, 2))))));
                }
                else
                {
                    rcd.betalingsdato = null;
                }
            }
            else
            {
                rcd.betalingsdato = null;
            }
            // --  belob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(56, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.belob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.belob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    rcd.belob = (belobmun * -1);
                }
                else
                {
                    rcd.belob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(59, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.belob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.belob = belobmun;
                }
                else
                {
                    rcd.belob = null;
                }
            }
            else
            {
                rcd.belob = null;
            }
            // --  faknr
            if ((sektion == "0211"))
            {
                rcd.faknr = rec.Substring(69, 9).Trim();
            }
            else if ((sektion == "0215"))
            {
                rcd.faknr = rec.Substring(72, 9).Trim();
            }
            else
            {
                rcd.faknr = null;
            }
            // --  pbsarkivnr
            if ((sektion == "0215"))
            {
                rcd.pbsarkivnr = rec.Substring(81, 22);
            }
            else
            {
                rcd.pbsarkivnr = null;
            }
            // --  indbetalingsdato
            if ((rec.Substring(103, 6) != "000000"))
            {
                rcd.indbetalingsdato = ("20"
                            + (rec.Substring(107, 2) + ("-"
                            + (rec.Substring(105, 2) + ("-" + rec.Substring(103, 2))))));
            }
            else
            {
                rcd.indbetalingsdato = null;
            }
            // --  bogforingsdato
            rcd.bogforingsdato = 0;
            if ((rec.Substring(109, 6) != "000000"))
            {
                rcd.bogforingsdato = ("20"
                            + (rec.Substring(113, 2) + ("-"
                            + (rec.Substring(111, 2) + ("-" + rec.Substring(109, 2))))));
            }
            else
            {
                rcd.bogforingsdato = null;
            }
            // --  betid
            rcd.betid = getbetid(frapbsid, sektion, transkode, rcd.bogforingsdato);
            // --  indbetalingsbelob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.indbetalingsbelob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    rcd.indbetalingsbelob = (belobmun * -1);
                }
                else
                {
                    rcd.indbetalingsbelob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.indbetalingsbelob = belobmun;
                }
                else
                {
                    rcd.indbetalingsbelob = null;
                }
            }
            else
            {
                rcd.indbetalingsbelob = null;
            }
            /*           
                       stSql = ("insert into tblbetlin (betid, pbssektionnr, pbstranskode, Nr, faknr, " + ("debitorkonto, aftalenr, betalingsdato, belob, indbetalingsdato, bogforingsdato, " + ("indbetalingsbelob, pbskortart, pbsgebyrbelob, pbsarkivnr) " + ("values ("
                                   + (qf(rcd.betid) + (","
                                   + (qf(rcd.pbssektionnr) + (","
                                   + (qf(rcd.pbstranskode) + (","
                                   + (qf(rcd.Nr) + (","
                                   + (qf(rcd.faknr) + (", "
                                   + (qf(rcd.debitorkonto) + (", "
                                   + (qf(rcd.aftalenr) + (", "
                                   + (qf(rcd.betalingsdato) + (", "
                                   + (qf(rcd.belob) + (", "
                                   + (qf(rcd.indbetalingsdato) + (", "
                                   + (qf(rcd.bogforingsdato) + (", "
                                   + (qf(rcd.indbetalingsbelob) + (", "
                                   + (qf(rcd.pbskortart) + (", "
                                   + (qf(rcd.pbsgebyrbelob) + (", "
                                   + (qf(rcd.pbsarkivnr) + ")")))))))))))))))))))))))))))))))));
                       rst = CurrentProject.Connection.Execute(stSql);
            */
            return rcd;
        }

        public int getbetid(int pfrapbsid, string psektion, string ptranskode, object pbogforingsdato)
        {
            int wid = 0;
            /*
                        ADODB.Recordset rst;
                        // TODO: On Error GoTo Warning!!!: The statement is not translatable 
                        stSql = ("SELECT id as wid FROM tblbet" + (" WHERE frapbsid = "
                                    + (qf(pfrapbsid) + ("   AND pbssektionnr = "
                                    + (qf(psektion) + ("   AND transkode = "
                                    + (qf(ptranskode) + ("   AND bogforingsdato = "
                                    + (qf(pbogforingsdato) + ";")))))))));
                        rst = CurrentProject.Connection.Execute(stSql);
                        if (!rst.eof)
                        {
                            wid = rst;
                            wid;
                        }
                        else
                        {
                            wid = nextval("tblbet_id_seq");
                            stSql = ("INSERT INTO tblbet (id, frapbsid, pbssektionnr, transkode, bogforingsdato)  values ("
                                        + (qf(wid) + (", "
                                        + (qf(pfrapbsid) + (","
                                        + (qf(psektion) + (","
                                        + (qf(ptranskode) + (","
                                        + (qf(pbogforingsdato) + ");"))))))))));
                            CurrentProject.Connection.Execute;
                            stSql;
                        }
             */
            return wid;
        }


        public void TestRead042()
        {
            int frapbsid;
            string sektion;
            string transkode;
            string rec;
            betlintype result;
            frapbsid = 1;
            sektion = "0211";
            transkode = "0236";
            rec = "BS0420398564402360000000100000000001231312345678910120310000000012755000000125                       " +
            "  1212031212030000000012755";
            result = read042(frapbsid, sektion, transkode, rec);
        }

        public int betalinger_fra_pbs()
        {
            string rec;
            string leverancetype;
            string leverancespecifikation;
            DateTime leverancedannelsesdato;
            string sektion;
            betlintype rcd;
            int wfrapbsid = 0;
            int wpbsfilesid;
            int wleveranceid;
            int wfilescount = 0;
            //  wpbsfilesid = 3450  //'--test test
            //  leverancetype = "0602"
            //  sektion = "0211"
            //  rec = "BS0420398564402360000000100000000001231312345678910120310000000012755000000125                         3112031112030000000012755"

            var lstpbsfiles = from h in dbData3060.Tblpbsfiles
                              join d in dbData3060.Tblpbsfile on h.Id equals d.Pbsfilesid
                              where d.Seqnr == 1 && d.Data.Substring(16, 4) == "0602" && d.Data.Substring(0, 2) == "BS"
                              select new
                              {
                                  h.Id,
                                  h.Path,
                                  h.Filename,
                                  leverancetype = d.Data.Substring(16, 4),
                                  delsystem = d.Data.Substring(13, 3),
                                  h.Pbsforsendelseid
                              };

            foreach (var rstpbsfiles in lstpbsfiles)
            {
                wpbsfilesid = rstpbsfiles.Id;
                wfilescount++;
                leverancetype = "";
                sektion = "";
                leverancespecifikation = "";

                var lstpbsfile = from d in dbData3060.Tblpbsfile
                                 where d.Pbsfilesid == wpbsfilesid && d.Seqnr > 0
                                 orderby d.Seqnr
                                 select d;

                foreach (var rstpbsfile in lstpbsfile)
                {
                    rec = rstpbsfile.Data;
                    // -- Bestem Leverance Type
                    if (rstpbsfile.Seqnr == 1)
                    {
                        if ((rec.Substring(0, 5) == "BS002"))
                        {
                            // -- Leverance Start
                            leverancetype = rec.Substring(16, 4);
                            leverancespecifikation = rec.Substring(20, 10);
                            leverancedannelsesdato = DateTime.Parse("20" + rec.Substring(53, 2) + "-" + rec.Substring(51, 2) + "-" + rec.Substring(49, 2));
                        }
                        else
                        {
                            throw new Exception("241 - Første record er ikke en Leverance start record");
                        }
                        if ((leverancetype == "0602"))
                        {
                            // -- Leverance 0602
                            var antal = (from c in dbData3060.Tblfrapbs
                                         where c.Leverancespecifikation == leverancespecifikation
                                         select c).Count();
                            if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere"); }

                            wleveranceid = nextval("leveranceid");
                            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
                            {
                                Delsystem = "BS1",
                                Leverancetype = "0602",
                                Oprettetaf = "Bet",
                                Oprettet = DateTime.Now,
                                Leveranceid = wleveranceid
                            };
                            dbData3060.Tblpbsforsendelse.InsertOnSubmit(rec_pbsforsendelse);

                            Tblfrapbs rec_frapbs = new Tblfrapbs
                            {
                                Delsystem = "BS1",
                                Leverancetype = "0602",
                                Leverancespecifikation = leverancespecifikation,
                                Leverancedannelsesdato = leverancedannelsesdato,
                                Udtrukket = DateTime.Now
                            };
                            rec_pbsforsendelse.Tblfrapbs.Add(rec_frapbs);

                            var rec_pbsfiles = (from c in dbData3060.Tblpbsfiles
                                                where c.Id == rstpbsfiles.Id
                                                select c).First();
                            rec_pbsforsendelse.Tblpbsfiles.Add(rec_pbsfiles);
                        }
                    }
                    if ((leverancetype == "0602"))
                    {
                        // -- Leverance 0602*********
                        // -- Bestem Sektions Type
                        if ((sektion == ""))
                        {
                            if ((rec.Substring(0, 5) == "BS012"))
                            {
                                // -- Sektion Start
                                sektion = rec.Substring(13, 4);
                            }
                            else if (!((rec.Substring(0, 5) == "BS992")
                                        || (rec.Substring(0, 5) == "BS002")))
                            {
                                throw new Exception("243 - F?rste record er ikke en Sektions start record");
                            }
                        }
                        if ((rec.Substring(0, 5) == "BS002"))
                        {
                            // -- Leverance start
                            // -- BEHANDL: Leverance start
                        }
                        else if ((sektion == "0211"))
                        {
                            // -- Sektion 0211 Betalingsinformation
                            if (((rec.Substring(0, 5) == "BS012")
                                        && (rec.Substring(13, 4) == "0211")))
                            {
                                // -- Sektion Start
                                // -- BEHANDL: Sektion Start
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0236")))
                            {
                                // -- Gennemf?rt automatisk betaling
                                // -- BEHANDL: Gennemf?rt automatisk betaling
                                rcd = read042(wfrapbsid, sektion, "0236", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0237")))
                            {
                                // -- Afvist betaling
                                // -- BEHANDL: Afvist betaling
                                rcd = read042(wfrapbsid, sektion, "0237", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0238")))
                            {
                                // -- Afmeldt betaling
                                // -- BEHANDL: Afmeldt betaling
                                rcd = read042(wfrapbsid, sektion, "0238", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0239")))
                            {
                                // -- Tilbagef?rt betaling
                                // -- BEHANDL: Tilbagef?rt betaling
                                rcd = read042(wfrapbsid, sektion, "0239", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS092")
                                        && (rec.Substring(13, 4) == "0211")))
                            {
                                // -- Sektion Slut
                                // -- BEHANDL: Sektion Slut
                                sektion = "";
                            }
                            else
                            {
                                throw new Exception("244 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                            }
                        }
                        else if ((sektion == "0215"))
                        {
                            // -- Sektion 0215 FI-Betalingsinformation
                            if (((rec.Substring(0, 5) == "BS012")
                                        && (rec.Substring(13, 4) == "0215")))
                            {
                                // -- Sektion Start
                                // -- BEHANDL: Sektion Start
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0297")))
                            {
                                // -- Gennemf?rt FI-betaling
                                // -- BEHANDL: Gennemf?rt FI-betaling
                                rcd = read042(wfrapbsid, sektion, "0297", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0299")))
                            {
                                // -- Tilbagef?rt FI-betaling
                                // -- BEHANDL: Tilbagef?rt FI-betaling
                                rcd = read042(wfrapbsid, sektion, "0299", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS092")
                                        && (rec.Substring(13, 4) == "0215")))
                            {
                                // -- Sektion Slut
                                // -- BEHANDL: Sektion Slut
                                sektion = "";
                            }
                            else
                            {
                                throw new Exception("245 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                            }
                        }
                        else if ((rec.Substring(0, 5) == "BS992"))
                        {
                            // -- Leverance slut
                            // -- BEHANDL: Leverance Slut
                            leverancetype = "";
                        }
                        else
                        {
                            throw new Exception("246 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                        }
                    }
                    else
                    {
                        throw new Exception("247 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                    }
                }  // slut rstpbsfile
                /*
                              // -- Update bogføringsbeløb
                stSql = "SELECT betid, sum(indbetalingsbelob) AS sumindbetalingsbelob FROM tblbetlin " & _
                        "WHERE betid IN (select id from tblbet where frapbsid = " & qf(wfrapbsid) & ") " & _
                        "GROUP BY betid;"
                Set rst = CurrentProject.Connection.Execute(stSql)
                Do While Not rst.eof
                    stSql = "UPDATE tblbet SET indbetalingsbelob = " & qf(rst!sumindbetalingsbelob) & " WHERE id = " & qf(rst!betid) & ";"
                   ''ebug.Print stSql & vbCrLf
                   CurrentProject.Connection.Execute stSql
                   rst.MoveNext
                Loop
                rst.Close;
                rstpbsfiles.MoveNext;
               */
            }  // slut rstpbsfiles
            return wfilescount;
        }

    }
}
