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
    public partial class FrmFaktura : Form
    {
        public FrmFaktura()
        {
            InitializeComponent();
        }

        private void FrmFaktura_Load(object sender, EventArgs e)
        {
            this.tblfakBindingSource.DataSource = Program.dbDataTransSumma.Tblfak;
            //this.tblfaklinBindingSource.DataSource = Program.dbDataTransSumma.Tblfaklin;
        }

        private void FrmFaktura_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }
    }
}
