using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using MimeKit;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Uniconta.API.GeneralLedger;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;
using Uniconta.DataModel;

namespace nsPbs3060
{
    public class clsUnicontaHelp
    {
        public void GetEmailBilag(CrudAPI api)
        {
            MimeMessage message;

            using (var imap_client = new ImapClient())
            {
                imap_client.Connect("imap.gigahost.dk", 993, true);
                imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                imap_client.Authenticate("regnskab@puls3060.dk", "1234West+");
                var Puls3060Bilag = imap_client.GetFolder("_Puls3060BilagArkiv");
                Puls3060Bilag.Open(FolderAccess.ReadOnly);

                var results = Puls3060Bilag.Search(SearchQuery.All);
                foreach (var result in results)
                {
                    message = Puls3060Bilag.GetMessage(result);

                    var msMail = Message2Pdf(message);

                    List<VouchersClient> documents = new List<VouchersClient>();
                    VouchersClient mail = new VouchersClient()
                    {
                        Fileextension = FileextensionsTypes.PDF,
                        Text = "e-Mail",
                        VoucherAttachment = msMail.ToArray(),
                        DocumentDate = DateTime.Now,
                    };
                    var task1 = api.Insert(mail);
                    task1.Wait();
                    var res1 = task1.Result;
                    documents.Add(mail);

                    foreach (var msg_attachment in message.Attachments)
                    {
                        if (!(msg_attachment is MessagePart))
                        {

                            FileextensionsTypes type = FileextensionsTypes.PDF;
                            switch (msg_attachment.ContentType.MediaSubtype.ToUpper())
                            {
                                case "PDF":
                                    type = FileextensionsTypes.PDF;
                                    break;

                                case "JPEG":
                                    type = FileextensionsTypes.JPEG;
                                    break;

                                case "TXT":
                                    type = FileextensionsTypes.TXT;
                                    break;

                                case "PLAIN":
                                    type = FileextensionsTypes.TXT;
                                    break;

                                case "MSWORD":
                                    type = FileextensionsTypes.DOC;
                                    break;

                                case "VND.OPENXMLFORMATS-OFFICEDOCUMENT.SPREADSHEETML.SHEET":
                                    type = FileextensionsTypes.XLSX;
                                    break;

                                default:
                                    type = FileextensionsTypes.UNK;
                                    break;
                            }
                            var part = (MimePart)msg_attachment;
                            MemoryStream msstream = new MemoryStream();
                            part.ContentObject.DecodeTo(msstream);
                            VouchersClient attm = new VouchersClient()
                            {
                                Fileextension = type,
                                Text = (msg_attachment as MimePart).FileName,
                                VoucherAttachment = msstream.ToArray(),
                                DocumentDate = DateTime.Now,
                            };
                            var task3 = api.Insert(attm);
                            task3.Wait();
                            var res3 = task3.Result;
                            documents.Add(attm);
                        }
                    }
                    if (documents.Count > 0)
                    {

                        VouchersClient folder = new VouchersClient()
                        {
                            _Fileextension = FileextensionsTypes.DIR,
                            _Text = message.Subject,
                            _DocumentDate = DateTime.Now,

                        };
                        var ref3 = folder.PrimaryKeyId;
                        var task4 = api.Insert(folder);
                        task4.Wait();
                        var res4 = task4.Result;
                        var ref1 = folder.PrimaryKeyId;

                        DocumentAPI docapi = new DocumentAPI(api);
                        var task5 = docapi.CreateFolder(folder, documents);
                        task5.Wait();
                        var res5 = task5.Result;
                        var ref2 = folder.PrimaryKeyId;

                        if (ref1 != ref2) //Delete ref1
                        {
                            var crit = new List<PropValuePair>();
                            var pair = PropValuePair.GenereteWhereElements("PrimaryKeyId", typeof(int), ref1.ToString());
                            crit.Add(pair);
                            var task6 = api.Query<VouchersClient>(null, crit);
                            task6.Wait();
                            var col = task6.Result;
                            if (col.Count() == 1)
                            {
                                var rec = col[0];
                                api.DeleteNoResponse(rec);
                            }
                        }
                    }

                }

                Puls3060Bilag.Close();
                imap_client.Disconnect(true);
            }
            return;// message;
        }

