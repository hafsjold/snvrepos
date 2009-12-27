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
    public partial class FrmKreditor : Form
    {
        public FrmKreditor()
        {
            InitializeComponent();
        }

        private void FrmKreditor_Load(object sender, EventArgs e)
        {
            this.bsKreditor.DataSource = Program.dbData3060.Tblkreditor;

        }
    }
}
