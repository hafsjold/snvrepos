using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
            int AntalOrdre = 0;
            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "order2summa");
            XDocument xmldata = XDocument.Parse(strxmldata);
            string Status = xmldata.Descendants("Status").First().Value;
            if (Status != "True") return AntalOrdre;

            var qry_ord = from forslag in xmldata.Descendants("Fak") select forslag;
            AntalOrdre = qry_ord.Count();

            /*
            var qry_ord = from f in Program.dbData3060.Tblfak
                          where f.SFakID == null && Startdato <= f.Betalingsdato && f.Betalingsdato <= Slutdato
                          join b in Program.dbData3060.Tblbetlin on f.Faknr equals b.Faknr
                          where b.Pbstranskode == "0236" || b.Pbstranskode == "0297"
                          select new { 
                              f.Id, 
                              Pbsfaknr = f.Faknr,
                              f.Nr,
                              f.Advisbelob,
                              f.Betalingsdato,
                              f.Vnr,
                              f.Bogfkonto, 
                              b.Indbetalingsdato
                          };

            AntalOrdre = qry_ord.Count();
            */

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

                XElement SFakIDupdatexml = new XElement("SFakIDupdate");

                foreach (var o in qry_ord)
                {
                    string Key = o.Descendants("Key").First().Value;
                    string strval = o.Descendants("Id").First().Value;
                    int Id = int.Parse(strval);
                    strval = o.Descendants("Nr").First().Value;
                    int Nr = int.Parse(strval);
                    strval = o.Descendants("Advisbelob").First().Value;
                    double Advisbelob = double.Parse(strval.Replace('.', ','));
                    strval = o.Descendants("Pbsfaknr").First().Value;
                    int Pbsfaknr = int.Parse(strval);
                    strval = o.Descendants("Vnr").First().Value;
                    int Vnr = int.Parse(strval);
                    strval = o.Descendants("Bogfkonto").First().Value;
                    int Bogfkonto = int.Parse(strval);

                    SidsteSFakID++;
                    SidsteRec_no++;
                    int orebelob = (int)Advisbelob * 100;

                    ordtype_s ord = new ordtype_s
                    (
                        SidsteSFakID,          //fakid
                        ToDay,                 //(o.Betalingsdato > o.Indbetalingsdato) ? (DateTime)o.Betalingsdato : (DateTime)o.Indbetalingsdato, //dato
                        ToDay,                 //(DateTime)o.Betalingsdato, //forfaldsdato
                        orebelob,              //fakbeløb i øre
                        (int)Nr2Debktonr(Nr) //debitornr
                    );
                    recFakturaer_s rec = new recFakturaer_s { rec_no = SidsteRec_no, rec_data = ord };
                    Program.karFakturaer_s.Add(rec);

                    var m_rec = (from m in Program.karMedlemmer where m.Nr == Nr select m).First();
                    recFakturastr_s rec_Fakturastr_s = new recFakturastr_s
                    {
                        Fakid = SidsteSFakID.ToString(),
                        Navn = m_rec.Navn,
                        Adresse = m_rec.Adresse,
                        Postnr = m_rec.Postnr,
                        Bynavn = m_rec.Bynavn,
                        Faknr = (int)Pbsfaknr,
                        Email = m_rec.Email
                    };
                    Program.karFakturastr_s.Add(rec_Fakturastr_s);

                    recFakturavarer_s rec_Fakturavarer_s = new recFakturavarer_s
                    {
                        Fakid = SidsteSFakID.ToString(),
                        Varenr = (int)Vnr,
                        VareTekst = "Puls 3060 kontingent",
                        Bogfkonto = (int)Bogfkonto,
                        Antal = 1,
                        Fakturabelob = (double)Advisbelob
                    };
                    Program.karFakturavarer_s.Add(rec_Fakturavarer_s);

                    try
                    {
                        XElement fakxml = new XElement("Fak");
                        fakxml.Add(new XElement("Key", Key));
                        fakxml.Add(new XElement("Id", Id));
                        fakxml.Add(new XElement("SFakID", SidsteSFakID));
                        SFakIDupdatexml.Add(new XElement(fakxml));
                    }
                    catch (System.InvalidOperationException)
                    {
                        throw;
                    }
                }
                string strSFakIDupdatexml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + SFakIDupdatexml.ToString();
                string result = objRest.HttpPost2(clsRest.urlBaseType.data, "order2summa", strSFakIDupdatexml);

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
            }
            return AntalOrdre;
        }

        public int OrderFaknrUpdate()
        {
            var qry = from k in Program.karFakturaer_s
                      where k.faknr > 0
                      join f in Program.dbData3060.Tblfak on k.fakid equals f.SFakID
                      where f.SFaknr == null
                      select new { f.Id, SFaknr = k.faknr };

            int AntalFakturaer = qry.Count();
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
            return AntalFakturaer;
        }

        public int BogforIndBetalinger()
        {
            int saveBetid = 0;
            var bogf = from s in Program.karFakturaer_s
                       //where s.saldo != 0
                       join f in Program.dbData3060.Tblfak on s.fakid equals f.SFakID
                       where f.SFaknr != null
                       join m in Program.karMedlemmer on f.Nr equals m.Nr
                       join bl in Program.dbData3060.Tblbetlin on f.Faknr equals bl.Faknr
                       join b in Program.dbData3060.Tblbet on bl.Betid equals b.Id
                       where b.Summabogfort != true
                       join p in Program.dbData3060.Tblfrapbs on b.Frapbsid equals p.Id
                       orderby p.Id, b.Id, bl.Id
                       select new
                       {
                           Frapbsid = p.Id,
                           p.Leverancespecifikation,
                           Betid = b.Id,
                           GruppeIndbetalingsbelob = b.Indbetalingsbelob,
                           Betlinid = bl.Id,
                           Fakid = f.Id,
                           bl.Betalingsdato,
                           bl.Indbetalingsdato,
                           m.Navn,
                           bl.Indbetalingsbelob,
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
                            Tekst = "Indbetalingskort K 81131945-" + ((DateTime)b.Indbetalingsdato).Day + "." + ((DateTime)b.Indbetalingsdato).Month,
                            Afstemningskonto = "Bank",
                            Belob = b.GruppeIndbetalingsbelob,
                            Kontonr = null,
                            Faknr = null
                        };
                        Program.karKladde.Add(gkl);

                        var rec_bet = (from ub in Program.dbData3060.Tblbet where ub.Id == b.Betid select ub).First();
                        rec_bet.Summabogfort = true;

                    }

                    recKladde kl = new recKladde
                    {
                        Dato = ToDay,  //(b.Betalingsdato > b.Indbetalingsdato) ? (DateTime)b.Betalingsdato : (DateTime)b.Indbetalingsdato,
                        Bilag = BS1_SidsteNr,
                        Tekst = "F" + b.SFaknr + " " + b.Navn,
                        Afstemningskonto = null,
                        Belob = b.Indbetalingsbelob,
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
            int AntalBetalinger = 0;
            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "udbetaling2summa/" + lobnr.ToString());
            XDocument xmldata = XDocument.Parse(strxmldata);
            string Status2 = xmldata.Descendants("Status").First().Value;
            if (Status2 == "True")
            {
                var overforsel = from o in xmldata.Descendants("Overforsel")
                                   select new
                                   {
                                       Id = clsPassXmlDoc.attr_val_int(o, "Id"),
                                       Nr = clsPassXmlDoc.attr_val_int(o, "Nr"),
                                       SFaknr = clsPassXmlDoc.attr_val_int(o, "SFaknr"),
                                       SFakID = clsPassXmlDoc.attr_val_int(o, "SFakID"),
                                       Tilpbsid = clsPassXmlDoc.attr_val_int(o, "Tilpbsid"),
                                       Betalingsdato = clsPassXmlDoc.attr_val_datetime(o, "Betalingsdato"),
                                       Advisbelob = (decimal)clsPassXmlDoc.attr_val_double(o, "Advisbelob"),
                                   };
                
                var bogf = from f in Program.karFakturaer_k
                           where f.saldo != 0
                           join o in overforsel on f.fakid equals o.SFakID
                           where o.Tilpbsid == lobnr
                           join m in Program.karMedlemmer on o.Nr equals m.Nr
                           orderby o.Betalingsdato ascending
                           select new
                           {
                               Fakid = o.Id,
                               m.Navn,
                               o.SFaknr,
                               o.Betalingsdato,
                               o.Advisbelob
                           };
                AntalBetalinger = bogf.Count();
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
                            Dato = clsUtil.bankdageplus((DateTime)b.Betalingsdato, -1),
                            Bilag = ++BS1_SidsteNr,
                            Tekst = "Overførsel",
                            Afstemningskonto = "Bank",
                            Belob = -b.Advisbelob,
                            Kontonr = null,
                            Faknr = null
                        };
                        Program.karKladde.Add(gkl);
                        recKladde kl = new recKladde
                        {
                            Dato = clsUtil.bankdageplus((DateTime)b.Betalingsdato, -1),
                            Bilag = BS1_SidsteNr,
                            Tekst = "KF" + b.SFaknr + " " + b.Navn,
                            Afstemningskonto = null,
                            Belob = -b.Advisbelob,
                            Kontonr = 65100,
                            Faknr = b.SFaknr
                        };
                        Program.karKladde.Add(kl);
                    }

                    Program.karStatus.save();
                    Program.karKladde.save();
                }
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
