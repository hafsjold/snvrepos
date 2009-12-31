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
    public partial class FrmRegnskab : Form
    {
        public FrmRegnskab()
        {
            InitializeComponent();
        }

        private void FrmRegnskab_Load(object sender, EventArgs e)
        {
            this.bsRegnskab.DataSource = Program.dbData3060.TblRegnskab;
        }

        private void FrmRegnskab_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
