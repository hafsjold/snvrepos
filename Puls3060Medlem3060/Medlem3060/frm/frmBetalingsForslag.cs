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

namespace nsPuls3060
{
    public partial class FrmBetalingsForslag : Form
    {
        ColumnSorter lvwKreditor_ColumnSorter;
        ColumnSorter lvwKrdFaktura_ColumnSorter;
        private string DragDropKey;
        private DateTime m_initdate;

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

        }


        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getBetalingsForslag();
        }

        private void getBetalingsForslag()
        {
            int AntalKreditorer = 0;
            int AntalForslag = 0;

            var qry_Kreditor = from h in Program.karFakturaer_k
                               where h.saldo > 0
                               join m in Program.karMedlemmer on h.kreditornr.ToString() equals m.Krdktonr
                               where Program.ValidatekBank(m.Bank)
                               select new
                               {
                                   h.fakid,
                                   m.Nr,
                                   m.Navn,
                                   m.Adresse,
                                   m.Postnr,
                                   m.Bank,
                                   h.faknr,
                                   h.saldo,
                               };

            this.lvwKreditor.Items.Clear();
            this.lvwKrdFaktura.Items.Clear();

            var antal = qry_Kreditor.Count();
            this.pgmForslag.Show();
            this.pgmForslag.Maximum = antal;
            this.pgmForslag.Minimum = 0;
            this.pgmForslag.Value = 0;
            this.pgmForslag.Step = 1;
            this.pgmForslag.Visible = true;
            this.Label_Forslagstekst.Visible = false;
            this.cmdBetal.Visible = false;

            pgmForslag.PerformStep();

            foreach (var m in qry_Kreditor)
            {
                AntalKreditorer++;

                AntalForslag++;

                ListViewItem it = lvwKreditor.Items.Add(m.fakid.ToString(), m.Navn, 0);
                //it.Tag = m;
                it.SubItems.Add(m.Nr.ToString());
                it.SubItems.Add(m.Adresse);
                it.SubItems.Add(m.Postnr);
                it.SubItems.Add(m.faknr.ToString());
                it.SubItems.Add((((decimal)m.saldo)/100).ToString());
                it.SubItems.Add(m.Bank);
                pgmForslag.PerformStep();
            }
            this.lvwKreditor.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (AntalForslag == 0)
            {
                this.Label_Forslagstekst.Text = "Der er ingen forslag";
                this.Label_Forslagstekst.Visible = true;
                this.cmdBetal.Visible = false;
            }
            else
            {
                this.Label_Forslagstekst.Visible = false;
                this.cmdBetal.Visible = false;
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
            string TilPBSFilename = "Unknown";
            int AntalBetalinger;
            int lobnr;
            int imax;
            string keyval;
            int Nr;
            int faknr;
            decimal advisbelob;
            string Bank;
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
            Program.dbData3060.tempBetalforslags.DeleteAllOnSubmit(Program.dbData3060.tempBetalforslags);
            Program.dbData3060.SubmitChanges();
            if ((imax == 0))
            {
                this.Label_Betaltekst.Text = "Der ikke noget at betale";
                this.Label_Betaltekst.Visible = true;
            }
            else
            {
                nsPbs3060.tempBetalforslag rec_tempBetalforslag = new nsPbs3060.tempBetalforslag
                {
                    betalingsdato = DateTime.Now,
                };
                Program.dbData3060.tempBetalforslags.InsertOnSubmit(rec_tempBetalforslag);
                var i = 0;
                foreach (ListViewItem lvi in lvwKrdFaktura.Items)
                {
                    this.pgmBetal.Value = ++i;
                    keyval = lvi.Name;
                    Nr = int.Parse(lvi.SubItems[1].Text);
                    faknr = int.Parse(lvi.SubItems[4].Text);
                    advisbelob = decimal.Parse(lvi.SubItems[5].Text);
                    Bank = lvi.SubItems[6].Text;

                    nsPbs3060.tempBetalforslaglinie rec_tempBetalforslaglinie = new nsPbs3060.tempBetalforslaglinie
                    {
                        Nr = Nr,
                        fakid = int.Parse(keyval),
                        advisbelob = (decimal)advisbelob,
                        bankregnr = Bank.Substring(0,4),
                        bankkontonr = Bank.Substring(5,10),
                        faknr = faknr,
                     };
                    rec_tempBetalforslag.tempBetalforslaglinies.Add(rec_tempBetalforslaglinie);
                }
                Program.dbData3060.SubmitChanges();

                clsOverfoersel objOverfoersel = new clsOverfoersel();

                Tuple<int,int> t = objOverfoersel.kreditor_fakturer_os1(Program.dbData3060); 
                AntalBetalinger = t.Item1;
                lobnr = t.Item2;
                this.pgmBetal.Value = imax * 2;
                if ((AntalBetalinger > 0))
                {
                    objOverfoersel.krdfaktura_overfoersel_action(Program.dbData3060, lobnr);
                    this.pgmBetal.Value = (imax * 3);
                    //clsSFTP objSFTP = new clsSFTP(Program.dbData3060);
                    //TilPBSFilename = objSFTP.WriteTilSFtp(Program.dbData3060, lobnr);
                    //objSFTP.DisconnectSFtp();
                    //objSFTP = null;
                    clsBankUdbetalingsUdskrift objBankUdbetalingsUdskrift = new clsBankUdbetalingsUdskrift();
                    objBankUdbetalingsUdskrift.BankUdbetalingsUdskrifter(Program.dbData3060, lobnr);
                    objBankUdbetalingsUdskrift = null;
                    objOverfoersel.overfoersel_mail(Program.dbData3060, lobnr);
                    clsSumma objSumma = new clsSumma();
                    objSumma.BogforUdBetalinger(lobnr);
                }
                this.pgmBetal.Value = (imax * 4);
                cmdBetal.Text = "Afslut";
                this.Label_Betaltekst.Text = ("Leverance til PBS i filen " + TilPBSFilename);
                this.Label_Betaltekst.Visible = true;
                this.pgmBetal.Visible = false;
            }
        }
    }
}
