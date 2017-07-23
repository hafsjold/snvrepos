using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trans2SummaHDC
{
    public partial class FrmPassword : Form
    {
        clsAppData _appdata;

        public FrmPassword()
        {
            InitializeComponent();
        }

        private void EncryptAppconfig_Click(object sender, EventArgs e)
        {
            clsAppData data = new clsAppData()
            {
                bEncryptApp = true,
            };
            clsApp.AddUpdateAppSettings(data);
            _appdata = new clsAppData();
            dsAppData.DataSource = _appdata;
        }

        private void Opdatermarkerededata_Click(object sender, EventArgs e)
        {
            clsApp.AddUpdateAppSettings(_appdata);
            _appdata = new clsAppData();
            dsAppData.DataSource = _appdata;
        }

        private void frmPassword_Load(object sender, EventArgs e)
        {
            _appdata = new clsAppData();
            dsAppData.DataSource = _appdata;
        }

        private void frmPassword_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
