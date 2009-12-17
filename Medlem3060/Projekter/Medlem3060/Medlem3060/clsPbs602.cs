using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsPuls3060
{

    public struct betlintype
    {
        public object betid;
        public object pbssektionnr;
        public object pbstranskode;
        public object Nr;
        public object faknr;
        public object debitorkonto;
        public object aftalenr;
        public object betalingsdato;
        public object belob;
        public object indbetalingsdato;
        public object bogforingsdato;
        public object indbetalingsbelob;
        public object pbskortart;
        public object pbsgebyrbelob;
        public object pbsarkivnr;
    }

    class clsPbs602
    {
          private DbData3060 dbData3060;

        private clsPbs602()
        {
        }
        public clsPbs602(DbData3060 pdbData3060)
        {
            dbData3060 = pdbData3060;
        }
        
        public void TestRead042()
        {
            int frapbsid;
            string sektion;
            string transkode;
            string rec;
            betlintype result;
            frapbsid = 1;
            sektion = "0211";
            transkode = "0236";
            rec = "BS0420398564402360000000100000000001231312345678910120310000000012755000000125                       " +
            "  1212031212030000000012755";
            result = read042(frapbsid, sektion, transkode, rec);
        }
        public int betalinger_fra_pbs()
        {
            string rec;
            string leverancetype;
            string leverancespecifikation;
            DateTime leverancedannelsesdato;
            string sektion;
            betlintype rcd;
            int wfrapbsid = 0;
            int wpbsfilesid;
            int wleveranceid;
            int wfilescount = 0;
            //  wpbsfilesid = 3450  //'--test test
            //  leverancetype = "0602"
            //  sektion = "0211"
            //  rec = "BS0420398564402360000000100000000001231312345678910120310000000012755000000125                         3112031112030000000012755"

            var lstpbsfiles = from h in dbData3060.Tblpbsfiles
                              join d in dbData3060.Tblpbsfile on h.Id equals d.Pbsfilesid
                              where d.Seqnr == 1 && d.Data.Substring(16, 4) == "0602" && d.Data.Substring(0, 2) == "BS"
                              select new
                              {
                                  h.Id,
                                  h.Path,
                                  h.Filename,
                                  leverancetype = d.Data.Substring(16, 4),
                                  delsystem = d.Data.Substring(13, 3),
                                  h.Pbsforsendelseid
                              };

            foreach (var rstpbsfiles in lstpbsfiles)
            {
                wpbsfilesid = rstpbsfiles.Id;
                wfilescount++;
                leverancetype = "";
                sektion = "";
                leverancespecifikation = "";

                var lstpbsfile = from d in dbData3060.Tblpbsfile
                                 where d.Pbsfilesid == wpbsfilesid && d.Seqnr > 0
                                 orderby d.Seqnr
                                 select d;

                foreach (var rstpbsfile in lstpbsfile)
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
                            var antal = (from c in dbData3060.Tblfrapbs
                                         where c.Leverancespecifikation == leverancespecifikation
                                         select c).Count();
                            if (antal > 0) { throw new Exception("242 - Leverance med pbsfilesid: " + wpbsfilesid + " og leverancespecifikation: " + leverancespecifikation + " er indlæst tidligere"); }

                            wleveranceid = clsPbs.nextval("leveranceid", dbData3060);
                            Tblpbsforsendelse rec_pbsforsendelse = new Tblpbsforsendelse
                            {
                                Delsystem = "BS1",
                                Leverancetype = "0602",
                                Oprettetaf = "Bet",
                                Oprettet = DateTime.Now,
                                Leveranceid = wleveranceid
                            };
                            dbData3060.Tblpbsforsendelse.InsertOnSubmit(rec_pbsforsendelse);

                            Tblfrapbs rec_frapbs = new Tblfrapbs
                            {
                                Delsystem = "BS1",
                                Leverancetype = "0602",
                                Leverancespecifikation = leverancespecifikation,
                                Leverancedannelsesdato = leverancedannelsesdato,
                                Udtrukket = DateTime.Now
                            };
                            rec_pbsforsendelse.Tblfrapbs.Add(rec_frapbs);

                            var rec_pbsfiles = (from c in dbData3060.Tblpbsfiles
                                                where c.Id == rstpbsfiles.Id
                                                select c).First();
                            rec_pbsforsendelse.Tblpbsfiles.Add(rec_pbsfiles);
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
                            else if (!((rec.Substring(0, 5) == "BS992")
                                        || (rec.Substring(0, 5) == "BS002")))
                            {
                                throw new Exception("243 - F?rste record er ikke en Sektions start record");
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
                                rcd = read042(wfrapbsid, sektion, "0236", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0237")))
                            {
                                // -- Afvist betaling
                                // -- BEHANDL: Afvist betaling
                                rcd = read042(wfrapbsid, sektion, "0237", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0238")))
                            {
                                // -- Afmeldt betaling
                                // -- BEHANDL: Afmeldt betaling
                                rcd = read042(wfrapbsid, sektion, "0238", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0239")))
                            {
                                // -- Tilbagef?rt betaling
                                // -- BEHANDL: Tilbagef?rt betaling
                                rcd = read042(wfrapbsid, sektion, "0239", rec);
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
                                rcd = read042(wfrapbsid, sektion, "0297", rec);
                            }
                            else if (((rec.Substring(0, 5) == "BS042")
                                        && (rec.Substring(13, 4) == "0299")))
                            {
                                // -- Tilbagef?rt FI-betaling
                                // -- BEHANDL: Tilbagef?rt FI-betaling
                                rcd = read042(wfrapbsid, sektion, "0299", rec);
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
                /*
                              // -- Update bogføringsbeløb
                stSql = "SELECT betid, sum(indbetalingsbelob) AS sumindbetalingsbelob FROM tblbetlin " & _
                        "WHERE betid IN (select id from tblbet where frapbsid = " & qf(wfrapbsid) & ") " & _
                        "GROUP BY betid;"
                Set rst = CurrentProject.Connection.Execute(stSql)
                Do While Not rst.eof
                    stSql = "UPDATE tblbet SET indbetalingsbelob = " & qf(rst!sumindbetalingsbelob) & " WHERE id = " & qf(rst!betid) & ";"
                   ''ebug.Print stSql & vbCrLf
                   CurrentProject.Connection.Execute stSql
                   rst.MoveNext
                Loop
                rst.Close;
                rstpbsfiles.MoveNext;
               */
            }  // slut rstpbsfiles
            return wfilescount;
        }

        public betlintype read042(int frapbsid, string sektion, string transkode, string rec)
        {
            int fortegn;
            double belobmun;
            int belob;
            betlintype rcd;

            // --  pbssektionnr
            rcd.pbssektionnr = sektion;
            // --  pbstranskode
            // - transkode 0236, gennemført automatisk betaling
            // - transkode 0237, afvist automatisk betaling
            // - transkode 0238, afmeldt automatisk betaling
            // - transkode 0239, tilbageført betaling
            rcd.pbstranskode = transkode;
            // --  debitorkonto
            if ((sektion == "0211"))
            {
                rcd.Nr = rec.Substring(33, 7);
                rcd.debitorkonto = rec.Substring(25, 15);
            }
            else if ((sektion == "0215"))
            {
                rcd.Nr = rec.Substring(37, 7);
                rcd.debitorkonto = rec.Substring(29, 15);
            }
            else
            {
                rcd.Nr = null;
                rcd.debitorkonto = null;
            }
            // --  aftalenr
            if ((sektion == "0211"))
            {
                rcd.aftalenr = rec.Substring(40, 9);
            }
            else
            {
                rcd.aftalenr = null;
            }
            // --  pbskortart
            if ((sektion == "0215"))
            {
                rcd.pbskortart = rec.Substring(44, 2);
            }
            else
            {
                rcd.pbskortart = null;
            }
            // --  pbsgebyrbelob
            if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(46, 1));
                belob = int.Parse(rec.Substring(47, 5));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.pbsgebyrbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.pbsgebyrbelob = belobmun;
                }
                else
                {
                    rcd.pbsgebyrbelob = 0;
                }
            }
            else
            {
                rcd.pbsgebyrbelob = 0;
            }
            // --  betalingsdato
            if ((sektion == "0211"))
            {
                if ((rec.Substring(49, 6) != "000000"))
                {
                    rcd.betalingsdato = ("20"
                                + (rec.Substring(53, 2) + ("-"
                                + (rec.Substring(51, 2) + ("-" + rec.Substring(49, 2))))));
                }
                else
                {
                    rcd.betalingsdato = null;
                }
            }
            else if ((sektion == "0215"))
            {
                if ((rec.Substring(52, 6) != "000000"))
                {
                    rcd.betalingsdato = ("20"
                                + (rec.Substring(56, 2) + ("-"
                                + (rec.Substring(54, 2) + ("-" + rec.Substring(52, 2))))));
                }
                else
                {
                    rcd.betalingsdato = null;
                }
            }
            else
            {
                rcd.betalingsdato = null;
            }
            // --  belob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(56, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.belob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.belob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    rcd.belob = (belobmun * -1);
                }
                else
                {
                    rcd.belob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(59, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.belob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.belob = belobmun;
                }
                else
                {
                    rcd.belob = null;
                }
            }
            else
            {
                rcd.belob = null;
            }
            // --  faknr
            if ((sektion == "0211"))
            {
                rcd.faknr = rec.Substring(69, 9).Trim();
            }
            else if ((sektion == "0215"))
            {
                rcd.faknr = rec.Substring(72, 9).Trim();
            }
            else
            {
                rcd.faknr = null;
            }
            // --  pbsarkivnr
            if ((sektion == "0215"))
            {
                rcd.pbsarkivnr = rec.Substring(81, 22);
            }
            else
            {
                rcd.pbsarkivnr = null;
            }
            // --  indbetalingsdato
            if ((rec.Substring(103, 6) != "000000"))
            {
                rcd.indbetalingsdato = ("20"
                            + (rec.Substring(107, 2) + ("-"
                            + (rec.Substring(105, 2) + ("-" + rec.Substring(103, 2))))));
            }
            else
            {
                rcd.indbetalingsdato = null;
            }
            // --  bogforingsdato
            rcd.bogforingsdato = 0;
            if ((rec.Substring(109, 6) != "000000"))
            {
                rcd.bogforingsdato = ("20"
                            + (rec.Substring(113, 2) + ("-"
                            + (rec.Substring(111, 2) + ("-" + rec.Substring(109, 2))))));
            }
            else
            {
                rcd.bogforingsdato = null;
            }
            // --  betid
            rcd.betid = getbetid(frapbsid, sektion, transkode, rcd.bogforingsdato);
            // --  indbetalingsbelob
            if ((sektion == "0211"))
            {
                fortegn = int.Parse(rec.Substring(55, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.indbetalingsbelob = belobmun;
                }
                else if ((fortegn == 2))
                {
                    rcd.indbetalingsbelob = (belobmun * -1);
                }
                else
                {
                    rcd.indbetalingsbelob = null;
                }
            }
            else if ((sektion == "0215"))
            {
                fortegn = int.Parse(rec.Substring(58, 1));
                belob = int.Parse(rec.Substring(115, 13));
                belobmun = (belob / 100);
                if ((fortegn == 0))
                {
                    rcd.indbetalingsbelob = 0;
                }
                else if ((fortegn == 1))
                {
                    rcd.indbetalingsbelob = belobmun;
                }
                else
                {
                    rcd.indbetalingsbelob = null;
                }
            }
            else
            {
                rcd.indbetalingsbelob = null;
            }
            /*           
                       stSql = ("insert into tblbetlin (betid, pbssektionnr, pbstranskode, Nr, faknr, " + ("debitorkonto, aftalenr, betalingsdato, belob, indbetalingsdato, bogforingsdato, " + ("indbetalingsbelob, pbskortart, pbsgebyrbelob, pbsarkivnr) " + ("values ("
                                   + (qf(rcd.betid) + (","
                                   + (qf(rcd.pbssektionnr) + (","
                                   + (qf(rcd.pbstranskode) + (","
                                   + (qf(rcd.Nr) + (","
                                   + (qf(rcd.faknr) + (", "
                                   + (qf(rcd.debitorkonto) + (", "
                                   + (qf(rcd.aftalenr) + (", "
                                   + (qf(rcd.betalingsdato) + (", "
                                   + (qf(rcd.belob) + (", "
                                   + (qf(rcd.indbetalingsdato) + (", "
                                   + (qf(rcd.bogforingsdato) + (", "
                                   + (qf(rcd.indbetalingsbelob) + (", "
                                   + (qf(rcd.pbskortart) + (", "
                                   + (qf(rcd.pbsgebyrbelob) + (", "
                                   + (qf(rcd.pbsarkivnr) + ")")))))))))))))))))))))))))))))))));
                       rst = CurrentProject.Connection.Execute(stSql);
            */
            return rcd;
        }

        public int getbetid(int pfrapbsid, string psektion, string ptranskode, object pbogforingsdato)
        {
            int wid = 0;
            /*
                        ADODB.Recordset rst;
                        // TODO: On Error GoTo Warning!!!: The statement is not translatable 
                        stSql = ("SELECT id as wid FROM tblbet" + (" WHERE frapbsid = "
                                    + (qf(pfrapbsid) + ("   AND pbssektionnr = "
                                    + (qf(psektion) + ("   AND transkode = "
                                    + (qf(ptranskode) + ("   AND bogforingsdato = "
                                    + (qf(pbogforingsdato) + ";")))))))));
                        rst = CurrentProject.Connection.Execute(stSql);
                        if (!rst.eof)
                        {
                            wid = rst!wid;
                        }
                        else
                        {
                            wid = nextval("tblbet_id_seq");
                            stSql = ("INSERT INTO tblbet (id, frapbsid, pbssektionnr, transkode, bogforingsdato)  values ("
                                        + (qf(wid) + (", "
                                        + (qf(pfrapbsid) + (","
                                        + (qf(psektion) + (","
                                        + (qf(ptranskode) + (","
                                        + (qf(pbogforingsdato) + ");"))))))))));
                            CurrentProject.Connection.Execute;
                            stSql;
                        }
             */
            return wid;
        }


   
    }
}
