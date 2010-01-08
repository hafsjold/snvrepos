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
    public partial class FrmMedlemmer : Form
    {
        public FrmMedlemmer()
        {
            InitializeComponent();
        }

        public void setErrorMode()
        {
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!this.Validates(e)) //run some custom validation on the value in that cell 
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Error";
                e.Cancel = false; //will prevent user from leaving cell, may not be the greatest idea, you can decide that yourself. 
            }
        }

        private bool Validates(DataGridViewCellValidatingEventArgs e)
        {
            string colName = this.dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
            return false;
        }

        private void frmMedlemmer_Load(object sender, EventArgs e)
        {
            this.dsMedlem.filldsMedlem();
            this.dataGridView1.AutoResizeColumns();
        }

        private void frmMedlemmer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.dsMedlem.savedsMedlem();
            Properties.Settings.Default.Save();
        }

    }
}
