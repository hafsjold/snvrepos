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
    public partial class FrmPusterummet : Form
    {
        public FrmPusterummet()
        {
            InitializeComponent();
        }

        private void FrmPusterummet_Load(object sender, EventArgs e)
        {
            this.bsvPusterummet.DataSource = Program.dbData3060.vPusterummets;
        }

        private void FrmPusterummet_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
