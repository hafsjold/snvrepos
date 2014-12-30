using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsPuls3060v2
{
    public class recKladde
    {
        public recKladde() { }

        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public string Afstemningskonto { get; set; }
        public decimal? Belob { get; set; }
        public int? Kontonr { get; set; }
        public int? Faknr { get; set; }
    }

    public class KarKladde : List<recKladde>
    {
        private string m_path { get; set; }

        public KarKladde()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kladde.dat";
        }

        public void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                var qry_this = from d in this select d;
                foreach (var b in qry_this)
                {
                    string ln = "";
                    //ln += (b.Dato == null) ? "," : String.Format("{0:yyyy-MM-dd}",b.Dato) + ",";
                    ln += (b.Dato == null) ? "," : (int)clsUtil.MSDateTime2Serial((DateTime)b.Dato) + ",";
                    ln += (b.Bilag == null) ? "," : b.Bilag.ToString() + ",";
                    ln += (b.Tekst == null) ? "," : @"""" + b.Tekst + @""",";
                    ln += (b.Afstemningskonto == null) ? "," : @"""" + b.Afstemningskonto + @""",";
                    ln += (b.Belob == null) ? "," : @"""" + ((decimal)(b.Belob)).ToString("0.00") + @""",";
                    ln += (b.Kontonr == null) ? ",," : b.Kontonr.ToString() + ",,";
                    ln += (b.Faknr == null) ? ",0," : b.Faknr.ToString() + ",0,";
                    sr.WriteLine(ln);
                }
            }

        }

    }
}
