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
    public partial class FrmTemplate : Form
    {
        public FrmTemplate()
        {
            InitializeComponent();
        }

        private void FrmTemplate_Load(object sender, EventArgs e)
        {
            this.bsTbltemplate.DataSource = Program.dbDataTransSumma.Tbltemplate;
        }

        private void FrmTemplate_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
