using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;

namespace nsPbs3060
{
    public class recMobilepay
    {
        public recMobilepay() { }

        public string Type { get; set; }
        public string Navn { get; set; }
        public string MobilNummer { get; set; }
        public decimal? Belob { get; set; }
        public string MobilePayNummer { get; set; }
        public string NavnBetalingssted { get; set; }
        public DateTime? Date { get; set; }
        public string TransaktionsID { get; set; }
        public string Besked { get; set; }
        public decimal? Gebyr { get; set; }
        public decimal? Saldo { get; set; }
        public string Imported { get; set; }
        public string Bogfoert { get; set; }
    }

    public class KarMobilepay : List<recMobilepay>
    {
        public enum action
        {
            import,
            export
        };
        private string m_importpath { get; set; }
        private string m_exportpath { get; set; }
        private int m_bankkontoid { get; set; }
        private dbData3060DataContext m_dbData3060 { get; set; }
        private CrudAPI m_api { get; set; }


        public KarMobilepay(dbData3060DataContext p_dbData3060, CrudAPI p_api, int bankkontoid, action act)
        {
            m_bankkontoid = bankkontoid;
            m_dbData3060 = p_dbData3060;
            m_api = p_api;
            string csvimportfile = "NoFile";
            if (act == action.import)
            {
                try
                {
                    csvimportfile = (from w in p_dbData3060.tblkontoudtogs where w.pid == m_bankkontoid select w).First().savefile;
                    csvimportfile = Environment.ExpandEnvironmentVariables(csvimportfile);
                }
                catch
                {
                    csvimportfile = "NoFile";
                }
                m_importpath = csvimportfile;
                open();
            }
            if (act == action.export)
            {
                export();
            }

        }

        public int export()
        {
            DateTime ExportFromDate = DateTime.Now.AddDays(-3);

            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyName", typeof(string), "Mobilepay");
            crit.Add(pair);
            var taskQryBankStatment = m_api.Query<BankStatementClient>(null, crit);
            taskQryBankStatment.Wait();
            var col = taskQryBankStatment.Result;
            if (col.Count() == 1)
            {
                ExportFromDate = col[0].LastTransaction;
                var DaysSlip = col[0].DaysSlip;
                ExportFromDate.AddDays(-DaysSlip);
            }

            //ExportFromDate = DateTime.Now.AddDays(-60);  //<<-----------------------------

            IOrderedQueryable<tblbankkonto> qrybankkonto =
                from w in m_dbData3060.tblbankkontos
                where w.bankkontoid == m_bankkontoid && (w.skjul == null || w.skjul == false) && w.dato >= ExportFromDate
                orderby w.dato
                select w;

            int antal = qrybankkonto.Count();

            using (StringWriter sr = new StringWriter())
            {

                string ln = @"pid;dato;tekst;beløb;saldo";
                sr.WriteLine(ln);

                foreach (var b in qrybankkonto)
                {
                    ln = "";
                    ln += b.pid.ToString() + ";";
                    ln += (b.dato == null) ? ";" : ((DateTime)b.dato).ToString("dd.MM.yyyy") + ";";
                    ln += (b.tekst == null) ? ";" : b.tekst.Replace(";"," ") + ";";
                    ln += (b.belob == null) ? ";" : ((decimal)(b.belob)).ToString("0.00") + @";";
                    ln += (b.saldo == null) ? ";" : ((decimal)(b.saldo)).ToString("0.00");
                    sr.WriteLine(ln);
                }
                byte[] attachment = Encoding.Default.GetBytes(sr.ToString());
                VouchersClient vc = new VouchersClient()
                {
                    Text = string.Format("Mobilepay Kontoudtog {0}", DateTime.Now),
                    Content = "Bankkontoudtog",
                    DocumentDate = DateTime.Now,
                    Fileextension = FileextensionsTypes.CSV,
                    VoucherAttachment = attachment,
                };
                var taskInsertVouchers = m_api.Insert(vc);
                taskInsertVouchers.Wait();
                var err = taskInsertVouchers.Result;

                var GLDailyJournalLines = InsertGLDailyJournalLines(qrybankkonto);
                return GLDailyJournalLines;
            }
        }

        public int InsertGLDailyJournalLines(IOrderedQueryable<tblbankkonto> qrybankkonto)
        {
            var GLDailyJournalLines = 0;
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var task1 = m_api.Query<GLDailyJournalClient>(null, crit);
            task1.Wait();
            var col = task1.Result;
            var rec_Master = col.FirstOrDefault();

            foreach (var bk in qrybankkonto)
            {
                tblmobilepay mp = null;

                var qrymobilepay = from v in m_dbData3060.tblmobilepays
                                   where v.pid == bk.mobilepay_pid
                                   && (v.Bogfoert == null || v.Bogfoert == false)
                                   select v;
                var qrycount = qrymobilepay.Count();
                if (qrycount == 1)
                {
                    mp = qrymobilepay.First();
                }
                else
                {
                    continue;
                }

                GLDailyJournalLines++;

                GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                {
                    Date = (DateTime)bk.dato,
                    Text = bk.tekst,
                    Account = "5850",
                };

                if (bk.belob > 0)
                {
                    jl.Debit = (double)bk.belob;
                }
                else
                {
                    jl.Credit = -(double)bk.belob;
                }


                switch (mp.Type.ToUpper())
                {
                    case "SALG":
                        jl.OffsetAccount = "1004";
                        break;

                    case "REFUNDERING":
                        jl.OffsetAccount = "1004";
                        break;

                    case "GEBYR":
                        jl.OffsetAccount = "4471";
                        break;

                    case "OVERFØRSEL":
                        jl.OffsetAccount = "5840";
                        break;

                    default:
                        jl.OffsetAccount = "9900";
                        break;
                }

                jl.SetMaster(rec_Master);
                var task2 = m_api.Insert(jl);
                task2.Wait();
                var err = task2.Result;

                mp.Bogfoert = true;
            }
            m_dbData3060.SubmitChanges();

            return GLDailyJournalLines;
        }


