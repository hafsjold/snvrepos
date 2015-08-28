using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace Trans2SummaHDC
{
    public class recKartotek
    {
        public recKartotek() { }

        public int? Nr { get; set; }
        public int? Kontonr { get; set; }
        public string Kontonavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Moms { get; set; }
        public decimal? Saldo { get; set; }
        public string DK { get; set; }
        public string Cvrnr { get; set; }
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
            recKartotek rec;
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            Regex regexKontoplan = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            Regex regexAdresse = new Regex(@"""""(.*?)"""",|([^,]*),|(.*)$");
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

                    MatchCollection collAdresse = regexAdresse.Matches(value[2]);
                    int n = 0;
                    int nMax = collAdresse.Count;
                    string[] value2 = new string[nMax];
                    foreach (Match m in collAdresse)
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            if (m.Groups[j].Success)
                            {
                                if (n < nMax)
                                {
                                    value2[n++] = m.Groups[j].ToString().Trim().Trim('"');
                                    break;
                                }
                            }
                        }
                    }

                    if (value[21] == "1") //Debitor
                    {
                        rec = new recKartotek
                        {
                            Nr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                            Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[19]) ? int.Parse(value[19]) : (int?)null,
                            Kontonavn = value[1],
                            Adresse = value2[0],
                            Postnr = value[3],
                            Bynavn = value[4],
                            Email = value[8],
                            Type = "Debitor",
                            Saldo = Microsoft.VisualBasic.Information.IsNumeric(value[23]) ? decimal.Parse(value[23]) : (decimal?)null,
                            DK = "0",
                            Cvrnr = value[14]
                        };
                        this.Add(rec);
                    }
                    if (value[22] == "1") //Kreditor
                    {
                        rec = new recKartotek
                        {
                            Nr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                            Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[20]) ? int.Parse(value[20]) : (int?)null,
                            Kontonavn = value[1],
                            Adresse = value2[0],
                            Postnr = value[3],
                            Bynavn = value[4],
                            Email = value[8],
                            Type = "Kreditor",
                            Saldo = Microsoft.VisualBasic.Information.IsNumeric(value[24]) ? decimal.Parse(value[24]) : (decimal?)null,
                            DK = "1",
                            Cvrnr = value[14]
                        };
                        this.Add(rec);
                    }
                }
            }
        }
    }
}
