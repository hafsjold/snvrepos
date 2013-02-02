using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace docdb
{
    public partial class frmAddDoc : Form
    {
        private string m_Dokument;
        private string m_Selskab;
        private int m_År;
        private string m_Produkt;

        public string Dokument
        {
            get { return m_Dokument; }
            set
            {
                m_Dokument = value;
                txtBoxDokument.Text = m_Dokument;
            }
        }
        public string Selskab
        {
            get { return m_Selskab; }
            set { m_Selskab = value; }
        }
        public int År
        {
            get { return m_År; }
            set { m_År = value; }
        }
        public string Produkt
        {
            get { return m_Produkt; }
            set { m_Produkt = value; }
        }

        public frmAddDoc()
        {
            InitializeComponent();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            Selskab = txtBoxSelskab.Text;
            try
            {
                År = int.Parse(txtBoxÅr.Text);
            }
            catch
            {

                År = 0;
            }
            Produkt = txtBoxProdukt.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
