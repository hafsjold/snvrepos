using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Uniconta.Common;
using Uniconta.ClientTools.DataModel;

namespace Trans2Summa3060
{
    public class recBankkontoudtogMobilePay
    {
        public recBankkontoudtogMobilePay() { }

        public string bnavn { get; set; }
        public string bmobilnummer { get; set; }
        public decimal? bbeløb { get; set; }
        public DateTime? bdato { get; set; }
        public DateTime? btid { get; set; }
        public string bid { get; set; }
        public string btekst { get; set; }
        public int? bbankkontoid { get; set; }
    }


    public class KarBankkontoudtogMobilePay : List<recBankkontoudtogMobilePay>
    {
        public enum action
        {
            import,
            export
        };

        private int m_bankkontoid { get; set; }
        private string m_path { get; set; }

        public KarBankkontoudtogMobilePay(int bankkontoid, action act)
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
            if (act == action.import) open();
        }

        public void open()
        {
            FileStream ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recBankkontoudtogMobilePay rec;
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

                    DateTime? wdatotid = null;
                    DateTime? wdato = Microsoft.VisualBasic.Information.IsDate(value[3]) ? DateTime.Parse(value[3]) : (DateTime?)null;
                    DateTime? wtid = Microsoft.VisualBasic.Information.IsDate(value[4]) ? DateTime.Parse(value[4]) : (DateTime?)null;
                    if ((wtid != null) && (wdato != null))
                    {
                        wdatotid = new DateTime(wdato.Value.Year, wdato.Value.Month, wdato.Value.Day, wtid.Value.Hour, wtid.Value.Minute, wtid.Value.Second);
                    }

                    decimal? wbeløb;
                    if (Microsoft.VisualBasic.Information.IsNumeric(value[2]))
                        wbeløb = decimal.Parse(value[2]);
                    else
                    {
                        try
                        {
                            wbeløb = -decimal.Parse(value[2].Replace("-", "").Trim());
                        }
                        catch
                        {
                            wbeløb = (decimal?)null;
                        }
                    }

                    if (wdatotid != null)
                    {
                        rec = new recBankkontoudtogMobilePay
                        {
                            bnavn = value[0],
                            bmobilnummer = value[1],
                            bbeløb = wbeløb,
                            bdato = wdatotid,
                            bid = value[5],
                            btekst = value[6],
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
            var qry1 = from w in this
                       join b in Program.dbDataTransSumma.tblmobilepays on w.bid equals b.mobilepay_id into bankkonto
                       from b in bankkonto.DefaultIfEmpty(new tblmobilepay { pid = 0, belob = null })
                       where b.belob == null
                       orderby w.bdato
                       select w;


            int antal1 = qry1.Count();
            foreach (var b in qry1)
            {
                tblmobilepay recBankkonto = new tblmobilepay
                {
                    navn = b.bnavn.Length > 35 ? b.bnavn.Substring(0, 35) : b.bnavn,
                    mobilnummer = b.bmobilnummer.Length > 10 ? b.bmobilnummer.Substring(0, 10) : b.bmobilnummer,
                    belob = b.bbeløb,
                    dato = b.bdato,
                    mobilepay_id = b.bid.Length > 25 ? b.bid.Substring(0, 25) : b.bid,
                    tekst = b.btekst.Length > 50 ? b.btekst.Substring(0, 50) : b.btekst,
                };
                Program.dbDataTransSumma.tblmobilepays.InsertOnSubmit(recBankkonto);

            }
            Program.dbDataTransSumma.SubmitChanges();

            var qry2 = from w in Program.dbDataTransSumma.tblmobilepays
                       where w.Imported == null
                       orderby w.dato
                       select w;

            int antal2 = qry2.Count();
            foreach (var b in qry2)
            {
                if (b.tekst.Length < 3)
                {
                    b.tekst += " " + b.navn + " " + b.mobilnummer;
                    b.tekst.Trim();
                }
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    pid = clsPbs.nextval("Tblbankkonto"),
                    bankkontoid = m_bankkontoid,
                    saldo = 0,
                    dato = b.dato,
                    tekst = b.tekst.Length > 50 ? b.tekst.Substring(0, 50) : b.tekst,
                    belob = b.belob,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                Program.dbDataTransSumma.SubmitChanges();
            }
        }

        public void test()
        {
            var api = UCInitializer.GetBaseAPI;
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyName", typeof(string), "Danske Bank");
            crit.Add(pair);
            var taskQryBankStatment = api.Query<BankStatementClient>(null, crit);
            taskQryBankStatment.Wait();
            var col = taskQryBankStatment.Result;
            if (col.Count() == 1)
            {
                var crit2 = new List<PropValuePair>();
                var pair1 = PropValuePair.GenereteWhereElements("BankId", typeof(int), "917755");
                crit2.Add(pair1);
                var pair2 = PropValuePair.GenereteOrderByElement("Date", true);
                crit2.Add(pair2);
                var taskQryBankStatmentLine = api.Query<BankStatementLineClient>(col, crit2);
                taskQryBankStatment.Wait();
                var col1 = taskQryBankStatmentLine.Result;
                foreach (var rec1 in col1)
                {
                    //BankStatementLineClient nyrac = rec1.clo
                    rec1._Text += " Test";
                    var task = api.Update(rec1);
                    task.Wait();
                    var res = task.Result;
                }
            }
        }

    }
}
