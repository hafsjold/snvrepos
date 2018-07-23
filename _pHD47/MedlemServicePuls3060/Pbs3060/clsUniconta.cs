using System;
using System.Collections.Generic;
using System.Text;
using Uniconta.API.System;
using Uniconta.DataModel;
using System.Linq;
using Uniconta.Common;
using Uniconta.ClientTools.DataModel;
using Uniconta.API.DebtorCreditor;
using Uniconta.API.GeneralLedger;

namespace Pbs3060
{
    public class clsUniconta
    {
        private CrudAPI m_api { get; set; }
        private CompanyFinanceYear m_CurrentCompanyFinanceYear { get; set; }
        private dbData3060DataContext m_dbData3060 { get; set; }

        public clsUniconta(dbData3060DataContext p_dbData3060, CrudAPI api)
        {
            m_dbData3060 = p_dbData3060;
            m_api = api;
            var task = api.Query<CompanyFinanceYear>();
            task.Wait();
            var cols = task.Result;
            foreach (var col in cols)
            {
                if (col._Current)
                {
                    m_CurrentCompanyFinanceYear = col;
                }
            }
        }

        public int BogforIndBetalinger()
        {
            if (m_CurrentCompanyFinanceYear.Closed == true) return 0;

            DateTime? Startdato = m_CurrentCompanyFinanceYear._FromDate;
            DateTime? Slutdato = m_CurrentCompanyFinanceYear._ToDate;


            int saveBetid = 0;
            var bogf = (from bl in m_dbData3060.Tblbetlin
                        where (bl.Pbstranskode == "0236" || bl.Pbstranskode == "0297") && (Startdato <= bl.Indbetalingsdato && bl.Indbetalingsdato <= Slutdato)
                        join b in m_dbData3060.Tblbet on bl.Betid equals b.Id
                        where b.Summabogfort == null || b.Summabogfort == false //<<-------------------------------
                        join p in m_dbData3060.Tblfrapbs on b.Frapbsid equals p.Id
                        orderby p.Id, b.Id, bl.Id
                        select new betrec
                        {
                            Frapbsid = p.Id,
                            Leverancespecifikation = p.Leverancespecifikation,
                            Betid = b.Id,
                            GruppeIndbetalingsbelob = b.Indbetalingsbelob,
                            Betlinid = bl.Id,
                            Betalingsdato = bl.Betalingsdato,
                            Indbetalingsdato = bl.Indbetalingsdato,
                            Indbetalingsbelob = bl.Indbetalingsbelob,
                            Faknr = bl.Faknr,
                            Debitorkonto = bl.Debitorkonto,
                            Nr = bl.Nr
                        }).ToList();

            int AntalBetalinger = bogf.Count();

            foreach (var b in bogf)
            {
                var critMedlem = new List<PropValuePair>();
                var pairMedlem = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), b.Nr.ToString());
                critMedlem.Add(pairMedlem);
                var taskMedlem = m_api.Query<Medlem>(critMedlem);
                taskMedlem.Wait();
                var resultMedlem = taskMedlem.Result;
                var antalMedlem = resultMedlem.Count();
                if (antalMedlem == 1)
                {
                    var recMedlem = resultMedlem.First();
                    b.DebitorNavn = recMedlem.KeyName;
                }
                else
                {
                    b.DebitorNavn = "Ukendt medlem";
                }
            }

