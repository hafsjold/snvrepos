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
    public partial class FrmKreditor : Form
    {
        public FrmKreditor()
        {
            InitializeComponent();
        }

        private void FrmKreditor_Load(object sender, EventArgs e)
        {
            Program.dbData3060.tblkreditor.Load();
            this.bsKreditor.DataSource = Program.dbData3060.tblkreditor.Local;
        }

        private void FrmKreditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
