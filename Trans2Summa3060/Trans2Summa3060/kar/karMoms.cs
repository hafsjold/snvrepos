using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2Summa3060
{
    public class recMoms
    {
        public recMoms() { }

        public string Momskode { get; set; }
        public decimal? Momspct { get; set; }
        public DateTime? Momsstartdato { get; set; }
        public int? Momskonto { get; set; }
        public string Momsnavn { get; set; }
        public string Momsks { get; set; }
        public int? Momstype { get; set; }
    }

    public class KarMoms : List<recMoms>
    {
        private string m_path { get; set; }

        public KarMoms()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "moms.dat";
            open();
        }

        public void open()
        {
            recMoms rec;
            rec = new recMoms
            {
                Momskode = null
            };
            this.Add(rec);

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
                        rec = new recMoms
                        {
                            Momskode = value[0],
                            Momspct = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? decimal.Parse(value[1]) : (decimal?)null,
                            Momsstartdato = clsUtil.MSSerial2DateTime((double)wDatonr),
                            Momskonto = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? int.Parse(value[3]) : (int?)null,
                            Momsnavn = value[4],
                            Momsks = value[5],
                            Momstype = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? int.Parse(value[6]) : (int?)null

                        };
                        this.Add(rec);
                    }
                }
            }
            ts.Close();
        }

        public static decimal getMomspct(string momskode)
        {
            if (Program.karRegnskab.MomsPeriode() == 2)
                return 0;

            try
            {
                return (decimal)(from m in Program.karMoms where m.Momskode == momskode select m.Momspct).First();
            }
            catch
            {
                return 0;
            }
        }

        public static bool isUdlandsmoms(string momskode)
        {
            try
            {
                if ((int)(from m in Program.karMoms where m.Momskode == momskode select m.Momstype).First() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
