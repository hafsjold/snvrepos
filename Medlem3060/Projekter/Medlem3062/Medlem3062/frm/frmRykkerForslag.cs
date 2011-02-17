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
    public partial class FrmRykkerForslag : Form
    {
        ColumnSorter lvwMedlem_ColumnSorter;
        ColumnSorter lvwRykker_ColumnSorter;
        private string DragDropKey;
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
        }

        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getRykkerForslag();
        }

        private void getRykkerForslag()
        {
            int AntalForslag = 0;
            IEnumerable<clsqry_medlemmer> qry_medlemmer;
            if (this.RykketTidligere.Checked)
            {
                clsRest objRest = new clsRest();
                string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "tidligererykker");
                XDocument xmldata = XDocument.Parse(strxmldata);
                string Status = xmldata.Descendants("Status").First().Value;
                if (Status != "True") return;
                qry_medlemmer = from h in xmldata.Descendants("RykkerForslag")
                                orderby clsPassXmlDoc.attr_val_int(h, "Nr")
                                select new clsqry_medlemmer
                               {
                                   Nr = clsPassXmlDoc.attr_val_int(h, "Nr"),
                                   Navn = clsPassXmlDoc.attr_val_string(h, "Navn"),
                                   Adresse = clsPassXmlDoc.attr_val_string(h, "Adresse"),
                                   Postnr = clsPassXmlDoc.attr_val_string(h, "Postnr"),
                                   Betalingsdato = clsPassXmlDoc.attr_val_date(h, "Betalingsdato"),
                                   Advisbelob = (decimal)clsPassXmlDoc.attr_val_double(h, "Advisbelob"),
                                   Faknr = clsPassXmlDoc.attr_val_int(h, "Faknr")
                               };


            }
            else
            {

                clsRest objRest = new clsRest();
                string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "rykkerforslag");
                XDocument xmldata = XDocument.Parse(strxmldata);
                string Status = xmldata.Descendants("Status").First().Value;
                if (Status != "True") return;
                qry_medlemmer = from h in xmldata.Descendants("RykkerForslag")
                                orderby clsPassXmlDoc.attr_val_int(h, "Nr")
                                select new clsqry_medlemmer
                                {
                                    Nr = clsPassXmlDoc.attr_val_int(h, "Nr"),
                                    Navn = clsPassXmlDoc.attr_val_string(h, "Navn"),
                                    Adresse = clsPassXmlDoc.attr_val_string(h, "Adresse"),
                                    Postnr = clsPassXmlDoc.attr_val_string(h, "Postnr"),
                                    Betalingsdato = clsPassXmlDoc.attr_val_date(h, "Betalingsdato"),
                                    Advisbelob = (decimal)clsPassXmlDoc.attr_val_double(h, "Advisbelob"),
                                    Faknr = clsPassXmlDoc.attr_val_int(h, "Faknr")
                                };
            }


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
            this.DelsystemBSH.Visible = false;

            pgmForslag.PerformStep();

            foreach (var m in qry_medlemmer)
            {
                    AntalForslag++;
                    ListViewItem it = lvwMedlem.Items.Add(m.Nr.ToString(), m.Navn, 0);
                    //it.Tag = m;
                    it.SubItems.Add(m.Nr.ToString());
                    it.SubItems.Add(m.Adresse);
                    it.SubItems.Add(m.Postnr);
                    it.SubItems.Add(string.Format("{0:yyyy-MM-dd}", m.Betalingsdato));
                    it.SubItems.Add(m.Advisbelob.ToString());
                    it.SubItems.Add(m.Faknr.ToString());
                    pgmForslag.PerformStep();
            }
            this.lvwMedlem.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (AntalForslag == 0)
            {
                this.Label_Forslagstekst.Text = "Der er ingen forslag";
                this.Label_Forslagstekst.Visible = true;
                this.cmdRykkere.Visible = false;
                this.DelsystemBSH.Visible = false;
            }
            else
            {
                this.Label_Forslagstekst.Visible = false;
                //this.cmdRykkere.Visible = true;
                //this.DelsystemBSH.Visible = true;
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
            this.DelsystemBSH.Visible = (this.lvwRykker.Items.Count > 0) ? true : false;
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
            this.DelsystemBSH.Visible = (this.lvwRykker.Items.Count > 0) ? true : false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdRykkere_Click(object sender, EventArgs e)
        {
            string TilPBSFilename;
            int AntalRykkere;
            int imax;
            string keyval;
            int faknr;
            double advisbelob;
            if ((this.cmdRykkere.Text == "Afslut"))
            {
                this.Close();
            }
            this.cmdForslag.Visible = false;
            this.cmdCancel.Visible = false;
            imax = lvwRykker.Items.Count;
            this.pgmRykker.Maximum = (imax * 4);
            this.pgmRykker.Minimum = 0;
            this.pgmRykker.Value = 0;
            this.pgmRykker.Visible = true;
            Program.dbData3060.TempRykkerforslag.DeleteAllOnSubmit(Program.dbData3060.TempRykkerforslag);
            Program.dbData3060.SubmitChanges();

            if ((imax == 0))
            {
                this.Label_Rykkertekst.Text = "Der ikke noget at rykkere";
                this.Label_Rykkertekst.Visible = true;
            }
            else
            {
                TempRykkerforslag rec_tempRykkerforslag = new TempRykkerforslag
                {
                    Betalingsdato = clsOverfoersel.bankdageplus(DateTime.Today, 5),
                    Bsh = this.DelsystemBSH.Checked
                };
                Program.dbData3060.TempRykkerforslag.InsertOnSubmit(rec_tempRykkerforslag);
                var i = 0;
                foreach (ListViewItem lvi in lvwRykker.Items)
                {
                    this.pgmRykker.Value = ++i;
                    keyval = lvi.Name;
                    advisbelob = double.Parse(lvi.SubItems[5].Text);
                    faknr = int.Parse(lvi.SubItems[6].Text);

                    TempRykkerforslaglinie rec_tempRykkerforslaglinie = new TempRykkerforslaglinie
                    {
                        Nr = int.Parse(keyval),
                        Advisbelob = (decimal)advisbelob,
                        Faknr = faknr
                    };
                    rec_tempRykkerforslag.TempRykkerforslaglinie.Add(rec_tempRykkerforslaglinie);
                }
                Program.dbData3060.SubmitChanges();

                clsPbs601 objPbs601 = new clsPbs601();
                nsPuls3060.clsPbs601.SetLobnr += new nsPuls3060.clsPbs601.Pbs601DelegateHandler(On_clsPbs601_SetLobnr);

                AntalRykkere = objPbs601.rykkere_bsh();
                this.pgmRykker.Value = imax * 2;
                if ((AntalRykkere > 0))
                {
                    if (this.DelsystemBSH.Checked) //RYKKERE med Indbetalingskort
                    {
                        objPbs601.faktura_og_rykker_601_action(m_lobnr, fakType.fdrykker);
                        this.pgmRykker.Value = (imax * 3);
                        //clsSFTP objSFTP = new clsSFTP();
                        //objSFTP.WriteTilSFtp(m_lobnr);
                        //objSFTP.DisconnectSFtp();
                        //objSFTP = null;
                    }
                    else //RYKKERE som emails
                    {
                        objPbs601.rykker_email(m_lobnr);
                        this.pgmRykker.Value = (imax * 3);
                    }
                }
                this.pgmRykker.Value = (imax * 4);
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
                this.Label_Rykkertekst.Text = ("Leverance til PBS er gemt i filen " + TilPBSFilename);
                this.Label_Rykkertekst.Visible = true;
                this.pgmRykker.Visible = false;
            }
        }

        private void On_clsPbs601_SetLobnr(int lobnr)
        {
            m_lobnr = lobnr;
        }
    }

    public class clsqry_medlemmer
    {
        public int? Nr { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public DateTime? Betalingsdato { get; set; }
        public decimal? Advisbelob { get; set; }
        public int? Faknr { get; set; }
    }
}
