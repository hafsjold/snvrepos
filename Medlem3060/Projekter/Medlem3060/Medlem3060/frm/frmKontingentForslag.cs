﻿using System;
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
        private int m_lobnr  = 0;


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

            if ((this.DatoKontingentTil.Value - this.DatoBetaltKontingentTil.Value) < (new TimeSpan(61, 0, 0, 0))) //Nov + Dec
            {
                this.DatoKontingentTil.Value.AddYears(1);
            }
            this.Aarskontingent.Text = "150";

            wt = m_initdate.AddMonths(1);
            this.DatoKontingentForfald.Value = clsOverfoersel.bankdageplus(wt.AddDays(-wt.Day + 2), 0);

        }

        private void DelsystemBSH_CheckStateChanged(object sender, EventArgs e)
        {
            DateTime wt = DateTime.Now;
            m_initdate = new DateTime(wt.Year, wt.Month, wt.Day);

            if (this.DelsystemBSH.Checked)
            {
                this.DatoKontingentForfald.Value = clsOverfoersel.bankdageplus(wt, 5);
            }
            else
            {
                wt = m_initdate.AddMonths(1);
                this.DatoKontingentForfald.Value = clsOverfoersel.bankdageplus(wt.AddDays(-wt.Day + 2), 0);
            }
        }

        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getKontingentForslag();
        }

        private void getKontingentForslag()
        {
            DateTime KontingentFradato = DateTime.MinValue;
            DateTime KontingentTildato = DateTime.MinValue;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            double dkontingent;
            int ikontingent;

            var qry_medlemmer = from h in Program.karMedlemmer
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
            this.DelsystemBSH.Visible = false;

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
                    switch (KontingentFradato.Month )
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            KontingentTildato = new DateTime(KontingentFradato.Year, 12, 31);
                            dkontingent = double.Parse(this.Aarskontingent.Text);
                            break;
                        
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            KontingentTildato = new DateTime(KontingentFradato.Year, 12, 31);
                            dkontingent = double.Parse(this.Aarskontingent.Text) / 2;
                            break;
                        
                        default:
                            KontingentTildato = new DateTime(KontingentFradato.Year + 1, 12, 31);
                            dkontingent = double.Parse(this.Aarskontingent.Text);
                            break;
                    }
                    ikontingent = (int)dkontingent;

                    ListViewItem it = lvwKontingent.Items.Add(m.Nr.ToString(), m.Navn, 0);
                    //it.Tag = m;
                    it.SubItems.Add(m.Nr.ToString());
                    it.SubItems.Add(m.Adresse);
                    it.SubItems.Add(m.Postnr);
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentFradato));
                    it.SubItems.Add(ikontingent.ToString());
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentTildato));
                }
                pgmForslag.PerformStep();
            }
            this.lvwKontingent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (AntalForslag == 0)
            {
                this.Label_Forslagstekst.Text = "Der er ingen forslag";
                this.Label_Forslagstekst.Visible = true;
                this.cmdFakturer.Visible = false;
                this.DelsystemBSH.Visible = false;
            }
            else
            {
                this.Label_Forslagstekst.Visible = false;
                this.cmdFakturer.Visible = true;
                this.DelsystemBSH.Visible = true;
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
                ListViewItem it = lvwKontingent.Items.Add(lvi.Name, lvi.Text, 0);

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
            this.DelsystemBSH.Visible = (this.lvwKontingent.Items.Count > 0) ? true : false;
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
                ListViewItem it = lvwMedlem.Items.Add(lvi.Name, lvi.Text, 0);

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
            this.DelsystemBSH.Visible = (this.lvwKontingent.Items.Count > 0) ? true : false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdFakturer_Click(object sender, EventArgs e)
        {
            string TilPBSFilename = "Unknown";
            int AntalFakturaer;
            int imax;
            string keyval;
            DateTime fradato;
            DateTime tildato;
            double advisbelob;
            if ((this.cmdFakturer.Text == "Afslut"))
            {
                this.Close();
            }
            this.cmdForslag.Visible = false;
            this.cmdCancel.Visible = false;
            imax = lvwKontingent.Items.Count;
            this.pgmFaktura.Maximum = (imax * 4);
            this.pgmFaktura.Minimum = 0;
            this.pgmFaktura.Value = 0;
            this.pgmFaktura.Visible = true;
            Program.dbData3060.ExecuteCommand("DELETE FROM tempKontforslag;");
            if ((imax == 0))
            {
                this.Label_Fakturatekst.Text = "Der ikke noget at fakturere";
                this.Label_Fakturatekst.Visible = true;
            }
            else
            {
                TempKontforslag rec_tempKontforslag = new TempKontforslag
                {
                    Betalingsdato = clsOverfoersel.bankdageplus(this.DatoKontingentForfald.Value,0),
                    Bsh = this.DelsystemBSH.Checked
                };
                Program.dbData3060.TempKontforslag.InsertOnSubmit(rec_tempKontforslag);
                var i = 0;
                foreach (ListViewItem lvi in lvwKontingent.Items)
                {
                    this.pgmFaktura.Value = ++i;
                    keyval = lvi.Name;
                    fradato = DateTime.Parse(lvi.SubItems[4].Text);
                    advisbelob = double.Parse(lvi.SubItems[5].Text);
                    tildato = DateTime.Parse(lvi.SubItems[6].Text);

                    TempKontforslaglinie rec_tempKontforslaglinie = new TempKontforslaglinie
                    {
                        Nr = int.Parse(keyval),
                        Advisbelob = (decimal)advisbelob,
                        Fradato = fradato,
                        Tildato = tildato
                    };
                    rec_tempKontforslag.TempKontforslaglinie.Add(rec_tempKontforslaglinie);
                }
                Program.dbData3060.SubmitChanges();

                clsPbs601 objPbs601 = new clsPbs601();
                nsPuls3060.clsPbs601.SetLobnr += new nsPuls3060.clsPbs601.Pbs601DelegateHandler(On_clsPbs601_SetLobnr);

                AntalFakturaer = objPbs601.kontingent_fakturer_bs1();
                this.pgmFaktura.Value = imax * 2;
                if ((AntalFakturaer > 0))
                {
                    objPbs601.faktura_og_rykker_601_action(m_lobnr, fakType.fdfaktura);
                    this.pgmFaktura.Value = (imax * 3);
                    clsSFTP objSFTP = new clsSFTP();
                    TilPBSFilename = objSFTP.WriteTilSFtp(m_lobnr);
                }
                this.pgmFaktura.Value = (imax * 4);
                cmdFakturer.Text = "Afslut";
                this.DelsystemBSH.Visible = false;
                this.Label_Fakturatekst.Text = ("Leverance til PBS i filen " + TilPBSFilename);
                this.Label_Fakturatekst.Visible = true;
                this.pgmFaktura.Visible = false;
            }
        }
        
        private void On_clsPbs601_SetLobnr(int lobnr)
        {
            m_lobnr = lobnr;
        }


    }
}
