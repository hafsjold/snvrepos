using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace nsHafsjoldData
{
    public partial class FrmBankafstemning : Form
    {
        ColumnSorter lvwKontoudtogBank_ColumnSorter;
        ColumnSorter lvwBilagBankkonto_ColumnSorter;
        private string DragDropKey;


        public FrmBankafstemning()
        {
            InitializeComponent();
            this.lvwKontoudtogBank_ColumnSorter = new ColumnSorter();
            this.lvwKontoudtogBank.ListViewItemSorter = lvwKontoudtogBank_ColumnSorter;
            this.lvwBilagBankkonto_ColumnSorter = new ColumnSorter();
            this.lvwBilagBankkonto.ListViewItemSorter = lvwBilagBankkonto_ColumnSorter;
        }

        private void lvwKontoudtogBank_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwKontoudtogBank_ColumnSorter.CurrentColumn = e.Column;
            this.lvwKontoudtogBank.Sort();
        }

        private void lvwBilagBankkonto_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwBilagBankkonto_ColumnSorter.CurrentColumn = e.Column;
            this.lvwBilagBankkonto.Sort();
        }

        private void FrmBankafstemning_Load(object sender, EventArgs e)
        {
        }


        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getBankafstemning();
        }

        private void getBankafstemning()
        {

            var qry_medlemmer = from h in Program.dbHafsjoldData.Tblbankkonto
                                select h;

            this.lvwKontoudtogBank.Items.Clear();
            this.lvwBilagBankkonto.Items.Clear();

            var antal = qry_medlemmer.Count();


            foreach (var m in qry_medlemmer)
            {
                ListViewItem it = lvwBilagBankkonto.Items.Add("item0", "item1", 0);
                it.SubItems.Add("item2");
                it.SubItems.Add("item3");
                it.SubItems.Add("item4");
                it.SubItems.Add("item5");
                it.SubItems.Add("item6");
                it.SubItems.Add("item7");
            }
            
            this.lvwBilagBankkonto.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }
        private void lvwKontoudtogBank_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwKontoudtogBank.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwBilagBankkonto_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwBilagBankkonto_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwKontoudtogBank.SelectedItems)
            {
                ListViewItem it = lvwBilagBankkonto.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwKontoudtogBank.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwBilagBankkonto.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        private void lvwBilagBankkonto_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwBilagBankkonto.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwKontoudtogBank_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwKontoudtogBank_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwBilagBankkonto.SelectedItems)
            {
                ListViewItem it = lvwKontoudtogBank.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwBilagBankkonto.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwKontoudtogBank.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
