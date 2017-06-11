using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nsPbs3060;

namespace Medlem3060uc
{
    public partial class FrmResend : Form
    {
        public FrmResend()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int pbsfilesid = int.Parse(txtpbsfilesid.Text);
            button1.Enabled = false;
            txtpbsfilesid.Enabled = false;
            clsSFTP objSFTP = new clsSFTP(Program.dbData3060);
            objSFTP.ReWriteTilSFtp(Program.dbData3060, pbsfilesid);
            objSFTP.DisconnectSFtp();
            objSFTP = null;
            button1.Text = "Udført";
        }
    }
}
