using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsHafsjoldData
{
    public class recKartotek
    {
        public recKartotek() { }

        public string Kontonavn { get; set; }
        public int? DebKonto { get; set; }
        public int? KrdKonto { get; set; }
        public decimal? DebSaldo { get; set; }
        public decimal? KrdSaldo { get; set; }
    }

    public class KarKartotek : List<recKartotek>
    {
        private string m_path { get; set; }

        public KarKartotek()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kartotek.dat";
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recKartotek rec;
            Regex regexKartotek = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 30;
                    string[] value = new string[iMax];
                    foreach (Match m in regexKartotek.Matches(ln))
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            if (m.Groups[j].Success)
                            {
                                if (i < iMax)
                                {
                                    value[i++] = m.Groups[j].ToString();
                                    break;
                                }
                            }
                        }
                    }

                    rec = new recKartotek
                    {
                        Kontonavn = value[1],
                        DebKonto = Microsoft.VisualBasic.Information.IsNumeric(value[19]) ? int.Parse(value[19]) : (int?)null,
                        KrdKonto = Microsoft.VisualBasic.Information.IsNumeric(value[20]) ? int.Parse(value[20]) : (int?)null,
                        DebSaldo = Microsoft.VisualBasic.Information.IsNumeric(value[23]) ? decimal.Parse(value[23]) : (decimal?)null,
                        KrdSaldo = Microsoft.VisualBasic.Information.IsNumeric(value[24]) ? decimal.Parse(value[24]) : (decimal?)null
                    };
                    this.Add(rec);
                }
            }
        }

    }
}
