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
    public class clsOverfor
    {
        public int? SFakID;
    }

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

            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "overforsel");
            XDocument xmldata = XDocument.Parse(strxmldata);
            string Status = xmldata.Descendants("Status").First().Value;
            if (Status != "True") return;
            var qry_overforsel = from h in xmldata.Descendants("Overforsel")
                                 select new clsOverfor
                                 {
                                     SFakID = clsPassXmlDoc.attr_val_int(h, "SFakID"),
                                 };

            var qry_Kreditor = from h in Program.karFakturaer_k
                               where h.saldo > 0
                               join m in Program.karMedlemmer on h.kreditornr.ToString() equals m.Krdktonr
                               where Program.ValidatekBank(m.Bank)
                               join f in qry_overforsel on h.fakid equals f.SFakID into overforsel
                               from f in overforsel.DefaultIfEmpty(new clsOverfor { SFakID = null })
                               where f.SFakID == null
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
                it.SubItems.Add((((decimal)m.saldo) / 100).ToString());
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
            Program.dbData3060.TempBetalforslag.DeleteAllOnSubmit(Program.dbData3060.TempBetalforslag);
            Program.dbData3060.SubmitChanges();
            if ((imax == 0))
            {
                this.Label_Betaltekst.Text = "Der ikke noget at betale";
                this.Label_Betaltekst.Visible = true;
            }
            else
            {
                XElement headxml = new XElement("TempBetalforslag");
                headxml.Add(new XElement("Betalingsdato", DateTime.Now));

                var i = 0;
                foreach (ListViewItem lvi in lvwKrdFaktura.Items)
                {
                    this.pgmBetal.Value = ++i;
                    keyval = lvi.Name;
                    Nr = int.Parse(lvi.SubItems[1].Text);
                    faknr = int.Parse(lvi.SubItems[4].Text);
                    advisbelob = decimal.Parse(lvi.SubItems[5].Text);
                    Bank = lvi.SubItems[6].Text;

                    XElement linxml = new XElement("TempBetalforslaglinie");
                    linxml.Add(new XElement("Nr", Nr));
                    linxml.Add(new XElement("Fakid", int.Parse(keyval)));
                    linxml.Add(new XElement("Advisbelob", (decimal)advisbelob));
                    linxml.Add(new XElement("Bankregnr", Bank.Substring(0, 4)));
                    linxml.Add(new XElement("Bankkontonr", Bank.Substring(5, 10)));
                    linxml.Add(new XElement("Faknr", faknr));
                    headxml.Add(new XElement(linxml));
                }
                clsRest objRest = new clsRest();
                string strheadxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + headxml.ToString();
                string result = objRest.HttpPost2(clsRest.urlBaseType.data, "overforsel", strheadxml);
                XDocument xmldata = XDocument.Parse(result);
                string Status = xmldata.Descendants("Status").First().Value;
                this.pgmBetal.Value = imax * 2;
                if (Status == "True")
                {
                    this.pgmBetal.Value = (imax * 3);
                    int lobnr = int.Parse(xmldata.Descendants("Lobnr").First().Value);
                    int antal = int.Parse(xmldata.Descendants("Antal").First().Value);
                    int sendqueueid = int.Parse(xmldata.Descendants("Sendqueueid").First().Value);

                    string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "tilpbs/" + sendqueueid.ToString());
                    XDocument xmldata2 = XDocument.Parse(strxmldata);
                    string Status2 = xmldata2.Descendants("Status").First().Value;
                    if (Status2 == "True")
                    {
                        clsSFTP objAppEngSFTP = new clsSFTP();
                        bool bSendt = objAppEngSFTP.WriteTilSFtp(xmldata2);
                        if (bSendt) overfoersel_mail(lobnr);
                        objAppEngSFTP.DisconnectSFtp();
                        objAppEngSFTP = null;
                    }
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

        public void overfoersel_mail(int lobnr)
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

            XElement headxml = new XElement("OverforselMail");
            headxml.Add(new XElement("Lobnr", lobnr));
            clsRest objRest = new clsRest();
            string strheadxml = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + headxml.ToString();
            string result = objRest.HttpPost2(clsRest.urlBaseType.data, "overforselmail", strheadxml);
            XDocument xmldata = XDocument.Parse(result);
            string Status = xmldata.Descendants("Status").First().Value;
            if (Status == "True")
            {
                var qry_email = from overforselemail in xmldata.Descendants("OverforselEmail")
                                select new
                                {
                                    Navn = clsPassXmlDoc.attr_val_string(overforselemail, "Navn"),
                                    Email = clsPassXmlDoc.attr_val_string(overforselemail, "Email"),
                                    Tekst = clsPassXmlDoc.attr_val_string(overforselemail, "Tekst"),
                                };
                foreach (var msg in qry_email)
                {
                    //  Create a new email object
                    Chilkat.Email email = new Chilkat.Email();

#if (DEBUG)
                email.Subject = "TEST Bankoverførsel fra Puls 3060: skal sendes til " + Program.MailToName + " " + Program.MailToAddr;
                email.AddTo(Program.MailToName, Program.MailToAddr);
#else
                    email.Subject = "Bankoverførsel fra Puls 3060";
                    if (msg.Email.Length > 0)
                    {
                        email.AddTo(msg.Navn, msg.Email);
                        email.AddBcc(Program.MailToName, Program.MailToAddr);
                    }
                    else
                    {
                        email.Subject += ": skal sendes til " + msg.Navn;
                        email.AddTo(Program.MailToName, Program.MailToAddr);
                    }
#endif
                    email.Body = msg.Tekst;
                    email.From = Program.MailFrom;
                    email.ReplyTo = Program.MailReply;

                    success = mailman.SendEmail(email);
                    if (success != true) throw new Exception(email.LastErrorText);
                }
            }
        }

    }
}
