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
    public partial class FrmInfotekst : Form
    {
        public FrmInfotekst()
        {
            InitializeComponent();
        }

        private void FrmInfotekst_Load(object sender, EventArgs e)
        {
            this.bsInfotekst.DataSource = Program.dbData3060.Tblinfotekst;
        }
    }
}
