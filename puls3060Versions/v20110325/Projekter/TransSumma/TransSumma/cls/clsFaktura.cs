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
    }
}
