using System;
using System.Linq;
using System.Data;
using nsPbs3060v2;
using System.Data.Entity;

namespace nsPuls3060v2
{
    public partial class dsMedlem
    {
         public void filldstblMedlemmer()
        {
            Program.dbData3060.tblMedlem.Load();
            var qry_medlemmer = from h in Program.dbData3060.tblMedlem.Local
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
                                    h.Kon,
                                    h.FodtDato,
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
                        nsPbs3060v2.tblMedlem m_rec;
                        try
                        {
                            m_rec = (from k in Program.dbData3060.tblMedlem
                                     where k.Nr == Nr_Key
                                     select k).First();
                        }
                        catch (System.InvalidOperationException)
                        {
                            m_rec = new nsPbs3060v2.tblMedlem
                            {
                                Nr = Nr_Key
                            };
                            Program.dbData3060.tblMedlem.Add(m_rec);
                        }
                        m_rec.Navn = m.Navn;
                        m_rec.Kaldenavn = (m.IsKaldenavnNull()) ? null : m.Kaldenavn;
                        m_rec.Adresse = (m.IsAdresseNull()) ? null : m.Adresse;
                        m_rec.Postnr = (m.IsPostnrNull()) ? null : m.Postnr;
                        m_rec.Bynavn = (m.IsBynavnNull()) ? null : m.Bynavn;
                        m_rec.Telefon = (m.IsTelefonNull()) ? null : (m.Telefon.Length > 8) ? m.Telefon.Substring(0, 4) + m.Telefon.Substring(5, 4) : m.Telefon;
                        m_rec.Email = (m.IsEmailNull()) ? null : m.Email;
                        m_rec.Bank = (m.IsBankNull()) ? null : m.Bank;
                        m_rec.Kon = (m.IsKonNull()) ? null : m.Kon.ToUpper();
                        m_rec.FodtDato = (m.IsFodtDatoNull()) ? (DateTime?)null : m.FodtDato;
                        m.AcceptChanges();
                        break;

                    case DataRowState.Deleted:
                        m.AcceptChanges();
                        break;

                    case DataRowState.Modified:
                        Nr_Key = m.Nr;
                        try
                        {
                            m_rec = (from k in Program.dbData3060.tblMedlem
                                     where k.Nr == Nr_Key
                                     select k).First();
                        }
                        catch (System.InvalidOperationException)
                        {
                            m_rec = new nsPbs3060v2.tblMedlem
                            {
                                Nr = Nr_Key
                            };
                            Program.dbData3060.tblMedlem.Add(m_rec);
                        }
                        m_rec.Navn = m.Navn;
                        m_rec.Kaldenavn = (m.IsKaldenavnNull()) ? null : m.Kaldenavn;
                        m_rec.Adresse = (m.IsAdresseNull()) ? null : m.Adresse;
                        m_rec.Postnr = (m.IsPostnrNull()) ? null : m.Postnr;
                        m_rec.Bynavn = (m.IsBynavnNull()) ? null : m.Bynavn;
                        m_rec.Telefon = (m.IsTelefonNull()) ? null : (m.Telefon.Length > 8) ? m.Telefon.Substring(0, 4) + m.Telefon.Substring(5, 4) : m.Telefon;
                        m_rec.Email = (m.IsEmailNull()) ? null : m.Email;
                        m_rec.Bank = (m.IsBankNull()) ? null : m.Bank;
                        m_rec.Kon = (m.IsKonNull()) ? null : m.Kon.ToUpper();
                        m_rec.FodtDato = (m.IsFodtDatoNull()) ? (DateTime?)null : m.FodtDato;
                        m.AcceptChanges();
                        break;
                }
            }

        }

    }
}