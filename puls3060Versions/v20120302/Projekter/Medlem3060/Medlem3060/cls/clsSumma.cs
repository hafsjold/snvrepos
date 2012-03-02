using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace nsPuls3060
{
    class clsSumma
    {
        public int Order2Summa()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            if (rec_regnskab.Afsluttet == true) return 0;

            DateTime? Startdato = rec_regnskab.Start;
            DateTime? Slutdato = rec_regnskab.Slut;
            if (rec_regnskab.DatoLaas != null)
            {
                if (rec_regnskab.DatoLaas > Startdato) Startdato = rec_regnskab.DatoLaas;
            }
            var qry_ord = from f in Program.dbData3060.tblfaks
                          where f.SFakID == null && Startdato <= f.betalingsdato && f.betalingsdato <= Slutdato
                          join b in Program.dbData3060.tblbetlins on f.faknr equals b.faknr
                          where b.pbstranskode == "0236" || b.pbstranskode == "0297"
                          select new { f.id, Pbsfaknr = f.faknr, f.Nr, f.advisbelob, f.betalingsdato, f.vnr, f.bogfkonto, b.indbetalingsdato };

            int AntalOrdre = qry_ord.Count();

            if (AntalOrdre > 0)
            {
                DateTime nu = DateTime.Now;
                DateTime ToDay = new DateTime(nu.Year, nu.Month, nu.Day); ;
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
                Program.karFakturastr_s = null;
                Program.karFakturavarer_s = null;

                foreach (var o in qry_ord)
                {
                    SidsteSFakID++;
                    SidsteRec_no++;
                    int orebelob = (int)o.advisbelob * 100;

                    ordtype_s ord = new ordtype_s
                    (
                        SidsteSFakID,          //fakid
                        ToDay,                 //(o.Betalingsdato > o.Indbetalingsdato) ? (DateTime)o.Betalingsdato : (DateTime)o.Indbetalingsdato, //dato
                        ToDay,                 //(DateTime)o.Betalingsdato, //forfaldsdato
                        orebelob,              //fakbeløb i øre
                        (int)Nr2Debktonr(o.Nr) //debitornr
                    );
                    recFakturaer_s rec = new recFakturaer_s { rec_no = SidsteRec_no, rec_data = ord };
                    Program.karFakturaer_s.Add(rec);

                    var m_rec = (from m in Program.karMedlemmer where m.Nr == o.Nr select m).First();
                    recFakturastr_s rec_Fakturastr_s = new recFakturastr_s
                    {
                        Fakid = SidsteSFakID.ToString(),
                        Navn = m_rec.Navn,
                        Adresse = m_rec.Adresse,
                        Postnr = m_rec.Postnr,
                        Bynavn = m_rec.Bynavn,
                        Faknr = (int)o.Pbsfaknr,
                        Email = m_rec.Email
                    };
                    Program.karFakturastr_s.Add(rec_Fakturastr_s);

                    recFakturavarer_s rec_Fakturavarer_s = new recFakturavarer_s
                    {
                        Fakid = SidsteSFakID.ToString(),
                        Varenr = (int)o.vnr,
                        VareTekst = "Puls 3060 kontingent",
                        Bogfkonto = (int)o.bogfkonto,
                        Antal = 1,
                        Fakturabelob = (double)o.advisbelob
                    };
                    Program.karFakturavarer_s.Add(rec_Fakturavarer_s);

                    try
                    {
                        tblfak rec_fak = (from f in Program.dbData3060.tblfaks
                                          where f.id == o.id
                                          select f).First();
                        rec_fak.SFakID = SidsteSFakID;
                    }
                    catch (System.InvalidOperationException)
                    {
                        throw;
                    }
                }
                Program.karFakturaer_s.save();
                Program.karFakturastr_s.save();
                Program.karFakturavarer_s.save();

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
                Program.dbData3060.SubmitChanges();
            }
            return AntalOrdre;
        }

        public int OrderFaknrUpdate()
        {
            var qry = from k in Program.karFakturaer_s
                      where k.faknr > 0
                      join f in Program.dbData3060.tblfaks on k.fakid equals f.SFakID
                      where f.SFaknr == null
                      select new { f.id, SFaknr = k.faknr };

            int AntalFakturaer = qry.Count();
            if (qry.Count() > 0)
            {
                foreach (var k in qry)
                {
                    try
                    {
                        tblfak rec_fak = (from f in Program.dbData3060.tblfaks
                                          where f.id == k.id
                                          select f).First();
                        rec_fak.SFaknr = k.SFaknr;
                    }
                    catch (System.InvalidOperationException)
                    {
                        throw;
                    }
                }
                Program.dbData3060.SubmitChanges();
            }
            return AntalFakturaer;
        }

        public int BogforIndBetalinger()
        {
            int saveBetid = 0;
            var bogf = from s in Program.karFakturaer_s
                       //where s.saldo != 0
                       join f in Program.dbData3060.tblfaks on s.fakid equals f.SFakID
                       where f.SFaknr != null
                       join m in Program.karMedlemmer on f.Nr equals m.Nr
                       join bl in Program.dbData3060.tblbetlins on f.faknr equals bl.faknr
                       join b in Program.dbData3060.tblbets on bl.betid equals b.id
                       where b.summabogfort != true
                       join p in Program.dbData3060.tblfrapbs on b.frapbsid equals p.id
                       orderby p.id, b.id, bl.id
                       select new
                       {
                           Frapbsid = p.id,
                           p.leverancespecifikation,
                           Betid = b.id,
                           GruppeIndbetalingsbelob = b.indbetalingsbelob,
                           Betlinid = bl.id,
                           Fakid = f.id,
                           bl.betalingsdato,
                           bl.indbetalingsdato,
                           m.Navn,
                           bl.indbetalingsbelob,
                           f.SFaknr
                       };
            int AntalBetalinger = bogf.Count();
            if (bogf.Count() > 0)
            {
                DateTime nu = DateTime.Now;
                DateTime ToDay = new DateTime(nu.Year, nu.Month, nu.Day); ;

                int BS1_SidsteNr = 0;
                try
                {
                    recStatus rec_Status = (from s in Program.karStatus where s.key == "BS1_SidsteNr" select s).First();
                    BS1_SidsteNr = int.Parse(rec_Status.value);
                }
                catch (System.InvalidOperationException)
                {
                }

                Program.karKladde = null;


                foreach (var b in bogf)
                {
                    if (saveBetid != b.Betid) // ny gruppe
                    {
                        saveBetid = b.Betid;
                        recKladde gkl = new recKladde
                        {
                            Dato = ToDay,    //(b.Betalingsdato > b.Indbetalingsdato) ? (DateTime)b.Betalingsdato : (DateTime)b.Indbetalingsdato,
                            Bilag = ++BS1_SidsteNr,
                            Tekst = "Indbetalingskort K 81131945-" + ((DateTime)b.indbetalingsdato).Day + "." + ((DateTime)b.indbetalingsdato).Month,
                            Afstemningskonto = "Bank",
                            Belob = b.GruppeIndbetalingsbelob,
                            Kontonr = null,
                            Faknr = null
                        };
                        Program.karKladde.Add(gkl);

                        var rec_bet = (from ub in Program.dbData3060.tblbets where ub.id == b.Betid select ub).First();
                        rec_bet.summabogfort = true;

                    }

                    recKladde kl = new recKladde
                    {
                        Dato = ToDay,  //(b.Betalingsdato > b.Indbetalingsdato) ? (DateTime)b.Betalingsdato : (DateTime)b.Indbetalingsdato,
                        Bilag = BS1_SidsteNr,
                        Tekst = "F" + b.SFaknr + " " + b.Navn,
                        Afstemningskonto = null,
                        Belob = b.indbetalingsbelob,
                        Kontonr = 56100,
                        Faknr = b.SFaknr
                    };
                    Program.karKladde.Add(kl);
                }
                Program.karStatus.save();
                Program.karKladde.save();
                Program.dbData3060.SubmitChanges();
            }
            return AntalBetalinger;
        }

        public int BogforUdBetalinger(int lobnr)
        {
            var bogf = from f in Program.karFakturaer_k
                       where f.saldo != 0
                       //join o in Program.dbData3060.Tbloverforsel on f.fakid equals o.SFakID
                       join o in Program.dbData3060.tbloverforsels on f.faknr equals o.SFaknr
                       where o.tilpbsid == lobnr
                       join m in Program.karMedlemmer on o.Nr equals m.Nr
                       orderby o.betalingsdato ascending
                       select new
                       {
                           Fakid = o.id,
                           m.Navn,
                           o.SFaknr,
                           o.betalingsdato,
                           o.advisbelob
                       };
            int AntalBetalinger = bogf.Count();
            if (bogf.Count() > 0)
            {
                int BS1_SidsteNr = 0;
                try
                {
                    recStatus rec_Status = (from s in Program.karStatus where s.key == "BS1_SidsteNr" select s).First();
                    BS1_SidsteNr = int.Parse(rec_Status.value);
                }
                catch (System.InvalidOperationException)
                {
                }

                Program.karKladde = null;

                foreach (var b in bogf)
                {
                    recKladde gkl = new recKladde
                    {
                        Dato = clsOverfoersel.bankdageplus((DateTime)b.betalingsdato, -1),
                        Bilag = ++BS1_SidsteNr,
                        Tekst = "Overførsel",
                        Afstemningskonto = "Bank",
                        Belob = -b.advisbelob,
                        Kontonr = null,
                        Faknr = null
                    };
                    Program.karKladde.Add(gkl);
                    recKladde kl = new recKladde
                    {
                        Dato = clsOverfoersel.bankdageplus((DateTime)b.betalingsdato, -1),
                        Bilag = BS1_SidsteNr,
                        Tekst = "KF" + b.SFaknr + " " + b.Navn,
                        Afstemningskonto = null,
                        Belob = -b.advisbelob,
                        Kontonr = 65100,
                        Faknr = b.SFaknr
                    };
                    Program.karKladde.Add(kl);
                }

                Program.karStatus.save();
                Program.karKladde.save();
                Program.dbData3060.SubmitChanges();
            }
            return AntalBetalinger;
        }

        public int? Nr2Debktonr(int? Nr)
        {
            if (Nr == null) return null;
            try
            {
                return int.Parse((from k in Program.karMedlemmer where k.Nr == Nr select k).First().Debktonr);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public int? Nr2Krdktonr(int? Nr)
        {
            if (Nr == null) return null;
            try
            {
                return int.Parse((from k in Program.karMedlemmer where k.Nr == Nr select k).First().Krdktonr);

            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
