using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsPuls3060
{
    public class recKladder
    {
        public recKladder() { }

        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public string Afstemningskonto { get; set; }
        public decimal? Belob { get; set; }
        public int? Konto { get; set; }
        public string Momskode { get; set; }
        public int? Faktura { get; set; }
        public int? Id { get; set; }
        public int? Regnskabid { get; set; }

    }

    public class KarKladder : List<recKladder>
    {
        private string m_path { get; set; }
        private int m_regnskabid;

        public KarKladder()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kladder.dat";
            m_regnskabid = rec_regnskab.Rid;
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recKladder rec;
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

                    int? wDatonr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? int.Parse(value[0]) : (int?)null;
                    rec = new recKladder
                    {
                        Dato = clsUtil.MSSerial2DateTime((double)wDatonr),
                        Bilag = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? int.Parse(value[1]) : (int?)null,
                        Tekst = value[2],
                        Afstemningskonto = value[3],
                        Belob = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? decimal.Parse(value[4]) : (decimal?)null,
                        Konto = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? int.Parse(value[5]) : (int?)null,
                        Momskode = value[6],
                        Faktura = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? int.Parse(value[7]) : (int?)null,
                        Id = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? int.Parse(value[8]) : (int?)null,
                        Regnskabid = m_regnskabid,
                    };
                    this.Add(rec);
                }
            }
            ts.Close();
        }

    }
}
