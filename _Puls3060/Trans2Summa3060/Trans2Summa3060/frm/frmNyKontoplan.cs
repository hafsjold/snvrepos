using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trans2Summa3060
{
    public partial class FrmNyKontoplan : Form
    {
        public FrmNyKontoplan()
        {
            InitializeComponent();
        }

        private void frmNyKontoplan_Load(object sender, EventArgs e)
        {
            this.karNyKontoplanBindingSource.DataSource = Program.karNyKontoplan;
        }

        private void frmNyKontoplan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.karNyKontoplan.save();
        }
    }
}
