using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2Summa3060
{
    public class recNyKontoplan
    {
        public recNyKontoplan() { }

        public int? Kontonr { get; set; }
        public string NytKontonr { get; set; }
    }

    public class KarNyKontoplan : List<recNyKontoplan>
    {
        private string m_path { get; set; }

        public KarNyKontoplan()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            //m_path = rec_regnskab.Placering + "nykontoplan.dat";
            m_path = @"C:\Users\regns\Documents\SummaSummarum\nykontoplan.csv";
            open();
        }

        public void open()
        {
            recNyKontoplan rec;
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            Regex regexKontoplan = new Regex(@"""(.*?)"";|([^,]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 2;
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

                    rec = new recNyKontoplan
                    {
                        Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                        NytKontonr = value[1],
                    };
                    this.Add(rec);

                }
            }
        }
        public static string NytKontonr(int? kontonr)
        {
            try
            {
                return (from m in Program.karNyKontoplan where m.Kontonr == kontonr select m.NytKontonr).First();
            }
            catch
            {
                return "";
            }
        }

        public static string NytKontonr(string AfstemningsKonto)
        {
            try
            {
                string strAfstemKonti = (from s in Program.karRegnskab where s.key == "AfstemKonti" select s.value).First();
                string[] strarrAfstemKonti = strAfstemKonti.Split(',');
                int?[] intarrAfstemKonti = new int?[strarrAfstemKonti.Length];
                for (int i = 0; i < strarrAfstemKonti.Length; i++)
                {
                    intarrAfstemKonti[i] = int.Parse(strarrAfstemKonti[i]);
                }
                var kontonr = (from k in Program.karKontoplan
                          where intarrAfstemKonti.Contains(k.Kontonr) && k.Kontonavn == AfstemningsKonto
                          select k.Kontonr).First();

                return (from m in Program.karNyKontoplan where m.Kontonr == kontonr select m.NytKontonr).First();
            }
            catch
            {
                return "";
            }
        }

    }
}
