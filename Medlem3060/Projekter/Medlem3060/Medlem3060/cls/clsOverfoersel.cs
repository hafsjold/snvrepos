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
