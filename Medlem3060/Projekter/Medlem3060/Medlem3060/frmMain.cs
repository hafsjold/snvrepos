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
        private DbData3060 dbData3060;
        
        public frmMain()
        {
            InitializeComponent();
            dbData3060 = new DbData3060(@"C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\dbData3060.sdf");

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbData3060.SubmitChanges();
        }

        private void Test_Click(object sender, EventArgs e)
        {
            clsPbs Test = new clsPbs();
            //Test.faktura_601_action(1);
            Test.TestRead042();
        }
    }
}
