using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2Summa3060
{
    public class recDimensionSag
    {
        public recDimensionSag() { }

        public int? Sagnr { get; set; }
        public string Sagnavn { get; set; }
        public string Dimension1 { get; set; }
        public string Dimension2 { get; set; }
    }

    public class KarDimensionSag : List<recDimensionSag>
    {
        private string m_path { get; set; }

        public KarDimensionSag()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "dimensionsag.csv";
            open();
        }

        public void open()
        {
            var fileinfo = new FileInfo(m_path);
            if (!fileinfo.Exists) return;

            recDimensionSag rec;
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            Regex regexSag = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 11;
                    string[] value = new string[iMax];
                    foreach (Match m in regexSag.Matches(ln))
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


                    rec = new recDimensionSag
                    {
                        Sagnr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                        Sagnavn = value[1],
                        Dimension1 = value[2],
                        Dimension2 = value[3],
                    };
                    this.Add(rec);

                }
            }
        }

        public void update()
        {
            var qry = from p in Program.karSag select p;
            int antal = qry.Count();

            FileStream ts = new FileStream(m_path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                 foreach (var p in qry)
                {
                        string line;
                        try
                        {
                            var rec = (from x in this where x.Sagnr == p.Sagnr && x.Dimension1 != "" select x).First();
                            line = string.Format("{0};{1};{2};{3}", rec.Sagnr, rec.Sagnavn, rec.Dimension1, rec.Dimension2);
                        }
                        catch
                        {
                            line = string.Format("{0};{1};{2};{3}", p.Sagnr, p.Sagnavn, "", "");
                        }
                        sr.WriteLine(line);
                }
            }
        }


        public static string getDimension1(int? sagnr)
        {
            try
            {
                return (from m in Program.karDimensionSag where m.Sagnr == sagnr select m.Dimension1).First();
            }
            catch
            {
                return "";
            }
        }

        public static string getDimension2(int? sagnr)
        {
            try
            {
                return (from m in Program.karDimensionSag where m.Sagnr == sagnr select m.Dimension2).First();
            }
            catch
            {
                return "";
            }
        }
    }
}
