using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPbs3060v2
{
    public class clsPbs603
    {
        private tblpbsforsendelse m_rec_pbsforsendelse;
        private tblpbsfilename m_rec_pbsfiles;
        private tblfrapbs m_rec_frapbs;
        private tblaftalelin m_rec_aftalelin;
        private tblindbetalingskort m_rec_indbetalingskort;

        public clsPbs603() { }

        public int aftaleoplysninger_fra_pbs(dbData3060 p_dbData3060)
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


            var qrypbsfiles = from h in p_dbData3060.tblpbsfilename
                              where h.pbsforsendelseid == null
                              join d in p_dbData3060.tblpbsfile on h.id equals d.pbsfilesid
                              where d.seqnr == 1 && d.data.Substring(16, 4) == "0603" && d.data.Substring(0, 2) == "BS"
                              select new
                              {
                                  h.id,
                                  h.path,
                                  h.filename,
                                  leverancetype = d.data.Substring(16, 4),
                                  delsystem = d.data.Substring(13, 3),
                              };

            int DebugCount = qrypbsfiles.Count();
            foreach (var rstpbsfiles in qrypbsfiles)
            {
                //try
                {
                    wpbsfilesid = rstpbsfiles.id;
                    AntalFiler++;
                    leverance = "";
                    sektion = "";
                    leverancespecifikation = "";


                    var qrypbsfile = from d in p_dbData3060.tblpbsfile
                                     where d.pbsfilesid == wpbsfilesid && d.data.Substring(0,6) != "PBCNET"
                                     orderby d.seqnr
                                     select d;

                    foreach (var rstpbsfile in qrypbsfile)
                    {
                        rec = rstpbsfile.data;
                        //  Bestem Leverance Type
                        if (rstpbsfile.seqnr == 1)
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
                                var antal = (from c in p_dbData3060.tblfrapbs
                                             where c.leverancespecifikation == leverancespecifikation
                                             select c).Count();
                                if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere"); }

                                wleveranceid = p_dbData3060.nextval("leveranceid"); 
                                m_rec_pbsforsendelse = new tblpbsforsendelse
                                {
                                    delsystem = "BS1",
                                    leverancetype = "0603",
                                    oprettetaf = "Bet",
                                    oprettet = DateTime.Now,
                                    leveranceid = wleveranceid
                                };
                                p_dbData3060.tblpbsforsendelse.Add(m_rec_pbsforsendelse);

                                m_rec_frapbs = new tblfrapbs
                                {
                                    delsystem = "BS1",
                                    leverancetype = "0603",
                                    leverancespecifikation = leverancespecifikation,
                                    leverancedannelsesdato = leverancedannelsesdato,
                                    udtrukket = DateTime.Now
                                };
                                m_rec_pbsforsendelse.tblfrapbs.Add(m_rec_frapbs);

                                m_rec_pbsfiles = (from c in p_dbData3060.tblpbsfilename
                                                  where c.id == rstpbsfiles.id
                                                  select c).First();
                                m_rec_pbsforsendelse.tblpbsfilename.Add(m_rec_pbsfiles);
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
                                    readaftale042(p_dbData3060, sektion, "0230", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0210"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("244 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
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
                                    readaftale042(p_dbData3060, sektion, "0231", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0232"))
                                {  //  Aftale afmeldt af pengeinstitut
                                    //  BEHANDL: aftale afmeldt af pengeinstitut
                                    readaftale042(p_dbData3060, sektion, "0232", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0233"))
                                {  //  Aftaler afmeldt af kreditor
                                    //  BEHANDL: aftaler afmeldt af kreditor
                                    readaftale042(p_dbData3060, sektion, "0233", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0234"))
                                {  //  Aftaler afmeldt af betalingsservice
                                    //  BEHANDL: aftaler afmeldt af betalingsservice
                                    readaftale042(p_dbData3060, sektion, "0234", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0212"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("245 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
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
                                    throw new Exception("246 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
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
                                    throw new Exception("247 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
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
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0295"))
                                {  //  Oplysninger fra indbetalingskort
                                    //  BEHANDL: Oplysninger fra indbetalingskort
                                    readgirokort042(p_dbData3060, sektion, "0295", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0217"))
                                {  //  Sektion Slut
                                    //  BEHANDL: Sektion Slut
                                    sektion = "";
                                }
                                else
                                {
                                    throw new Exception("248 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                                };
                                // -******************************************************************************************************
                                // -******************************************************************************************************
                            }
                            else if (sektion == "0219")
                            {  //  Sektion 0219 Aktive aftaler om Elektronisk Indbetalingskort
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0219"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0230"))
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
                                    throw new Exception("249 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
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
                                throw new Exception("250 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                            };
                        }
                   
                        else
                        {
                            throw new Exception("251 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                        };

                    }

                }
                /*  
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
                */
            }
            if (dummy == 1) dummy = 2;
            p_dbData3060.SaveChanges();
            return AntalFiler;
        }

        public void readaftale042(dbData3060 p_dbData3060, string sektion, string transkode, string rec)
        {
            // --  pbssektionnr
            // --  pbstranskode
            // - transkode 0230, aktiv aftale
            // - transkode 0231, tilgang af ny betalingsaftale
            // - transkode 0232, aftale afmeldt af pengeinstitut
            // - transkode 0233, aftale afmeldt af kreditor
            // - transkode 0234, aftale afmeldt af betalingsservice
            m_rec_aftalelin = new tblaftalelin
            {
                pbssektionnr = sektion,
                pbstranskode = transkode
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
            m_rec_aftalelin.debitorkonto = rec.Substring(25, 15);

            //  debgrpnr
            m_rec_aftalelin.debgrpnr = rec.Substring(20, 5);

            //  aftalenr
            m_rec_aftalelin.aftalenr = int.Parse(rec.Substring(40, 9));

            //  aftalestartdato
            if (rec.Substring(49, 6) != "000000")
            {
                m_rec_aftalelin.aftalestartdato = DateTime.Parse("20" + rec.Substring(53, 2) + "-" + rec.Substring(51, 2) + "-" + rec.Substring(49, 2));
            }
            else
            {
                m_rec_aftalelin.aftalestartdato = null;
            };

            //  aftaleslutdato
            if (rec.Substring(55, 6) != "000000")
            {
                m_rec_aftalelin.aftaleslutdato = DateTime.Parse("20" + rec.Substring(59, 2) + "-" + rec.Substring(57, 2) + "-" + rec.Substring(55, 2));
            }
            else
            {
                m_rec_aftalelin.aftaleslutdato = null;
            };

            if ((from h in p_dbData3060.tblMedlem where h.Nr == m_rec_aftalelin.Nr select h).Count() == 1)
            {
                // Add tblaftalelin
                m_rec_frapbs.tblaftalelin.Add(m_rec_aftalelin);
            }
        }

        public void readgirokort042(dbData3060 p_dbData3060, string sektion, string transkode, string rec)
        {
            // --  pbssektionnr
            // --  pbstranskode

            decimal belobmun;
            int belob;

            m_rec_indbetalingskort = new tblindbetalingskort
            {
                pbssektionnr = sektion,
                pbstranskode = transkode
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
            m_rec_indbetalingskort.debitorkonto = rec.Substring(25, 15);

            //  debgrpnr
            m_rec_indbetalingskort.debgrpnr = rec.Substring(20, 5);

            //  Kortartkode
            m_rec_indbetalingskort.kortartkode = rec.Substring(40, 2);

            //  FI-kreditor
            m_rec_indbetalingskort.fikreditornr  = rec.Substring(42, 8);

            //  Indbetalerident
            m_rec_indbetalingskort.indbetalerident  = rec.Substring(50, 19);

            //  dato
            if (rec.Substring(55, 6) != "000000")
            {
                m_rec_indbetalingskort.dato = DateTime.Parse("20" + rec.Substring(73, 2) + "-" + rec.Substring(71, 2) + "-" + rec.Substring(69, 2));
            }
            else
            {
                m_rec_indbetalingskort.dato = null;
            };

            //  Beløb
            belob = int.Parse(rec.Substring(75, 13));
            belobmun = ((decimal)belob) / 100;
            m_rec_indbetalingskort.belob = belobmun;

            //  Faknr
            m_rec_indbetalingskort.faknr = int.Parse(rec.Substring(88, 9));

            if ((from h in p_dbData3060.tblMedlem where h.Nr == m_rec_indbetalingskort.Nr select h).Count() == 1)
            {
                if ((from k in p_dbData3060.tblindbetalingskort where k.Nr == m_rec_indbetalingskort.Nr && k.indbetalerident == m_rec_indbetalingskort.indbetalerident select k).Count() == 0)
                {
                    // Add tblindbetalingskort
                    m_rec_frapbs.tblindbetalingskort.Add(m_rec_indbetalingskort);
                }
            }
        }
    }
}
