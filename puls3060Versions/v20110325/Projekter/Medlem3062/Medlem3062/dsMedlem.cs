using System;
using System.Linq;
using System.Data;
using System.Xml.Linq;
using System.Collections.Generic;

namespace nsPuls3060
{


    public partial class dsMedlem
    {

        
        public void filldsMedlem()
        {
            var qry_medlemmer = from h in Program.karMedlemmer
                                //join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                                //from x in details1.DefaultIfEmpty(new TblMedlem { Nr = -1, Kon = null, FodtDato = (DateTime?)null })
                                //where x.Nr == -1
                                select new
                                {
                                    h.Nr,
                                    h.Navn,
                                    h.Kaldenavn,
                                    h.Adresse,
                                    h.Postnr,
                                    h.Bynavn,
                                    h.Telefon,
                                    h.Email,
                                    //XNr = x == null ? (int?)null : x.Nr,
                                    Kon = (string)null,
                                    FodtDato = (DateTime?)null,
                                    h.Bank
                                };

            var antal = qry_medlemmer.Count();
            foreach (var m in qry_medlemmer)
            {
                KartotekRow MyRow = (KartotekRow)tableKartotek.Rows.Add(
                    m.Nr,
                    m.Navn,
                    m.Kaldenavn,
                    m.Adresse,
                    m.Postnr,
                    m.Bynavn,
                    m.Telefon,
                    m.Email,
                    m.Kon,
                    m.FodtDato,
                    m.Bank
                  );

                MyRow.AcceptChanges();


                var Nr = m.Nr;
                var Navn = m.Navn.Trim();
                var Kaldenavn = m.Kaldenavn.Trim();
                var Adresse = m.Adresse.Trim();
                var Postnr = m.Postnr.Trim();
                var Bynavn = m.Bynavn.Trim();
                var Telefon = m.Telefon.Trim();
                var Email = m.Email.Trim();
                var Bank = m.Bank.Trim();
               
                tblSyncMedlemRow SyncMedlemRow = (tblSyncMedlemRow)tabletblSyncMedlem.Rows.Add(
                      Nr,
                      Navn,
                      Kaldenavn,
                      Adresse,
                      Postnr,
                      Bynavn,
                      Telefon,
                      Email,
                      Bank
                );

                SyncMedlemRow.AcceptChanges();
            }
        }

        public void savedsMedlem()
        {
            foreach (KartotekRow m in tableKartotek.Rows)
            {
                switch (m.RowState)
                {
                    case DataRowState.Added:
                        var Nr_Key = m.Nr;
                        var k_rec = new clsMedlem()
                        {
                            Nr = Nr_Key,
                            Navn = m.Navn
                        };
                        k_rec.Kaldenavn = (m.IsKaldenavnNull()) ? null : m.Kaldenavn;
                        k_rec.Adresse = (m.IsAdresseNull()) ? null : m.Adresse;
                        k_rec.Postnr = (m.IsPostnrNull()) ? null : m.Postnr;
                        k_rec.Bynavn = (m.IsBynavnNull()) ? null : m.Bynavn;
                        k_rec.Telefon = (m.IsTelefonNull()) ? null : m.Telefon;
                        k_rec.Email = (m.IsEmailNull()) ? null : m.Email;
                        k_rec.Bank = (m.IsBankNull()) ? null : m.Bank;
                        k_rec.setKreditor();
                        k_rec.getNewCvsString();
                        Program.karMedlemmer.Add(k_rec);
                        
                        /*
                        TblMedlem m_rec;
                        try
                        {
                            m_rec = (from k in Program.dbData3060.TblMedlem
                                     where k.Nr == Nr_Key
                                     select k).First();
                        }
                        catch (System.InvalidOperationException)
                        {
                            m_rec = new TblMedlem
                            {
                                Nr = Nr_Key
                            };
                            Program.dbData3060.TblMedlem.InsertOnSubmit(m_rec);
                        }
                        m_rec.Kon = (m.IsKonNull()) ? null : m.Kon;
                        m_rec.FodtDato = (m.IsFodtDatoNull()) ? (DateTime?)null : m.FodtDato;
                        */
                        
                        m.AcceptChanges();
                        break;

                    case DataRowState.Deleted:
                        m.AcceptChanges();
                        break;

                    case DataRowState.Modified:
                        Nr_Key = m.Nr;
                        k_rec = (from k in Program.karMedlemmer
                                 where k.Nr == Nr_Key
                                 select k).First();

                        k_rec.Navn = m.Navn;
                        k_rec.Kaldenavn = (m.IsKaldenavnNull()) ? null : m.Kaldenavn;
                        k_rec.Adresse = (m.IsAdresseNull()) ? null : m.Adresse;
                        k_rec.Postnr = (m.IsPostnrNull()) ? null : m.Postnr;
                        k_rec.Bynavn = (m.IsBynavnNull()) ? null : m.Bynavn;
                        k_rec.Telefon = (m.IsTelefonNull()) ? null : m.Telefon;
                        k_rec.Email = (m.IsEmailNull()) ? null : m.Email;
                        k_rec.Bank = (m.IsBankNull()) ? null : m.Bank;
                        k_rec.setKreditor();
                        Program.karMedlemmer.Update(Nr_Key);

                        /*
                        try
                        {
                            m_rec = (from k in Program.dbData3060.TblMedlem
                                     where k.Nr == Nr_Key
                                     select k).First();
                        }
                        catch (System.InvalidOperationException)
                        {
                            m_rec = new TblMedlem
                            {
                                Nr = Nr_Key
                            };
                            Program.dbData3060.TblMedlem.InsertOnSubmit(m_rec);
                        }
                        m_rec.Kon = (m.IsKonNull()) ? null : m.Kon;
                        m_rec.FodtDato = (m.IsFodtDatoNull()) ? (DateTime?)null : m.FodtDato;
                        */
                        
                        m.AcceptChanges();
                        break;
                }
            }
            Program.karDkkonti.save();
            Program.karKortnr.save();
            Program.karMedlemmer.Save();
        }

