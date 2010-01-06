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
    public partial class FrmKontingentForslag : Form
    {
        ColumnSorter lvwMedlem_ColumnSorter;
        ColumnSorter lvwKontingent_ColumnSorter;


        public FrmKontingentForslag()
        {
            InitializeComponent();
            this.lvwMedlem_ColumnSorter = new ColumnSorter();
            this.lvwMedlem.ListViewItemSorter = lvwMedlem_ColumnSorter;
            this.lvwKontingent_ColumnSorter = new ColumnSorter();
            this.lvwKontingent.ListViewItemSorter = lvwKontingent_ColumnSorter;

        }

        private void lvwMedlem_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwMedlem_ColumnSorter.CurrentColumn = e.Column;
            this.lvwMedlem.Sort();
        }
        
        private void lvwKontingent_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwKontingent_ColumnSorter.CurrentColumn = e.Column;
            this.lvwKontingent.Sort();
        }

        private void FrmKontingentForslag_Load(object sender, EventArgs e)
        {
            open();
        }

        
        private void open()
        {
            var qry_medlemmer = from h in Program.karMedlemmer
                                orderby h.Kaldenavn
                                select h;

            var antal = qry_medlemmer.Count();
            foreach (var m in qry_medlemmer)
            {
                ListViewItem it = lvwMedlem.Items.Add(m.Nr.ToString(), m.Navn, 0);
                it.SubItems.Add(m.Nr.ToString());
                it.SubItems.Add(m.Adresse);
                it.SubItems.Add(m.Postnr);
                it.SubItems.Add(m.Bynavn);
            }
            this.lvwMedlem.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            
            foreach (var m in qry_medlemmer)
            {
                ListViewItem it = lvwKontingent.Items.Add(m.Nr.ToString(), m.Navn, 0);
                it.SubItems.Add(m.Nr.ToString());
                it.SubItems.Add(m.Adresse);
                it.SubItems.Add(m.Postnr);
                it.SubItems.Add(m.Bynavn);
            }
            this.lvwKontingent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }



    }
}
