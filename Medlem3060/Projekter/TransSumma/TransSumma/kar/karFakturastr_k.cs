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

        public int Fakid { get; set; }
        public string FakNavnAdresse { get; set; }
        public string LevNavnAdresse { get; set; }
        public string Note { get; set; }
        public string Felt03 { get; set; }
        public string Felt04 { get; set; }
        public string Felt05 { get; set; }
        public string Felt06 { get; set; }
        public string Felt07 { get; set; }
        public string Felt08 { get; set; }
        public string DeresRef { get; set; }
        public string Felt10 { get; set; }
        public string Felt11 { get; set; }
        public string Felt12 { get; set; }
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
            Regex regexFakturastr = new Regex("(?:^|,)(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^,]*)", options);

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

                    MatchCollection collFakturastr = regexFakturastr.Matches(ln2);
                    int i = 0;
                    int iMax = collFakturastr.Count;
                    string[] value = new string[iMax];
                    foreach (Match m in collFakturastr)
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


                    rec = new recFakturastr_k
                    {
                        Fakid = wFakid,
                        FakNavnAdresse = value[0],
                        LevNavnAdresse = value[1],
                        Note = value[2],
                        Felt03 = value[3],
                        Felt04 = value[4],
                        Felt05 = value[5],
                        Felt06 = value[6],
                        Felt07 = value[7],
                        Felt08 = value[8],
                        DeresRef = value[9],
                        Felt10 = value[10],
                        Felt11 = value[11],
                        Felt12 = value[12],
                    };
                    this.Add(rec);
                }
            }
        }
    }
}
