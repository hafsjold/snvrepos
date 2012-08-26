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
    public partial class FrmKontoudtog : Form
    {
        public FrmKontoudtog()
        {
            InitializeComponent();
        }

        private void FrmKontoudtog_Load(object sender, EventArgs e)
        {
            this.bsTblkontoudtog.DataSource = Program.dbDataTransSumma.Tblkontoudtog;
        }

        private void FrmKontoudtog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
