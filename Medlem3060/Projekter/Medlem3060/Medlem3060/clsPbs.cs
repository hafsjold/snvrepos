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


    class clsPbs
    {
        private struct lintype
        {
            string advistekst;
            double advisbelob;
        }

        private struct infolintype
        {
            long getinfotekst;
        }        
        
        private DbData3060 dbData3060;
        
        public clsPbs() {
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

        private string write042(string sektionnr, string pbsnr, string transkode, string debgrpnr, string medlemsnr, long aftalenr, System.DateTime betaldato, long fortegn, long belob, long faknr)
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

        private string write092(string pbsnr, string sektionnr, string debgrpnr, string antal1, long belob1, long antal2, long antal3)
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
        //************************************************************************************************
        //************************************************************************************************
        //************************************************************************************************
        //************************************************************************************************
        public void faktura_601_action(long lobnr) {
        string rec;
        lintype lin;
        infolintype infolin;
        long recnr;
        string command;
        int fortegn;
        bool selector;
        long wleveranceid;
        long wpbsforsendelseid;
        long wpbsfilesid;
        long seq;
        string stSql;
        //ADODB.Recordset rst;
        //ADODB.Recordset rstdeb;
        //ADODB.Recordset rsttil;
        //ADODB.Recordset rstkrd;
        //ADODB.Recordset rstfile;
        string vatTilPBSFileNavn;
        // -- Betalingsoplysninger
        string h_linie;
        // -- Tekst til hovedlinie på advis
        long belobint;
        string advistekst;
        long advisbelob;
        // -- Tællere
        long antal042;
        // -- Antal 042: Antal foranstående 042 records
        long belob042;
        // -- Beløb: Nettobeløb i 042 records
        long antal052;
        // -- Antal 052: Antal foranstående 052 records
        long antal022;
        // -- Antal 022: Antal foranstående 022 records
        long antalsek;
        // -- Antal sektioner i leverancen
        long antal042tot;
        // -- Antal 042: Antal foranstående 042 records
        long belob042tot;
        // -- Beløb: Nettobeløb i 042 records
        long antal052tot;
        // -- Antal 052: Antal foranstående 052 records
        long antal022tot;
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
/*
        stSql = ("Select count(id) as antal from tbltilpbs where id = " + (lobnr + ";"));
        rst = CurrentProject.Connection.Execute(stSql);
        if (rst) {
            Antal = 0;
            Err.Raise;
            101;
            ("Der er ingen PBS forsendelse for id: " + lobnr);
            stSql = ("Select count(id) as antal from tbltilpbs where id = " 
                        + (lobnr + " and pbsforsendelseid is not null;"));
            rst = CurrentProject.Connection.Execute(stSql);
            if (rst) {
                Antal = 1;
                Err.Raise;
                102;
                ("Pbsforsendelse for id: " 
                            + (lobnr + " er allerede sendt"));
                stSql = ("Select count(id) as antal from tblfak where tilpbsid = " 
                            + (lobnr + ";"));
                rst = CurrentProject.Connection.Execute(stSql);
                if (rst) {
                    Antal = 0;
                    Err.Raise;
                    103;
                    ("Der er ingen pbs transaktioner for tilpbsid: " 
                                + (lobnr + ";"));
                    stSql = ("SELECT udtrukket, bilagdato, delsystem, leverancetype FROM tbltilpbs WHERE id = " + lobnr);
                    rsttil = new ADODB.Recordset();
                    rsttil.Open;
                    stSql;
                    CurrentProject.Connection;
                    adOpenDynamic;
                    adLockOptimistic;
                    if (IsNull(rsttil, udtrukket)) {
                        rsttil;
                    }
                    udtrukket = Now;
                    if (IsNull(rsttil, bilagdato)) {
                        rsttil;
                    }
                    bilagdato = rsttil;
                    udtrukket;
                    if (IsNull(rsttil, delsystem)) {
                        rsttil;
                    }
                    delsystem = "BS1";
                    if (IsNull(rsttil, leverancetype)) {
                        rsttil;
                    }
                    leverancetype = "";
                    rsttil.Update;
                    wpbsforsendelseid = nextval("tblpbsforsendelse_id_seq");
                    wleveranceid = nextval("leveranceid");
                    stSql = ("INSERT INTO tblpbsforsendelse (id, delsystem, leverancetype, oprettetaf, oprettet, leveranceid) " + ("values(" 
                                + (wpbsforsendelseid + (", \"" + rsttil))));
                    (delsystem + ("\", \"" + rsttil));
                    (leverancetype + ("\", \"Fak\", #" 
                                + (format(Now(), "mm-dd-yyyy hh:mm:ss") + ("#, " 
                                + (wleveranceid + ");")))));
                    CurrentProject.Connection.Execute;
                    stSql;
                    wpbsfilesid = nextval("tblpbsfiles_id_seq");
                    stSql = ("INSERT INTO tblpbsfiles (id, pbsforsendelseid ) values(" 
                                + (wpbsfilesid + (", " 
                                + (wpbsforsendelseid + ");"))));
                    CurrentProject.Connection.Execute;
                    stSql;
                    stSql = "SELECT r.rid, r.Navn, r.Start, r.Slut, r.Placering, r.Eksportmappe, r.TilPBS, r.FraPBS FROM tblAktivt" +
                    "Regnskab AS a INNER JOIN tblRegnskab AS r ON a.rid = r.rid;";
                    rst = CurrentProject.Connection.Execute(stSql);
                    if (!rst.eof) {
                        vatTilPBSFileNavn = rst;
                        (TilPBS + ("PBS" 
                                    + (wleveranceid + ".lst")));
                    }
                    stSql = ("SELECT datalevnr, datalevnavn, pbsnr, sektionnr, debgrpnr, regnr, kontonr, transkodebetaling, delsyst" +
                    "em FROM tblkreditor WHERE delsystem = \"" + rsttil);
                    (delsystem + "\";");
                    rstkrd = CurrentProject.Connection.Execute(stSql);
                    stSql = "select * from tblpbsfile;";
                    rstfile = new ADODB.Recordset();
                    rstfile.Open;
                    stSql;
                    CurrentProject.Connection;
                    adOpenDynamic;
                    adLockOptimistic;
                    // -- Leverance Start - 0601 Betalingsoplysninger
                    // - rstkrd!datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
                    // - rsttil!delsystem - Delsystem:  Dataleverandør delsystem
                    // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
                    // - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
                    // - rsttil!udtrukket - Dato: 000000 eller leverancens dannelsesdato
                    rec = write002(IfNull(rstkrd, datalevnr), IfNull(rsttil, delsystem), "0601", wleveranceid, IfNull(rsttil, udtrukket));
                    seq = (seq + 1);
                    rstfile.AddNew;
                    rstfile;
                    pbsfilesid = wpbsfilesid;
                    rstfile;
                    seqnr = seq;
                    rstfile;
                    Data = rec;
                    rstfile.Update;
                    // -- Sektion start  sektion 0112/0117
                    // -  rstkrd!pbsnr       - PBS-nr.: Kreditors PBS-nummer
                    // -  rstkrd!sektionnr   - Sektionsnr.: 0112/0117 (Betalinger med lang advistekst)
                    // -  rstkrd!debgrpnr    - Debitorgruppenr.: Debitorgruppenummer
                    // -  rstkrd!datalevnavn - Leveranceidentifikation: Brugers identifikation hos dataleverandør
                    // -  rsttil!udtrukket   - Dato: 000000 eller leverancens dannelsesdato
                    // -  rstkrd!regnr       - Reg.nr.: Overførselsregistreringsnummer
                    // -  rstkrd!kontonr     - Kontonr.: Overførselskontonummer
                    // -  h_linie            - H-linie: Tekst til hovedlinie på advis
                    rec = write012(IfNull(rstkrd, pbsnr), IfNull(rstkrd, sektionnr), IfNull(rstkrd, debgrpnr), IfNull(rstkrd, datalevnavn), IfNull(rsttil, udtrukket), IfNull(rstkrd, regnr), IfNull(rstkrd, kontonr), h_linie);
                    seq = (seq + 1);
                    rstfile.AddNew;
                    rstfile;
                    pbsfilesid = wpbsfilesid;
                    rstfile;
                    seqnr = seq;
                    rstfile;
                    Data = rec;
                    rstfile.Update;
                    antalsek = (antalsek + 1);
                    stSql = ("SELECT " + ("f.id AS id, " + ("m.Nr, " + ("m.Kundenr AS kundenr, " + ("m.Navn1 AS navn, " + ("m.Adresse1 AS adresse, " + ("m.Postnr AS postnr, " + ("f.faknr AS faknr, " + ("f.betalingsdato AS betalingsdato, " + ("f.infotekst, " + ("f.tilpbsid, " + ("f.advistekst, " + ("f.advisbelob AS belob " + ("FROM tblfak AS f " + ("LEFT JOIN qryPBSKunde AS m ON f.Nr = m.Nr " + ("WHERE (((f.Nr) Is Not Null) " + ("AND ((f.tilpbsid)= " 
                                + (lobnr + (")) " + "ORDER BY f.Nr;")))))))))))))))))));
                    rstdeb = CurrentProject.Connection.Execute(stSql);
                    while (!rstdeb.eof) {
                        // -- Debitornavn
                        // - rstkrd!sektionnr -
                        // - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
                        // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                        // - 1                - Recordnr.: 001
                        // - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                        // - rstdeb!kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                        // - 0                - Aftalenr.: 000000000 eller 999999999
                        // - rstdeb!navn      - Navn: Debitors navn
                        rec = write022(IfNull(rstkrd, sektionnr), IfNull(rstkrd, pbsnr), "0240", 1, IfNull(rstkrd, debgrpnr), IfNull(rstdeb, kundenr), 0, IfNull(rstdeb, Navn));
                        antal022 = (antal022 + 1);
                        antal022tot = (antal022tot + 1);
                        seq = (seq + 1);
                        rstfile.AddNew;
                        rstfile;
                        pbsfilesid = wpbsfilesid;
                        rstfile;
                        seqnr = seq;
                        rstfile;
                        Data = rec;
                        rstfile.Update;
                        // -- Debitoradresse 1/adresse 2
                        // - rstkrd!sektionnr -
                        // - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
                        // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                        // - 2                - Recordnr.: 002
                        // - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                        // - rstdeb!kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                        // - 0                - Aftalenr.: 000000000 eller 999999999
                        // - rstdeb!adresse   - Adresse 1: Adresselinie 1
                        rec = write022(IfNull(rstkrd, sektionnr), IfNull(rstkrd, pbsnr), "0240", 2, IfNull(rstkrd, debgrpnr), IfNull(rstdeb, kundenr), 0, IfNull(rstdeb, Adresse));
                        antal022 = (antal022 + 1);
                        antal022tot = (antal022tot + 1);
                        seq = (seq + 1);
                        rstfile.AddNew;
                        rstfile;
                        pbsfilesid = wpbsfilesid;
                        rstfile;
                        seqnr = seq;
                        rstfile;
                        Data = rec;
                        rstfile.Update;
                        // -- Debitorpostnummer
                        // - rstkrd!sektionnr -
                        // - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
                        // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                        // - 3                - Recordnr.: 003
                        // - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                        // - rstdeb!kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                        // - 0                - Aftalenr.: 000000000 eller 999999999
                        // - rstdeb!postnr    - Postnr.: Postnummer
                        rec = write022(IfNull(rstkrd, sektionnr), IfNull(rstkrd, pbsnr), "0240", 9, IfNull(rstkrd, debgrpnr), IfNull(rstdeb, kundenr), 0, IfNull(rstdeb, Postnr));
                        antal022 = (antal022 + 1);
                        antal022tot = (antal022tot + 1);
                        seq = (seq + 1);
                        rstfile.AddNew;
                        rstfile;
                        pbsfilesid = wpbsfilesid;
                        rstfile;
                        seqnr = seq;
                        rstfile;
                        Data = rec;
                        rstfile.Update;
                        // -- Forfald betaling
                        if (rstdeb) {
                            (belob > 0);
                            fortegn = 1;
                            // -- Fortegnskode: 1 = træk
                            belobint = rstdeb;
                            (belob * 100);
                            belob042 = (belob042 + belobint);
                            belob042tot = (belob042tot + belobint);
                        }
                        else if (rstdeb) {
                            (belob < 0);
                            fortegn = 2;
                            // -- Fortegnskode: 2 = indsættelse
                            belobint = rstdeb;
                            (belob * -100);
                            belob042 = (belob042 - belobint);
                            belob042tot = (belob042tot - belobint);
                        }
                        else {
                            fortegn = 0;
                            // -- Fortegnskode: 0 = 0-beløb
                            belobint = 0;
                        }
                        // - rstkrd!sektionnr         -
                        // - rstkrd!pbsnr             - PBS-nr.: Kreditors PBS-nummer
                        // - rstkrd!transkodebetaling - Transkode: 0280/0285 (Betaling)
                        // - rstkrd!debgrpnr          - Debitorgruppenr.: Debitorgruppenummer
                        // - rstdeb!kundenr           - Kundenr.: Debitors kundenummer hos kreditor
                        // - 0                        - Aftalenr.: 000000000 eller 999999999
                        // - rstdeb!betalingsdato     -
                        // - fortegn                  -
                        // - belobint                 - Beløb: Beløb i øre uden fortegn
                        // - rstdeb!faknr             - faknr: Information vedrørende betalingen.
                        rec = write042(IfNull(rstkrd, sektionnr), IfNull(rstkrd, pbsnr), IfNull(rstkrd, transkodebetaling), IfNull(rstkrd, debgrpnr), IfNull(rstdeb, kundenr), 0, IfNull(rstdeb, betalingsdato), fortegn, belobint, IfNull(rstdeb, faknr));
                        antal042 = (antal042 + 1);
                        antal042tot = (antal042tot + 1);
                        seq = (seq + 1);
                        rstfile.AddNew;
                        rstfile;
                        pbsfilesid = wpbsfilesid;
                        rstfile;
                        seqnr = seq;
                        rstfile;
                        Data = rec;
                        rstfile.Update;
                        string[] arradvis;
                        int n;
                        arradvis = Split(rstdeb, advistekst, "\r\n", ,, vbBinaryCompare);
                        recnr = 0;
                        for (n = LBound(arradvis); (n <= UBound(arradvis)); n++) {
                            switch (n) {
                                case LBound(arradvis):
                                    recnr = (recnr + 1);
                                    advistekst = arradvis[n];
                                    advisbelob = IfNull(rstdeb, belob);
                                    break;
                                default:
                                    recnr = (recnr + 1);
                                    advistekst = arradvis[n];
                                    advisbelob = 0;
                                    break;
                            }
                            // -- Tekst til advis
                            antal052 = (antal052 + 1);
                            antal052tot = (antal052tot + 1);
                            selector = true;
                            rec = write052(IfNull(rstkrd, sektionnr), IfNull(rstkrd, pbsnr), "0241", recnr, IfNull(rstkrd, debgrpnr), IfNull(rstdeb, kundenr), 0, advistekst, advisbelob, "", 0);
                            seq = (seq + 1);
                            rstfile.AddNew;
                            rstfile;
                            pbsfilesid = wpbsfilesid;
                            rstfile;
                            seqnr = seq;
                            rstfile;
                            Data = rec;
                            rstfile.Update;
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
                        rstdeb.MoveNext;
                    }
                    // --rstdeb
                    // -- Sektion slut - sektion 0112/117
                    // - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
                    // - rstkrd!sektionnr - Sektionsnr.: 0112/0117 (Betalinger)
                    // - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                    // - antal042         - Antal 042: Antal foranstående 042 records
                    // - belob042         - Beløb: Nettobeløb i 042 records
                    // - antal052         - Antal 052: Antal foranstående 052 records
                    // - antal022         - Antal 022: Antal foranstående 022 records
                    rec = write092(IfNull(rstkrd, pbsnr), IfNull(rstkrd, sektionnr), IfNull(rstkrd, debgrpnr), antal042, belob042, antal052, antal022);
                    seq = (seq + 1);
                    rstfile.AddNew;
                    rstfile!pbsfilesid = wpbsfilesid;
                    rstfile!seqnr = seq;
                    rstfile!Data = rec;
                    rstfile.Update;
                    // -- Leverance slut  - 0601 Betalingsoplysninger
                    // - rstkrd!datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
                    // - rstkrd!delsystem - Delsystem:  Dataleverandør delsystem
                    // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
                    // - antalsek         - Antal sektioner: Antal sektioner i leverancen
                    // - antal042tot      - Antal 042: Antal foranstående 042 records
                    // - belob042tot      - Beløb: Nettobeløb i 042 records
                    // - antal052tot      - Antal 052: Antal foranstående 052 records
                    // - antal022tot      - Antal 022: Antal foranstående 022 records
                    rec = write992(IfNull(rstkrd, datalevnr), IfNull(rstkrd, delsystem), "0601", antalsek, antal042tot, belob042tot, antal052tot, antal022tot);
                    seq = (seq + 1);
                    rstfile.AddNew;
                    rstfile;
                    pbsfilesid = wpbsfilesid;
                    rstfile;
                    seqnr = seq;
                    rstfile;
                    Data = rec;
                    rstfile.Update;
                    stSql = ("UPDATE tbltilpbs SET " + ("udtrukket = #" 
                                + (format(rsttil, udtrukket, "mm-dd-yyyy") + ("#, " + ("pbsforsendelseid = " 
                                + (wpbsforsendelseid + (", " + ("leverancespecifikation = " 
                                + (wleveranceid + (" " + ("WHERE id = " 
                                + (lobnr + ";"))))))))))));
                    CurrentProject.Connection.Execute;
                    stSql;
                Exit_faktura_601_action:
                    // TODO: Exit Function: Warning!!! Need to return the value
                    return;
                Err_faktura_601_action:
                    MsgBox;
                    ("Fejl nr " 
                                + (Err.Number + ("\r\n" + ("\r\n" + Err.Description))));
                    vbCritical;
                    Exit_faktura_601_action;
                }
            }
        }
    */}


    }
}
