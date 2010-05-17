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
    public partial class FrmBetalingsForslag : Form
    {
        ColumnSorter lvwKreditor_ColumnSorter;
        ColumnSorter lvwKrdFaktura_ColumnSorter;
        private string DragDropKey;
        private DateTime m_initdate;
        private int m_lobnr = 0;


        public FrmBetalingsForslag()
        {
            InitializeComponent();
            this.lvwKreditor_ColumnSorter = new ColumnSorter();
            this.lvwKreditor.ListViewItemSorter = lvwKreditor_ColumnSorter;
            this.lvwKrdFaktura_ColumnSorter = new ColumnSorter();
            this.lvwKrdFaktura.ListViewItemSorter = lvwKrdFaktura_ColumnSorter;
        }

        private void lvwKreditor_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwKreditor_ColumnSorter.CurrentColumn = e.Column;
            this.lvwKreditor.Sort();
        }

        private void lvwKrdFaktura_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwKrdFaktura_ColumnSorter.CurrentColumn = e.Column;
            this.lvwKrdFaktura.Sort();
        }

        private void FrmBetalingsForslag_Load(object sender, EventArgs e)
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
            this.DatoKontingentForfald.Value = wt.AddDays(-wt.Day + 4);

        }


        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getBetalingsForslag();
        }

        private void getBetalingsForslag()
        {
            DateTime KontingentFradato = DateTime.MinValue;
            DateTime KontingentTildato = DateTime.MinValue;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            double dkontingent;
            int ikontingent;

            var qry_medlemmer = from h in Program.karMedlemmer
                                select h;

            this.lvwKreditor.Items.Clear();
            this.lvwKrdFaktura.Items.Clear();

            var antal = qry_medlemmer.Count();
            this.pgmForslag.Show();
            this.pgmForslag.Maximum = antal;
            this.pgmForslag.Minimum = 0;
            this.pgmForslag.Value = 0;
            this.pgmForslag.Step = 1;
            this.pgmForslag.Visible = true;
            this.Label_Forslagstekst.Visible = false;
            this.cmdBetal.Visible = false;

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
                    switch (KontingentFradato.Month)
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

                    ListViewItem it = lvwKrdFaktura.Items.Add(m.Nr.ToString(), m.Navn, 0);
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
            this.lvwKrdFaktura.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (AntalForslag == 0)
            {
                this.Label_Forslagstekst.Text = "Der er ingen forslag";
                this.Label_Forslagstekst.Visible = true;
                this.cmdBetal.Visible = false;
            }
            else
            {
                this.Label_Forslagstekst.Visible = false;
                this.cmdBetal.Visible = true;
            }
            this.pgmForslag.Visible = false;

        }
        private void lvwKreditor_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwKreditor.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwKrdFaktura_DragEnter(object sender, DragEventArgs e)
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

        private void lvwKrdFaktura_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwKreditor.SelectedItems)
            {
                ListViewItem it = lvwKrdFaktura.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwKreditor.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwKrdFaktura.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.cmdBetal.Visible = (this.lvwKrdFaktura.Items.Count > 0) ? true : false;
        }


        private void lvwKrdFaktura_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwKrdFaktura.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwKreditor_DragEnter(object sender, DragEventArgs e)
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

        private void lvwKreditor_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwKrdFaktura.SelectedItems)
            {
                ListViewItem it = lvwKreditor.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwKrdFaktura.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwKreditor.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.cmdBetal.Visible = (this.lvwKrdFaktura.Items.Count > 0) ? true : false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBetal_Click(object sender, EventArgs e)
        {
            string TilPBSFilename;
            int AntalFakturaer;
            int imax;
            string keyval;
            DateTime fradato;
            DateTime tildato;
            double advisbelob;
            if ((this.cmdBetal.Text == "Afslut"))
            {
                this.Close();
            }
            this.cmdForslag.Visible = false;
            this.cmdCancel.Visible = false;
            imax = lvwKrdFaktura.Items.Count;
            this.pgmBetal.Maximum = (imax * 4);
            this.pgmBetal.Minimum = 0;
            this.pgmBetal.Value = 0;
            this.pgmBetal.Visible = true;
            Program.dbData3060.ExecuteCommand("DELETE FROM tempKontforslag;");
            if ((imax == 0))
            {
                this.Label_Betaltekst.Text = "Der ikke noget at fakturere";
                this.Label_Betaltekst.Visible = true;
            }
            else
            {
                TempKontforslag rec_tempKontforslag = new TempKontforslag
                {
                    Betalingsdato = this.DatoKontingentForfald.Value,
                };
                Program.dbData3060.TempKontforslag.InsertOnSubmit(rec_tempKontforslag);
                var i = 0;
                foreach (ListViewItem lvi in lvwKrdFaktura.Items)
                {
                    this.pgmBetal.Value = ++i;
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
                this.pgmBetal.Value = imax * 2;
                if ((AntalFakturaer > 0))
                {
                    objPbs601.faktura_601_action(m_lobnr);
                    this.pgmBetal.Value = (imax * 3);
                    clsSFTP objSFTP = new clsSFTP();
                    objSFTP.WriteTilSFtp(m_lobnr);
                }
                this.pgmBetal.Value = (imax * 4);
                cmdBetal.Text = "Afslut";

                try
                {
                    var rec_tilpbs = (from t in Program.dbData3060.Tbltilpbs where t.Id == m_lobnr select t).First();
                    TilPBSFilename = "PBS" + rec_tilpbs.Leverancespecifikation + ".lst";
                }
                catch (System.InvalidOperationException)
                {
                    TilPBSFilename = "PBSNotFound.lst";
                }
                this.Label_Betaltekst.Text = ("Leverance til PBS er gemt i filen " + TilPBSFilename);
                this.Label_Betaltekst.Visible = true;
                this.pgmBetal.Visible = false;
            }
        }

        private void On_clsPbs601_SetLobnr(int lobnr)
        {
            m_lobnr = lobnr;
        }
    }
}
