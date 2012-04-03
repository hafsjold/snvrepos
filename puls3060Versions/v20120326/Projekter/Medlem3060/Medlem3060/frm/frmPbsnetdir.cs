using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nsPbs3060;

namespace nsPuls3060
{
    public partial class FrmPbsnetdir : Form
    {
        public FrmPbsnetdir()
        {
            InitializeComponent();
        }

        private void FrmPbsnetdir_Load(object sender, EventArgs e)
        {
            clsSFTP objSFTP = new clsSFTP(Program.XdbData3060);
            objSFTP.ReadDirFraSFtp();
            objSFTP.DisconnectSFtp();
            objSFTP = null;
            this.bsMemPbsnetdir.DataSource = Program.memPbsnetdir; //***MHA***
        }
    }
}
