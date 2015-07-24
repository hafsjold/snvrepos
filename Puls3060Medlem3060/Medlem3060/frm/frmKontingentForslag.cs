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
            //getKontingentForslag();
            getRSMembership_KontingentForslag();
        }

        private void getKontingentForslag()
        {
            DateTime KontingentFradato = DateTime.MinValue;
            DateTime KontingentTildato = DateTime.MinValue;
            bool tilmeldtpbs = false;
            bool indmeldelse = false;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            int ikontingent;

            var qry_medlemmer = from h in Program.dbData3060.tblMedlems
                                select new clsMedlemInternAll
                                {
                                    Nr = h.Nr,
                                    Navn = h.Navn,
                                    Kaldenavn = h.Kaldenavn,
                                    Adresse = h.Adresse,
                                    Postnr = h.Postnr,
                                    Bynavn = h.Bynavn,
                                    Telefon = h.Telefon,
                                    Email = h.Email,
                                    Kon = h.Kon.ToString(),
                                    FodtDato = h.FodtDato,
                                    Bank = h.Bank,
                                    erMedlem = ((bool)Program.dbData3060.erMedlem(h.Nr)) ? 1 : 0,
                                    indmeldelsesDato = Program.dbData3060.indmeldtdato(h.Nr),
                                    udmeldelsesDato = Program.dbData3060.udmeldtdato(h.Nr),
                                    kontingentBetaltTilDato = Program.dbData3060.kontingentdato(h.Nr),
                                    opkrævningsDato = Program.dbData3060.forfaldsdato(h.Nr),
                                    kontingentTilbageførtDato = Program.dbData3060.tilbageførtkontingentdato(h.Nr),
                                    erMedlemPusterummet = ((bool)Program.dbData3060.MedlemPusterummet(h.Nr)) ? 1 : 0,
                                };


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
                tilmeldtpbs = false;
                indmeldelse = false;

                if (m.erMedlem == 0) //er ikke medlem
                {
                    bSelected = false;
                }
                else if ((m.udmeldelsesDato != null) && (m.udmeldelsesDato < DatoBetaltKontingentTil.Value))
                {
                    bSelected = false;
                }
                else if (m.erMedlemPusterummet == 1)
                {
                    bSelected = false;
                }
                else //Er medlem
                {
                    if ((m.kontingentBetaltTilDato != null) && (m.kontingentBetaltTilDato > m.indmeldelsesDato))  //'Der findes en kontingent-betaling
                    {
                        if (m.kontingentBetaltTilDato.Value.Date > this.DatoBetaltKontingentTil.Value)   //der er betalt kontingent efter DatoBetaltKontingentTil
                        {
                            bSelected = false;
                        }
                        else
                        {
                            if (m.kontingentBetaltTilDato.Value.Date >= m.indmeldelsesDato)
                            {
                                KontingentFradato = ((DateTime)m.kontingentBetaltTilDato.Value.Date).AddDays(1);
                            }
                        }
                    }
                    else  //Der findes ingen kontingent-betaling
                    {
                        KontingentFradato = (DateTime)m.indmeldelsesDato;
                        indmeldelse = true;
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
                    tilmeldtpbs = (bool)Program.dbData3060.erPBS(m.Nr);
                    clsKontingent objKontingent = new clsKontingent(Program.dbData3060, KontingentFradato, m.Nr);
                    KontingentTildato = objKontingent.KontingentTildato;
                    ikontingent = (int)objKontingent.Kontingent;

                    ListViewItem it = lvwKontingent.Items.Add(m.Nr.ToString(), m.Navn, 0);
                    //it.Tag = m;
                    it.SubItems.Add(m.Nr.ToString());
                    it.SubItems.Add(m.Adresse);
                    it.SubItems.Add(m.Postnr);
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentFradato));
                    it.SubItems.Add(ikontingent.ToString());
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentTildato));
                    it.SubItems.Add((indmeldelse) ? "J" : "N");
                    it.SubItems.Add((tilmeldtpbs) ? "J" : "N");
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
        private void getRSMembership_KontingentForslag()
        {
            puls3060_dkEntities jdb = new puls3060_dkEntities();
            DateTime KontingentFradato = DateTime.MinValue;
            DateTime KontingentTildato = DateTime.MinValue;
            bool tilmeldtpbs = false;
            bool indmeldelse = false;
            int AntalMedlemmer = 0;
            int AntalForslag = 0;
            int ikontingent;
            int iNr = 0;

            var qry_rsmembership = from s in jdb.ecpwt_rsmembership_membership_subscribers
                                   where s.membership_id == 6
                                   join tf in jdb.ecpwt_rsmembership_transactions on s.from_transaction_id equals tf.id
                                   join tl in jdb.ecpwt_rsmembership_transactions on s.last_transaction_id equals tl.id
                                   join m in jdb.ecpwt_rsmembership_subscribers on s.user_id equals m.user_id
                                   join u in jdb.ecpwt_users on s.user_id equals u.id
                                   select new 
                                   {
                                        Nr = m.f14,
                                        Navn = u.name,
                                        Adresse = m.f1,
                                        Postnr = m.f4,
                                        indmeldelsesDato = tf.date,
                                        kontingentBetaltTilDato = s.membership_end,
                                        Kontingent = tl.price,
                                        s.user_id,
                                        tl.user_data
                                   };


            this.lvwMedlem.Items.Clear();
            this.lvwKontingent.Items.Clear();

            var antal = qry_rsmembership.Count();
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

            var rsm = qry_rsmembership.ToArray();
            foreach (var m in rsm)
            {
                bool bSelected = true;
                AntalMedlemmer++;
                tilmeldtpbs = false;
                indmeldelse = false;

                if (m.Nr == "")
                    iNr = 10000 + m.user_id;
                else
                    iNr = int.Parse(m.Nr);
            
                bool erMedlemPusterummet = ((from um in jdb.ecpwt_user_usergroup_map
                                         join g in jdb.ecpwt_usergroups on um.group_id equals g.id
                                         where g.title == "Pusterummet" && um.user_id == m.user_id
                                         select um.user_id).Count() > 0);

                if (erMedlemPusterummet)
                {
                    bSelected = false;
                }
                else //Er medlem
                {
                    if ((m.kontingentBetaltTilDato != null) && (m.kontingentBetaltTilDato > m.indmeldelsesDato))  //'Der findes en kontingent-betaling
                    {
                        if (m.kontingentBetaltTilDato > this.DatoBetaltKontingentTil.Value)   //der er betalt kontingent efter DatoBetaltKontingentTil
                        {
                            bSelected = false;
                        }
                        else
                        {
                            if (m.kontingentBetaltTilDato >= m.indmeldelsesDato)
                            {
                                KontingentFradato = ((DateTime)m.kontingentBetaltTilDato);
                            }
                        }
                    }
                    else  //Der findes ingen kontingent-betaling
                    {
                        KontingentFradato = (DateTime)m.indmeldelsesDato;
                        indmeldelse = true;
                    }
                }

 
                if (bSelected)
                {
                    DateTime TodayMinus90 = DateTime.Now.AddDays(-90);
                    var qry_fak = from f in Program.dbData3060.tblfaks where f.Nr == iNr && f.betalingsdato > TodayMinus90
                                  join t in Program.dbData3060.tblrsmembership_transactions on f.id equals t.id
                                  select f;

                    if (qry_fak.Count() > 0) //Der findes en opkrævning
                    {
                             bSelected = false;
                    }
                }


                if (bSelected)
                {
                    AntalForslag++;
                    tilmeldtpbs = (bool)Program.dbData3060.erPBS(iNr);
                    KontingentTildato = KontingentFradato.AddMonths(12);
                    ikontingent = (int)m.Kontingent;

                    ListViewItem it = lvwKontingent.Items.Add(m.user_id.ToString(), m.Navn, 0);
                    //it.Tag = m;
                    it.SubItems.Add(m.user_id.ToString());
                    it.SubItems.Add(m.Adresse);
                    it.SubItems.Add(m.Postnr);
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentFradato));
                    it.SubItems.Add(ikontingent.ToString());
                    it.SubItems.Add(string.Format("{0:dd-MM-yyy}", KontingentTildato));
                    it.SubItems.Add((indmeldelse) ? "J" : "N");
                    it.SubItems.Add((tilmeldtpbs) ? "J" : "N");
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
            int lobnr;
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
            Program.dbData3060.tempKontforslags.DeleteAllOnSubmit(Program.dbData3060.tempKontforslags);
            Program.dbData3060.SubmitChanges();
            if ((imax == 0))
            {
                this.Label_Fakturatekst.Text = "Der ikke noget at fakturere";
                this.Label_Fakturatekst.Visible = true;
            }
            else
            {
                nsPbs3060.tempKontforslag rec_tempKontforslag = new nsPbs3060.tempKontforslag
                {
                    betalingsdato = clsOverfoersel.bankdageplus(this.DatoKontingentForfald.Value, 0),
                    bsh = this.DelsystemBSH.Checked
                };
                Program.dbData3060.tempKontforslags.InsertOnSubmit(rec_tempKontforslag);
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

                    nsPbs3060.tempKontforslaglinie rec_tempKontforslaglinie = new nsPbs3060.tempKontforslaglinie
                    {
                        Nr = int.Parse(keyval),
                        advisbelob = (decimal)advisbelob,
                        fradato = fradato,
                        tildato = tildato,
                        indmeldelse = indmeldelse,
                        tilmeldtpbs = tilmeldtpbs,
                    };
                    rec_tempKontforslag.tempKontforslaglinies.Add(rec_tempKontforslaglinie);
                }
                Program.dbData3060.SubmitChanges();

                clsPbs601 objPbs601 = new clsPbs601();

                Tuple<int, int> tresult = objPbs601.kontingent_fakturer_bs1(Program.dbData3060);
                AntalFakturaer = tresult.Item1;
                lobnr = tresult.Item2;
                this.pgmFaktura.Value = imax * 2;
                if ((AntalFakturaer > 0))
                {
                    objPbs601.faktura_og_rykker_601_action(Program.dbData3060, lobnr, fakType.fdfaktura);
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
                puls3060_dkEntities jdb = new puls3060_dkEntities();
                Memkontingentforslag memKontingentforslag = new Memkontingentforslag();
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

                    recKontingentforslag rec_Kontingentforslag = new recKontingentforslag
                    {
                        betalingsdato = clsOverfoersel.bankdageplus(this.DatoKontingentForfald.Value, 0),
                        bsh = this.DelsystemBSH.Checked,
                        user_id = int.Parse(keyval),
                        membership_id = 6,
                        advisbelob = (decimal)advisbelob,
                        fradato = fradato,
                        tildato = tildato,
                        indmeldelse = indmeldelse,
                        tilmeldtpbs = tilmeldtpbs,
                    };
                    memKontingentforslag.Add(rec_Kontingentforslag);
                }

                clsPbs601 objPbs601 = new clsPbs601();

                Tuple<int, int> tresult = objPbs601.rsmembeshhip_kontingent_fakturer_bs1(Program.dbData3060, jdb,memKontingentforslag);
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
