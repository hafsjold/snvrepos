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
            string Status = "False";
            IEnumerable<clsqry_medlemmer> qry_medlemmer;
            if (this.RykketTidligere.Checked)
            {
                clsRest objRest = new clsRest();
                string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "tidligererykker");
                XDocument xmldata = XDocument.Parse(strxmldata);
                Status = xmldata.Descendants("Status").First().Value;
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
                Status = xmldata.Descendants("Status").First().Value;
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

            var antal = qry_medlemmer.Count();
            this.lvwMedlem.Items.Clear();
            this.lvwRykker.Items.Clear();
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

            if ((imax == 0))
            {
                this.Label_Rykkertekst.Text = "Der ikke noget at rykkere";
                this.Label_Rykkertekst.Visible = true;
            }
            else
            {
                XElement headxml = new XElement("TempRykkerforslag");
                headxml.Add(new XElement("Betalingsdato", clsUtil.bankdageplus(DateTime.Today, 5)));
                headxml.Add(new XElement("Bsh", this.DelsystemBSH.Checked));

                var i = 0;
                foreach (ListViewItem lvi in lvwRykker.Items)
                {
                    this.pgmRykker.Value = ++i;
                    keyval = lvi.Name;
                    advisbelob = double.Parse(lvi.SubItems[5].Text);
                    faknr = int.Parse(lvi.SubItems[6].Text);

                    XElement linxml = new XElement("TempRykkerforslaglinie");
                    linxml.Add(new XElement("Nr", int.Parse(keyval)));
                    linxml.Add(new XElement("Advisbelob", (decimal)advisbelob));
                    linxml.Add(new XElement("Faknr", faknr));
                    headxml.Add(new XElement(linxml));

                }
                clsRest objRest = new clsRest();
                string strheadxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + headxml.ToString();
                string strxmldata = objRest.HttpPost2(clsRest.urlBaseType.data, "pbs601", strheadxml);
                XDocument xmldata = XDocument.Parse(strxmldata);
                string Status = xmldata.Descendants("Status").First().Value;
                if (Status == "True")
                {
                    if (this.DelsystemBSH.Checked) //RYKKERE med Indbetalingskort
                    {
                        //objPbs601.faktura_og_rykker_601_action(m_lobnr, fakType.fdrykker);
                        //this.pgmRykker.Value = (imax * 3);
                        //clsSFTP objSFTP = new clsSFTP();
                        //objSFTP.WriteTilSFtp(m_lobnr);
                        //objSFTP.DisconnectSFtp();
                        //objSFTP = null;
                    }
                    else //RYKKERE som emails
                    {
                        var qry_email = from rykkeremail in xmldata.Descendants("RykkerEmail")
                                        select new
                                        {
                                            Navn = clsPassXmlDoc.attr_val_string(rykkeremail, "Navn"),
                                            Email = clsPassXmlDoc.attr_val_string(rykkeremail, "Email"),
                                            Emne = clsPassXmlDoc.attr_val_string(rykkeremail, "Emne"),
                                            Tekst = clsPassXmlDoc.attr_val_string(rykkeremail, "Tekst"),
                                        };
                        foreach (var rykkeremail in qry_email)
                        {
                            sendRykkerEmail(rykkeremail.Navn, rykkeremail.Email, rykkeremail.Emne, rykkeremail.Tekst);
                        }
                    }
                    this.pgmRykker.Value = (imax * 3);
                }
            }

            this.pgmRykker.Value = (imax * 4);
            cmdRykkere.Text = "Afslut";

            TilPBSFilename = "PBSNotFound.lst";
            this.Label_Rykkertekst.Text = ("Leverance til PBS er gemt i filen " + TilPBSFilename);
            this.Label_Rykkertekst.Visible = true;
            this.pgmRykker.Visible = false;
        }

        public void sendRykkerEmail(string ToName, string ToAddr, string subject, string body)
        {
            Chilkat.MailMan mailman = new Chilkat.MailMan();
            bool success;
            success = mailman.UnlockComponent("HAFSJOMAILQ_9QYSMgP0oR1h");
            if (success != true) throw new Exception(mailman.LastErrorText);

            //  Use the GMail SMTP server
            mailman.SmtpHost = Program.Smtphost;
            mailman.SmtpPort = int.Parse(Program.Smtpport);
            mailman.SmtpSsl = bool.Parse(Program.Smtpssl);

            //  Set the SMTP login/password.
            mailman.SmtpUsername = Program.Smtpuser;
            mailman.SmtpPassword = Program.Smtppasswd;

            //  Create a new email object
            Chilkat.Email email = new Chilkat.Email();


#if (DEBUG)
            email.AddTo(Program.MailToName, Program.MailToAddr);
            email.Subject = "TEST " + subject + " skal sendes til: " + ToName + " " + ToAddr;
#else
            email.AddTo(ToName, ToAddr);
            email.Subject = subject;
            email.AddCC("Claus Knudsen", "claus@puls3060.dk");
            email.AddBcc(Program.MailToName, Program.MailToAddr);
#endif
            email.Body = body;
            email.From = Program.MailFrom;
            email.ReplyTo = Program.MailReply;

            success = mailman.SendEmail(email);
            if (success != true) throw new Exception(email.LastErrorText);

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
