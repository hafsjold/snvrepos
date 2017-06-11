using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medlem3060uc
{
    public partial class FrmPassword : Form
    {
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void Setpassword()
        {
            Program.Password = Program.Protect(this.password.Text);
            if (this.checkSavePassword.Checked)
                global::Medlem3060uc.Properties.Settings.Default.UserPassword = Program.Password;
            else
                global::Medlem3060uc.Properties.Settings.Default.UserPassword = "";
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Setpassword();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