        public MemoryStream Message2Pdf(MimeMessage message)
        {
            MemoryStream msMail = new MemoryStream();
            string html = message.GetTextBody(MimeKit.Text.TextFormat.Html);
            string txt = message.GetTextBody(MimeKit.Text.TextFormat.Text);
            PdfDocument pdfMail = null;
            if (html != null)
            {
                pdfMail = PdfGenerator.GeneratePdf(html, PageSize.A4);
            }
            else
            {
                pdfMail = PdfGenerator.GeneratePdf(txt, PageSize.A4);
            }
            pdfMail.Save(msMail, false);

            XPdfForm form = XPdfForm.FromStream(msMail);
            PdfDocument outputDocument = new PdfDocument();
            XGraphics gfx;

            int count = form.PageCount;
            for (int idx = 0; idx < count; idx++)
            {
                PdfPage page = outputDocument.AddPage();
                if (form.PageCount > idx)
                {
                    // Get a graphics object for page
                    gfx = XGraphics.FromPdfPage(page);
                    if (idx == 0)
                    {
                        DrawPageHeading(page, gfx, message);
                        // Draw the page like an image
                        gfx.DrawImage(form, new XRect(form.PointWidth * 0.02, form.PointHeight * 0.10, form.PointWidth * 0.90, form.PointHeight * 0.90));
                    }
                    else
                    {
                        gfx.DrawImage(form, new XRect(form.PointWidth, form.PointHeight, form.PointWidth, form.PointHeight));
                    }
                }

            }
            msMail = new MemoryStream();
            outputDocument.Save(msMail, false);
            return msMail;
        }

        void DrawPageHeading(PdfPage page, XGraphics gfx, MimeMessage message)
        {
            var From = message.From.ToString();
            From = Regex.Replace(From, @"""", "");
            var ResentDate = message.ResentDate.DateTime.ToString();
            var Date = message.Date.DateTime.ToString();
            var To = message.To.ToString();
            To = Regex.Replace(To, @"""", "");
            var Subject = message.Subject;
            string filer = "";
            foreach (var msg_attachment in message.Attachments)
            {
                if ((msg_attachment is MimePart))
                {
                    var part = msg_attachment as MimePart;
                    if (filer.Length > 0)
                        filer += "," + part.FileName;
                    else
                        filer += part.FileName;
                }
            }

            // HACK²
            gfx.MUH = PdfFontEncoding.Unicode;
            gfx.MFEH = PdfFontEmbedding.Default;

            // You always need a MigraDoc document for rendering.
            MigraDoc.DocumentObjectModel.Document doc = new MigraDoc.DocumentObjectModel.Document();

            // Create a new style called Table based on style Normal
            var style = doc.Styles.AddStyle("Table", "Normal");
            style.Font.Name = "Verdana";
            style.Font.Name = "Arial";
            style.Font.Size = 9;

            Section sec = doc.AddSection();
            var table = sec.AddTable();
            table.Style = "Table";

            // Before you can add a row, you must define the columns 
            Column column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column = table.AddColumn("17cm");
            column.Format.Alignment = ParagraphAlignment.Left;

            Row row = table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Fra:");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].Borders.Visible = false;
            row.Cells[1].AddParagraph(From);
            row.Cells[1].Format.Font.Bold = false;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Format.SpaceAfter = 2;

            row = table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Sendt: ");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].Borders.Visible = false;
            row.Cells[1].AddParagraph(Date);
            row.Cells[1].Format.Font.Bold = false;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Format.SpaceAfter = 2;

            row = table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Til:");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].Borders.Visible = false;
            row.Cells[1].AddParagraph(To);
            row.Cells[1].Format.Font.Bold = false;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Format.SpaceAfter = 2;

            row = table.AddRow();
            row.Cells[0].Borders.Visible = false;
            row.Cells[0].AddParagraph("Emne:");
            row.Cells[0].Format.Font.Bold = true;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].Borders.Visible = false;
            row.Cells[1].AddParagraph(Subject);
            row.Cells[1].Format.Font.Bold = false;
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;

            if (filer.Length > 0)
            {
                row.Format.SpaceAfter = 2;
                row = table.AddRow();
                row.Cells[0].Borders.Visible = false;
                row.Cells[0].AddParagraph("Vedhæftede filer:");
                row.Cells[0].Format.Font.Bold = true;
                row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                row.Cells[1].Borders.Visible = false;
                row.Cells[1].AddParagraph(filer);
                row.Cells[1].Format.Font.Bold = false;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            }

            // Create a renderer and prepare (=layout) the document
            MigraDoc.Rendering.DocumentRenderer docRenderer = new DocumentRenderer(doc);
            docRenderer.PrepareDocument();

            var box = page.MediaBox.ToXRect();
            box.Inflate(-20, -25);
            // Render the paragraph. You can render tables or shapes the same way.
            docRenderer.RenderObject(gfx, box.Location.X, box.Location.X, box.Width, table);
        }




