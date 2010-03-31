using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsHafsjoldData
{
    public partial class FrmNyKladde : Form
    {
        public FrmNyKladde()
        {
            InitializeComponent();
        }

        private void TestMD_Load(object sender, EventArgs e)
        {
            bsXWbilag.DataSource = from b in Program.dbHafsjoldData.Tblwbilag select b;
        }

        private void FrmNyKladde_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dbHafsjoldData.SubmitChanges();
        }
    }
}
