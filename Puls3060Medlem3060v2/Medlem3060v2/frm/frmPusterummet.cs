using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;

namespace nsPuls3060
{
    public partial class FrmPusterummet : Form
    {
        public FrmPusterummet()
        {
            InitializeComponent();
        }

        private void FrmPusterummet_Load(object sender, EventArgs e)
        {
            Program.dbData3060.vPusterummet.Load();
            this.bsvPusterummet.DataSource = Program.dbData3060.vPusterummet.Local;
        }

        private void FrmPusterummet_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
