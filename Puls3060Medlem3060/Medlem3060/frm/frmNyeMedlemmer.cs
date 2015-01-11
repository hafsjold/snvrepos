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
    public partial class FrmNyeMedlemmer : Form
    {
        public FrmNyeMedlemmer()
        {
            InitializeComponent();
        }

        private void FrmNyeMedlemmer_Load(object sender, EventArgs e)
        {
            this.bstblNytMedlem.DataSource = Program.dbData3060.tblNytMedlems;
        }

        private void FrmNyeMedlemmer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

    }
}
