using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using nsPbs3060;

namespace nsPuls3060
{
    class clsSumma
    {
        public int BogforIndBetalinger()
        {
            var rec_regnskab = Program.qryAktivRegnskab();
            if (rec_regnskab.Afsluttet == true) return 0;

            DateTime? Startdato = rec_regnskab.Start;
            DateTime? Slutdato = rec_regnskab.Slut;
            if (rec_regnskab.DatoLaas != null)
            {
                if (rec_regnskab.DatoLaas > Startdato) Startdato = rec_regnskab.DatoLaas;
            }
            int saveBetid = 0;
            /*
            var bogf = from f in Program.dbData3060.tblfaks
                       where Startdato <= f.betalingsdato && f.betalingsdato <= Slutdato
                       join m in Program.dbData3060.tblMedlems on f.Nr equals m.Nr
                       join bl in Program.dbData3060.tblbetlins on f.faknr equals bl.faknr
                       where bl.pbstranskode == "0236" || bl.pbstranskode == "0297"
                       join b in Program.dbData3060.tblbets on bl.betid equals b.id
                       where b.summabogfort == null ||  b.summabogfort == false
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
                           f.Nr,
                           f.bogfkonto,
                           f.faknr
                       };
            */
            var bogf = from bl in Program.dbData3060.tblbetlins
                       where bl.pbstranskode == "0236" || bl.pbstranskode == "0297"
                       join b in Program.dbData3060.tblbets on bl.betid equals b.id
                       where b.summabogfort == null || b.summabogfort == false
                       join p in Program.dbData3060.tblfrapbs on b.frapbsid equals p.id
                       orderby p.id, b.id, bl.id
                       select new
                       {
                           Frapbsid = p.id,
                           p.leverancespecifikation,
                           Betid = b.id,
                           GruppeIndbetalingsbelob = b.indbetalingsbelob,
                           Betlinid = bl.id,
                           bl.betalingsdato,
                           bl.indbetalingsdato,
                           bl.indbetalingsbelob,
                           bl.faknr,
                           bl.debitorkonto
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
                            Dato = ToDay,
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

                    var fak = from f in Program.dbData3060.tblfaks
                              where f.faknr == b.faknr
                              join m in Program.dbData3060.tblMedlems on f.Nr equals m.Nr
                              select new { f.faknr, f.Nr, m.Navn, f.bogfkonto };
                    
                    var bet = from bi in Program.dbData3060.tblbetalingsidentifikations
                              where bi.betalingsidentifikation == b.debitorkonto
                              join m in Program.dbData3060.tblMedlems on bi.Nr equals m.Nr
                              select new {bi.Nr, m.Navn, bi.bogfkonto };

                    if (fak.Count() == 1) //Kontingent betaling
                    {
                        var f = fak.First();
                        recKladde kl = new recKladde
                        {
                            Dato = ToDay,
                            Bilag = BS1_SidsteNr,
                            Tekst = ("F" + f.faknr + " " + f.Nr + " " + f.Navn).PadRight(40, ' ').Substring(0, 40),
                            Afstemningskonto = null,
                            Belob = b.indbetalingsbelob,
                            Kontonr = f.bogfkonto,
                            Faknr = null
                        };
                        Program.karKladde.Add(kl);
                    }
                    else if (bet.Count() == 1) //Betaling med betalingsidentifikation
                    {
                        var f = bet.First();
                        recKladde kl = new recKladde
                        {
                            Dato = ToDay,
                            Bilag = BS1_SidsteNr,
                            Tekst = (f.Nr + " " + f.Navn).PadRight(40, ' ').Substring(0, 40),
                            Afstemningskonto = null,
                            Belob = b.indbetalingsbelob,
                            Kontonr = f.bogfkonto,
                            Faknr = null
                        };
                        Program.karKladde.Add(kl);
                    }
                    else //Anden betaling
                    {
                        recKladde kl = new recKladde
                        {
                            Dato = ToDay,
                            Bilag = BS1_SidsteNr,
                            Tekst = ("Ukendt betaling").PadRight(40, ' ').Substring(0, 40),
                            Afstemningskonto = null,
                            Belob = b.indbetalingsbelob,
                            Kontonr = 65050,
                            Faknr = null
                        };
                        Program.karKladde.Add(kl);
                    }
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
                       //join o in Program.dbData3060.tbloverforsels on f.fakid equals o.SFakID
                       join o in Program.dbData3060.tbloverforsels on f.faknr equals o.SFaknr
                       where o.tilpbsid == lobnr
                       join m in Program.dbData3060.tblMedlems on o.Nr equals m.Nr
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
