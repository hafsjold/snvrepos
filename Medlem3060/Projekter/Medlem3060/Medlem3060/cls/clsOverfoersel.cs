using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace nsPuls3060
{
    public class clsOverfoersel
    {
        private Tblpbsfiles m_rec_pbsfiles;

        public delegate void OverfoerselDelegateHandler(int lobnr);
        public static event OverfoerselDelegateHandler SetLobnr;

        public int kreditor_fakturer_os1()
        {
            int lobnr;
            string wadvistekst;
            int wantalfakturaer;
            wantalfakturaer = 0;

            Tbltilpbs rec_tilpbs = new Tbltilpbs
            {
                Delsystem = "OS2",
                Leverancetype = null,
                Udtrukket = DateTime.Now
            };
            Program.dbData3060.Tbltilpbs.InsertOnSubmit(rec_tilpbs);
            Program.dbData3060.SubmitChanges();
            lobnr = rec_tilpbs.Id;

            var rstmedlems = from k in Program.karMedlemmer
                             join l in Program.dbData3060.TempBetalforslaglinie on k.Nr equals l.Nr
                             join h in Program.dbData3060.TempBetalforslag on l.Betalforslagid equals h.Id
                             select new
                             { 
                                 k.Nr,
                                 k.Navn,
                                 h.Betalingsdato,
                                 l.Advisbelob,
                             };

            foreach (var rstmedlem in rstmedlems)
            {

                wadvistekst = "Puls 3060 Betaling";
                Tbloverforsel rec_krdfak = new Tbloverforsel 
                {
                    Nr = rstmedlem.Nr,
                    Advistekst = wadvistekst,
                    Advisbelob = rstmedlem.Advisbelob,
                };
                rec_tilpbs.Tbloverforsel.Add(rec_krdfak);
                wantalfakturaer++;
            }
            Program.dbData3060.SubmitChanges();
            SetLobnr(lobnr);
            return wantalfakturaer;

        }

        public int krdfaktura_overfoersel_action(int lobnr)
        {
            string rec;
            int seq = 0;
            //rcd record;
            //krd record;
            //deb record;
            //debinfo record;
            //bel record;
            //lin record;
            //curlin refcursor;
            int recnr;
            string command;
            int fortegn;
            bool selector;
            int wbilagid;
            int wtransid;
            int wpbsfilesid;
            int wpbsforsendelseid;
            int wleveranceid;
            DateTime wdispositionsdato;
            int whour;
            int wbankdage;
            int wantaloverforsler;

            // Betalingsoplysninger
            int belobint;

            // Tællere
            int antalos5;        // Antal OS5: Antal foranstående OS5 records
            int belobos5;        // Beløb: Nettobeløb i OS5 records

            int antalsek;        // Antal sektioner i leverancen
            int antalos5tot;     // Antal OS5: Antal foranstående OS5 records
            int belobos5tot;     // Beløb: Nettobeløb i OS5 records

            whour = DateTime.Now.Hour;
            if (whour > 17) wbankdage = 3;
            else wbankdage = 2;
            wdispositionsdato = bankdageplus(DateTime.Now, wbankdage);

            wantaloverforsler = (from h in Program.dbData3060.Tbloverforsel where h.Tbltilpbs == null select h).Count();
            if (wantaloverforsler == 0) return 0;

            antalos5 = 0;
            belobos5 = 0;

            antalsek = 0;
            antalos5tot = 0;
            belobos5tot = 0;

            {
                var antal = (from c in Program.dbData3060.Tbltilpbs
                             where c.Id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in Program.dbData3060.Tbltilpbs
                             where c.Id == lobnr && c.Pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            {
                var antal = (from c in Program.dbData3060.Tbloverforsel 
                             where c.Tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in Program.dbData3060.Tbltilpbs
                          where c.Id == lobnr
                          select c).First();
            if (rsttil.Udtrukket == null) { rsttil.Udtrukket = DateTime.Now; }
            if (rsttil.Bilagdato == null) { rsttil.Bilagdato = rsttil.Udtrukket; }
            if (rsttil.Delsystem == null) { rsttil.Delsystem = "OS1"; }
            if (rsttil.Leverancetype != null) { rsttil.Leverancetype = null; }
            Program.dbData3060.SubmitChanges();


            wleveranceid = clsPbs.nextval("leveranceid");
            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
            {
                Delsystem = rsttil.Delsystem,
                Leverancetype = rsttil.Leverancetype,
                Oprettetaf = "Bet",
                Oprettet = DateTime.Now,
                Leveranceid = wleveranceid
            };
            Program.dbData3060.Tblpbsforsendelse.InsertOnSubmit(rec_pbsforsendelse);
            rec_pbsforsendelse.Tbltilpbs.Add(rsttil);

            Tblpbsfiles rec_pbsfiles = new Tblpbsfiles();
            rec_pbsforsendelse.Tblpbsfiles.Add(rec_pbsfiles);

            Tblkreditor krd = (from k in Program.dbData3060.Tblkreditor
                               where k.Delsystem == rsttil.Delsystem 
                               select k).First();

            
            // -- Leverance Start - OS1
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
            rec = writeOS1(krd.Datalevnr, wleveranceid);
            Tblpbsfile rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            var qrydeb = from h in Program.dbData3060.Tbloverforsel
                         where h.Tbltilpbs == null
                         select h;

            // Start loop over betalinger i tbloverforsel
            foreach (Tbloverforsel deb in qrydeb)
            {
                // Sektion start – (OS2)
                antalos5 = 0;
                belobos5 = 0;

                // -- OS2
                // - wdispositionsdato - Dispositionsdato
                // - krd.regnr         - Reg.nr.: Overførselsregistreringsnummer
                // - krd.kontonr       - Kontonr.: Overførselskontonummer
                // - krd.datalevnr     - Dataleverandørnr.: Dataleverandørens SE-nummer
                rec = writeOS2(wdispositionsdato, krd.Regnr, krd.Kontonr, krd.Datalevnr);
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                ++antalsek;

                // -- Forfald betaling
                if (deb.Advisbelob > 0)
                {
                    belobint = (int)(deb.Advisbelob * ((decimal)100));
                    belobos5 = belobos5 + belobint;
                    belobos5tot = belobos5tot + belobint;
                }
                else
                {
                    belobint = 0;
                }

                //SELECT INTO debinfo bankregnr, bankkontonr FROM public.kontoinfo(deb.kundenr);
                string debinfo_bankregnr = "1234";
                string debinfo_bankkontonr = "1234567890";

                // -- OS5
                // - debinfo.bankregnr   - Betalingsmodtager registreringsnummer
                // - debinfo.bankkontonr - Betalingsmodtager kontonummer
                // - belobint            - Beløb: Beløb i øre uden fortegn
                // - wdispositionsdato   - Dispositionsdato
                // - krd.regnr           - Reg.nr.: Overførselsregistreringsnummer
                // - krd.kontonr         - Kontonr.: Overførselskontonummer
                // - deb.advistekst      - Tekst på Betalingsmodtagers kontoudtog
                // - deb.kundenr         - Ref til betalingsmodtager til eget brug
                rec = writeOS5( debinfo_bankregnr, debinfo_bankkontonr, belobint, wdispositionsdato, krd.Regnr, krd.Kontonr, deb.Advistekst, (int)deb.Nr);
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                antalos5++;
                antalos5tot++;


                // -- Sektion slut – (OS8)
                // - OS8
                // - antalos5          - Antal 042: Antal foranstående 042 records
                // - belobos5          - Beløb: Nettobeløb i 042 records
                // - wdispositionsdato - Dispositionsdato
                // - krd.regnr         - Reg.nr.: Overførselsregistreringsnummer
                // - krd.kontonr       - Kontonr.: Overførselskontonummer
                // - krd.datalevnr     - Dataleverandørnr.: Dataleverandørens SE-nummer
                rec = writeOS8(antalos5, belobos5, wdispositionsdato, krd.Regnr, krd.Kontonr, krd.Datalevnr);
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            }

            // -- Leverance slut - (OS9)

            // --OS9 
            // - antalos5tot    - Antal 042: Antal foranstående 042 records
            // - belobos5tot    - Beløb: Nettobeløb i 042 records
            // - krd.datalevnr  - Dataleverandørnr.: Dataleverandørens SE-nummer
            rec = writeOS9(antalos5tot, belobos5tot, krd.Datalevnr);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            return wantaloverforsler;
        }

        private DateTime bankdageplus(DateTime fradato, int antdage)
        {
            DateTime dato;
            int antal = 0;
            bool fridag;
            bool negativ;
            DateTime[] paaskedag = { new DateTime(2010, 4, 4) 
                                     , new DateTime(2011, 4, 24) 
                                     , new DateTime(2012, 4, 8) 
                                     , new DateTime(2013, 3, 31) 
                                     , new DateTime(2014, 4, 20) 
                                     , new DateTime(2015, 4, 5) 
                                     , new DateTime(2016, 3, 27) 
                                     , new DateTime(2017, 4, 16) 
                                     , new DateTime(2018, 4, 1) 
                                     , new DateTime(2019, 4, 21) 
                                     , new DateTime(2020, 4, 12) 
                                   };
            if (antdage < 0)
            {
                negativ = true;
                dato = fradato.AddDays(1);
            }
            else
            {
                negativ = false;
                dato = fradato.AddDays(-1);
            }

            while (antal <= Math.Abs(antdage))
            {
                if (negativ) dato = dato.AddDays(-1);
                else dato = dato.AddDays(1);

                if (dato.DayOfWeek == DayOfWeek.Saturday) fridag = true; //lørdag
                else if (dato.DayOfWeek == DayOfWeek.Sunday) fridag = true; //søndag
                else if ((dato.Month == 1) && (dato.Day == 1)) fridag = true; //1. nytårsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(3))) fridag = true; //skærtorsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(2))) fridag = true; //langfredag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-1))) fridag = true; //2. påskedag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-26))) fridag = true; //st. bededag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-39))) fridag = true; //kristi himmelfartsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-50))) fridag = true; //2. pinsedag
                else if ((dato.Month == 6) && (dato.Day == 5)) fridag = true; //grundlovsdag
                else if ((dato.Month == 12) && (dato.Day == 24)) fridag = true; //juleaften
                else if ((dato.Month == 12) && (dato.Day == 25)) fridag = true; //1. juledag
                else if ((dato.Month == 12) && (dato.Day == 26)) fridag = true; //2. juledag
                else if ((dato.Month == 12) && (dato.Day == 31)) fridag = true; //nytårsaftens dag
                else fridag = false;
                if (!fridag) ++antal;
            }

            return dato;
        }

        private string writeOS1(string datalevnr, int levident)
        {
            string rec = null;
            string kontroltal = "000000000";

            rec += "OS121PBS-OVERFØRSEL";
            rec += lpad(kontroltal, 9, '?');				// Kontroltal for dataleverandør
            rec += lpad(levident, 20, '0');					// Leveranceidentifikation
            rec += rpad("", 3, '0');                        // Filler
            rec += lpad(datalevnr, 8, '?');					// Dataleverandørens CVR-nummer
            rec += rpad("", 1, '0');                        // Leverancekvitering
            rec += rpad("", 20, '0');                       // Filler
            return rec;
        }

        private string writeOS2(DateTime dispdato, string regnr, string kontonr, string datalevnr)
        {
            string rec = null;

            rec += "OS2";                                                   // Recordtype
            rec += lpad("80", 2, '0');                                      // Overførselstype = 80
            rec += rpad("", 26, '0');                                       // Filler
            rec += lpad(String.Format("{0:ddMMyy}", dispdato), 6, '?');     // Dispositionsdato
            rec += lpad(regnr, 4, '0');                                     // Reg.nr.: Betalingsafsender registreringsnummer
            rec += lpad(kontonr, 10, '0');                                  // Kontonr.: Betalingsafsender kontonummer
            rec += lpad(datalevnr, 8, '?');					                // Dataleverandørens CVR-nummer
            rec += lpad(datalevnr, 8, '?');					                // Betalingsafsenders CVR-nummer
            rec += rpad("", 13, '0');                                       // Filler
            return rec;
        }

        private string writeOS5(string bankregnr, string bankkontonr, int belob, DateTime betaldato, string regnr, string kontonr, string advistekst, int modtager)
        {
            string rec = null;

            rec += "OS5";                                               // Recordtype
            rec += lpad("80", 2, '0');                                  // Overførselstype = 80
            rec += lpad(bankregnr, 4, '0');                             // Reg.nr.: Betalingsmodtager registreringsnummer
            rec += lpad(bankkontonr, 10, '0');                          // Kontonr.: Betalingsmodtager kontonummer
            rec += lpad(belob, 12, '0');						        // Beløb uden fortegn i øre
            rec += lpad(String.Format("{0:ddMMyy}", betaldato), 6, '?');// Dispositionsdato
            rec += lpad(regnr, 4, '0');                                 // Reg.nr.: Betalingsafsender registreringsnummer
            rec += lpad(kontonr, 10, '0');                              // Kontonr.: Betalingsafsender kontonummer
            rec += rpad(advistekst, 20, ' ');				            // Tekst på Betalingsmodtagers kontoudtog
            rec += lpad(modtager, 13, '0');					            // Ref til betalingsmodtager til eget brug
            rec += rpad("", 44, '0');                                   // Filler
            return rec;
        }

        private string writeOS8(int antal1, int belob1, DateTime dispdato, string regnr, string kontonr, string datalevnr)
        {
            string rec = null;

            rec += "OS8";
            rec += lpad("80", 2, '0');                                  // Overførselstype = 80
            rec += rpad("", 4, '0');                                    // Filler
            rec += lpad(antal1, 10, '0');					            // Antal overførsler i denne sektion
            rec += lpad(belob1, 12, '0');					            // Totalbeløb denne sektion
            rec += lpad(String.Format("{0:ddMMyy}", dispdato), 6, '?'); // Dispositionsdato
            rec += lpad(regnr, 4, '0');                                 // Reg.nr.: Betalingsafsender registreringsnummer
            rec += lpad(kontonr, 10, '0');                              // Kontonr.: Betalingsafsender kontonummer
            rec += lpad(datalevnr, 8, '?');					            // Dataleverandørens CVR-nummer
            rec += lpad(datalevnr, 8, '?');					            // Betalingsafsenders CVR-nummer
            rec += rpad("", 13, '0');                                   // Filler
            return rec;
        }

        private string writeOS9(int antal2, int belob2, string datalevnr)
        {
            string rec = null;

            rec += "OS929";
            rec += rpad("", 4, '0');                // Filler
            rec += lpad(antal2, 10, '0');			// Total antal overførsler
            rec += lpad(belob2, 12, '0');			// Total beløb
            rec += rpad("", 6, '0');                // Filler
            rec += rpad("", 14, '9');               // Filler
            rec += lpad(datalevnr, 8, '?');			// Betalingsafsenders CVR-nummer
            rec += rpad("", 21, '0');               // Filler
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
    }
}
