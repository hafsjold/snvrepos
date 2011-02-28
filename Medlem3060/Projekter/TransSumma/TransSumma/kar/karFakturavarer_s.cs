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
        public double Fakturabelob { get; set; }
    }

    public class KarFakturavarer_s : List<recFakturavarer_s>
    {
        private string m_path { get; set; }

        public KarFakturavarer_s()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "fakturavarer_s.dat";
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
                    string ln = rec.Fakid + @"=""" + rec.Varenr + @",""""" + rec.VareTekst + @"""""," + rec.Bogfkonto + "," + rec.Antal + @",,""""" + rec.Fakturabelob + @",00"""",,,""""" + rec.Fakturabelob + @",00"""",""""" + rec.Fakturabelob + @",00"""",0,,,,,""";
                    sr.WriteLine(ln);
                }
            }

        }

    }
}
