using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Security.Principal;

namespace bjArkiv
{
    public partial class frmAddDoc : Form
    {
        public clsArkiv arkiv { get; set; }
        public xmldoc startrec { get; set; }

        public frmAddDoc()
        {
            InitializeComponent();
        }

        public frmAddDoc(Point Start)
        {
            global::bjArkiv.Properties.Settings.Default.frmAddDocLocation = Start;
            InitializeComponent();
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmAddDoc_Load(object sender, EventArgs e)
        {
            xmldocsBindingSource.DataSource = arkiv.docdb;
            try
            {
                int start = ((xmldocs)xmldocsBindingSource.DataSource).IndexOf(startrec);
                xmldocsBindingSource.CurrencyManager.Position = start;
            }
            catch  { }
        }
    }
}
