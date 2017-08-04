using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Uniconta.Common;
using Uniconta.ClientTools.DataModel;
using Uniconta.API.System;

namespace Trans2SummaHDC
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
        public int? bbankkontoid { get; set; }
    }


    public class KarBankkontoudtogDanskebank : List<recBankkontoudtogDanskebank>
    {
        private int m_bankkontoid { get; set; }
        private string m_path { get; set; }
        private CrudAPI m_api { get; set; }

        public KarBankkontoudtogDanskebank(CrudAPI p_api, int bankkontoid)
        {
            m_bankkontoid = bankkontoid;
            m_api = p_api;
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
                            bstatus = value[4],
                            bafstemt = value[5],
                            bbankkontoid = m_bankkontoid
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

        public void export()
        {
            DateTime ExportFromDate = DateTime.Now.AddDays(-3);
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyName", typeof(string), "DanskeBank");
            crit.Add(pair);
            var taskQryBankStatment = m_api.Query<BankStatementClient>(null, crit);
            taskQryBankStatment.Wait();
            var col = taskQryBankStatment.Result;
            if (col.Count() == 1)
            {
                ExportFromDate = col[0].LastTransaction;
                var DaysSlip = col[0].DaysSlip;
                ExportFromDate = ExportFromDate.AddDays(-DaysSlip);
            }

            //ExportFromDate = DateTime.Now.AddDays(-30); //<-------------------Fjernes

            using (StringWriter sr = new StringWriter())
            {
                var qry = from w in Program.dbDataTransSumma.tblbankkontos
                          where w.bankkontoid == m_bankkontoid && (w.skjul == null || w.skjul == false) && w.dato >= ExportFromDate
                          orderby w.dato
                          select w;

                int antal = qry.Count();

                string ln = @"pid;dato;tekst;beløb;saldo";
                sr.WriteLine(ln);

                foreach (var b in qry)
                {
                    ln = "";
                    ln += b.pid.ToString() + ";";
                    ln += (b.dato == null) ? ";" : ((DateTime)b.dato).ToString("dd.MM.yyyy") + ";";
                    ln += (b.tekst == null) ? ";" : b.tekst + ";";
                    ln += (b.belob == null) ? ";" : ((decimal)(b.belob)).ToString("0.00") + @";";
                    ln += (b.saldo == null) ? ";" : ((decimal)(b.saldo)).ToString("0.00");
                    sr.WriteLine(ln);
                }
                byte[] attachment = Encoding.Default.GetBytes(sr.ToString());
                VouchersClient vc = new VouchersClient()
                {
                    Text = string.Format("Danske Bank Kontoudtog {0}", DateTime.Now),
                    Content = "Bankkontoudtog",
                    DocumentDate = DateTime.Now,
                    Fileextension = FileextensionsTypes.CSV,
                    VoucherAttachment = attachment,
                };
                var taskInsertVouchers = m_api.Insert(vc);
                taskInsertVouchers.Wait();
                var err = taskInsertVouchers.Result;

            }
        }
    }
}
