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
    public partial class frmMain : Form
    {
        private DbData3060 m_dbData3060;
        
        public frmMain()
        {
            InitializeComponent();
            m_dbData3060 = new DbData3060(@"C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\dbData3060.sdf");

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_dbData3060.SubmitChanges();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            clsPbs objPbs = new clsPbs(m_dbData3060);
            clsPbs601 objPbs601 = new clsPbs601(m_dbData3060);
            //objPbs601.faktura_601_action(1);
            clsPbs602 objPbs602 = new clsPbs602(m_dbData3060);
            //objPbs602.TestRead042();
            objPbs602.ReadFraPbsFile();
        }
    }
}
