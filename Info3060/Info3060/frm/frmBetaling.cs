using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nsInfo3060
{
    public partial class FrmBetaling : Form
    {
        public FrmBetaling()
        {
            InitializeComponent();
        }

        private void FrmBetaling_Load(object sender, EventArgs e)
        {
            this.tblbetalingsidentifikationBindingSource.DataSource = Program.dbData3060.tblbetalingsidentifikations;
        }

        private void FrmBetaling_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dbData3060.SubmitChanges();
        }
    }
}
