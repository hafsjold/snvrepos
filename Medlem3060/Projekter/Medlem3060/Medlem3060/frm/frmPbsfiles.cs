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
    public partial class FrmPbsfiles : Form
    {
        public FrmPbsfiles()
        {
            InitializeComponent();
        }

        private void FrmPbsfiles_Load(object sender, EventArgs e)
        {
            this.tblpbsfilesBindingSource.DataSource = Program.dbData3060.Tblpbsfiles;
            this.dataGridView1.AutoResizeColumns();
        }
    }
}
