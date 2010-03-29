using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsHafsjoldData
{
    public class recDanskeErhverv
    {
        public recDanskeErhverv() { }

        public DateTime? Dato { get; set; }
        public string Tekst { get; set; }
        public decimal? Belob { get; set; }
        public decimal? Saldo { get; set; }
    }

    public class KarDanskeErhverv : List<recDanskeErhverv>
    {
        private string m_path { get; set; }

        public KarDanskeErhverv()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + "DanskeErhver.csv";
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            int lnr = 0;

            recDanskeErhverv rec;
            Regex regexDanskeErhverv = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    lnr++;
                    int i = 0;
                    int iMax = 10;
                    string[] value = new string[iMax];
                    foreach (Match m in regexDanskeErhverv.Matches(ln))
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

                    if ((lnr > 1) && (value[4] == "Udført"))
                    {
                        rec = new recDanskeErhverv
                        {
                            Dato = Microsoft.VisualBasic.Information.IsDate(value[0]) ? DateTime.Parse(value[0]) : (DateTime?)null,
                            Tekst = value[1],
                            Belob = Microsoft.VisualBasic.Information.IsNumeric(value[2]) ? decimal.Parse(value[2]) : (decimal?)null,
                            Saldo = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null
                        };
                        this.Add(rec);
                    }
                }
            }
        }

    }
}
