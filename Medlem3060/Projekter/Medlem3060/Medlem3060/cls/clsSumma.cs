using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nsPuls3060
{
    class clsSumma
    {
        public void Order2Summa()
        {
            var qry_ord = from f in Program.dbData3060.Tblfak
                          where f.SFakID == null
                          join b in Program.dbData3060.Tblbetlin on f.Faknr equals b.Faknr
                          where b.Pbstranskode == "0236" || b.Pbstranskode == "0297"
                          select new { f.Id, Pbsfaknr = f.Faknr, f.Nr, f.Advisbelob, f.Betalingsdato, f.Vnr, f.Bogfkonto, b.Indbetalingsdato };


            if (qry_ord.Count() > 0)
            {
                var SidsteSFakID = (from f in Program.karFakturaer_s select f.fakid).Max();
                var SidsteRec_no = (from f in Program.karFakturaer_s select f.rec_no).Max();

                Program.karFakturastr_s = null;
                Program.karFakturavarer_s = null;

                foreach (var o in qry_ord)
                {
                    SidsteSFakID++;
                    SidsteRec_no++;
                    int orebelob = (int)o.Advisbelob * 100;
                    ordtype ord = new ordtype
                    (
                        SidsteSFakID,               //fakid
                        (o.Betalingsdato > o.Indbetalingsdato) ? (DateTime)o.Betalingsdato : (DateTime)o.Indbetalingsdato, //dato
                        (DateTime)o.Betalingsdato, //forfaldsdato
                        orebelob,                  //fakbeløb i øre
                        100000 + (int)o.Nr         //debitornr
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
                        Varenr = (int)o.Vnr,
                        VareTekst = "Puls 3060 kontingent",
                        Bogfkonto = (int)o.Bogfkonto,
                        Antal = 1,
                        Fakturabelob = (double)o.Advisbelob
                    };
                    Program.karFakturavarer_s.Add(rec_Fakturavarer_s);

                    try
                    {
                        Tblfak rec_fak = (from f in Program.dbData3060.Tblfak
                                          where f.Id == o.Id
                                          select f).First();
                        rec_fak.SFakID = SidsteSFakID;
                        rec_fak.Betalt = true;
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
        }

        public void OrderFaknrUpdate()
        {
            var qry = from k in Program.karFakturaer_s
                      join f in Program.dbData3060.Tblfak on k.fakid equals f.SFakID
                      where f.SFaknr == null
                      select new { f.Id, SFaknr = k.faknr};
            
            if (qry.Count() > 0)
            {
                foreach (var k in qry)
                {
                    try
                    {
                        Tblfak rec_fak = (from f in Program.dbData3060.Tblfak
                                          where f.Id == k.Id
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
        }
    }
}
