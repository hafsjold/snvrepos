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
    class clsOverfoersel
    {

        public void overfoersel()
        {
            int lobnr;
            int seq = 0;
            //rec varchar(128);
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

        private string writeOS1(string datalevnr, string levident)
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
