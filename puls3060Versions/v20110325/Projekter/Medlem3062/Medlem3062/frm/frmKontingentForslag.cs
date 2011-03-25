using System;
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
    public partial class FrmKontingentForslag : Form
    {
        ColumnSorter lvwMedlem_ColumnSorter;
        ColumnSorter lvwKontingent_ColumnSorter;
        private string DragDropKey;
        private DateTime m_initdate;
        private int m_lobnr = 0;


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
            this.Aarskontingent.Text = "150";
            this.AarskontingentPbs.Text = "150";

            wt = m_initdate.AddMonths(1);
            this.DatoKontingentForfald.Value = clsUtil.bankdageplus(wt.AddDays(-wt.Day + 2), 0);

        }

        private void DelsystemBSH_CheckStateChanged(object sender, EventArgs e)
        {
            DateTime wt = DateTime.Now;
            m_initdate = new DateTime(wt.Year, wt.Month, wt.Day);

            if (this.DelsystemBSH.Checked)
            {
                this.DatoKontingentForfald.Value = clsUtil.bankdageplus(wt, 5);
            }
            else
            {
                wt = m_initdate.AddMonths(1);
                this.DatoKontingentForfald.Value = clsUtil.bankdageplus(wt.AddDays(-wt.Day + 2), 0);
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
            bool tilmeldtpbs = false;
            bool indmeldelse = false;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            double dkontingent;
            int ikontingent;

            this.lvwMedlem.Items.Clear();
            this.lvwKontingent.Items.Clear();

            var antal = 2;
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

            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "kontingentforslag");
            XDocument xmldata = XDocument.Parse(strxmldata);
            string Status = xmldata.Descendants("Status").First().Value;
            if (Status == "True")
            {
                var list = from forslag in xmldata.Descendants("KontingentForslag") select forslag;
                antal = list.Count();

                foreach (var forslag in list)
                {
                    AntalMedlemmer++;
                    tilmeldtpbs = false;
                    indmeldelse = false;

                    var wNr = forslag.Descendants("Nr").First().Value.Trim();
                    var wNavn = forslag.Descendants("Navn").First().Value.Trim();
                    var wAdresse = forslag.Descendants("Adresse").First().Value.Trim();
                    var wPostnr = forslag.Descendants("Postnr").First().Value.Trim();
                    KontingentFradato = DateTime.Parse(forslag.Descendants("KontingentFradato").First().Value.Trim());
                    tilmeldtpbs = (forslag.Descendants("Indmeldelse").First().Value.Trim() == "True") ? true : false;
                    tilmeldtpbs = (forslag.Descendants("Tilmeldtpbs").First().Value.Trim() == "True") ? true : false;




                    AntalForslag++;
                    //tilmeldtpbs = clsPbs.gettilmeldtpbs(m.Nr);
                    switch (KontingentFradato.Month)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                            KontingentTildato = new DateTime(KontingentFradato.Year, 12, 31);
                            if (indmeldelse)
                            {
                                dkontingent = double.Parse(this.AarskontingentPbs.Text);
                            }
                            else
                            {
                                dkontingent = (tilmeldtpbs) ? double.Parse(this.AarskontingentPbs.Text) : double.Parse(this.Aarskontingent.Text);
                            }
                            break;

                        default:
                            KontingentTildato = new DateTime(KontingentFradato.Year + 1, 12, 31);
                            if (indmeldelse)
                            {
                                dkontingent = double.Parse(this.AarskontingentPbs.Text) * 3 / 2;
                            }
                            else
                            {
                                dkontingent = (tilmeldtpbs) ? double.Parse(this.AarskontingentPbs.Text) * 3 / 2 : double.Parse(this.Aarskontingent.Text) * 3 / 2;
                            }
                            break;
                    }
                    ikontingent = (int)dkontingent;

                    ListViewItem it = lvwKontingent.Items.Add(wNr, wNavn, 0);
                    //it.Tag = m;
                    it.SubItems.Add(wNr);
                    it.SubItems.Add(wAdresse);
                    it.SubItems.Add(wPostnr);
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentFradato));
                    it.SubItems.Add(ikontingent.ToString());
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentTildato));
                    it.SubItems.Add((indmeldelse) ? "J" : "N");
                    it.SubItems.Add((tilmeldtpbs) ? "J" : "N");

                    pgmForslag.PerformStep();
                }
                this.lvwKontingent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
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

        private void cmdFakturer_Click(object sender, EventArgs e)
        {
            string TilPBSFilename = "Unknown";
            //int AntalFakturaer;
            int imax;
            string keyval;
            DateTime fradato;
            DateTime tildato;
            bool tilmeldtpbs;
            bool indmeldelse;

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
                XElement headxml = new XElement("TempKontforslag");
                headxml.Add(new XElement("Betalingsdato", clsUtil.bankdageplus(this.DatoKontingentForfald.Value, 0)));
                headxml.Add(new XElement("Bsh", this.DelsystemBSH.Checked));

                var i = 0;
                foreach (ListViewItem lvi in lvwKontingent.Items)
                {
                    this.pgmFaktura.Value = ++i;
                    keyval = lvi.Name;
                    fradato = DateTime.Parse(lvi.SubItems[4].Text);
                    advisbelob = double.Parse(lvi.SubItems[5].Text);
                    tildato = DateTime.Parse(lvi.SubItems[6].Text);
                    indmeldelse = (lvi.SubItems[7].Text == "J") ? true : false;
                    tilmeldtpbs = (lvi.SubItems[8].Text == "J") ? true : false;

                    XElement linxml = new XElement("TempKontforslaglinie");
                    linxml.Add(new XElement("Nr", int.Parse(keyval)));
                    linxml.Add(new XElement("Advisbelob", (decimal)advisbelob));
                    linxml.Add(new XElement("Fradato", fradato));
                    linxml.Add(new XElement("Tildato", tildato));
                    linxml.Add(new XElement("Indmeldelse", indmeldelse));
                    linxml.Add(new XElement("Tilmeldtpbs", tilmeldtpbs));
                    headxml.Add(new XElement(linxml));
                }
                clsRest objRest = new clsRest();
                string strheadxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + headxml.ToString();
                string result = objRest.HttpPost2(clsRest.urlBaseType.data, "pbs601", strheadxml);

                /*
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
                    objSFTP.DisconnectSFtp();
                    objSFTP = null;
                }
                */
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
