using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace nsPuls3060
{
    public class recVarer
    {
        public recVarer() { }

        public int? Varenr { get; set; }
        public string Varenavn { get; set; }
        public string Enhed { get; set; }
        public decimal? Salgspris { get; set; }
        public int? Salgskonto { get; set; }
    }

    class KarVarer : List<recVarer>
    {
        private string m_path { get; set; }
        public KarVarer()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "varer.dat";
            open();
        }

        public void open()
        {
            recVarer rec;
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            Regex regexKontoplan = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 7;
                    string[] value = new string[iMax];
                    foreach (Match m in regexKontoplan.Matches(ln))
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

                    if (!value[0].StartsWith("*"))
                    {
                        int? wDatonr = Microsoft.VisualBasic.Information.IsNumeric(value[2]) ? int.Parse(value[2]) : (int?)null;
                        rec = new recVarer
                        {
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                            Varenavn = value[1],
                            Enhed = value[2],
                            Salgspris = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                            Salgskonto = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? int.Parse(value[4]) : (int?)null,
                        };
                        this.Add(rec);
                    }
                }
            }
            ts.Close();
        }

    }
}
