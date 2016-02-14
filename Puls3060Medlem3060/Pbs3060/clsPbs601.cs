using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System.Collections;

namespace nsPbs3060
{
    public enum datefmtType
    {
        fdmmddyyyy = 0,
        fdmmddyyyyhhmmss = 1,
    }

    public enum fakType
    {
        fdfaktura = 0,
        fdrykker = 1,
        fdrsmembership = 2,
    }

    public class clsPbs601
    {
        public clsPbs601() { }

        public Tuple<int, int> advis_autoxxx(dbData3060DataContext p_dbData3060, int ref_lobnr)
        {
            int lobnr = 0;
            string wadvistekst = "";
            int winfotekst;
            int wantaladvis = 0;
            string wDelsystem = "EML";

            var rstmedlems = from f in p_dbData3060.tblfaks
                             where f.tilpbsid == ref_lobnr && f.betalingsdato > DateTime.Today
                             join a in p_dbData3060.tbladvis on f.faknr equals a.faknr into jadvis
                             from a in jadvis.DefaultIfEmpty()
                             where a.id == null
                             join b in p_dbData3060.tblbetlins on f.faknr equals b.faknr into jbetlins
                             from b in jbetlins.DefaultIfEmpty()
                             where b.id == null
                             join r in p_dbData3060.tblrykkers on f.faknr equals r.faknr into jrykkers
                             from r in jrykkers.DefaultIfEmpty()
                             where r.id == null
                             select new
                             {
                                 f.Nr,
                                 f.betalingsdato,
                                 f.advisbelob,
                                 f.faknr,
                                 f.indmeldelse
                             };

            int adviscount = rstmedlems.Count();
            if (adviscount > 0)
            {
                tbltilpb rec_tilpbs = new tbltilpb
                {
                    delsystem = wDelsystem,
                    leverancetype = "0601",
                    udtrukket = DateTime.Now
                };
                p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
                p_dbData3060.SubmitChanges();
                lobnr = rec_tilpbs.id;

                foreach (var rstmedlem in rstmedlems)
                {
                    winfotekst = (rstmedlem.indmeldelse) ? 52 : 50;

                    tbladvi rec_advis = new tbladvi
                    {
                        betalingsdato = rstmedlem.betalingsdato,
                        Nr = rstmedlem.Nr,
                        faknr = rstmedlem.faknr,
                        advistekst = wadvistekst,
                        advisbelob = rstmedlem.advisbelob,
                        infotekst = winfotekst,
                        maildato = DateTime.Today,
                    };
                    rec_tilpbs.tbladvis.Add(rec_advis);
                    wantaladvis++;
                    if (wantaladvis >= 30) break; //max 30 advis på gang
                }
                p_dbData3060.SubmitChanges();
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

            var rstmedlems = from i in p_dbData3060.vAdvis_indbetalingskorts
                             where i.dato > DateTime.Today
                             join f in p_dbData3060.tblfaks on i.faknr equals f.faknr
                             select new
                             {
                                 i.Nr,
                                 betalingsdato = i.dato,
                                 advisbelob = i.belob,
                                 i.faknr,
                                 f.indmeldelse
                             };

            int adviscount = rstmedlems.Count();
            if (adviscount > 0)
            {
                tbltilpb rec_tilpbs = new tbltilpb
                {
                    delsystem = wDelsystem,
                    leverancetype = "0601",
                    udtrukket = DateTime.Now
                };
                p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
                p_dbData3060.SubmitChanges();
                lobnr = rec_tilpbs.id;

                foreach (var rstmedlem in rstmedlems)
                {
                    winfotekst = (rstmedlem.indmeldelse) ? 51 : 50;

                    tbladvi rec_advis = new tbladvi
                    {
                        betalingsdato = rstmedlem.betalingsdato,
                        Nr = rstmedlem.Nr,
                        faknr = rstmedlem.faknr,
                        advistekst = wadvistekst,
                        advisbelob = rstmedlem.advisbelob,
                        infotekst = winfotekst,
                        maildato = DateTime.Today,
                    };
                    rec_tilpbs.tbladvis.Add(rec_advis);
                    wantaladvis++;
                    if (wantaladvis >= 30) break; //max 30 advis på gang
                }
                p_dbData3060.SubmitChanges();
            }
            return new Tuple<int, int>(wantaladvis, lobnr);
        }

        public void advis_email(dbData3060DataContext p_dbData3060, int lobnr)
        {
            int wleveranceid;
            int? wSaveFaknr;

            {
                var antal = (from c in p_dbData3060.tbltilpbs
                             where c.id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in p_dbData3060.tbltilpbs
                             where c.id == lobnr && c.pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            {
                var antal = (from c in p_dbData3060.tbladvis
                             where c.tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in p_dbData3060.tbltilpbs
                          where c.id == lobnr
                          select c).First();
            if (rsttil.udtrukket == null) { rsttil.udtrukket = DateTime.Now; }
            if (rsttil.bilagdato == null) { rsttil.bilagdato = rsttil.udtrukket; }
            if (rsttil.delsystem == null) { rsttil.delsystem = "EML"; }
            if (rsttil.leverancetype == null) { rsttil.leverancetype = ""; }
            p_dbData3060.SubmitChanges();

            wleveranceid = (int)(from r in p_dbData3060.nextval("leveranceid") select r.id).First();
            tblpbsforsendelse rec_pbsforsendelse = new tblpbsforsendelse
            {
                delsystem = rsttil.delsystem,
                leverancetype = rsttil.leverancetype,
                oprettetaf = "Adv",
                oprettet = DateTime.Now,
                leveranceid = wleveranceid
            };
            p_dbData3060.tblpbsforsendelses.InsertOnSubmit(rec_pbsforsendelse);
            rec_pbsforsendelse.tbltilpbs.Add(rsttil);

            IEnumerable<clsRstdeb> rstdebs;
            if (true)
            {
                rstdebs = from k in p_dbData3060.tblrsmembership_transactions
                          join f in p_dbData3060.tblfaks on k.id equals f.id
                          join r in p_dbData3060.tbladvis on f.faknr equals r.faknr
                          where r.tilpbsid == lobnr && r.Nr != null
                          orderby r.faknr
                          select new clsRstdeb
                          {
                              Nr = f.Nr,
                              Kundenr = 32001610000000 + f.Nr,
                              Kaldenavn = k.name,
                              Navn = k.name,
                              Adresse = k.adresse,
                              Postnr = k.postnr,
                              Faknr = r.faknr,
                              Betalingsdato = f.betalingsdato,
                              Fradato = f.fradato,
                              Tildato = f.tildato,
                              Infotekst = r.infotekst,
                              Tilpbsid = r.tilpbsid,
                              Advistekst = r.advistekst,
                              Belob = r.advisbelob,
                              Email = k.user_email,
                              indbetalerident = f.indbetalerident,
                              indmeldelse = f.indmeldelse,
                          };
            }
            else
            {
                rstdebs = from k in p_dbData3060.tblMedlems
                          join r in p_dbData3060.tbladvis on k.Nr equals r.Nr
                          where r.tilpbsid == lobnr && r.Nr != null
                          join f in p_dbData3060.tblfaks on r.faknr equals f.faknr
                          orderby r.faknr
                          select new clsRstdeb
                          {
                              Nr = k.Nr,
                              Kundenr = 32001610000000 + k.Nr,
                              Kaldenavn = k.Kaldenavn,
                              Navn = k.Navn,
                              Adresse = k.Adresse,
                              Postnr = k.Postnr,
                              Faknr = r.faknr,
                              Betalingsdato = f.betalingsdato,
                              Fradato = f.fradato,
                              Tildato = f.tildato,
                              Infotekst = r.infotekst,
                              Tilpbsid = r.tilpbsid,
                              Advistekst = r.advistekst,
                              Belob = r.advisbelob,
                              Email = k.Email,
                              indbetalerident = f.indbetalerident,
                              indmeldelse = f.indmeldelse,
                          };
            }

            wSaveFaknr = 0;
            foreach (var rstdeb in rstdebs)
            {
                string OcrString = null;
                string windbetalerident = rstdeb.indbetalerident;
                if (rstdeb.Faknr != wSaveFaknr) //Løser problem med mere flere PBS Tblindbetalingskort records pr Faknr
                {
                    OcrString = p_dbData3060.OcrString(rstdeb.Faknr);
                    if (string.IsNullOrEmpty(OcrString))
                    {
                        if (clsHelper.Mod10Check(windbetalerident))
                        {
                            OcrString = string.Format(@"+71< {0}+81131945<", windbetalerident);
                        }
                    }

                    /*
                    string infotekst = new clsInfotekst
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
                        sendtsom = p_dbData3060.SendtSomString(rstdeb.Faknr),
                        kundenr = rstdeb.Kundenr.ToString()
                    }.getinfotekst(p_dbData3060);

                    if (infotekst.Length > 0)
                    {

                        //Send email
                        sendAdvisRykkerEmail(p_dbData3060, rstdeb.Navn, rstdeb.Email, "Betaling af Puls 3060 Kontingent", infotekst);

                    }
                    */
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
                        sendtsom = p_dbData3060.SendtSomString(rstdeb.Faknr),
                        kundenr = rstdeb.Kundenr.ToString()
                    };
                    string subject = "Betaling af Puls 3060 Kontingent";
                    Boolean bBcc = false;
                    if (rstdeb.indmeldelse != null) {
                        if ((Boolean)(rstdeb.indmeldelse) == true) bBcc = true;
                    }
                    //Send email
                    sendHtmlEmail(p_dbData3060, rstdeb.Navn, rstdeb.Email, subject, objInfotekst, bBcc);
                }
                wSaveFaknr = rstdeb.Faknr;
            } // -- End rstdebs

            rsttil.udtrukket = DateTime.Now;
            rsttil.leverancespecifikation = wleveranceid.ToString();
            p_dbData3060.SubmitChanges();
        }

        public Tuple<int, int> rykker_auto(dbData3060DataContext p_dbData3060, puls3060_dkEntities p_dbPuls3060_dk)
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

            var rstmedlems = from h in p_dbData3060.tblrsmembership_transactions
                             join f in p_dbData3060.tblfaks on h.id equals f.id
                             where f.SFaknr == null &&
                                   f.rykkerstop == false &&
                                   f.betalingsdato.Value.AddDays(90) > now && //må ikke være ælder end 90 dage <<<============mha 2014-01-11=====
                                   f.betalingsdato.Value.AddDays(7) <= now && //skal have været forfalden i 7 dage
                                   (int)(from q in p_dbData3060.tblrykkers where q.faknr == f.faknr select q).Count() == 0
                             orderby f.fradato, f.id
                             select new
                             {
                                 Nr = h.memberid,
                                 f.betalingsdato,
                                 f.advisbelob,
                                 f.faknr,
                                 f.indmeldelse,
                                 h.user_id,
                                 h.trans_id
                             };
            wantal1 = rstmedlems.Count();
            if (wantal1 > 0)
            {
                foreach (var m in rstmedlems)
                {
                    bool AllreadyPayedOrCancelledOrDeleted = ((from q in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers where q.user_id == m.user_id && q.membership_id == 6 && (q.membership_end > now_plus60 || q.status == 3) select q.id).Count() > 0);
                    if (!AllreadyPayedOrCancelledOrDeleted)
                    {
                        //Test om ecpwt_rsmembership_transactions findes
                        AllreadyPayedOrCancelledOrDeleted = ((from q in p_dbPuls3060_dk.ecpwt_rsmembership_transactions where q.id == m.trans_id select q.id).Count() > 0);                   
                    }                    
                    if (!AllreadyPayedOrCancelledOrDeleted)
                    {
                        recRyk rec = new recRyk
                        {
                            Nr = m.Nr,
                            betalingsdato = m.betalingsdato,
                            advisbelob = m.advisbelob,
                            faknr = m.faknr,
                            indmeldelse = m.indmeldelse
                        };
                        memRyk.Add(rec);
                        wantalrykkere++;
                        if (wantalrykkere >= 30) break; //max 30 rykkere på gang
                    }
                }

                if (wantalrykkere > 0)
                {
                    tbltilpb rec_tilpbs = new tbltilpb
                    {
                        delsystem = wDelsystem,
                        leverancetype = "0601",
                        udtrukket = DateTime.Now
                    };
                    p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
                    p_dbData3060.SubmitChanges();
                    lobnr = rec_tilpbs.id;

                    foreach (var rstmedlem in memRyk)
                    {
                        winfotekst = (rstmedlem.indmeldelse) ? 31 : 30;

                        tblrykker rec_rykker = new tblrykker
                        {
                            betalingsdato = rstmedlem.betalingsdato,
                            Nr = rstmedlem.Nr,
                            faknr = rstmedlem.faknr,
                            advistekst = wadvistekst,
                            advisbelob = rstmedlem.advisbelob,
                            infotekst = winfotekst,
                            rykkerdato = DateTime.Today,
                        };
                        rec_tilpbs.tblrykkers.Add(rec_rykker);
                    }
                    p_dbData3060.SubmitChanges();
                }

            } return new Tuple<int, int>(wantalrykkere, lobnr);
        }

        public Tuple<int, int> rykkere_bsh(dbData3060DataContext p_dbData3060)
        {
            int lobnr;
            string wadvistekst = "";
            int winfotekst;
            int wantalrykkere;
            wantalrykkere = 0;

            bool? wbsh = (from h in p_dbData3060.tempRykkerforslags select h.bsh).First();
            bool bsh = (wbsh == null) ? false : (bool)wbsh;
            string wDelsystem;
            if (bsh) wDelsystem = "BSH";
            else wDelsystem = "EML";


            tbltilpb rec_tilpbs = new tbltilpb
            {
                delsystem = wDelsystem,
                leverancetype = "0601",
                udtrukket = DateTime.Now
            };
            p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
            p_dbData3060.SubmitChanges();
            lobnr = rec_tilpbs.id;

            var rstmedlems = from k in p_dbData3060.tblMedlems
                             join l in p_dbData3060.tempRykkerforslaglinies on k.Nr equals l.Nr
                             join f in p_dbData3060.tblfaks on l.faknr equals f.faknr
                             join h in p_dbData3060.tempRykkerforslags on l.Rykkerforslagid equals h.id
                             select new
                             {
                                 k.Nr,
                                 k.Navn,
                                 h.betalingsdato,
                                 l.advisbelob,
                                 l.faknr,
                                 f.fradato,
                                 f.tildato,
                                 f.indmeldelse,
                                 f.tilmeldtpbs
                             };

            foreach (var rstmedlem in rstmedlems)
            {
                if (bsh) winfotekst = (rstmedlem.indmeldelse) ? 21 : 20;
                else winfotekst = (rstmedlem.indmeldelse) ? 31 : 30;

                tblrykker rec_rykker = new tblrykker
                {
                    betalingsdato = rstmedlem.betalingsdato,
                    Nr = rstmedlem.Nr,
                    faknr = rstmedlem.faknr,
                    advistekst = wadvistekst,
                    advisbelob = rstmedlem.advisbelob,
                    infotekst = winfotekst,
                    rykkerdato = DateTime.Today,
                };
                rec_tilpbs.tblrykkers.Add(rec_rykker);
                wantalrykkere++;
                if (wantalrykkere >= 30) break; //max 30 rykkere på gang
            }
            p_dbData3060.SubmitChanges();
            return new Tuple<int, int>(wantalrykkere, lobnr);
        }

        //******************************************************************************************************
        public List<string[]> RSMembership_KontingentForslag(DateTime p_DatoBetaltKontingentTil, dbData3060DataContext p_dbData3060)
        {
            List<string[]> items = new List<string[]>();
            puls3060_dkEntities jdb = new puls3060_dkEntities();
            DateTime KontingentFradato = DateTime.MinValue;
            DateTime KontingentTildato = DateTime.MinValue;
            bool tilmeldtpbs = false;
            bool indmeldelse = false;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            int ikontingent;
            int iNr = 0;

            var qry_rsmembership = from s in jdb.ecpwt_rsmembership_membership_subscribers
                                   where s.membership_id == 6 && s.status != 3
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
                                       tl.user_data
                                   };
            var antal = qry_rsmembership.Count();
            var rsm = qry_rsmembership.ToArray();
            foreach (var m in rsm)
            {
                bool bSelected = true;
                AntalMedlemmer++;
                tilmeldtpbs = false;
                indmeldelse = false;

                if (m.Nr == "")
                    iNr = 10000 + m.user_id;
                else
                    iNr = int.Parse(m.Nr);

                bool erMedlemPusterummet = ((from um in jdb.ecpwt_user_usergroup_map
                                             join g in jdb.ecpwt_usergroups on um.group_id equals g.id
                                             where g.title == "Pusterummet" && um.user_id == m.user_id
                                             select um.user_id).Count() > 0);

                if (erMedlemPusterummet)
                {
                    bSelected = false;
                }
                else //Er medlem
                {
                    if ((m.kontingentBetaltTilDato != null) && (m.kontingentBetaltTilDato > m.indmeldelsesDato))  //'Der findes en kontingent-betaling
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
                }


                if (bSelected)
                {
                    DateTime TodayMinus90 = DateTime.Now.AddDays(-90);
                    var qry_fak = from f in p_dbData3060.tblfaks
                                  where f.Nr == iNr && f.betalingsdato > TodayMinus90
                                  join t in p_dbData3060.tblrsmembership_transactions on f.id equals t.id
                                  select f;

                    if (qry_fak.Count() > 0) //Der findes en opkrævning
                    {
                        bSelected = false;
                    }
                }


                if (bSelected)
                {
                    AntalForslag++;
                    tilmeldtpbs = (bool)p_dbData3060.erPBS(iNr);
                    KontingentTildato = KontingentFradato.AddMonths(12);
                    ikontingent = (int)m.Kontingent;

                    string[] item = new string[9];
                    item[0] = m.user_id.ToString();
                    item[1] = m.Navn;
                    item[2] = m.Adresse;
                    item[3] = m.Postnr;
                    item[4] = string.Format("{0:dd-MM-yyy}", KontingentFradato);
                    item[5] = ikontingent.ToString();
                    item[6] = string.Format("{0:dd-MM-yyy}", KontingentTildato);
                    item[7] = (indmeldelse) ? "J" : "N";
                    item[8] = (tilmeldtpbs) ? "J" : "N";
                    items.Add(item);
                }
            }
            return items;
        }
        //******************************************************************************************************

        public Tuple<int, int> kontingent_fakturer_auto(dbData3060DataContext p_dbData3060)
        {
            int lobnr = 0;
            string wadvistekst = "";
            int winfotekst = 0;
            int wantalfakturaer = 0;
            string wDelsystem;
            wDelsystem = "BSH";

            DateTime KontingentFradato = DateTime.MinValue;
            bool tilmeldtpbs = false;
            bool indmeldelse = false;
            int AntalForslag = 0;
            MemKont memKont = new MemKont();

            var qry_medlemmer = from h in p_dbData3060.tblMedlems
                                select new
                                {
                                    Nr = h.Nr,
                                    Navn = h.Navn,
                                    FodtDato = h.FodtDato,
                                    erMedlem = ((bool)p_dbData3060.erMedlem(h.Nr)) ? 1 : 0,
                                    indmeldelsesDato = p_dbData3060.indmeldtdato(h.Nr),
                                    udmeldelsesDato = p_dbData3060.udmeldtdato(h.Nr),
                                    kontingentBetaltTilDato = p_dbData3060.kontingentdato(h.Nr),
                                    opkrævningsDato = p_dbData3060.forfaldsdato(h.Nr),
                                };

            foreach (var m in qry_medlemmer)
            {
                bool bSelected = true;
                tilmeldtpbs = false;
                indmeldelse = false;

                if (m.erMedlem == 0) //er ikke medlem
                {
                    bSelected = false;
                }
                else //Er medlem
                {
                    if ((m.kontingentBetaltTilDato != null) && (m.kontingentBetaltTilDato > m.indmeldelsesDato))  //'Der findes en kontingent-betaling
                    {
                        bSelected = false;
                    }
                    else  //Der findes ingen kontingent-betaling
                    {
                        KontingentFradato = (DateTime)m.indmeldelsesDato;
                        indmeldelse = true;
                    }
                }

                if (bSelected)
                {
                    if (m.opkrævningsDato != null) //Der findes en opkrævning
                    {
                        if (((DateTime)m.opkrævningsDato) > KontingentFradato)
                        {
                            bSelected = false;
                        }
                    }
                }


                if (bSelected && indmeldelse)
                {
                    AntalForslag++;
                    tilmeldtpbs = (bool)p_dbData3060.erPBS(m.Nr);
                    clsKontingent objKontingent = new clsKontingent(p_dbData3060, KontingentFradato, m.Nr);

                    recKont rec = new recKont
                    {
                        Nr = m.Nr,
                        Navn = m.Navn,
                        betalingsdato = clsOverfoersel.bankdageplus(DateTime.Today, 8),
                        advisbelob = objKontingent.Kontingent,
                        fradato = KontingentFradato,
                        tildato = objKontingent.KontingentTildato,
                        indmeldelse = true,
                        tilmeldtpbs = tilmeldtpbs
                    };
                    memKont.Add(rec);
                }
            }


            if (AntalForslag > 0)
            {
                tbltilpb rec_tilpbs = new tbltilpb
                {
                    delsystem = wDelsystem,
                    leverancetype = "0601",
                    udtrukket = DateTime.Now
                };
                p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
                p_dbData3060.SubmitChanges();
                lobnr = rec_tilpbs.id;

                foreach (var rstmedlem in memKont)
                {
                    if (rstmedlem.indmeldelse) winfotekst = 11;
                    else winfotekst = (rstmedlem.tilmeldtpbs) ? 10 : 12;
                    int next_faknr = (int)(from r in p_dbData3060.nextval("faknr") select r.id).First();
                    tblfak rec_fak = new tblfak
                    {
                        betalingsdato = rstmedlem.betalingsdato,
                        Nr = rstmedlem.Nr,
                        faknr = next_faknr,
                        advistekst = wadvistekst,
                        advisbelob = rstmedlem.advisbelob,
                        infotekst = winfotekst,
                        bogfkonto = 1800,
                        vnr = 1,
                        fradato = rstmedlem.fradato,
                        tildato = rstmedlem.tildato,
                        indmeldelse = rstmedlem.indmeldelse,
                        tilmeldtpbs = rstmedlem.tilmeldtpbs
                    };
                    rec_tilpbs.tblfaks.Add(rec_fak);
                    wantalfakturaer++;
                }
                p_dbData3060.SubmitChanges();
            }
            return new Tuple<int, int>(wantalfakturaer, lobnr);

        }

        public Tuple<int, int> rsmembeshhip_fakturer_auto(dbData3060DataContext p_dbData3060, puls3060_dkEntities p_dbPuls3060_dk)
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
                           where (t.status == "pending" && t.type == "new" && t.@params == "membership_id=6" && t.gateway == "PBS")
                              || (t.status == "pending" && t.type == "new" && t.@params == "membership_id=6" && t.gateway == "PayPal" && t.date < now_minus60)
                           select t;
            List<ecpwt_rsmembership_transactions> trans = qrytrans.ToList();

            int antal = trans.Count();
            foreach (var tr in trans)
            {
                bool AllreadyPayed = ((from q in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers where q.user_id == tr.user_id && q.membership_id == 6 && q.membership_end > now_minus60 select q.id).Count() > 0);
                if (AllreadyPayed)
                    continue;

                bool newTrans = ((from q in p_dbData3060.tblrsmembership_transactions where q.trans_id == tr.id select q.id).Count() == 0);
                if (!newTrans)
                    continue;

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
                     indmeldelse = true,
                     tilmeldtpbs = false,
                     name = recud.name,
                     username = recud.username,
                     adresse = recud.adresse,
                     postnr = recud.postnr,
                     bynavn = recud.bynavn,
                     mobil = recud.mobil,
                     memberid = (recud.memberid != "") ? int.Parse(recud.memberid) : 10000 + tr.user_id,
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
                tbltilpb rec_tilpbs = new tbltilpb
                {
                    delsystem = wDelsystem,
                    leverancetype = "0601",
                    udtrukket = DateTime.Now
                };
                p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
                p_dbData3060.SubmitChanges();
                lobnr = rec_tilpbs.id;

                foreach (var rec_trans in memRSMembershipTransactions)
                {
                    if (rec_trans.indmeldelse) winfotekst = 11;
                    else winfotekst = (rec_trans.tilmeldtpbs) ? 10 : 12;
                    int next_faknr = (int)(from r in p_dbData3060.nextval("faknr") select r.id).First();
                    string windbetalerident = clsHelper.generateIndbetalerident(next_faknr); //?????????????????????????????????
                    tblfak rec_fak = new tblfak
                    {
                        betalingsdato = clsOverfoersel.bankdageplus(DateTime.Today, 8),
                        Nr = rec_trans.memberid,
                        faknr = next_faknr,
                        advistekst = wadvistekst,
                        advisbelob = rec_trans.price,
                        infotekst = winfotekst,
                        bogfkonto = 1800,
                        vnr = 1,
                        fradato = rec_trans.date,
                        tildato = rec_trans.date.AddYears(1),
                        indmeldelse = rec_trans.indmeldelse,
                        tilmeldtpbs = rec_trans.tilmeldtpbs,
                        indbetalerident = windbetalerident, // ToDo generer indbetalerident
                        tblrsmembership_transaction = new tblrsmembership_transaction()
                        {
                            trans_id = rec_trans.id,
                            user_id = rec_trans.user_id,
                            user_email = rec_trans.user_email,
                            user_data = rec_trans.user_data,
                            type = rec_trans.type,
                            @params = rec_trans.@params,
                            ip = rec_trans.ip,
                            date = rec_trans.date,
                            price = rec_trans.price,
                            coupon = rec_trans.coupon,
                            currency = rec_trans.currency,
                            hash = rec_trans.hash,
                            custom = rec_trans.custom,
                            gateway = rec_trans.gateway,
                            status = rec_trans.status,
                            response_log = rec_trans.response_log,
                            name = rec_trans.name,
                            adresse = rec_trans.adresse,
                            postnr = rec_trans.postnr,
                            bynavn = rec_trans.bynavn,
                            memberid = rec_trans.memberid,
                            membership_id = rec_trans.membership_id,
                            subscriber_id = rec_trans.subscriber_id
                        }
                    };
                    rec_tilpbs.tblfaks.Add(rec_fak);
                    wantalfakturaer++;
                }
                p_dbData3060.SubmitChanges();
            }

            return new Tuple<int, int>(wantalfakturaer, lobnr);

        }

