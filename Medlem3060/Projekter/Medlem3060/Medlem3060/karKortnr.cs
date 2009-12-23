using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsPuls3060
{
    public class recKortnr
    {
        public recKortnr() { }
        public string Nr { get; set; }
    }

    public class KarKortnr : List<recKortnr>
    {
        private DbData3060 m_dbData3060;
        private string m_path { get; set; }

        public KarKortnr(DbData3060 pdbData3060)
        {
            m_dbData3060 = pdbData3060;
            var rec_regnskab = (from r in m_dbData3060.TblRegnskab
                                join a in m_dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                select r).First();

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
                    rec = new recKortnr { Nr = ln };
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
                               orderby d.Nr
                               select d;
                foreach (var rec in qry_this)
                {
                    sr.WriteLine(rec.Nr);
                }
            }

        }

    }
}
