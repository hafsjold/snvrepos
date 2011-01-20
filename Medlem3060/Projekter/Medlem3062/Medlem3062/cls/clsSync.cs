using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;

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

    public class clsImEksportAppEngMedlem
    {
        public clsImEksportAppEngMedlem()
        {
            bNr = false;
            bNavn = false;
            bKaldenavn = false;
            bAdresse = false;
            bPostnr = false;
            bBynavn = false;
            bEmail = false;
            bTelefon = false;
            bKon = false;
            bFodtDato = false;
            bBank = false;
        }

        public ImpExp ieAction { get; set; }
        public string Act { get; set; }
        public bool bNr { get; set; }
        public bool bNavn { get; set; }
        public bool bKaldenavn { get; set; }
        public bool bAdresse { get; set; }
        public bool bPostnr { get; set; }
        public bool bBynavn { get; set; }
        public bool bEmail { get; set; }
        public bool bTelefon { get; set; }
        public bool bKon { get; set; }
        public bool bFodtDato { get; set; }
        public bool bBank { get; set; }
        public int? Nr { get; set; }
        public string Navn { get; set; }
        public string Kaldenavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Kon { get; set; }
        public DateTime? FodtDato { get; set; }
        public string Bank { get; set; }
        public void ExecuteImEksport()
        {
            if (ieAction == ImpExp.fdImport) Import();
            else Eksport();
        }
        private void Eksport()
        {
            clsRest objRest = new clsRest();
            if (Act == "del")
            {
                string retur = objRest.HttpDelete2("Medlem/" + Nr);
            }
            else
            {
                XElement xml = new XElement("Medlem", new XElement("Nr", Nr));
                if (bNavn) xml.Add(new XElement("Navn", Navn));
                if (bKaldenavn) xml.Add(new XElement("Kaldenavn", Kaldenavn));
                if (bAdresse) xml.Add(new XElement("Adresse", Adresse));
                if (bPostnr) xml.Add(new XElement("Postnr", Postnr));
                if (bBynavn) xml.Add(new XElement("Bynavn", Bynavn));
                if (bTelefon) xml.Add(new XElement("Telefon", Telefon));
                if (bEmail) xml.Add(new XElement("Email", Email));
                if (bKon) xml.Add(new XElement("Kon", Kon));
                if (bFodtDato) xml.Add(new XElement("FodtDato", ((DateTime)FodtDato).ToString("yyyy-MM-dd")));
                if (bBank) xml.Add(new XElement("Bank", Bank));
                string strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                string retur = objRest.HttpPost2("Medlem", strxml);
            }

        }
        private void Import()
        {
            object[] val = null;
            try
            {
                DataRow row = Program.dsMedlemImport.Kartotek.Rows.Find(Nr);
                val = row.ItemArray;
                if (bNavn) val[1] = Navn;
                if (bKaldenavn) val[2] = Kaldenavn;
                if (bAdresse) val[3] = Adresse;
                if (bPostnr) val[4] = Postnr;
                if (bBynavn) val[5] = Bynavn;
                if (bTelefon) val[6] = Telefon;
                if (bEmail) val[7] = Email;
                if (bKon) val[8] = Kon;
                if (bFodtDato) val[9] = FodtDato;
                if (bBank) val[10] = Bank;

                row.BeginEdit();
                row.ItemArray = val;
                row.EndEdit();
            }
            //catch (MissingPrimaryKeyException e)
            catch (NullReferenceException e)
            {
                e.GetType();
                val = new object[11];
                val[0] = Nr;
                if (bNavn) val[1] = Navn;
                if (bKaldenavn) val[2] = Kaldenavn;
                if (bAdresse) val[3] = Adresse;
                if (bPostnr) val[4] = Postnr;
                if (bBynavn) val[5] = Bynavn;
                if (bTelefon) val[6] = Telefon;
                if (bEmail) val[7] = Email;
                if (bKon) val[8] = Kon;
                if (bFodtDato) val[9] = FodtDato;
                if (bBank) val[10] = Bank;
                Program.dsMedlemImport.Kartotek.Rows.Add(val);
            }
            Program.dsMedlemImport.savedsMedlem();
        }
    }

    public enum ImpExp : int
    {
        fdImport = 1,
        fdEksport = 2
    }

    public class clsSync
    {
        private int m_action;
        public void actionSync(int action)
        {
            m_action = action;
            if (m_action == 1)
            {
                Program.dbData3060.Tblsync.DeleteAllOnSubmit(Program.dbData3060.Tblsync);
            }
            if (m_action == 2)
            {
                Program.dbData3060.Tempsync.DeleteAllOnSubmit(Program.dbData3060.Tempsync);
            }
            if (m_action == 3)
            {
                Program.dbData3060.Tempsync2.DeleteAllOnSubmit(Program.dbData3060.Tempsync2);
            }

            Program.dbData3060.SubmitChanges();
            /*
            try
            {
                Program.dbData3060.SubmitChanges(ConflictMode.ContinueOnConflict);
            }

            catch (ChangeConflictException e)
            {
                // Automerge database values for members that client has not modified.
                foreach (ObjectChangeConflict occ in Program.dbData3060.ChangeConflicts)
                {
                    occ.Resolve(RefreshMode.KeepChanges);
                }
            }
            // Submit succeeds on second try.
            Program.dbData3060.SubmitChanges(ConflictMode.FailOnFirstConflict);
            */

            if ((m_action == 1) | (m_action == 2))
            {
                actionMedlemSync();
            }
            if (m_action == 3)
            {
                actionMedlemxmlSync();
            }
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
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.medlem_nr,
                    Value = getString(m.Nr),
                };
                action(s);

                //Navn
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.navn,
                    Value = getString(m.Navn)
                };
                action(s);

                //Kaldenavn
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.kaldenavn,
                    Value = getString(m.Kaldenavn)
                };
                action(s);

                //Adresse
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.adresse,
                    Value = getString(m.Adresse)
                };
                action(s);

                //Postnr
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.postnr,
                    Value = getString(m.Postnr)
                };
                action(s);

                //Bynavn
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.bynavn,
                    Value = getString(m.Bynavn)
                };
                action(s);

                //Telefon
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.telefon,
                    Value = getString(m.Telefon)
                };
                action(s);

                //Email
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.email,
                    Value = getString(m.Email)
                };
                action(s);

                //Kon
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.kon,
                    Value = getString(m.Kon)
                };
                action(s);

                //FodtDato
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.fodtdato,
                    Value = getString(m.FodtDato, "yyyy-MM-dd")
                };
                action(s);

                //Bank
                s = new Tblsync
                {
                    Nr = (int)m.Nr,
                    Source = (byte)tblsource.medlem,
                    Source_id = (int)m.Nr,
                    Field_id = (byte)tblfield.bank,
                    Value = getString(m.Bank)
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
                    Nr = s.Nr,
                    Source = s.Source,
                    Source_id = s.Source_id,
                    Field_id = s.Field_id,
                    Value = s.Value
                };
                Program.dbData3060.Tempsync.InsertOnSubmit(ts);
            }
            if (m_action == 3)
            {
                Tempsync2 ts = new Tempsync2
                {
                    Nr = s.Nr,
                    Source = s.Source,
                    Source_id = s.Source_id,
                    Field_id = s.Field_id,
                    Value = s.Value
                };
                Program.dbData3060.Tempsync2.InsertOnSubmit(ts);
            }
        }

        public void actionMedlemxmlSync()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2("Medlem");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from person in xdoc.Descendants("Person") select person;
            int antal = list.Count();
            Tblsync s;
            foreach (var person in list)
            {

                if ((person.Descendants("Nr").First().Value != null) && (person.Descendants("Nr").First().Value != "None"))
                {
                    var Nr = person.Descendants("Nr").First().Value;
                    var Navn = person.Descendants("Navn").First().Value;
                    var Kaldenavn = person.Descendants("Kaldenavn").First().Value;
                    var Adresse = person.Descendants("Adresse").First().Value;
                    var Postnr = person.Descendants("Postnr").First().Value;
                    var Bynavn = person.Descendants("Bynavn").First().Value;
                    var Telefon = person.Descendants("Telefon").First().Value;
                    var Email = person.Descendants("Email").First().Value;
                    var Kon = person.Descendants("Kon").First().Value;
                    var FodtDato = person.Descendants("FodtDato").First().Value;
                    var Bank = person.Descendants("Bank").First().Value;

                    //Nr
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.medlem_nr,
                        Value = Nr,
                    };
                    action(s);

                    //Navn
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.navn,
                        Value = Navn
                    };
                    action(s);

                    //Kaldenavn
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.kaldenavn,
                        Value = Kaldenavn
                    };
                    action(s);

                    //Adresse
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.adresse,
                        Value = Adresse
                    };
                    action(s);

                    //Postnr
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.postnr,
                        Value = Postnr
                    };
                    action(s);

                    //Bynavn
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.bynavn,
                        Value = Bynavn
                    };
                    action(s);

                    //Telefon
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.telefon,
                        Value = Telefon
                    };
                    action(s);

                    //Email
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.email,
                        Value = Email
                    };
                    action(s);

                    //Kon
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.kon,
                        Value = Kon
                    };
                    action(s);

                    //FodtDato
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.fodtdato,
                        Value = FodtDato
                    };
                    action(s);

                    //Bank
                    s = new Tblsync
                    {
                        Nr = int.Parse(Nr),
                        Source = (byte)tblsource.medlem,
                        Source_id = int.Parse(Nr),
                        Field_id = (byte)tblfield.bank,
                        Value = Bank
                    };
                    action(s);
                }
            }
            Program.dbData3060.SubmitChanges();
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

        //********************************************************************************
        //********************************************************************************

        private string getString(string Value)
        {
            if (Value == null) return "None";
            if (Value.Length == 0) return "None";
            return Value;
        }

        private string getString(int? Value)
        {
            if (Value == null) return "None";
            return getString((int)Value);
        }

        private string getString(int Value)
        {
            return Value.ToString();
        }

        private string getString(DateTime? Value)
        {
            if (Value == null) return "None";
            return getString((DateTime)Value);
        }

        private string getString(DateTime Value)
        {
            return Value.ToString("yyyy-MM-dd");
        }

        private string getString(DateTime? Value, string Format)
        {
            if (Value == null) return "None";
            return getString((DateTime)Value, Format);
        }

        private string getString(DateTime Value, string Format)
        {
            return Value.ToString(Format);
        }

        public enum tblsource : byte
        {
            medlem = 254,
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
        }

        internal void importeksport(ImpExp ieAction)
        {
            IOrderedQueryable<Tempimpexp> imp = null;
            if (ieAction == ImpExp.fdImport)
            {
                imp = from t in Program.dbData3060.Tempimpexp
                      where t.Ie == "i"
                      && t.Source == 254
                      && t.Act != "del"
                      orderby t.Nr, t.Source descending, t.Source_id, t.Field_id
                      select t;
            }
            if (ieAction == ImpExp.fdEksport)
            {
                imp = from t in Program.dbData3060.Tempimpexp
                      where t.Ie == "e"
                      orderby t.Nr, t.Source, t.Source_id, t.Field_id
                      select t;
            }

            int antal = imp.Count();
            int Last_Nr = 0;
            byte Last_Source = 0;
            int Last_Source_id = 0;
            bool bFirst = true;
            bool bBreak = false;
            clsImEksportAppEngMedlem objMedlem = null;

            foreach (var t in imp)
            {
                bBreak = ((t.Source_id != Last_Source_id) || (t.Source != Last_Source) || (t.Nr != Last_Nr));
                if ((bBreak) && (!bFirst)) //Save Data   
                {
                    switch (Last_Source)
                    {
                        case (byte)tblsource.medlem:  //Medlem
                            medlemupdate(objMedlem);  //Save Medlem
                            break;

                        default:
                            break;
                    }
                }
                if ((bBreak) || (bFirst)) //Init Data
                {
                    switch (t.Source)
                    {
                        case (byte)tblsource.medlem:    //Medlem
                            objMedlem = new clsImEksportAppEngMedlem();
                            objMedlem.ieAction = ieAction;
                            objMedlem.Nr = t.Nr;
                            objMedlem.bNr = true;
                            objMedlem.Act = t.Act;
                            break;

                        default:
                            break;
                    }
                }
                // Get Data
                switch (t.Source)
                {
                    case (byte)tblsource.medlem:    //Medlem
                        switch (t.Field_id)
                        {
                            case 1:   //medlem_nr
                                objMedlem.Nr = int.Parse(t.Value);
                                objMedlem.bNr = true;
                                break;
                            case 2:   //navn
                                objMedlem.Navn = t.Value;
                                objMedlem.bNavn = true;
                                break;
                            case 3:   //kaldenavn
                                objMedlem.Kaldenavn = t.Value;
                                objMedlem.bKaldenavn = true;
                                break;
                            case 4:   //adresse
                                objMedlem.Adresse = t.Value;
                                objMedlem.bAdresse = true;
                                break;
                            case 5:   //postnr
                                objMedlem.Postnr = t.Value;
                                objMedlem.bPostnr = true;
                                break;
                            case 6:   //bynavn
                                objMedlem.Bynavn = t.Value;
                                objMedlem.bBynavn = true;
                                break;
                            case 7:   //telefon
                                objMedlem.Telefon = t.Value;
                                objMedlem.bTelefon = true;
                                break;
                            case 8:   //email
                                objMedlem.Email = t.Value;
                                objMedlem.bEmail = true;
                                break;
                            case 9:   //kon
                                objMedlem.Kon = t.Value;
                                objMedlem.bKon = true;
                                break;
                            case 10:   //fodtdato
                                objMedlem.FodtDato = DateTime.Parse(t.Value);
                                objMedlem.bFodtDato = true;
                                break;
                            case 11:   //bank
                                objMedlem.Bank = t.Value;
                                objMedlem.bBank = true;
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }

                // Save Status
                Last_Nr = t.Nr;
                Last_Source = t.Source;
                Last_Source_id = t.Source_id;
                bFirst = false;
            }
            if (!bFirst) //Save Data   
            {
                switch (Last_Source)
                {
                    case (byte)tblsource.medlem:    //Medlem
                        medlemupdate(objMedlem);    //Save Medlem
                        break;

                    default:
                        break;
                }
            }
            if (ieAction == ImpExp.fdImport) Program.dsMedlemImport.savedsMedlem();
        }

        private void medlemupdate(clsImEksportAppEngMedlem objMedlem)
        {
            objMedlem.ExecuteImEksport(); //throw new NotImplementedException();
        }

    }
}
