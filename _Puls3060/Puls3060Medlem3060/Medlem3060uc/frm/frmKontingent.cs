using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medlem3060uc
{
    public partial class FrmKontingent : Form
    {
        public FrmKontingent()
        {
            InitializeComponent();
        }

        private void FrmKontingent_Load(object sender, EventArgs e)
        {
            this.bsKontingent.DataSource = Program.dbData3060.tblKontingents;
        }

        private void FrmKontingent_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
