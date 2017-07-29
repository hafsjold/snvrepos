using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using nsPbs3060;
using Uniconta.ClientTools.DataModel;
using Uniconta.DataModel;
using Uniconta.Common;
using Uniconta.API.GeneralLedger;
using Uniconta.API.System;

namespace nsPbs3060
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

        public int BogforPaypalBetalinger()
        {
            if (m_CurrentCompanyFinanceYear.Closed == true) return 0;

            DateTime? Startdato = m_CurrentCompanyFinanceYear._FromDate;
            DateTime? Slutdato = m_CurrentCompanyFinanceYear._ToDate;

            puls3060_nyEntities jdb = new puls3060_nyEntities(true);
            clsPbs602 objPbs602 = new clsPbs602();
            MemBogfoeringsKlader bogf = objPbs602.konter_paypal_betalinger_fra_rsmembership(jdb, (DateTime)Startdato, (DateTime)Slutdato);

            int AntalBetalinger = 0;
            if (bogf.Count() > 0)
            {
                DateTime nu = DateTime.Now;
                DateTime ToDay = new DateTime(nu.Year, nu.Month, nu.Day); ;
                InsertGLDailyJournalLines(bogf);
                m_dbData3060.SubmitChanges();
            }
            return AntalBetalinger;
        }

        public int BogforIndBetalinger()
        {
            if (m_CurrentCompanyFinanceYear.Closed == true) return 0;

            DateTime? Startdato = m_CurrentCompanyFinanceYear._FromDate;
            DateTime? Slutdato = m_CurrentCompanyFinanceYear._ToDate;


            int saveBetid = 0;
            var bogf = from bl in m_dbData3060.tblbetlins
                       where (bl.pbstranskode == "0236" || bl.pbstranskode == "0297") && (Startdato <= bl.indbetalingsdato && bl.indbetalingsdato <= Slutdato)
                       join b in m_dbData3060.tblbets on bl.betid equals b.id
                       where b.summabogfort == null || b.summabogfort == false //<<-------------------------------
                       join p in m_dbData3060.tblfrapbs on b.frapbsid equals p.id
                       orderby p.id, b.id, bl.id
                       select new
                       {
                           Frapbsid = p.id,
                           p.leverancespecifikation,
                           Betid = b.id,
                           GruppeIndbetalingsbelob = b.indbetalingsbelob,
                           Betlinid = bl.id,
                           bl.betalingsdato,
                           bl.indbetalingsdato,
                           bl.indbetalingsbelob,
                           bl.faknr,
                           bl.debitorkonto
                       };
            int AntalBetalinger = bogf.Count();
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
                            Tekst = "Indbetalingskort K 81131945-" + ((DateTime)b.indbetalingsdato).Day + "." + ((DateTime)b.indbetalingsdato).Month,
                            Afstemningskonto = "Bank",
                            Belob = b.GruppeIndbetalingsbelob,
                            Kontonr = null,
                            Faknr = null,
                            Sagnr = null
                        };
                        karKladde.Add(gkl);

                        var rec_bet = (from ub in m_dbData3060.tblbets where ub.id == b.Betid select ub).First();
                        rec_bet.summabogfort = true;

                    }

                    char[] trim0 = {'0'};
                    IQueryable<msmrecs> msm;
                    if (b.faknr != 0)
                    {
                        msm = from f in m_dbData3060.tblfaks
                              where f.faknr == b.faknr
                              join m in m_dbData3060.tblrsmembership_transactions on f.id equals m.id
                              select new msmrecs
                              {
                                  faknr = f.faknr,
                                  Nr = f.Nr,
                                  name = m.name,
                                  bogfkonto = f.bogfkonto,
                                  fradato = f.fradato,
                                  tildato = f.tildato
                              }; 
                    }
                    else
                    {
                        msm = from f in m_dbData3060.tblfaks
                              where f.indbetalerident.TrimStart(trim0) == b.debitorkonto.TrimStart(trim0)
                              join m in m_dbData3060.tblrsmembership_transactions on f.id equals m.id
                              select new msmrecs
                              {
                                  faknr = f.faknr,
                                  Nr = f.Nr,
                                  name = m.name,
                                  bogfkonto = f.bogfkonto,
                                  fradato = f.fradato,
                                  tildato = f.tildato
                              };
                    }

                    if (msm.Count() == 1) //Kontingent betaling for RSMembership
                    {
                        var f = msm.First();
                        decimal[] arrBelob = clsPbs602.fordeling((decimal)b.indbetalingsbelob, (DateTime)f.fradato, (DateTime)f.tildato, (DateTime)Startdato, (DateTime)Slutdato);
                        recBogfoeringsKlader kl;
                        string wTekst = ("F" + f.faknr + " " + f.Nr + " " + f.name).PadRight(40, ' ').Substring(0, 40);
                        try
                        {
                            if (((DateTime)b.indbetalingsdato - (DateTime)b.betalingsdato).Days > 300) // faktura mere end 300 dage gammel
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
                            Belob = b.indbetalingsbelob,
                            Kontonr = 6833, //65050,
                            Faknr = null,
                            Sagnr = null

                        };
                        karKladde.Add(kl);
                    }
                }
                InsertGLDailyJournalLines(karKladde);
                m_dbData3060.SubmitChanges();
            }
            return AntalBetalinger;
        }

        public void InsertGLDailyJournalLines(MemBogfoeringsKlader karKladde)
        {
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var task1 = m_api.Query<GLDailyJournalClient>(null, crit);
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
}
