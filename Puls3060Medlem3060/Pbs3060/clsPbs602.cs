using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using nsPbs3060.PayPalServiceReference;

namespace nsPbs3060
{

    public class clsPbs602
    {
        private tblpbsforsendelse m_rec_pbsforsendelse;
        private tblpbsfilename m_rec_pbsfiles;
        private tblfrapb m_rec_frapbs;
        private tblbet m_rec_bet;
        private tblbetlin m_rec_betlin;

        public clsPbs602() { }

        public void TestRead042()
        {
            string sektion;
            string transkode;
            string rec;
            sektion = "0211";
            transkode = "0236";
            rec = "BS0420398564402360000000100000000001231312345678910120310000000012755000000125                       " +
            "  1212031212030000000012755";
            read042(sektion, transkode, rec);
        }
        public int betalinger_fra_pbs(dbData3060DataContext p_dbData3060)
        {
            string rec;
            string leverancetype;
            string leverancespecifikation;
            DateTime leverancedannelsesdato;
            string sektion;
            int wpbsfilesid;
            int wleveranceid;
            int AntalFiler = 0;
            //  wpbsfilesid = 3450  //'--test test
            //  leverancetype = "0602"
            //  sektion = "0211"
            //  rec = "BS0420398564402360000000100000000001231312345678910120310000000012755000000125                         3112031112030000000012755"

            var qrypbsfiles = from h in p_dbData3060.tblpbsfilenames
                              where h.pbsforsendelseid == null
                              join d in p_dbData3060.tblpbsfiles on h.id equals d.pbsfilesid
                              where d.seqnr == 1 && d.data.Substring(16, 4) == "0602" && d.data.Substring(0, 2) == "BS"
                              select new
                              {
                                  h.id,
                                  h.path,
                                  h.filename,
                                  leverancetype = d.data.Substring(16, 4),
                                  delsystem = d.data.Substring(13, 3),
                              };

            foreach (var rstpbsfiles in qrypbsfiles)
            {
                try
                {
                    wpbsfilesid = rstpbsfiles.id;
                    AntalFiler++;
                    leverancetype = "";
                    sektion = "";
                    leverancespecifikation = "";

                    var qrypbsfile = from d in p_dbData3060.tblpbsfiles
                                     where d.pbsfilesid == wpbsfilesid && d.data.Substring(0, 6) != "PBCNET"
                                     orderby d.seqnr
                                     select d;

                    foreach (var rstpbsfile in qrypbsfile)
                    {
                        rec = rstpbsfile.data;
                        // -- Bestem Leverance Type
                        if (rstpbsfile.seqnr == 1)
                        {
                            if ((rec.Substring(0, 5) == "BS002"))
                            {
                                // -- Leverance Start
                                leverancetype = rec.Substring(16, 4);
                                leverancespecifikation = rec.Substring(20, 10);
                                leverancedannelsesdato = DateTime.Parse("20" + rec.Substring(53, 2) + "-" + rec.Substring(51, 2) + "-" + rec.Substring(49, 2));
                            }
                            else
                            {
                                throw new Exception("241 - Første record er ikke en Leverance start record");
                            }
                            if ((leverancetype == "0602"))
                            {
                                // -- Leverance 0602
                                var antal = (from c in p_dbData3060.tblfrapbs
                                             where c.leverancespecifikation == leverancespecifikation
                                             select c).Count();
                                if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere"); }

                                wleveranceid = (int)(from r in p_dbData3060.nextval("leveranceid") select r.id).First();
                                m_rec_pbsforsendelse = new tblpbsforsendelse
                                {
                                    delsystem = "BS1",
                                    leverancetype = "0602",
                                    oprettetaf = "Bet",
                                    oprettet = DateTime.Now,
                                    leveranceid = wleveranceid
                                };
                                p_dbData3060.tblpbsforsendelses.InsertOnSubmit(m_rec_pbsforsendelse);

                                m_rec_frapbs = new tblfrapb
                                {
                                    delsystem = "BS1",
                                    leverancetype = "0602",
                                    leverancespecifikation = leverancespecifikation,
                                    leverancedannelsesdato = leverancedannelsesdato,
                                    udtrukket = DateTime.Now
                                };
                                m_rec_pbsforsendelse.tblfrapbs.Add(m_rec_frapbs);

                                m_rec_pbsfiles = (from c in p_dbData3060.tblpbsfilenames
                                                  where c.id == rstpbsfiles.id
                                                  select c).First();
                                m_rec_pbsforsendelse.tblpbsfilenames.Add(m_rec_pbsfiles);
                            }
                        }
                        if ((leverancetype == "0602"))
                        {
                            // -- Leverance 0602*********
                            // -- Bestem Sektions Type
                            if ((sektion == ""))
                            {
                                if ((rec.Substring(0, 5) == "BS012"))
                                {
                                    // -- Sektion Start
                                    sektion = rec.Substring(13, 4);
                                }
                                else if (!((rec.Substring(0, 5) == "BS992") || (rec.Substring(0, 5) == "BS002")))
                                {
                                    throw new Exception("243 - Første record er ikke en Sektions start record");
                                }
                            }
                            if ((rec.Substring(0, 5) == "BS002"))
                            {
                                // -- Leverance start
                                // -- BEHANDL: Leverance start
                            }
                            else if ((sektion == "0211"))
                            {
                                // -- Sektion 0211 Betalingsinformation
                                if (((rec.Substring(0, 5) == "BS012")
                                            && (rec.Substring(13, 4) == "0211")))
                                {
                                    // -- Sektion Start
                                    // -- BEHANDL: Sektion Start
                                }
                                else if (((rec.Substring(0, 5) == "BS042")
                                            && (rec.Substring(13, 4) == "0236")))
                                {
                                    // -- Gennemf?rt automatisk betaling
                                    // -- BEHANDL: Gennemf?rt automatisk betaling
                                    read042(sektion, "0236", rec);
                                }
                                else if (((rec.Substring(0, 5) == "BS042")
                                            && (rec.Substring(13, 4) == "0237")))
                                {
                                    // -- Afvist betaling
                                    // -- BEHANDL: Afvist betaling
                                    read042(sektion, "0237", rec);
                                }
                                else if (((rec.Substring(0, 5) == "BS042")
                                            && (rec.Substring(13, 4) == "0238")))
                                {
                                    // -- Afmeldt betaling
                                    // -- BEHANDL: Afmeldt betaling
                                    read042(sektion, "0238", rec);
                                }
                                else if (((rec.Substring(0, 5) == "BS042")
                                            && (rec.Substring(13, 4) == "0239")))
                                {
                                    // -- Tilbagef?rt betaling
                                    // -- BEHANDL: Tilbagef?rt betaling
                                    read042(sektion, "0239", rec);
                                }
                                else if (((rec.Substring(0, 5) == "BS092")
                                            && (rec.Substring(13, 4) == "0211")))
                                {
                                    // -- Sektion Slut
                                    // -- BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("244 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                                }
                            }
                            else if ((sektion == "0215"))
                            {
                                // -- Sektion 0215 FI-Betalingsinformation
                                if (((rec.Substring(0, 5) == "BS012")
                                            && (rec.Substring(13, 4) == "0215")))
                                {
                                    // -- Sektion Start
                                    // -- BEHANDL: Sektion Start
                                }
                                else if (((rec.Substring(0, 5) == "BS042")
                                            && (rec.Substring(13, 4) == "0297")))
                                {
                                    // -- Gennemf?rt FI-betaling
                                    // -- BEHANDL: Gennemf?rt FI-betaling
                                    read042(sektion, "0297", rec);
                                }
                                else if (((rec.Substring(0, 5) == "BS042")
                                            && (rec.Substring(13, 4) == "0299")))
                                {
                                    // -- Tilbagef?rt FI-betaling
                                    // -- BEHANDL: Tilbagef?rt FI-betaling
                                    read042(sektion, "0299", rec);
                                }
                                else if (((rec.Substring(0, 5) == "BS092")
                                            && (rec.Substring(13, 4) == "0215")))
                                {
                                    // -- Sektion Slut
                                    // -- BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("245 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                                }
                            }
                            else if ((rec.Substring(0, 5) == "BS992"))
                            {
                                // -- Leverance slut
                                // -- BEHANDL: Leverance Slut
                                leverancetype = "";
                            }
                            else
                            {
                                throw new Exception("246 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                            }
                        }
                        else
                        {
                            throw new Exception("247 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                        }
                    }  // slut rstpbsfile

                    // -- Update indbetalingsbelob
                    foreach (tblbet rec_bet in m_rec_frapbs.tblbets)
                    {
                        var SumIndbetalingsbelob = (
                            from c in rec_bet.tblbetlins
                            group c by c.betid into g
                            select new { Betid = g.Key, SumIndbetalingsbelob = g.Sum(c => c.indbetalingsbelob) }
                            ).First().SumIndbetalingsbelob;

                        rec_bet.indbetalingsbelob = SumIndbetalingsbelob;
                    }

                }
                catch (Exception e)
                {
                    switch (e.Message.Substring(0, 3))
                    {
                        case "241":   //241 - Første record er ikke en Leverance start record
                        case "242":   //242 - Leverancen er indlæst tidligere
                        case "243":   //243 - Første record er ikke en Sektions start record
                        case "244":   //244 - Record ukendt
                        case "245":   //245 - Record ukendt
                        case "246":   //246 - Record ukendt
                        case "247":   //247 - Record ukendt
                            AntalFiler--;
                            break;

                        default:
                            throw;
                    }
                }
            }  // slut rstpbsfiles
            p_dbData3060.SubmitChanges();
            return AntalFiler;
        }

        public void read042(string sektion, string transkode, string rec)
        {
            int fortegn;
            decimal belobmun;
            int belob;

            // --  pbssektionnr
            // --  pbstranskode
            // - transkode 0236, gennemført automatisk betaling
            // - transkode 0237, afvist automatisk betaling
            // - transkode 0238, afmeldt automatisk betaling
            // - transkode 0239, tilbageført betaling
            m_rec_betlin = new tblbetlin
            {
                pbssektionnr = sektion,
                pbstranskode = transkode
            };

            // --  debitorkonto
            if ((sektion == "0211"))
            {
                m_rec_betlin.Nr = int.Parse(rec.Substring(33, 7)); //***MHA***
                m_rec_betlin.debitorkonto = rec.Substring(25, 15);
            }
            else if ((sektion == "0215"))
            {
                m_rec_betlin.Nr = int.Parse(rec.Substring(37, 7));
                m_rec_betlin.debitorkonto = rec.Substring(29, 15);
            }
            else
            {
                m_rec_betlin.Nr = null;
                m_rec_betlin.debitorkonto = null;
            }

            // --  aftalenr
            if ((sektion == "0211"))
            {
                m_rec_betlin.aftalenr = int.Parse(rec.Substring(40, 9));
            }
            else
            {
                m_rec_betlin.aftalenr = null;
            }

            // --  pbskortart
            if ((sektion == "0215"))
            {
                m_rec_betlin.pbskortart = rec.Substring(44, 2);
            }
            else
            {
                m_rec_betlin.pbskortart = null;
            }

            // --  pbsgebyrbelob
            if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(46, 1));
                belob = int.Parse(rec.Substring(47, 5));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.pbsgebyrbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.pbsgebyrbelob = belobmun;
                }
                else
                {
                    m_rec_betlin.pbsgebyrbelob = 0;
                }
            }
            else
            {
                m_rec_betlin.pbsgebyrbelob = 0;
            }

            // --  betalingsdato
            if ((sektion == "0211"))
            {
                if ((rec.Substring(49, 6) != "000000"))
                {
                    m_rec_betlin.betalingsdato = DateTime.Parse("20"
                                + (rec.Substring(53, 2) + ("-"
                                + (rec.Substring(51, 2) + ("-" + rec.Substring(49, 2))))));
                }
                else
                {
                    m_rec_betlin.betalingsdato = null;
                }
            }
            else if ((sektion == "0215"))
            {
                if ((rec.Substring(52, 6) != "000000"))
                {
                    m_rec_betlin.betalingsdato = DateTime.Parse("20"
                                + (rec.Substring(56, 2) + ("-"
                                + (rec.Substring(54, 2) + ("-" + rec.Substring(52, 2))))));
                }
                else
                {
                    m_rec_betlin.betalingsdato = null;
                }
            }
            else
            {
                m_rec_betlin.betalingsdato = null;
            }

            // --  belob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(56, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.belob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.belob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    m_rec_betlin.belob = (belobmun * -1);
                }
                else
                {
                    m_rec_betlin.belob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(59, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.belob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.belob = belobmun;
                }
                else
                {
                    m_rec_betlin.belob = null;
                }
            }
            else
            {
                m_rec_betlin.belob = null;
            }

            // --  faknr
            if ((sektion == "0211"))
            {
                m_rec_betlin.faknr = int.Parse("0" + rec.Substring(69, 9).Trim()); //***MHA***
            }
            else if ((sektion == "0215"))
            {
                m_rec_betlin.faknr = int.Parse("0" + rec.Substring(72, 9).Trim());
            }
            else
            {
                m_rec_betlin.faknr = null;
            }

            // --  pbsarkivnr
            if ((sektion == "0215"))
            {
                m_rec_betlin.pbsarkivnr = rec.Substring(81, 22);
            }
            else
            {
                m_rec_betlin.pbsarkivnr = null;
            }

            // --  indbetalingsdato
            if ((rec.Substring(103, 6) != "000000"))
            {
                m_rec_betlin.indbetalingsdato = DateTime.Parse("20"
                            + (rec.Substring(107, 2) + ("-"
                            + (rec.Substring(105, 2) + ("-" + rec.Substring(103, 2))))));
            }
            else
            {
                m_rec_betlin.indbetalingsdato = null;
            }

            // --  bogforingsdato
            if ((rec.Substring(109, 6) != "000000"))
            {
                m_rec_betlin.bogforingsdato = DateTime.Parse("20"
                            + (rec.Substring(113, 2) + ("-"
                            + (rec.Substring(111, 2) + ("-" + rec.Substring(109, 2))))));
            }
            else
            {
                m_rec_betlin.bogforingsdato = null;
            }

            // --  indbetalingsbelob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.indbetalingsbelob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    m_rec_betlin.indbetalingsbelob = (belobmun * -1);
                }
                else
                {
                    m_rec_betlin.indbetalingsbelob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.indbetalingsbelob = belobmun;
                }
                else
                {
                    m_rec_betlin.indbetalingsbelob = null;
                }
            }
            else
            {
                m_rec_betlin.indbetalingsbelob = null;
            }


            // Find or Create tblber record
            try
            {
                m_rec_bet = (from c in m_rec_frapbs.tblbets
                             where c.pbssektionnr == sektion
                                && c.transkode == transkode
                                && c.bogforingsdato == m_rec_betlin.bogforingsdato
                             select c).First();

            }
            catch (System.InvalidOperationException)
            {
                m_rec_bet = new tblbet
                {
                    pbssektionnr = sektion,
                    transkode = transkode,
                    bogforingsdato = m_rec_betlin.bogforingsdato
                };
                m_rec_frapbs.tblbets.Add(m_rec_bet);
            }

            // Add tblbetlin
            m_rec_bet.tblbetlins.Add(m_rec_betlin);
        }

