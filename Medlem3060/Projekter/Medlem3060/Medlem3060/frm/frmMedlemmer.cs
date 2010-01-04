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

        public void setErrorMode()
        {
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!this.Validates(e)) //run some custom validation on the value in that cell 
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Error";
                e.Cancel = false; //will prevent user from leaving cell, may not be the greatest idea, you can decide that yourself. 
            }
        }

        private bool Validates(DataGridViewCellValidatingEventArgs e)
        {
            string colName = this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
            return false;
        }

        private void frmMedlemmer_Load(object sender, EventArgs e)
        {
            open();
        }

        private void frmMedlemmer_FormClosing(object sender, FormClosingEventArgs e)
        {
            save();
            Properties.Settings.Default.Save();
        }

        private void open()
        {
            var qry_medlemmer = from h in Program.karMedlemmer
                                join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                                from x in details1.DefaultIfEmpty(new TblMedlem { Nr = -1, Knr = (int?)null, Kon = null, FodtDato = (DateTime?)null })
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
                                    Knr = x == null ? (int?)null : x.Knr,
                                    Kon = x == null ? null : x.Kon,
                                    FodtDato = x == null ? (DateTime?)null : x.FodtDato
                                };

            var antal = qry_medlemmer.Count();
            foreach (var m in qry_medlemmer)
            {
                DataRow MyRow = null;
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
                        k_rec.Kaldenavn = (m.IsKaldenavnNull()) ? null : m.Kaldenavn;
                        k_rec.Adresse = (m.IsAdresseNull()) ? null : m.Adresse;
                        k_rec.Postnr = (m.IsPostnrNull()) ? null : m.Postnr;
                        k_rec.Bynavn = (m.IsBynavnNull()) ? null : m.Bynavn;
                        k_rec.Telefon = (m.IsTelefonNull()) ? null : m.Telefon;
                        k_rec.Email = (m.IsEmailNull()) ? null : m.Email;
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
                        m_rec.Knr = (m.IsKnrNull()) ? (int?)null : m.Knr;
                        m_rec.Kon = (m.IsKonNull()) ? null : m.Kon;
                        m_rec.FodtDato = (m.IsFodtDatoNull()) ? (DateTime?)null : m.FodtDato;
                        break;

                    case DataRowState.Deleted:
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
                        m_rec.Knr = (m.IsKnrNull()) ? (int?)null : m.Knr;
                        m_rec.Kon = (m.IsKonNull()) ? null : m.Kon;
                        m_rec.FodtDato = (m.IsFodtDatoNull()) ? (DateTime?)null : m.FodtDato;
                        break;
                }
            }
            Program.karMedlemmer.Save();
        }
    }
}
