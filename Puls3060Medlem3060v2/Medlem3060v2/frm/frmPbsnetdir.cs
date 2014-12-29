using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using nsPbs3060v2;

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
            clsSFTP objSFTP = new clsSFTP(Program.dbData3060);
            objSFTP.ReadDirFraSFtp();
            this.bsMemPbsnetdir.DataSource = objSFTP.m_memPbsnetdir;
            objSFTP.DisconnectSFtp();
            objSFTP = null;
         }
    }
}
