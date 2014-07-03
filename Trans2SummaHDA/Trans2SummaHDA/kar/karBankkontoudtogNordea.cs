using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace Trans2SummaHDA
{

    public class recBankkontoudtogNordea
    {
        public recBankkontoudtogNordea() { }

        public DateTime? bdato { get; set; }
        public string btekst { get; set; }
        public DateTime? rdato { get; set; }
        public decimal? bbeløb { get; set; }
        public decimal? bsaldo { get; set; }
        public int? bbankkontoid { get; set; }
    }

    public class KarBankkontoudtogNordea : List<recBankkontoudtogNordea>
    {
        private int m_bankkontoid { get; set; }
        private string m_path { get; set; }
        private string b105 = "                                                                                                         ";

        public KarBankkontoudtogNordea(int bankkontoid)
        {
            m_bankkontoid = bankkontoid;
            string csvfile;
            try
            {
                csvfile = (from w in Program.dbDataTransSumma.tblkontoudtogs where w.pid == m_bankkontoid select w).First().savefile;
            }
            catch
            {
                csvfile = "NoFile";
            }

            var rec_regnskab = Program.qryAktivRegnskab();
            m_path = rec_regnskab.Eksportmappe + csvfile;
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
                            bbankkontoid = m_bankkontoid
                        };
                        this.Add(rec);
                    }
                }
            }
            ts.Close();
        }


        public void load()
        {
            var qry = from w in this
                      join b in Program.dbDataTransSumma.tblbankkontos on new { dato = w.bdato, belob = w.bbeløb, saldo = w.bsaldo, bankkontoid = w.bbankkontoid } equals new { dato = b.dato, belob = b.belob, saldo = b.saldo, bankkontoid = b.bankkontoid } into bankkonto
                      from b in bankkonto.DefaultIfEmpty(new tblbankkonto { pid = 0, belob = null })
                      where b.belob == null
                      orderby w.bdato
                      select w;


            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    pid = clsPbs.nextval("Tblbankkonto"),
                    bankkontoid = b.bbankkontoid,
                    saldo = b.bsaldo,
                    dato = b.bdato,
                    tekst = b.btekst,
                    belob = b.bbeløb
                };
                Program.dbDataTransSumma.tblbankkontos.InsertOnSubmit(recBankkonto);

            }
            Program.dbDataTransSumma.SubmitChanges();
        }

    }
}
