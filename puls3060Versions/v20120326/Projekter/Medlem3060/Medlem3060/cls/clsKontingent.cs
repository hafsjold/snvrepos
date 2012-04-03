using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    public class clsKontingent
    {
        public decimal Kontingent { get; set; }
        public DateTime KontingentTildato { get; set; }

        private clsKontingent() { }

        public clsKontingent(DateTime startdato, int? Nr)
        {
            DateTime? fodtdato;
            try
            {
                fodtdato = (from m in Program.XdbData3060.tblMedlems where m.Nr == Nr select m.FodtDato).First();

            }
            catch
            {
                fodtdato = null;
            } 
            this.BeregnKontingent(startdato, fodtdato); 
        }

        public clsKontingent(DateTime startdato, DateTime? fodtdato)
        {
            this.BeregnKontingent(startdato, fodtdato);
        }

        private void BeregnKontingent(DateTime startdato, DateTime? fodtdato) 
        {
            int alder;
            if (fodtdato == null) 
            {
                alder = 40;
            } 
            else 
            {
                try
                {
                    alder = startdato.Subtract((DateTime)fodtdato).Days / 365;

                }
                catch
                {
                    alder = 40;
                }
            }
            
            decimal kont = 0;
            bool HalfPlusNextYear;
            DateTime KontingentFradato;
            DateTime KontingentTildato;
            switch (startdato.Month)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    KontingentFradato = new DateTime(startdato.Year, 1, 1);
                    KontingentTildato = new DateTime(startdato.Year, 12, 31);
                    HalfPlusNextYear = false;
                    break;

                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    KontingentFradato = new DateTime(startdato.Year, 7, 1);
                    KontingentTildato = new DateTime(startdato.Year + 1, 12, 31);
                    HalfPlusNextYear = true;
                    break;

                default:
                    KontingentFradato = new DateTime(startdato.Year + 1, 1, 1);
                    KontingentTildato = new DateTime(startdato.Year + 1, 12, 31);
                    HalfPlusNextYear = false;
                    break;
            }

            var qry1 = from k in Program.XdbData3060.tblKontingents
                      where (k.startalder <= alder && k.slutalder >= alder) && (k.startdato.Date >= KontingentFradato.Date || k.slutdato.Date >= KontingentFradato.Date)
                      select k;

            int antal = qry1.Count();

            var qry2 = from k in qry1
                       where k.slutdato.Date <= KontingentTildato
                       orderby k.startdato
                       select k;

            int antal2 = qry2.Count();

            int n = 1;
            foreach (var k in qry2) 
            {
                if ((n == 1) && (HalfPlusNextYear))
                    kont += k.aarskontingent/2;
                else
                    kont += k.aarskontingent;

                n++;
            }
            this.Kontingent = kont;
            this.KontingentTildato = KontingentTildato;
        }
    }
}
