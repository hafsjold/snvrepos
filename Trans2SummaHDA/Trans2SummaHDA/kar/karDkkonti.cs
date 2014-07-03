using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Trans2SummaHDA
{
    public class recDkkonti
    {
        public recDkkonti() { }
        public int Debnr { get; set; }
    }

    public class KarDkkonti : List<recDkkonti>
    {
        private string m_path { get; set; }

        public KarDkkonti()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "dkkonti.dat";
            open();
        }

        private void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recDkkonti rec;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    rec = new recDkkonti { Debnr = int.Parse(ln) };
                    this.Add(rec);
                }
            }
        }

        public static int nextval()
        {
            try
            {
                int maxNr = (from k in Program.karDkkonti orderby k.Debnr descending select k.Debnr).First();
                maxNr++;
                recDkkonti rec = new recDkkonti { Debnr = maxNr };
                Program.karDkkonti.Add(rec);
                return maxNr;

            }
            catch (Exception)
            {
                int maxNr = 1;
                recDkkonti rec = new recDkkonti { Debnr = maxNr };
                Program.karDkkonti.Add(rec);
                return maxNr;
            }
        }

        public void save()
        {
            var qry = from k in this orderby k.Debnr select k;
            FileStream ts = new FileStream(m_path, FileMode.Truncate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                foreach (var i in qry)
                {
                    sr.WriteLine(i.Debnr.ToString());
                }
            }
        }

    }
}
