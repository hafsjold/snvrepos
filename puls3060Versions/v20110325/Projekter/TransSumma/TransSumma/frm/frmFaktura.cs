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
        }

        private void FrmFaktura_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void tblfaklinDataGridView_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.C && e.Control)
            {
                this.copyToClipboard();
                e.Handled = true; //otherwise the control itself tries to “copy”
            }
        }

        private void copyToClipboard()
        {
            IDataObject clipboardData = getDataObject();
            Clipboard.SetDataObject(clipboardData);
        }

        private IDataObject getDataObject()
        {
            DataObject clipboardData = this.tblfaklinDataGridView.GetClipboardContent();
            return clipboardData;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.copyToClipboard();
        }
    }
}
