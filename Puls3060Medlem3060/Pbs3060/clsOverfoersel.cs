using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Net.Imap;

namespace nsPbs3060
{
    public class clsOverfoersel
    {
        public Tuple<int, int> kreditor_fakturer_os1(dbData3060DataContext p_dbData3060)
        {
            int lobnr;
            string wadvistekst;
            int wantalfakturaer;
            wantalfakturaer = 0;

            tbltilpb rec_tilpbs = new tbltilpb
            {
                delsystem = "OS1",
                leverancetype = null,
                udtrukket = DateTime.Now
            };
            p_dbData3060.tbltilpbs.InsertOnSubmit(rec_tilpbs);
            p_dbData3060.SubmitChanges();
            lobnr = rec_tilpbs.id;

            var rstmedlems = from l in p_dbData3060.tempBetalforslaglinies
                             join h in p_dbData3060.tempBetalforslags on l.Betalforslagid equals h.id
                             select new
                             {
                                 l.Nr,
                                 l.Navn,
                                 l.Kaldenavn,
                                 l.Email,
                                 h.betalingsdato,
                                 l.advisbelob,
                                 l.fakid,
                                 l.bankregnr,
                                 l.bankkontonr,
                                 l.faknr,
                             };

            foreach (var rstmedlem in rstmedlems)
            {

                wadvistekst = "Puls3060-" + rstmedlem.faknr;
                tbloverforsel rec_krdfak = new tbloverforsel
                {
                    Nr = rstmedlem.Nr,
                    Navn = rstmedlem.Navn,
                    Kaldenavn = rstmedlem.Kaldenavn,
                    Email = rstmedlem.Email,
                    advistekst = wadvistekst,
                    advisbelob = rstmedlem.advisbelob,
                    SFakID = rstmedlem.fakid,
                    SFaknr = rstmedlem.faknr,
                    bankregnr = rstmedlem.bankregnr,
                    bankkontonr = rstmedlem.bankkontonr,
                    betalingsdato = clsOverfoersel.bankdageplus(rstmedlem.betalingsdato, 0)
                };
                rec_tilpbs.tbloverforsels.Add(rec_krdfak);
                wantalfakturaer++;
            }
            p_dbData3060.SubmitChanges();
            return new Tuple<int, int>(wantalfakturaer, lobnr);
        }

