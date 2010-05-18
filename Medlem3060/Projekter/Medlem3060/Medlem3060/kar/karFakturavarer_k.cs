using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace nsPuls3060
{
    public class recFakturavarer_k
    {
        public recFakturavarer_k() { }

        public string Fakid { get; set; }
        public int Varenr { get; set; }
        public string VareTekst { get; set; }
        public int Bogfkonto { get; set; }
        public int Antal { get; set; }
        public double Fakturabelob { get; set; }
    }

    public class KarFakturavarer_k : List<recFakturavarer_k>
    {
        private string m_path { get; set; }

        public KarFakturavarer_k()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "fakturavarer_k.dat";
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recFakturavarer_k rec;
            RegexOptions options = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            Regex regexFaknr = new Regex("(^[0-9]*)=(.*$)", options);
            Regex regexFakturavarer = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", options);
            Regex regexFakline = new Regex(@"""""(.*?)"""",|([^,]*),|(.*)$");

            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    MatchCollection collFaknr = regexFaknr.Matches(ln);
                    string[] value1 = new string[2];

                    foreach (Match m in collFaknr)
                    {
                        for (int j = 1; j <= 2; j++)
                        {
                            if (m.Groups[j].Success)
                            {
                                value1[j - 1] = m.Groups[j].ToString().Trim();
                            }
                        }
                    }
                    string faknr = value1[0];
                    string ln2 = value1[1];

                    MatchCollection collFakturavarer = regexFakturavarer.Matches(ln2);
                    int i = 0;
                    int iMax = collFakturavarer.Count;
                    string[] value = new string[iMax];
                    foreach (Match m in collFakturavarer)
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            if (m.Groups[j].Success)
                            {
                                if (i < iMax)
                                {
                                    value[i++] = m.Groups[j].ToString().Trim().Trim('"');
                                    break;
                                }
                            }
                        }
                    }

                    foreach (string ln3 in value)
                    {
                        MatchCollection collFaktline = regexFakline.Matches(ln3);
                        int n = 0;
                        int nMax = collFaktline.Count;
                        string[] value3 = new string[nMax];
                        foreach (Match m in collFaktline)
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                if (m.Groups[j].Success)
                                {
                                    if (n < nMax)
                                    {
                                        value3[n++] = m.Groups[j].ToString().Trim().Trim('"');
                                        break;
                                    }
                                }
                            }
                        }
                        int xyztest = 1;

                        /*
                        rec = new recFakturavarer_k
                        {
                            Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                            Kontonavn = value[1],
                            Type = value[2],
                            Moms = value[3],
                            Saldo = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                            DK = value[9]
                        };
                        this.Add(rec);
                        */
                    }
                }
            }
        }


        public void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.Append, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                var qry_this = from d in this
                               orderby d.Fakid
                               select d;
                foreach (var rec in qry_this)
                {
                    string ln = rec.Fakid + @"=""" + rec.Varenr + @",""""" + rec.VareTekst + @"""""," + rec.Bogfkonto + "," + rec.Antal + @",,""""" + rec.Fakturabelob + @",00"""",,,""""" + rec.Fakturabelob + @",00"""",""""" + rec.Fakturabelob + @",00"""",0,,,,,""";
                    sr.WriteLine(ln);
                }
            }

        }

    }
}