        public void fillPerson()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2(clsRest.urlBaseType.sync,"Medlem");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from person in xdoc.Descendants("Person") select person;
            int antal = list.Count();
            foreach (var person in list)
            {

                if ((person.Descendants("Nr").First().Value != null) && (person.Descendants("Nr").First().Value != "None"))
                {
                    var Nr = person.Descendants("Nr").First().Value.Trim();
                    var Navn = person.Descendants("Navn").First().Value.Trim();
                    var Kaldenavn = person.Descendants("Kaldenavn").First().Value.Trim();
                    var Adresse = person.Descendants("Adresse").First().Value.Trim();
                    var Postnr = person.Descendants("Postnr").First().Value.Trim();
                    var Bynavn = person.Descendants("Bynavn").First().Value.Trim();
                    var Telefon = person.Descendants("Telefon").First().Value.Trim();
                    if (Telefon == "None") Telefon = "";
                    var Email = person.Descendants("Email").First().Value.Trim();
                    if (Email == "None") Email = "";
                    var Kon = person.Descendants("Kon").First().Value.Trim();
                    var FodtDato = person.Descendants("FodtDato").First().Value.Trim();
                    var Bank = person.Descendants("Bank").First().Value.Trim();
                    if (Bank == "None") Bank = "";

                    tblPersonRow PersonRow = (tblPersonRow)tabletblPerson.Rows.Add(
                      Nr,
                      Navn,
                      Kaldenavn,
                      Adresse,
                      Postnr,
                      Bynavn,
                      Telefon,
                      Email,
                      Kon,
                      FodtDato,
                      Bank
                    );

                    PersonRow.AcceptChanges();

                    tblSyncPersonRow SyncPersonRow = (tblSyncPersonRow)tabletblSyncPerson.Rows.Add(
                      Nr,
                      Navn,
                      Kaldenavn,
                      Adresse,
                      Postnr,
                      Bynavn,
                      Telefon,
                      Email,
                      Bank
                    );
                    SyncPersonRow.AcceptChanges();
                }
            }
        }

