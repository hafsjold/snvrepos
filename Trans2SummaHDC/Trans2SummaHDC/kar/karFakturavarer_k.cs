using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Trans2SummaHDC
{
    public class recFakturavarer_k
    {
        public recFakturavarer_k() { }

        public int Fakid { get; set; }
        public int Line { get; set; }
        public int? Varenr { get; set; }
        public string VareTekst { get; set; }
        public int? Bogfkonto { get; set; }
        public decimal? Antal { get; set; }
        public string Enhed { get; set; }
        public decimal? Pris { get; set; }
        public decimal? Rabat { get; set; }
        public decimal? Moms { get; set; }
        public decimal? Nettobelob { get; set; }
        public decimal? Bruttobelob { get; set; }
        public decimal? Momspct { get; set; }
        public string Felt11 { get; set; }
        public string Felt12 { get; set; }
        public string Felt13 { get; set; }
        public string Felt14 { get; set; }
        public string Felt15 { get; set; }
    }

    public class KarFakturavarer_k : List<recFakturavarer_k>
    {
        private string m_path { get; set; }

        public KarFakturavarer_k()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "fakturavarer_k.dat";
            open();
        }

        public KarFakturavarer_k(bool Append)
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
                                    value[i++] = m.Groups[j].ToString().Trim().Trim('"');
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

                        rec = new recFakturavarer_k
                        {
                            Fakid = wFakid,
                            Line = wLine,
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(value3[0]) ? int.Parse(value3[0]) : (int?)null,
                            VareTekst = value3[1],
                            Bogfkonto = Microsoft.VisualBasic.Information.IsNumeric(value3[2]) ? int.Parse(value3[2]) : (int?)null,
                            Antal = Microsoft.VisualBasic.Information.IsNumeric(value3[3]) ? decimal.Parse(value3[3]) : (decimal?)null,
                            Enhed = value3[4],
                            Pris = Microsoft.VisualBasic.Information.IsNumeric(value3[5]) ? decimal.Parse(value3[5]) : (decimal?)null,
                            Rabat = Microsoft.VisualBasic.Information.IsNumeric(value3[6]) ? decimal.Parse(value3[6]) : (decimal?)null,
                            Moms = Microsoft.VisualBasic.Information.IsNumeric(value3[7]) ? decimal.Parse(value3[7]) : (decimal?)null,
                            Nettobelob = Microsoft.VisualBasic.Information.IsNumeric(value3[8]) ? decimal.Parse(value3[8]) : (decimal?)null,
                            Bruttobelob = Microsoft.VisualBasic.Information.IsNumeric(value3[9]) ? decimal.Parse(value3[9]) : (decimal?)null,
                            Momspct = Microsoft.VisualBasic.Information.IsNumeric(value3[10]) ? decimal.Parse(value3[10]) : (decimal?)null,
                            Felt11 = value3[11],
                            Felt12 = value3[12],
                            Felt13 = value3[13],
                            Felt14 = value3[14],
                            Felt15 = value3[15],
                        };
                        this.Add(rec);
                    }
                }
            }
        }


        public void save()
        {
            FileStream ts = new FileStream(m_path, FileMode.Append, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                int lastFakid = 0;
                string ln = "";
                var qry_this = from d in this
                               orderby d.Fakid, d.Line
                               select d;

                foreach (var rec in qry_this)
                {
                    if ((lastFakid != 0) && (lastFakid != rec.Fakid))
                    {
                        string lnx = lastFakid + @"=" + ln;
                        sr.WriteLine(lnx);
                        ln = "";
                    }

                    string wantal = rec.Antal.ToString();
                    if (wantal.Contains(","))
                    {
                        string yantal = wantal.TrimEnd('0').TrimEnd(',');
                        wantal = yantal;
                    }
                    string vline = quote(rec.Varenr.ToString(), 2)  //#0
                         + ","
                         + quote(rec.VareTekst, 2)  //#1
                         + ","
                         + quote(rec.Bogfkonto.ToString(), 2) //#2
                         + ","
                         + quote(wantal, 2) //#3
                         + ","
                         + quote(rec.Enhed, 2) //#4
                         + ","
                         + quote(fmt(rec.Pris), 2) //#5
                         + ","
                         + quote(fmt(rec.Rabat), 2)  //#6
                         + ","
                         + quote(fmt(rec.Moms), 2)  //#7
                         + ","
                         + quote(fmt(rec.Nettobelob), 2)  //#8
                         + ","
                         + quote(fmt(rec.Bruttobelob), 2) //#9
                         + ","
                         + quote(rec.Momspct.ToString(), 2) //#10
                         + ",,,,,";
                    if (ln.Length > 0)
                        ln += "," + quote(vline, 1);
                    else
                        ln = quote(vline, 1);

                    lastFakid = rec.Fakid;
                }

                if (ln.Length > 0)
                {
                    string lnx = lastFakid + @"=" + ln;
                    sr.WriteLine(lnx);
                }
            }
        }

        private string fmt(decimal? dec)
        {
            if (dec == null)
                return "";
            else
                return ((decimal)dec).ToString("N2");
        }

        private string quote(string val, int antalQuotes)
        {
            string s;
            if (val == null)
                s = "";
            else
                s = val;

            s = s.Trim();
            if (s.IndexOf(' ') == -1 && s.IndexOf(',') == -1 && s.IndexOf('"') == -1)
            {
                return s;
            }
            else
            {
                if (antalQuotes == 1)
                {
                    return "\"" + s + "\"";
                }
                else
                {
                    return "\"\"" + s + "\"\"";
                }
            }
        }

    }
}
