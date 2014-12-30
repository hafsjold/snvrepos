using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;

namespace nsPuls3060v2
{
    public partial class FrmMedlemExtra : Form
    {
        public FrmMedlemExtra()
        {
            InitializeComponent();
        }

        private void FrmMedlemExtra_Load(object sender, EventArgs e)
        {
            Program.dbData3060.tblMedlemExtra.Load() ;
            this.bstblMedlemExtra.DataSource = Program.dbData3060.tblMedlemExtra.Local;
        }

        private void FrmMedlemExtra_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
