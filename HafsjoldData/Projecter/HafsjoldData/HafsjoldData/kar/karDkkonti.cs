using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsHafsjoldData
{
    public class recDkkonti
    {
        public recDkkonti() { }
        public string Debnr { get; set; }
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
                    rec = new recDkkonti { Debnr = ln };
                    this.Add(rec);
                }
            }
        }

        public void save()
        {
            var maxNr = (from m in Program.karMedlemmer select m.Nr).Max();
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                for (var i = 100000; i <= (100000 + maxNr); i++)
                {
                    sr.WriteLine(i.ToString());
                }
            }
        }

    }
}
