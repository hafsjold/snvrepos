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
    public partial class FrmPbsnetdir : Form
    {
        public FrmPbsnetdir()
        {
            InitializeComponent();
        }

        private void FrmPbsnetdir_Load(object sender, EventArgs e)
        {
            clsAppEngSFTP objAppEngSFTP = new clsAppEngSFTP();
            objAppEngSFTP.ReadDirFraSFtp();
            objAppEngSFTP.DisconnectSFtp();
            objAppEngSFTP = null;
            this.bsMemPbsnetdir.DataSource = Program.memPbsnetdir;
        }
    }
}
