using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medlem3060uc
{
    public partial class FrmNytMedlem : Form
    {
        public FrmNytMedlem()
        {
            InitializeComponent();
        }

        private void FrmNytMedlem_Load(object sender, EventArgs e)
        {
            this.tblIndmeldelseBindingSource.DataSource = Program.dbData3060.tblIndmeldelses.Where(i => i.Nr == null);
        }

        private void FrmNytMedlem_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void tblIndmeldelseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Program.dbData3060.SubmitChanges();
            }
            catch { }
        }
    }
}
