using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace nsPbs3060
{

    public class FillerText
    {
        public static string Text { get; set;}
        public static string MediumText { get; set; }
        public static string ShortText { get; set; }    
    }

    public class clsMedlemPDF
    {
        public int? Nr { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Kon { get; set; }
        public string FodtAar { get; set; }
        public DateTime MedlemTil { get; set; }
    }

    public class clsPDFTest02
    {
        public static void Main()
        {
            FillerText.Text = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
Officia do fugiat ipsum est elit, do proident, aliqua. ullamco ea ex cupidatat aute aute Lorem dolor sit";
            FillerText.MediumText = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor";
            FillerText.ShortText = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad";

            // Create a MigraDoc document
            Document document = CreateDocument();
 
            //string ddl = MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToString(document);
            MigraDoc.DocumentObjectModel.IO.DdlWriter.WriteToFile(document, "MigraDoc.mdddl");

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;

            renderer.RenderDocument();

            // Save the document...
            string filename = "HelloMigraDoc.pdf";
            renderer.PdfDocument.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }

        public static Document CreateDocument()
        {
            // Create a new MigraDoc document
            Document document = new Document();
            document.Info.Title = "Hello, MigraDoc";
            document.Info.Subject = "Demonstrates an excerpt of the capabilities of MigraDoc.";
            document.Info.Author = "Stefan Lange";

            DefineStyles(document);

            //DefineCover(document);
            //DefineTableOfContents(document);

            DefineContentSection(document);

            //DefineParagraphs(document);
            DefineTables(document);
            //DefineCharts(document);

            return document;
        }

        /// <SUMMARY>
        /// Defines the styles used in the document.
        /// </SUMMARY>
        public static void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            //style.Font.Name = "Times New Roman";
            style.Font.Name = "Calibri";
            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.
            style.Font.Size = 8;


            style = document.Styles["Heading1"];
            style.Font.Name = "Tahoma";
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Color = Colors.DarkBlue;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles["Heading3"];
            style.Font.Size = 10;
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceBefore = 6;
            style.ParagraphFormat.SpaceAfter = 3;
            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called TextBox based on style Normal
            style = document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            // Create a new style called TOC based on style Normal
            style = document.Styles.AddStyle("TOC", "Normal");
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Blue;
        }

        /// <SUMMARY>
        /// Defines the cover page.
        /// </SUMMARY>
        public static void DefineCover(Document document)
        {
            Section section = document.AddSection();

            Paragraph paragraph = section.AddParagraph();
            paragraph.Format.SpaceAfter = "3cm";

            //Image image = section.AddImage("../../images/Logo landscape.png");
            //image.Width = "10cm";

            paragraph = section.AddParagraph("A sample document that demonstrates the\ncapabilities of MigraDoc");
            paragraph.Format.Font.Size = 16;
            paragraph.Format.Font.Color = Colors.DarkRed;
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Format.SpaceAfter = "3cm";

            paragraph = section.AddParagraph("Rendering date: ");
            paragraph.AddDateField();
        }

        /// <SUMMARY>
        /// Defines the cover page.
        /// </SUMMARY>
        public static void DefineTableOfContents(Document document)
        {
            Section section = document.LastSection;

            section.AddPageBreak();
            Paragraph paragraph = section.AddParagraph("Table of Contents");
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Bold = true;
            paragraph.Format.SpaceAfter = 24;
            paragraph.Format.OutlineLevel = OutlineLevel.Level1;

            paragraph = section.AddParagraph();
            paragraph.Style = "TOC";
            Hyperlink hyperlink = paragraph.AddHyperlink("Paragraphs");
            hyperlink.AddText("Paragraphs\t");
            hyperlink.AddPageRefField("Paragraphs");

            paragraph = section.AddParagraph();
            paragraph.Style = "TOC";
            hyperlink = paragraph.AddHyperlink("Tables");
            hyperlink.AddText("Tables\t");
            hyperlink.AddPageRefField("Tables");

            paragraph = section.AddParagraph();
            paragraph.Style = "TOC";
            hyperlink = paragraph.AddHyperlink("Charts");
            hyperlink.AddText("Charts\t");
            hyperlink.AddPageRefField("Charts");
        }

        /// <SUMMARY>
        /// Defines page setup, headers, and footers.
        /// </SUMMARY>
        static void DefineContentSection(Document document)
        {
            Section section = document.AddSection();
            section.PageSetup.OddAndEvenPagesHeaderFooter = true;
            section.PageSetup.StartingNumber = 1;

            section.PageSetup.LeftMargin = Unit.FromMillimeter(15);

            HeaderFooter header = section.Headers.Primary;
            header.AddParagraph("\tOdd Page Header");

            header = section.Headers.EvenPage;
            header.AddParagraph("Even Page Header");

            // Create a paragraph with centered page number. See definition of style "Footer".
            Paragraph paragraph = new Paragraph();
            paragraph.AddTab();
            paragraph.AddPageField();

            // Add paragraph to footer for odd pages.
            section.Footers.Primary.Add(paragraph);
            // Add clone of paragraph to footer for odd pages. Cloning is necessary because an object must
            // not belong to more than one other object. If you forget cloning an exception is thrown.
            section.Footers.EvenPage.Add(paragraph.Clone());
        }

        public static void DefineParagraphs(Document document)
        {
            Paragraph paragraph = document.LastSection.AddParagraph("Paragraph Layout Overview", "Heading1");
            paragraph.AddBookmark("Paragraphs");

            DemonstrateAlignment_1(document);
            DemonstrateIndent(document);
            DemonstrateFormattedText(document);
            DemonstrateBordersAndShading(document);
        }

        static void DemonstrateAlignment_1(Document document)
        {
            document.LastSection.AddParagraph("Alignment", "Heading2");

            document.LastSection.AddParagraph("Left Aligned", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            paragraph.AddText(FillerText.Text);

            document.LastSection.AddParagraph("Right Aligned", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddText(FillerText.Text);

            document.LastSection.AddParagraph("Centered", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.AddText(FillerText.Text);

            document.LastSection.AddParagraph("Justified", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Alignment = ParagraphAlignment.Justify;
            paragraph.AddText(FillerText.MediumText);
        }

        static void DemonstrateIndent(Document document)
        {
            document.LastSection.AddParagraph("Indent", "Heading2");

            document.LastSection.AddParagraph("Left Indent", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.Format.LeftIndent = "2cm";
            paragraph.AddText(FillerText.Text);

            document.LastSection.AddParagraph("Right Indent", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.RightIndent = "1in";
            paragraph.AddText(FillerText.Text);

            document.LastSection.AddParagraph("First Line Indent", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.FirstLineIndent = "12mm";
            paragraph.AddText(FillerText.Text);

            document.LastSection.AddParagraph("First Line Negative Indent", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.LeftIndent = "1.5cm";
            paragraph.Format.FirstLineIndent = "-1.5cm";
            paragraph.AddText(FillerText.Text);
        }

        static void DemonstrateFormattedText(Document document)
        {
            document.LastSection.AddParagraph("Formatted Text", "Heading2");

            //document.LastSection.AddParagraph("Left Aligned", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.AddText("Text can be formatted ");
            paragraph.AddFormattedText("bold", TextFormat.Bold);
            paragraph.AddText(", ");
            paragraph.AddFormattedText("italic", TextFormat.Italic);
            paragraph.AddText(", or ");
            paragraph.AddFormattedText("bold & italic", TextFormat.Bold | TextFormat.Italic);
            paragraph.AddText(".");
            paragraph.AddLineBreak();
            paragraph.AddText("You can set the ");
            FormattedText formattedText = paragraph.AddFormattedText("size ");
            formattedText.Size = 15;
            paragraph.AddText("the ");
            formattedText = paragraph.AddFormattedText("color ");
            formattedText.Color = Colors.Firebrick;
            paragraph.AddText("the ");
            formattedText = paragraph.AddFormattedText("font", new MigraDoc.DocumentObjectModel.Font("Verdana"));
            paragraph.AddText(".");
            paragraph.AddLineBreak();
            paragraph.AddText("You can set the ");
            formattedText = paragraph.AddFormattedText("subscript");
            formattedText.Subscript = true;
            paragraph.AddText(" or ");
            formattedText = paragraph.AddFormattedText("superscript");
            formattedText.Superscript = true;
            paragraph.AddText(".");
        }

        static void DemonstrateBordersAndShading(Document document)
        {
            document.LastSection.AddPageBreak();
            document.LastSection.AddParagraph("Borders and Shading", "Heading2");

            document.LastSection.AddParagraph("Border around Paragraph", "Heading3");

            Paragraph paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Borders.Width = 2.5;
            paragraph.Format.Borders.Color = Colors.Navy;
            paragraph.Format.Borders.Distance = 3;
            paragraph.AddText(FillerText.MediumText);

            document.LastSection.AddParagraph("Shading", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Format.Shading.Color = Colors.LightCoral;
            paragraph.AddText(FillerText.Text);

            document.LastSection.AddParagraph("Borders & Shading", "Heading3");

            paragraph = document.LastSection.AddParagraph();
            paragraph.Style = "TextBox";
            paragraph.AddText(FillerText.MediumText);
        }

        public static void DefineTables(Document document)
        {
            //Paragraph paragraph = document.LastSection.AddParagraph("Table Overview", "Heading1");
            //paragraph.AddBookmark("Tables");

            DemonstrateSimpleTable(document);
            //DemonstrateAlignment_2(document);
            //DemonstrateCellMerge(document);
        }

        public static void DemonstrateSimpleTable(Document document)
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

            //document.LastSection.AddParagraph("Simple Tables", "Heading2");

            Table table = new Table();
            table.Borders.Width = 0.75;

            Column column = table.AddColumn(Unit.FromMillimeter(10));
            column.Format.Alignment = ParagraphAlignment.Right;
            table.AddColumn(Unit.FromMillimeter(25));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(30));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(10));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(20));
            column.Format.Alignment = ParagraphAlignment.Left;
            table.AddColumn(Unit.FromMillimeter(40));
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

            //table.SetEdge(0, 0, 2, 3, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);

            document.LastSection.Add(table);
        }

        public static void DemonstrateAlignment_2(Document document)
        {
            document.LastSection.AddParagraph("Cell Alignment", "Heading2");

            Table table = document.LastSection.AddTable();
            table.Borders.Visible = true;
            table.Format.Shading.Color = Colors.LavenderBlush;
            table.Shading.Color = Colors.Salmon;
            table.TopPadding = 5;
            table.BottomPadding = 5;

            Column column = table.AddColumn();
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn();
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn();
            column.Format.Alignment = ParagraphAlignment.Right;

            table.Rows.Height = 35;

            Row row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Top;
            row.Cells[0].AddParagraph("Text");
            row.Cells[1].AddParagraph("Text");
            row.Cells[2].AddParagraph("Text");

            row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].AddParagraph("Text");
            row.Cells[1].AddParagraph("Text");
            row.Cells[2].AddParagraph("Text");

            row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[0].AddParagraph("Text");
            row.Cells[1].AddParagraph("Text");
            row.Cells[2].AddParagraph("Text");
        }

        public static void DemonstrateCellMerge(Document document)
        {
            document.LastSection.AddParagraph("Cell Merge", "Heading2");

            Table table = document.LastSection.AddTable();
            table.Borders.Visible = true;
            table.TopPadding = 5;
            table.BottomPadding = 5;

            Column column = table.AddColumn();
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn();
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn();
            column.Format.Alignment = ParagraphAlignment.Right;

            table.Rows.Height = 35;

            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Merge Right");
            row.Cells[0].MergeRight = 1;

            row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[0].MergeDown = 1;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[0].AddParagraph("Merge Down");

            table.AddRow();
        }

        public static void DefineCharts(Document document)
        {
            Paragraph paragraph = document.LastSection.AddParagraph("Chart Overview", "Heading1");
            paragraph.AddBookmark("Charts");

            document.LastSection.AddParagraph("Sample Chart", "Heading2");

            Chart chart = new Chart();
            chart.Left = 0;

            chart.Width = Unit.FromCentimeter(16);
            chart.Height = Unit.FromCentimeter(12);
            Series series = chart.SeriesCollection.AddSeries();
            series.ChartType = ChartType.Column2D;
            series.Add(new double[] { 1, 17, 45, 5, 3, 20, 11, 23, 8, 19 });
            series.HasDataLabel = true;

            series = chart.SeriesCollection.AddSeries();
            series.ChartType = ChartType.Line;
            series.Add(new double[] { 41, 7, 5, 45, 13, 10, 21, 13, 18, 9 });

            XSeries xseries = chart.XValues.AddXSeries();
            xseries.Add("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N");

            chart.XAxis.MajorTickMark = TickMarkType.Outside;
            chart.XAxis.Title.Caption = "X-Axis";

            chart.YAxis.MajorTickMark = TickMarkType.Outside;
            chart.YAxis.HasMajorGridlines = true;

            chart.PlotArea.LineFormat.Color = Colors.DarkGray;
            chart.PlotArea.LineFormat.Width = 1;

            document.LastSection.Add(chart);
        }
    }
}
