using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace docdblite
{
    public partial class frmAddDoc : Form
    {
        private string m_Dokument;
        private string m_Virksomhed;
        private string m_Emne;
        private string m_Dokument_type;
        private int m_År;
        private string m_Ekstern_kilde;
        private string m_Beskrivelse;
        private string m_Oprettet_af;
        private DateTime m_Oprettet_dato;

        public string Dokument
        {
            get { return m_Dokument; }
            set
            {
                m_Dokument = value;
                txtBoxDokument.Text = m_Dokument;
            }
        }
        public string Virksomhed
        {
            get { return m_Virksomhed; }
            set { m_Virksomhed = value; }
        }
        public string Emne
        {
            get { return m_Emne; }
            set { m_Emne = value; }
        }
        public string Dokument_type
        {
            get { return m_Dokument_type; }
            set { m_Dokument_type = value; }
        }
        public int År
        {
            get { return m_År; }
            set { m_År = value; }
        }
        public string Ekstern_kilde
        {
            get { return m_Ekstern_kilde; }
            set { m_Ekstern_kilde = value; }
        }
        public string Beskrivelse
        {
            get { return m_Beskrivelse; }
            set { m_Beskrivelse = value; }
        }
        public string Oprettet_af
        {
            get { return m_Oprettet_af; }
            set { m_Oprettet_af = value; }
        }
        public DateTime Oprettet_dato
        {
            get { return m_Oprettet_dato; }
            set { m_Oprettet_dato = value; }
        }

        public frmAddDoc()
        {
            InitializeComponent();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            Virksomhed = txtBoxVirksomhed.Text;
            Emne = txtBoxEmne.Text;
            Dokument_type = txtBoxDokument_type.Text;
            try
            {
                År = int.Parse(txtBoxÅr.Text);
            }
            catch
            {

                År = 0;
            }
            Ekstern_kilde = txtBoxEkstern_kilde.Text;
            Beskrivelse = txtBoxBeskrivelse.Text;
            Oprettet_af = @"BUUSJENSEN\mha";
            Oprettet_dato = DateTime.Now;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
