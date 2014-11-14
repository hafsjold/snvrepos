using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nsInfo3060
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            Program.frmMain = this;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
#if (DEBUG)
            testToolStripMenuItem.Visible = true;
#endif
        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if (DEBUG)

#endif
        }

        private void betaligToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Betaling"))
            {
                FrmBetaling frmBetaling = new FrmBetaling();
                frmBetaling.MdiParent = this;
                frmBetaling.Show();
            }
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void databasePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Indtast Password"))
            {
                FrmPassword m_frmPassword = new FrmPassword();
                m_frmPassword.MdiParent = this;
                m_frmPassword.Show();
            }
        }
    }
}
