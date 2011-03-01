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
    public partial class FrmUdskrivbilag : Form
    {
        public FrmUdskrivbilag()
        {
            InitializeComponent();
        }

        private void frmUdskrivbilag_Load(object sender, EventArgs e)
        {
            var qry = from p in Program.dbDataTransSumma.Tblbankkonto select p;
            BindingSource.DataSource = qry;
            this.reportViewer1.RefreshReport();
        }
    }
}
