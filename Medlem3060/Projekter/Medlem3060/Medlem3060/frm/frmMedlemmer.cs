using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsPuls3060
{
    public partial class FrmMedlemmer : Form
    {
        public FrmMedlemmer()
        {
            InitializeComponent();
        }

        private void frmMedlemmer_Load(object sender, EventArgs e)
        {
            open();
        }
        
        private void frmMedlemmer_FormClosing(object sender, FormClosingEventArgs e)
        {
            save();
        }

        private void open()
        {
            var qry_medlemmer = from h in Program.karMedlemmer
                                join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                                from x in details1.DefaultIfEmpty(new TblMedlem {Nr = -1,  Knr = -1, Kon = "X", FodtDato = new DateTime(1900,1,1) })
                                //where x.Nr== -1
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
                                    XNr = x == null ? -1 : x.Nr,
                                    Knr = x == null ? -1 : x.Knr,
                                    Kon = x == null ? "-1" : x.Kon,
                                    FodtDato = x == null ? new DateTime(1900, 01, 01) : x.FodtDato
                                };

            var antal = qry_medlemmer.Count();
            foreach (var m in qry_medlemmer)
            {
                DataRow MyRow = null;
                if (m.XNr == -1)
                    MyRow = this.dsMedlem.Kartotek.Rows.Add(m.Nr, m.Navn, m.Kaldenavn, m.Adresse, m.Postnr, m.Bynavn, m.Telefon, m.Email);
                else
                    MyRow = this.dsMedlem.Kartotek.Rows.Add(m.Nr, m.Navn, m.Kaldenavn, m.Adresse, m.Postnr, m.Bynavn, m.Telefon, m.Email, m.Knr, m.Kon, m.FodtDato);

                MyRow.AcceptChanges();
            }
            this.dataGridView1.AutoResizeColumns();
        }

        private void save()
        {
            foreach (nsPuls3060.dsMedlem.KartotekRow m in this.dsMedlem.Kartotek.Rows)
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
                        if (!m.IsKaldenavnNull()) k_rec.Kaldenavn = m.Kaldenavn;
                        if (!m.IsAdresseNull()) k_rec.Adresse = m.Adresse;
                        if (!m.IsPostnrNull()) k_rec.Postnr = m.Postnr;
                        if (!m.IsBynavnNull()) k_rec.Bynavn = m.Bynavn;
                        if (!m.IsTelefonNull()) k_rec.Telefon = m.Telefon;
                        if (!m.IsEmailNull()) k_rec.Email = m.Email;
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
                        if (!m.IsKnrNull()) m_rec.Knr = m.Knr;
                        if (!m.IsKonNull()) m_rec.Kon = m.Kon;
                        if (!m.IsFodtDatoNull()) m_rec.FodtDato = m.FodtDato;
                        break;

                    case DataRowState.Deleted:
                        break;

                    case DataRowState.Modified:
                        Nr_Key = m.Nr;
                        k_rec = (from k in Program.karMedlemmer
                                     where k.Nr == Nr_Key
                                     select k).First();
                        
                        k_rec.Navn = m.Navn;
                        if (!m.IsKaldenavnNull()) k_rec.Kaldenavn = m.Kaldenavn;
                        if (!m.IsAdresseNull()) k_rec.Adresse = m.Adresse;
                        if (!m.IsPostnrNull()) k_rec.Postnr = m.Postnr;
                        if (!m.IsBynavnNull()) k_rec.Bynavn = m.Bynavn;
                        if (!m.IsTelefonNull()) k_rec.Telefon = m.Telefon;
                        if (!m.IsEmailNull()) k_rec.Email = m.Email;
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
                        if (!m.IsKnrNull()) m_rec.Knr = m.Knr;
                        if (!m.IsKonNull()) m_rec.Kon = m.Kon;
                        if (!m.IsFodtDatoNull()) m_rec.FodtDato = m.FodtDato;
                        //m_rec.Knr = m.IsKnrNull() ? -1 : m.Knr;
                        //m_rec.Kon = m.IsKonNull() ? null : m.Kon;
                        //m_rec.FodtDato = m.IsFodtDatoNull() ? new DateTime(1900, 01, 01) : m.FodtDato;
                        break;
                }
            }
            Program.karMedlemmer.Save();
        }
    }
}
