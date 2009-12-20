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
            //var qry_medlemmer =
               //from k in m_dbData3060.TblMedlem
               //from k in m_objMedlemmer
               //where k.Postnr < 3000
               //select k;
            //join m in m_dbData3060.TblMedlem on k.Nr equals m.Nr
            //where m.FodtDato > DateTime.Parse("1980-01-01")
            //select new { k.Nr, k.Navn, k.Kaldenavn, k.Adresse, k.Postnr, k.Bynavn, k.Email, k.Telefon, m.Knr, m.Kon, m.FodtDato };

            //var antal = qry_medlemmer.Count();
            clsMedlem rec_med = new clsMedlem { 
                Nr = 501,
                Navn = "Andreas Hafsjold",
                Kaldenavn = "Andreas",
                Adresse = "Nørremarken 31",
                Postnr = "3060",
                Bynavn = "Espergærde",
                Email = "and@hafsjold.dk",
                Telefon = "4913 3540"
            };
            m_objMedlemmer.Add(rec_med);
            this.dataGridView1.DataSource = m_objMedlemmer;
            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.AutoSize = true;
            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.AllowUserToAddRows = true;

        }

        private void frmMedlemmer_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
