using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MailKit;
using MimeKit;
using MailKit.Net.Imap;
using MailKit.Search;

namespace Pbs3060
{
    public enum datefmtType
    {
        fdmmddyyyy = 0,
        fdmmddyyyyhhmmss = 1,
    }

    public enum pbsType
    {
        betalingsservice = 0,
        indbetalingskort = 1,
    }

    public class clsPbs601
    {
        public clsPbs601() { }

        public Tuple<int, int> advis_auto_lobnr(dbData3060DataContext p_dbData3060, int ref_lobnr)
        {
            int lobnr = 0;
            string wadvistekst = "";
            int winfotekst;
            int wantaladvis = 0;
            string wDelsystem = "EML";

            var rstmedlems = from f in p_dbData3060.Tblfak
                             where f.Tilpbsid == ref_lobnr && f.Betalingsdato > DateTime.Today
                             join a in p_dbData3060.Tbladvis on f.Faknr equals a.Faknr into jadvis
                             from a in jadvis.DefaultIfEmpty()
                             where a.Id == null
                             join b in p_dbData3060.Tblbetlin on f.Faknr equals b.Faknr into jbetlins
                             from b in jbetlins.DefaultIfEmpty()
                             where b.Id == null
                             join r in p_dbData3060.Tblrykker on f.Faknr equals r.Faknr into jrykkers
                             from r in jrykkers.DefaultIfEmpty()
                             where r.Id == null
                             select new
                             {
                                 f.Nr,
                                 f.Betalingsdato,
                                 f.Advisbelob,
                                 f.Faknr,
                                 f.Indmeldelse
                             };

            int adviscount = rstmedlems.Count();
            if (adviscount > 0)
            {
                Tbltilpbs rec_tilpbs = new Tbltilpbs
                {
                    Delsystem = wDelsystem,
                    Leverancetype = "0601",
                    Udtrukket = DateTime.Now
                };
                p_dbData3060.Tbltilpbs.Add(rec_tilpbs);
                p_dbData3060.SaveChanges();
                lobnr = rec_tilpbs.Id;

                foreach (var rstmedlem in rstmedlems)
                {
                    winfotekst = (rstmedlem.Indmeldelse) ? 52 : 50;

                    Tbladvis rec_advis = new Tbladvis
                    {
                        Betalingsdato = rstmedlem.Betalingsdato,
                        Nr = rstmedlem.Nr,
                        Faknr = rstmedlem.Faknr,
                        Advistekst = wadvistekst,
                        Advisbelob = rstmedlem.Advisbelob,
                        Infotekst = winfotekst,
                        Maildato = DateTime.Today,
                    };
                    rec_tilpbs.Tbladvis.Add(rec_advis);
                    wantaladvis++;
                    if (wantaladvis >= 30) break; //max 30 advis på gang
                }
                p_dbData3060.SaveChanges();
            }
            return new Tuple<int, int>(wantaladvis, lobnr);
        }

        public Tuple<int, int> advis_auto(dbData3060DataContext p_dbData3060)
        {
            int lobnr = 0;
            string wadvistekst = "";
            int winfotekst;
            int wantaladvis = 0;
            string wDelsystem = "EML";

            var rstmedlems = from i in p_dbData3060.vAdvis_indbetalingskort
                             where i.dato > DateTime.Today
                             join f in p_dbData3060.Tblfak on i.faknr equals f.Faknr
                             select new
                             {
                                 i.Nr,
                                 betalingsdato = i.dato,
                                 advisbelob = i.belob,
                                 i.faknr,
                                 f.Indmeldelse
                             };

            int adviscount = rstmedlems.Count();
            if (adviscount > 0)
            {
                Tbltilpbs rec_tilpbs = new Tbltilpbs
                {
                    Delsystem = wDelsystem,
                    Leverancetype = "0601",
                    Udtrukket = DateTime.Now
                };
                p_dbData3060.Tbltilpbs.Add(rec_tilpbs);
                p_dbData3060.SaveChanges();
                lobnr = rec_tilpbs.Id;

                foreach (var rstmedlem in rstmedlems)
                {
                    winfotekst = (rstmedlem.Indmeldelse) ? 51 : 50;

                    Tbladvis rec_advis = new Tbladvis
                    {
                        Betalingsdato = rstmedlem.betalingsdato,
                        Nr = rstmedlem.Nr,
                        Faknr = rstmedlem.faknr,
                        Advistekst = wadvistekst,
                        Advisbelob = rstmedlem.advisbelob,
                        Infotekst = winfotekst,
                        Maildato = DateTime.Today,
                    };
                    rec_tilpbs.Tbladvis.Add(rec_advis);
                    wantaladvis++;
                    if (wantaladvis >= 30) break; //max 30 advis på gang
                }
                p_dbData3060.SaveChanges();
            }
            return new Tuple<int, int>(wantaladvis, lobnr);
        }

