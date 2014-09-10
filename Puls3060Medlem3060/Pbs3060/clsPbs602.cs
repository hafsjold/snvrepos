using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;

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
                m_rec_betlin.Nr = int.Parse(rec.Substring(33, 7));
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
                m_rec_betlin.faknr = int.Parse("0" + rec.Substring(69, 9).Trim());
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
    }
}
