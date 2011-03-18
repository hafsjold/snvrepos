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
    public partial class FrmBilag : Form
    {
        public FrmBilag()
        {
            InitializeComponent();
        }

        private void FrmBilag_Load(object sender, EventArgs e)
        {
            this.tblbilagBindingSource.DataSource = Program.dbDataTransSumma.Tblbilag;
        }
        private void FrmBilag_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