        public void advis_email_lobnr(dbData3060DataContext p_dbData3060, int lobnr)
        {
            int wleveranceid;
            int? wSaveFaknr;

            {
                var antal = (from c in p_dbData3060.Tbltilpbs
                             where c.Id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in p_dbData3060.Tbltilpbs
                             where c.Id == lobnr && c.Pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            {
                var antal = (from c in p_dbData3060.Tbladvis
                             where c.Tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in p_dbData3060.Tbltilpbs
                          where c.Id == lobnr
                          select c).First();
            if (rsttil.Udtrukket == null) { rsttil.Udtrukket = DateTime.Now; }
            if (rsttil.Bilagdato == null) { rsttil.Bilagdato = rsttil.Udtrukket; }
            if (rsttil.Delsystem == null) { rsttil.Delsystem = "EML"; }
            if (rsttil.Leverancetype == null) { rsttil.Leverancetype = ""; }
            p_dbData3060.SaveChanges();

            wleveranceid = p_dbData3060.nextval_v2("leveranceid");
            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
            {
                Delsystem = rsttil.Delsystem,
                Leverancetype = rsttil.Leverancetype,
                Oprettetaf = "Adv",
                Oprettet = DateTime.Now,
                Leveranceid = wleveranceid
            };
            p_dbData3060.Tblpbsforsendelse.Add(rec_pbsforsendelse);
            rec_pbsforsendelse.Tbltilpbs.Add(rsttil);

            IEnumerable<clsRstdeb> rstdebs;

            rstdebs = from k in p_dbData3060.TblrsmembershipTransactions
                      join f in p_dbData3060.Tblfak on k.Id equals f.Id
                      join r in p_dbData3060.Tbladvis on f.Faknr equals r.Faknr
                      where r.Tilpbsid == lobnr && r.Nr != null
                      orderby r.Faknr
                      select new clsRstdeb
                      {
                          Nr = f.Nr,
                          Kundenr = 32001610000000 + f.Nr,
                          Kaldenavn = k.Name,
                          Navn = k.Name,
                          Adresse = k.Adresse,
                          Postnr = k.Postnr,
                          Faknr = r.Faknr,
                          Betalingsdato = f.Betalingsdato,
                          Fradato = f.Fradato,
                          Tildato = f.Tildato,
                          Infotekst = r.Infotekst,
                          Tilpbsid = r.Tilpbsid,
                          Advistekst = r.Advistekst,
                          Belob = r.Advisbelob,
                          Email = k.UserEmail,
                          indbetalerident = f.Indbetalerident,
                          indmeldelse = f.Indmeldelse,
                      };


            wSaveFaknr = 0;
            foreach (var rstdeb in rstdebs)
            {
                string OcrString = null;
                string windbetalerident = rstdeb.indbetalerident;
                if (rstdeb.Faknr != wSaveFaknr) //Løser problem med mere flere PBS Tblindbetalingskort records pr Faknr
                {
                    OcrString = p_dbData3060.OcrString((int)rstdeb.Faknr);
                    if (string.IsNullOrEmpty(OcrString))
                    {
                        if (Mod10Check(windbetalerident))
                        {
                            OcrString = string.Format(@"+71< {0}+81131945<", windbetalerident);
                        }
                    }

                    clsInfotekst objInfotekst = new clsInfotekst
                    {
                        infotekst_id = rstdeb.Infotekst,
                        numofcol = null,
                        navn_medlem = rstdeb.Navn,
                        kaldenavn = rstdeb.Kaldenavn,
                        fradato = rstdeb.Fradato,
                        tildato = rstdeb.Tildato,
                        betalingsdato = rstdeb.Betalingsdato,
                        advisbelob = rstdeb.Belob,
                        ocrstring = OcrString,
                        underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører",
                        sendtsom = p_dbData3060.SendtSomString((int)rstdeb.Faknr),
                        kundenr = rstdeb.Kundenr.ToString()
                    };
                    string subject = "Betaling af Puls 3060 Kontingent";
                    Boolean bBcc = false;
                    if (rstdeb.indmeldelse != null)
                    {
                        if ((Boolean)(rstdeb.indmeldelse) == true) bBcc = true;
                    }
                    //Send email
                    sendHtmlEmail(p_dbData3060, rstdeb.Navn, rstdeb.Email, subject, objInfotekst, bBcc);
                }
                wSaveFaknr = rstdeb.Faknr;
            } // -- End rstdebs

            rsttil.Udtrukket = DateTime.Now;
            rsttil.Leverancespecifikation = wleveranceid.ToString();
            p_dbData3060.SaveChanges();
        }

        public Tuple<int, int> rykker_auto(dbData3060DataContext p_dbData3060, puls3060_nyEntities p_dbPuls3060_dk)
        {
            int lobnr = 0;
            string wadvistekst = "";
            int winfotekst = 0;
            int wantalrykkere = 0;
            int wantal1 = 0;
            string wDelsystem = "EML";
            MemRyk memRyk = new MemRyk();
            DateTime now = DateTime.UtcNow;
            DateTime now_plus60 = DateTime.UtcNow.AddDays(60);

            var rstmedlems = from h in p_dbData3060.TblrsmembershipTransactions
                             join f in p_dbData3060.Tblfak on h.Id equals f.Id
                             where f.Sfaknr == null &&
                                   f.Rykkerstop == false &&
                                   f.Betalingsdato.Value.AddDays(90) > now && //må ikke være ælder end 90 dage <<<============mha 2014-01-11=====
                                   f.Betalingsdato.Value.AddDays(7) <= now && //skal have været forfalden i 7 dage
                                   (int)(from q in p_dbData3060.Tblrykker where q.Faknr == f.Faknr select q).Count() == 0
                             orderby f.Fradato, f.Id
                             select new
                             {
                                 Nr = h.Memberid,
                                 f.Betalingsdato,
                                 f.Advisbelob,
                                 f.Faknr,
                                 f.Indmeldelse,
                                 h.UserId,
                                 h.TransId
                             };
            wantal1 = rstmedlems.Count();
            if (wantal1 > 0)
            {
                foreach (var m in rstmedlems)
                {
                    bool AllreadyPayedOrCancelledOrDeleted = ((from q in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers where q.user_id == m.UserId && q.membership_id == 6 && (q.membership_end > now_plus60 || q.status == 3) select q.id).Count() > 0);
                    if (!AllreadyPayedOrCancelledOrDeleted)
                    {
                        //Test om ecpwt_rsmembership_transactions findes
                        AllreadyPayedOrCancelledOrDeleted = ((from q in p_dbPuls3060_dk.ecpwt_rsmembership_transactions where q.id == m.TransId select q.id).Count() > 0);
                    }
                    if (!AllreadyPayedOrCancelledOrDeleted)
                    {
                        recRyk rec = new recRyk
                        {
                            Nr = m.Nr,
                            betalingsdato = m.Betalingsdato,
                            advisbelob = m.Advisbelob,
                            faknr = m.Faknr,
                            indmeldelse = m.Indmeldelse
                        };
                        memRyk.Add(rec);
                        wantalrykkere++;
                        if (wantalrykkere >= 30) break; //max 30 rykkere på gang
                    }
                }

                if (wantalrykkere > 0)
                {
                    Tbltilpbs rec_tilpbs = new Tbltilpbs
                    {
                        Delsystem = wDelsystem,
                        Leverancetype = "0601",
                        Udtrukket = DateTime.Now
                    };
                    p_dbData3060.Tbltilpbs.Add(rec_tilpbs);
                    p_dbData3060.SaveChanges();
                    lobnr = rec_tilpbs.Id;

                    foreach (var rstmedlem in memRyk)
                    {
                        winfotekst = (rstmedlem.indmeldelse) ? 31 : 30;

                        Tblrykker rec_rykker = new Tblrykker
                        {
                            Betalingsdato = rstmedlem.betalingsdato,
                            Nr = rstmedlem.Nr,
                            Faknr = rstmedlem.faknr,
                            Advistekst = wadvistekst,
                            Advisbelob = rstmedlem.advisbelob,
                            Infotekst = winfotekst,
                            Rykkerdato = DateTime.Today,
                        };
                        rec_tilpbs.Tblrykker.Add(rec_rykker);
                    }
                    p_dbData3060.SaveChanges();
                }

            }
            return new Tuple<int, int>(wantalrykkere, lobnr);
        }

        public List<string[]> RSMembership_KontingentForslag(DateTime p_DatoBetaltKontingentTil, dbData3060DataContext p_dbData3060)
        {
            List<string[]> items = new List<string[]>();
            puls3060_nyEntities jdb = new puls3060_nyEntities();
            DateTime KontingentFradato = DateTime.MinValue;
            DateTime KontingentTildato = DateTime.MinValue;
            bool tilmeldtpbs = false;
            bool indmeldelse = false;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            int ikontingent;
            int? iNr;
            string sNavn;

            var qry_rsmembership = from s in jdb.ecpwt_rsmembership_membership_subscribers
                                   where s.membership_id == 6 && s.status != 2 && s.status != 3
                                   join tf in jdb.ecpwt_rsmembership_transactions on s.from_transaction_id equals tf.id
                                   join tl in jdb.ecpwt_rsmembership_transactions on s.last_transaction_id equals tl.id
                                   join m in jdb.ecpwt_rsmembership_subscribers on s.user_id equals m.user_id
                                   join u in jdb.ecpwt_users on s.user_id equals u.id
                                   select new
                                   {
                                       Nr = m.f14,
                                       Navn = u.name,
                                       Adresse = m.f1,
                                       Postnr = m.f4,
                                       indmeldelsesDato = tf.date,
                                       kontingentBetaltTilDato = s.membership_end,
                                       Kontingent = tl.price,
                                       s.user_id,
                                       tl.user_data,
                                       subscriber_id = s.id
                                   };
            var antal = qry_rsmembership.Count();
            var rsm = qry_rsmembership.ToArray();
            foreach (var m in rsm)
            {
                // opdater Nr og Navn fra m.user_data 
                User_data recud = clsHelper.unpack_UserData(m.user_data);
                if (!string.IsNullOrEmpty(recud.memberid))
                {
                    iNr = int.Parse(recud.memberid);
                }
                else
                {
                    if (!string.IsNullOrEmpty(m.Nr))
                        iNr = int.Parse(m.Nr);
                    else
                        iNr = null;
                }
                sNavn = recud.name;

                bool bSelected = true;
                AntalMedlemmer++;
                tilmeldtpbs = false;
                indmeldelse = false;

                bool erMedlemPusterummet = ((from um in jdb.ecpwt_user_usergroup_map
                                             join g in jdb.ecpwt_usergroups on um.group_id equals g.id
                                             where g.title == "Pusterummet" && um.user_id == m.user_id
                                             select um.user_id).Count() > 0);


                if ((m.kontingentBetaltTilDato != null) && (m.kontingentBetaltTilDato > m.indmeldelsesDato))  //Der findes en kontingent-betaling
                {
                    if (m.kontingentBetaltTilDato > p_DatoBetaltKontingentTil)   //der er betalt kontingent efter DatoBetaltKontingentTil
                    {
                        bSelected = false;
                    }
                    else
                    {
                        if (m.kontingentBetaltTilDato >= m.indmeldelsesDato)
                        {
                            KontingentFradato = ((DateTime)m.kontingentBetaltTilDato);
                        }
                    }
                }
                else  //Der findes ingen kontingent-betaling
                {
                    KontingentFradato = (DateTime)m.indmeldelsesDato;
                    indmeldelse = true;
                }

                if (bSelected)
                {
                    DateTime TodayMinus90 = DateTime.Now.AddDays(-90);
                    var qry_fak = from f in p_dbData3060.Tblfak
                                  join t in p_dbData3060.TblrsmembershipTransactions on f.Id equals t.Id
                                  where t.SubscriberId == m.subscriber_id && f.Betalingsdato > TodayMinus90
                                  select f;

                    if (qry_fak.Count() > 0) //Der findes en opkrævning
                    {
                        bSelected = false;
                    }
                }


                if (bSelected)
                {
                    // beregn kontingent baseret på KontingentFradato !!!!!!!!!!!!!!!!!!!!!!!!
                    AntalForslag++;
                    tilmeldtpbs = (bool)p_dbData3060.erPBS((int)iNr);
                    clsKontingent objKontingent = new clsKontingent(p_dbData3060, KontingentFradato);
                    KontingentTildato = objKontingent.KontingentTildato;
                    ikontingent = (int)objKontingent.Kontingent;

                    string[] item = new string[11];
                    item[0] = m.user_id.ToString();
                    item[1] = sNavn;
                    item[2] = m.Adresse;
                    item[3] = m.Postnr;
                    item[4] = string.Format("{0:dd-MM-yyy}", KontingentFradato);
                    item[5] = ikontingent.ToString();
                    item[6] = string.Format("{0:dd-MM-yyy}", KontingentTildato);
                    item[7] = (indmeldelse) ? "J" : "N";
                    item[8] = (tilmeldtpbs) ? "J" : "N";
                    item[9] = m.subscriber_id.ToString(); ;
                    item[10] = (iNr != null) ? iNr.ToString() : "";
                    items.Add(item);
                }
            }
            return items;
        }

        public Tuple<int, int> paypal_pending_rsmembeshhip_fakturer_auto(dbData3060DataContext p_dbData3060, puls3060_nyEntities p_dbPuls3060_dk)
        {
            int lobnr = 0;
            string wadvistekst = "";
            int winfotekst = 0;
            int wantalfakturaer = 0;
            string wDelsystem;
            wDelsystem = "BSH";
            DateTime now_minus60 = DateTime.UtcNow.AddMinutes(-60);
            DateTime KontingentFradato = DateTime.MinValue;
            int AntalForslag = 0;
            MemRSMembershipTransactions memRSMembershipTransactions = new MemRSMembershipTransactions();

            var qrytrans = from t in p_dbPuls3060_dk.ecpwt_rsmembership_transactions
                           where (t.status == "pending" && t.type == "new" && t.@params == "membership_id=6" && t.gateway.ToUpper() == "PBS")
                              //|| (t.status == "pending" && t.type == "new" && t.@params == "membership_id=6" && t.gateway.ToUpper() == "PAYPAL" && t.date < now_minus60)
                              || (t.status == "pending" && t.type == "new" && t.@params == "membership_id=6" && t.gateway.ToUpper() == "INDBETALINGSKORT")
                           select t;
            List<ecpwt_rsmembership_transactions> trans = qrytrans.ToList();

            int antal = trans.Count();
            foreach (var tr in trans)
            {
                bool AllreadyPayed = ((from q in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers where q.user_id == tr.user_id && q.membership_id == 6 && q.membership_end > now_minus60 select q.id).Count() > 0);
                if (AllreadyPayed)
                    continue;

                bool newTrans = ((from q in p_dbData3060.TblrsmembershipTransactions where q.TransId == tr.id select q.Id).Count() == 0);
                if (!newTrans)
                    continue;

                User_data recud = null;
                try
                {
                    recud = clsHelper.unpack_UserData(tr.user_data);

                }
                catch
                {
                    continue;
                }
                if (recud.postnr.Length != 4)
                {
                    continue;
                }

                recRSMembershipTransactions rec = new recRSMembershipTransactions
                {
                    id = tr.id,
                    user_id = tr.user_id,
                    user_email = tr.user_email,
                    user_data = tr.user_data,
                    type = tr.type,
                    @params = tr.@params,
                    date = tr.date,
                    ip = tr.ip,
                    price = tr.price,
                    coupon = tr.coupon,
                    currency = tr.currency,
                    hash = tr.hash,
                    custom = tr.custom,
                    gateway = tr.gateway,
                    status = tr.status,
                    response_log = tr.response_log,
                    indmeldelse = true,
                    tilmeldtpbs = false,
                    name = recud.name,
                    username = recud.username,
                    adresse = recud.adresse,
                    postnr = recud.postnr,
                    bynavn = recud.bynavn,
                    mobil = recud.mobil,
                    memberid = (!string.IsNullOrEmpty(recud.memberid)) ? int.Parse(recud.memberid) : p_dbData3060.nextval_v2("memberid"),
                    kon = recud.kon,
                    fodtaar = recud.fodtaar,
                    message = recud.message,
                    password = recud.password,
                    fradato = null,
                    tildato = null
                };

                int? parm_membership_id = clsHelper.getParam(tr.@params, "membership_id");
                rec.membership_id = (parm_membership_id != null) ? (int)parm_membership_id : 0;
                rec.subscriber_id = clsHelper.getParam(tr.@params, "id");
                memRSMembershipTransactions.Add(rec);
                AntalForslag++;
            }


            if (AntalForslag > 0)
            {
                Tbltilpbs rec_tilpbs = new Tbltilpbs
                {
                    Delsystem = wDelsystem,
                    Leverancetype = "0601",
                    Udtrukket = DateTime.Now
                };
                p_dbData3060.Tbltilpbs.Add(rec_tilpbs);
                p_dbData3060.SaveChanges();
                lobnr = rec_tilpbs.Id;

                foreach (var rec_trans in memRSMembershipTransactions)
                {
                    if (rec_trans.indmeldelse) winfotekst = 11;
                    else winfotekst = (rec_trans.tilmeldtpbs) ? 10 : 12;
                    int next_faknr = p_dbData3060.nextval_v2("faknr") ;
                    string windbetalerident = clsHelper.generateIndbetalerident(next_faknr); //?????????????????????????????????
                    Tblfak rec_fak = new Tblfak
                    {
                        Betalingsdato = clsHelper.bankdageplus(DateTime.Today, 8),
                        Nr = rec_trans.memberid,
                        Faknr = next_faknr,
                        Advistekst = wadvistekst,
                        Advisbelob = rec_trans.price,
                        Infotekst = winfotekst,
                        Bogfkonto = 1800,
                        Vnr = 1,
                        Fradato = rec_trans.date,
                        Tildato = rec_trans.date.AddYears(1),
                        Indmeldelse = rec_trans.indmeldelse,
                        Tilmeldtpbs = rec_trans.tilmeldtpbs,
                        Indbetalerident = windbetalerident, // ToDo generer indbetalerident
                        Tblrsmembership_transactions = new TblrsmembershipTransactions()
                        {
                            TransId = rec_trans.id,
                            UserId = rec_trans.user_id,
                            UserEmail = rec_trans.user_email,
                            UserData = rec_trans.user_data,
                            Type = rec_trans.type,
                            Params = rec_trans.@params,
                            Ip = rec_trans.ip,
                            Date = rec_trans.date,
                            Price = rec_trans.price,
                            Coupon = rec_trans.coupon,
                            Currency = rec_trans.currency,
                            Hash = rec_trans.hash,
                            Custom = rec_trans.custom,
                            Gateway = rec_trans.gateway,
                            Status = rec_trans.status,
                            ResponseLog = rec_trans.response_log,
                            Name = rec_trans.name,
                            Adresse = rec_trans.adresse,
                            Postnr = rec_trans.postnr,
                            Bynavn = rec_trans.bynavn,
                            Memberid = rec_trans.memberid,
                            MembershipId = rec_trans.membership_id,
                            SubscriberId = rec_trans.subscriber_id
                        }
                    };
                    rec_tilpbs.Tblfak.Add(rec_fak);
                    clsHelper.Update_memberid_in_rsmembership_transaction(rec_trans);
                    p_dbData3060.SaveChanges();
                    wantalfakturaer++;
                }
            }

            return new Tuple<int, int>(wantalfakturaer, lobnr);

        }

        public Tuple<int, int> rsmembeshhip_kontingent_fakturer_bs1(dbData3060DataContext p_dbData3060, puls3060_nyEntities p_dbPuls3060_dk, Memkontingentforslag memKontingentforslag, pbsType forsType)
        {
            int lobnr;
            string wadvistekst = "";
            int winfotekst;
            int wantalfakturaer;
            wantalfakturaer = 0;

            bool? wbsh = (from h in memKontingentforslag select h.bsh).First();
            bool bsh = (wbsh == null) ? false : (bool)wbsh;
            string wDelsystem;
            if (bsh) wDelsystem = "BSH";
            else wDelsystem = "BS1";

            Tbltilpbs rec_tilpbs = new Tbltilpbs   //<------------------------------------
            {
                Delsystem = wDelsystem,
                Leverancetype = "0601",
                Udtrukket = DateTime.Now
            };
            p_dbData3060.Tbltilpbs.Add(rec_tilpbs);
            p_dbData3060.SaveChanges();
            lobnr = rec_tilpbs.Id;

            IEnumerable<recKontingentforslag> qry;
            if (wDelsystem == "BS1")
            {
                if (forsType == pbsType.betalingsservice)
                {
                    qry = from k in memKontingentforslag where k.tilmeldtpbs select k;
                }
                else
                {
                    qry = from k in memKontingentforslag where (!k.tilmeldtpbs) select k;
                }
            }
            else if (wDelsystem == "BSH")
            {
                if (forsType == pbsType.betalingsservice)
                {
                    qry = from k in memKontingentforslag where k.user_id < 0 select k; // ingen skal vælges her
                }
                else
                {
                    qry = from k in memKontingentforslag select k; // Alle skal vælges her
                }
            }
            else
            {
                qry = from k in memKontingentforslag where k.user_id < 0 select k; // ingen skal vælges her
            }


            foreach (recKontingentforslag rec in qry)
            {
                string wmemberid = rec.memberid.ToString();
                var qry_rsmembership = from s in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers
                                       where s.id == rec.subscriber_id && s.membership_id == rec.membership_id
                                       join tf in p_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.from_transaction_id equals tf.id
                                       join tl in p_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.last_transaction_id equals tl.id
                                       join m in p_dbPuls3060_dk.ecpwt_rsmembership_subscribers on s.user_id equals m.user_id
                                       join u in p_dbPuls3060_dk.ecpwt_users on s.user_id equals u.id
                                       select new
                                       {
                                           Nr = wmemberid, //m.f14,
                                           Navn = rec.name, //u.name,
                                           Adresse = m.f1,
                                           Postnr = m.f4,
                                           Bynavn = m.f2,
                                           Mobil = m.f6,
                                           indmeldelsesDato = tf.date,
                                           kontingentBetaltTilDato = s.membership_end,
                                           Kontingent = tl.price,
                                           user_id = s.user_id,
                                           rsmembership_membership_subscribers_id = s.id,
                                           last_transaction_id = s.last_transaction_id,
                                           user_email = u.email,
                                           user_data = tl.user_data,
                                           currency = tl.currency,
                                           memberid = rec.memberid
                                       };
                int antal = qry_rsmembership.Count();
                if (antal != 1) continue;
                var rsm = qry_rsmembership.First();

                string hashkey = clsHelper.GenerateStringHash(rsm.user_id.ToString() + rsm.Navn + DateTime.Now.ToString());
                User_data mydata = clsHelper.unpack_UserData(rsm.user_data);
                mydata.password = null;
                mydata.adresse = rsm.Adresse;
                mydata.postnr = rsm.Postnr;
                mydata.bynavn = rsm.Bynavn;
                mydata.email = rsm.user_email;
                mydata.mobil = rsm.Mobil;

                //if ((mydata.memberid == null) || (mydata.memberid == "")) mydata.memberid = (10000 + rsm.user_id).ToString();
                if ((mydata.memberid == null) || (mydata.memberid == "")) mydata.memberid = rsm.memberid.ToString();
                string new_user_data = clsHelper.pack_UserData(mydata);
                string newparm = string.Format(@"id={0};membership_id={1}", rsm.rsmembership_membership_subscribers_id, rec.membership_id);
                ecpwt_rsmembership_transactions new_trans = new ecpwt_rsmembership_transactions
                {
                    user_id = rsm.user_id,
                    user_email = rsm.user_email,
                    user_data = new_user_data,
                    type = "renew",
                    @params = newparm,
                    date = DateTime.UtcNow,
                    ip = "::1",
                    price = rec.advisbelob,
                    coupon = "",
                    currency = rsm.currency,
                    hash = "",
                    custom = hashkey,
                    gateway = "PBS",
                    status = "pending",
                    response_log = "",
                };
                p_dbPuls3060_dk.ecpwt_rsmembership_transactions.Add(new_trans);
                p_dbPuls3060_dk.SaveChanges();

                ecpwt_rsmembership_transactions rec_trans = (from t in p_dbPuls3060_dk.ecpwt_rsmembership_transactions where t.custom == hashkey select t).First();

                //foreach (var rstmedlem in rsm)
                {
                    if (rec.indmeldelse) winfotekst = 11;
                    else winfotekst = (rec.tilmeldtpbs) ? 10 : 12;
                    int next_faknr = p_dbData3060.nextval_v2("faknr");
                    string windbetalerident = clsHelper.generateIndbetalerident(next_faknr); //?????????????????????????????????
                    Tblfak rec_fak = new Tblfak
                    {
                        Betalingsdato = rec.betalingsdato,
                        Nr = int.Parse(rsm.Nr),
                        Faknr = next_faknr,
                        Advistekst = wadvistekst,
                        Advisbelob = rec.advisbelob,
                        Infotekst = winfotekst,
                        Bogfkonto = 1800,
                        Vnr = 1,
                        Fradato = rec.fradato,
                        Tildato = rec.tildato,
                        Indmeldelse = rec.indmeldelse,
                        Tilmeldtpbs = rec.tilmeldtpbs,
                        Indbetalerident = windbetalerident, // ToDo generer indbetalerident
                        Tblrsmembership_transactions = new TblrsmembershipTransactions()
                        {
                            TransId = rec_trans.id,
                            UserId = rec_trans.user_id,
                            UserEmail = rec_trans.user_email,
                            UserData = rec_trans.user_data,
                            Type = rec_trans.type,
                            Params = rec_trans.@params,
                            Ip = rec_trans.ip,
                            Date = rec_trans.date,
                            Price = rec_trans.price,
                            Coupon = rec_trans.coupon,
                            Currency = rec_trans.currency,
                            Hash = rec_trans.hash,
                            Custom = rec_trans.custom,
                            Gateway = rec_trans.gateway,
                            Status = rec_trans.status,
                            ResponseLog = rec_trans.response_log,
                            Name = mydata.name,
                            Adresse = mydata.adresse,
                            Postnr = mydata.postnr,
                            Bynavn = mydata.bynavn,
                            Memberid = int.Parse(rsm.Nr),
                            MembershipId = rec.membership_id,
                            SubscriberId = rsm.rsmembership_membership_subscribers_id
                        }
                    };
                    rec_tilpbs.Tblfak.Add(rec_fak);
                    wantalfakturaer++;
                }
            }
            p_dbData3060.SaveChanges();
            return new Tuple<int, int>(wantalfakturaer, lobnr);

        }

        public void rykker_email_lobnr(dbData3060DataContext p_dbData3060, int lobnr)
        {
            int wleveranceid;
            int? wSaveFaknr;

            {
                var antal = (from c in p_dbData3060.Tbltilpbs
                             where c.Id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in p_dbData3060.Tbltilpbs
                             where c.Id == lobnr && c.Pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            {
                var antal = (from c in p_dbData3060.Tblrykker
                             where c.Tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in p_dbData3060.Tbltilpbs
                          where c.Id == lobnr
                          select c).First();
            if (rsttil.Udtrukket == null) { rsttil.Udtrukket = DateTime.Now; }
            if (rsttil.Bilagdato == null) { rsttil.Bilagdato = rsttil.Udtrukket; }
            if (rsttil.Delsystem == null) { rsttil.Delsystem = "EML"; }
            if (rsttil.Leverancetype == null) { rsttil.Leverancetype = ""; }
            p_dbData3060.SaveChanges();

            wleveranceid = p_dbData3060.nextval_v2("leveranceid");
            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
            {
                Delsystem = rsttil.Delsystem,
                Leverancetype = rsttil.Leverancetype,
                Oprettetaf = "Ryk",
                Oprettet = DateTime.Now,
                Leveranceid = wleveranceid
            };
            p_dbData3060.Tblpbsforsendelse.Add(rec_pbsforsendelse);
            rec_pbsforsendelse.Tbltilpbs.Add(rsttil);

            var rstdebs = from r in p_dbData3060.Tblrykker
                          where r.Tilpbsid == lobnr && r.Nr != null
                          join f in p_dbData3060.Tblfak on r.Faknr equals f.Faknr
                          join k in p_dbData3060.TblrsmembershipTransactions on f.Id equals k.Id
                          orderby r.Faknr
                          select new clsRstdeb
                          {
                              Nr = k.Memberid,
                              Kundenr = 32001610000000 + k.Memberid,
                              Kaldenavn = k.Name,
                              Navn = k.Name,
                              Adresse = k.Adresse,
                              Postnr = k.Postnr,
                              Faknr = r.Faknr,
                              Betalingsdato = f.Betalingsdato,
                              Fradato = f.Fradato,
                              Tildato = f.Tildato,
                              Infotekst = r.Infotekst,
                              Tilpbsid = r.Tilpbsid,
                              Advistekst = r.Advistekst,
                              Belob = r.Advisbelob,
                              Email = k.UserEmail,
                              indbetalerident = f.Indbetalerident,
                              indmeldelse = f.Indmeldelse,
                          };

            wSaveFaknr = 0;
            foreach (var rstdeb in rstdebs)
            {
                if (rstdeb.Faknr != wSaveFaknr) //Løser problem med mere flere PBS Tblindbetalingskort records pr Faknr
                {
                    clsInfotekst objInfotekst = new clsInfotekst
                    {
                        infotekst_id = rstdeb.Infotekst,
                        numofcol = null,
                        navn_medlem = rstdeb.Navn,
                        kaldenavn = rstdeb.Kaldenavn,
                        fradato = rstdeb.Fradato,
                        tildato = rstdeb.Tildato,
                        betalingsdato = rstdeb.Betalingsdato,
                        advisbelob = rstdeb.Belob,
                        ocrstring = p_dbData3060.OcrString((int)rstdeb.Faknr),
                        underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører",
                        sendtsom = p_dbData3060.SendtSomString((int)rstdeb.Faknr),
                        kundenr = rstdeb.Kundenr.ToString()
                    };
                    string subject = "Rykker for Betaling af Puls 3060 Kontingent";
                    Boolean bBcc = true;
                    /*
                    if (rstdeb.indmeldelse != null)
                    {
                        if ((Boolean)(rstdeb.indmeldelse) == true) bBcc = true;
                    }
                    */
                    //Send email
                    sendHtmlEmail(p_dbData3060, rstdeb.Navn, rstdeb.Email, subject, objInfotekst, bBcc);
                }
                wSaveFaknr = rstdeb.Faknr;
            } // -- End rstdebs

            rsttil.Udtrukket = DateTime.Now;
            rsttil.Leverancespecifikation = wleveranceid.ToString();
            p_dbData3060.SaveChanges();
        }

        public void faktura_og_rykker_601_action_lobnr(dbData3060DataContext p_dbData3060, int lobnr)
        {
            string rec;
            //lintype lin;
            //infolintype infolin;
            int recnr;
            int fortegn;
            int wleveranceid;
            int seq;
            // -- Betalingsoplysninger
            string h_linie;
            // -- Tekst til hovedlinie på advis
            int belobint;
            string advistekst;
            int advisbelob;
            // -- Tællere
            int antal042;
            // -- Antal 042: Antal foranstående 042 records
            int belob042;
            // -- Beløb: Nettobeløb i 042 records
            int antal052;
            // -- Antal 052: Antal foranstående 052 records
            int antal022;
            // -- Antal 022: Antal foranstående 022 records
            int antalsek;
            // -- Antal sektioner i leverancen
            int antal042tot;
            // -- Antal 042: Antal foranstående 042 records
            int belob042tot;
            // -- Beløb: Nettobeløb i 042 records
            int antal052tot;
            // -- Antal 052: Antal foranstående 052 records
            int antal022tot;
            // -- Antal 022: Antal foranstående 022 records
            // TODO: On Error GoTo Warning!!!: The statement is not translatable 
            // --lobnr = 275 '--debug
            h_linie = "LØBEKLUBBEN PULS 3060";
            seq = 0;
            antal042 = 0;
            belob042 = 0;
            antal052 = 0;
            antal022 = 0;
            antalsek = 0;
            antal042tot = 0;
            belob042tot = 0;
            antal052tot = 0;
            antal022tot = 0;

            {
                var antal = (from c in p_dbData3060.Tbltilpbs
                             where c.Id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in p_dbData3060.Tbltilpbs
                             where c.Id == lobnr && c.Pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }

            {
                var antal = (from c in p_dbData3060.Tblfak
                             where c.Tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }
            var rsttil = (from c in p_dbData3060.Tbltilpbs
                          where c.Id == lobnr
                          select c).First();
            if (rsttil.Udtrukket == null) { rsttil.Udtrukket = DateTime.Now; }
            if (rsttil.Bilagdato == null) { rsttil.Bilagdato = rsttil.Udtrukket; }
            if (rsttil.Delsystem == null) { rsttil.Delsystem = "BS1"; }  // ????????????????
            if (rsttil.Leverancetype == null) { rsttil.Leverancetype = ""; }
            p_dbData3060.SaveChanges();

            wleveranceid = p_dbData3060.nextval_v2("leveranceid");
            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
            {
                Delsystem = rsttil.Delsystem,
                Leverancetype = rsttil.Leverancetype,
                Oprettetaf = "Fak",
                Oprettet = DateTime.Now,
                Leveranceid = wleveranceid
            };
            p_dbData3060.Tblpbsforsendelse.Add(rec_pbsforsendelse);
            rec_pbsforsendelse.Tbltilpbs.Add(rsttil);

            Tblpbsfilename rec_pbsfiles = new Tblpbsfilename();
            rec_pbsforsendelse.Tblpbsfilename.Add(rec_pbsfiles);

            var rstkrd = (from c in p_dbData3060.tblkreditor
                          where c.delsystem == rsttil.Delsystem
                          select c).First();


            // -- Leverance Start - 0601 Betalingsoplysninger
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - rsttil.Delsystem - Delsystem:  Dataleverandør delsystem
            // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
            // - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
            // - rsttil!udtrukket - Dato: 000000 eller leverancens dannelsesdato
            rec = write002(rstkrd.datalevnr, rsttil.Delsystem, "0601", wleveranceid.ToString(), (DateTime)rsttil.Udtrukket);
            Tblpbsfile rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            // -- Sektion start - sektion 0112/0117
            // -  rstkrd.Pbsnr       - PBS-nr.: Kreditors PBS-nummer
            // -  rstkrd.Sektionnr   - Sektionsnr.: 0112/0117 (Betalinger med lang advistekst)
            // -  rstkrd.Debgrpnr    - Debitorgruppenr.: Debitorgruppenummer
            // -  rstkrd.Datalevnavn - Leveranceidentifikation: Brugers identifikation hos dataleverandør
            // -  rsttil.Udtrukket   - Dato: 000000 eller leverancens dannelsesdato
            // -  rstkrd.Regnr       - Reg.nr.: Overførselsregistreringsnummer
            // -  rstkrd.Kontonr     - Kontonr.: Overførselskontonummer
            // -  h_linie            - H-linie: Tekst til hovedlinie på advis
            rec = write012(rstkrd.pbsnr, rstkrd.sektionnr, rstkrd.debgrpnr, rstkrd.datalevnavn, (DateTime)rsttil.Udtrukket, rstkrd.regnr, rstkrd.kontonr, h_linie);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
            antalsek++;

            IEnumerable<clsRstdeb> rstdebs;

            rstdebs = from k in p_dbData3060.TblrsmembershipTransactions
                      join f in p_dbData3060.Tblfak on k.Id equals f.Id
                      where f.Tilpbsid == lobnr && f.Nr != null
                      orderby f.Nr
                      select new clsRstdeb
                      {
                          Nr = f.Nr,
                          Kundenr = 32001610000000 + f.Nr,
                          Kaldenavn = k.Name,
                          Navn = k.Name,
                          Adresse = k.Adresse,
                          Postnr = k.Postnr,
                          Faknr = f.Faknr,
                          Betalingsdato = f.Betalingsdato,
                          Fradato = f.Fradato,
                          Tildato = f.Tildato,
                          Infotekst = f.Infotekst,
                          Tilpbsid = f.Tilpbsid,
                          Advistekst = f.Advistekst,
                          Belob = f.Advisbelob,
                          indbetalerident = f.Indbetalerident,
                          indmeldelse = f.Indmeldelse,
                      };


            foreach (var rstdeb in rstdebs)
            {
                // -- Debitornavn
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 1                - Recordnr.: 001
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Navn      - Navn: Debitors navn
                rec = write022(rstkrd.sektionnr, rstkrd.pbsnr, "0240", 1, rstkrd.debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Navn);
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // Split adresse i 2 felter hvis længde > 35
                string rstdeb_Adresse1 = null;
                string rstdeb_Adresse2 = null;
                bool bStart_Adresse1 = true;
                bool bStart_Adresse2 = true;
                if (rstdeb.Adresse.Length <= 35)
                {
                    rstdeb_Adresse1 = rstdeb.Adresse;
                }
                else
                {
                    string[] words = rstdeb.Adresse.Split(' ');
                    for (var i = 0; i < words.Length; i++)
                    {
                        if (bStart_Adresse1 == true)
                        {
                            rstdeb_Adresse1 = words[i];
                            bStart_Adresse1 = false;
                        }
                        else
                        {
                            if ((rstdeb_Adresse1.Length + 1 + words[i].Length) <= 35)
                            {
                                rstdeb_Adresse1 += " " + words[i];
                            }
                            else
                            {
                                if (bStart_Adresse2 == true)
                                {
                                    rstdeb_Adresse2 = words[i];
                                    bStart_Adresse2 = false;
                                }
                                else
                                {
                                    rstdeb_Adresse2 += " " + words[i];
                                }
                            }
                        }

                    }
                }

                // -- Debitoradresse 1/adresse 2
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 2                - Recordnr.: 002
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Adresse   - Adresse 1: Adresselinie 1
                rec = write022(rstkrd.sektionnr, rstkrd.pbsnr, "0240", 2, rstkrd.debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb_Adresse1);
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                if (!bStart_Adresse2)
                {
                    // -- Debitoradresse 1/adresse 3
                    // - rstkrd.Sektionnr -
                    // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                    // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                    // - 2                - Recordnr.: 002
                    // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                    // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                    // - 0                - Aftalenr.: 000000000 eller 999999999
                    // - rstdeb.Adresse   - Adresse 1: Adresselinie 1
                    rec = write022(rstkrd.sektionnr, rstkrd.pbsnr, "0240", 3, rstkrd.debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb_Adresse2);
                    antal022++;
                    antal022tot++;
                    rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                    rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
                }

                // -- Debitorpostnummer
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 3                - Recordnr.: 003
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Postnr    - Postnr.: Postnummer
                rec = write022(rstkrd.sektionnr, rstkrd.pbsnr, "0240", 9, rstkrd.debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Postnr.ToString());
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // -- Forfald betaling
                if (rstdeb.Belob > 0)
                {
                    fortegn = 1;
                    // -- Fortegnskode: 1 = træk
                    belobint = ((int)rstdeb.Belob) * 100;
                    belob042 += belobint;
                    belob042tot += belobint;
                }
                else if (rstdeb.Belob < 0)
                {
                    fortegn = 2;
                    // -- Fortegnskode: 2 = indsættelse
                    belobint = ((int)rstdeb.Belob) * (-100);
                    belob042 -= belobint;
                    belob042tot -= belobint;
                }
                else
                {
                    fortegn = 0;  // -- Fortegnskode: 0 = 0-beløb
                    belobint = 0;
                }
                // - rstkrd.Sektionnr         -
                // - rstkrd.Pbsnr             - PBS-nr.: Kreditors PBS-nummer
                // - rstkrd.Transkodebetaling - Transkode: 0280/0285 (Betaling)
                // - rstkrd.Debgrpnr          - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr           - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                        - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Betalingsdato     -
                // - fortegn                  -
                // - belobint                 - Beløb: Beløb i øre uden fortegn
                // - rstdeb.Faknr             - faknr: Information vedrørende betalingen.
                // - rstdeb.indbetalerident   - indbetalerident: Information vedrørende OCR linie.
                rec = write042(rstkrd.sektionnr, rstkrd.pbsnr, rstkrd.transkodebetaling, rstkrd.debgrpnr, rstdeb.Kundenr.ToString(), 0, (DateTime)rstdeb.Betalingsdato, fortegn, belobint, (int)rstdeb.Faknr, rstdeb.indbetalerident);
                antal042++;
                antal042tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                recnr = 0;
                string[] splitchars = { "\r\n" };
                string[] arradvis;
                if (rstdeb.Advistekst.Length > 0)
                {
                    arradvis = rstdeb.Advistekst.Split(splitchars, StringSplitOptions.None);
                    for (int n = arradvis.GetLowerBound(0); n <= arradvis.GetUpperBound(0); n++)
                    {
                        if (n == arradvis.GetLowerBound(0))
                        {
                            recnr++;
                            advistekst = arradvis[n];
                            advisbelob = (int)rstdeb.Belob;
                        }
                        else
                        {
                            recnr++;
                            advistekst = arradvis[n];
                            advisbelob = 0;
                        }

                        // -- Tekst til advis
                        antal052++;
                        antal052tot++;

                        // - rstkrd!sektionnr  -
                        // - rstkrd!pbsnr      - PBS-nr.: Kreditors PBS-nummer
                        // - "0241"            - Transkode: 0241 (Tekstlinie)
                        // - recnr             - Recordnr.: 001-999
                        // - rstkrd!debgrpnr   - Debitorgruppenr.: Debitorgruppenummer
                        // - rstdeb!kundenr    - Kundenr.: Debitors kundenummer hos kreditor
                        // - 0                 - Aftalenr.: 000000000 eller 999999999
                        // - advistekst        - Advistekst 1: Tekstlinie på advis
                        // - 0.0               - Advisbeløb 1: Beløb på advis
                        // - ""                - Advistekst 2: Tekstlinie på advis
                        // - 0.0               - Advisbeløb 2: Beløb på advis
                        rec = write052(rstkrd.sektionnr, rstkrd.pbsnr, "0241", recnr, rstkrd.debgrpnr, rstdeb.Kundenr.ToString(), 0, advistekst, advisbelob, "", 0);
                        rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                        rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
                    }
                }

                string infotekst = new clsInfotekst
                {
                    infotekst_id = rstdeb.Infotekst,
                    numofcol = 60,
                    navn_medlem = rstdeb.Navn,
                    kaldenavn = rstdeb.Kaldenavn,
                    fradato = rstdeb.Fradato,
                    tildato = rstdeb.Tildato,
                    betalingsdato = rstdeb.Betalingsdato,
                    advisbelob = rstdeb.Belob,
                    ocrstring = p_dbData3060.OcrString((int)rstdeb.Faknr),
                    underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører",
                    kundenr = rstdeb.Kundenr.ToString()
                }.getinfotekst(p_dbData3060);

                if (infotekst.Length > 0)
                {
                    arradvis = infotekst.Split(splitchars, StringSplitOptions.None);
                    foreach (string advisline in arradvis)
                    {
                        recnr++;
                        antal052++;
                        antal052tot++;

                        // - rstkrd!sektionnr     -
                        // - rstkrd!pbsnr         - PBS-nr.: Kreditors PBS-nummer
                        // - "0241"               - Transkode: 0241 (Tekstlinie)
                        // - recnr                - Recordnr.: 001-999
                        // - rstkrd!debgrpnr      - Debitorgruppenr.: Debitorgruppenummer
                        // - rstdeb!kundenr       - Kundenr.: Debitors kundenummer hos kreditor
                        // - 0                    - Aftalenr.: 000000000 eller 999999999
                        // - infolin.getinfotekst - Advistekst 1: Tekstlinie på advis
                        // - 0.0                  - Advisbeløb 1: Beløb på advis
                        // - ""                   - Advistekst 2: Tekstlinie på advis
                        // - 0.0                  - Advisbeløb 2: Beløb på advis
                        rec = write052(rstkrd.sektionnr, rstkrd.pbsnr, "0241", recnr, rstkrd.debgrpnr, rstdeb.Kundenr.ToString(), 0, advisline, 0, "", 0);
                        rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                        rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
                    }
                }

            } // -- End rstdebs

            // -- Sektion slut - sektion 0112/117
            // - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
            // - rstkrd!sektionnr - Sektionsnr.: 0112/0117 (Betalinger)
            // - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
            // - antal042         - Antal 042: Antal foranstående 042 records
            // - belob042         - Beløb: Nettobeløb i 042 records
            // - antal052         - Antal 052: Antal foranstående 052 records
            // - antal022         - Antal 022: Antal foranstående 022 records
            rec = write092(rstkrd.pbsnr, rstkrd.sektionnr, rstkrd.debgrpnr, antal042, belob042, antal052, antal022);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            // -- Leverance slut  - 0601 Betalingsoplysninger
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - rstkrd.Delsystem - Delsystem:  Dataleverandør delsystem
            // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
            // - antalsek         - Antal sektioner: Antal sektioner i leverancen
            // - antal042tot      - Antal 042: Antal foranstående 042 records
            // - belob042tot      - Beløb: Nettobeløb i 042 records
            // - antal052tot      - Antal 052: Antal foranstående 052 records
            // - antal022tot      - Antal 022: Antal foranstående 022 records
            rec = write992(rstkrd.datalevnr, rstkrd.delsystem, "0601", antalsek, antal042tot, belob042tot, antal052tot, antal022tot);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            rsttil.Udtrukket = DateTime.Now;
            rsttil.Leverancespecifikation = wleveranceid.ToString();
            p_dbData3060.SaveChanges();
        }

        private string write002(string datalevnr, string delsystem, string levtype, string levident, System.DateTime levdato)
        {
            string rec = null;

            rec = "BS002";
            rec += lpad(datalevnr, 8, '?');
            rec += lpad(delsystem, 3, '?');
            rec += lpad(levtype, 4, '?');
            rec += lpad(levident, 10, '0');
            rec += rpad("", 19, ' ');
            rec += lpad(String.Format("{0:ddMMyy}", levdato), 6, '?');
            return rec;
        }

        private string write012(string pbsnr, string sektionnr, string debgrpnr, string levident, System.DateTime levdato, string regnr, string kontonr, string h_linie)
        {
            string rec = null;

            rec = "BS012";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(sektionnr, 4, '?');
            //-- Sektionsnr.
            if (sektionnr == "0100")
            {
                rec += rpad("", 3, ' ');
                //-- Filler
                rec += lpad(debgrpnr, 5, '0');
                //-- Debitorgruppenr.: Debitorgruppenummer
                rec += rpad(levident, 15, ' ');
                //-- Leveranceidentifikation: Brugers identifikation hos dataleverandør
                rec += rpad("", 9, ' ');
                //-- Filler
                //-- Dato: 000000 eller leverancens dannelsesdato
                rec += lpad(String.Format("{0:ddMMyy}", levdato), 6, '?');
            }
            else if (sektionnr == "0112")
            {
                rec += rpad("", 5, ' ');
                //-- Filler
                rec += lpad(debgrpnr, 5, '0');
                //-- Debitorgruppenr.: Debitorgruppenummer
                rec += rpad(levident, 15, ' ');
                //-- Leveranceidentifikation: Brugers identifikation hos dataleverandør
                rec += rpad("", 4, ' ');
                //-- Filler
                //-- Dato: 00000000 eller leverancens dannelsesdato
                rec += lpad(String.Format("{0:ddMMyyyy}", levdato), 8, '?');
            }
            else if (sektionnr == "0117")
            {
                rec += rpad("", 5, ' ');
                //-- Filler
                rec += lpad(debgrpnr, 5, '0');
                //-- Debitorgruppenr.: Debitorgruppenummer
                rec += rpad(levident, 15, ' ');
                //-- Leveranceidentifikation: Brugers identifikation hos dataleverandør
                rec += rpad("", 4, ' ');
                //-- Filler
                //-- Dato: 00000000 eller leverancens dannelsesdato
                rec += lpad(String.Format("{0:ddMMyyyy}", levdato), 8, '?');
            }
            rec += lpad(regnr, 4, '0');
            //-- Reg.nr.: Overførselsregistreringsnummer
            rec += lpad(kontonr, 10, '0');
            //-- Kontonr.: Overførselskontonummer
            rec += h_linie;
            //-- H-linie: Tekst til hovedlinie på advis
            return rec;
        }

        private string write022(string sektionnr, string pbsnr, string transkode, long recordnr, string debgrpnr, string personid, long aftalenr, string Data)
        {
            string rec = null;

            rec = "BS022";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(transkode, 4, '?');
            //-- Transkode:
            if (sektionnr == "0112")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else if (sektionnr == "0117")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else
            {
                //-- Recordnr.
                rec += lpad(recordnr, 3, '0');
            }
            rec += lpad(debgrpnr, 5, '0');
            //-- Debitorgruppenr.: Debitorgruppenummer
            rec += lpad(personid, 15, '0');
            //-- Kundenr.: Debitors kundenummer hos kreditor
            rec += lpad(aftalenr, 9, '0');
            //-- Aftalenr.
            if (recordnr == 9)
            {
                rec += rpad("", 15, ' ');
                //-- Filler
                rec += rpad(Data, 4, ' ');
                //-- Debitor postnr
                //-- Debitor landekode
                rec += rpad("DK", 3, ' ');
            }
            else
            {
                //-- Debitor navn og adresse
                rec += Data;
            }

            return rec;
        }

        private string write042(string sektionnr, string pbsnr, string transkode, string debgrpnr, string medlemsnr, int aftalenr, System.DateTime betaldato, int fortegn, int belob, int faknr, string indbetalerident)
        {
            string rec = null;

            rec = "BS042";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(transkode, 4, '?');
            //-- Transkode:
            if (sektionnr == "0112")
            {
                //-- Recordnr.
                rec += lpad("", 5, '0');
            }
            else if (sektionnr == "0117")
            {
                //-- Recordnr.
                rec += lpad("", 5, '0');
            }
            else
            {
                //-- Recordnr.
                rec += lpad("", 3, '0');
            }
            rec += lpad(debgrpnr, 5, '0');
            //-- Debitorgruppenr.: Debitorgruppenummer
            rec += lpad(medlemsnr, 15, '0');
            //-- Kundenr.: Debitors kundenummer hos kreditor
            rec += lpad(aftalenr, 9, '0');
            //-- Aftalenr.
            if (sektionnr == "0112")
            {
                rec += lpad(String.Format("{0:ddMMyyyy}", betaldato), 8, '?');
            }
            else if (sektionnr == "0117")
            {
                rec += lpad(String.Format("{0:ddMMyyyy}", betaldato), 8, '?');
            }
            else
            {
                rec += lpad(String.Format("{0:ddMMyyyy}", betaldato), 6, '?');
            }
            rec += lpad(fortegn, 1, '0');
            rec += lpad(belob, 13, '0');
            rec += lpad(faknr, 9, '0');
            rec += rpad("", 21, ' ');
            if (sektionnr == "0112")
            {
                rec += rpad("", 2, '0');
            }
            else if (sektionnr == "0117")
            {
                rec += rpad("", 2, '0');
            }
            else
            {
                rec += rpad("", 6, '0');
            }

            if (sektionnr == "0117")
            {
                if (Mod10Check(indbetalerident))
                {
                    if (indbetalerident.Length == 15)
                    {
                        rec += indbetalerident;
                    }
                }
            }

            return rec;
        }

        private string write052(string sektionnr, string pbsnr, string transkode, long recordnr, string debgrpnr, string medlemsnr, long aftalenr, string advistekst1, double advisbelob1, string advistekst2, double advisbelob2)
        {
            string rec = null;


            rec = "BS052";
            //-- Recordtype
            rec += lpad(pbsnr, 8, '?');
            //-- PBS-nr.: Kreditors PBS-nummer
            rec += lpad(transkode, 4, '?');
            //-- Transkode:
            if (sektionnr == "0112")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else if (sektionnr == "0117")
            {
                //-- Recordnr.
                rec += lpad(recordnr, 5, '0');
            }
            else
            {
                //-- Recordnr.
                rec += lpad(recordnr, 3, '0');
            }
            rec += lpad(debgrpnr, 5, '0');
            //-- Debitorgruppenr.: Debitorgruppenummer
            rec += lpad(medlemsnr, 15, '0');
            //-- Kundenr.: Debitors kundenummer hos kreditor
            rec += lpad(aftalenr, 9, '0');
            //-- Aftalenr.
            rec += " ";
            if (sektionnr == "0112")
            {
                if (advisbelob1 != 0)
                {
                    rec += rpad(advistekst1, 38, ' ');
                    //-- Advistekst 1: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob1_formated = String.Format("{0:###0.00}", advisbelob1);
                    rec += rpad(advisbelob1_formated.Replace('.', ','), 9, ' ');
                }
                else
                {
                    //-- Advistekst 1: Tekstlinie på advis
                    rec += advistekst1;
                }
            }
            else if (sektionnr == "0117")
            {
                if (advisbelob1 != 0)
                {
                    rec += rpad(advistekst1, 38, ' ');
                    //-- Advistekst 1: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob1_formated = String.Format("{0:###0.00}", advisbelob1);
                    rec += rpad(advisbelob1_formated.Replace('.', ','), 9, ' ');
                }
                else
                {
                    //-- Advistekst 1: Tekstlinie på advis
                    rec += advistekst1;
                }
            }
            else
            {
                if (advisbelob1 != 0)
                {
                    rec += rpad(advistekst1, 29, ' ');
                    //-- Advistekst 1: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob1_formated = String.Format("{0:###0.00}", advisbelob1);
                    rec += rpad(advisbelob1_formated.Replace('.', ','), 9, ' ');
                }
                else
                {
                    //-- Advistekst 1: Tekstlinie på advis
                    rec += rpad(advistekst1, 38, ' ');
                }
                rec += '0';
                if (advisbelob2 != 0)
                {
                    rec += rpad(advistekst2, 29, ' ');
                    //-- Advistekst 2: Tekstlinie på advis
                    //-- Advisbeløb 1: Beløb på advis
                    string advisbelob2_formated = String.Format("{0:###0.00}", advisbelob2);
                    rec += rpad(advisbelob2_formated.Replace('.', ','), 9, ' ');
                }
                else if (advistekst2 != null)
                {
                    //-- Advistekst 2: Tekstlinie på advis
                    rec += advistekst2;
                }
            }
            return rec;
        }

        private string write092(string pbsnr, string sektionnr, string debgrpnr, int antal1, int belob1, int antal2, int antal3)
        {

            string rec = null;

            rec = "BS092";
            rec += lpad(pbsnr, 8, '?');
            rec += lpad(sektionnr, 4, '?');
            if (sektionnr == "0112")
            {
                rec += rpad("", 5, '0');
                rec += lpad(debgrpnr, 5, '0');
                rec += rpad("", 4, ' ');
            }
            else if (sektionnr == "0117")
            {
                rec += rpad("", 5, '0');
                rec += lpad(debgrpnr, 5, '0');
                rec += rpad("", 4, ' ');
            }
            else
            {
                rec += rpad("", 3, '0');
                rec += lpad(debgrpnr, 5, '0');
                rec += rpad("", 6, ' ');
            }
            rec += lpad(antal1, 11, '0');
            rec += lpad(belob1, 15, '0');
            rec += lpad(antal2, 11, '0');
            rec += rpad("", 15, ' ');
            rec += lpad(antal3, 11, '0');

            return rec;
        }

        private string write992(string datalevnr, string delsystem, string levtype, long antal1, long antal2, long belob2, long antal3, long antal4)
        {

            string rec = null;
            rec = "BS992";
            rec += lpad(datalevnr, 8, '?');
            rec += lpad(delsystem, 3, '?');
            rec += lpad(levtype, 4, '?');
            rec += lpad(antal1, 11, '0');
            rec += lpad(antal2, 11, '0');
            rec += lpad(belob2, 15, '0');
            rec += lpad(antal3, 11, '0');
            rec += lpad("", 15, '0');
            rec += lpad(antal4, 11, '0');
            rec += lpad("", 34, '0');

            return rec;
        }

        public object lpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadLeft(Length, PadChar);
        }

        public object rpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadRight(Length, PadChar);
        }

        public bool Mod10Check(string indbetalerident)
        {
            //// check whether input string is null or empty
            if (string.IsNullOrEmpty(indbetalerident))
            {
                return false;
            }

            //// 1.	Starting with the check digit double the value of every other digit 
            //// 2.	If doubling of a number results in a two digits number, add up
            ///   the digits to get a single digit number. This will results in eight single digit numbers                    
            //// 3. Get the sum of the digits
            int sumOfDigits = indbetalerident.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);


            //// If the final sum is divisible by 10, then the credit card number
            //   is valid. If it is not divisible by 10, the number is invalid.            
            return sumOfDigits % 10 == 0;
        }

        public int rsmembeshhip_betalinger_auto(dbData3060DataContext p_dbData3060, puls3060_nyEntities p_dbPuls3060_dk)
        {
            int winfotekst = 0;
            Boolean bBcc = false;
            int wantalbetalinger = 0;
            int AntalBetalinger = 0;
            DateTime now_minus5days = DateTime.UtcNow.AddDays(-5);//<------------------
            MemRSMembershipTransactions memRSMembershipTransactions = new MemRSMembershipTransactions();

            var qrytrans = from s in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers
                           where s.membership_id == 6 && s.status == 0
                           join t in p_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.last_transaction_id equals t.id
                           where t.status == "completed" && t.date > now_minus5days
                           orderby t.date ascending
                           select new
                           {
                               subscriber_id = s.id,
                               s.membership_id,
                               s.membership_start,
                               s.membership_end,
                               t.id,
                               t.user_id,
                               t.user_email,
                               t.user_data,
                               t.type,
                               t.@params,
                               t.date,
                               t.ip,
                               t.price,
                               t.coupon,
                               t.currency,
                               t.hash,
                               t.custom,
                               t.gateway,
                               t.status,
                               t.response_log
                           };
            var trans = qrytrans.ToList();

            int antal = trans.Count();
            foreach (var tr in trans)
            {
                bool newTrans = ((from q in p_dbData3060.TblrsmembershipPayments where q.TransId == tr.id select q.Id).Count() == 0);
                if (!newTrans) //<------------------Debug
                    continue;

                try  //<------------------Opdateret 29-6-2018 MHA
                {
                    User_data recud = clsHelper.unpack_UserData(tr.user_data);
                    recRSMembershipTransactions rec = new recRSMembershipTransactions
                    {
                        id = tr.id,
                        user_id = tr.user_id,
                        user_email = tr.user_email,
                        user_data = tr.user_data,
                        type = tr.type,
                        @params = tr.@params,
                        date = tr.date,
                        ip = tr.ip,
                        price = tr.price,
                        coupon = tr.coupon,
                        currency = tr.currency,
                        hash = tr.hash,
                        custom = tr.custom,
                        gateway = tr.gateway,
                        status = tr.status,
                        response_log = tr.response_log,
                        indmeldelse = (tr.type == "new") ? true : false,
                        tilmeldtpbs = false, // ?????
                        name = recud.name,
                        username = recud.username,
                        adresse = recud.adresse,
                        postnr = recud.postnr,
                        bynavn = recud.bynavn,
                        mobil = recud.mobil,
                        memberid = (!string.IsNullOrEmpty(recud.memberid)) ? int.Parse(recud.memberid) : 0,
                        kon = recud.kon,
                        fodtaar = recud.fodtaar,
                        message = recud.message,
                        password = recud.password,
                        fradato = tr.membership_start,
                        tildato = tr.membership_end,
                        membership_id = tr.membership_id,
                        subscriber_id = tr.subscriber_id
                    };
                    memRSMembershipTransactions.Add(rec);
                    AntalBetalinger++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("rsmembeshhip_betalinger_auto failed: {0} end", tr.user_data));
                }
            }


            if (AntalBetalinger > 0)
            {
                foreach (var rec_trans in memRSMembershipTransactions)
                {
                    if (rec_trans.indmeldelse) bBcc = true;
                    else bBcc = false;

                    //if (rec_trans.indmeldelse) winfotekst = 72;
                    //else winfotekst = (rec_trans.tilmeldtpbs) ? 10 : 12;
                    winfotekst = 72;

                    TblrsmembershipPayments rec_payment = new TblrsmembershipPayments()
                    {
                        TransId = rec_trans.id,
                        UserId = rec_trans.user_id,
                        UserEmail = rec_trans.user_email,
                        UserData = rec_trans.user_data,
                        Type = rec_trans.type,
                        Params = rec_trans.@params,
                        Ip = rec_trans.ip,
                        Date = rec_trans.date,
                        Price = rec_trans.price,
                        Coupon = rec_trans.coupon,
                        Currency = rec_trans.currency,
                        Hash = rec_trans.hash,
                        Custom = rec_trans.custom,
                        Gateway = rec_trans.gateway,
                        Status = rec_trans.status,
                        ResponseLog = rec_trans.response_log,
                        Name = rec_trans.name,
                        Adresse = rec_trans.adresse,
                        Postnr = rec_trans.postnr,
                        Bynavn = rec_trans.bynavn,
                        Memberid = rec_trans.memberid,
                        MembershipId = rec_trans.membership_id,
                        SubscriberId = rec_trans.subscriber_id,
                        MembershipStart = rec_trans.fradato,
                        MembershipEnd = rec_trans.tildato
                    };

                    clsInfotekst objInfotekst = new clsInfotekst
                    {
                        infotekst_id = winfotekst,
                        numofcol = null,
                        navn_medlem = rec_trans.name,
                        kaldenavn = rec_trans.name,
                        fradato = rec_trans.fradato,
                        tildato = rec_trans.tildato,
                        betalingsdato = rec_trans.date,
                        advisbelob = rec_trans.price,
                        //ocrstring = p_dbData3060.OcrString(rstdeb.Faknr),
                        underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører",
                        kundenr = rec_trans.memberid.ToString()
                    };
                    string ToName = rec_trans.name;
                    string ToAddr = rec_trans.user_email;
                    string subject = "Kvittering for medlemsskab af Puls 3060";
                    sendHtmlEmail(p_dbData3060, ToName, ToAddr, subject, objInfotekst, bBcc);

                    p_dbData3060.TblrsmembershipPayments.Add(rec_payment);
                    wantalbetalinger++;
                }
                p_dbData3060.SaveChanges();
            }
            return wantalbetalinger;
        }

        public void sendHtmlEmail(dbData3060DataContext p_dbData3060, string ToName, string ToAddr, string subject, clsInfotekst objInfotekst, Boolean bBcc)
        {
            string TemplateName = "Template-" + objInfotekst.infotekst_id.ToString();
            MimeMessage message_template = GetEmailTemplate(TemplateName);
            var builder = new BodyBuilder();
            builder.HtmlBody = objInfotekst.substitute_message(message_template.HtmlBody);
            builder.TextBody = objInfotekst.substitute_message(message_template.TextBody);
            var ts = message_template.BodyParts.Where(a => a.ContentType.MediaType == "image");
            foreach (MimePart attachment in ts)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                attachment.ContentObject.DecodeTo(ms);
                ms.Position = 0;
                MimeEntity img = builder.LinkedResources.Add(attachment.FileName, ms);
                img.ContentId = attachment.ContentId;
            }
            var message = new MimeMessage();
            message.Body = builder.ToMessageBody();

#if (DEBUG)
            message.To.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
            message.Subject = "TEST " + subject + " skal sendes til: " + ToName + " - " + ToAddr;
#else
            message.To.Add(new MailboxAddress(ToName, ToAddr));
            if (bBcc)
            {
                message.Bcc.Add(new MailboxAddress("Bjarne V. Hansen", "formand@puls3060.dk"));
            }
            message.Subject = subject;
#endif

            message.From.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
            using (var smtp_client = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp_client.Connect("smtp.gigahost.dk", 587, false);
                var AuthenticationMechanisms = smtp_client.AuthenticationMechanisms;
                smtp_client.AuthenticationMechanisms.Remove("CRAM-MD5");
                smtp_client.AuthenticationMechanisms.Remove("LOGIN");
                //smtp_client.AuthenticationMechanisms.Remove("PLAIN");
                smtp_client.AuthenticationMechanisms.Remove("NTLM");
                smtp_client.AuthenticationMechanisms.Remove("DIGEST-MD5");
                smtp_client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);

                using (var imap_client = new ImapClient())
                {
                    imap_client.Connect("imap.gigahost.dk", 993, true);
                    imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                    imap_client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);

                    var SendtPost = imap_client.GetFolder("Sendt post");
                    SendtPost.Open(FolderAccess.ReadWrite);

                    SendtPost.Append(message);
                    smtp_client.Send(message);

                    SendtPost.Close();
                    imap_client.Disconnect(true);
                }
                smtp_client.Disconnect(true);
            }
        }

        public MimeMessage GetEmailTemplate(string SearchText)
        {
            MimeMessage message;

            using (var imap_client = new ImapClient())
            {
                imap_client.Connect("imap.gigahost.dk", 993, true);
                imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                imap_client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);
                var Puls3060Templates = imap_client.GetFolder("Puls3060Templates");
                Puls3060Templates.Open(FolderAccess.ReadOnly);

                var result = Puls3060Templates.Search(SearchQuery.SubjectContains(SearchText));
                if (result.Count == 1)
                    message = Puls3060Templates.GetMessage(result.First());
                else
                    message = null;

                if (message == null) { Console.WriteLine("Not found"); }

                Puls3060Templates.Close();
                imap_client.Disconnect(true);
            }
            return message;
        }
    }