        public int betalinger_til_rsmembership(dbData3060DataContext p_dbData3060, puls3060_dkEntities p_dbPuls3060_dk)
        {
            int saveBetid = 0;
            var rsmbrshp = from bl in p_dbData3060.tblbetlins
                           where (bl.pbstranskode == "0236" || bl.pbstranskode == "0297")
                           join b in p_dbData3060.tblbets on bl.betid equals b.id
                           where b.rsmembership == null || b.rsmembership == false
                           join p in p_dbData3060.tblfrapbs on b.frapbsid equals p.id
                           orderby p.id, b.id, bl.id
                           select new
                           {
                               Frapbsid = p.id,
                               p.leverancespecifikation,
                               Betid = b.id,
                               Betlinid = bl.id,
                               bl.betalingsdato,
                               bl.indbetalingsdato,
                               bl.indbetalingsbelob,
                               bl.faknr,
                               bl.debitorkonto
                           };

            int AntalBetalinger = rsmbrshp.Count();
            Program.Log(string.Format("betalinger_til_rsmembership - AntalBetalinger {0}", AntalBetalinger));
            if (rsmbrshp.Count() > 0)
            {
                clsPbsHelper objPbsHelperd = new clsPbsHelper();
                foreach (var b in rsmbrshp)
                {
                    if (saveBetid != b.Betid) // ny gruppe
                    {
                        saveBetid = b.Betid;
                        var rec_bet = (from ub in p_dbData3060.tblbets where ub.id == b.Betid select ub).First();
                        rec_bet.rsmembership = true;
                    }

                    // Do somthing here
                    var qry = from f in p_dbData3060.tblfaks
                              where f.faknr == b.faknr
                              join m in p_dbData3060.tblrsmembership_transactions on f.id equals m.id
                              select new tblmembershippayment
                              {
                                  rsmembership_transactions_id = m.trans_id,
                              };
                    if (qry.Count() == 1)
                    {
                        tblmembershippayment rec_membershippayment = qry.First();
                        //*********************************************************
                        int new_rsmembership_transactions_id = objPbsHelperd.OpdateringAfSlettet_rsmembership_transaction(rec_membershippayment.rsmembership_transactions_id, p_dbData3060);
                        Program.Log(string.Format("betalinger_til_rsmembership - transactions_id {0} --> {1}", rec_membershippayment.rsmembership_transactions_id, p_dbData3060, new_rsmembership_transactions_id));
                        rec_membershippayment.rsmembership_transactions_id = new_rsmembership_transactions_id;
                         //*********************************************************
                        p_dbPuls3060_dk.tblmembershippayments.Add(rec_membershippayment);
                        p_dbPuls3060_dk.SaveChanges();
                        Program.Log(string.Format("betalinger_til_rsmembership - faknr {0} betalt", b.faknr));
                    }
                }
                p_dbData3060.SubmitChanges();
            }
            return AntalBetalinger;
        }

