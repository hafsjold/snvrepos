using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2SummaHDC
{
    public class recAfstemningskonto
    {
        public recAfstemningskonto() { }

        public string Kontonavn { get; set; }
    }

    public class KarAfstemningskonti : List<recAfstemningskonto>
    {
        public KarAfstemningskonti()
        {
            open();
        }

        public void open()
        {
            recAfstemningskonto rec;
            rec = new recAfstemningskonto { Kontonavn = null };
            this.Add(rec);

            KarStatus objStatus = new KarStatus();
            string strAfstemKonti = (from s in Program.karRegnskab where s.key == "AfstemKonti" select s.value).First();
            string[] strarrAfstemKonti = strAfstemKonti.Split(',');
            int?[] intarrAfstemKonti = new int?[strarrAfstemKonti.Length];
            for (int i = 0; i < strarrAfstemKonti.Length; i++)
            {
                intarrAfstemKonti[i] = int.Parse(strarrAfstemKonti[i]);
            }
            var qry = from k in Program.karKontoplan
                      where intarrAfstemKonti.Contains(k.Kontonr)
                      select k.Kontonavn;

            foreach (string s in qry)
            {
                rec = new recAfstemningskonto { Kontonavn = s };
                this.Add(rec);
            }
        }
    }

}