using System;
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

    class clsPbs602
    {
        private Tblpbsforsendelse m_rec_pbsforsendelse;
        private Tblpbsfiles m_rec_pbsfiles;
        private Tblpbsfile m_rec_pbsfile;
        private Tblfrapbs m_rec_frapbs;
        private Tblbet m_rec_bet;
        private Tblbetlin m_rec_betlin;

        public clsPbs602()
        {
        }


        public int ReadFraPbsFile()
        {
            string FraPBSFolderPath;
            DirectoryInfo fld;
            FileStream ts;
            string ln;

            Program.memPbsnetdir = null; //opret ny memPbsnetdir
            var rec_regnskab = Program.qryAktivRegnskab();
            FraPBSFolderPath = rec_regnskab.FraPBS;

            fld = new DirectoryInfo(FraPBSFolderPath);
            foreach (FileInfo f in fld.GetFiles())
            {
                recPbsnetdir rec = new recPbsnetdir
                {
                    Type = 8,
                    Path = f.Directory.Name,
                    Filename = f.Name,
                    Size = (int)f.Length,
                    Atime = f.LastAccessTime,
                    Mtime = f.LastWriteTime
                };
                Program.memPbsnetdir.Add(rec);
            }
            var leftqry_pbsnetdir =
                from h in Program.memPbsnetdir
                join d1 in Program.dbData3060.Tblpbsfiles on new { h.Path, h.Filename } equals new { d1.Path, d1.Filename } into details
                from d1 in details.DefaultIfEmpty(new Tblpbsfiles { Id = -1, Type = (int?)null, Path = null, Filename = null, Size = (int?)null, Atime = (DateTime?)null, Mtime = (DateTime?)null, Perm = null, Uid = (int?)null, Gid = (int?)null })
                where d1.Path == null && d1.Filename == null
                select h;

            int AntalFiler = leftqry_pbsnetdir.Count();
            if (leftqry_pbsnetdir.Count() > 0)
            {
                foreach (var rec_pbsnetdir in leftqry_pbsnetdir)
                {
                    Tblpbsfiles m_rec_pbsfiles = new Tblpbsfiles
                   {
                       Type = rec_pbsnetdir.Type,
                       Path = rec_pbsnetdir.Path,
                       Filename = rec_pbsnetdir.Filename,
                       Size = rec_pbsnetdir.Size,
                       Atime = rec_pbsnetdir.Atime,
                       Mtime = rec_pbsnetdir.Mtime,
                       Perm = rec_pbsnetdir.Perm,
                       Uid = rec_pbsnetdir.Uid,
                       Gid = rec_pbsnetdir.Gid
                   };
                    Program.dbData3060.Tblpbsfiles.InsertOnSubmit(m_rec_pbsfiles);

                    string varFromFile = FraPBSFolderPath + rec_pbsnetdir.Filename;
                    int seqnr = 0;
                    ts = new FileStream(varFromFile, FileMode.Open, FileAccess.Read, FileShare.None);
                    ln = null;
                    using (StreamReader sr = new StreamReader(ts, Encoding.Default))
                    {
                        while ((ln = sr.ReadLine()) != null)
                        {
                            if (((seqnr == 0) && !(ln.Substring(0, 6) == "PBCNET")) || (seqnr > 0)) { seqnr++; }
                            m_rec_pbsfile = new Tblpbsfile
                            {
                                Seqnr = seqnr,
                                Data = ln
                            };
                            m_rec_pbsfiles.Tblpbsfile.Add(m_rec_pbsfile);
                        }
                    }
                    m_rec_pbsfiles.Transmittime = DateTime.Now;
                    Program.dbData3060.SubmitChanges();
                }
            }
            Program.dbData3060.SubmitChanges();
            return AntalFiler;
        }

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
        public int betalinger_fra_pbs()
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

            var qrypbsfiles = from h in Program.dbData3060.Tblpbsfiles
                              where h.Pbsforsendelseid == null
                              join d in Program.dbData3060.Tblpbsfile on h.Id equals d.Pbsfilesid
                              where d.Seqnr == 1 && d.Data.Substring(16, 4) == "0602" && d.Data.Substring(0, 2) == "BS"
                              select new
                              {
                                  h.Id,
                                  h.Path,
                                  h.Filename,
                                  leverancetype = d.Data.Substring(16, 4),
                                  delsystem = d.Data.Substring(13, 3),
                              };

            foreach (var rstpbsfiles in qrypbsfiles)
            {
                try
                {
                    wpbsfilesid = rstpbsfiles.Id;
                    AntalFiler++;
                    leverancetype = "";
                    sektion = "";
                    leverancespecifikation = "";

                    var qrypbsfile = from d in Program.dbData3060.Tblpbsfile
                                     where d.Pbsfilesid == wpbsfilesid && d.Data.Substring(0, 6) != "PBCNET"
                                     orderby d.Seqnr
                                     select d;

                    foreach (var rstpbsfile in qrypbsfile)
                    {
                        rec = rstpbsfile.Data;
                        // -- Bestem Leverance Type
                        if (rstpbsfile.Seqnr == 1)
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
                                var antal = (from c in Program.dbData3060.Tblfrapbs
                                             where c.Leverancespecifikation == leverancespecifikation
                                             select c).Count();
                                if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere"); }

                                wleveranceid = clsPbs.nextval("leveranceid");
                                m_rec_pbsforsendelse = new Tblpbsforsendelse
                                {
                                    Delsystem = "BS1",
                                    Leverancetype = "0602",
                                    Oprettetaf = "Bet",
                                    Oprettet = DateTime.Now,
                                    Leveranceid = wleveranceid
                                };
                                Program.dbData3060.Tblpbsforsendelse.InsertOnSubmit(m_rec_pbsforsendelse);

                                m_rec_frapbs = new Tblfrapbs
                                {
                                    Delsystem = "BS1",
                                    Leverancetype = "0602",
                                    Leverancespecifikation = leverancespecifikation,
                                    Leverancedannelsesdato = leverancedannelsesdato,
                                    Udtrukket = DateTime.Now
                                };
                                m_rec_pbsforsendelse.Tblfrapbs.Add(m_rec_frapbs);

                                m_rec_pbsfiles = (from c in Program.dbData3060.Tblpbsfiles
                                                  where c.Id == rstpbsfiles.Id
                                                  select c).First();
                                m_rec_pbsforsendelse.Tblpbsfiles.Add(m_rec_pbsfiles);
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
                                    throw new Exception("244 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
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
                                    throw new Exception("245 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
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
                                throw new Exception("246 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                            }
                        }
                        else
                        {
                            throw new Exception("247 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                        }
                    }  // slut rstpbsfile

                    // -- Update indbetalingsbelob
                    foreach (Tblbet rec_bet in m_rec_frapbs.Tblbet)
                    {
                        var SumIndbetalingsbelob = (
                            from c in rec_bet.Tblbetlin
                            group c by c.Betid into g
                            select new { Betid = g.Key, SumIndbetalingsbelob = g.Sum(c => c.Indbetalingsbelob) }
                            ).First().SumIndbetalingsbelob;

                        rec_bet.Indbetalingsbelob = SumIndbetalingsbelob;
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
            Program.dbData3060.SubmitChanges();
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
            m_rec_betlin = new Tblbetlin
            {
                Pbssektionnr = sektion,
                Pbstranskode = transkode
            };

            // --  debitorkonto
            if ((sektion == "0211"))
            {
                m_rec_betlin.Nr = int.Parse(rec.Substring(33, 7));
                m_rec_betlin.Debitorkonto = rec.Substring(25, 15);
            }
            else if ((sektion == "0215"))
            {
                m_rec_betlin.Nr = int.Parse(rec.Substring(37, 7));
                m_rec_betlin.Debitorkonto = rec.Substring(29, 15);
            }
            else
            {
                m_rec_betlin.Nr = null;
                m_rec_betlin.Debitorkonto = null;
            }

            // --  aftalenr
            if ((sektion == "0211"))
            {
                m_rec_betlin.Aftalenr = int.Parse(rec.Substring(40, 9));
            }
            else
            {
                m_rec_betlin.Aftalenr = null;
            }

            // --  pbskortart
            if ((sektion == "0215"))
            {
                m_rec_betlin.Pbskortart = rec.Substring(44, 2);
            }
            else
            {
                m_rec_betlin.Pbskortart = null;
            }

            // --  pbsgebyrbelob
            if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(46, 1));
                belob = int.Parse(rec.Substring(47, 5));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.Pbsgebyrbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.Pbsgebyrbelob = belobmun;
                }
                else
                {
                    m_rec_betlin.Pbsgebyrbelob = 0;
                }
            }
            else
            {
                m_rec_betlin.Pbsgebyrbelob = 0;
            }

            // --  betalingsdato
            if ((sektion == "0211"))
            {
                if ((rec.Substring(49, 6) != "000000"))
                {
                    m_rec_betlin.Betalingsdato = DateTime.Parse("20"
                                + (rec.Substring(53, 2) + ("-"
                                + (rec.Substring(51, 2) + ("-" + rec.Substring(49, 2))))));
                }
                else
                {
                    m_rec_betlin.Betalingsdato = null;
                }
            }
            else if ((sektion == "0215"))
            {
                if ((rec.Substring(52, 6) != "000000"))
                {
                    m_rec_betlin.Betalingsdato = DateTime.Parse("20"
                                + (rec.Substring(56, 2) + ("-"
                                + (rec.Substring(54, 2) + ("-" + rec.Substring(52, 2))))));
                }
                else
                {
                    m_rec_betlin.Betalingsdato = null;
                }
            }
            else
            {
                m_rec_betlin.Betalingsdato = null;
            }

            // --  belob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(56, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.Belob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.Belob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    m_rec_betlin.Belob = (belobmun * -1);
                }
                else
                {
                    m_rec_betlin.Belob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(59, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.Belob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.Belob = belobmun;
                }
                else
                {
                    m_rec_betlin.Belob = null;
                }
            }
            else
            {
                m_rec_betlin.Belob = null;
            }

            // --  faknr
            if ((sektion == "0211"))
            {
                m_rec_betlin.Faknr = int.Parse("0" + rec.Substring(69, 9).Trim());
            }
            else if ((sektion == "0215"))
            {
                m_rec_betlin.Faknr = int.Parse("0" + rec.Substring(72, 9).Trim());
            }
            else
            {
                m_rec_betlin.Faknr = null;
            }

            // --  pbsarkivnr
            if ((sektion == "0215"))
            {
                m_rec_betlin.Pbsarkivnr = rec.Substring(81, 22);
            }
            else
            {
                m_rec_betlin.Pbsarkivnr = null;
            }

            // --  indbetalingsdato
            if ((rec.Substring(103, 6) != "000000"))
            {
                m_rec_betlin.Indbetalingsdato = DateTime.Parse("20"
                            + (rec.Substring(107, 2) + ("-"
                            + (rec.Substring(105, 2) + ("-" + rec.Substring(103, 2))))));
            }
            else
            {
                m_rec_betlin.Indbetalingsdato = null;
            }

            // --  bogforingsdato
            if ((rec.Substring(109, 6) != "000000"))
            {
                m_rec_betlin.Bogforingsdato = DateTime.Parse("20"
                            + (rec.Substring(113, 2) + ("-"
                            + (rec.Substring(111, 2) + ("-" + rec.Substring(109, 2))))));
            }
            else
            {
                m_rec_betlin.Bogforingsdato = null;
            }

            // --  indbetalingsbelob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.Indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.Indbetalingsbelob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    m_rec_betlin.Indbetalingsbelob = (belobmun * -1);
                }
                else
                {
                    m_rec_betlin.Indbetalingsbelob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    m_rec_betlin.Indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    m_rec_betlin.Indbetalingsbelob = belobmun;
                }
                else
                {
                    m_rec_betlin.Indbetalingsbelob = null;
                }
            }
            else
            {
                m_rec_betlin.Indbetalingsbelob = null;
            }


            // Find or Create tblbet record
            try
            {
                m_rec_bet = (from c in m_rec_frapbs.Tblbet
                             where c.Pbssektionnr == sektion
                                && c.Transkode == transkode
                                && c.Bogforingsdato == m_rec_betlin.Bogforingsdato
                             select c).First();

            }
            catch (System.InvalidOperationException)
            {
                m_rec_bet = new Tblbet
                {
                    Pbssektionnr = sektion,
                    Transkode = transkode,
                    Bogforingsdato = m_rec_betlin.Bogforingsdato
                };
                m_rec_frapbs.Tblbet.Add(m_rec_bet);
            }

            // Add tblbetlin
            m_rec_bet.Tblbetlin.Add(m_rec_betlin);
        }
    }
}
