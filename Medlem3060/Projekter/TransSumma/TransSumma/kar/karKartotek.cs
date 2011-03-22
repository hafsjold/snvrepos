using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace nsPuls3060
{
    
    public class KarKartotek : List<recKontoplan>
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
            recKontoplan rec;
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            Regex regexKontoplan = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 30;
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

                    if (value[21] == "1") //Debitor
                    {
                        rec = new recKontoplan
                        {
                            Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[19]) ? int.Parse(value[19]) : (int?)null,
                            Kontonavn = value[1],
                            Type = "Debitor",
                            Saldo = Microsoft.VisualBasic.Information.IsNumeric(value[23]) ? decimal.Parse(value[23]) : (decimal?)null,
                            DK = "0"
                        };
                        this.Add(rec);
                    }
                    if (value[22] == "1") //Kreditor
                    {
                        rec = new recKontoplan
                        {
                            Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[20]) ? int.Parse(value[20]) : (int?)null,
                            Kontonavn = value[1],
                            Type = "Kreditor",
                            Saldo = Microsoft.VisualBasic.Information.IsNumeric(value[24]) ? decimal.Parse(value[24]) : (decimal?)null,
                            DK = "1"
                        };
                        this.Add(rec);
                    }
                }
            }
        }
    }
}
