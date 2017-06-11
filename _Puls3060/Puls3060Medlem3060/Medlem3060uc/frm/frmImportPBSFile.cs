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
    public partial class FrmImportPBSFile : Form
    {
        public FrmImportPBSFile()
        {
            InitializeComponent();
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            clsSFTP objSFTP = new clsSFTP(Program.dbData3060);
            int AntalImportFiler = objSFTP.ReadFromLocalFile(Program.dbData3060, textFilePath.Text);
            objSFTP.DisconnectSFtp();
            objSFTP = null;

            cmdImport.Enabled = false;
            textFilePath.Enabled = false;
            if (AntalImportFiler == 1)
                labelMessage.Text = string.Format("{0} er importeret", textFilePath.Text);
            else
            {
                labelMessage.Text = string.Format("{0} kunne ikke importeres", textFilePath.Text);
                labelMessage.ForeColor = Color.Red;
            }

            labelMessage.Visible = true;

        }
    }
}
