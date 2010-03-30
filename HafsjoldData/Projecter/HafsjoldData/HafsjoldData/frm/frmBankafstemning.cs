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
        ColumnSorter lvwKontoudtog_ColumnSorter;
        ColumnSorter lvwKontoudtogAfstemt_ColumnSorter;
        ColumnSorter lvwBilag_ColumnSorter;
        ColumnSorter lvwBilagAfstemt_ColumnSorter;
        private string DragDropKey;


        public FrmBankafstemning()
        {
            InitializeComponent();
            this.lvwKontoudtog_ColumnSorter = new ColumnSorter();
            this.lvwKontoudtog.ListViewItemSorter = lvwKontoudtog_ColumnSorter;
            this.lvwKontoudtogAfstemt_ColumnSorter = new ColumnSorter();
            this.lvwKontoudtogAfstemt.ListViewItemSorter = lvwKontoudtogAfstemt_ColumnSorter;

            this.lvwBilag_ColumnSorter = new ColumnSorter();
            this.lvwBilag.ListViewItemSorter = lvwBilag_ColumnSorter;
            this.lvwBilagAfstemt_ColumnSorter = new ColumnSorter();
            this.lvwBilagAfstemt.ListViewItemSorter = lvwBilagAfstemt_ColumnSorter;
        }

        private void FrmBankafstemning_Load(object sender, EventArgs e)
        {
            getBankafstemning();
        }

        private void lvwKontoudtog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwKontoudtog_ColumnSorter.CurrentColumn = e.Column;
            this.lvwKontoudtog.Sort();
        }

        private void lvwKontoudtogAfstemt_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwKontoudtogAfstemt_ColumnSorter.CurrentColumn = e.Column;
            this.lvwKontoudtogAfstemt.Sort();
        }

        private void lvwBilag_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwBilag_ColumnSorter.CurrentColumn = e.Column;
            this.lvwBilag.Sort();
        }

        private void lvwBilagAfstemt_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwBilagAfstemt_ColumnSorter.CurrentColumn = e.Column;
            this.lvwBilagAfstemt.Sort();
        }

        private void getBankafstemning()
        {

            var qry_Kontoudtog = from h in Program.dbHafsjoldData.Tblbankkonto
                                 where h.Afstem == (int?)null
                                 orderby h.Dato
                                 select h;

            this.lvwKontoudtog.Items.Clear();
            this.lvwKontoudtogAfstemt.Items.Clear();

            foreach (var m in qry_Kontoudtog)
            {
                ListViewItem it = lvwKontoudtog.Items.Add(m.Id.ToString(), string.Format("{0:dd-MM-yyy}", m.Dato), 0);
                it.SubItems.Add(m.Tekst);
                it.SubItems.Add(m.Belob.ToString());
            }

            this.lvwKontoudtog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            this.lvwKontoudtogAfstemt.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);

            var qry_Bilag = from h in Program.dbHafsjoldData.Tbltrans
                            where h.Kontonr == 58000 & h.Afstem == (int?)null
                            join d1 in Program.dbHafsjoldData.Tblbilag on h.Bilagpid equals d1.Pid
                            orderby d1.Dato
                            select new {h.Pid, d1.Dato, d1.Bilag, h.Tekst, h.Belob};

            foreach (var m in qry_Bilag)
            {
                ListViewItem it = lvwBilag.Items.Add(m.Pid.ToString(), string.Format("{0:dd-MM-yyy}", m.Dato), 0);
                it.SubItems.Add(m.Bilag.ToString());
                it.SubItems.Add(m.Tekst);
                it.SubItems.Add(m.Belob.ToString());
            }

            this.lvwBilag.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
            this.lvwBilagAfstemt.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }

        private void lvwKontoudtog_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwKontoudtog.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwKontoudtogAfstemt_DragEnter(object sender, DragEventArgs e)
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

        private void lvwKontoudtogAfstemt_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwKontoudtog.SelectedItems)
            {
                ListViewItem it = lvwKontoudtogAfstemt.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwKontoudtog.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwKontoudtogAfstemt.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }


        private void lvwKontoudtogAfstemt_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwKontoudtogAfstemt.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwKontoudtog_DragEnter(object sender, DragEventArgs e)
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

        private void lvwKontoudtog_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwKontoudtogAfstemt.SelectedItems)
            {
                ListViewItem it = lvwKontoudtog.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwKontoudtogAfstemt.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwKontoudtog.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }

        private void lvwBilag_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwBilag.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwBilagAfstemt_DragEnter(object sender, DragEventArgs e)
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

        private void lvwBilagAfstemt_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwBilag.SelectedItems)
            {
                ListViewItem it = lvwBilagAfstemt.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwBilag.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwBilagAfstemt.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }


        private void lvwBilagAfstemt_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwBilagAfstemt.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwBilag_DragEnter(object sender, DragEventArgs e)
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

        private void lvwBilag_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwBilagAfstemt.SelectedItems)
            {
                ListViewItem it = lvwBilag.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwBilagAfstemt.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwBilag.AutoResizeColumns(ColumnHeaderAutoResizeStyle.None);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