        public MemBogfoeringsKlader konter_paypal_betalinger_fra_rsmembership(puls3060_dkEntities p_dbPuls3060_dk)
        {
            DateTime Regnskabsaar_Startdato = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime Regnskabsaar_Slutdato = new DateTime(DateTime.Now.Year, 12, 31);
            return konter_paypal_betalinger_fra_rsmembership(p_dbPuls3060_dk, Regnskabsaar_Startdato, Regnskabsaar_Slutdato);
        }

        public MemBogfoeringsKlader konter_paypal_betalinger_fra_rsmembership(puls3060_dkEntities p_dbPuls3060_dk, DateTime Regnskabsaar_Startdato, DateTime Regnskabsaar_Slutdato)
        {
            clsPayPal objPayPal = new clsPayPal();
            MemBogfoeringsKlader klader = new MemBogfoeringsKlader();
            int BS1_SidsteNr = 0;
            var qry_rsmembership = from s in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers
                                   where s.membership_id == 6
                                   join tl in p_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.last_transaction_id equals tl.id
                                   where tl.gateway == "PayPal"
                                   join p in p_dbPuls3060_dk.tblpaypalpayments on tl.hash equals p.paypal_transactions_id into p1
                                   from p in p1.DefaultIfEmpty()
                                   where p.bogfoert == null || p.bogfoert == false
                                   join u in p_dbPuls3060_dk.ecpwt_users on s.user_id equals u.id
                                   select new
                                   {
                                       paypal_transaction_id = tl.hash,
                                       Navn = u.name,
                                       s.membership_id,
                                       s.membership_start,
                                       s.membership_end,
                                   };
            var arr_rsmembership = qry_rsmembership.ToArray();

            if (arr_rsmembership.Count() > 0)
            {
                foreach (var rsmembership in arr_rsmembership)
                {
                    if (rsmembership.paypal_transaction_id.Length == 0)
                        continue;
                    
                    PaymentTransactionSearchResultType paypal_trans = objPayPal.getPayPalTransaction(rsmembership.paypal_transaction_id);

                    if (paypal_trans != null)
                    {
                        decimal Belob = decimal.Parse(paypal_trans.GrossAmount.Value.Replace(".", ","));
                        decimal[] arrBeløb = clsPbs602.fordeling(Belob, rsmembership.membership_start, rsmembership.membership_end, Regnskabsaar_Startdato, Regnskabsaar_Slutdato);
                        string stdate = string.Format(" {0}-{1}", rsmembership.membership_start.ToString("d.M.yyyy"), rsmembership.membership_end.ToString("d.M.yyyy"));

                        int wBilag = ++BS1_SidsteNr;
                        recBogfoeringsKlader recklade = new recBogfoeringsKlader
                        {
                            Dato = paypal_trans.Timestamp,
                            Bilag = wBilag,
                            Tekst = ("Paypal: " + rsmembership.paypal_transaction_id).PadRight(40, ' ').Substring(0, 40),
                            Afstemningskonto = "PayPal",
                            Belob = Belob,
                            Kontonr = null,
                            Faknr = null,
                            Sagnr = null
                        };
                        klader.Add(recklade);

                        if (arrBeløb[0] > 0)
                        {
                            recklade = new recBogfoeringsKlader
                            {
                                Dato = paypal_trans.Timestamp,
                                Bilag = wBilag,
                                Tekst = (rsmembership.Navn + stdate).PadRight(40, ' ').Substring(0, 40),
                                Afstemningskonto = "",
                                Belob = arrBeløb[0],
                                Kontonr = 1800,
                                Faknr = null,
                                Sagnr = null
                            };
                            klader.Add(recklade);
                        }

                        if (arrBeløb[1] > 0)
                        {
                            recklade = new recBogfoeringsKlader
                            {
                                Dato = paypal_trans.Timestamp,
                                Bilag = wBilag,
                                Tekst = (rsmembership.Navn + stdate).PadRight(40, ' ').Substring(0, 40),
                                Afstemningskonto = "",
                                Belob = arrBeløb[1],
                                Kontonr = 64200,
                                Faknr = null,
                                Sagnr = null
                            };
                            klader.Add(recklade);
                        }

                        recklade = new recBogfoeringsKlader
                        {
                            Dato = paypal_trans.Timestamp,
                            Bilag = wBilag,
                            Tekst = ("PayPal Gebyr").PadRight(40, ' ').Substring(0, 40),
                            Afstemningskonto = "PayPal",
                            Belob = decimal.Parse(paypal_trans.FeeAmount.Value.Replace(".", ",")),
                            Kontonr = 9950,
                            Faknr = null,
                            Sagnr = null
                        };
                        klader.Add(recklade);

                        tblpaypalpayment rec_paypalpayments = (from p in p_dbPuls3060_dk.tblpaypalpayments where p.paypal_transactions_id == rsmembership.paypal_transaction_id select p).FirstOrDefault();
                        if (rec_paypalpayments == null)
                        {
                            rec_paypalpayments = new tblpaypalpayment
                            {
                                paypal_transactions_id = rsmembership.paypal_transaction_id,
                                bogfoert = true
                            };
                            p_dbPuls3060_dk.tblpaypalpayments.Add(rec_paypalpayments);
                        }
                        else
                        {
                            rec_paypalpayments.bogfoert = true;
                        }
                        p_dbPuls3060_dk.SaveChanges();
                        
                    }
                }
            }



            return klader;
        }

        public static decimal[] fordeling(decimal Belob, DateTime Bilag_Startdato, DateTime Bilag_Slutdato, DateTime Regnskabsaar_Startdato, DateTime Regnskabsaar_Slutdato)
        {
            DateTime yearend = Regnskabsaar_Slutdato;
            double DaysThisYear = (yearend - Bilag_Startdato).TotalDays;
            if (DaysThisYear <= 0)
                DaysThisYear = 0;
            var membership_days = (int)(Bilag_Slutdato - Bilag_Startdato).Days;

            decimal BelobThisYear = (decimal)((int)((decimal)(DaysThisYear / membership_days) * Belob));
            decimal BelobNextYear = Belob - BelobThisYear;
            if (BelobNextYear < 2)
            {
                BelobThisYear = Belob;
                BelobNextYear = 0;
            }
            if (BelobThisYear < 2)
            {
                BelobThisYear = 0;
                BelobNextYear = Belob;
            }
            decimal[] arrBelob = { BelobThisYear, BelobNextYear };
            return arrBelob;
        }
    }
}
