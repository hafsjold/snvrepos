using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Uniconta.Common;
using Uniconta.ClientTools.DataModel;
using Uniconta.API.System;

namespace nsPbs3060
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
        public enum action
        {
            import,
            export
        };

        private int m_bankkontoid { get; set; }
        private string m_path { get; set; }
        private dbData3060DataContext m_dbData3060 { get; set; }
        private CrudAPI m_api { get; set; }

        public KarBankkontoudtogDanskebank(dbData3060DataContext p_dbData3060, CrudAPI p_api, int bankkontoid, action act)
        {
            m_bankkontoid = bankkontoid;
            m_dbData3060 = p_dbData3060;
            m_api = p_api;
            string csvfile;
            if (act == action.import)
            {
                try
                {
                    csvfile = (from w in p_dbData3060.tblkontoudtogs where w.pid == m_bankkontoid select w).First().savefile;
                    csvfile = Environment.ExpandEnvironmentVariables(csvfile);
                }
                catch
                {
                    csvfile = "NoFile";
                }
                m_path = csvfile;
                open();
            }
            if (act == action.export)
            {
                export();
            }
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
                    int iMax = 9;
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
                            btekst = extract_tekst(value[2]),
                            bbeløb = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? decimal.Parse(value[4]) : (decimal?)null,
                            bsaldo = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? decimal.Parse(value[5]) : (decimal?)null,
                            bstatus = value[6],
                            bbankkontoid = m_bankkontoid
                        };
                        if ((rec.bstatus == "Udført")|| (rec.bstatus == "Completed"))
                            this.Add(rec);
                    }
                }
            }
            ts.Close();
        }

        public string extract_tekst(string iTekst)
        {
            //string iTekst = @"MP 1785409448 Business Modtaget: 24.06.2017 15:34          Telefonnummer på salgssted: +45 29  82 38 08                            Salgssted: Løbeklubben Puls 3060";
            Regex regexMobilepay = new Regex(@"(^MP\s+.+)\s+Business");
            var m = regexMobilepay.Matches(iTekst);
            if (m.Count == 1)
            {
                string mp = m[0].Groups[1].Value;

                Regex regexMobilepayBesked = new Regex(@"Besked:\s+(.+)|Message:\s+(.+)");
                var b = regexMobilepayBesked.Matches(iTekst);
                if (b.Count == 1)
                {
                    string msg = b[0].Groups[1].Value;
                    if (msg.Length == 0)
                        msg = b[0].Groups[2].Value;

                    msg = Regex.Replace(msg, @"\s+", " ");
                    string newtekst = mp + " " + msg;
                    return newtekst;
                }
                else
                {
                    string newtekst = mp;
                    return newtekst;
                }
            }
            return iTekst;
        }


        public void load()
        {
            var qry = from w in this
                      join b in m_dbData3060.tblbankkontos on new { dato = w.bdato, belob = w.bbeløb, saldo = w.bsaldo, bankkontoid = w.bbankkontoid } equals new { dato = b.dato, belob = b.belob, saldo = b.saldo, bankkontoid = b.bankkontoid } into bankkonto
                      from b in bankkonto.DefaultIfEmpty(new tblbankkonto { pid = 0, belob = null })
                      where b.belob == null
                      orderby w.bdato
                      select w;


            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = b.bbankkontoid,
                    saldo = b.bsaldo,
                    dato = b.bdato,
                    tekst = (b.btekst.Length > 60) ? b.btekst.Substring(0, 60) : b.btekst,
                    belob = b.bbeløb
                };
                m_dbData3060.tblbankkontos.InsertOnSubmit(recBankkonto);
                m_dbData3060.SubmitChanges();
            }

        }

        public void export()
        {
            DateTime ExportFromDate = DateTime.Now.AddDays(-3);
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyName", typeof(string), "Danske Bank");
            crit.Add(pair);
            var taskQryBankStatment = m_api.Query<BankStatementClient>(crit);
            taskQryBankStatment.Wait();
            var col = taskQryBankStatment.Result;
            if (col.Count() == 1)
            {
                ExportFromDate = col[0].LastTransaction;
                var DaysSlip = col[0].DaysSlip;
                ExportFromDate = ExportFromDate.AddDays(-DaysSlip);
            }

            //ExportFromDate = DateTime.Now.AddDays(-70); //<-------------------Fjernes

            using (StringWriter sr = new StringWriter())
            {
                var qry = from w in m_dbData3060.tblbankkontos
                          where w.bankkontoid == m_bankkontoid && (w.skjul == null || w.skjul == false) && w.dato >= ExportFromDate
                          orderby w.dato
                          select w;

                int antal = qry.Count();

                string ln = @"pid;dato;tekst;beløb;saldo";
                sr.WriteLine(ln);

                foreach (var b in qry)
                {
                    ln = "";
                    ln += @"""" + b.pid.ToString() + @"""" + ";";
                    ln += (b.dato == null) ? ";" : @"""" + ((DateTime)b.dato).ToString("dd.MM.yyyy") + @"""" + ";";
                    ln += (b.tekst == null) ? ";" : @"""" + b.tekst.Replace(";", " ") + @"""" + ";";
                    ln += (b.belob == null) ? ";" : @"""" + ((decimal)(b.belob)).ToString("0.00") + @"""" + ";";
                    ln += (b.saldo == null) ? ";" : @"""" + ((decimal)(b.saldo)).ToString("0.00") + @"""";
                    sr.WriteLine(ln);
                }

                //byte[] attachment = Encoding.Default.GetBytes(sr.ToString());
                byte[] attachment = Encoding.GetEncoding(1251).GetBytes(sr.ToString());
                VouchersClient vc = new VouchersClient()
                {
                    Text = "DanskeErhverv",
                    Content = "Bankkontoudtog",
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
