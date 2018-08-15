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
    public class recPaypal
    {
        public recPaypal() { }

        public DateTime? Date { get; set; }
        public string Time_Zone { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Fee { get; set; }
        public decimal? Net { get; set; }
        public string From_Email_Address { get; set; }
        public string To_Email_Address { get; set; }
        public string Transaction_ID { get; set; }
        public string Counterparty_Status { get; set; }
        public string Address_Status { get; set; }
        public string Item_Title { get; set; }
        public string Item_ID { get; set; }
        public decimal? Shipping_and_Handling_Amount { get; set; }
        public decimal? Insurance_Amount { get; set; }
        public decimal? Sales_Tax { get; set; }
        public string Option_1_Name { get; set; }
        public string Option_1_Value { get; set; }
        public string Option_2_Name { get; set; }
        public string Option_2_Value { get; set; }
        public string Auction_Site { get; set; }
        public string Buyer_ID { get; set; }
        public string Item_URL { get; set; }
        public DateTime? Closing_Date { get; set; }
        public string Escrow_Id { get; set; }
        public string Invoice_Id { get; set; }
        public string Reference_Txn_ID { get; set; }
        public string Invoice_Number { get; set; }
        public string Custom_Number { get; set; }
        public string Quantity { get; set; }
        public string Receipt_ID { get; set; }
        public decimal? Balance { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip_Code { get; set; }
        public string Country { get; set; }
        public string Contact_Phone_Number { get; set; }
    }

    public class KarPaypal : List<recPaypal>
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


        public KarPaypal(dbData3060DataContext p_dbData3060, CrudAPI p_api, int bankkontoid, action act)
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

        public void export()
        {
            DateTime ExportFromDate = DateTime.Now.AddDays(-3); 
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyName", typeof(string), "PayPal");
            crit.Add(pair);
            var taskQryBankStatment = m_api.Query<BankStatementClient>(crit);
            taskQryBankStatment.Wait();
            var col = taskQryBankStatment.Result;
            if (col.Count() == 1)
            {
                ExportFromDate = col[0].LastTransaction;
                var DaysSlip = col[0].DaysSlip;
                ExportFromDate.AddDays(-DaysSlip);
            }

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
                    Text = string.Format("PayPal Kontoudtog {0}", DateTime.Now),
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


        public void open()
        {
            FileStream ts = new FileStream(m_importpath, FileMode.Open, FileAccess.Read, FileShare.None);
            string ln = null;
            recPaypal rec;
            Regex regexPaypal = new Regex(@"""(.*?)""\t|([^\t]*)\t|(.*)$");
            int iLinenr = 0;
            using (StreamReader sr = new StreamReader(ts, Encoding.Default))
            {
                while ((ln = sr.ReadLine()) != null)
                {
                    iLinenr++;
                    int i = 0;
                    int iMax = 50;
                    string[] value = new string[iMax];
                    foreach (Match m in regexPaypal.Matches(ln))
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
                    if (iLinenr > 1)
                    {
                        rec = new recPaypal
                        {
                            Date = readDateTime(value[0], value[1]),
                            Time_Zone = value[2],
                            Name = value[3],
                            Type = value[4],
                            Status = value[5],
                            Currency = value[6],
                            Gross = readValuta(value[7]),
                            Fee = readValuta(value[8]),
                            Net = readValuta(value[9]),
                            From_Email_Address = value[10],
                            To_Email_Address = value[11],
                            Transaction_ID = value[12],
                            Counterparty_Status = value[13],
                            Address_Status = value[14],
                            Item_Title = value[15],
                            Item_ID = value[16],
                            Shipping_and_Handling_Amount = readValuta(value[17]),
                            Insurance_Amount = readValuta(value[18]),
                            Sales_Tax = readValuta(value[19]),
                            Option_1_Name = value[20],
                            Option_1_Value = value[21],
                            Option_2_Name = value[22],
                            Option_2_Value = value[23],
                            Auction_Site = value[24],
                            Buyer_ID = value[25],
                            Item_URL = value[26],
                            Closing_Date = readDate(value[27]),
                            Escrow_Id = value[28],
                            Invoice_Id = value[29],
                            Reference_Txn_ID = value[30],
                            Invoice_Number = value[31],
                            Custom_Number = value[32],
                            Quantity = value[33],
                            Receipt_ID = value[34],
                            Balance = readValuta(value[35]),
                            Address_Line_1 = value[36],
                            Address_Line_2 = value[37],
                            City = value[38],
                            State = value[39],
                            Zip_Code = value[40],
                            Country = value[41],
                            Contact_Phone_Number = value[42],
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

        public decimal? readValuta(string sVal)
        {
            decimal? val = Microsoft.VisualBasic.Information.IsNumeric(sVal) ? decimal.Parse(sVal) : (decimal?)null;
            return val;
        }

        public void load_paypal()
        {
            var qry = from w in this
                      join b in m_dbData3060.tblpaypals
                        on new { date = w.Date, name = w.Name, type = w.Type, gross = w.Gross, balance = w.Balance, transaction_id = w.Transaction_ID }
                        equals new { date = b.Date, name = b.Name, type = b.Type, gross = b.Gross, balance = b.Balance, transaction_id = b.Transaction_ID } into paypalkonto
                      from b in paypalkonto.DefaultIfEmpty(new tblpaypal { pid = 0, Gross = null })
                      where b.Gross == null
                      orderby w.Date
                      select w;

            int iLinenr = 0;
            int antal = qry.Count();
            foreach (var b in qry)
            {
                iLinenr++;
                tblpaypal recPaypal = new tblpaypal
                {
                    Date = b.Date,
                    Time_Zone = b.Time_Zone,
                    Name = b.Name,
                    Type = b.Type,
                    Status = b.Status,
                    Currency = b.Currency,
                    Gross = b.Gross,
                    Fee = b.Fee,
                    Net = b.Net,
                    From_Email_Address = b.From_Email_Address,
                    To_Email_Address = b.To_Email_Address,
                    Transaction_ID = b.Transaction_ID,
                    Counterparty_Status = b.Counterparty_Status,
                    Address_Status = b.Address_Status,
                    Item_Title = b.Item_Title,
                    Item_ID = b.Item_ID,
                    Shipping_and_Handling_Amount = b.Shipping_and_Handling_Amount,
                    Insurance_Amount = b.Insurance_Amount,
                    Sales_Tax = b.Sales_Tax,
                    Option_1_Name = b.Option_1_Name,
                    Option_1_Value = b.Option_1_Value,
                    Option_2_Name = b.Option_2_Name,
                    Option_2_Value = b.Option_2_Value,
                    Auction_Site = b.Auction_Site,
                    Buyer_ID = b.Buyer_ID,
                    Item_URL = b.Item_URL,
                    Closing_Date = b.Closing_Date,
                    Escrow_Id = b.Escrow_Id,
                    Invoice_Id = b.Invoice_Id,
                    Reference_Txn_ID = b.Reference_Txn_ID,
                    Invoice_Number = b.Invoice_Number,
                    Custom_Number = b.Custom_Number,
                    Quantity = b.Quantity,
                    Receipt_ID = b.Receipt_ID,
                    Balance = b.Balance,
                    Address_Line_1 = b.Address_Line_1,
                    Address_Line_2 = b.Address_Line_2,
                    City = b.City,
                    State = b.State,
                    Zip_Code = b.Zip_Code,
                    Country = b.Country,
                    Contact_Phone_Number = b.Contact_Phone_Number,
                };
                m_dbData3060.tblpaypals.InsertOnSubmit(recPaypal);
                m_dbData3060.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
        }

        public void load_bankkonto()
        {
            load_bankkonto1();
            load_bankkonto2();
            load_bankkonto3();
            load_bankkonto4();
            load_bankkonto5();
            load_bankkonto6();
            load_bankkonto7();
        }

        private void load_bankkonto1()
        {
            var qry = from w in m_dbData3060.tblpaypals
                      where w.Imported == null && w.Currency == "DKK"
                      && (w.Type == "Charge From Credit Card" || w.Type == "PayPal card confirmation refund")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance,
                    dato = b.Date,
                    tekst = "Overført til PayPal",
                    belob = b.Gross,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto2()
        {
            var qry = from w in m_dbData3060.tblpaypals
                      where w.Imported == null && w.Currency == "DKK"
                      && w.Type == "Payment Received" && w.From_Email_Address == @"paypal@trustly.com"
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance,
                    dato = b.Date,
                    tekst = "Overført fra Nordea",
                    belob = b.Gross,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto3()
        {
            var qry = from w in m_dbData3060.tblpaypals
                      where w.Imported == null && w.Currency == "DKK"
                      && (w.Type == "Shopping Cart Payment Received")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance,
                    dato = b.Date,
                    tekst = "Shopping Cart Payment Received",
                    belob = b.Gross,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);

                recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance + b.Fee,
                    dato = b.Date,
                    tekst = "PayPal Fee",
                    belob = b.Fee,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);

                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto4()
        {
            var qry = from w in m_dbData3060.tblpaypals
                      where w.Imported == null && w.Currency == "DKK"
                      && (w.Type == "Refund")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance,
                    dato = b.Date,
                    tekst = "Refund",
                    belob = b.Gross,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);

                recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance + b.Fee,
                    dato = b.Date,
                    tekst = "PayPal Fee",
                    belob = b.Fee,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.tblbankkontos.InsertOnSubmit(recBankkonto);

                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto5()
        {
            var qry = from w in m_dbData3060.tblpaypals
                      where w.Imported == null && w.Currency == "DKK"
                      && (w.Type == "Withdraw Funds to Bank Account")
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance,
                    dato = b.Date,
                    tekst = "Overført til Nordea",
                    belob = b.Gross,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto6()
        {
            var qry = from w in m_dbData3060.tblpaypals
                      where w.Imported == null //&& w.Currency == "DKK"
                      && (w.Type == "Express Checkout Payment Sent"
                      || w.Type == "Preapproved Payment Sent"
                      || w.Type == "Recurring Payment Sent"
                      || w.Type == "Web Accept Payment Sent"
                      || w.Type == "Shopping Cart Payment Sent"
                      )
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                decimal? GrossDKK = null;
                string kontoudtogstekst = "";
                if (b.Currency != "DKK")
                {
                    var qry2 = from w in m_dbData3060.tblpaypals
                               where w.Type == "Currency Conversion" && w.Reference_Txn_ID == b.Transaction_ID && w.Reference_Txn_ID == b.Transaction_ID && w.Currency == "DKK"
                               select w;
                    if (qry2.Count() == 1)
                    {
                        kontoudtogstekst = b.Name + " " + b.Gross + " " + b.Currency.ToLower();
                        GrossDKK = qry2.First().Gross;
                    }
                }
                else
                {
                    kontoudtogstekst = b.Name;
                    GrossDKK = b.Gross;
                }

                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance,
                    dato = b.Date,
                    tekst = kontoudtogstekst,
                    belob = GrossDKK,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);
                m_dbData3060.SubmitChanges();
            }
        }

        private void load_bankkonto7()
        {
            var qry = from w in m_dbData3060.tblpaypals
                      where w.Imported == null && w.Currency == "DKK"
                      && (w.Type == "Web Accept Payment Received"
                      || w.Type == "Recurring Payment Received"
                      )
                      orderby w.Date
                      select w;

            int antal = qry.Count();
            foreach (var b in qry)
            {
                tblbankkonto recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance,
                    dato = b.Date,
                    tekst = b.Item_Title.Length > 50 ? b.Item_Title.Substring(0, 50) : b.Item_Title,
                    belob = b.Gross,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);

                recBankkonto = new tblbankkonto
                {
                    bankkontoid = m_bankkontoid,
                    saldo = b.Balance + b.Fee,
                    dato = b.Date,
                    tekst = "PayPal Fee",
                    belob = b.Fee,
                };
                b.Imported = true;
                b.tblbankkontos.Add(recBankkonto);

                m_dbData3060.SubmitChanges();
            }
        }

    }
}
