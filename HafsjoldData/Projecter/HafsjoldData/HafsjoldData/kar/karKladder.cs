using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsHafsjoldData
{
    public class recKladder
    {
        public recKladder() { }

        public int? Rid { get; set; }
        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public string Afstemningskonto { get; set; }
        public decimal? Belob { get; set; }
        public int? Konto { get; set; }
        public string MomsKode { get; set; }
        public int? Faknr { get; set; }
        public int? Id { get; set; }
    }

    public class KarKladder : List<recKladder>
    {
        private string m_path { get; set; }
        private int m_rid;

        public KarKladder()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Placering + "kladder.dat";
            m_rid = rec_regnskab.Rid;
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recKladder rec;
            Regex regexKladder = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 10;
                    string[] value = new string[iMax];
                    foreach (Match m in regexKladder.Matches(ln))
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

                    double? wDatoNr = Microsoft.VisualBasic.Information.IsNumeric(value[0]) ? double.Parse(value[0]) : (double?)null;
                    DateTime? wDato = (DateTime?)null;
                    if (wDatoNr != (double?)null) wDato = (DateTime?)clsUtil.MSSerial2DateTime((double)wDatoNr);
                    
                    rec = new recKladder
                    {
                        Rid = m_rid,
                        Dato = wDato,
                        Bilag = Microsoft.VisualBasic.Information.IsNumeric(value[1]) ? int.Parse(value[1]) : (int?)null,
                        Tekst = value[2],
                        Afstemningskonto = value[3],
                        Belob = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? decimal.Parse(value[4]) : (decimal?)null,
                        Konto = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? int.Parse(value[5]) : (int?)null,
                        MomsKode = value[6],
                        Faknr = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? int.Parse(value[7]) : (int?)null,
                        Id =  Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? int.Parse(value[8]) : (int?)null                  
                    };
                    this.Add(rec);
                }
            }
        }

    }
}
