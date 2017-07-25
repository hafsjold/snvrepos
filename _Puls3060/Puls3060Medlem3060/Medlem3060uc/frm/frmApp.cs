using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nsPbs3060;

namespace Medlem3060uc
{
    public partial class FrmApp : Form
    {
        private clsAppData m_AppData = null;
        public FrmApp()
        {
            InitializeComponent();
            message.Text = "";
            m_AppData = new clsAppData();
        }

        private void frmApp_Load(object sender, EventArgs e)
        {
            bsAppData.DataSource = m_AppData;
        }

        private void btnOpdater_Click(object sender, EventArgs e)
        {
            clsApp.AddUpdateAppSettings(m_AppData);
            message.Text = "Opdateret";
            m_AppData = new clsAppData();
            bsAppData.DataSource = m_AppData;
        }

        private void btnAfslut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EncryptAppconfig_Click(object sender, EventArgs e)
        {
            clsAppData w_AppData = new clsAppData();
            w_AppData.bEncryptApp = true;
            clsApp.AddUpdateAppSettings(w_AppData);
            message.Text = "App.confir Encrypted";
            m_AppData = new clsAppData();
            bsAppData.DataSource = m_AppData;
        }
    }
}
