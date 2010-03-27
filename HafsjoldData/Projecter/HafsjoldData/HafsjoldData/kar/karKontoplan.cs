using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsHafsjoldData
{
    public class recKontoplan
    {
        public recKontoplan() { }

        public int? Kontonr { get; set; }
        public string Kontonavn { get; set; }
        public string Type { get; set; }
        public string Moms { get; set; }
        public decimal? Saldo { get; set; }
    }

    public class KarKontoplan : List<recKontoplan>
    {
        private string m_path { get; set; }

        public KarKontoplan()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kontoplan.dat";
        }

        private void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recKontoplan rec;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    string[] X = ln.Split('=');
                    rec = new recKontoplan { 
                        //key = X[0], 
                        //value = X[1] 
                    };
                    this.Add(rec);
                }
            }
        }

    }
}