        public void krdfaktura_overfoersel_action(dbData3060DataContext p_dbData3060, int lobnr)
        {
            string rec;
            int seq = 0;
            int wleveranceid;
            DateTime wdispositionsdato;
            int whour;
            int wbankdage;

            // Betalingsoplysninger
            int belobint;

            // Tællere
            int antalos5;        // Antal OS5: Antal foranstående OS5 records
            int belobos5;        // Beløb: Nettobeløb i OS5 records

            int antalsek;        // Antal sektioner i leverancen
            int antalos5tot;     // Antal OS5: Antal foranstående OS5 records
            int belobos5tot;     // Beløb: Nettobeløb i OS5 records

            whour = DateTime.Now.Hour;
            if (whour > 17) wbankdage = 3;
            else wbankdage = 2;
            wdispositionsdato = clsOverfoersel.bankdageplus(DateTime.Now, wbankdage);

            antalos5 = 0;
            belobos5 = 0;

            antalsek = 0;
            antalos5tot = 0;
            belobos5tot = 0;



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
                var antal = (from c in p_dbData3060.tbloverforsels
                             where c.tilpbsid == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in p_dbData3060.tbltilpbs
                          where c.id == lobnr
                          select c).First();
            if (rsttil.udtrukket == null) { rsttil.udtrukket = DateTime.Now; }
            if (rsttil.bilagdato == null) { rsttil.bilagdato = rsttil.udtrukket; }
            if (rsttil.delsystem == null) { rsttil.delsystem = "OS1"; }
            if (rsttil.leverancetype != null) { rsttil.leverancetype = null; }
            p_dbData3060.SubmitChanges();


            wleveranceid = (int)(from r in p_dbData3060.nextval("leveranceid") select r.id).First();
            tblpbsforsendelse rec_pbsforsendelse = new tblpbsforsendelse
            {
                delsystem = rsttil.delsystem,
                leverancetype = rsttil.leverancetype,
                oprettetaf = "Udb",
                oprettet = DateTime.Now,
                leveranceid = wleveranceid
            };
            p_dbData3060.tblpbsforsendelses.InsertOnSubmit(rec_pbsforsendelse);
            rec_pbsforsendelse.tbltilpbs.Add(rsttil);

            tblpbsfilename rec_pbsfiles = new tblpbsfilename();
            rec_pbsforsendelse.tblpbsfilenames.Add(rec_pbsfiles);

            tblkreditor krd = (from k in p_dbData3060.tblkreditors
                               where k.delsystem == rsttil.delsystem
                               select k).First();


            // -- Leverance Start - OS1
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
            rec = writeOS1(krd.datalevnr, wleveranceid);
            tblpbsfile rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
            rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

            var qrydeb = from h in p_dbData3060.tbloverforsels
                         where h.tilpbsid == lobnr
                         select h;

            // Start loop over betalinger i tbloverforsel
            int testantal = qrydeb.Count();
            foreach (tbloverforsel rec_overfoersel in qrydeb)
            {
                DateTime Betalingsdato = (rec_overfoersel.betalingsdato == null) ? wdispositionsdato : (DateTime)rec_overfoersel.betalingsdato;
                if (Betalingsdato < wdispositionsdato) Betalingsdato = wdispositionsdato;
                rec_overfoersel.betalingsdato = Betalingsdato; //opdater aktuel betalingsdato

                // Sektion start – (OS2)
                antalos5 = 0;
                belobos5 = 0;

                // -- OS2
                // - Betalingsdato  - Dispositionsdato
                // - krd.regnr      - Reg.nr.: Overførselsregistreringsnummer
                // - krd.kontonr    - Kontonr.: Overførselskontonummer
                // - krd.datalevnr  - Dataleverandørnr.: Dataleverandørens SE-nummer
                rec = writeOS2(Betalingsdato, krd.regnr, krd.kontonr, krd.datalevnr);
                rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

                ++antalsek;

                // -- Forfald betaling
                if (rec_overfoersel.advisbelob > 0)
                {
                    belobint = (int)(rec_overfoersel.advisbelob * ((decimal)100));
                    belobos5 = belobos5 + belobint;
                    belobos5tot = belobos5tot + belobint;
                }
                else
                {
                    belobint = 0;
                }

                // -- OS5
                // - debinfo.bankregnr   - Betalingsmodtager registreringsnummer
                // - debinfo.bankkontonr - Betalingsmodtager kontonummer
                // - belobint            - Beløb: Beløb i øre uden fortegn
                // - Betalingsdato       - Dispositionsdato
                // - krd.regnr           - Reg.nr.: Overførselsregistreringsnummer
                // - krd.kontonr         - Kontonr.: Overførselskontonummer
                // - deb.advistekst      - Tekst på Betalingsmodtagers kontoudtog
                // - deb.SFakID          - Ref til betalingsmodtager til eget brug
                rec = writeOS5(rec_overfoersel.bankregnr, rec_overfoersel.bankkontonr, belobint, Betalingsdato, krd.regnr, krd.kontonr, rec_overfoersel.advistekst, (int)rec_overfoersel.SFakID);
                rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

                antalos5++;
                antalos5tot++;


                // -- Sektion slut – (OS8)
                // - OS8
                // - antalos5          - Antal 042: Antal foranstående 042 records
                // - belobos5          - Beløb: Nettobeløb i 042 records
                // - Betalingsdato     - Dispositionsdato
                // - krd.regnr         - Reg.nr.: Overførselsregistreringsnummer
                // - krd.kontonr       - Kontonr.: Overførselskontonummer
                // - krd.datalevnr     - Dataleverandørnr.: Dataleverandørens SE-nummer
                rec = writeOS8(antalos5, belobos5, Betalingsdato, krd.regnr, krd.kontonr, krd.datalevnr);
                rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

            }

            // -- Leverance slut - (OS9)

            // --OS9 
            // - antalos5tot    - Antal 042: Antal foranstående 042 records
            // - belobos5tot    - Beløb: Nettobeløb i 042 records
            // - krd.datalevnr  - Dataleverandørnr.: Dataleverandørens SE-nummer
            rec = writeOS9(antalos5tot, belobos5tot, krd.datalevnr);
            rec_pbsfile = new tblpbsfile { seqnr = ++seq, data = rec };
            rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

            rsttil.udtrukket = DateTime.Now;
            rsttil.leverancespecifikation = wleveranceid.ToString();
            p_dbData3060.SubmitChanges();

        }

