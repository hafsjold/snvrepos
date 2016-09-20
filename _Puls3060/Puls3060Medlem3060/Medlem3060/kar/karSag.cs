using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsPuls3060
{
    public class recSag
    {
        public recSag() { }

        public int? Sagnr { get; set; }
        public string Sagnavn { get; set; }
    }

    public class KarSag : List<recSag>
    {
        private string m_path { get; set; }

        public KarSag()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "sager.dat";
            open();
        }

        public void open()
        {
            recSag rec;
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            Regex regexSag = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
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


                        rec = new recSag
                        {
                            Sagnr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null,
                            Sagnavn = value[1]
                        };
                        this.Add(rec);

                }
            }
        }

   
        public static string getSagnavn(int? sagnr)
        {
            try
            {
                return (from m in Program.karSag where m.Sagnr == sagnr select m.Sagnavn).First();
            }
            catch
            {
                return "Sag findes ikke";
            }
        }
  
    }
}