            if (bogf.Count() > 0)
            {
                DateTime nu = DateTime.Now;
                DateTime ToDay = new DateTime(nu.Year, nu.Month, nu.Day); ;

                int BS1_SidsteNr = 0;
                MemBogfoeringsKlader karKladde = new MemBogfoeringsKlader();

                int count = 0;
                foreach (var b in bogf)
                {

                    if (saveBetid != b.Betid) // ny gruppe
                    {
                        saveBetid = b.Betid;
                        recBogfoeringsKlader gkl = new recBogfoeringsKlader
                        {
                            Dato = ToDay,
                            Bilag = ++BS1_SidsteNr,
                            Tekst = "Indbetalingskort K 81131945-" + ((DateTime)b.Indbetalingsdato).Day + "." + ((DateTime)b.Indbetalingsdato).Month,
                            Afstemningskonto = "Bank",
                            Belob = b.GruppeIndbetalingsbelob,
                            Kontonr = null,
                            Faknr = null,
                            Sagnr = null
                        };
                        karKladde.Add(gkl);

                        var rec_bet = (from ub in m_dbData3060.Tblbet where ub.Id == b.Betid select ub).First();
                        rec_bet.Summabogfort = true;

                    }

                    char[] trim0 = { '0' };
                    IQueryable<msmrecs> msm;
                    if (b.Faknr != 0) //Indbetalingskort sendt af Nets
                    {
                        msm = from f in m_dbData3060.Tblfak
                              where f.Faknr == b.Faknr
                              select new msmrecs
                              {
                                  faknr = f.Faknr,
                                  Nr = f.Nr,
                                  name = b.DebitorNavn,
                                  bogfkonto = f.Bogfkonto,
                                  fradato = f.Fradato,
                                  tildato = f.Tildato
                              };

                    }
                    else //Indbetalingskort ikke sendt af Nets
                    {
                        msm = from f in m_dbData3060.Tblfak
                              where f.Indbetalerident.TrimStart(trim0) == b.Debitorkonto.TrimStart(trim0)
                              select new msmrecs
                              {
                                  faknr = f.Faknr,
                                  Nr = f.Nr,
                                  name = b.DebitorNavn,
                                  bogfkonto = f.Bogfkonto,
                                  fradato = f.Fradato,
                                  tildato = f.Tildato
                              };
                    }

                    if (msm.Count() == 1) //Kontingent betaling for RSMembership
                    {
                        var f = msm.First();
                        decimal[] arrBelob = clsPbs602.fordeling((decimal)b.Indbetalingsbelob, (DateTime)f.fradato, (DateTime)f.tildato, (DateTime)Startdato, (DateTime)Slutdato);
                        recBogfoeringsKlader kl;
                        string wTekst = ("F" + f.faknr + " " + f.Nr + " " + f.name).PadRight(40, ' ').Substring(0, 40);
                        try
                        {
                            if (((DateTime)b.Indbetalingsdato - (DateTime)b.Betalingsdato).Days > 300) // faktura mere end 300 dage gammel
                            {
                                wTekst = ("???" + wTekst).PadRight(40, ' ').Substring(0, 40);
                            }
                        }
                        catch { }

                        if (arrBelob[0] > 0)
                        {
                            kl = new recBogfoeringsKlader
                            {
                                Dato = ToDay,
                                Bilag = BS1_SidsteNr,
                                Tekst = wTekst,
                                Afstemningskonto = null,
                                Belob = arrBelob[0],
                                Kontonr = 1003,  //f.bogfkonto,
                                Faknr = null,
                                Sagnr = null
                            };
                            karKladde.Add(kl);
                        }

                        if (arrBelob[1] > 0)
                        {
                            kl = new recBogfoeringsKlader
                            {
                                Dato = ToDay,
                                Bilag = BS1_SidsteNr,
                                Tekst = wTekst,
                                Afstemningskonto = null,
                                Belob = arrBelob[1],
                                Kontonr = 6831, //64200,
                                Faknr = null,
                                Sagnr = null
                            };
                            karKladde.Add(kl);
                        }
                    }
                    else //Anden betaling
                    {
                        recBogfoeringsKlader kl = new recBogfoeringsKlader
                        {
                            Dato = ToDay,
                            Bilag = BS1_SidsteNr,
                            Tekst = ("Ukendt betaling").PadRight(40, ' ').Substring(0, 40),
                            Afstemningskonto = null,
                            Belob = b.Indbetalingsbelob,
                            Kontonr = 6833, //65050,
                            Faknr = null,
                            Sagnr = null

                        };
                        karKladde.Add(kl);
                    }
                }
                InsertGLDailyJournalLines(karKladde);
                m_dbData3060.SaveChanges();
            }
            return AntalBetalinger;
        }

        public void InsertGLDailyJournalLines(MemBogfoeringsKlader karKladde)
        {
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var task1 = m_api.Query<GLDailyJournalClient>(crit);
            task1.Wait();
            var col = task1.Result;
            var rec_Master = col.FirstOrDefault();

            foreach (var kk in karKladde)
            {

                GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                {
                    Date = (DateTime)kk.Dato,
                    Text = kk.Tekst,
                };
                if ((kk.Afstemningskonto == "Bank")
                && (kk.Kontonr == null))
                {
                    jl.Account = "5840";
                    if (kk.Belob > 0)
                    {
                        jl.Debit = (double)kk.Belob;
                    }
                    else
                    {
                        jl.Credit = -(double)kk.Belob;
                    }
                }

                if ((kk.Afstemningskonto == "PayPal")
                && (kk.Kontonr == null))
                {
                    jl.Account = "5830";
                    if (kk.Belob > 0)
                    {
                        jl.Debit = (double)kk.Belob;
                    }
                    else
                    {
                        jl.Credit = -(double)kk.Belob;
                    }
                }

                if ((kk.Afstemningskonto == "PayPal") //NEW
                && (kk.Kontonr != null))
                {
                    jl.Account = "5830";
                    jl.OffsetAccount = kk.Kontonr.ToString();
                    if (kk.Belob > 0)
                    {
                        jl.Debit = (double)kk.Belob;
                    }
                    else
                    {
                        jl.Credit = -(double)kk.Belob;
                    }
                }

                if ((String.IsNullOrEmpty(kk.Afstemningskonto))
                && (kk.Kontonr != null))
                {
                    jl.Account = kk.Kontonr.ToString();
                    if (kk.Belob > 0)
                    {
                        jl.Credit = (double)kk.Belob;
                    }
                    else
                    {
                        jl.Debit = -(double)kk.Belob;
                    }
                }
                jl.SetMaster(rec_Master);
                var task2 = m_api.Insert(jl);
                task2.Wait();
                var err = task2.Result;
            }
        }

        public void TestFakturering()
        {
            InvoiceAPI invoiceAPI = new InvoiceAPI(m_api);

            var taskDebtorOrder = m_api.Query<DebtorOrderClient>();
            taskDebtorOrder.Wait();
            var DebtorOrders = taskDebtorOrder.Result;
            foreach (var DebtorOrder in DebtorOrders)
            {
                List<DebtorOrderClient> Masters = new List<DebtorOrderClient>();
                Masters.Add(DebtorOrder);
                var taskDebtorOrderLines = m_api.Query<DebtorOrderLineClient>(Masters, null);
                taskDebtorOrderLines.Wait();
                var DebtorOrderLines = taskDebtorOrderLines.Result;
                var taskInvoice = invoiceAPI.PostInvoice(DebtorOrder, DebtorOrderLines, DateTime.Now, 0, true, null, null);
                taskInvoice.Wait();
                InvoicePostingResult resultInvoice = taskInvoice.Result;
                ErrorCodes Err = resultInvoice.Err;
                DCInvoice Header = resultInvoice.Header;
                IEnumerable<InvTrans> Lines = resultInvoice.Lines;
                PostingResult ledgerRes = resultInvoice.ledgerRes;
                if (Err == ErrorCodes.Succes)
                {
                    var xxx = DebtorOrder.InvoiceInterval;
                    var yyy = DebtorOrder.Invoices;
                }

            }
        }
    }

    public class msmrecs
    {
        public int? faknr { get; set; }
        public int? Nr { get; set; }
        public string name { get; set; }
        public int? bogfkonto { get; set; }
        public DateTime? fradato { get; set; }
        public DateTime? tildato { get; set; }
    }

    public class betrec
    {
        public int Frapbsid { get; set; }
        public string Leverancespecifikation { get; set; }
        public int Betid { get; set; }
        public decimal? GruppeIndbetalingsbelob { get; set; }
        public int Betlinid { get; set; }
        public DateTime? Betalingsdato { get; set; }
        public DateTime? Indbetalingsdato { get; set; }
        public decimal? Indbetalingsbelob { get; set; }
        public int? Faknr { get; set; }
        public string Debitorkonto { get; set; }
        public int? Nr { get; set; }
        public string DebitorNavn { get; set; }
    }
}
