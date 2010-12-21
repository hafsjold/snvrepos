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
            clsSFTP objSFTP = new clsSFTP();
            objSFTP.ReWriteTilSFtp(pbsfilesid);
            objSFTP.DisconnectSFtp();
            objSFTP = null;
            button1.Text = "Udført";
        }
    }
}