        public void overfoersel_mail_old(dbData3060DataContext p_dbData3060, int lobnr)
        {
            var antal = (from c in p_dbData3060.tbltilpbs
                         where c.id == lobnr
                         select c).Count();
            if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }

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


            var qrykrd = from k in p_dbData3060.tblMedlems
                         join h in p_dbData3060.tbloverforsels on k.Nr equals h.Nr
                         where h.tilpbsid == lobnr
                         select new
                         {
                             k.Nr,
                             k.Email,
                             k.Kaldenavn,
                             k.Navn,
                             h.betalingsdato,
                             h.advistekst,
                             h.advisbelob,
                             h.bankregnr,
                             h.bankkontonr,
                         };


            // Start loop over betalinger i tbloverforsel
            int testantal = qrykrd.Count();
            foreach (var krd in qrykrd)
            {
                //  Create a new email object
                MailMessage email = new MailMessage();

#if (DEBUG)
                email.Subject = "TEST Bankoverførsel fra Puls 3060: skal sendes til " + p_dbData3060.GetSysinfo("MAILTONAME") + " - " + p_dbData3060.GetSysinfo("MAILTOADDR");
                email.To.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILTOADDR"), p_dbData3060.GetSysinfo("MAILTONAME")));
#else
                email.Subject = "Bankoverførsel fra Puls 3060";
                if (krd.Email.Length > 0)
                {
                    email.To.Add(new MailAddress(krd.Email, krd.Navn));
                    email.Bcc.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILTOADDR"), p_dbData3060.GetSysinfo("MAILTONAME"))); 
                }
                else
                {
                    email.Subject += ": skal sendes til " + krd.Navn;
                    email.To.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILTOADDR"), p_dbData3060.GetSysinfo("MAILTONAME")));
                }
#endif
                email.Body = new clsInfotekst
                {
                    infotekst_id = 40,
                    numofcol = null,
                    kaldenavn = krd.Kaldenavn,
                    betalingsdato = krd.betalingsdato,
                    advisbelob = krd.advisbelob,
                    bankkonto = krd.bankregnr + "-" + krd.bankkontonr,
                    advistekst = krd.advistekst,
                    underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører"
                }.getinfotekst(p_dbData3060);

                email.From = new MailAddress(p_dbData3060.GetSysinfo("MAILFROM"));
                email.ReplyToList.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILREPLY")));

                smtp.Send(email);
            }
        }

        public void overfoersel_mail(dbData3060DataContext p_dbData3060, int lobnr)
        {
            var antal = (from c in p_dbData3060.tbltilpbs
                         where c.id == lobnr
                         select c).Count();
            if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }

            var qrykrd = from h in p_dbData3060.tbloverforsels
                         where h.tilpbsid == lobnr
                         select new
                         {
                             h.Nr,
                             h.Email,
                             h.Kaldenavn,
                             h.Navn,
                             h.betalingsdato,
                             h.advistekst,
                             h.advisbelob,
                             h.bankregnr,
                             h.bankkontonr,
                         };


            // Start loop over betalinger i tbloverforsel
            int testantal = qrykrd.Count();

