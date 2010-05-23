using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    class clsPbs603
    {
        public int aftaleoplysninger_fra_pbs()
        {

            int dummy = 0;
            string rec;
            string leverance;
            string sektion;
            int wpbsfilesid;
            int wpbsforsendelseid;
            int wfrapbsid;
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
            foreach (var rstpbsfiles in qrypbsfiles)
            {
                try
                {
                    wpbsfilesid = rstpbsfiles.Id;
                    AntalFiler++;
                    leverance = "";
                    sektion = "";

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
                            }
                            else
                            {
                                throw new Exception("241 - Første record er ikke en Leverance start record");
                            };

                            if (leverance == "0603")
                            { //  Leverance 0603
                                wpbsforsendelseid = clsPbs.nextval("pbs.wpbsforsendelseid_id_seq");
                                wleveranceid = clsPbs.nextval("pbs.leveranceid");
                                //INSERT INTO pbs.tblpbsforsendelse (id, delsystem, leverancetype, oprettetaf, oprettet, leveranceid) values(wpbselseid,  "BS1","0603", "Aft", now(), wleveranceid);
                                wfrapbsid = clsPbs.nextval("pbs.tblfrapbs_id_seq");
                                //insert into pbs.tblfrapbs (id, delsystem, leverancetype, udtrukket, pbselseid) values(wfrapbsid, "BS1","0603",CURRENT_TIMESTAMP, wpbsforsendelseid);
                                //update pbs.tblpbsfiles set pbsforsendelseid = wpbsforsendelseid where id = wpbsfilesid;
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
                                    if (!((rec.Substring(0, 5) == "BS992") | (rec.Substring(0, 5) == "BS002")))
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
                                if ((rec.Substring(0, 5) == "BS012") & (rec.Substring(13, 4) == "0210"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") & (rec.Substring(13, 4) == "0230"))
                                {  //  Aktive aftaler
                                    //  BEHANDL: Aktive aftaler
                                    //select into rcd * FROM pbs.readaftale042(wfrapbsid, sektion, "0230", rec);
                                    //insert into pbs.tblaftalelin (frapbsid, pbssektionnr, pbstranskode, debitorkonto, debgrpnr, aftalenr, aftalestartdato, aftaleslutdato)
                                    //values (rcd.frapbsid, rcd.pbssektionnr, rcd.pbstranskode, rcd.debitorkonto, rcd.debgrpnr, rcd.aftalenr, rcd.aftalestartdato, rcd.aftaleslutdato);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") & (rec.Substring(13, 4) == "0210"))
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
                                if ((rec.Substring(0, 5) == "BS012") & (rec.Substring(13, 4) == "0212"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") & (rec.Substring(13, 4) == "0231"))
                                {  //  Tilgang af nye betalingsaftaler
                                    //  BEHANDL: Tilgang af nye betalingsaftaler
                                    //select into rcd * FROM pbs.readaftale042(wfrapbsid, sektion, "0231", rec);
                                    //insert into pbs.tblaftalelin (frapbsid, pbssektionnr, pbstranskode, debitorkonto, debgrpnr, aftalenr, aftalestartdato, aftaleslutdato)
                                    //values (rcd.frapbsid, rcd.pbssektionnr, rcd.pbstranskode, rcd.debitorkonto, rcd.debgrpnr, rcd.aftalenr, rcd.aftalestartdato, rcd.aftaleslutdato);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") & (rec.Substring(13, 4) == "0232"))
                                {  //  Aftale afmeldt af pengeinstitut
                                    //  BEHANDL: aftale afmeldt af pengeinstitut
                                    //select into rcd * FROM pbs.readaftale042(wfrapbsid, sektion, "0232", rec);
                                    //insert into pbs.tblaftalelin (frapbsid, pbssektionnr, pbstranskode, debitorkonto, debgrpnr, aftalenr, aftalestartdato, aftaleslutdato) 
                                    //values (rcd.frapbsid, rcd.pbssektionnr, rcd.pbstranskode, rcd.debitorkonto, rcd.debgrpnr, rcd.aftalenr, rcd.aftalestartdato, rcd.aftaleslutdato);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") & (rec.Substring(13, 4) == "0233"))
                                {  //  Aftaler afmeldt af kreditor
                                    //  BEHANDL: aftaler afmeldt af kreditor
                                    //select into rcd * FROM pbs.readaftale042(wfrapbsid, sektion, "0233", rec);
                                    //insert into pbs.tblaftalelin (frapbsid, pbssektionnr, pbstranskode, debitorkonto, debgrpnr, aftalenr, aftalestartdato, aftaleslutdato)
                                    //values (rcd.frapbsid, rcd.pbssektionnr, rcd.pbstranskode, rcd.debitorkonto, rcd.debgrpnr, rcd.aftalenr, rcd.aftalestartdato, rcd.aftaleslutdato);

                                }
                                else if ((rec.Substring(0, 5) == "BS042") & (rec.Substring(13, 4) == "0234"))
                                {  //  Aftaler afmeldt af betalingsservice
                                    //  BEHANDL: aftaler afmeldt af betalingsservice
                                    //select into rcd * FROM pbs.readaftale042(wfrapbsid, sektion, "0234", rec);
                                    //insert into pbs.tblaftalelin (frapbsid, pbssektionnr, pbstranskode, debitorkonto, debgrpnr, aftalenr, aftalestartdato, aftaleslutdato)
                                    //values (rcd.frapbsid, rcd.pbssektionnr, rcd.pbstranskode, rcd.debitorkonto, rcd.debgrpnr, rcd.aftalenr, rcd.aftalestartdato, rcd.aftaleslutdato);

                                }
                                else if ((rec.Substring(0, 5) == "BS092") & (rec.Substring(13, 4) == "0212"))
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
                                if ((rec.Substring(0, 5) == "BS012") & (rec.Substring(13, 4) == "0214"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") & (rec.Substring(13, 4) == "0235"))
                                {  //  Forfald automatisk betaling
                                    //  BEHANDL: Forfald automatisk betaling
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS042") & (rec.Substring(13, 4) == "0295"))
                                {  //  Forfald manuel betaling
                                    //  BEHANDL: Forfald manuel betaling
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS092") & (rec.Substring(13, 4) == "0214"))
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
                                if ((rec.Substring(0, 5) == "BS012") & (rec.Substring(14 - 1, 4) == "0215"))
                                {  //  Sektion Start
                                    //  BEHANDL: Sektion Start
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS022") & (rec.Substring(13, 4) == "0240"))
                                {  //  Navn/adresse på debitor
                                    //  BEHANDL: Navn/adresse på debitor
                                    dummy = 1;

                                }
                                else if ((rec.Substring(0, 5) == "BS092") & (rec.Substring(14 - 1, 4) == "0215"))
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
                            else if (rec.Substring(0, 5) == "BS992")
                            {  //  Leverance slut
                                //  BEHANDL: Leverance Slut
                                leverance = "";
                            }
                            else
                            {
                                throw new Exception("248 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
                            };
                        }
                        else
                        {
                            throw new Exception("249 - Rec# " + rstpbsfile.Seqnr + " ukendt: " + rec);
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
                            AntalFiler--;
                            break;

                        default:
                            throw;
                    }
                }

            }

            return AntalFiler;
        }
    }
}
