using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsPuls3060
{
    public class recStatus
    {
        public recStatus() { }
        public string key { get; set; }
        public string value { get; set; }
    }

    public class KarStatus : List<recStatus>
    {
        private DbData3060 m_dbData3060;
        private string m_path { get; set; }

        public KarStatus(DbData3060 pdbData3060)
        {
            m_dbData3060 = pdbData3060;
            var rec_regnskab = (from r in m_dbData3060.TblRegnskab
                                join a in m_dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                select r).First();

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
                    rec = new recStatus { key = X[0], value = X[1] };
                    this.Add(rec);
                }
            }
        }

        public void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Write, FileShare.None);
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

    }
}