        public Tuple<int, int> kontingent_fakturer_bs1(dbData3060DataContext p_dbData3060)
        {
            int lobnr;
            string wadvistekst = "";
            int winfotekst;
            int wantalfakturaer;
            wantalfakturaer = 0;

            bool? wbsh = (from h in p_dbData3060.tempKontforslags select h.bsh).First();
            bool bsh = (wbsh == null) ? false : (bool)wbsh;
            string wDelsystem;
            if (bsh) wDelsystem = "BSH";
            else wDelsystem = "BS1";

            tbltilpb rec_tilpbs = new tbltilpb
            {
                delsystem = wDelsystem,
                leverancetype = "0601",
                udtrukket = DateTime.Now
            };
            p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
            p_dbData3060.SubmitChanges();
            lobnr = rec_tilpbs.id;

            var rstmedlems = from k in p_dbData3060.tblMedlems
                             join l in p_dbData3060.tempKontforslaglinies on k.Nr equals l.Nr
                             join h in p_dbData3060.tempKontforslags on l.Kontforslagid equals h.id
                             select new
                             {
                                 k.Nr,
                                 k.Navn,
                                 h.betalingsdato,
                                 l.advisbelob,
                                 l.fradato,
                                 l.tildato,
                                 l.indmeldelse,
                                 l.tilmeldtpbs,
                             };

            foreach (var rstmedlem in rstmedlems)
            {
                if (rstmedlem.indmeldelse) winfotekst = 11;
                else winfotekst = (rstmedlem.tilmeldtpbs) ? 10 : 12;
                int next_faknr = (int)(from r in p_dbData3060.nextval("faknr") select r.id).First();
                tblfak rec_fak = new tblfak
                {
                    betalingsdato = rstmedlem.betalingsdato,
                    Nr = rstmedlem.Nr,
                    faknr = next_faknr,
                    advistekst = wadvistekst,
                    advisbelob = rstmedlem.advisbelob,
                    infotekst = winfotekst,
                    bogfkonto = 1800,
                    vnr = 1,
                    fradato = rstmedlem.fradato,
                    tildato = rstmedlem.tildato,
                    indmeldelse = rstmedlem.indmeldelse,
                    tilmeldtpbs = rstmedlem.tilmeldtpbs
                };
                rec_tilpbs.tblfaks.Add(rec_fak);
                wantalfakturaer++;
            }
            p_dbData3060.SubmitChanges();
            return new Tuple<int, int>(wantalfakturaer, lobnr);

        }

