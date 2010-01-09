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
        ColumnSorter lvwLog_ColumnSorter;

        public FrmMedlemmer()
        {
            InitializeComponent();
            this.lvwLog_ColumnSorter = new ColumnSorter();
            this.lvwLog.ListViewItemSorter = lvwLog_ColumnSorter;

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


            this.lvwLog.Items.Clear();
            foreach (var MedlemLog in qry)
            {
                ListViewItem it = lvwLog.Items.Add(string.Format("{0:yyyy-MM-dd}", MedlemLog.Akt_dato));
                it.SubItems.Add(MedlemLog.Akt_tekst);
            }
            this.lvwLog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }
    }
}
