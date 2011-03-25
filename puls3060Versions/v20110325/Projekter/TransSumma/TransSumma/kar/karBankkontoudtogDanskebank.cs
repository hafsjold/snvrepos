using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsPuls3060
{
    public class recBankkontoudtogDanskebank
    {
        public recBankkontoudtogDanskebank() { }

        public DateTime? bdato { get; set; }
        public string btekst { get; set; }
        public decimal? bbeløb { get; set; }
        public decimal? bsaldo { get; set; }
        public string bstatus { get; set; }
        public string bafstemt { get; set; }
    }


    public class KarBankkontoudtogDanskebank : List<recBankkontoudtogDanskebank>
    {
        private string m_path { get; set; }

        public KarBankkontoudtogDanskebank()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + "Danske Erhverv.csv";
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recBankkontoudtogDanskebank rec;
            Regex regexKontoplan = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 6;
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

                    DateTime? wdato = Microsoft.VisualBasic.Information.IsDate(value[0]) ? DateTime.Parse(value[0]) : (DateTime?)null;
                    if (wdato != null)
                    {
                        rec = new recBankkontoudtogDanskebank
                        {
                            bdato = wdato,
                            btekst = value[1],
                            bbeløb = Microsoft.VisualBasic.Information.IsNumeric(value[2]) ? decimal.Parse(value[2]) : (decimal?)null,
                            bsaldo = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                            bstatus =  value[4],
                            bafstemt = value[5]
                       };
                       if (rec.bstatus == "Udført")
                            this.Add(rec);
                    }
                }
            }
            ts.Close();
        }


        public void load()
        {
            var qry = from w in this
                      join b in Program.dbDataTransSumma.Tblbankkonto on new { dato = w.bdato, belob = w.bbeløb, saldo = w.bsaldo } equals new { dato = b.Dato, belob = b.Belob, saldo = b.Saldo } into bankkonto
                      from b in bankkonto.DefaultIfEmpty(new Tblbankkonto { Pid = 0, Belob = null })
                      where b.Belob == null 
                      orderby w.bdato
                      select w;


            int antal = qry.Count();
            foreach (var b in qry)
            {
                Tblbankkonto recBankkonto = new Tblbankkonto
                {
                    Pid = clsPbs.nextval("Tblbankkonto"),
                    Saldo = b.bsaldo,
                    Dato = b.bdato,
                    Tekst = b.btekst,
                    Belob = b.bbeløb
                };
                Program.dbDataTransSumma.Tblbankkonto.InsertOnSubmit(recBankkonto);

            }
            Program.dbDataTransSumma.SubmitChanges();
        }

    }
}
