using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsHafsjoldData
{
    public class recPosteringsjournal
    {
        public recPosteringsjournal() { }
        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public int? Kontonr { get; set; }
        public string Kontonavn { get; set; }
        public decimal? Moms { get; set; }
        public decimal? Debet { get; set; }
        public decimal? Kredit { get; set; }
        public int? Nr { get; set; }
        public int? Id { get; set; }
        public int? Sag { get; set; }
    }

    public class KarPosteringsjournal : List<recPosteringsjournal>
    {
        private string m_path { get; set; }

        public KarPosteringsjournal()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + "Posteringsjournal.txt";
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            int lnr = 0;
            recPosteringsjournal rec;
            Regex regexPosteringsjournal = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    lnr++;
                    int i = 0;
                    int iMax = 11;
                    string[] value = new string[iMax];
                    foreach (Match m in regexPosteringsjournal.Matches(ln))
                    {
                        for (int j = 1; j <= 3; j++)
                        {
                            if (m.Groups[j].Success)
                            {
                                if (i < iMax)
                                {
                                    value[i++] = m.Groups[j].ToString();
                                }
                                j = 4; //Break loop
                            }
                        }
                    }
                    if (lnr > 1)
                    {
                        rec = new recPosteringsjournal
                        {
                            Dato = Microsoft.VisualBasic.Information.IsDate(value[0]) ? DateTime.Parse(value[0]) : (DateTime?)null ,
                            Bilag = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? int.Parse(value[1]) : (int?)null,
                            Tekst = value[2],
                            Kontonr = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? int.Parse(value[3]) : (int?)null,
                            Kontonavn = value[4],
                            Moms = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? decimal.Parse(value[5]) : (decimal?)null,
                            Debet = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? decimal.Parse(value[6]) : (decimal?)null,
                            Kredit = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                            Nr = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? int.Parse(value[8]) : (int?)null,
                            Id = Microsoft.VisualBasic.Information.IsNumeric(value[9]) ? int.Parse(value[9]) : (int?)null,
                            Sag = Microsoft.VisualBasic.Information.IsNumeric(value[10]) ? int.Parse(value[10]) : (int?)null
                        };
                        this.Add(rec);
                    }
                }
            }
        }

    }
}
