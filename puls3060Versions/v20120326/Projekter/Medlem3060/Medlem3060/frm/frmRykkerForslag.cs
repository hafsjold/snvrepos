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
                qry_medlemmer = from h in Program.dbData3060.tblMedlems
                                join f in Program.dbData3060.tblfaks on h.Nr equals f.Nr
                                where f.SFaknr == null &&
                                      f.rykkerstop == false &&
                                      (int)(from q in Program.dbData3060.tblrykkers where q.faknr == f.faknr select q).Count() > 0
                                orderby f.fradato, f.id
                                select new clsqry_medlemmer
                                {
                                    Nr = h.Nr,
                                    Navn = h.Navn,
                                    Adresse = h.Adresse,
                                    Postnr = h.Postnr,
                                    Betalingsdato = f.betalingsdato,
                                    Advisbelob = f.advisbelob,
                                    Faknr = f.faknr
                                };
            }
            else
            {
                qry_medlemmer = from h in Program.dbData3060.tblMedlems
                                join f in Program.dbData3060.tblfaks on h.Nr equals f.Nr
                                where f.SFaknr == null &&
                                      f.rykkerstop == false &&
                                      f.betalingsdato.Value.AddDays(7) <= DateTime.Today &&
                                      (int)(from q in Program.dbData3060.tblrykkers where q.faknr == f.faknr select q).Count() == 0
                                orderby f.fradato, f.id
                                select new clsqry_medlemmer
                                {
                                    Nr = h.Nr,
                                    Navn = h.Navn,
                                    Adresse = h.Adresse,
                                    Postnr = h.Postnr,
                                    Betalingsdato = f.betalingsdato,
                                    Advisbelob = f.advisbelob,
                                    Faknr = f.faknr
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
                if ((bool)Program.dbData3060.kanRykkes(m.Nr))
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
            Program.dbData3060.tempRykkerforslags.DeleteAllOnSubmit(Program.dbData3060.tempRykkerforslags);
            Program.dbData3060.SubmitChanges();

            if ((imax == 0))
            {
                this.Label_Rykkertekst.Text = "Der ikke noget at rykkere";
                this.Label_Rykkertekst.Visible = true;
            }
            else
            {
                tempRykkerforslag rec_tempRykkerforslag = new tempRykkerforslag
                {
                    betalingsdato = clsOverfoersel.bankdageplus(DateTime.Today, 5),
                    bsh = this.DelsystemBSH.Checked
                };
                Program.dbData3060.tempRykkerforslags.InsertOnSubmit(rec_tempRykkerforslag);
                var i = 0;
                foreach (ListViewItem lvi in lvwRykker.Items)
                {
                    this.pgmRykker.Value = ++i;
                    keyval = lvi.Name;
                    advisbelob = double.Parse(lvi.SubItems[5].Text);
                    faknr = int.Parse(lvi.SubItems[6].Text);

                    tempRykkerforslaglinie rec_tempRykkerforslaglinie = new tempRykkerforslaglinie
                    {
                        Nr = int.Parse(keyval),
                        advisbelob = (decimal)advisbelob,
                        faknr = faknr
                    };
                    rec_tempRykkerforslag.tempRykkerforslaglinies.Add(rec_tempRykkerforslaglinie);
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
                        clsSFTP objSFTP = new clsSFTP();
                        objSFTP.WriteTilSFtp(m_lobnr);
                        objSFTP.DisconnectSFtp();
                        objSFTP = null;
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
                    var rec_tilpbs = (from t in Program.dbData3060.tbltilpbs where t.id == m_lobnr select t).First();
                    TilPBSFilename = "PBS" + rec_tilpbs.leverancespecifikation + ".lst";
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
