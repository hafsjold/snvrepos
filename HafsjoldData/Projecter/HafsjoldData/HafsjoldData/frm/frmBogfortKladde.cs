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
    public partial class FrmBogfortKladde : Form
    {
        public FrmBogfortKladde()
        {
            InitializeComponent();
        }

        private void frmBogfortKladde_Load(object sender, EventArgs e)
        {
            tblbilagBindingSource.DataSource = from b in Program.dbHafsjoldData.Tblbilag select b;
        }
    }
}
