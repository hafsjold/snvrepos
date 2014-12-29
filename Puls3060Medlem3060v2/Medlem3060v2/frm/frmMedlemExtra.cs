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
    public partial class FrmMedlemExtra : Form
    {
        public FrmMedlemExtra()
        {
            InitializeComponent();
        }

        private void FrmMedlemExtra_Load(object sender, EventArgs e)
        {
            this.bstblMedlemExtra.DataSource = Program.dbData3060.tblMedlemExtra;
        }

        private void FrmMedlemExtra_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
