using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Trans2SummaHDA
{
    public class recFakturastr_s
    {
        public recFakturastr_s() { }

        public string Fakid { get; set; }
        public clsNavnAdresse FakNavnAdresse { get; set; }
        public string Email { get; set; }
        public string Cvrnr { get; set; }
    }

    public class KarFakturastr_s : List<recFakturastr_s>
    {
        private string m_path { get; set; }

        public KarFakturastr_s()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "fakturastr_s.dat";
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
                    string ln = rec.Fakid + @"=""" + rec.FakNavnAdresse.getCsv() + @""",,,,,,,,," + rec.Email + @"," + rec.Cvrnr + ",,";
                    sr.WriteLine(ln);
                }
            }

        }

    }
}