        public static byte[] AppendImageToPdf(byte[] pdf, byte[] img, Point position, double scale)
        {
            using (System.IO.MemoryStream msPdf = new System.IO.MemoryStream(pdf))
            {
                using (System.IO.MemoryStream msImg = new System.IO.MemoryStream(img))
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(msImg);
                    PdfSharp.Pdf.PdfDocument document = PdfSharp.Pdf.IO.PdfReader.Open(msPdf);
                    PdfSharp.Pdf.PdfPage page = document.Pages[0];
                    PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                    PdfSharp.Drawing.XImage ximg = PdfSharp.Drawing.XImage.FromGdiPlusImage(image);

                    gfx.DrawImage(
                        ximg,
                        position.X,
                        position.Y,
                        ximg.Width * scale,
                        ximg.Height * scale
                    );

                    using (System.IO.MemoryStream msFinal = new System.IO.MemoryStream())
                    {
                        document.Save(msFinal);
                        return msFinal.ToArray();
                    }

                }
            }
        }

        public void TestGetEmailBilag()
        {
            MimeMessage message;

            using (var imap_client = new ImapClient())
            {
                imap_client.Connect("imap.gigahost.dk", 993, true);
                imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                imap_client.Authenticate("regnskab@puls3060.dk", "1234West+");
                var Puls3060Bilag = imap_client.GetFolder("_Puls3060Bilag");
                Puls3060Bilag.Open(FolderAccess.ReadOnly);

                var results = Puls3060Bilag.Search(SearchQuery.All);
                foreach (var result in results)
                {
                    var xx = result.Id;
                }
                if (results.Count > 0)
                    message = Puls3060Bilag.GetMessage(results.First());
                else
                    message = null;

                if (message == null) { Console.WriteLine("Not found"); }

                string html = message.GetTextBody(MimeKit.Text.TextFormat.Html);

                MemoryStream s3 = new MemoryStream();

                foreach (var attachment in message.Attachments)
                {
                    if (!(attachment is MessagePart))
                    {
                        if (attachment.ContentType.MediaSubtype == "pdf")
                        {
                            var part = (MimePart)attachment;
                            part.ContentObject.DecodeTo(s3);
                            break;
                        }
                    }
                }

                PdfDocument one = PdfGenerator.GeneratePdf(html, PageSize.A4);
                PdfDocument two = PdfGenerator.GeneratePdf(html, PageSize.A4);
                MemoryStream s1 = new MemoryStream();
                MemoryStream s2 = new MemoryStream();
                one.Save(s1, false);
                two.Save(s2, false);

                using (PdfDocument onex = PdfReader.Open(s1, PdfDocumentOpenMode.Import))
                using (PdfDocument twox = PdfReader.Open(s2, PdfDocumentOpenMode.Import))
                using (PdfDocument treex = PdfReader.Open(s3, PdfDocumentOpenMode.Import))
                using (PdfDocument outPdf = new PdfDocument())
                {
                    CopyPages(onex, outPdf);
                    CopyPages(twox, outPdf);
                    CopyPages(treex, outPdf);

                    outPdf.Save("file1and3.pdf");
                }
                Puls3060Bilag.Close();
                imap_client.Disconnect(true);
            }
            return;// message;
        }

        void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }
    }
}
