using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Trans2SummaHDC
{
    public class recTrans2Summa
    {
        public recTrans2Summa() { }
        public string key { get; set; }
        public string value { get; set; }
    }

    public class KarTrans2Summa : List<recTrans2Summa>
    {
        private string m_path { get; set; }

        public KarTrans2Summa()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "trans2summa.dat";
            open();
        }

        private void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recTrans2Summa rec;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    string[] X = ln.Split('=');
                    rec = new recTrans2Summa { key = X[0], value = X[1] };
                    this.Add(rec);
                }
            }
        }

        public void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.Truncate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                var qry_this = from d in this
                               orderby d.key
                               select d;
                foreach (var rec in qry_this)
                {
                    sr.WriteLine(rec.key + "=" + rec.value);
                }
            }

        }

        public string BilagPath()
        {
            try
            {
                return (from r in this where r.key == "BilagPath" select r.value).First();
            }
            catch
            {
                return "";
            }
        }

    }
}
