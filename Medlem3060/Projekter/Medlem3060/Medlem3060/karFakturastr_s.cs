using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsPuls3060
{
    public class recFakturastr_s
    {
        public recFakturastr_s() { }
        public string Fakid { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public int Faknr { get; set; }
        public string Email { get; set; }
    }

    public class KarFakturastr_s : List<recFakturastr_s>
    {
        private DbData3060 m_dbData3060;
        private string m_path { get; set; }

        public KarFakturastr_s(DbData3060 pdbData3060)
        {
            m_dbData3060 = pdbData3060;
            var rec_regnskab = (from r in m_dbData3060.TblRegnskab
                                join a in m_dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                select r).First();

            m_path = rec_regnskab.Placering + "fakturastr_s.dat";
            open();
        }

        private void open()
        {
            /*
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recFakturastr_s rec;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    //rec = new recFakturastr_s { Fakid = ln };
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
                    string ln = rec.Fakid + @"=""""""" + rec.Navn + @""""",""""" + rec.Adresse + @""""",""""" + rec.Postnr + " " + rec.Bynavn + @""""""",,,," + rec.Faknr + @",,,,," + rec.Email + @",,,";
                    sr.WriteLine(ln);
                }
            }

        }

    }
}
