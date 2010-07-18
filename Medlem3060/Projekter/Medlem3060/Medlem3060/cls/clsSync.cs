using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace nsPuls3060
{
    public class clsLog2
    {
        public byte Source;
        public int? Id;
        public int? Nr;
        public DateTime? Logdato;
        public int? Akt_id;
        public DateTime? Akt_dato;
    }
    
    public class clsSync
    {
        private int m_action;
        public void actionSync(int action)
        {
            m_action = action;
            if (m_action == 1) Program.dbData3060.ExecuteCommand("DELETE FROM tblsync;");
            if (m_action == 2) Program.dbData3060.ExecuteCommand("DELETE FROM tempsync;");
            actionMedlemSync();
            actionMedlemlogSync();
        }
        private void actionMedlemSync()
        {
            var medlem = from m1 in Program.karMedlemmer
                         join m2 in Program.dbData3060.TblMedlem on m1.Nr equals m2.Nr into medlem2
                         from m2 in medlem2.DefaultIfEmpty()
                         select new
                         {
                             Nr = m1.Nr,
                             Navn = m1.Navn,
                             Kaldenavn = m1.Kaldenavn,
                             Adresse = m1.Adresse,
                             Postnr = m1.Postnr,
                             Bynavn = m1.Bynavn,
                             Telefon = m1.Telefon,
                             Email = m1.Email,
                             Kon = m2.Kon,
                             FodtDato = m2.FodtDato,
                             Bank = m1.Bank,
                         };
            Tblsync s;
            foreach (var m in medlem)
            {
                //Nr
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.medlem_nr,
                    Value = getString(m.Nr),
                };
                action(s);

                //Navn
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.navn,
                    Value = getString(m.Navn)
                };
                action(s);

                //Kaldenavn
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.kaldenavn,
                    Value = getString(m.Kaldenavn)
                };
                action(s);

                //Adresse
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.adresse,
                    Value = getString(m.Adresse)
                };
                action(s);

                //Postnr
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.postnr,
                    Value = getString(m.Postnr)
                };
                action(s);

                //Bynavn
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.bynavn,
                    Value = getString(m.Bynavn)
                };
                action(s);

                //Telefon
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.telefon,
                    Value = getString(m.Telefon)
                };
                action(s);

                //Email
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.email,
                    Value = getString(m.Email)
                };
                action(s);

                //Kon
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.kon,
                    Value = getString(m.Kon)
                };
                action(s);

                //FodtDato
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.fodtdato,
                    Value = getString(m.FodtDato)
                };
                action(s);

                //Bank
                s = new Tblsync
                {
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.bank,
                    Value = getString(m.Bank)
                };
                action(s);
            }
            Program.dbData3060.SubmitChanges();
        }
        
        private void actionMedlemlogSync()
        {
            var qryMedlemLog = from m in Program.dbData3060.TblMedlemLog
                               select new clsLog2
                               {
                                   Source = (byte)tblsource.medlemlog,
                                   Id = (int?)m.Id,
                                   Nr = (int?)m.Nr,
                                   Logdato = (DateTime?)m.Logdato,
                                   Akt_id = (int?)m.Akt_id,
                                   Akt_dato = (DateTime?)m.Akt_dato
                               };
            var qryFak = from f in Program.dbData3060.Tblfak
                         join p in Program.dbData3060.Tbltilpbs on f.Tilpbsid equals p.Id
                         select new clsLog2
                         {
                             Source = (byte)tblsource.fak,
                             Id = (int?)f.Id,
                             Nr = (int?)f.Nr,
                             Logdato = (DateTime)p.Bilagdato,
                             Akt_id = (int?)20,
                             Akt_dato = (DateTime?)f.Betalingsdato
                         };

            var qryBetlin = from b in Program.dbData3060.Tblbetlin
                            join f in Program.dbData3060.Tblfak on b.Faknr equals f.Faknr
                            where b.Pbstranskode == "0236" || b.Pbstranskode == "0297"
                            select new clsLog2
                            {
                                Source = (byte)tblsource.betlin,
                                Id = (int?)b.Id,
                                Nr = (int?)b.Nr,
                                Logdato = (DateTime?)b.Indbetalingsdato,
                                Akt_id = (int?)30,
                                Akt_dato = (DateTime?)f.Tildato
                            };

            var qryBetlin40 = from b in Program.dbData3060.Tblbetlin
                              where b.Pbstranskode == "0237"
                              select new clsLog2
                              {
                                  Source = (byte)tblsource.betlin40,
                                  Id = (int?)b.Id,
                                  Nr = (int?)b.Nr,
                                  Logdato = (DateTime?)(((DateTime)b.Betalingsdato).AddSeconds(-30)),  //Workaround for problem med samme felt (b.Betalingsdato) 2 gange
                                  Akt_id = (int?)40,
                                  Akt_dato = (DateTime?)b.Betalingsdato
                              };


            var medlemlog = qryMedlemLog.Union(qryFak)
                                           .Union(qryBetlin)
                                           .Union(qryBetlin40);

            
            
            
            Tblsync s;
            foreach (var l in medlemlog)
            {
                //Id
                s = new Tblsync
                {
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.medlemlog_id,
                    Value = getString(l.Id)
                };
                action(s);

                //Nr
                s = new Tblsync
                {
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.medlemlog_nr,
                    Value = getString(l.Nr)
                };
                action(s);

                //Logdato
                s = new Tblsync
                {
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.logdato,
                    Value = getString(l.Logdato)
                };
                action(s);

                //Akt_id
                s = new Tblsync
                {
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.akt_id,
                    Value = getString(l.Akt_id)
                };
                action(s);

                //Akt_dato
                s = new Tblsync
                {
                    Source = l.Source,
                    Source_id = (int)l.Id,
                    Field_id = (byte)tblfield.akt_dato,
                    Value = getString(l.Akt_dato)
                };
                action(s);
            }
            Program.dbData3060.SubmitChanges();
        }

        private void action(Tblsync s)
        {
            if (m_action == 1) Program.dbData3060.Tblsync.InsertOnSubmit(s);
            if (m_action == 2)
            {
                Tempsync ts = new Tempsync
                {
                    Source = s.Source,
                    Source_id = s.Source_id,
                    Field_id = s.Field_id,
                    Value = s.Value
                };
                Program.dbData3060.Tempsync.InsertOnSubmit(ts);
            }
        }

        public void toxml() 
        {
            XElement xml = new XElement("syncs",
                from p in Program.dbData3060.Tempsync
                select new XElement("sync",
                    new XAttribute("s", p.Source),
                    new XAttribute("s_id", p.Source_id),
                    new XAttribute("f_id", p.Field_id),
                    new XAttribute("v", p.Value),
                    new XAttribute("a", "add")
                    ));
            xml.Save(@"c:\mysync.xml");     
        }

        public void medlemxmldelete()
        {
            clsRest objRest = new clsRest();
            string strxml = objRest.HttpGet2("Medlem");
            
            XElement list = XElement.Parse(strxml);
            var medlem = from m in list.Elements("Medlem") 
                         select new 
                         {
                             Key = (string)m.Element("key"),
                             Nr = (string)m.Element("Nr")
                         };
            int antal = medlem.Count();
            foreach (var m in medlem) 
            {
                string delstrxml = objRest.HttpDelete2("Medlem/" + m.Key);
            }
        }
        
        public void medlemxml()
        {

            var medlem = from m1 in Program.karMedlemmer
                         join m2 in Program.dbData3060.TblMedlem on m1.Nr equals m2.Nr into medlem2
                         from m2 in medlem2.DefaultIfEmpty()
                         select new
                         {
                             Nr = m1.Nr,
                             Navn = m1.Navn,
                             Kaldenavn = m1.Kaldenavn,
                             Adresse = m1.Adresse,
                             Postnr = m1.Postnr,
                             Bynavn = m1.Bynavn,
                             Telefon = m1.Telefon,
                             Email = m1.Email,
                             Kon = m2.Kon,
                             FodtDato = m2.FodtDato,
                             Bank = m1.Bank,
                         };
            clsRest objRest = new clsRest();
            int antal = medlem.Count();
            foreach (var m in medlem) 
            {
                XElement xml = new XElement("Medlem",
                                 new XElement("key", ""),
                                 new XElement("Nr", m.Nr),
                                 new XElement("Navn", m.Navn),
                                 new XElement("Kaldenavn", m.Kaldenavn),
                                 new XElement("Adresse", m.Adresse),
                                 new XElement("Postnr", m.Postnr),
                                 new XElement("Bynavn", m.Bynavn),
                                 new XElement("Telefon", m.Telefon),
                                 new XElement("Email", m.Email),
                                 new XElement("Kon", m.Kon),
                                 new XElement("FodtDato", ((DateTime)m.FodtDato).ToString("yyyy-MM-dd")),
                                 new XElement("Bank", m.Bank)
                         );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Medlem", strxml);

            
            }

        }

        public void medlemlogxml()
        {
            var qryMedlemLog = from m in Program.dbData3060.TblMedlemLog
                               select new clsLog2
                               {
                                   Source = (byte)tblsource.medlemlog,
                                   Id = (int?)m.Id,
                                   Nr = (int?)m.Nr,
                                   Logdato = (DateTime?)m.Logdato,
                                   Akt_id = (int?)m.Akt_id,
                                   Akt_dato = (DateTime?)m.Akt_dato
                               };
            var qryFak = from f in Program.dbData3060.Tblfak
                         join p in Program.dbData3060.Tbltilpbs on f.Tilpbsid equals p.Id
                         select new clsLog2
                         {
                             Source = (byte)tblsource.fak,
                             Id = (int?)f.Id,
                             Nr = (int?)f.Nr,
                             Logdato = (DateTime)p.Bilagdato,
                             Akt_id = (int?)20,
                             Akt_dato = (DateTime?)f.Betalingsdato
                         };

            var qryBetlin = from b in Program.dbData3060.Tblbetlin
                            join f in Program.dbData3060.Tblfak on b.Faknr equals f.Faknr
                            where b.Pbstranskode == "0236" || b.Pbstranskode == "0297"
                            select new clsLog2
                            {
                                Source = (byte)tblsource.betlin,
                                Id = (int?)b.Id,
                                Nr = (int?)b.Nr,
                                Logdato = (DateTime?)b.Indbetalingsdato,
                                Akt_id = (int?)30,
                                Akt_dato = (DateTime?)f.Tildato
                            };

            var qryBetlin40 = from b in Program.dbData3060.Tblbetlin
                              where b.Pbstranskode == "0237"
                              select new clsLog2
                              {
                                  Source = (byte)tblsource.betlin40,
                                  Id = (int?)b.Id,
                                  Nr = (int?)b.Nr,
                                  Logdato = (DateTime?)(((DateTime)b.Betalingsdato).AddSeconds(-30)),  //Workaround for problem med samme felt (b.Betalingsdato) 2 gange
                                  Akt_id = (int?)40,
                                  Akt_dato = (DateTime?)b.Betalingsdato
                              };


            var medlemlog = qryMedlemLog.Union(qryFak)
                                        .Union(qryBetlin)
                                        .Union(qryBetlin40);




            clsRest objRest = new clsRest();
            int antal = medlemlog.Count();
            foreach (var l in medlemlog)
            {
                XElement xml = new XElement("Medlemlog",
                                 new XElement("Source", l.Source),
                                 new XElement("Source_id", l.Id),
                                 new XElement("Nr", l.Nr),
                                 new XElement("Logdato", ((DateTime)l.Logdato).ToString("yyyy-MM-ddTHH:mm:ss")),
                                 new XElement("Akt_id", l.Akt_id),
                                 new XElement("Akt_dato", ((DateTime)l.Akt_dato).ToString("yyyy-MM-ddTHH:mm:ss"))
                         );
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Medlemlog", strxml);


            }
        }
        //********************************************************************************
        //********************************************************************************
        private string getString(string Value)
        {
            if (Value == null) return null;
            return Value;
        }

        private string getString(int? Value)
        {
            if (Value == null) return null;
            return getString((int)Value);
        }

        private string getString(int Value)
        {
            return Value.ToString();
        }

        private string getString(DateTime? Value)
        {
            if (Value == null) return null;
            return getString((DateTime)Value);
        }

        private string getString(DateTime Value)
        {
            return Value.ToString();
        }

        public enum tblsource : byte
        {
            medlem = 1,
            medlemlog = 2,
            fak = 3,
            betlin = 4,
            betlin40 = 5,
        }

        public enum tblfield : byte
        {
            medlem_nr = 1,
            navn = 2,
            kaldenavn = 3,
            adresse = 4,
            postnr = 5,
            bynavn = 6,
            telefon = 7,
            email = 8,
            kon = 9,
            fodtdato = 10,
            bank = 11,

            sourceid = 12,
            medlemlog_id = 13,
            logdato = 14,
            akt_id = 15,
            akt_dato = 16,
            medlemlog_nr = 17
        }


    }
}
