using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace nsPuls3060
{
    public partial class FrmKontingentForslag : Form
    {
        ColumnSorter lvwMedlem_ColumnSorter;
        ColumnSorter lvwKontingent_ColumnSorter;
        private string DragDropKey;
        private DateTime m_initdate;

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
            DateTime wt = DateTime.Now;
            m_initdate = new DateTime(wt.Year, wt.Month, wt.Day);

            wt = m_initdate.AddMonths(1);
            this.DatoBetaltKontingentTil.Value = wt.AddDays(-wt.Day);

            wt = m_initdate.AddMonths(13 - m_initdate.Month);
            this.DatoKontingentTil.Value = wt.AddDays(-wt.Day);

            if ((this.DatoKontingentTil.Value - this.DatoBetaltKontingentTil.Value) < (new TimeSpan(183, 0, 0, 0)))
            {
                this.DatoKontingentTil.Value.AddYears(1);
            }
            this.Aarskontingent.Text = "150";

            wt = m_initdate.AddMonths(1);
            this.DatoKontingentForfald.Value = wt.AddDays(-wt.Day + 4);

        }


        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getKontingentForslag();
        }

        private void getKontingentForslag()
        {
            DateTime KontingentFradato = DateTime.MinValue;

            var qry_medlemmer = from h in Program.karMedlemmer
                                orderby h.Kaldenavn
                                select h;

            var antal = qry_medlemmer.Count();
            foreach (var m in qry_medlemmer)
            {
                bool bSelected = true;
                if (!m.erMedlem()) //er ikke medlem
                {
                    bSelected = false;
                }
                else //Er medlem
                {
                    if (m.kontingentBetaltDato != null)  //'Der findes en kontingent-betaling
                    {
                        if (m.kontingentBetaltDato > this.DatoBetaltKontingentTil.Value)   //der er betalt kontingent efter DatoBetaltKontingentTil
                        {
                            bSelected = false;
                        }
                        else
                        {
                            if (m.kontingentBetaltDato >= m.indmeldelsesDato)
                            {
                                KontingentFradato = ((DateTime)m.kontingentBetaltDato).AddDays(1);
                            }
                        }
                    }
                    else  //Der findes ingen kontingent-betaling
                    {
                        KontingentFradato = (DateTime)m.indmeldelsesDato;
                    }
                }

                if (bSelected)
                {
                    if (m.opkrævningsDato != null) //Der findes en opkrævning
                    {
                        if (((DateTime)m.opkrævningsDato) > KontingentFradato)
                        {
                            bSelected = false;
                        }
                    }
                }

                if (bSelected)
                {
                    ListViewItem it = lvwKontingent.Items.Add(m.Navn, 0);
                    it.SubItems.Add(m.Nr.ToString());
                    it.SubItems.Add(m.Adresse);
                    it.SubItems.Add(m.Postnr);
                    it.SubItems.Add(m.Bynavn);
                }
                this.lvwKontingent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }
        private void lvwMedlem_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwMedlem.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwKontingent_DragEnter(object sender, DragEventArgs e)
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

        private void lvwKontingent_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwMedlem.SelectedItems)
            {
                string Tekst = lvi.Text;
                ListViewItem it = lvwKontingent.Items.Add(Tekst);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwMedlem.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwKontingent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        private void lvwKontingent_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwKontingent.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwMedlem_DragEnter(object sender, DragEventArgs e)
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

        private void lvwMedlem_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwKontingent.SelectedItems)
            {
                string Tekst = lvi.Text;
                ListViewItem it = lvwMedlem.Items.Add(lvi.Text);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwKontingent.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwMedlem.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


    }
}
