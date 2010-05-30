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
    public partial class FrmRykkerForslag : Form
    {
        ColumnSorter lvwMedlem_ColumnSorter;
        ColumnSorter lvwRykker_ColumnSorter;
        private string DragDropKey;
        private DateTime m_initdate;
        private int m_lobnr = 0;


        public FrmRykkerForslag()
        {
            InitializeComponent();
            this.lvwMedlem_ColumnSorter = new ColumnSorter();
            this.lvwMedlem.ListViewItemSorter = lvwMedlem_ColumnSorter;
            this.lvwRykker_ColumnSorter = new ColumnSorter();
            this.lvwRykker.ListViewItemSorter = lvwRykker_ColumnSorter;
        }

        private void lvwMedlem_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwMedlem_ColumnSorter.CurrentColumn = e.Column;
            this.lvwMedlem.Sort();
        }

        private void lvwRykker_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwRykker_ColumnSorter.CurrentColumn = e.Column;
            this.lvwRykker.Sort();
        }

        private void FrmRykkerForslag_Load(object sender, EventArgs e)
        {
            DateTime wt = DateTime.Now;
            m_initdate = new DateTime(wt.Year, wt.Month, wt.Day);

            wt = m_initdate.AddMonths(13 - m_initdate.Month);
            this.DatoKontingentTil.Value = wt.AddDays(-wt.Day);

            wt = m_initdate.AddMonths(1);
            this.DatoKontingentForfald.Value = wt.AddDays(-wt.Day + 4);
        }


        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getRykkerForslag();
        }

        private void getRykkerForslag()
        {
            DateTime RykkerFradato = DateTime.MinValue;
            DateTime RykkerTildato = DateTime.MinValue;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;

            var qry_medlemmer = from h in Program.karMedlemmer
                                join f in Program.dbData3060.Tblfak on h.Nr equals f.Nr
                                where f.SFaknr == null
                                select new
                                {
                                    h.Nr,
                                    h.Navn,
                                    h.Adresse,
                                    h.Postnr,
                                    f.Betalingsdato,
                                    f.Fradato,
                                    f.Tildato,
                                    f.Advisbelob
                                };

            this.lvwMedlem.Items.Clear();
            this.lvwRykker.Items.Clear();

            var antal = qry_medlemmer.Count();
            this.pgmForslag.Show();
            this.pgmForslag.Maximum = antal;
            this.pgmForslag.Minimum = 0;
            this.pgmForslag.Value = 0;
            this.pgmForslag.Step = 1;
            this.pgmForslag.Visible = true;
            this.Label_Forslagstekst.Visible = false;
            this.cmdRykkere.Visible = false;

            pgmForslag.PerformStep();

            foreach (var m in qry_medlemmer)
            {
                AntalMedlemmer++;
                ListViewItem it = lvwRykker.Items.Add(m.Nr.ToString(), m.Navn, 0);
                //it.Tag = m;
                it.SubItems.Add(m.Nr.ToString());
                it.SubItems.Add(m.Adresse);
                it.SubItems.Add(m.Postnr);
                it.SubItems.Add(string.Format("{0:dd-MM-yyy}", m.Fradato));
                it.SubItems.Add(m.Advisbelob.ToString());
                it.SubItems.Add(string.Format("{0:dd-MM-yyy}", m.Tildato));
                pgmForslag.PerformStep();
            }
            this.lvwRykker.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (AntalForslag == 0)
            {
                this.Label_Forslagstekst.Text = "Der er ingen forslag";
                this.Label_Forslagstekst.Visible = true;
                this.cmdRykkere.Visible = false;
            }
            else
            {
                this.Label_Forslagstekst.Visible = false;
                this.cmdRykkere.Visible = true;
            }
            this.pgmForslag.Visible = false;

        }
        private void lvwMedlem_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwMedlem.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwRykker_DragEnter(object sender, DragEventArgs e)
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

        private void lvwRykker_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwMedlem.SelectedItems)
            {
                ListViewItem it = lvwRykker.Items.Add(lvi.Name, lvi.Text, 0);

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
            this.lvwRykker.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.cmdRykkere.Visible = (this.lvwRykker.Items.Count > 0) ? true : false;
        }


        private void lvwRykker_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = random.Next(1000, 64000).ToString();
            lvwRykker.DoDragDrop(DragDropKey, DragDropEffects.Move);
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
            foreach (ListViewItem lvi in lvwRykker.SelectedItems)
            {
                ListViewItem it = lvwMedlem.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwRykker.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwMedlem.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            this.cmdRykkere.Visible = (this.lvwRykker.Items.Count > 0) ? true : false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdFakturer_Click(object sender, EventArgs e)
        {
            string TilPBSFilename;
            int AntalFakturaer;
            int imax;
            string keyval;
            DateTime fradato;
            DateTime tildato;
            double advisbelob;
            if ((this.cmdRykkere.Text == "Afslut"))
            {
                this.Close();
            }
            this.cmdForslag.Visible = false;
            this.cmdCancel.Visible = false;
            imax = lvwRykker.Items.Count;
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
                    Betalingsdato = this.DatoKontingentForfald.Value,
                };
                Program.dbData3060.TempKontforslag.InsertOnSubmit(rec_tempKontforslag);
                var i = 0;
                foreach (ListViewItem lvi in lvwRykker.Items)
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
                    objPbs601.faktura_601_action(m_lobnr);
                    this.pgmFaktura.Value = (imax * 3);
                    //objPbs601.WriteTilPbsFile(m_lobnr);
                    clsSFTP objSFTP = new clsSFTP();
                    objSFTP.WriteTilSFtp(m_lobnr);
                }
                this.pgmFaktura.Value = (imax * 4);
                cmdRykkere.Text = "Afslut";

                try
                {
                    var rec_tilpbs = (from t in Program.dbData3060.Tbltilpbs where t.Id == m_lobnr select t).First();
                    TilPBSFilename = "PBS" + rec_tilpbs.Leverancespecifikation + ".lst";
                }
                catch (System.InvalidOperationException)
                {
                    TilPBSFilename = "PBSNotFound.lst";
                }
                this.Label_Fakturatekst.Text = ("Leverance til PBS er gemt i filen " + TilPBSFilename);
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
