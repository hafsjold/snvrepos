﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace nsPuls3060
{
    public struct lintype
    {
        //string advistekst;
        //double advisbelob;
    }

    public struct infolintype
    {
        //long getinfotekst;
    }

    public enum datefmtType
    {
        fdmmddyyyy = 0,
        fdmmddyyyyhhmmss = 1,
    }

    class clsPbs601
    {
        private Tblpbsfiles m_rec_pbsfiles;

        public clsPbs601(){}

        public bool WriteTilPbsFile(int lobnr)
        {
            string TilPBSFolderPath;
            string TilPBSFilename;
            string varToFile;
            FileInfo f;
            FileStream ts;

            var rec_regnskab = Program.qryAktivRegnskab();
            TilPBSFolderPath = rec_regnskab.TilPBS;

            var qry_selectfiles =
                from h in Program.dbData3060.Tblpbsforsendelse
                join d1 in Program.dbData3060.Tblpbsfiles on h.Id equals d1.Pbsforsendelseid into details1
                from d1 in details1.DefaultIfEmpty()
                join d2 in Program.dbData3060.Tbltilpbs on h.Id equals d2.Pbsforsendelseid into details2
                from d2 in details2.DefaultIfEmpty()
                where d2.Id == lobnr && d1.Id != null && d1.Filename == null
                select new
                {
                    tilpbsid = d2.Id,
                    d2.Leverancespecifikation,
                    d2.Delsystem,
                    d2.Leverancetype,
                    d2.Bilagdato,
                    d2.Pbsforsendelseid,
                    d2.Udtrukket,
                    pbsfilesid = d1.Id
                };


            foreach (var rec_selecfiles in qry_selectfiles)
            {
                var qry_pbsfiles = from h in Program.dbData3060.Tblpbsfiles
                           where h.Id  == rec_selecfiles.pbsfilesid
                           select h;
                if (qry_pbsfiles.Count() > 0)
                {
                    m_rec_pbsfiles = qry_pbsfiles.First();
                    TilPBSFilename = "PBS" + rec_selecfiles.Leverancespecifikation + ".lst";
                    varToFile = TilPBSFolderPath + TilPBSFilename;
                    ts = new FileStream(varToFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                    
                    var qry_pbsfile =
                        from h in m_rec_pbsfiles.Tblpbsfile
                        orderby h.Seqnr
                        select h;
                    
                    using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
                    {
                        foreach (var rec_pbsfile in qry_pbsfile)
                        {
                            sr.WriteLine(rec_pbsfile.Data);
                        }
                    }
                    f = new FileInfo(varToFile);
                    m_rec_pbsfiles.Type = 8;
                    m_rec_pbsfiles.Path = f.Directory.Name;
                    m_rec_pbsfiles.Filename = f.Name;
                    m_rec_pbsfiles.Size = (int)f.Length;
                    m_rec_pbsfiles.Atime = f.LastAccessTime;
                    m_rec_pbsfiles.Mtime = f.LastWriteTime;
                }
            }
        return true;
        }
        
        public int kontingent_fakturer_bs1()
        {
            int lobnr;
            string wadvistekst;
            int wantalfakturaer;
            wantalfakturaer = 0;

            Tbltilpbs rec_tilpbs = new Tbltilpbs
            {
                Delsystem = "BS1",
                Leverancetype = "0601",
                Udtrukket = DateTime.Now
            };
            Program.dbData3060.Tbltilpbs.InsertOnSubmit(rec_tilpbs);
            lobnr = rec_tilpbs.Id;

            var rstmedlems = from h in Program.dbData3060.TempKontforslag
                             join l in Program.dbData3060.TempKontforslaglinie on h.Id equals l.Kontforslagid
                             join m in Program.dbData3060.TblMedlem on l.Nr equals m.Nr
                             join k in Program.karMedlemmer on l.Nr equals k.Nr 
                             select new
                             {
                                 m.Nr,
                                 k.Navn,
                                 h.Betalingsdato,
                                 l.Advisbelob,
                                 l.Fradato,
                                 h.Tildato
                             };

            foreach (var rstmedlem in rstmedlems)
            {

                wadvistekst = "Puls 3060 Medlemskontingent";
                wadvistekst += "\r\n" + "  for " + rstmedlem.Navn;
                wadvistekst += "\r\n" + "  perioden fra " + string.Format("{0:yyyy-MM-dd}", rstmedlem.Fradato) + " til " + string.Format("{0:yyyy-MM-dd}", rstmedlem.Tildato);
                wadvistekst += "\r\n" + "Besøg Puls 3060\"s hjemmeside på www.puls3060.dk";
                Tblfak rec_fak = new Tblfak
                {
                    Betalingsdato = rstmedlem.Betalingsdato,
                    Nr = rstmedlem.Nr,
                    Faknr = clsPbs.nextval("faknr"),
                    Advistekst = wadvistekst,
                    Advisbelob = rstmedlem.Advisbelob,
                    Infotekst = 0,
                    Bogfkonto = 1800,
                    Vnr = 1,
                    Fradato = rstmedlem.Fradato,
                    Tildato = rstmedlem.Tildato
                };
                rec_tilpbs.Tblfak.Add(rec_fak);
                wantalfakturaer++;
            }
            Program.dbData3060.SubmitChanges();

            if (wantalfakturaer > 0) { faktura_601_action(lobnr); }
            return wantalfakturaer;

        }
        public void faktura_601_action(int lobnr)
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
                var antal = (from c in Program.dbData3060.Tbltilpbs
                             where c.Id == lobnr
                             select c).Count();
                if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }
            }
            {
                var antal = (from c in Program.dbData3060.Tbltilpbs
                             where c.Id == lobnr && c.Pbsforsendelseid != null
                             select c).Count();
                if (antal > 0) { throw new Exception("102 - Pbsforsendelse for id: " + lobnr + " er allerede sendt"); }
            }
            {
                var antal = (from c in Program.dbData3060.Tblfak
                             where c.Tilpbsid == lobnr
                             select c).Count();
                // if (antal == 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
                // næste linie skal fjernes efter test
                if (antal != 0) { throw new Exception("103 - Der er ingen pbs transaktioner for tilpbsid: " + lobnr); }
            }

            var rsttil = (from c in Program.dbData3060.Tbltilpbs
                          where c.Id == lobnr
                          select c).First();
            if (rsttil.Udtrukket == null) { rsttil.Udtrukket = DateTime.Now; }
            if (rsttil.Bilagdato == null) { rsttil.Bilagdato = rsttil.Udtrukket; }
            if (rsttil.Delsystem == null) { rsttil.Delsystem = "BS1"; }
            if (rsttil.Leverancetype == null) { rsttil.Leverancetype = ""; }
            Program.dbData3060.SubmitChanges();

            wleveranceid = clsPbs.nextval("leveranceid");

            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
            {
                Delsystem = rsttil.Delsystem,
                Leverancetype = rsttil.Leverancetype,
                Oprettetaf = "Fak",
                Oprettet = DateTime.Now,
                Leveranceid = wleveranceid
            };
            Program.dbData3060.Tblpbsforsendelse.InsertOnSubmit(rec_pbsforsendelse);

            Tblpbsfiles rec_pbsfiles = new Tblpbsfiles();
            rec_pbsforsendelse.Tblpbsfiles.Add(rec_pbsfiles);

            var rstkrd = (from c in Program.dbData3060.Tblkreditor
                          where c.Delsystem == rsttil.Delsystem
                          select c).First();


            // -- Leverance Start - 0601 Betalingsoplysninger
            // - rstkrd.Datalevnr - Dataleverandørnr.: Dataleverandørens SE-nummer
            // - rsttil.Delsystem - Delsystem:  Dataleverandør delsystem
            // - "0601"           - Leverancetype: 0601 (Betalingsoplysninger)
            // - wleveranceid     - Leveranceidentifikation: Løbenummer efter eget valg
            // - rsttil!udtrukket - Dato: 000000 eller leverancens dannelsesdato
            rec = write002(rstkrd.Datalevnr, rsttil.Delsystem, "0601", wleveranceid.ToString(), (DateTime)rsttil.Udtrukket);
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
            rec = write012(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, rstkrd.Datalevnavn, (DateTime)rsttil.Udtrukket, rstkrd.Regnr, rstkrd.Kontonr, h_linie);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
            antalsek++;
            var rstdebs = from f in Program.dbData3060.Tblfak
                          where f.Tilpbsid == lobnr && f.Nr != null
                          join m in Program.dbData3060.TblMedlem on f.Nr equals m.Nr
                          join k in Program.karMedlemmer on f.Nr equals k.Nr 
                          orderby f.Nr
                          select new
                          {
                              f.Id,
                              m.Nr,
                              Kundenr = 32001610000000 + m.Nr,
                              k.Navn,
                              k.Adresse,
                              k.Postnr,
                              f.Faknr,
                              f.Betalingsdato,
                              f.Infotekst,
                              f.Tilpbsid,
                              f.Advistekst,
                              belob = f.Advisbelob
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
                rec = write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 1, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Navn);
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // -- Debitoradresse 1/adresse 2
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 2                - Recordnr.: 002
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Adresse   - Adresse 1: Adresselinie 1
                rec = write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 2, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Adresse);
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // -- Debitorpostnummer
                // - rstkrd.Sektionnr -
                // - rstkrd.Pbsnr     - PBS-nr.: Kreditors PBS-nummer
                // - "0240"           - Transkode: 0240 (Navn/adresse på debitor)
                // - 3                - Recordnr.: 003
                // - rstkrd.Debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
                // - rstdeb.Kundenr   - Kundenr.: Debitors kundenummer hos kreditor
                // - 0                - Aftalenr.: 000000000 eller 999999999
                // - rstdeb.Postnr    - Postnr.: Postnummer
                rec = write022(rstkrd.Sektionnr, rstkrd.Pbsnr, "0240", 9, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, rstdeb.Postnr.ToString());
                antal022++;
                antal022tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                // -- Forfald betaling
                if (rstdeb.belob > 0)
                {
                    fortegn = 1;
                    // -- Fortegnskode: 1 = træk
                    belobint = ((int)rstdeb.belob) * 100;
                    belob042 += belobint;
                    belob042tot += belobint;
                }
                else if (rstdeb.belob < 0)
                {
                    fortegn = 2;
                    // -- Fortegnskode: 2 = indsættelse
                    belobint = ((int)rstdeb.belob) * (-100);
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
                rec = write042(rstkrd.Sektionnr, rstkrd.Pbsnr, rstkrd.Transkodebetaling, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, (DateTime)rstdeb.Betalingsdato, fortegn, belobint, (int)rstdeb.Faknr);
                antal042++;
                antal042tot++;
                rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                string[] arradvis;
                int n;
                string[] splitchars = { "\r\n" };
                arradvis = rstdeb.Advistekst.Split(splitchars, StringSplitOptions.None);
                recnr = 0;
                for (n = arradvis.GetLowerBound(0); n <= arradvis.GetUpperBound(0); n++)
                {
                    if (n == arradvis.GetLowerBound(0))
                    {
                        recnr++;
                        advistekst = arradvis[n];
                        advisbelob = (int)rstdeb.belob;
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
                    rec = write052(rstkrd.Sektionnr, rstkrd.Pbsnr, "0241", recnr, rstkrd.Debgrpnr, rstdeb.Kundenr.ToString(), 0, advistekst, advisbelob, "", 0);
                    rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
                    rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);
                }
                // '  FOR infolin IN SELECT * FROM pbs.getinfotekst(rstdeb!id, rstdeb!infotekstid)
                // '  LOOP  '--pbs.getinfotekst
                // '    recnr = recnr + 1
                // '    '-- Tekst til advis
                // '    antal052 = antal052 + 1
                // '    antal052tot = antal052tot + 1
                // '    selector = True
                // '
                // '    '- rstkrd!sektionnr     -
                // '    '- rstkrd!pbsnr         - PBS-nr.: Kreditors PBS-nummer
                // '    '- "0241"               - Transkode: 0241 (Tekstlinie)
                // '    '- recnr                - Recordnr.: 001-999
                // '    '- rstkrd!debgrpnr      - Debitorgruppenr.: Debitorgruppenummer
                // '    '- rstdeb!kundenr       - Kundenr.: Debitors kundenummer hos kreditor
                // '    '- 0                    - Aftalenr.: 000000000 eller 999999999
                // '    '- infolin.getinfotekst - Advistekst 1: Tekstlinie på advis
                // '    '- 0.0                  - Advisbeløb 1: Beløb på advis
                // '    '- ""                   - Advistekst 2: Tekstlinie på advis
                // '    '- 0.0                  - Advisbeløb 2: Beløb på advis
                // '    rec = write052(IfNull(rstkrd!sektionnr), _
                // '              IfNull(rstkrd!pbsnr), _
                // '              "0241", _
                // '              recnr, _
                // '              IfNull(rstkrd!debgrpnr), _
                // '              IfNull(rstdeb!kundenr), _
                // '              0, _
                // '              IfNull(infolin.getinfotekst), _
                // '              0, _
                // '              "", _
                // '              0)
                // '    seq = seq + 1
                // '    rstfile.AddNew
                // '    rstfile!pbsfilesid = wpbsfilesid
                // '    rstfile!Seqnr = seq
                // '    rstfile!data = rec
                // '    rstfile.Update
                // '    ts.WriteLine rec
                // '  END LOOP  '--pbs.getinfotekst

            } // -- End rstdebs

            // -- Sektion slut - sektion 0112/117
            // - rstkrd!pbsnr     - PBS-nr.: Kreditors PBS-nummer
            // - rstkrd!sektionnr - Sektionsnr.: 0112/0117 (Betalinger)
            // - rstkrd!debgrpnr  - Debitorgruppenr.: Debitorgruppenummer
            // - antal042         - Antal 042: Antal foranstående 042 records
            // - belob042         - Beløb: Nettobeløb i 042 records
            // - antal052         - Antal 052: Antal foranstående 052 records
            // - antal022         - Antal 022: Antal foranstående 022 records
            rec = write092(rstkrd.Pbsnr, rstkrd.Sektionnr, rstkrd.Debgrpnr, antal042, belob042, antal052, antal022);
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
            rec = write992(rstkrd.Datalevnr, rstkrd.Delsystem, "0601", antalsek, antal042tot, belob042tot, antal052tot, antal022tot);
            rec_pbsfile = new Tblpbsfile { Seqnr = ++seq, Data = rec };
            rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

            rsttil.Udtrukket = DateTime.Now;
            rsttil.Leverancespecifikation = wleveranceid.ToString();
            Program.dbData3060.SubmitChanges();
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

        private string write042(string sektionnr, string pbsnr, string transkode, string debgrpnr, string medlemsnr, int aftalenr, System.DateTime betaldato, int fortegn, int belob, int faknr)
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

    }
}