            using (var smtp_client = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp_client.Connect("send.one.com", 465, true);
                smtp_client.AuthenticationMechanisms.Remove("XOAUTH2");
                smtp_client.Authenticate("regnskab@puls3060.dk", "1234West");

                using (var imap_client = new ImapClient())
                {
                    imap_client.Connect("imap.one.com", 993, true);
                    imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                    imap_client.Authenticate("regnskab@puls3060.dk", "1234West");

                    var SendtPost = imap_client.GetFolder("INBOX.Sendt post");
                    SendtPost.Open(FolderAccess.ReadWrite);


                    foreach (var krd in qrykrd)
                    {
                        //  Create a new email object
                        var message = new MimeMessage();
#if (DEBUG)
                        message.Subject = "TEST Bankoverførsel fra Puls 3060: skal sendes til " + "Regnskab Puls3060" + " - " + "regnskab@puls3060.dk";
                        message.To.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
#else
                    message.Subject = "Bankoverførsel fra Puls 3060";
                    if (krd.Email.Length > 0)
                    {
                        message.To.Add(new MailboxAddress(krd.Navn, krd.Email));
                    }
                    else
                    {
                        message.Subject += ": skal sendes til " + krd.Navn;
                        message.To.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
                    }
#endif
                        message.From.Add(new MailboxAddress("Regnskab Puls3060", "regnskab@puls3060.dk"));
                        message.Body = new TextPart("plain")
                        {
                            Text = new clsInfotekst
                                {
                                    infotekst_id = 40,
                                    numofcol = null,
                                    kaldenavn = krd.Kaldenavn,
                                    betalingsdato = krd.betalingsdato,
                                    advisbelob = krd.advisbelob,
                                    bankkonto = krd.bankregnr + "-" + krd.bankkontonr,
                                    advistekst = krd.advistekst,
                                    underskrift_navn = "\r\nMogens Hafsjold\r\nRegnskabsfører"
                                }.getinfotekst(p_dbData3060)
                        };
                        SendtPost.Append(message);
                        smtp_client.Send(message);
                    }
                    SendtPost.Close();
                    imap_client.Disconnect(true);
                }
                smtp_client.Disconnect(true);
            }
        }

        public static DateTime bankdageplus(DateTime pfradato, int antdage)
        {
            DateTime fradato = new DateTime(pfradato.Year, pfradato.Month, pfradato.Day);
            DateTime dato;
            int antal = 0;
            bool fridag;
            bool negativ;
            DateTime[] paaskedag = {   new DateTime(2009, 4, 12)
                                     , new DateTime(2010, 4, 4) 
                                     , new DateTime(2011, 4, 24) 
                                     , new DateTime(2012, 4, 8) 
                                     , new DateTime(2013, 3, 31) 
                                     , new DateTime(2014, 4, 20) 
                                     , new DateTime(2015, 4, 5) 
                                     , new DateTime(2016, 3, 27) 
                                     , new DateTime(2017, 4, 16) 
                                     , new DateTime(2018, 4, 1) 
                                     , new DateTime(2019, 4, 21) 
                                     , new DateTime(2020, 4, 12) 
                                   };
            if (antdage < 0)
            {
                negativ = true;
                dato = fradato.AddDays(1);
            }
            else
            {
                negativ = false;
                dato = fradato.AddDays(-1);
            }

            while (antal <= Math.Abs(antdage))
            {
                if (negativ) dato = dato.AddDays(-1);
                else dato = dato.AddDays(1);

                if (dato.DayOfWeek == DayOfWeek.Saturday) fridag = true; //lørdag
                else if (dato.DayOfWeek == DayOfWeek.Sunday) fridag = true; //søndag
                else if ((dato.Month == 1) && (dato.Day == 1)) fridag = true; //1. nytårsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(3))) fridag = true; //skærtorsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(2))) fridag = true; //langfredag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-1))) fridag = true; //2. påskedag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-26))) fridag = true; //st. bededag (fredag)
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-39))) fridag = true; //kristi himmelfartsdag (torsdag)
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-40))) fridag = true; //fredag efter kristi himmelfartsdag
                else if ((from p in paaskedag select p).Contains(dato.AddDays(-50))) fridag = true; //2. pinsedag
                else if ((dato.Month == 6) && (dato.Day == 5)) fridag = true; //grundlovsdag
                else if ((dato.Month == 12) && (dato.Day == 24)) fridag = true; //juleaften
                else if ((dato.Month == 12) && (dato.Day == 25)) fridag = true; //1. juledag
                else if ((dato.Month == 12) && (dato.Day == 26)) fridag = true; //2. juledag
                else if ((dato.Month == 12) && (dato.Day == 31)) fridag = true; //nytårsaftens dag
                else fridag = false;
                if (!fridag) ++antal;
            }

            return dato;
        }

        private string writeOS1(string datalevnr, int levident)
        {
            string rec = null;
            string kontroltal = "000000000";

            rec += "OS121PBS-OVERFØRSEL";
            rec += lpad(kontroltal, 9, '?');				// Kontroltal for dataleverandør
            rec += lpad(levident, 20, '0');					// Leveranceidentifikation
            rec += rpad("", 3, '0');                        // Filler
            rec += lpad(datalevnr, 8, '?');					// Dataleverandørens CVR-nummer
            rec += rpad("", 1, '0');                        // Leverancekvitering
            rec += rpad("", 20, '0');                       // Filler
            return rec;
        }

        private string writeOS2(DateTime dispdato, string regnr, string kontonr, string datalevnr)
        {
            string rec = null;

            rec += "OS2";                                                   // Recordtype
            rec += lpad("80", 2, '0');                                      // Overførselstype = 80
            rec += rpad("", 26, '0');                                       // Filler
            rec += lpad(String.Format("{0:ddMMyy}", dispdato), 6, '?');     // Dispositionsdato
            rec += lpad(regnr, 4, '0');                                     // Reg.nr.: Betalingsafsender registreringsnummer
            rec += lpad(kontonr, 10, '0');                                  // Kontonr.: Betalingsafsender kontonummer
            rec += lpad(datalevnr, 8, '?');					                // Dataleverandørens CVR-nummer
            rec += lpad(datalevnr, 8, '?');					                // Betalingsafsenders CVR-nummer
            rec += rpad("", 13, '0');                                       // Filler
            return rec;
        }

        private string writeOS5(string bankregnr, string bankkontonr, int belob, DateTime betaldato, string regnr, string kontonr, string advistekst, int modtager)
        {
            string rec = null;

            rec += "OS5";                                               // Recordtype
            rec += lpad("80", 2, '0');                                  // Overførselstype = 80
            rec += lpad(bankregnr, 4, '0');                             // Reg.nr.: Betalingsmodtager registreringsnummer
            rec += lpad(bankkontonr, 10, '0');                          // Kontonr.: Betalingsmodtager kontonummer
            rec += lpad(belob, 12, '0');						        // Beløb uden fortegn i øre
            rec += lpad(String.Format("{0:ddMMyy}", betaldato), 6, '?');// Dispositionsdato
            rec += lpad(regnr, 4, '0');                                 // Reg.nr.: Betalingsafsender registreringsnummer
            rec += lpad(kontonr, 10, '0');                              // Kontonr.: Betalingsafsender kontonummer
            rec += rpad(advistekst, 20, ' ');				            // Tekst på Betalingsmodtagers kontoudtog
            rec += lpad(modtager, 13, '0');					            // Ref til betalingsmodtager til eget brug
            rec += rpad("", 44, '0');                                   // Filler
            return rec;
        }

        private string writeOS8(int antal1, int belob1, DateTime dispdato, string regnr, string kontonr, string datalevnr)
        {
            string rec = null;

            rec += "OS8";
            rec += lpad("80", 2, '0');                                  // Overførselstype = 80
            rec += rpad("", 4, '0');                                    // Filler
            rec += lpad(antal1, 10, '0');					            // Antal overførsler i denne sektion
            rec += lpad(belob1, 12, '0');					            // Totalbeløb denne sektion
            rec += lpad(String.Format("{0:ddMMyy}", dispdato), 6, '?'); // Dispositionsdato
            rec += lpad(regnr, 4, '0');                                 // Reg.nr.: Betalingsafsender registreringsnummer
            rec += lpad(kontonr, 10, '0');                              // Kontonr.: Betalingsafsender kontonummer
            rec += lpad(datalevnr, 8, '?');					            // Dataleverandørens CVR-nummer
            rec += lpad(datalevnr, 8, '?');					            // Betalingsafsenders CVR-nummer
            rec += rpad("", 13, '0');                                   // Filler
            return rec;
        }

        private string writeOS9(int antal2, int belob2, string datalevnr)
        {
            string rec = null;

            rec += "OS929";
            rec += rpad("", 4, '0');                // Filler
            rec += lpad(antal2, 10, '0');			// Total antal overførsler
            rec += lpad(belob2, 12, '0');			// Total beløb
            rec += rpad("", 6, '0');                // Filler
            rec += rpad("", 14, '9');               // Filler
            rec += lpad(datalevnr, 8, '?');			// Betalingsafsenders CVR-nummer
            rec += rpad("", 21, '0');               // Filler
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
    }
}
