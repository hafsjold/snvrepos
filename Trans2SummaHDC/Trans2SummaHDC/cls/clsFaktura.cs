using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trans2SummaHDC
{
    class clsFaktura
    {
        public void ImportSalgsfakturaer()
        {
            int? lastFakid = null;
            tblfak recFak = null;
            var rec_regnskab = Program.qryAktivRegnskab();
            var qrySFak = from sfv in Program.karFakturavarer_s
                          join sf in Program.karFakturaer_s on new { fakid = sfv.Fakid } equals new { fakid = sf.fakid }
                          where sf.faknr != 0 && sf.faktype == 0
                          join fl in Program.dbDataTransSumma.tblfaklins
                          on new
                          {
                              regnskabsid = (int?)rec_regnskab.Rid,
                              fakid = (int?)sfv.Fakid,
                              sk = "S",
                              line = (int?)sfv.Line
                          }
                          equals new
                          {
                              regnskabsid = fl.regnskabid,
                              fakid = fl.fakid,
                              sk = fl.sk,
                              line = fl.faklinnr
                          }
                          into tblfaklin
                          from fl in tblfaklin.DefaultIfEmpty(new tblfaklin { pid = 0, fakpid = 0, regnskabid = null })
                          where fl.pid == 0
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
                              Momskode = KarKontoplan.getMomskode(sfv.Bogfkonto),
                              Antal = sfv.Antal,
                              Enhed = sfv.Enhed,
                              Pris = sfv.Pris,
                              Rabat = sfv.Rabat,
                              Moms = sfv.Moms,
                              Nettobelob = sfv.Nettobelob,
                              Bruttobelob = sfv.Bruttobelob,
                          };

            int antal = qrySFak.Count();

            foreach (var s in qrySFak)
            {
                if ((!(s.Fakid == 0)) && (lastFakid != s.Fakid))
                {
                    try
                    {
                        recFak = (from f in Program.dbDataTransSumma.tblfaks
                                  where f.regnskabid == rec_regnskab.Rid && f.sk == "S" && f.fakid == s.Fakid
                                  select f).First();
                    }
                    catch
                    {
                        recFak = new tblfak
                        {
                            udskriv = true,
                            regnskabid = s.Regnskabid,
                            sk = s.Sk,
                            fakid = s.Fakid,
                            faknr = s.Faknr,
                            dato = s.Dato,
                            konto = s.debitornr
                        };
                        Program.dbDataTransSumma.tblfaks.InsertOnSubmit(recFak);
                    }
                }


                tblfaklin recFaklin = new tblfaklin
                {
                    sk = s.Sk,
                    regnskabid = s.Regnskabid,
                    fakid = s.Fakid,
                    faklinnr = s.Faklinnr,
                    varenr = s.Varenr.ToString(),
                    tekst = s.Tekst,
                    konto = s.Konto,
                    momskode = s.Momskode,
                    antal = s.Antal,
                    enhed = s.Enhed,
                    pris = s.Pris,
                    rabat = s.Rabat,
                    moms = s.Moms,
                    nettobelob = s.Nettobelob,
                    bruttobelob = s.Bruttobelob
                };
                Program.dbDataTransSumma.tblfaklins.InsertOnSubmit(recFaklin);
                if (!(s.Fakid == 0)) recFak.tblfaklins.Add(recFaklin);
                lastFakid = s.Fakid;
            }
            Program.dbDataTransSumma.SubmitChanges();

        }

        public void ImportKøbsfakturaer()
        {
            int? lastFakid = null;
            tblfak recFak = null;
            var rec_regnskab = Program.qryAktivRegnskab();
            var qryKFak = from kfv in Program.karFakturavarer_k
                          join kf in Program.karFakturaer_k on new { fakid = kfv.Fakid } equals new { fakid = kf.fakid }
                          where kf.faknr != 0 && kf.faktype == 2
                          join fl in Program.dbDataTransSumma.tblfaklins
                          on new
                          {
                              regnskabsid = (int?)rec_regnskab.Rid,
                              fakid = (int?)kfv.Fakid,
                              sk = "K",
                              line = (int?)kfv.Line
                          }
                          equals new
                          {
                              regnskabsid = fl.regnskabid,
                              fakid = fl.fakid,
                              sk = fl.sk,
                              line = fl.faklinnr
                          }
                          into tblfaklin
                          from fl in tblfaklin.DefaultIfEmpty(new tblfaklin { pid = 0, fakpid = 0, regnskabid = null })
                          where fl.pid == 0
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
                              Momskode = KarKontoplan.getMomskode(kfv.Bogfkonto),
                              Antal = kfv.Antal,
                              Enhed = kfv.Enhed,
                              Pris = kfv.Pris,
                              Rabat = kfv.Rabat,
                              Moms = kfv.Moms,
                              Nettobelob = kfv.Nettobelob,
                              Bruttobelob = kfv.Bruttobelob,
                          };

            int antal = qryKFak.Count();

            foreach (var k in qryKFak)
            {
                if ((!(k.Fakid == 0)) && (lastFakid != k.Fakid))
                {
                    try
                    {
                        recFak = (from f in Program.dbDataTransSumma.tblfaks
                                  where f.regnskabid == rec_regnskab.Rid && f.sk == "K" && f.fakid == k.Fakid
                                  select f).First();
                    }
                    catch
                    {
                        recFak = new tblfak
                        {
                            udskriv = true,
                            regnskabid = k.Regnskabid,
                            sk = k.Sk,
                            fakid = k.Fakid,
                            faknr = k.Faknr,
                            dato = k.Dato,
                            konto = k.kreditornr
                        };
                        Program.dbDataTransSumma.tblfaks.InsertOnSubmit(recFak);
                    }
                }


                tblfaklin recFaklin = new tblfaklin
                {
                    sk = k.Sk,
                    regnskabid = k.Regnskabid,
                    fakid = k.Fakid,
                    faklinnr = k.Faklinnr,
                    varenr = k.Varenr.ToString(),
                    tekst = k.Tekst,
                    konto = k.Konto,
                    momskode = k.Momskode,
                    antal = k.Antal,
                    enhed = k.Enhed,
                    pris = k.Pris,
                    rabat = k.Rabat,
                    moms = k.Moms,
                    nettobelob = k.Nettobelob,
                    bruttobelob = k.Bruttobelob
                };
                Program.dbDataTransSumma.tblfaklins.InsertOnSubmit(recFaklin);
                if (!(k.Fakid == 0)) recFak.tblfaklins.Add(recFaklin);
                lastFakid = k.Fakid;
            }
            Program.dbDataTransSumma.SubmitChanges();

        }

        public int SalgsOrder2Summa(IList<tblwfak> wFak)
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
            var qry_ord = from sf in wFak //Program.dbDataTransSumma.Tblwfak
                          where sf.sk == "S"
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
                    var qry_ordlin = from sfl in o.tblwfaklins select sfl;
                    if (qry_ordlin.Count() == 0)  //der findes ingen ordrelinier
                        continue;

                    SidsteSFakID++;
                    SidsteRec_no++;
                    //var qry_ordlin = from sfl in Program.dbDataTransSumma.Tblwfaklin where sfl.Fakpid == o.Pid select sfl;
                    int orebelob = (int)((from s in qry_ordlin select s.bruttobelob).Sum() * 100);
                    int momsbelob = (int)((from s in qry_ordlin select s.moms).Sum() * 100);
                    if (o.dato == null) o.dato = ToDay;
                    if (o.konto == null) o.konto = 0;

                    ordtype_s ord = new ordtype_s
                    (
                        SidsteSFakID,                     //fakid
                        (DateTime)o.dato,                 //dato
                        ((DateTime)o.dato).AddDays(14),   //forfaldsdato
                        orebelob,                         //fakbeløb i øre
                        (int)(o.konto),                   //debitornr
                        momsbelob                         //momsbeløb i øre
                    );
                    recFakturaer_s rec = new recFakturaer_s { rec_no = SidsteRec_no, rec_data = ord };
                    Program.karFakturaer_s.Add(rec);

                    var m_rec = (from m in Program.karKartotek where m.Kontonr == o.konto select m).First();
                    string[] wAdresse = new string[1];
                    wAdresse[0] = m_rec.Adresse;
                    clsNavnAdresse wFakNavnAdresse = new clsNavnAdresse
                    {
                        Navn = m_rec.Kontonavn,
                        Adresse = wAdresse,
                        Postnr = m_rec.Postnr,
                        Bynavn = m_rec.Bynavn,
                    };
                    recFakturastr_s rec_Fakturastr_s = new recFakturastr_s
                    {
                        Fakid = SidsteSFakID.ToString(),
                        FakNavnAdresse = wFakNavnAdresse,
                        Email = m_rec.Email,
                        Cvrnr = m_rec.Cvrnr,
                    };
                    m_karFakturastr_s.Add(rec_Fakturastr_s);

                    foreach (var ol in qry_ordlin)
                    {
                        recFakturavarer_s rec_Fakturavarer_s = new recFakturavarer_s
                        {
                            Fakid = SidsteSFakID,
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(ol.varenr) ? int.Parse(ol.varenr) : (int?)null,
                            VareTekst = ol.tekst,
                            Bogfkonto = ol.konto,
                            Antal = ol.antal,
                            Enhed = ol.enhed,
                            Pris = ol.pris,
                            Moms = ol.moms,
                            Nettobelob = ol.nettobelob,
                            Bruttobelob = ol.bruttobelob,
                            Momspct = KarMoms.getMomspct(ol.momskode)
                        };
                        m_karFakturavarer_s.Add(rec_Fakturavarer_s);
                    }
                }

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
                Program.karFakturaer_s.save();
                m_karFakturastr_s.save();
                m_karFakturavarer_s.save();

                Program.dbDataTransSumma.SubmitChanges();
            }
            return AntalOrdre;
        }

        public int KøbsOrder2Summa(IList<tblwfak> wFak)
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            if (rec_regnskab.Afsluttet == true) return 0;
            KarFakturastr_k m_karFakturastr_k = null;
            KarFakturavarer_k m_karFakturavarer_k = null;

            DateTime? Startdato = rec_regnskab.Start;
            DateTime? Slutdato = rec_regnskab.Slut;
            if (rec_regnskab.DatoLaas != null)
            {
                if (rec_regnskab.DatoLaas > Startdato) Startdato = rec_regnskab.DatoLaas;
            }
            var qry_ord = from sf in wFak
                          where sf.sk == "K"
                          select sf;

            int AntalOrdre = qry_ord.Count();

            if (AntalOrdre > 0)
            {
                DateTime ToDay = DateTime.Today;
                int SidsteKFakID;
                int SidsteRec_no;
                try
                {
                    SidsteKFakID = (from f in Program.karFakturaer_k select f.fakid).Max();
                }
                catch (System.InvalidOperationException)
                {
                    SidsteKFakID = 0;
                }
                try
                {
                    SidsteRec_no = (from f in Program.karFakturaer_k select f.rec_no).Max();
                }
                catch (System.InvalidOperationException)
                {
                    SidsteRec_no = 0;
                }
                m_karFakturastr_k = new KarFakturastr_k();
                m_karFakturavarer_k = new KarFakturavarer_k(true);

                foreach (var o in qry_ord)
                {
                    var qry_ordlin = from sfl in o.tblwfaklins select sfl;
                    if (qry_ordlin.Count() == 0) //der findes ingen ordrelinier
                        continue;

                    SidsteKFakID++;
                    SidsteRec_no++;
                    int orebelob = (int)((from s in qry_ordlin select s.bruttobelob).Sum() * 100);
                    int momsbelob = (int)((from s in qry_ordlin select s.moms).Sum() * 100);
                    if (o.dato == null) o.dato = ToDay;
                    if (o.konto == null) o.konto = 0;
                    if (o.kreditorbilagsnr == null) o.kreditorbilagsnr = 0;
                    ordtype_k ord = new ordtype_k
                    (
                        SidsteKFakID,                     //fakid
                        (DateTime)o.dato,                 //dato
                        ((DateTime)o.dato).AddDays(14),   //forfaldsdato
                        orebelob,                         //fakbeløb i øre
                        (int)(o.konto),                   //kreditornr
                        momsbelob,                        //momsbeløb i øre
                        (int)(o.kreditorbilagsnr)         //kreditorbilagsnr
                    );
                    recFakturaer_k rec = new recFakturaer_k { rec_no = SidsteRec_no, rec_data = ord };
                    Program.karFakturaer_k.Add(rec);

                    var m_rec = (from m in Program.karKartotek where m.Kontonr == o.konto select m).First();
                    string[] wAdresse = new string[1];
                    wAdresse[0] = m_rec.Adresse;
                    clsNavnAdresse wFakNavnAdresse = new clsNavnAdresse
                    {
                        Navn = m_rec.Kontonavn,
                        Adresse = wAdresse,
                        Postnr = m_rec.Postnr,
                        Bynavn = m_rec.Bynavn,
                    };
                    recFakturastr_k rec_Fakturastr_k = new recFakturastr_k
                    {
                        Fakid = SidsteKFakID,
                        FakNavnAdresse = wFakNavnAdresse,
                        Cvrnr = m_rec.Cvrnr,
                    };
                    m_karFakturastr_k.Add(rec_Fakturastr_k);

                    foreach (var ol in qry_ordlin)
                    {
                        recFakturavarer_k rec_Fakturavarer_k = new recFakturavarer_k
                        {
                            Fakid = SidsteKFakID,
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(ol.varenr) ? int.Parse(ol.varenr) : (int?)null,
                            VareTekst = ol.tekst,
                            Bogfkonto = ol.konto,
                            Antal = ol.antal,
                            Enhed = ol.enhed,
                            Pris = ol.pris,
                            Moms = ol.moms,
                            Nettobelob = ol.nettobelob,
                            Bruttobelob = ol.bruttobelob,
                            Momspct = KarMoms.getMomspct(ol.momskode)
                        };
                        m_karFakturavarer_k.Add(rec_Fakturavarer_k);
                    }
                }

                try
                {
                    recStatus rec_Status = (from s in Program.karStatus where s.key == "SidsteKFakID" select s).First();
                    rec_Status.value = SidsteKFakID.ToString();
                }
                catch (System.InvalidOperationException)
                {
                    recStatus rec_Status = new recStatus
                    {
                        key = "SidsteKFakID",
                        value = SidsteKFakID.ToString()
                    };
                    Program.karStatus.Add(rec_Status);
                }
                Program.karStatus.save();

                Program.karFakturaer_k.save();
                m_karFakturastr_k.save();
                m_karFakturavarer_k.save();

                Program.dbDataTransSumma.SubmitChanges();
            }
            return AntalOrdre;
        }

        public void KøbsOrderOmk()
        {
            decimal omkfaktor;
            var qry_ord = from f in Program.dbDataTransSumma.tblfaks
                          where f.sk == "K"
                          select f;
            foreach (var o in qry_ord)
            {
                var qry_lin_omk = from l in o.tblfaklins
                                  join fl in Program.dbDataTransSumma.tblvareomkostningers on l.konto equals fl.kontonr
                                  where fl.omktype == "vareomk"
                                  select l;

                var qry_lin_vare = from l in o.tblfaklins
                                   join fl in Program.dbDataTransSumma.tblvareomkostningers on l.konto equals fl.kontonr into omklin
                                   from fl in omklin.DefaultIfEmpty(new tblvareomkostninger { kontonr = 0, omktype = "varelinie" })
                                   where fl.omktype != "vareomk"
                                   select l;

                int antal = qry_lin_omk.Count();
                if (antal > 0)
                {
                    decimal omkbelob = (decimal)(from s in qry_lin_omk select s.nettobelob).Sum();
                    decimal varebelob = (decimal)(from s in qry_lin_vare select s.nettobelob).Sum();
                    if (varebelob != 0)
                    {
                        omkfaktor = 1 + omkbelob / varebelob;
                        decimal total1 = varebelob + omkbelob;
                        decimal total2 = varebelob * omkfaktor;
                    }
                    else
                        omkfaktor = 1;

                    foreach (var l in qry_lin_vare)
                    {
                        l.omkostbelob = decimal.Round((decimal)l.nettobelob * (omkfaktor - 1), 2);
                    }
                }
            }
            Program.dbDataTransSumma.SubmitChanges();
        }

    }
}
