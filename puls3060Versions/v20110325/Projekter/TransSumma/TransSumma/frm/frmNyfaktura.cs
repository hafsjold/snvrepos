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
    public partial class FrmNyfaktura : Form
    {
        public FrmNyfaktura()
        {
            InitializeComponent();
        }

        private void FrmNyfaktura_Load(object sender, EventArgs e)
        {
            this.tblwfakBindingSource.DataSource = Program.dbDataTransSumma.Tblwfak;
        }

        private void FrmNyfaktura_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