        public void savePerson()
        {
            foreach (tblPersonRow p in tabletblPerson.Rows)
            {
                clsRest objRest = null;
                XElement xml = null;
                string retur = null;
                string strxml = null;
                switch (p.RowState)
                {
                    case DataRowState.Added:
                        objRest = new clsRest();
                        xml = new XElement("Medlem", new XElement("Nr", p.Nr));
                        xml.Add(new XElement("Navn", p.Navn));
                        xml.Add(new XElement("Kaldenavn", p.Kaldenavn));
                        xml.Add(new XElement("Adresse", p.Adresse));
                        xml.Add(new XElement("Postnr", p.Postnr));
                        xml.Add(new XElement("Bynavn", p.Bynavn));
                        xml.Add(new XElement("Telefon", p.Telefon));
                        xml.Add(new XElement("Email", p.Email));
                        xml.Add(new XElement("Kon", p.Kon));
                        xml.Add(new XElement("FodtDato", ((DateTime)p.FodtDato).ToString("yyyy-MM-dd")));
                        xml.Add(new XElement("Bank", p.Bank));
                        strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                        retur = objRest.HttpPost2(clsRest.urlBaseType.sync, "Medlem", strxml);
                        p.AcceptChanges();
                        break;

                    case DataRowState.Deleted:
                        objRest = new clsRest();
                        retur = objRest.HttpDelete2(clsRest.urlBaseType.sync, "Medlem/" + p.Nr);
                        p.AcceptChanges();
                        break;

                    case DataRowState.Modified:
                        objRest = new clsRest();
                        xml = new XElement("Medlem", new XElement("Nr", p.Nr));
                        xml.Add(new XElement("Navn", p.Navn));
                        xml.Add(new XElement("Kaldenavn", p.Kaldenavn));
                        xml.Add(new XElement("Adresse", p.Adresse));
                        xml.Add(new XElement("Postnr", p.Postnr));
                        xml.Add(new XElement("Bynavn", p.Bynavn));
                        xml.Add(new XElement("Telefon", p.Telefon));
                        xml.Add(new XElement("Email", p.Email));
                        xml.Add(new XElement("Kon", p.Kon));
                        xml.Add(new XElement("FodtDato", ((DateTime)p.FodtDato).ToString("yyyy-MM-dd")));
                        xml.Add(new XElement("Bank", p.Bank));
                        strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                        retur = objRest.HttpPost2(clsRest.urlBaseType.sync, "Medlem", strxml); 
                        p.AcceptChanges();
                        break;
                }
            }
        }

        public void fillMedlog()
        {
            clsRest objRest = new clsRest();
            string retur = objRest.HttpGet2(clsRest.urlBaseType.sync, "Medlog");
            XDocument xdoc = XDocument.Parse(retur);
            var list = from medlog in xdoc.Descendants("Medlog") select medlog;
            int antal = list.Count();
            foreach (var medlog in list)
            {

                if ((medlog.Descendants("Nr").First().Value != null) && (medlog.Descendants("Nr").First().Value != "None") && (medlog.Descendants("Nr").First().Value.Length <= 4))
                {
                    var Nr = medlog.Descendants("Nr").First().Value.Trim();
                    var Logdato = medlog.Descendants("Logdato").First().Value.Trim();
                    var Akt_id = medlog.Descendants("Akt_id").First().Value.Trim();
                    var Akt_dato = medlog.Descendants("Akt_dato").First().Value.Trim();

                    tblMedlogRow MedlogRow = (tblMedlogRow)tabletblMedlog.Rows.Add(
                      Nr,
                      Logdato,
                      Akt_id,
                      Akt_dato
                    );

                    MedlogRow.AcceptChanges();
                }
            }
        }

        public void saveMedlog()
        {
            foreach (tblMedlogRow l in tabletblMedlog.Rows)
            {
                clsRest objRest = null;
                XElement xml = null;
                string retur = null;
                string strxml = null;
                switch (l.RowState)
                {
                    case DataRowState.Added:
                        objRest = new clsRest();
                        int Id = clsPbs.nextval("Medlogid");
                        xml = new XElement("Medlog");
                        xml.Add(new XElement("Id", Id));
                        xml.Add(new XElement("Source_id", Id));
                        xml.Add(new XElement("Source", "Medlog"));
                        xml.Add(new XElement("Nr", l.Nr));
                        xml.Add(new XElement("Logdato", l.Logdato));
                        xml.Add(new XElement("Akt_id", l.Akt_id));
                        xml.Add(new XElement("Akt_dato", l.Akt_dato));
                        strxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xml.ToString();
                        retur = objRest.HttpPost2(clsRest.urlBaseType.sync, "Medlog", strxml);
                        l.AcceptChanges();
                        break;
                }
            }
        }
    }

}