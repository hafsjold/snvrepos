
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPbs3060
{
    public class clsKontingent
    {
        public decimal Kontingent { get; set; }
        public DateTime KontingentTildato { get; set; }

        private clsKontingent() { }

        public clsKontingent(dbData3060DataContext p_dbData3060, DateTime startdato)
        {
            this.BeregnKontingent(p_dbData3060, startdato);
        }

        private void BeregnKontingent(dbData3060DataContext p_dbData3060, DateTime startdato)
        {
            decimal kont = 0;
            DateTime KontingentFradato = startdato;
            DateTime KontingentTildato = KontingentFradato.AddYears(1).AddDays(-1);

            var qry1 = from k in p_dbData3060.tblKontingents
                       where k.startdato.Date >= KontingentFradato.Date || k.slutdato.Date >= KontingentFradato.Date
                       orderby k.startdato
                       select k;

            int antal = qry1.Count();

            int n = 1;
            foreach (var k in qry1)
            {
                if (n == 1)
                    kont += k.aarskontingent;
                n++;
            }
            this.Kontingent = kont;
            this.KontingentTildato = KontingentTildato;
        }

    }
}
