using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsPuls3060
{
    public class recFakturastr_k
    {
        public recFakturastr_k() { }

        public string Fakid { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public int Faknr { get; set; }
        public string Email { get; set; }
    }

    public class KarFakturastr_k : List<recFakturastr_k>
    {
        private string m_path { get; set; }

        public KarFakturastr_k()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "fakturastr_k.dat";
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recFakturastr_k rec;
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
                    int wFakid = int.Parse(value1[0]);
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
                                    value[i++] = m.Groups[j].ToString().Trim();
                                    break;
                                }
                            }
                        }
                    }

                    int wLine = 0;
                    foreach (string ln3 in value)
                    {
                        wLine++;
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

                        rec = new recFakturastr_k
                        {
                            /*
                            Fakid = wFakid,
                            Line = wLine,
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(value3[0]) ? int.Parse(value3[0]) : (int?)null,
                            VareTekst = value3[1],
                            Bogfkonto = Microsoft.VisualBasic.Information.IsNumeric(value3[2]) ? int.Parse(value3[2]) : (int?)null,
                            Antal = Microsoft.VisualBasic.Information.IsNumeric(value3[3]) ? int.Parse(value3[3]) : (int?)null,
                            Enhed = value3[4],
                            Pris = Microsoft.VisualBasic.Information.IsNumeric(value3[5]) ? decimal.Parse(value3[5]) : (decimal?)null,
                            Rabat = Microsoft.VisualBasic.Information.IsNumeric(value3[6]) ? decimal.Parse(value3[6]) : (decimal?)null,
                            Felt07 = value3[7],
                            Felt08 = Microsoft.VisualBasic.Information.IsNumeric(value3[8]) ? decimal.Parse(value3[8]) : (decimal?)null,
                            Fakturabelob = Microsoft.VisualBasic.Information.IsNumeric(value3[9]) ? decimal.Parse(value3[9]) : (decimal?)null,
                            Felt10 = value3[10],
                            Felt11 = value3[11],
                            Felt12 = value3[12],
                            Felt13 = value3[13],
                            Felt14 = value3[14],
                            Felt15 = value3[15],
                            */
                        };
                        this.Add(rec);
                    }
                }
            }
        }
        
        private void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.Append, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                var qry_this = from d in this
                               orderby d.Fakid
                               select d;
                foreach (var rec in qry_this)
                {
                    string ln = rec.Fakid + @"=""""""" + rec.Navn + @""""",""""" + rec.Adresse + @""""",""""" + rec.Postnr + " " + rec.Bynavn + @""""""",,,," + rec.Faknr + @",,,,," + rec.Email + @",,,";
                    sr.WriteLine(ln);
                }
            }

        }

    }
}
