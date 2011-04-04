using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    class clsFaktura
    {
        public void ImportSalgsfakturaer()
        {
            int? lastFakid = null;
            Tblfak recFak = null;
            var rec_regnskab = Program.qryAktivRegnskab();
            var qrySFak = from sfv in Program.karFakturavarer_s
                             join sf in Program.karFakturaer_s on new { fakid = sfv.Fakid } equals new { fakid = sf.fakid }
                             where sf.faknr != 0
                             join fl in Program.dbDataTransSumma.Tblfaklin
                             on new
                             {
                                 regnskabsid = (int?)rec_regnskab.Rid,
                                 fakid = (int?)sfv.Fakid,
                                 sk = "S",
                                 line = (int?)sfv.Line
                             }
                             equals new
                             {
                                 regnskabsid = fl.Regnskabid,
                                 fakid = fl.Fakid,
                                 sk = fl.Sk,
                                 line = fl.Faklinnr
                             }
                             into tblfaklin
                             from fl in tblfaklin.DefaultIfEmpty(new Tblfaklin { Pid = 0, Fakpid = 0, Regnskabid = null })
                             where fl.Pid == 0
                             orderby sfv.Fakid, sfv.Line
                             select new 
                             {
                                 Regnskabid = rec_regnskab.Rid,
                                 Sk = "S",
                                 Fakid = sfv.Fakid,
                                 Faknr = sf.faknr,
                                 Dato = sf.dato,
                                 debitornr = sf.debitornr,
                                 Faklinnr = sfv.Line,
                                 Varenr = sfv.Varenr,
                                 Tekst = sfv.VareTekst,
                                 Konto = sfv.Bogfkonto,
                                 Momskode = "S25",
                                 Antal = sfv.Antal,
                                 Enhed = sfv.Enhed,
                                 Pris = sfv.Pris,
                                 Rabat = sfv.Rabat,
                                 Moms = sfv.Moms,
                                 Belob = sfv.Fakturabelob,
                             };

            int antal = qrySFak.Count();

            foreach (var s in qrySFak)
            {
                if ((!(s.Fakid == 0)) && (lastFakid != s.Fakid))
                {
                    try
                    {
                        recFak = (from f in Program.dbDataTransSumma.Tblfak
                                  where f.Regnskabid == rec_regnskab.Rid && f.Sk == "S" && f.Fakid == s.Fakid
                                  select f).First();
                    }
                    catch
                    {
                        recFak = new Tblfak
                        {
                            Udskriv = true,
                            Regnskabid = s.Regnskabid,
                            Sk = s.Sk,
                            Fakid = s.Fakid,
                            Faknr = s.Faknr,
                            Dato = s.Dato,
                            Konto = s.debitornr
                        };
                        Program.dbDataTransSumma.Tblfak.InsertOnSubmit(recFak);
                    }
                }


                Tblfaklin recFaklin = new Tblfaklin
                {
                    Sk = s.Sk,
                    Regnskabid = s.Regnskabid,
                    Fakid = s.Fakid,
                    Faklinnr = s.Faklinnr,
                    Varenr = s.Varenr.ToString(),
                    Tekst = s.Tekst,
                    Konto = s.Konto,
                    Momskode = s.Momskode, 
                    Antal = s.Antal,
                    Enhed = s.Enhed,
                    Pris = s.Pris,
                    Rabat = s.Rabat,
                    Moms = s.Moms,
                    Belob = s.Belob
                };
                Program.dbDataTransSumma.Tblfaklin.InsertOnSubmit(recFaklin);
                if (!(s.Fakid == 0)) recFak.Tblfaklin.Add(recFaklin);
                lastFakid = s.Fakid;
            }
            Program.dbDataTransSumma.SubmitChanges();

        }

        public void ImportKøbsfakturaer()
        {
            int? lastFakid = null;
            Tblfak recFak = null;
            var rec_regnskab = Program.qryAktivRegnskab();
            var qryKFak = from kfv in Program.karFakturavarer_k
                          join kf in Program.karFakturaer_k on new { fakid = kfv.Fakid } equals new { fakid = kf.fakid }
                          where kf.faknr != 0
                          join fl in Program.dbDataTransSumma.Tblfaklin
                          on new
                          {
                              regnskabsid = (int?)rec_regnskab.Rid,
                              fakid = (int?)kfv.Fakid,
                              sk = "K",
                              line = (int?)kfv.Line
                          }
                          equals new
                          {
                              regnskabsid = fl.Regnskabid,
                              fakid = fl.Fakid,
                              sk = fl.Sk,
                              line = fl.Faklinnr
                          }
                          into tblfaklin
                          from fl in tblfaklin.DefaultIfEmpty(new Tblfaklin { Pid = 0, Fakpid = 0, Regnskabid = null })
                          where fl.Pid == 0
                          orderby kfv.Fakid, kfv.Line
                          select new
                          {
                              Regnskabid = rec_regnskab.Rid,
                              Sk = "K",
                              Fakid = kfv.Fakid,
                              Faknr = kf.faknr,
                              Dato = kf.dato,
                              kreditornr = kf.kreditornr,
                              Faklinnr = kfv.Line,
                              Varenr = kfv.Varenr,
                              Tekst = kfv.VareTekst,
                              Konto = kfv.Bogfkonto,
                              Momskode = "K25",
                              Antal = kfv.Antal,
                              Enhed = kfv.Enhed,
                              Pris = kfv.Pris,
                              Rabat = kfv.Rabat,
                              Moms = kfv.Moms,
                              Belob = kfv.Fakturabelob,
                          };

            int antal = qryKFak.Count();

            foreach (var k in qryKFak)
            {
                if ((!(k.Fakid == 0)) && (lastFakid != k.Fakid))
                {
                    try
                    {
                        recFak = (from f in Program.dbDataTransSumma.Tblfak
                                  where f.Regnskabid == rec_regnskab.Rid && f.Sk == "K" && f.Fakid == k.Fakid
                                  select f).First();
                    }
                    catch
                    {
                        recFak = new Tblfak
                        {
                            Udskriv = true,
                            Regnskabid = k.Regnskabid,
                            Sk = k.Sk,
                            Fakid = k.Fakid,
                            Faknr = k.Faknr,
                            Dato = k.Dato,
                            Konto = k.kreditornr
                        };
                        Program.dbDataTransSumma.Tblfak.InsertOnSubmit(recFak);
                    }
                }


                Tblfaklin recFaklin = new Tblfaklin
                {
                    Sk = k.Sk,
                    Regnskabid = k.Regnskabid,
                    Fakid = k.Fakid,
                    Faklinnr = k.Faklinnr,
                    Varenr = k.Varenr.ToString(),
                    Tekst = k.Tekst,
                    Konto = k.Konto,
                    Momskode = k.Momskode,
                    Antal = k.Antal,
                    Enhed = k.Enhed,
                    Pris = k.Pris,
                    Rabat = k.Rabat,
                    Moms = k.Moms,
                    Belob = k.Belob
                };
                Program.dbDataTransSumma.Tblfaklin.InsertOnSubmit(recFaklin);
                if (!(k.Fakid == 0)) recFak.Tblfaklin.Add(recFaklin);
                lastFakid = k.Fakid;
            }
            Program.dbDataTransSumma.SubmitChanges();

        }

        
        public int SalgsOrder2Summa()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            if (rec_regnskab.Afsluttet == true) return 0;
            KarFakturastr_s m_karFakturastr_s = null;
            KarFakturavarer_s m_karFakturavarer_s = null;

            DateTime? Startdato = rec_regnskab.Start;
            DateTime? Slutdato = rec_regnskab.Slut;
            if (rec_regnskab.DatoLaas != null)
            {
                if (rec_regnskab.DatoLaas > Startdato) Startdato = rec_regnskab.DatoLaas;
            }
            var qry_ord = from sf in Program.dbDataTransSumma.Tblwfak
                          where sf.Sk == "S"
                          select sf;

            int AntalOrdre = qry_ord.Count();

            if (AntalOrdre > 0)
            {
                DateTime ToDay = DateTime.Today;
                int SidsteSFakID;
                int SidsteRec_no;
                try
                {
                    SidsteSFakID = (from f in Program.karFakturaer_s select f.fakid).Max();
                }
                catch (System.InvalidOperationException)
                {
                    SidsteSFakID = 0;
                }
                try
                {
                    SidsteRec_no = (from f in Program.karFakturaer_s select f.rec_no).Max();
                }
                catch (System.InvalidOperationException)
                {
                    SidsteRec_no = 0;
                }
                m_karFakturastr_s = new KarFakturastr_s();
                m_karFakturavarer_s = new KarFakturavarer_s(true);

                foreach (var o in qry_ord)
                {

                    SidsteSFakID++;
                    SidsteRec_no++;
                    var qry_ordlin = from sfl in Program.dbDataTransSumma.Tblwfaklin where sfl.Fakpid == o.Pid select sfl;
                    int orebelob = (int)((from s in qry_ordlin select s.Belob).Sum()) * 100;

                    ordtype_s ord = new ordtype_s
                    (  
                        SidsteSFakID,                     //fakid
                        (DateTime)o.Dato,                 //dato
                        ((DateTime)o.Dato).AddDays(14),   //forfaldsdato
                        orebelob,                         //fakbeløb i øre
                        (int)(o.Konto)                    //debitornr
                    );
                    recFakturaer_s rec = new recFakturaer_s { rec_no = SidsteRec_no, rec_data = ord };
                    Program.karFakturaer_s.Add(rec);

                    var m_rec = (from m in Program.karKartotek where m.Kontonr == o.Konto select m).First();
                    recFakturastr_s rec_Fakturastr_s = new recFakturastr_s
                    {
                        Fakid = SidsteSFakID.ToString(),
                        Navn = m_rec.Kontonavn,
                        Adresse = m_rec.Adresse,
                        Postnr = m_rec.Postnr,
                        Bynavn = m_rec.Bynavn,
                        Email = m_rec.Email
                    };
                    m_karFakturastr_s.Add(rec_Fakturastr_s);

                    foreach (var l in qry_ordlin)
                    {
                        recFakturavarer_s rec_Fakturavarer_s = new recFakturavarer_s
                        {
                            Fakid = SidsteSFakID,
                            Varenr = int.Parse(l.Varenr),
                            VareTekst = l.Tekst,
                            Bogfkonto = l.Konto,
                            Antal = l.Antal,
                            Pris = l.Pris,
                            Moms = l.Moms,
                            Fakturabelob = l.Belob
                        };
                        m_karFakturavarer_s.Add(rec_Fakturavarer_s);
                    }
                }
                Program.karFakturaer_s.save();
                m_karFakturastr_s.save();
                m_karFakturavarer_s.save();

                try
                {
                    recStatus rec_Status = (from s in Program.karStatus where s.key == "SidsteSFakID" select s).First();
                    rec_Status.value = SidsteSFakID.ToString();
                }
                catch (System.InvalidOperationException)
                {
                    recStatus rec_Status = new recStatus
                    {
                        key = "SidsteSFakID",
                        value = SidsteSFakID.ToString()
                    };
                    Program.karStatus.Add(rec_Status);
                }
                Program.karStatus.save();
                Program.dbDataTransSumma.SubmitChanges();
            }
            return AntalOrdre;
        }
        
    }
}
