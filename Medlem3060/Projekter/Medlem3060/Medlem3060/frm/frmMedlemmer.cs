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
        private ColumnSorter lvwLog_ColumnSorter;
        private string RowBeforeSort = null;

        public FrmMedlemmer()
        {
            InitializeComponent();
            this.lvwLog_ColumnSorter = new ColumnSorter();
            this.lvwLog.ListViewItemSorter = lvwLog_ColumnSorter;
            Medlem_ReadOnly(false);
            this.panelAdd.Location = this.panelUpdate.Location;
        }

        private void lvwLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwLog_ColumnSorter.CurrentColumn = e.Column;
            this.lvwLog.Sort();
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

        private void Nr_TextChanged(object sender, EventArgs e)
        {

            this.lvwLog.Items.Clear();
            try
            {
                var medlem = (from m in Program.karMedlemmer
                              where m.Nr == int.Parse(((TextBox)sender).Text)
                              select m).First();

                if (medlem.erMedlem())
                {
                    this.Overskrift.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    this.Overskrift.ForeColor = System.Drawing.Color.Red;
                }

                var qrylog = Program.qryLog()
                                    .Where(u => u.Nr == int.Parse(((TextBox)sender).Text))
                                    .Where(u => u.Logdato <= DateTime.Now)
                                    .OrderByDescending(u => u.Logdato);

                var qry = from l in qrylog
                          join a in Program.dbData3060.TblAktivitet on l.Akt_id equals a.Id
                          select new { l.Akt_dato, a.Akt_tekst };


                foreach (var MedlemLog in qry)
                {
                    ListViewItem it = lvwLog.Items.Add(string.Format("{0:yyyy-MM-dd}", MedlemLog.Akt_dato));
                    it.SubItems.Add(MedlemLog.Akt_tekst);
                }
                this.lvwLog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch (System.InvalidOperationException)
            {
                this.Overskrift.ForeColor = System.Drawing.Color.Red;
            }

        }

        private void Navn_TextChanged(object sender, EventArgs e)
        {
            this.Overskrift.Text = ((TextBox)sender).Text;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                switch (((DataGridView)sender).CurrentCell.OwningColumn.Name)
                {
                    case "navnDataGridViewTextBoxColumn":
                        this.Navn.Text = ((DataGridView)sender).CurrentCell.Value.ToString();
                        break;

                    case "kaldenavnDataGridViewTextBoxColumn":
                        this.Kaldenavn.Text = ((DataGridView)sender).CurrentCell.Value.ToString();
                        break;

                    case "adresseDataGridViewTextBoxColumn":
                        this.Adresse.Text = ((DataGridView)sender).CurrentCell.Value.ToString();
                        break;

                    case "postnrDataGridViewTextBoxColumn":
                        this.Postnr.Text = ((DataGridView)sender).CurrentCell.Value.ToString();
                        break;
                }

            }
            catch (Exception)
            {
            }
        }

        private void Medlem_ReadOnly(bool val)
        {
            this.Navn.ReadOnly = val;
            this.Kaldenavn.ReadOnly = val;
            this.Adresse.ReadOnly = val;
            this.Postnr.ReadOnly = val;
            this.Bynavn.ReadOnly = val;
            this.Telefon.ReadOnly = val;
            this.Email.ReadOnly = val;
            this.Knr.ReadOnly = val;
            this.Kon.ReadOnly = val;
            this.FodtDato.ReadOnly = val;
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            //Medlem_ReadOnly(false);
            int tblMedlem_nr = clsPbs.nextval("tblMedlem");
            this.dsMedlem.Kartotek.Rows.Add(tblMedlem_nr, "Søren Nyhansen 3", "Søren3", "Nørremarken 45", "3060", "Espergærde");
            this.dataGridView1.Update();
            foreach (DataGridViewRow r in this.dataGridView1.Rows)
            {
                if (r.Cells[0].Value.ToString() == tblMedlem_nr.ToString())
                {
                    int ci = dataGridView1.CurrentCell.ColumnIndex;
                    dataGridView1.CurrentCell = r.Cells[ci];
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            //this.bindingNavigator1.Validate();
            this.kartotekBindingSource.EndEdit();
            this.dataGridView1.Update();
            //this.kartotekBindingSource.CancelEdit();
            this.dsMedlem.savedsMedlem();
            selectRowBeforeSort();
        }

        //private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        //{
        //    int tblMedlem_nr = clsPbs.nextval("tblMedlem");
        //    this.dataGridView1.CurrentRow.Cells[0].Value = tblMedlem_nr;
        //    this.dataGridView1.CurrentRow.Cells[1].Value = "";
        //    this.Nr.Text = tblMedlem_nr.ToString();
        //    this.Navn.Text = "";
        //    RowBeforeSort = tblMedlem_nr.ToString();
        //}


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex == -1) && (e.ColumnIndex != -1))//if both -1, it is the "select all" corner 
            {
                if (dataGridView1.Columns[e.ColumnIndex].CellType == typeof(DataGridViewTextBoxCell))
                {//sorting 
                    RowBeforeSort = ((DataGridView)sender).CurrentRow.Cells[0].Value.ToString();
                }
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            selectRowBeforeSort();
        }

        private void selectRowBeforeSort()
        {
            foreach (DataGridViewRow r in this.dataGridView1.Rows)
            {
                if (r.Cells[0].Value.ToString() == RowBeforeSort)
                {
                    int ci = dataGridView1.CurrentCell.ColumnIndex;
                    dataGridView1.CurrentCell = r.Cells[ci];
                }
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.panelUpdate.Visible = false;
            this.panelAdd.Visible = true;
            this.I_Overskrift.ForeColor = System.Drawing.Color.Blue;
            this.I_Nr.Text = "*";
            this.I_Navn.Focus();
        }

        private void cmdCancel_I_Record_Click(object sender, EventArgs e)
        {
            this.panelUpdate.Visible = true;
            this.panelAdd.Visible = false;
            this.Navn.Focus();
        }

        private void cmdSave_I_Recird_Click(object sender, EventArgs e)
        {
            int tblMedlem_nr = clsPbs.nextval("tblMedlem");
            this.dsMedlem.Kartotek.Rows.Add(tblMedlem_nr, I_Navn.Text, I_Kaldenavn.Text, I_Adresse.Text, I_Postnr.Text, I_Bynavn.Text, I_Telefon.Text, I_Email.Text, I_Knr.Text, I_Kon.Text, I_FodtDato.Text);
            this.dsMedlem.savedsMedlem();
            try
            {
                TblMedlemLog recLog = new TblMedlemLog
                {
                    Nr = tblMedlem_nr,
                    Logdato = DateTime.Now,
                    Akt_id = 10,
                    Akt_dato = DateTime.Parse(I_Indmeldelsesdato.Text)
                };
                Program.dbData3060.TblMedlemLog.InsertOnSubmit(recLog);
                Program.dbData3060.SubmitChanges();

            }
            catch (Exception)
            {
            }
            this.dataGridView1.Update();
            foreach (DataGridViewRow r in this.dataGridView1.Rows)
            {
                if (r.Cells[0].Value.ToString() == tblMedlem_nr.ToString())
                {
                    int ci = dataGridView1.CurrentCell.ColumnIndex;
                    dataGridView1.CurrentCell = r.Cells[ci];
                }
            }
            this.panelUpdate.Visible = true;
            this.panelAdd.Visible = false;
            this.Navn.Focus();
        }

    }
}
