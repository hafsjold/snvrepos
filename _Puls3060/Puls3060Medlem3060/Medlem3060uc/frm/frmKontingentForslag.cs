using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using nsPbs3060;

namespace Medlem3060uc
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

            if ((this.DatoKontingentTil.Value - this.DatoBetaltKontingentTil.Value) < (new TimeSpan(184, 0, 0, 0))) //Jul + Aug + Sep + Okt + Nov + Dec
            {
                this.DatoKontingentTil.Value = this.DatoKontingentTil.Value.AddYears(1);
            }
            wt = m_initdate.AddMonths(1);
            this.DatoKontingentForfald.Value = clsHelper.bankdageplus(wt.AddDays(-wt.Day + 2), 0);

        }

        private void DelsystemBSH_CheckStateChanged(object sender, EventArgs e)
        {
            DateTime wt = DateTime.Now;
            m_initdate = new DateTime(wt.Year, wt.Month, wt.Day);

            if (this.DelsystemBSH.Checked)
            {
                this.DatoKontingentForfald.Value = clsHelper.bankdageplus(wt, 5);
            }
            else
            {
                wt = m_initdate.AddMonths(1);
                this.DatoKontingentForfald.Value = clsHelper.bankdageplus(wt.AddDays(-wt.Day + 2), 0);
            }
        }

        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getRSMembership_KontingentForslag();
        }

        private void getRSMembership_KontingentForslag()
        {
            clsPbs601 objPbs601 = new clsPbs601();
            List<string[]> items = objPbs601.RSMembership_KontingentForslag(this.DatoBetaltKontingentTil.Value, Program.dbData3060);
            int AntalForslag = items.Count();
            foreach (var item in items)
            {
                ListViewItem it = lvwKontingent.Items.Add(item[0], item[1], 0);
                it.SubItems.Add(item[0]);
                it.SubItems.Add(item[2]);
                it.SubItems.Add(item[3]);
                it.SubItems.Add(item[4]);
                it.SubItems.Add(item[5]);
                it.SubItems.Add(item[6]);
                it.SubItems.Add(item[7]);
                it.SubItems.Add(item[8]);
                it.SubItems.Add(item[9]);
                it.SubItems.Add(item[10]);
            }

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

        private void cmdRSMembership_Fakturer_Click(object sender, EventArgs e)
        {
            string TilPBSFilename = "Unknown";
            int AntalFakturaer;
            int lobnr;
            int imax;
            string keyval;
            DateTime fradato;
            DateTime tildato;
            bool tilmeldtpbs;
            bool indmeldelse;
            int subscriber_id;
            int memberid;
            string name;

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
            if ((imax == 0))
            {
                this.Label_Fakturatekst.Text = "Der ikke noget at fakturere";
                this.Label_Fakturatekst.Visible = true;
            }
            else
            {
                puls3060_nyEntities jdb = new puls3060_nyEntities(true);
                Memkontingentforslag memKontingentforslag = new Memkontingentforslag();
                var i = 0;
                foreach (ListViewItem lvi in lvwKontingent.Items)
                {
                    this.pgmFaktura.Value = ++i;
                    keyval = lvi.Name;
                    name = lvi.Text;
                    fradato = DateTime.Parse(lvi.SubItems[4].Text);
                    advisbelob = double.Parse(lvi.SubItems[5].Text);
                    tildato = DateTime.Parse(lvi.SubItems[6].Text);
                    indmeldelse = (lvi.SubItems[7].Text == "J") ? true : false;
                    tilmeldtpbs = (lvi.SubItems[8].Text == "J") ? true : false;
                    subscriber_id = int.Parse(lvi.SubItems[9].Text);
                    memberid = (!string.IsNullOrEmpty(lvi.SubItems[10].Text)) ? int.Parse(lvi.SubItems[10].Text) : (int)(from r in Program.dbData3060.nextval("memberid") select r.id).First();

                    recKontingentforslag rec_Kontingentforslag = new recKontingentforslag
                    {
                        betalingsdato = clsHelper.bankdageplus(this.DatoKontingentForfald.Value, 0),
                        bsh = this.DelsystemBSH.Checked,
                        user_id = int.Parse(keyval),
                        membership_id = 6,
                        advisbelob = (decimal)advisbelob,
                        fradato = fradato,
                        tildato = tildato,
                        indmeldelse = indmeldelse,
                        tilmeldtpbs = tilmeldtpbs,
                        subscriber_id = subscriber_id,
                        memberid = memberid,
                        name = name
                    };
                    memKontingentforslag.Add(rec_Kontingentforslag);
                }

                clsPbs601 objPbs601 = new clsPbs601();

                Tuple<int, int> tresult = objPbs601.rsmembeshhip_kontingent_fakturer_bs1(Program.dbData3060, jdb, memKontingentforslag);
                AntalFakturaer = tresult.Item1;
                lobnr = tresult.Item2;
                this.pgmFaktura.Value = imax * 2;
                if ((AntalFakturaer > 0))
                {
                    objPbs601.faktura_og_rykker_601_action(Program.dbData3060, lobnr, fakType.fdrsmembership);
                    this.pgmFaktura.Value = (imax * 3);
                    clsSFTP objSFTP = new clsSFTP(Program.dbData3060);
                    TilPBSFilename = objSFTP.WriteTilSFtp(Program.dbData3060, lobnr);
                    objSFTP.DisconnectSFtp();
                    objSFTP = null;
                }
                this.pgmFaktura.Value = (imax * 4);
                cmdFakturer.Text = "Afslut";
                this.DelsystemBSH.Visible = false;
                this.Label_Fakturatekst.Text = ("Leverance til PBS i filen " + TilPBSFilename);
                this.Label_Fakturatekst.Visible = true;
                this.pgmFaktura.Visible = false;
            }
        }
    }
}