    public class clsRstdeb
    {
        public int? Nr { get; set; }
        public long? Kundenr { get; set; }
        public string Kaldenavn { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public int? Faknr { get; set; }
        public DateTime? Betalingsdato { get; set; }
        public DateTime? Fradato { get; set; }
        public DateTime? Tildato { get; set; }
        public int? Infotekst { get; set; }
        public int? Tilpbsid { get; set; }
        public string Advistekst { get; set; }
        public decimal? Belob { get; set; }
        //public string OcrString { get; set; }
        public string Email { get; set; }
        public string indbetalerident { get; set; }
        public Boolean? indmeldelse { get; set; }

    }

    public class recRyk
    {
        public int Nr { get; set; }
        public DateTime? betalingsdato { get; set; }
        public decimal? advisbelob { get; set; }
        public int? faknr { get; set; }
        public bool indmeldelse { get; set; }
    }
    public class MemRyk : List<recRyk>
    {

    }

    public class recRSMembershipTransactions
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string user_data { get; set; }
        public string user_email { get; set; }
        public string type { get; set; }
        public string @params { get; set; }
        public string ip { get; set; }
        public DateTime date { get; set; }
        public Decimal price { get; set; }
        public string coupon { get; set; }
        public string currency { get; set; }
        public string hash { get; set; }
        public string custom { get; set; }
        public string gateway { get; set; }
        public string status { get; set; }
        public string response_log { get; set; }

        public string name { get; set; }
        public string username { get; set; }
        public string adresse { get; set; }
        public string postnr { get; set; }
        public string bynavn { get; set; }
        public string mobil { get; set; }
        public int memberid { get; set; }
        public string kon { get; set; }
        public string fodtaar { get; set; }
        public string message { get; set; }
        public string password { get; set; }
        public int membership_id { get; set; }
        public int? subscriber_id { get; set; }
        public bool indmeldelse { get; set; }
        public bool tilmeldtpbs { get; set; }
        public DateTime? fradato { get; set; }
        public DateTime? tildato { get; set; }
    }

    public class MemRSMembershipTransactions : List<recRSMembershipTransactions>
    {

    }
}
