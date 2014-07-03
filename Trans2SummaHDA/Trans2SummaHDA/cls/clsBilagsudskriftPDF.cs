using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;

namespace Trans2SummaHDA
{
    class clsBilagsudskriftPDF
    {
        public static void BilagsudskriftPDF()
        {
            tblbilag[] Bilag;
            var qry = from b in Program.dbDataTransSumma.tblbilags
                      where b.udskriv == true
                      && b.tbltrans.Count() > 0
                      select b;
            int count = qry.Count();
            int iMax = 99999;
            if (count > iMax)
            {
                Bilag = new tblbilag[iMax];
            }
            else
            {
                Bilag = new tblbilag[count];
            }

            int i = 0;
            foreach (var b in qry)
            {
                UdskrivBilag(b);
                b.udskriv = false;
                if (i >= iMax) break;
            }

            //Program.dbDataTransSumma.SubmitChanges();
        }

        public static void UdskrivBilag(tblbilag Bilag)
        {
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();

            //margin
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
            margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(3.17f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;

            // Create one page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);

            float y = 10;

            //title
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Center);
            page.Canvas.DrawString("Bogføringsbilag", font1, brush1, page.Canvas.ClientSize.Width / 2, y, format1);
            y = y + font1.MeasureString("Bogføringsbilag", format1).Height;
            y = y + 5;
            
            String[][] dataSource = new String[Bilag.tbltrans.Count + 1][];
            int i = 0;
            foreach (tbltran t in Bilag.tbltrans)
            {
                if (i ==0)
                {
                    String[] headings = new String[5];
                    headings[0] = "Tekst";
                    headings[1] = "Kontonr";
                    headings[2] = "Kontonavn";
                    headings[3] = "Debet";
                    headings[4] = "Kredit";
                    dataSource[i++] = headings;
                }

                String[] datarow = new String[5];
                datarow[0] = t.tekst;
                datarow[1] = t.kontonr.ToString();
                datarow[2] = t.kontonavn;
                datarow[3] = t.debet != null ? ((decimal)(t.debet)).ToString("#,0.00;-#,0.00") : "";
                datarow[4] = t.kredit != null ? ((decimal)(t.kredit)).ToString("#,0.00;-#,0.00") : "";
                dataSource[i++] = datarow;
            }

            PdfTable table = new PdfTable();
            table.Style.CellPadding = 2;
            table.Style.HeaderSource = PdfHeaderSource.Rows;
            table.Style.HeaderRowCount = 1;
            table.Style.HeaderStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            table.Style.HeaderStyle.Font = new PdfTrueTypeFont(new Font("Arial", 9f, FontStyle.Bold));
            table.Style.ShowHeader = true;
            table.DataSource = dataSource;

            float width = page.Canvas.ClientSize.Width;
            table.Columns[0].Width = width * 0.30f * width;
            table.Columns[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
            table.Columns[1].Width = width * 0.10f * width;
            table.Columns[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            table.Columns[2].Width = width * 0.30f * width;
            table.Columns[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
            table.Columns[3].Width = width * 0.15f * width;
            table.Columns[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            table.Columns[4].Width = width * 0.15f * width;
            table.Columns[4].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);

            PdfLayoutResult result = table.Draw(page, new PointF(0, y));
            y = y + result.Bounds.Height + 5;

            PdfBrush brush2 = PdfBrushes.Gray;
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 9f));
            page.Canvas.DrawString(String.Format("* {0} poster på bilaget.", Bilag.tbltrans.Count),
                font2, brush2, 5, y);

            //Save pdf file.
            doc.SaveToFile("SimpleTable3.pdf");
            doc.Close();
        }

    }

}

