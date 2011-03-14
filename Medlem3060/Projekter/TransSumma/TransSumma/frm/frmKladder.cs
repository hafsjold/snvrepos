using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace nsPuls3060
{
    public partial class FrmKladder : Form
    {
        public FrmKladder()
        {
            InitializeComponent();
        }

        private void FrmKladder_Load(object sender, EventArgs e)
        {
            this.tblbilagBindingSource.DataSource = Program.dbDataTransSumma.Tblbilag;
        }

        private void bilagTextBox_TextChanged(object sender, EventArgs e)
        {

        }

    }

}