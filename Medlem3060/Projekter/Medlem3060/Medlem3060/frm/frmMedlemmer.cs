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
            this.panelAdd.Location = this.panelDisplay.Location;
            this.panelUpdate.Location = this.panelDisplay.Location;
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
            int Nr = int.Parse(((TextBox)sender).Text);
            Update_lvwLog(Nr);

        }

        private void Update_lvwLog(int P_Nr)
        {
            this.lvwLog.Items.Clear();
            try
            {
                var medlem = (from m in Program.karMedlemmer
                              where m.Nr == P_Nr
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
                                    .Where(u => u.Nr == P_Nr)
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

        //private void cmdUpdate_Click(object sender, EventArgs e)
        //{
        //    int tblMedlem_nr = int.Parse(this.Nr.Text);
        //    //this.dsMedlem.savedsMedlem();
        //
        //    this.dataGridView1.Update();
        //    SortOrder cc = this.dataGridView1.SortOrder;
        //    this.dataGridView1.Sort(this.dataGridView1.SortedColumn, ListSortDirection.Ascending);
        //    foreach (DataGridViewRow r in this.dataGridView1.Rows)
        //    {
        //        if (r.Cells[0].Value.ToString() == tblMedlem_nr.ToString())
        //{
        //            int ci = dataGridView1.CurrentCell.ColumnIndex;
        //            dataGridView1.CurrentCell = r.Cells[ci];
        //        }
        //    }
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
            this.panelDisplay.Visible = false;
            this.panelUpdate.Visible = false;
            this.panelAdd.Visible = true;
            this.I_Overskrift.ForeColor = System.Drawing.Color.Blue;
            this.I_Nr.Text = "*";
            this.I_Navn.Text = null;
            this.I_Kaldenavn.Text = null;
            this.I_Adresse.Text = null;
            this.I_Postnr.Text = null;
            this.I_Bynavn.Text = null;
            this.I_Telefon.Text = null;
            this.I_Email.Text = null;
            this.I_Knr.Text = null;
            this.I_Kon.Text = null;
            this.I_DT_FodtDato.Value = null;
            this.I_DT_Indmeldelsesdato.Value = DateTime.Now;
            this.I_Navn.Focus();
        }

        private void cmdCancel_I_Record_Click(object sender, EventArgs e)
        {
            this.panelDisplay.Visible = true;
            this.panelUpdate.Visible = false;
            this.panelAdd.Visible = false;
            this.Navn.Focus();
        }

        private void cmdSave_I_Record_Click(object sender, EventArgs e)
        {
            int tblMedlem_nr = clsPbs.nextval("tblMedlem");
            object[] val = new object[11];
            val[0] = tblMedlem_nr;
            val[1] = (I_Navn.Text.Length == 0) ? "" : I_Navn.Text;
            val[2] = (I_Kaldenavn.Text.Length == 0) ? null : I_Kaldenavn.Text;
            val[3] = (I_Adresse.Text.Length == 0) ? null : I_Adresse.Text;
            val[4] = (I_Postnr.Text.Length == 0) ? null : I_Postnr.Text;
            val[5] = (I_Bynavn.Text.Length == 0) ? null : I_Bynavn.Text;
            val[6] = (I_Telefon.Text.Length == 0) ? null : I_Telefon.Text;
            val[7] = (I_Email.Text.Length == 0) ? null : I_Email.Text;
            val[8] = (I_Knr.Text.Length == 0) ? ((short?)null) : short.Parse(I_Knr.Text);
            val[9] = (I_Kon.Text.Length == 0) ? null : I_Kon.Text;
            val[10] = (I_DT_FodtDato.Value == null) ? ((DateTime?)null) : (DateTime)I_DT_FodtDato.Value;
            this.dsMedlem.Kartotek.Rows.Add(val);
            this.dsMedlem.savedsMedlem();
            if (I_DT_Indmeldelsesdato.Value != null)
            {
                try
                {
                    DateTime nu = DateTime.Now;
                    TblMedlemLog recLog = new TblMedlemLog
                    {
                        Nr = tblMedlem_nr,
                        Logdato = new DateTime(nu.Year, nu.Month, nu.Day),
                        Akt_id = 10,
                        Akt_dato = (DateTime)I_DT_Indmeldelsesdato.Value
                    };
                    Program.dbData3060.TblMedlemLog.InsertOnSubmit(recLog);
                    Program.dbData3060.SubmitChanges();
                }
                catch (Exception)
                {
                }
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
            this.panelDisplay.Visible = true;
            this.panelAdd.Visible = false;
            this.Navn.Focus();
        }

        private void bindingNavigatorUpdateItem_Click(object sender, EventArgs e)
        {
            this.panelDisplay.Visible = false;
            this.panelUpdate.Visible = true;
            this.panelAdd.Visible = false;
            this.U_Overskrift.ForeColor = System.Drawing.Color.Blue;

            this.U_Nr.Text = this.Nr.Text;
            this.U_Navn.Text = this.Navn.Text;
            this.U_Kaldenavn.Text = this.Kaldenavn.Text;
            this.U_Adresse.Text = this.Adresse.Text;
            this.U_Postnr.Text = this.Postnr.Text;
            this.U_Bynavn.Text = this.Bynavn.Text;
            this.U_Telefon.Text = this.Telefon.Text;
            this.U_Email.Text = this.Email.Text;
            this.U_Knr.Text = this.Knr.Text;
            this.U_Kon.Text = this.Kon.Text;
            this.U_DT_FodtDato.Value = (this.FodtDato.Text.Length == 0) ? (DateTime?)null : DateTime.Parse(this.FodtDato.Text);
            this.U_NyAktivitet.Items.Clear();
            this.U_NyAktivitet.Items.Add("Indmeldelse");
            this.U_NyAktivitet.Items.Add("Udmeldelse");
            this.U_NyAktivitet.Items.Add("Kontingent betalt til");
            this.U_NyAktivitet.Text = null;
            this.U_DT_NyAktivitetDato.Value = null;
            this.U_Navn.Focus();
        }

        private void cmdCancel_U_Record_Click(object sender, EventArgs e)
        {
            this.panelDisplay.Visible = true;
            this.panelUpdate.Visible = false;
            this.panelAdd.Visible = false;
            this.Navn.Focus();
        }

        private void cmdSave_U_Record_Click(object sender, EventArgs e)
        {
            int tblMedlem_nr = int.Parse(this.U_Nr.Text);
            DataRow row = this.dsMedlem.Kartotek.Rows.Find(tblMedlem_nr);
            object[] val = row.ItemArray;
            val[1] = (U_Navn.Text.Length == 0) ? "" : U_Navn.Text;
            val[2] = (U_Kaldenavn.Text.Length == 0) ? null : U_Kaldenavn.Text;
            val[3] = (U_Adresse.Text.Length == 0) ? null : U_Adresse.Text;
            val[4] = (U_Postnr.Text.Length == 0) ? null : U_Postnr.Text;
            val[5] = (U_Bynavn.Text.Length == 0) ? null : U_Bynavn.Text;
            val[6] = (U_Telefon.Text.Length == 0) ? null : U_Telefon.Text;
            val[7] = (U_Email.Text.Length == 0) ? null : U_Email.Text;
            val[8] = (U_Knr.Text.Length == 0) ? ((short?)null) : short.Parse(U_Knr.Text);
            val[9] = (U_Kon.Text.Length == 0) ? null : U_Kon.Text;
            val[10] = (U_DT_FodtDato.Value == null) ? ((DateTime?)null) : (DateTime)U_DT_FodtDato.Value;
            row.BeginEdit();
            row.ItemArray = val;
            row.EndEdit();
            this.dsMedlem.savedsMedlem();
            if (U_DT_NyAktivitetDato.Value != null)
            {
                int Akt_id;
                switch (U_NyAktivitet.Text)
                {
                    case "Indmeldelse":
                        Akt_id = 10;
                        break;
                    case "Kontingent betalt til":
                        Akt_id = 30;
                        break;
                    case "Udmeldelse":
                        Akt_id = 50;
                        break;
                    default:
                        Akt_id = 0;
                        break;
                }
                if (Akt_id != 0)
                {
                    try
                    {
                        DateTime nu = DateTime.Now;
                        TblMedlemLog recLog = new TblMedlemLog
                        {
                            Nr = tblMedlem_nr,
                            Logdato = new DateTime(nu.Year, nu.Month, nu.Day),
                            Akt_id = Akt_id,
                            Akt_dato = (DateTime)U_DT_NyAktivitetDato.Value
                        };
                        Program.dbData3060.TblMedlemLog.InsertOnSubmit(recLog);
                        Program.dbData3060.SubmitChanges();
                    }
                    catch (Exception)
                    {
                    }
                }
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
            Update_lvwLog(tblMedlem_nr);
            
            this.panelDisplay.Visible = true;
            this.panelUpdate.Visible = false;
            this.Navn.Focus();
        }

    }
}
