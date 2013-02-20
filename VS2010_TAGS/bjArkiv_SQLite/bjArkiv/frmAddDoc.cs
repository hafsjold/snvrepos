using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Security.Principal;

namespace bjArkiv
{
    public partial class frmAddDoc : Form
    {
        private int m_Ref_nr;
        private string m_Dokument;
        private string m_Virksomhed;
        private string m_Emne;
        private string m_Dokument_type;
        private int m_År;
        private string m_Ekstern_kilde;
        private string m_Beskrivelse;
        private string m_Oprettet_af;
        private DateTime m_Oprettet_dato;
        private bool m_Opret;

        public int Ref_nr
        {
            get { return m_Ref_nr; }
            set { m_Ref_nr = value; }
        }
        public string Dokument
        {
            get { return m_Dokument; }
            set
            {
                m_Dokument = value;
                txtBoxDokument.Text = value;
            }
        }
        public string Virksomhed
        {
            get { return m_Virksomhed; }
            set
            {
                m_Virksomhed = value;
                txtBoxVirksomhed.Text = value;
            }
        }
        public string Emne
        {
            get { return m_Emne; }
            set
            {
                m_Emne = value;
                txtBoxEmne.Text = value;
            }
        }
        public string Dokument_type
        {
            get { return m_Dokument_type; }
            set
            {
                m_Dokument_type = value;
                txtBoxDokument_type.Text = value;
            }
        }
        public int År
        {
            get { return m_År; }
            set
            {
                m_År = value;
                txtBoxÅr.Text = value.ToString();
            }
        }
        public string Ekstern_kilde
        {
            get { return m_Ekstern_kilde; }
            set
            {
                m_Ekstern_kilde = value;
                txtBoxEkstern_kilde.Text = value;

            }
        }
        public string Beskrivelse
        {
            get { return m_Beskrivelse; }
            set
            {
                m_Beskrivelse = value;
                txtBoxBeskrivelse.Text = value;
            }
        }
        public string Oprettet_af
        {
            get { return m_Oprettet_af; }
            set
            {
                m_Oprettet_af = value;
            }
        }
        public DateTime Oprettet_dato
        {
            get { return m_Oprettet_dato; }
            set
            {
                m_Oprettet_dato = value;
            }
        }
        public bool Opret
        {
            get { return m_Opret; }
            set
            {
                m_Opret = value;
                if (m_Opret)
                {
                    this.butOK.Text = "Opret";
                    this.Text = "Opret dokument";
                    m_Ref_nr = 0;
                    txtBoxRef_nr.Text = "";
                }
                else
                {
                    this.butOK.Text = "Gem";
                    this.Text = "Opdater dokument";
                    txtBoxRef_nr.Text = m_Ref_nr.ToString();
                }
            }
        }

        public frmAddDoc()
        {
            InitializeComponent();
        }

        public frmAddDoc(Point Start)
        {
            global::bjArkiv.Properties.Settings.Default.frmAddDocLocation = Start;
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

            AppDomain appDomain = Thread.GetDomain();
            appDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            WindowsPrincipal windowsPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
            Oprettet_af = windowsPrincipal.Identity.Name;

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
