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
    public partial class FrmInfotekst : Form
    {
        public FrmInfotekst()
        {
            InitializeComponent();
        }

        private void FrmInfotekst_Load(object sender, EventArgs e)
        {
            Program.dbData3060.tblinfotekst.Load();
            this.bsInfotekst.DataSource = Program.dbData3060.tblinfotekst.Local;
        }
    }
}