        public Tuple<int, int> rsmembeshhip_kontingent_fakturer_bs1(dbData3060DataContext p_dbData3060, puls3060_dkEntities p_dbPuls3060_dk, Memkontingentforslag memKontingentforslag)
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

            tbltilpb rec_tilpbs = new tbltilpb
            {
                delsystem = wDelsystem,
                leverancetype = "0601",
                udtrukket = DateTime.Now
            };
            p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
            p_dbData3060.SubmitChanges();
            lobnr = rec_tilpbs.id;

            foreach (recKontingentforslag rec in memKontingentforslag)
            {
                var qry_rsmembership = from s in p_dbPuls3060_dk.ecpwt_rsmembership_membership_subscribers
                                       where s.user_id == rec.user_id && s.membership_id == rec.membership_id
                                       join tf in p_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.from_transaction_id equals tf.id
                                       join tl in p_dbPuls3060_dk.ecpwt_rsmembership_transactions on s.last_transaction_id equals tl.id
                                       join m in p_dbPuls3060_dk.ecpwt_rsmembership_subscribers on s.user_id equals m.user_id
                                       join u in p_dbPuls3060_dk.ecpwt_users on s.user_id equals u.id
                                       select new
                                       {
                                           Nr = m.f14,
                                           Navn = u.name,
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
                                           currency = tl.currency
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
                if ((mydata.memberid == null) || (mydata.memberid == "")) mydata.memberid = (10000 + rsm.user_id).ToString();
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
                    int next_faknr = (int)(from r in p_dbData3060.nextval("faknr") select r.id).First();
                    tblfak rec_fak = new tblfak
                    {
                        betalingsdato = rec.betalingsdato,
                        Nr = int.Parse(rsm.Nr),
                        faknr = next_faknr,
                        advistekst = wadvistekst,
                        advisbelob = rec.advisbelob,
                        infotekst = winfotekst,
                        bogfkonto = 1800,
                        vnr = 1,
                        fradato = rec.fradato,
                        tildato = rec.tildato,
                        indmeldelse = rec.indmeldelse,
                        tilmeldtpbs = rec.tilmeldtpbs,
                        tblrsmembership_transaction = new tblrsmembership_transaction()
                        {
                            trans_id = rec_trans.id,
                            user_id = rec_trans.user_id,
                            user_email = rec_trans.user_email,
                            user_data = rec_trans.user_data,
                            type = rec_trans.type,
                            @params = rec_trans.@params,
                            ip = rec_trans.ip,
                            date = rec_trans.date,
                            price = rec_trans.price,
                            coupon = rec_trans.coupon,
                            currency = rec_trans.currency,
                            hash = rec_trans.hash,
                            custom = rec_trans.custom,
                            gateway = rec_trans.gateway,
                            status = rec_trans.status,
                            response_log = rec_trans.response_log,
                            name = mydata.name,
                            adresse = mydata.adresse,
                            postnr = mydata.postnr,
                            bynavn = mydata.bynavn,
                            memberid = int.Parse(rsm.Nr),
                            membership_id = rec.membership_id,
                            subscriber_id = rsm.rsmembership_membership_subscribers_id
                        }
                    };
                    rec_tilpbs.tblfaks.Add(rec_fak);
                    wantalfakturaer++;
                }
            }
            p_dbData3060.SubmitChanges();
            return new Tuple<int, int>(wantalfakturaer, lobnr);

        }

