using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPbs3060
{
    public class clsPbs686
    {
        private tblpbsforsendelse m_rec_pbsforsendelse;
        private tblpbsfilename m_rec_pbsfiles;
        private tblfrapb m_rec_frapbs;
        private tblindbetalingskort m_rec_indbetalingskort;

        public clsPbs686() { }

        public int aftaleoplysninger_fra_pbs(dbData3060DataContext p_dbData3060)
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


            var qrypbsfiles = from h in p_dbData3060.tblpbsfilenames
                              where h.pbsforsendelseid == null
                              join d in p_dbData3060.tblpbsfiles on h.id equals d.pbsfilesid
                              where d.seqnr == 1 && d.data.Substring(16, 4) == "0686" && d.data.Substring(0, 2) == "BS"
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


                    var qrypbsfile = from d in p_dbData3060.tblpbsfiles
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
                                leverancedannelsesdato = DateTime.Parse(rec.Substring(53, 4) + "-" + rec.Substring(51, 2) + "-" + rec.Substring(49, 2));

                            }
                            else
                            {
                                throw new Exception("241 - Første record er ikke en Leverance start record");
                            };

                            if (leverance == "0686")
                            {
                                // -- Leverance 0686
                                var antal = (from c in p_dbData3060.tblfrapbs
                                             where c.leverancespecifikation == leverancespecifikation
                                             select c).Count();
                                if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere"); }

                                wleveranceid = (int)(from r in p_dbData3060.nextval("leveranceid") select r.id).First();  
                                m_rec_pbsforsendelse = new tblpbsforsendelse
                                {
                                    delsystem = "BS1",
                                    leverancetype = "0686",
                                    oprettetaf = "Bet",
                                    oprettet = DateTime.Now,
                                    leveranceid = wleveranceid
                                };
                                p_dbData3060.tblpbsforsendelses.InsertOnSubmit(m_rec_pbsforsendelse);

                                m_rec_frapbs = new tblfrapb
                                {
                                    delsystem = "BS1",
                                    leverancetype = "0686",
                                    leverancespecifikation = leverancespecifikation,
                                    leverancedannelsesdato = leverancedannelsesdato,
                                    udtrukket = DateTime.Now
                                };
                                m_rec_pbsforsendelse.tblfrapbs.Add(m_rec_frapbs);

                                m_rec_pbsfiles = (from c in p_dbData3060.tblpbsfilenames
                                                  where c.id == rstpbsfiles.id
                                                  select c).First();
                                m_rec_pbsforsendelse.tblpbsfilenames.Add(m_rec_pbsfiles);
                            };
                        };

                        if (leverance == "0686")
                        { //  Leverance 0686

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
                            else if (sektion == "0195")
                            {  //  Sektion 0195 Indbetalingskort til e-Boks
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0195"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0274"))
                                {  //  Indbetalingskort til e-Boks
                                    //  BEHANDL: Indbetalingskort til e-Boks
                                    readindbetalingskort042(p_dbData3060, sektion, "0274", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0195"))
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
                            else if (sektion == "0196")
                            {  //  Sektion 0196 elektronisk indbetalingskort
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0196"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0274"))
                                {  //  elektronisk indbetalingskort
                                    //  BEHANDL: elektronisk indbetalingskort
                                    readindbetalingskort042(p_dbData3060, sektion, "0274", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0196"))
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
                            else if (sektion == "0197")
                            {  //  Sektion 0197 papirindbetalingskort
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0197"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0274"))
                                {  //  papirindbetalingskort
                                    //  BEHANDL: papirindbetalingskort
                                    readindbetalingskort042(p_dbData3060, sektion, "0274", rec);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0197"))
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
                            else if (sektion == "0198")
                            {  //  Sektion 0198 ej placerbare
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0198"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0274"))
                                {  //  ej placerbare
                                    //  BEHANDL: ej placerbare
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0198"))
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
                            else if (sektion == "0199")
                            {  //  Sektion 0199 uanbringelig
                                if ((rec.Substring(0, 5) == "BS012") && (rec.Substring(13, 4) == "0199"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") && (rec.Substring(13, 4) == "0275"))
                                {  //  uanbringelig
                                    //  BEHANDL: uanbringelig
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS092") && (rec.Substring(13, 4) == "0199"))
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
                            else if (rec.Substring(0, 5) == "BS992")
                            {  //  Leverance slut
                                //  BEHANDL: Leverance Slut
                                leverance = "";
                            }
                            else
                            {
                                throw new Exception("249 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
                            };
                        }
                   
                        else
                        {
                            throw new Exception("250 - Rec# " + rstpbsfile.seqnr + " ukendt: " + rec);
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
                            AntalFiler--;
                            break;

                        default:
                            throw;
                    }
                }
                */
            }
            if (dummy == 1) dummy = 2;
            p_dbData3060.SubmitChanges();
            return AntalFiler;
        }

        public void readindbetalingskort042(dbData3060DataContext p_dbData3060, string sektion, string transkode, string rec)
        {
            // --  pbssektionnr
            // --  pbstranskode
            // ---  transkode 0195, indbetalingskort til e-Boks
            // ---  transkode 0196, elektronisk indbetalingskort i netbank
            // ---  transkode 0197, papirindbetalingskort

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
                m_rec_indbetalingskort.Nr = int.Parse(rec.Substring(35, 7));
            }
            catch
            {
                m_rec_indbetalingskort.Nr = 0;
            }
            
            //  debitorkonto
            m_rec_indbetalingskort.debitorkonto = rec.Substring(27, 15);

            //  debgrpnr
            m_rec_indbetalingskort.debgrpnr = rec.Substring(22, 5);

            //  Kortartkode
            m_rec_indbetalingskort.kortartkode = rec.Substring(128, 2);

            //  FI-kreditor
            m_rec_indbetalingskort.fikreditornr  = rec.Substring(130, 8);

            //  Indbetalerident
            m_rec_indbetalingskort.indbetalerident  = "000" + rec.Substring(82, 16); //Tidligere 19 lang i 217 record

            //  dato
            if (rec.Substring(51, 8) != "00000000")
            {
                m_rec_indbetalingskort.dato = DateTime.Parse(rec.Substring(55, 4) + "-" + rec.Substring(53, 2) + "-" + rec.Substring(51, 2));
            }
            else
            {
                m_rec_indbetalingskort.dato = null;
            };

            //  Beløb
            belob = int.Parse(rec.Substring(60, 13));
            belobmun = ((decimal)belob) / 100;
            m_rec_indbetalingskort.belob = belobmun;

            //  Faknr
            m_rec_indbetalingskort.faknr = int.Parse(rec.Substring(73, 9));

            //  Netbank regnr
            if (transkode == "0196")
                m_rec_indbetalingskort.regnr = rec.Substring(138, 4);

            if ((from h in p_dbData3060.tblMedlems where h.Nr == m_rec_indbetalingskort.Nr select h).Count() == 1)
            {
                if ((from k in p_dbData3060.tblindbetalingskorts where k.Nr == m_rec_indbetalingskort.Nr && k.indbetalerident == m_rec_indbetalingskort.indbetalerident select k).Count() == 0)
                {
                    // Add tblindbetalingskort
                    m_rec_frapbs.tblindbetalingskorts.Add(m_rec_indbetalingskort);
                }
            }
        }
    }
}
