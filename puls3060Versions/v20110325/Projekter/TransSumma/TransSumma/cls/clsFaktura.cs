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
                          where sf.faknr != 0 && sf.faktype == 0
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
                    Nettobelob = s.Nettobelob,
                    Bruttobelob = s.Bruttobelob
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
                          where kf.faknr != 0 && kf.faktype == 2
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
                    Nettobelob = k.Nettobelob,
                    Bruttobelob = k.Bruttobelob
                };
                Program.dbDataTransSumma.Tblfaklin.InsertOnSubmit(recFaklin);
                if (!(k.Fakid == 0)) recFak.Tblfaklin.Add(recFaklin);
                lastFakid = k.Fakid;
            }
            Program.dbDataTransSumma.SubmitChanges();

        }

        public int SalgsOrder2Summa(IList<Tblwfak> wFak)
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
                    //var qry_ordlin = from sfl in Program.dbDataTransSumma.Tblwfaklin where sfl.Fakpid == o.Pid select sfl;
                    var qry_ordlin = from sfl in o.Tblwfaklin select sfl;
                    int orebelob = (int)((from s in qry_ordlin select s.Bruttobelob).Sum() * 100);
                    int momsbelob = (int)((from s in qry_ordlin select s.Moms).Sum() * 100);
                    if (o.Dato == null) o.Dato = ToDay;
                    if (o.Konto == null) o.Konto = 0;

                    ordtype_s ord = new ordtype_s
                    (
                        SidsteSFakID,                     //fakid
                        (DateTime)o.Dato,                 //dato
                        ((DateTime)o.Dato).AddDays(14),   //forfaldsdato
                        orebelob,                         //fakbeløb i øre
                        (int)(o.Konto),                   //debitornr
                        momsbelob                         //momsbeløb i øre
                    );
                    recFakturaer_s rec = new recFakturaer_s { rec_no = SidsteRec_no, rec_data = ord };
                    Program.karFakturaer_s.Add(rec);

                    var m_rec = (from m in Program.karKartotek where m.Kontonr == o.Konto select m).First();
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
                    };
                    m_karFakturastr_s.Add(rec_Fakturastr_s);

                    foreach (var ol in qry_ordlin)
                    {
                        recFakturavarer_s rec_Fakturavarer_s = new recFakturavarer_s
                        {
                            Fakid = SidsteSFakID,
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(ol.Varenr) ? int.Parse(ol.Varenr) : (int?)null,
                            VareTekst = ol.Tekst,
                            Bogfkonto = ol.Konto,
                            Antal = ol.Antal,
                            Enhed = ol.Enhed,
                            Pris = ol.Pris,
                            Moms = ol.Moms,
                            Nettobelob = ol.Nettobelob,
                            Bruttobelob = ol.Bruttobelob,
                            Momspct = (ol.Momskode == "S25") ? 25 : 0
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

        public int KøbsOrder2Summa(IList<Tblwfak> wFak)
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
            var qry_ord = from sf in wFak //Program.dbDataTransSumma.Tblwfak
                          where sf.Sk == "K"
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

                    SidsteKFakID++;
                    SidsteRec_no++;
                    var qry_ordlin = from sfl in o.Tblwfaklin select sfl;
                    int orebelob = (int)((from s in qry_ordlin select s.Bruttobelob).Sum() * 100);
                    int momsbelob = (int)((from s in qry_ordlin select s.Moms).Sum() * 100);
                    if (o.Dato == null) o.Dato = ToDay;
                    if (o.Konto == null) o.Konto = 0;
                    if (o.Kreditorbilagsnr == null) o.Kreditorbilagsnr = 0;
                    ordtype_k ord = new ordtype_k
                    (
                        SidsteKFakID,                     //fakid
                        (DateTime)o.Dato,                 //dato
                        ((DateTime)o.Dato).AddDays(14),   //forfaldsdato
                        orebelob,                         //fakbeløb i øre
                        (int)(o.Konto),                   //kreditornr
                        momsbelob,                        //momsbeløb i øre
                        (int)(o.Kreditorbilagsnr)         //kreditorbilagsnr
                    );
                    recFakturaer_k rec = new recFakturaer_k { rec_no = SidsteRec_no, rec_data = ord };
                    Program.karFakturaer_k.Add(rec);

                    var m_rec = (from m in Program.karKartotek where m.Kontonr == o.Konto select m).First();
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
                    };
                    m_karFakturastr_k.Add(rec_Fakturastr_k);

                    foreach (var ol in qry_ordlin)
                    {
                        recFakturavarer_k rec_Fakturavarer_k = new recFakturavarer_k
                        {
                            Fakid = SidsteKFakID,
                            Varenr = Microsoft.VisualBasic.Information.IsNumeric(ol.Varenr) ? int.Parse(ol.Varenr) : (int?)null,
                            VareTekst = ol.Tekst,
                            Bogfkonto = ol.Konto,
                            Antal = ol.Antal,
                            Enhed = ol.Enhed,
                            Pris = ol.Pris,
                            Moms = ol.Moms,
                            Nettobelob = ol.Nettobelob,
                            Bruttobelob = ol.Bruttobelob,
                            Momspct = (ol.Momskode == "K25") ? 25 : 0
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
            var qry_ord = from f in Program.dbDataTransSumma.Tblfak
                          where f.Sk == "K"
                          select f;
            foreach (var o in qry_ord)
            {
                var qry_lin_omk = from l in o.Tblfaklin
                              join fl in Program.dbDataTransSumma.Tblvareomkostninger on l.Konto equals fl.Kontonr
                              select l;

                var qry_lin_vare = from l in o.Tblfaklin
                                   join fl in Program.dbDataTransSumma.Tblvareomkostninger on l.Konto equals fl.Kontonr into omklin
                                   from fl in omklin.DefaultIfEmpty(new Tblvareomkostninger { Kontonr = 0, Omktype = "varelinie" })
                                   where fl.Omktype == "varelinie"
                                   select l;
                
                int antal = qry_lin_omk.Count();
                if (antal > 0)
                {
                    decimal omkbelob = (decimal)(from s in qry_lin_omk select s.Nettobelob).Sum();
                    decimal varebelob = (decimal)(from s in qry_lin_vare select s.Nettobelob).Sum();
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
                        l.Omkostbelob = decimal.Round((decimal)l.Nettobelob * (omkfaktor - 1), 2);
                    }
                }
            }
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
