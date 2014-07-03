using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2SummaHDA
{
    public class recKontoplan
    {
        public recKontoplan() { }

        public int? Kontonr { get; set; }
        public string Kontonavn { get; set; }
        public string Type { get; set; }
        public string Moms { get; set; }
        public decimal? Saldo { get; set; }
        public string DK { get; set; }
    }

    public class KarKontoplan : List<recKontoplan>
    {
        private string m_path { get; set; }

        public KarKontoplan()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kontoplan.dat";
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
                    int iMax = 11;
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

                    if ((value[2] == "Drift") || (value[2] == "Status"))
                    {
                        rec = new recKontoplan
                        {
                            Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                            Kontonavn = value[1],
                            Type = value[2],
                            Moms = value[3],
                            Saldo = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                            DK = value[9]
                        };
                        this.Add(rec);
                    }
                }
            }
        }
        public static string getMomskode(int? kontonr)
        {
            if (Program.karRegnskab.MomsPeriode() == 2)
                return "";

            try
            {
                return (from m in Program.karKontoplan where m.Kontonr == kontonr select m.Moms).First();
            }
            catch
            {
                return "";
            }
        }

        public static string getKontonavn(int? kontonr)
        {
            try
            {
                return (from m in Program.karKontoplan where m.Kontonr == kontonr select m.Kontonavn).First();
            }
            catch
            {
                return "Konto findes ikke";
            }
        }
    }
}
