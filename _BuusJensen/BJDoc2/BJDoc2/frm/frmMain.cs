using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BJDoc2
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            Program.frmMain = this;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool FocusChild(string child)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Text.ToUpper() == child.ToUpper())
                {
                    frm.Focus();
                    return true;
                }
            }
            return false;
        }

        public Form GetChild(string child)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Text.ToUpper() == child.ToUpper())
                {
                    return frm;
                }
            }
            return null;
        }

        private void brugerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Bruger"))
            {
                FrmBruger m_Bruger = new FrmBruger();
                m_Bruger.MdiParent = this;
                m_Bruger.Show();
            }
        }

        private void featureTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("FeatureType"))
            {
                FrmFeatureType m_FeatureType = new FrmFeatureType();
                m_FeatureType.MdiParent = this;
                m_FeatureType.Show();
            }
        }





        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.db.SaveChanges();
        }

    }
}