        public void open()
        {
            FileStream ts = new FileStream(m_importpath, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recMobilepay rec;
            Regex regexMobilepay = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            int iLinenr = 0;
            using (StreamReader sr = new StreamReader(ts, Encoding.Unicode))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    iLinenr++;
                    int i = 0;
                    int iMax = 15;
                    string[] value = new string[iMax];
                    foreach (Match m in regexMobilepay.Matches(ln))
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
                    if (iLinenr > 2)
                    {
                        rec = new recMobilepay
                        {
                            Type = value[0],
                            Navn = value[1],
                            MobilNummer = value[2],
                            Belob = readBeløb(value[3]),
                            MobilePayNummer = value[4],
                            NavnBetalingssted = value[5],
                            Date = readDateTime(value[6], value[7]),
                            TransaktionsID = value[8],
                            Besked = value[9],
                            Gebyr = readBeløb(value[10]),
                            Saldo = readBeløb(value[11]),
                        };
                        this.Add(rec);
                    }
                }
            }
            ts.Close();
        }

        private DateTime? readDate(string sDate)
        {
            DateTime? d = Microsoft.VisualBasic.Information.IsDate(sDate) ? DateTime.Parse(sDate) : (DateTime?)null;
            return d;
        }

        public DateTime? readDateTime(string sDate, string sTime)
        {
            DateTime? dt = null;
            DateTime? d = Microsoft.VisualBasic.Information.IsDate(sDate) ? DateTime.Parse(sDate) : (DateTime?)null;
            DateTime? t = Microsoft.VisualBasic.Information.IsDate(sTime) ? DateTime.Parse(sTime) : (DateTime?)null;
            try
            {
                dt = new DateTime(d.Value.Year, d.Value.Month, d.Value.Day, t.Value.Hour, t.Value.Hour, t.Value.Second);
            }
            catch { }
            return dt;
        }

        public decimal? readBeløb(string sVal)
        {
            decimal? val = Microsoft.VisualBasic.Information.IsNumeric(sVal) ? decimal.Parse(sVal) : (decimal?)null;
            return val;
        }

        public void load_Mobilepay()
        {
            var qry = from w in this
                      join b in m_dbData3060.tblmobilepays
                        on new { date = w.Date, name = w.Navn, type = w.Type, saldo = w.Saldo, transaction_id = w.TransaktionsID }
                        equals new { date = b.Date, name = b.Navn, type = b.Type, saldo = b.Saldo, transaction_id = b.TransaktionsID } into Mobilepaykonto
                      from b in Mobilepaykonto.DefaultIfEmpty(new tblmobilepay { pid = 0, Saldo = null })
                      where b.Saldo == null
                      orderby w.Date
                      select w;

            int iLinenr = 0;
            int antal = qry.Count();
            foreach (var b in qry)
            {
                iLinenr++;
                tblmobilepay recMobilepay = new tblmobilepay
                {
                    Type = b.Type,
                    Navn = b.Navn,
                    MobilNummer = b.MobilNummer,
                    Belob = b.Belob,
                    MobilePayNummer = b.MobilePayNummer,
                    NavnBetalingssted = b.NavnBetalingssted,
                    Date = b.Date,
                    TransaktionsID = b.TransaktionsID,
                    Besked = b.Besked,
                    Gebyr = b.Gebyr,
                    Saldo = b.Saldo,
                };
                m_dbData3060.tblmobilepays.InsertOnSubmit(recMobilepay);
                m_dbData3060.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
        }

        public void load_bankkonto()
        {
            load_bankkonto1();

            load_bankkonto2();
            load_bankkonto3();
            load_bankkonto4();
        }

        private void load_bankkonto1()
        {
            var qry = from w in m_dbData3060.tblmobilepays
                      where w.Imported == null
                      && (w.Type.ToUpper() == "SALG")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                string wtekst = string.Format("{0}, {1}", b.Navn, b.Besked).TrimEnd();
                if (wtekst.Length > 60) { wtekst = wtekst.Substring(0, 60); }
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Saldo,
                    dato = b.Date,
                    tekst = wtekst,
                    belob = b.Belob,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto2()
        {
            var qry = from w in m_dbData3060.tblmobilepays
                      where w.Imported == null
                      && (w.Type.ToUpper() == "REFUNDERING")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Saldo,
                    dato = b.Date,
                    tekst = string.Format("Refundering til {0}", b.Navn),
                    belob = -b.Belob,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto3()
        {
            var qry = from w in m_dbData3060.tblmobilepays
                      where w.Imported == null
                      && (w.Type.ToUpper() == "GEBYR")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Saldo,
                    dato = b.Date,
                    tekst = "Gebyr",
                    belob = -b.Belob,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto4()
        {
            var qry = from w in m_dbData3060.tblmobilepays
                      where w.Imported == null
                      && (w.Type.ToUpper() == "OVERFØRSEL")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Saldo,
                    dato = b.Date,
                    tekst = "Overførsel til Danske Bank",
                    belob = -b.Belob,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

    }
}

