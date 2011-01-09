using System;
using System.Linq;
using System.Data;

namespace nsPuls3060
{


    public partial class dsMedlem
    {

        public void filldsMedlem()
        {
            var qry_medlemmer = from h in Program.karMedlemmer
                                join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                                from x in details1.DefaultIfEmpty(new TblMedlem { Nr = -1, Kon = null, FodtDato = (DateTime?)null })
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
                                    XNr = x == null ? (int?)null : x.Nr,
                                    Kon = x == null ? null : x.Kon,
                                    FodtDato = x == null ? (DateTime?)null : x.FodtDato,
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
                        m.AcceptChanges();
                        break;
                }
            }
            Program.karDkkonti.save();
            Program.karKortnr.save();
            Program.karMedlemmer.Save();
        }
    }
}