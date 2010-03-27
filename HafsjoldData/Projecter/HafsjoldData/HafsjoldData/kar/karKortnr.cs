using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsHafsjoldData
{
    public class recKortnr
    {
        public recKortnr() { }
        public string Nr { get; set; }
    }

    public class KarKortnr : List<recKortnr>
    {
        private string m_path { get; set; }

        public KarKortnr()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
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
            var maxNr = (from m in Program.karMedlemmer select m.Nr).Max();
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                for (var i = 1; i <= maxNr; i++)
                {
                    sr.WriteLine(i.ToString());
                }
            }

        }

    }
}
