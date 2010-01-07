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
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            double dkontingent;
            int ikontingent;


            var qry_medlemmer = from h in Program.karMedlemmer
                                where h.Nr > 500
                                select h;
            
            this.lvwMedlem.Items.Clear();
            this.lvwKontingent.Items.Clear();

            var antal = qry_medlemmer.Count();
            this.pgmForslag.Show();
            this.pgmForslag.Maximum = antal;
            this.pgmForslag.Minimum = 0;
            this.pgmForslag.Value = 0; 
            this.pgmForslag.Step = 1;
            this.pgmForslag.Visible = true;
            this.Label_Forslagstekst.Visible = false;
            this.cmdFakturer.Visible = false;

            pgmForslag.PerformStep();

            foreach (var m in qry_medlemmer)
            {
                bool bSelected = true;
                AntalMedlemmer++;
                if (!m.erMedlem()) //er ikke medlem
                {
                    bSelected = false;
                }
                else //Er medlem
                {
                    if (m.kontingentBetaltTilDato != null)  //'Der findes en kontingent-betaling
                    {
                        if (m.kontingentBetaltTilDato > this.DatoBetaltKontingentTil.Value)   //der er betalt kontingent efter DatoBetaltKontingentTil
                        {
                            bSelected = false;
                        }
                        else
                        {
                            if (m.kontingentBetaltTilDato >= m.indmeldelsesDato)
                            {
                                KontingentFradato = ((DateTime)m.kontingentBetaltTilDato).AddDays(1);
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
                    AntalForslag++;
                    dkontingent = (double.Parse(this.Aarskontingent.Text) / 365) * ((this.DatoKontingentTil.Value - KontingentFradato).Days + 1) + 0.49;
                    ikontingent = (int)dkontingent;

                    ListViewItem it = lvwKontingent.Items.Add(m.Navn, 0);
                    //it.Tag = m;
                    it.SubItems.Add(m.Nr.ToString());
                    it.SubItems.Add(m.Adresse);
                    it.SubItems.Add(m.Postnr);
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentFradato));
                    it.SubItems.Add(ikontingent.ToString());
                }
                pgmForslag.PerformStep();
            }
            this.lvwKontingent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (AntalForslag == 0)
            {
                this.Label_Forslagstekst.Text = "Der er ingen forslag";
                this.Label_Forslagstekst.Visible = true;
                this.cmdFakturer.Visible = false;
            }
            else
            {
                this.Label_Forslagstekst.Visible = false;
                this.cmdFakturer.Visible = true;
            }
            this.pgmForslag.Visible = false;

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
            this.cmdFakturer.Visible = (this.lvwKontingent.Items.Count > 0) ? true : false;
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
            this.cmdFakturer.Visible = (this.lvwKontingent.Items.Count > 0) ? true : false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
