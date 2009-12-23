using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsPuls3060
{
    public class recFakturavarer_s
    {
public recFakturavarer_s() { }
        public string Fakid { get; set; }
        public int Varenr { get; set; }
        public string VareTekst { get; set; }
        public int Bogfkonto { get; set; }
        public int Antal { get; set; }
        public double Indbetalingsbelob { get; set; }
    }

    public class KarFakturavarer_s : List<recFakturavarer_s>
    {
        private DbData3060 m_dbData3060;
        private string m_path { get; set; }

        public KarFakturavarer_s(DbData3060 pdbData3060)
        {
            m_dbData3060 = pdbData3060;
            var rec_regnskab = (from r in m_dbData3060.TblRegnskab
                                join a in m_dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                select r).First();

            m_path = rec_regnskab.Placering + "fakturavarer_s.dat";
            open();
        }

        private void open()
        {
            /*
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recFakturavarer_s rec;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    //rec = new recFakturavarer_s { Fakid = ln };
                    //this.Add(rec);
                }
            }
            */
        }

        public void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.Append, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                var qry_this = from d in this
                               orderby d.Fakid
                               select d;
                foreach (var rec in qry_this)
                {
                    string ln = rec.Fakid + @"=""" + rec.Varenr + @",""""" + rec.VareTekst + @"""""," + rec.Bogfkonto + "," + rec.Antal + @",,""""" + rec.Indbetalingsbelob + @",00"""",,,""""" + rec.Indbetalingsbelob + @",00"""",""""" + rec.Indbetalingsbelob + @",00"""",0,,,,,""";
                    sr.WriteLine(ln);
                }
            }

        }

    }
}