        public void rykker_email(dbData3060DataContext p_dbData3060, int lobnr)
        {
            int wleveranceid;
            int? wSaveFaknr;

            {
                var antal = (from c in p_dbData3060.tbltilpbs
                             where c.id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in p_dbData3060.tbltilpbs
                             where c.id == lobnr && c.pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            {
                var antal = (from c in p_dbData3060.tblrykkers
                             where c.tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in p_dbData3060.tbltilpbs
                          where c.id == lobnr
                          select c).First();
            if (rsttil.udtrukket == null) { rsttil.udtrukket = DateTime.Now; }
            if (rsttil.bilagdato == null) { rsttil.bilagdato = rsttil.udtrukket; }
            if (rsttil.delsystem == null) { rsttil.delsystem = "EML"; }
            if (rsttil.leverancetype == null) { rsttil.leverancetype = ""; }
            p_dbData3060.SubmitChanges();

            wleveranceid = (int)(from r in p_dbData3060.nextval("leveranceid") select r.id).First();
            tblpbsforsendelse rec_pbsforsendelse = new tblpbsforsendelse
            {
                delsystem = rsttil.delsystem,
                leverancetype = rsttil.leverancetype,
                oprettetaf = "Ryk",
                oprettet = DateTime.Now,
                leveranceid = wleveranceid
            };
            p_dbData3060.tblpbsforsendelses.InsertOnSubmit(rec_pbsforsendelse);
            rec_pbsforsendelse.tbltilpbs.Add(rsttil);

            var rstdebs = from r in p_dbData3060.tblrykkers
                          where r.tilpbsid == lobnr && r.Nr != null
                          join f in p_dbData3060.tblfaks on r.faknr equals f.faknr
                          join k in p_dbData3060.tblrsmembership_transactions on f.id equals k.id
                          orderby r.faknr
                          select new clsRstdeb
                          {
                              Nr = k.memberid,
                              Kundenr = 32001610000000 + k.memberid,
                              Kaldenavn = k.name,
                              Navn = k.name,
                              Adresse = k.adresse,
                              Postnr = k.postnr,
                              Faknr = r.faknr,
                              Betalingsdato = f.betalingsdato,
                              Fradato = f.fradato,
                              Tildato = f.tildato,
                              Infotekst = r.infotekst,
                              Tilpbsid = r.tilpbsid,
                              Advistekst = r.advistekst,
                              Belob = r.advisbelob,
                              Email = k.user_email,
                              indbetalerident = f.indbetalerident,
                              indmeldelse = f.indmeldelse,
                          };

            wSaveFaknr = 0;
            foreach (var rstdeb in rstdebs)
            {
                if (rstdeb.Faknr != wSaveFaknr) //Løser problem med mere flere PBS Tblindbetalingskort records pr Faknr
                {
                    /*
                    string infotekst = new clsInfotekst
                    {
                        infotekst_id = rstdeb.Infotekst,
                        numofcol = null,
                        navn_medlem = rstdeb.Navn,
                        kaldenavn = rstdeb.Kaldenavn,
                        fradato = rstdeb.Fradato,
                        tildato = rstdeb.Tildato,
                        betalingsdato = rstdeb.Betalingsdato,
                        advisbelob = rstdeb.Belob,
                        ocrstring = p_dbData3060.OcrString(rstdeb.Faknr),
                        underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører",
                        sendtsom = p_dbData3060.SendtSomString(rstdeb.Faknr),
                        kundenr = rstdeb.Kundenr.ToString()
                    }.getinfotekst(p_dbData3060);

                    if (infotekst.Length > 0)
                    {

                        //Send email
                        sendAdvisRykkerEmail(p_dbData3060, rstdeb.Navn, rstdeb.Email, "Betaling af Puls 3060 Kontingent", infotekst);

                    }
                    */
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
                        ocrstring = p_dbData3060.OcrString(rstdeb.Faknr),
                        underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører",
                        sendtsom = p_dbData3060.SendtSomString(rstdeb.Faknr),
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

            rsttil.udtrukket = DateTime.Now;
            rsttil.leverancespecifikation = wleveranceid.ToString();
            p_dbData3060.SubmitChanges();
        }

        public void faktura_og_rykker_601_action(dbData3060DataContext p_dbData3060, int lobnr, fakType fakryk)
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
                var antal = (from c in p_dbData3060.tbltilpbs
                             where c.id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in p_dbData3060.tbltilpbs
                             where c.id == lobnr && c.pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            if ((fakryk == fakType.fdfaktura) || (fakryk == fakType.fdrsmembership)) //KONTINGENT FAKTURA
            {
                var antal = (from c in p_dbData3060.tblfaks
                             where c.tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }
            if (fakryk == fakType.fdrykker) //RYKKER
            {
                var antal = (from c in p_dbData3060.tblrykkers
                             where c.tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in p_dbData3060.tbltilpbs
                          where c.id == lobnr
                          select c).First();
            if (rsttil.udtrukket == null) { rsttil.udtrukket = DateTime.Now; }
            if (rsttil.bilagdato == null) { rsttil.bilagdato = rsttil.udtrukket; }
            if (rsttil.delsystem == null) { rsttil.delsystem = "BS1"; }  // ????????????????
            if (rsttil.leverancetype == null) { rsttil.leverancetype = ""; }
            p_dbData3060.SubmitChanges();

            wleveranceid = (int)(from r in p_dbData3060.nextval("leveranceid") select r.id).First();
            tblpbsforsendelse rec_pbsforsendelse = new tblpbsforsendelse
            {
                delsystem = rsttil.delsystem,
                leverancetype = rsttil.leverancetype,
                oprettetaf = "Fak",
                oprettet = DateTime.Now,
                leveranceid = wleveranceid
            };
            p_dbData3060.tblpbsforsendelses.InsertOnSubmit(rec_pbsforsendelse);
            rec_pbsforsendelse.tbltilpbs.Add(rsttil);

            tblpbsfilename rec_pbsfiles = new tblpbsfilename();
            rec_pbsforsendelse.tblpbsfilenames.Add(rec_pbsfiles);

            var rstkrd = (from c in p_dbData3060.tblkreditors
                          where c.delsystem == rsttil.delsystem
                          select c).First();


            // -- Leverance Start - 0601 Betalingsoplysninger
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - rsttil.Delsystem - Delsystem:  Dataleverandør delsystem
            // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
            // - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
            // - rsttil!udtrukket - Dato: 000000 eller leverancens dannelsesdato
            rec = write002(rstkrd.datalevnr, rsttil.delsystem, "0601", wleveranceid.ToString(), (DateTime)rsttil.udtrukket);
            tblpbsfile rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
            rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

            // -- Sektion start - sektion 0112/0117
            // -  rstkrd.Pbsnr       - PBS-nr.: Kreditors PBS-nummer
            // -  rstkrd.Sektionnr   - Sektionsnr.: 0112/0117 (Betalinger med lang advistekst)
            // -  rstkrd.Debgrpnr    - Debitorgruppenr.: Debitorgruppenummer
            // -  rstkrd.Datalevnavn - Leveranceidentifikation: Brugers identifikation hos dataleverandør
            // -  rsttil.Udtrukket   - Dato: 000000 eller leverancens dannelsesdato
            // -  rstkrd.Regnr       - Reg.nr.: Overførselsregistreringsnummer
            // -  rstkrd.Kontonr     - Kontonr.: Overførselskontonummer
            // -  h_linie            - H-linie: Tekst til hovedlinie på advis
            rec = write012(rstkrd.pbsnr, rstkrd.sektionnr, rstkrd.debgrpnr, rstkrd.datalevnavn, (DateTime)rsttil.udtrukket, rstkrd.regnr, rstkrd.kontonr, h_linie);
            rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
            rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);
            antalsek++;

            IEnumerable<clsRstdeb> rstdebs;
            if (fakryk == fakType.fdfaktura) //KONTINGENT FAKTURA
            {

                rstdebs = from k in p_dbData3060.tblMedlems
                          join f in p_dbData3060.tblfaks on k.Nr equals f.Nr
                          where f.tilpbsid == lobnr && f.Nr != null
                          orderby f.Nr
                          select new clsRstdeb
                          {
                              Nr = k.Nr,
                              Kundenr = 32001610000000 + k.Nr,
                              Kaldenavn = k.Kaldenavn,
                              Navn = k.Navn,
                              Adresse = k.Adresse,
                              Postnr = k.Postnr,
                              Faknr = f.faknr,
                              Betalingsdato = f.betalingsdato,
                              Fradato = f.fradato,
                              Tildato = f.tildato,
                              Infotekst = f.infotekst,
                              Tilpbsid = f.tilpbsid,
                              Advistekst = f.advistekst,
                              Belob = f.advisbelob,
                              indbetalerident = f.indbetalerident,
                              indmeldelse = f.indmeldelse,
                          };
            }
            else if (fakryk == fakType.fdrykker) //RYKKER
            {
                rstdebs = from k in p_dbData3060.tblMedlems
                          join r in p_dbData3060.tblrykkers on k.Nr equals r.Nr
                          where r.tilpbsid == lobnr && r.Nr != null
                          join f in p_dbData3060.tblfaks on r.faknr equals f.faknr
                          orderby r.Nr
                          select new clsRstdeb
                          {
                              Nr = k.Nr,
                              Kundenr = 32001610000000 + k.Nr,
                              Kaldenavn = k.Kaldenavn,
                              Navn = k.Navn,
                              Adresse = k.Adresse,
                              Postnr = k.Postnr,
                              Faknr = r.faknr,
                              Betalingsdato = r.betalingsdato,
                              Fradato = f.fradato,
                              Tildato = f.tildato,
                              Infotekst = r.infotekst,
                              Tilpbsid = r.tilpbsid,
                              Advistekst = r.advistekst,
                              Belob = r.advisbelob,
                              indbetalerident = f.indbetalerident,
                              indmeldelse = f.indmeldelse,
                          };
            }
            else if (fakryk == fakType.fdrsmembership) //KONTINGENT FAKTURA
            {
                rstdebs = from k in p_dbData3060.tblrsmembership_transactions
                          join f in p_dbData3060.tblfaks on k.id equals f.id
                          where f.tilpbsid == lobnr && f.Nr != null
                          orderby f.Nr
                          select new clsRstdeb
                          {
                              Nr = f.Nr,
                              Kundenr = 32001610000000 + f.Nr,
                              Kaldenavn = k.name,
                              Navn = k.name,
                              Adresse = k.adresse,
                              Postnr = k.postnr,
                              Faknr = f.faknr,
                              Betalingsdato = f.betalingsdato,
                              Fradato = f.fradato,
                              Tildato = f.tildato,
                              Infotekst = f.infotekst,
                              Tilpbsid = f.tilpbsid,
                              Advistekst = f.advistekst,
                              Belob = f.advisbelob,
                              indbetalerident = f.indbetalerident,
                              indmeldelse = f.indmeldelse,
                          };
            }
            else
            {
                rstdebs = null;
            }

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
                rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

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
                rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

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
                    rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                    rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);
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
                rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

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
                rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

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
                        rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                        rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);
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
                    ocrstring = p_dbData3060.OcrString(rstdeb.Faknr),
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
                        rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                        rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);
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
            rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
            rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

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
            rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
            rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

            rsttil.udtrukket = DateTime.Now;
            rsttil.leverancespecifikation = wleveranceid.ToString();
            p_dbData3060.SubmitChanges();
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
                if (clsHelper.Mod10Check(indbetalerident))
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

        public void sendAdvisRykkerEmail_old(dbData3060DataContext p_dbData3060, string ToName, string ToAddr, string subject, string body)
        {

            string SmtpUsername = p_dbData3060.GetSysinfo("SMTPUSER");
            string SmtpPassword = p_dbData3060.GetSysinfo("SMTPPASSWD");
            var smtp = new System.Net.Mail.SmtpClient
            {
                Host = p_dbData3060.GetSysinfo("SMTPHOST"),
                Port = int.Parse(p_dbData3060.GetSysinfo("SMTPPORT")),
                EnableSsl = bool.Parse(p_dbData3060.GetSysinfo("SMTPSSL")),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SmtpUsername, SmtpPassword)
            };

            MailMessage email = new MailMessage();

#if (DEBUG)
            email.To.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILTOADDR"), p_dbData3060.GetSysinfo("MAILTONAME")));
            email.Subject = "TEST " + subject + " skal sendes til: " + ToName + " - " + ToAddr;
#else
            if (ToAddr.Length > 0)
            {
                email.To.Add(new MailAddress(ToAddr, ToName));
                email.Subject = subject;
            }
            else
            {
                email.To.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILTOADDR"), p_dbData3060.GetSysinfo("MAILTONAME")));
                email.Subject = subject + " skal sendes til: " + ToName;
            }
            email.Bcc.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILTOADDR"), p_dbData3060.GetSysinfo("MAILTONAME")));

#endif
            email.Body = body;
            email.From = new MailAddress(p_dbData3060.GetSysinfo("MAILFROM"));
            email.ReplyToList.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILREPLY")));

            smtp.Send(email);
        }

        public void sendAdvisRykkerEmail(dbData3060DataContext p_dbData3060, string ToName, string ToAddr, string subject, string body)
        {
            var message = new MimeMessage();
#if (DEBUG)
            message.To.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
            message.Subject = "TEST " + subject + " skal sendes til: " + ToName + " - " + ToAddr;
#else
            if (ToAddr.Length > 0)
            {
                message.To.Add(new MailboxAddress(ToName, ToAddr));
                message.Subject = subject;
            }
            else
            {
                message.To.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
                message.Subject = subject + " skal sendes til: " + ToName;
            }
#endif
            message.Body = new TextPart("plain") { Text = body };
            message.From.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));

            using (var smtp_client = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp_client.Connect("smtp.gigahost.dk", 587, false);
                smtp_client.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp_client.Authenticate("regnskab@puls3060.dk", "1234West+");

                using (var imap_client = new ImapClient())
                {
                    imap_client.Connect("imap.gigahost.dk", 993, true);
                    imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                    imap_client.Authenticate("regnskab@puls3060.dk", "1234West+");

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

        public int rsmembeshhip_betalinger_auto(dbData3060DataContext p_dbData3060, puls3060_dkEntities p_dbPuls3060_dk)
        {
            int winfotekst = 0;
            Boolean bBcc = false;
            int wantalbetalinger = 0;
            int AntalBetalinger = 0;
            DateTime now_minus5days = DateTime.UtcNow.AddDays(-5);
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
                bool newTrans = ((from q in p_dbData3060.tblrsmembership_payments where q.trans_id == tr.id select q.id).Count() == 0);
                if (!newTrans)
                    continue;

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
                    memberid = (recud.memberid != "") ? int.Parse(recud.memberid) : 10000 + tr.user_id,
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


            if (AntalBetalinger > 0)
            {
                foreach (var rec_trans in memRSMembershipTransactions)
                {
                    if (rec_trans.indmeldelse) bBcc = true;
                    else bBcc = false;

                    //if (rec_trans.indmeldelse) winfotekst = 72;
                    //else winfotekst = (rec_trans.tilmeldtpbs) ? 10 : 12;
                    winfotekst = 72;

                    tblrsmembership_payment rec_payment = new tblrsmembership_payment()
                    {
                        trans_id = rec_trans.id,
                        user_id = rec_trans.user_id,
                        user_email = rec_trans.user_email,
                        user_data = rec_trans.user_data,
                        type = rec_trans.type,
                        @params = rec_trans.@params,
                        ip = rec_trans.ip,
                        date = rec_trans.date,
                        price = rec_trans.price,
                        coupon = rec_trans.coupon,
                        currency = rec_trans.currency,
                        hash = rec_trans.hash,
                        custom = rec_trans.custom,
                        gateway = rec_trans.gateway,
                        status = rec_trans.status,
                        response_log = rec_trans.response_log,
                        name = rec_trans.name,
                        adresse = rec_trans.adresse,
                        postnr = rec_trans.postnr,
                        bynavn = rec_trans.bynavn,
                        memberid = rec_trans.memberid,
                        membership_id = rec_trans.membership_id,
                        subscriber_id = rec_trans.subscriber_id,
                        membership_start = rec_trans.fradato,
                        membership_end = rec_trans.tildato
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

                    p_dbData3060.tblrsmembership_payments.InsertOnSubmit(rec_payment);
                    wantalbetalinger++;
                }
                p_dbData3060.SubmitChanges();
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
                message.Bcc.Add(new MailboxAddress("Henrik Bo Larsen", "formand@puls3060.dk"));
                message.Bcc.Add(new MailboxAddress("Morten Wiberg", "mw@puls3060.dk"));
            }            
            message.Subject = subject;
#endif

            message.From.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
            using (var smtp_client = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp_client.Connect("smtp.gigahost.dk", 587, false);
                smtp_client.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp_client.Authenticate("regnskab@puls3060.dk", "1234West+");

                using (var imap_client = new ImapClient())
                {
                    imap_client.Connect("imap.gigahost.dk", 993, true);
                    imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                    imap_client.Authenticate("regnskab@puls3060.dk", "1234West+");

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
                imap_client.Authenticate("regnskab@puls3060.dk", "1234West+");
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

    public class recKont
    {
        public int Nr { get; set; }
        public string Navn { get; set; }
        public DateTime betalingsdato { get; set; }
        public decimal advisbelob { get; set; }
        public DateTime fradato { get; set; }
        public DateTime tildato { get; set; }
        public bool indmeldelse { get; set; }
        public bool tilmeldtpbs { get; set; }

    }
    public class MemKont : List<recKont>
    {

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
