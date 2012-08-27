using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Trans2Summa
{
    public class recStatus
    {
        public recStatus() { }
        public string key { get; set; }
        public string value { get; set; }
    }

    public class KarStatus : List<recStatus>
    {
        private string m_path { get; set; }

        public KarStatus()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "status.dat";
            open();
        }

        private void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recStatus rec;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    string[] X = ln.Split('=');
                    try
                    {
                        rec = new recStatus { key = X[0], value = X[1] };
                        this.Add(rec);
                    }
                    catch { }
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

        public int BS1_NæsteNr()
        {
            recStatus rec;
            int BS1_SidsteNr;
            try
            {
                rec = (from r in this where r.key == "BS1_SidsteNr" select r).First();
                BS1_SidsteNr = int.Parse(rec.value);
                BS1_SidsteNr++;
                rec.value = BS1_SidsteNr.ToString();
                return BS1_SidsteNr;
            }
            catch
            {
                rec = new recStatus
                {
                    key = "BS1_SidsteNr",
                    value = "1"
                };
                this.Add(rec);
                return 1;
            }


        }
    }
}
