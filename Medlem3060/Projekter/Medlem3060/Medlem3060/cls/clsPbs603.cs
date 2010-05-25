using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    class clsPbs603
    {
        private Tblpbsforsendelse m_rec_pbsforsendelse;
        private Tblpbsfiles m_rec_pbsfiles;
        private Tblfrapbs m_rec_frapbs;
        private Tblaftalelin m_rec_aftalelin;
        private Tblindbetalingskort m_rec_indbetalingskort;

        public clsPbs603()
        {
        
        }
        
        public int aftaleoplysninger_fra_pbs()
        {

            int dummy = 0;
            string rec;
            string leverance;
            string leverancespecifikation;
            DateTime leverancedannelsesdato;
            string sektion;
            int wpbsfilesid;
            int wleveranceid;
            int AntalFiler = 0;


            var qrypbsfiles = from h in Program.dbData3060.Tblpbsfiles
                              where h.Pbsforsendelseid == null
                              join d in Program.dbData3060.Tblpbsfile on h.Id equals d.Pbsfilesid
                              where d.Seqnr == 1 && d.Data.Substring(16, 4) == "0603" && d.Data.Substring(0, 2) == "BS"
                              select new
                              {
                                  h.Id,
                                  h.Path,
                                  h.Filename,
                                  leverancetype = d.Data.Substring(16, 4),
                                  delsystem = d.Data.Substring(13, 3),
                              };

            int DebugCount = qrypbsfiles.Count();
            foreach (var rstpbsfiles in qrypbsfiles)
            {
                try
                {
                    wpbsfilesid = rstpbsfiles.Id;
                    AntalFiler++;
                    leverance = "";
                    sektion = "";
                    leverancespecifikation = "";


                    var qrypbsfile = from d in Program.dbData3060.Tblpbsfile
                                     where d.Pbsfilesid == wpbsfilesid && d.Seqnr > 0
                                     orderby d.Seqnr
                                     select d;

                    foreach (var rstpbsfile in qrypbsfile)
                    {
                        rec = rstpbsfile.Data;
                        //  Bestem Leverance Type
                        if (rstpbsfile.Seqnr == 1)
                        {
                            if (rec.Substring(0, 5) == "BS002")
                            {  //  Leverance Start
                                leverance = rec.Substring(16, 4);
                                leverancespecifikation = rec.Substring(20, 10);
                                leverancedannelsesdato = DateTime.Parse("20" + rec.Substring(53, 2) + "-" + rec.Substring(51, 2) + "-" + rec.Substring(49, 2));

                            }
                            else
                            {
                                throw new Exception("241 - Første record er ikke en Leverance start record");
                            };

                            if (leverance == "0603")
                            {
                                // -- Leverance 0603
                                var antal = (from c in Program.dbData3060.Tblfrapbs
                                             where c.Leverancespecifikation == leverancespecifikation
                                             select c).Count();
                                if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere"); }

                                wleveranceid = clsPbs.nextval("leveranceid");
                                m_rec_pbsforsendelse = new Tblpbsforsendelse
                                {
                                    Delsystem = "BS1",
                                    Leverancetype = "0603",
                                    Oprettetaf = "Bet",
                                    Oprettet = DateTime.Now,
                                    Leveranceid = wleveranceid
                                };
                                Program.dbData3060.Tblpbsforsendelse.InsertOnSubmit(m_rec_pbsforsendelse);

                                m_rec_frapbs = new Tblfrapbs
                                {
                                    Delsystem = "BS1",
                                    Leverancetype = "0603",
                                    Leverancespecifikation = leverancespecifikation,
                                    Leverancedannelsesdato = leverancedannelsesdato,
                                    Udtrukket = DateTime.Now
                                };
                                m_rec_pbsforsendelse.Tblfrapbs.Add(m_rec_frapbs);

                                m_rec_pbsfiles = (from c in Program.dbData3060.Tblpbsfiles
                                                  where c.Id == rstpbsfiles.Id
                                                  select c).First();
                                m_rec_pbsforsendelse.Tblpbsfiles.Add(m_rec_pbsfiles);
                            };
                        };

                        if (leverance == "0603")
                        { //  Leverance 0603

                            //  Bestem Sektions Type
                            if (sektion == "")
                            {
                                if (rec.Substring(0, 5) == "BS012")
                                {  //  Sektion Start
                                    sektion = rec.Substring(13, 4);
                                }
                                else
                                {
                                    if (!((rec.Substring(0, 5) == "BS992") || (rec.Substring(0, 5) == "BS002")))
                                    {
                                        throw new Exception("243 - Første record er ikke en Sektions start record");
                                    };
                                };
                            };

                            if (rec.Substring(0, 5) == "BS002")
                            {  //  Leverance start
                                //  BEHANDL: Leverance start
                                dummy = 1;
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (sektion == "0210")
                            {  //  Sektion 0210 Aktive aftaler
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0210"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0230"))
                                {  //  Aktive aftaler
                                    //  BEHANDL: Aktive aftaler
                                    readaftale042(sektion, "0230", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0210"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("244 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                                };
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (sektion == "0212")
                            {  //  Sektion 0212 Til- og afgang af betalingsaftaler
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0212"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0231"))
                                {  //  Tilgang af nye betalingsaftaler
                                    //  BEHANDL: Tilgang af nye betalingsaftaler
                                    readaftale042( sektion, "0231", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0232"))
                                {  //  Aftale afmeldt af pengeinstitut
                                    //  BEHANDL: aftale afmeldt af pengeinstitut
                                    readaftale042(sektion, "0232", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0233"))
                                {  //  Aftaler afmeldt af kreditor
                                    //  BEHANDL: aftaler afmeldt af kreditor
                                    readaftale042(sektion, "0233", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0234"))
                                {  //  Aftaler afmeldt af betalingsservice
                                    //  BEHANDL: aftaler afmeldt af betalingsservice
                                    readaftale042(sektion, "0234", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0212"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("245 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                                };
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (sektion == "0214")
                            {  //  Sektion 0214 Forfaldsoplysninger
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0214"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0235"))
                                {  //  Forfald automatisk betaling
                                    //  BEHANDL: Forfald automatisk betaling
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0295"))
                                {  //  Forfald manuel betaling
                                    //  BEHANDL: Forfald manuel betaling
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0214"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("246 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                                };
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (sektion == "0215")
                            {  //  Sektion 0215 Debitornavn/-adresse
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(14 - 1, 4) == "0215"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS022") && (rec.Substring(13, 4) == "0240"))
                                {  //  Navn/adresse på debitor
                                    //  BEHANDL: Navn/adresse på debitor
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(14 - 1, 4) == "0215"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("247 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                                };
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (sektion == "0217")
                            {  //  Sektion 0217 Oplysninger fra indbetalingskort
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0217"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS022") && (rec.Substring(13, 4) == "0295"))
                                {  //  Oplysninger fra indbetalingskort
                                    //  BEHANDL: Oplysninger fra indbetalingskort
                                    readgirokort042(sektion, "0295", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(14 - 1, 4) == "0217"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("248 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                                };
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (sektion == "0219")
                            {  //  Sektion 0217 Aktive aftaler om Elektronisk Indbetalingskort
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0219"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS022") && (rec.Substring(13, 4) == "0230"))
                                {  //  Aktiv aftale om Elektronisk Indbetalingskort
                                    //  BEHANDL: Aktiv aftale om Elektronisk Indbetalingskort
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(14 - 1, 4) == "0219"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("249 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                                };
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (rec.Substring(0, 5) == "BS992")
                            {  //  Leverance slut
                                //  BEHANDL: Leverance Slut
                                leverance = "";
                            }
                            else
                            {
                                throw new Exception("250 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                            };
                        }
                   
                        else
                        {
                            throw new Exception("251 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                        };

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
                        case "248":   //248 - Record ukendt
                        case "249":   //249 - Record ukendt
                        case "250":   //250 - Record ukendt
                        case "251":   //251 - Record ukendt
                            AntalFiler--;
                            break;

                        default:
                            throw;
                    }
                }

            }
            Program.dbData3060.SubmitChanges();
            return AntalFiler;
        }

        public void readaftale042(string sektion, string transkode, string rec)
        {
            // --  pbssektionnr
            // --  pbstranskode
            // - transkode 0230, aktiv aftale
            // - transkode 0231, tilgang af ny betalingsaftale
            // - transkode 0232, aftale afmeldt af pengeinstitut
            // - transkode 0233, aftale afmeldt af kreditor
            // - transkode 0234, aftale afmeldt af betalingsservice
            m_rec_aftalelin = new Tblaftalelin
            {
                Pbssektionnr = sektion,
                Pbstranskode = transkode
            };

            //  Medlem Nr
            try
            {
                m_rec_aftalelin.Nr = int.Parse(rec.Substring(33, 7));
            }
            catch
            {
                m_rec_aftalelin.Nr = 0;
            }
            //  debitorkonto
            m_rec_aftalelin.Debitorkonto = rec.Substring(25, 15);

            //  debgrpnr
            m_rec_aftalelin.Debgrpnr = rec.Substring(20, 5);

            //  aftalenr
            m_rec_aftalelin.Aftalenr = int.Parse(rec.Substring(40, 9));

            //  aftalestartdato
            if (rec.Substring(49, 6) != "000000")
            {
                m_rec_aftalelin.Aftalestartdato = DateTime.Parse("20" + rec.Substring(53, 2) + "-" + rec.Substring(51, 2) + "-" + rec.Substring(49, 2));
            }
            else
            {
                m_rec_aftalelin.Aftalestartdato = null;
            };

            //  aftaleslutdato
            if (rec.Substring(55, 6) != "000000")
            {
                m_rec_aftalelin.Aftaleslutdato = DateTime.Parse("20" + rec.Substring(59, 2) + "-" + rec.Substring(57, 2) + "-" + rec.Substring(55, 2));
            }
            else
            {
                m_rec_aftalelin.Aftaleslutdato = null;
            };

            if ((from h in Program.dbData3060.TblMedlem where h.Nr == m_rec_aftalelin.Nr select h).Count() == 1)
            {
                // Add tblaftalelin
                m_rec_frapbs.Tblaftalelin.Add(m_rec_aftalelin);
            }
        }

        public void readgirokort042(string sektion, string transkode, string rec)
        {
            // --  pbssektionnr
            // --  pbstranskode

            m_rec_indbetalingskort = new Tblindbetalingskort
            {
                Pbssektionnr = sektion,
                Pbstranskode = transkode
            };

            //  Medlem Nr
            try
            {
                m_rec_indbetalingskort.Nr = int.Parse(rec.Substring(33, 7));
            }
            catch
            {
                m_rec_indbetalingskort.Nr = 0;
            }
            
            //  debitorkonto
            m_rec_aftalelin.Debitorkonto = rec.Substring(25, 15);

            //  debgrpnr
            m_rec_indbetalingskort.Debgrpnr = rec.Substring(20, 5);

            //  Kortartkode
            m_rec_indbetalingskort.Kortartkode = rec.Substring(40, 2);

            //  FI-kreditor
            m_rec_indbetalingskort.Fikreditornr  = rec.Substring(42, 8);

            //  Indbetalerident
            m_rec_indbetalingskort.Indbetalerident  = rec.Substring(50, 19);

            //  dato
            if (rec.Substring(55, 6) != "000000")
            {
                m_rec_indbetalingskort.Dato = DateTime.Parse("20" + rec.Substring(73, 2) + "-" + rec.Substring(71, 2) + "-" + rec.Substring(69, 2));
            }
            else
            {
                m_rec_indbetalingskort.Dato = null;
            };

            //  Beløb
            m_rec_indbetalingskort.Belob = 1;//rec.Substring(75,13); ?????????????????????????????

            //  Faknr
            m_rec_indbetalingskort.Faknr = int.Parse(rec.Substring(88, 9));

            if ((from h in Program.dbData3060.TblMedlem where h.Nr == m_rec_indbetalingskort.Nr select h).Count() == 1)
            {
                // Add tblindbetalingskort
                m_rec_frapbs.Tblindbetalingskort.Add(m_rec_indbetalingskort);
            }
        }
    }
}
