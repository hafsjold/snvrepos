using MailKit;
using MailKit.Net.Imap;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace nsPbs3060
{
    public class clsPDFMedlem
    {
        Document m_document;
        string m_ddl;
        PdfDocumentRenderer m_renderer;

        public clsPDFMedlem()
        {
            CreateDocument();
            m_ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(m_document);
            m_renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            m_renderer.Document = m_document;
            m_renderer.RenderDocument();
        }

        public void Save()
        {
            // Save the document...
            string filename = "MedlemPuls3060.pdf";
            m_renderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
        public System.IO.Stream SaveStream()
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            m_renderer.PdfDocument.Save(stream,false);
            return stream;
        }

        private void CreateDocument()
        {
            // Create a new MigraDoc document
            m_document = new Document();
            m_document.Info.Title = "Puls 3060 Medlemsliste";
            m_document.Info.Author = "Mogens Hafsjold";

            Style style = m_document.Styles["Normal"];
            style.Font.Name = "Calibri";
            style.Font.Size = 8;
            
            Section section = m_document.AddSection();
            section.PageSetup.Orientation = Orientation.Landscape;
            section.PageSetup.TopMargin = Unit.FromMillimeter(10);
            section.PageSetup.BottomMargin = Unit.FromMillimeter(10);
            section.PageSetup.LeftMargin = Unit.FromMillimeter(10);
            section.PageSetup.RightMargin = Unit.FromMillimeter(10);

            MedlemTable();
        }

        private void MedlemTable()
        {
            puls3060_nyEntities jdb = new puls3060_nyEntities(true);

            var qry = from u in jdb.ecpwt_users
                      join m in jdb.ecpwt_rsmembership_membership_subscribers on u.id equals m.user_id
                      where m.membership_id == 6 && m.status == 0
                      join t in jdb.ecpwt_rsmembership_transactions on m.last_transaction_id equals t.id
                      join a in jdb.ecpwt_rsmembership_subscribers on m.user_id equals a.user_id
                      orderby u.name
                      select new
                      {
                          u.id,
                          u.name,
                          a.f1,
                          a.f4,
                          a.f2,
                          a.f6,
                          u.email,
                          a.f14,
                          m.membership_start,
                          m.membership_end,
                          t.user_data
                      };
            List<clsMedlemPDF> MedlemmerAll = new List<clsMedlemPDF>();
            foreach (var m in qry)
            {

                User_data recud = clsHelper.unpack_UserData(m.user_data);
                clsMedlemPDF recMedlem = new clsMedlemPDF
                {
                    Nr = m.id,
                    Navn = m.name,
                    Adresse = m.f1,
                    Postnr = m.f4,
                    Bynavn = m.f2,
                    Telefon = m.f6,
                    Email = m.email,
                    Kon = recud.kon,
                    FodtAar = recud.fodtaar,
                    MedlemTil = m.membership_end
                };
                MedlemmerAll.Add(recMedlem);
            }

            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromMillimeter(10));
            column.Format.Alignment = ParagraphAlignment.Right;
            table.AddColumn(Unit.FromMillimeter(50));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(50));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(10));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(25));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(50));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(15));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(11));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(8));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(20));
            column.Format.Alignment = ParagraphAlignment.Left;

            Row row = table.AddRow();
            row.Shading.Color = Colors.LightBlue;
            Cell cell = row.Cells[0];
            cell.AddParagraph("Nr");
            cell = row.Cells[1];
            cell.AddParagraph("Navn");
            cell = row.Cells[2];
            cell.AddParagraph("Adresse");
            cell = row.Cells[3];
            cell.AddParagraph("Postnr");
            cell = row.Cells[4];
            cell.AddParagraph("By");
            cell = row.Cells[5];
            cell.AddParagraph("Email");
            cell = row.Cells[6];
            cell.AddParagraph("Telefon");
            cell = row.Cells[7];
            cell.AddParagraph("Køn");
            cell = row.Cells[8];
            cell.AddParagraph("Født");
            cell = row.Cells[9];
            cell.AddParagraph("Medlem Til");


            foreach (clsMedlemPDF m in MedlemmerAll)
            {
                row = table.AddRow();
                cell = row.Cells[0];
                cell.AddParagraph(m.Nr.ToString());
                cell = row.Cells[1];
                cell.AddParagraph(m.Navn);
                cell = row.Cells[2];
                cell.AddParagraph(m.Adresse);
                cell = row.Cells[3];
                cell.AddParagraph(m.Postnr);
                cell = row.Cells[4];
                cell.AddParagraph(m.Bynavn);
                cell = row.Cells[5];
                cell.AddParagraph(m.Email);
                cell = row.Cells[6];
                cell.AddParagraph(m.Telefon);
                cell = row.Cells[7];
                cell.AddParagraph(m.Kon);
                cell = row.Cells[8];
                cell.AddParagraph(m.FodtAar);
                cell = row.Cells[9];
                cell.AddParagraph(m.MedlemTil.ToString("yyyy-MM-dd"));
            }
            m_document.LastSection.Add(table);
        }

        public void imapSavePDFFile()
        {
            string PSubjectBody = "Puls 3060 Medlems Liste";
            string local_filename = "MedlemPuls3060.pdf";
            Stream fs = SaveStream();

            MimeMessage message = new MimeMessage();
            TextPart body;

            message.To.Add(new MailboxAddress(@"Regnskab Puls3060", @"regnskab@puls3060.dk"));
            message.From.Add(new MailboxAddress(@"Regnskab Puls3060", @"regnskab@puls3060.dk"));
            message.Subject = PSubjectBody + ": " + local_filename;
            body = new TextPart("plain") { Text = PSubjectBody + ": " + local_filename };


            var attachment = new MimePart("application", "pdf")
            {
                ContentObject = new ContentObject(fs, ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = local_filename
            };

            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;

            using (var client = new ImapClient())
            {
                client.Connect("imap.gigahost.dk", 993, true);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);

                var PBS = client.GetFolder("INBOX.PBS");
                PBS.Open(FolderAccess.ReadWrite);
                PBS.Append(message);
                PBS.Close();

                client.Disconnect(true);
            }

        }

    }
}
