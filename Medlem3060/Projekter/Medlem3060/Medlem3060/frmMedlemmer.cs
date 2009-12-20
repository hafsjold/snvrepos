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
    public partial class frmMedlemmer : Form
    {
        private DbData3060 m_dbData3060;
        private clsMedlemmer m_objMedlemmer;
        
        public frmMedlemmer(DbData3060 pdbData3060)
        {
            InitializeComponent();
            m_dbData3060 = pdbData3060;
            m_objMedlemmer = new clsMedlemmer(m_dbData3060);
        }

        private void frmMedlemmer_Load(object sender, EventArgs e)
        {
            var qry_medlemmer =
                from k in m_objMedlemmer
                join m in m_dbData3060.TblMedlem on k.Nr equals m.Nr
                select new { k.Nr, k.Navn, k.Kaldenavn, k.Adresse, k.Postnr, k.Bynavn, k.Email, k.Telefon, m.Knr, m.Kon, m.FodtDato };
            var antal = qry_medlemmer.Count();
            foreach (var m in qry_medlemmer) 
            {
                DataRow MyRow = this.dsMedlem.Kartotek.Rows.Add(m.Nr, m.Navn, m.Kaldenavn, m.Adresse, m.Postnr, m.Bynavn, m.Telefon, m.Email, m.Knr, m.Kon, m.FodtDato);
                MyRow.AcceptChanges();
            }
            this.dataGridView1.AutoResizeColumns();
        }

        private void frmMedlemmer_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (nsPuls3060.dsMedlem.KartotekRow m in this.dsMedlem.Kartotek.Rows)
            {
                switch (m.RowState)
                {
                    case DataRowState.Added:
                        break;
                    case DataRowState.Deleted:
                        break;
                    case DataRowState.Modified:
                        int Nr_Key = m.Nr;
                        var k_rec = (from k in m_objMedlemmer
                                     where k.Nr == Nr_Key
                                     select k).First();
                        k_rec.Navn = m.Navn;
                        k_rec.Kaldenavn = m.Kaldenavn;
                        k_rec.Adresse = m.Adresse;
                        k_rec.Postnr = m.Postnr;
                        k_rec.Bynavn = m.Bynavn;
                        k_rec.Telefon = m.Telefon;
                        k_rec.Email = m.Email;
                        m_objMedlemmer.Update(Nr_Key);
                        var m_rec = (from k in m_dbData3060.TblMedlem
                                     where k.Nr == Nr_Key
                                     select k).First();
                        m_rec.Knr = m.Knr;
                        m_rec.Kon = m.Kon;
                        m_rec.FodtDato = m.FodtDato;
                        break;
                }
            }
            m_objMedlemmer.Save();
        }
    }
}
