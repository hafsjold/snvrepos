using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Trans2Summa3060
{
    public class recKortnr
    {
        public recKortnr() { }
        public int Nr { get; set; }
    }

    public class KarKortnr : List<recKortnr>
    {
        private string m_path { get; set; }

        public KarKortnr()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kortnr.dat";
            open();
        }

        private void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recKortnr rec;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    rec = new recKortnr { Nr = int.Parse(ln) };
                    this.Add(rec);
                }
            }
        }

        public static int nextval()
        {
            try
            {
                int maxNr = (from k in Program.karKortnr orderby k.Nr descending select k.Nr).First();
                maxNr++;
                recKortnr rec = new recKortnr { Nr = maxNr };
                Program.karKortnr.Add(rec);
                return maxNr;

            }
            catch (Exception)
            {
                int maxNr = 1;
                recKortnr rec = new recKortnr { Nr = maxNr };
                Program.karKortnr.Add(rec);
                return maxNr;
            }
        }

        public void save()
        {
            var qry = from k in this orderby k.Nr select k;
            FileStream ts = new FileStream(m_path, FileMode.Truncate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                foreach (var i in qry)
                {
                    sr.WriteLine(i.Nr.ToString());
                }
            }

        }

    }
}
