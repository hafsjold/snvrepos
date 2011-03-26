using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace nsPuls3060
{
   
    public class recBankkontoudtogNordea
    {
        public recBankkontoudtogNordea() { }

        public DateTime? bdato { get; set; }
        public string btekst { get; set; }
        public DateTime? rdato { get; set; }
        public decimal? bbeløb { get; set; }
        public decimal? bsaldo { get; set; }
    }
    
    public class KarBankkontoudtogNordea : List<recBankkontoudtogNordea>
    {
        private string m_path { get; set; }
        private string b105 = "                                                                                                         ";

        public KarBankkontoudtogNordea()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + "Nordea Puls3060.csv";
            open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recBankkontoudtogNordea rec;
            Regex regexKontoplan = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    int i = 0;
                    int iMax = 5;
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
                        string wtekst = value[1] + b105;
                        string wtekst1 = wtekst.Substring(0, 35).TrimEnd(' ');
                        string wtekst2 = wtekst.Substring(35, 35).TrimEnd(' '); ;
                        string wtekst3 = wtekst.Substring(70, 35).TrimEnd(' '); ;
                        string wtekst4 = wtekst1 + wtekst2 + wtekst3;
                        rec = new recBankkontoudtogNordea
                        {
                            bdato = wdato,
                            btekst = (wtekst4.Length > 50) ? wtekst4.Substring(0, 50) : wtekst4,
                            rdato = Microsoft.VisualBasic.Information.IsDate(value[2]) ? DateTime.Parse(value[2]) : (DateTime?)null,
                            bbeløb = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                            bsaldo = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? decimal.Parse(value[4]) : (decimal?)null,
                        };
                        this.Add(rec);
                    }
                }
            }
            ts.Close();
        }


        public void load()
        {
            Guid nullGuid = new Guid("00000000-0000-0000-0000-000000000000"); 
            var qry = from w in this
                      join b in Program.dbDataTransSumma.Tblbankkonto on new { dato = w.bdato, belob = w.bbeløb, saldo = w.bsaldo } equals new { dato = b.Dato, belob = b.Belob, saldo = b.Saldo } into bankkonto
                      from b in bankkonto.DefaultIfEmpty(new Tblbankkonto { Pid = nullGuid, Belob = null })
                      where b.Belob == null 
                      orderby w.bdato
                      select w;


            int antal = qry.Count();
            foreach (var b in qry)
            {
                Tblbankkonto recBankkonto = new Tblbankkonto
                {
                    Pid = new Guid(),
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
