﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml.Linq;

namespace nsPuls3060
{
    public partial class FrmBankafstemning : Form
    {
        ColumnSorter lvwBank_ColumnSorter;
        ColumnSorter lvwTrans_ColumnSorter;
        ColumnSorter lvwAfstemBank_ColumnSorter;
        ColumnSorter lvwAfstemTrans_ColumnSorter;
        private string DragDropKey;


        public FrmBankafstemning()
        {
            InitializeComponent();
            this.lvwBank_ColumnSorter = new ColumnSorter();
            this.lvwBank.ListViewItemSorter = lvwBank_ColumnSorter;
            this.lvwTrans_ColumnSorter = new ColumnSorter();
            this.lvwTrans.ListViewItemSorter = lvwTrans_ColumnSorter;
            this.lvwAfstemBank_ColumnSorter = new ColumnSorter();
            this.lvwAfstemBank.ListViewItemSorter = lvwAfstemBank_ColumnSorter;
            this.lvwAfstemTrans_ColumnSorter = new ColumnSorter();
            this.lvwAfstemTrans.ListViewItemSorter = lvwAfstemTrans_ColumnSorter;
        }

        private void lvwMedlem_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwBank_ColumnSorter.CurrentColumn = e.Column;
            this.lvwBank.Sort();
        }

        private void lvwTrans_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwTrans_ColumnSorter.CurrentColumn = e.Column;
            this.lvwTrans.Sort();
        }

        private void lvwAfstemBank_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwAfstemBank_ColumnSorter.CurrentColumn = e.Column;
            this.lvwAfstemBank.Sort();
        }

        private void lvwAfstemTrans_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwAfstemTrans_ColumnSorter.CurrentColumn = e.Column;
            this.lvwAfstemTrans.Sort();
        }
        
        private void FrmBankafstemning_Load(object sender, EventArgs e)
        {
        }

        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getBankkonto();
            getTrans();
        }

        private void getBankkonto()
        {
            int AntalForslag = 0;
            IEnumerable<clsqry_bank> qry_bank;
            if (this.RykketTidligere.Checked)
            {
                qry_bank = from b in Program.dbDataTransSumma.Tblbankkonto
                           where b.Afstem > 0
                           orderby b.Dato
                           select new clsqry_bank
                           {
                               Pid = b.Pid,
                               Dato = b.Dato,
                               Tekst = b.Tekst,
                               Belob = b.Belob 
                           };
            }
            else
            {

                qry_bank = from b in Program.dbDataTransSumma.Tblbankkonto
                           where b.Afstem == null
                           orderby b.Dato
                           select new clsqry_bank
                           {
                               Pid = b.Pid,
                               Dato = b.Dato,
                               Tekst = b.Tekst,
                               Belob = b.Belob
                           };
            }

            var antal = qry_bank.Count();
            this.lvwBank.Items.Clear();


            foreach (var b in qry_bank)
            {
                AntalForslag++;
                ListViewItem it = lvwBank.Items.Add(b.Pid.ToString(), string.Format("{0:yyyy-MM-dd}", b.Dato), 0);
                //it.Tag = b;
                it.SubItems.Add(b.Tekst);
                it.SubItems.Add(b.Belob.ToString());
            }
            this.lvwBank.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


        }
        private void getTrans()
        {
            int AntalForslag = 0;
            IEnumerable<clsqry_trans> qry_trans;
            if (this.RykketTidligere.Checked)
            {
                qry_trans = from t in Program.dbDataTransSumma.Tbltrans
                            join b in Program.dbDataTransSumma.Tblbilag on t.Bilagpid equals b.Pid
                            where t.Afstem > 0 && t.Kontonr == 58000
                            orderby b.Dato descending
                            select new clsqry_trans
                            {
                                Pid = t.Pid,
                                Dato = b.Dato,
                                Bilag = b.Bilag,
                                Tekst = t.Tekst,
                                Belob = t.Belob
                            };
            }
            else
            {

                qry_trans = from t in Program.dbDataTransSumma.Tbltrans
                            join b in Program.dbDataTransSumma.Tblbilag on t.Bilagpid equals b.Pid
                            where t.Afstem == null && t.Kontonr == 58000
                            orderby b.Dato descending
                            select new clsqry_trans
                            {
                                Pid = t.Pid,
                                Dato = b.Dato,
                                Bilag = b.Bilag,
                                Tekst = t.Tekst,
                                Belob = t.Belob
                            };
            }

            var antal = qry_trans.Count();
            this.lvwTrans.Items.Clear();

            foreach (var t in qry_trans)
            {
                AntalForslag++;
                ListViewItem it = lvwTrans.Items.Add(t.Pid.ToString(), string.Format("{0:yyyy-MM-dd}", t.Dato), 0);
                //it.Tag = t;
                it.SubItems.Add(t.Bilag.ToString());
                it.SubItems.Add(t.Tekst);
                it.SubItems.Add(t.Belob.ToString());
            }
            this.lvwTrans.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        private void lvwBank_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "B" + random.Next(1000, 64000).ToString();
            lvwBank.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwBank_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("AB")) 
                    { 
                        e.Effect = DragDropEffects.Move; 
                    } else 
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwBank_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwAfstemBank.SelectedItems)
            {
                ListViewItem it = lvwBank.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwAfstemBank.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwBank.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void lvwTrans_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "T" + random.Next(1000, 64000).ToString();
            lvwTrans.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwTrans_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("A"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwTrans_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwBank.SelectedItems)
            {
                ListViewItem it = lvwTrans.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwBank.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwTrans.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        private void lvwAfstemBank_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "AB" + random.Next(1000, 64000).ToString();
            lvwAfstemBank.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwAfstemBank_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("B"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwAfstemBank_DragDrop(object sender, DragEventArgs e)
        {
            if (DragDropKey.StartsWith("B"))
            {
                foreach (ListViewItem lvi in lvwBank.SelectedItems)
                {
                    ListViewItem it = lvwAfstemBank.Items.Add(lvi.Name, lvi.Text, 0);

                    for (int i = 1; i < lvi.SubItems.Count; i++)
                    {
                        string SubTekst = lvi.SubItems[i].Text;
                        it.SubItems.Add(SubTekst);

                    }
                }
                foreach (ListViewItem lvi in lvwBank.SelectedItems)
                {
                    lvi.Remove();
                }
            } 
            this.lvwAfstemBank.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void lvwAfstemTrans_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "AT" + random.Next(1000, 64000).ToString();
            lvwAfstemBank.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwAfstemTrans_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("T"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwAfstemTrans_DragDrop(object sender, DragEventArgs e)
        {
            if (DragDropKey.StartsWith("T"))
            {
                foreach (ListViewItem lvi in lvwTrans.SelectedItems)
                {
                    ListViewItem it = lvwAfstemTrans.Items.Add(lvi.Name, lvi.Text, 0);

                    for (int i = 1; i < lvi.SubItems.Count; i++)
                    {
                        string SubTekst = lvi.SubItems[i].Text;
                        it.SubItems.Add(SubTekst);

                    }
                }
                foreach (ListViewItem lvi in lvwTrans.SelectedItems)
                {
                    lvi.Remove();
                }
            }
            this.lvwAfstemTrans.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

    public class clsqry_bank
    {
        public int? Pid { get; set; }
        public DateTime? Dato { get; set; }
        public string Tekst { get; set; }
        public decimal? Belob { get; set; }
    }

    public class clsqry_trans
    {
        public int? Pid { get; set; }
        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public decimal? Belob { get; set; }
    }
}
