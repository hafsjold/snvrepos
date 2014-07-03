using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trans2SummaHDA
{
    public partial class FrmBilag : Form
    {
        public FrmBilag()
        {
            InitializeComponent();
        }

        private void FrmBilag_Load(object sender, EventArgs e)
        {
            var qryTblbilag = from b in Program.dbDataTransSumma.tblbilags orderby b.dato descending select b;
            this.tblbilagBindingSource.DataSource = qryTblbilag;
        }
        private void FrmBilag_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
