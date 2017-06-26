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

namespace Medlem3060uc
{
    class clsUniconta
    {


        public int BogforPaypalBetalinger()
        {
            var rec_regnskab = UCInitializer.CurrentCompanyFinanceYear;
            if (rec_regnskab.Closed == true) return 0;

            DateTime? Startdato = rec_regnskab._FromDate;
            DateTime? Slutdato = rec_regnskab._ToDate;

            puls3060_nyEntities jdb = new puls3060_nyEntities(true);
            clsPbs602 objPbs602 = new clsPbs602();
            MemBogfoeringsKlader bogf = objPbs602.konter_paypal_betalinger_fra_rsmembership(jdb, (DateTime)Startdato, (DateTime)Slutdato);

            int AntalBetalinger = 0;
            if (bogf.Count() > 0)
            {
                DateTime nu = DateTime.Now;
                DateTime ToDay = new DateTime(nu.Year, nu.Month, nu.Day); ;

                Program.karKladde = null;
                foreach (var b in bogf)
                {
                    recKladde kl = new recKladde
                    {
                        Dato = b.Dato,
                        Bilag = b.Bilag,
                        Tekst = b.Tekst,
                        Afstemningskonto = b.Afstemningskonto,
                        Belob = b.Belob,
                        Kontonr = b.Kontonr,
                        Faknr = b.Faknr,
                        Sagnr = b.Sagnr
                    };
                    Program.karKladde.Add(kl);
                    AntalBetalinger = (int)b.Bilag;
                }
                InsertGLDailyJournalLines(Program.karKladde);
                Program.dbData3060.SubmitChanges();
            }
            return AntalBetalinger;
        }

        public int BogforIndBetalinger()
        {
            var rec_regnskab = UCInitializer.CurrentCompanyFinanceYear;
            if (rec_regnskab.Closed == true) return 0;

            DateTime? Startdato = rec_regnskab._FromDate;
            DateTime? Slutdato = rec_regnskab._ToDate;


            int saveBetid = 0;
            var bogf = from bl in Program.dbData3060.tblbetlins
                       where (bl.pbstranskode == "0236" || bl.pbstranskode == "0297") && (Startdato <= bl.indbetalingsdato && bl.indbetalingsdato <= Slutdato)
                       join b in Program.dbData3060.tblbets on bl.betid equals b.id
                       where b.summabogfort == null || b.summabogfort == false //<<-------------------------------
                       join p in Program.dbData3060.tblfrapbs on b.frapbsid equals p.id
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
                Program.karKladde = null;

                int count = 0;
                foreach (var b in bogf)
                {

                    if (saveBetid != b.Betid) // ny gruppe
                    {
                        saveBetid = b.Betid;
                        recKladde gkl = new recKladde
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
                        Program.karKladde.Add(gkl);

                        var rec_bet = (from ub in Program.dbData3060.tblbets where ub.id == b.Betid select ub).First();
                        rec_bet.summabogfort = true;

                    }

                    var msm = from f in Program.dbData3060.tblfaks
                              where f.faknr == b.faknr
                              join m in Program.dbData3060.tblrsmembership_transactions on f.id equals m.id
                              select new { f.faknr, f.Nr, m.name, f.bogfkonto, f.fradato, f.tildato };

                    if (msm.Count() == 1) //Kontingent betaling for RSMembership
                    {
                        var f = msm.First();
                        decimal[] arrBelob = clsPbs602.fordeling((decimal)b.indbetalingsbelob, (DateTime)f.fradato, (DateTime)f.tildato, (DateTime)Startdato, (DateTime)Slutdato);
                        recKladde kl;
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
                            kl = new recKladde
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
                            Program.karKladde.Add(kl);
                        }

                        if (arrBelob[1] > 0)
                        {
                            kl = new recKladde
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
                            Program.karKladde.Add(kl);
                        }
                    }
                    else //Anden betaling
                    {
                        recKladde kl = new recKladde
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
                        Program.karKladde.Add(kl);
                    }
                }
                InsertGLDailyJournalLines(Program.karKladde);
                Program.dbData3060.SubmitChanges();
            }
            return AntalBetalinger;
        }

        async public void InsertGLDailyJournalLines(KarKladde karKladde)
        {
            var api = UCInitializer.GetBaseAPI;
            var col3 = await api.Query<NumberSerieClient>();
            int NR = 4;
            var crit = new List<PropValuePair>();
            var pair = PropValuePair.GenereteWhereElements("KeyStr", typeof(String), "Dag");
            crit.Add(pair);
            var col = await api.Query<GLDailyJournalClient>(null, crit);
            var rec_Master = col.FirstOrDefault();

            foreach (var kk in karKladde)
            {

                GLDailyJournalLineClient jl = new GLDailyJournalLineClient()
                {
                    Date = (DateTime)kk.Dato,
                    Text = kk.Tekst,
                };
                if((kk.Afstemningskonto == "Bank") 
                && (kk.Kontonr == null))
                {
                    jl.Account = "5820";
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

                if ((kk.Afstemningskonto == null)
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
                var err = await api.Insert(jl);
            }


        }
    }
}
