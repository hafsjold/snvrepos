using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pipeClient
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            enablemenus();
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

        private void enablemenus()
        {
            if (Program.bLogedIn)
            {
                appDataToolStripMenuItem.Enabled = true;
                statusToolStripMenuItem.Enabled = true;
                toolStripStatusLogedinUser.Text = string.Format("Loged in som {0}", Program.User);
            }
            else
            {
                appDataToolStripMenuItem.Enabled = false;
                statusToolStripMenuItem.Enabled = false;
                toolStripStatusLogedinUser.Text = "NOT Loged in, Vælg Action-->Login";
            }
        }

        private void appDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("AppData"))
            {
                FrmAppData frmAppData = new FrmAppData();
                frmAppData.MdiParent = this;
                frmAppData.Show();
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Login"))
            {
                Program.frmLogin = new FrmLogin();
            }
            login();
            enablemenus();
        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void login()
        {
            Program.frmLogin.txtBruger.Text = Program.User;
            while (true)
            {
                DialogResult res = Program.frmLogin.ShowDialog(this);
                if (res == DialogResult.Cancel) return;
                if (!Program.frmLogin.bNyBruger)
                {
                    var rxes = clsPassword.CheckPassword(Program.frmLogin.txtBruger.Text, Program.frmLogin.txtPassword.Text, false);
                    if (!rxes)
                    {
                        Program.frmLogin.lblError.Text = "Forkert Bruger eller Password";
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    var ryes = clsPassword.CheckPassword(Program.frmLogin.txtBruger.Text, Program.frmLogin.txtPassword.Text, true);
                    return;
                }
            }
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("frmStatus"))
            {
                FrmStatus frmStatus = new FrmStatus();
                frmStatus.MdiParent = this;
                frmStatus.Show();
            }
        }
    }
}
