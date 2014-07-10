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
using System.IO;

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

            Program.dbDataTransSumma.SubmitChanges();
        }

        public static void UdskrivBilag(tblbilag Bilag)
        {
           int Regnskabid = (int)Bilag.regnskabid;
            recMemRegnskab rec_regnskab = (from r in Program.memRegnskab where r.Rid == Regnskabid select r).First();
            string firma = rec_regnskab.Firmanavn;
            string regnskabsnavn = rec_regnskab.Navn;
            DateTime startdato = (DateTime)rec_regnskab.Start;
            DateTime slutdato = (DateTime)rec_regnskab.Slut;
            DateTime bilagsdato = (DateTime)Bilag.dato;

            int startMonth = startdato.Month;
            int bilagMonth = bilagsdato.Month;
            int regnskabsaar = slutdato.Year;
            int pnr;
            if (bilagMonth < startMonth)
            {
                pnr = bilagMonth + 12 - startMonth + 1;
            }
            else
            {
                pnr = bilagMonth - startMonth + 1;
            }
            DateTime PeriodeMonthYear = new DateTime(slutdato.Year, pnr, 1);
            string Periode = PeriodeMonthYear.ToString("MM");

            
            //Create a pdf document.
            PdfDocument doc = new PdfDocument();

            doc.DocumentInformation.Author = "Hafsjold Data ApS";
            doc.DocumentInformation.Title = String.Format("Bogføringsbilag {0}", regnskabsnavn);
            doc.DocumentInformation.Creator = "Trans2SummaHDA";
            doc.DocumentInformation.Subject = String.Format("Bilag {0}", ((int)Bilag.bilag).ToString());
            doc.DocumentInformation.CreationDate = DateTime.Now;

            //margin
            PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
            PdfMargins margin = new PdfMargins();
            margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Bottom = margin.Top;
            margin.Left = unitCvtr.ConvertUnits(2f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
            margin.Right = margin.Left;

            // Create one page
            PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
            float width = page.Canvas.ClientSize.Width;

            float y = 10;

            //title
            PdfBrush brush1 = PdfBrushes.Black;
            PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
            PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Left);
            string s = String.Format("Bogføringsbilag {0}", regnskabsnavn); 
            page.Canvas.DrawString(s, font1, brush1, 1, y, format1);
            y = y + font1.MeasureString(s, format1).Height;
            y = y + 25;

            PdfBrush brush2 = PdfBrushes.Black;
            PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 10f, FontStyle.Bold));
            
            PdfStringFormat format2 = new PdfStringFormat(PdfTextAlignment.Left);
            s = String.Format("Dato: {0}", ((DateTime)Bilag.dato).ToShortDateString()); 
            page.Canvas.DrawString(s, font2, brush2, 1, y, format2);

            PdfStringFormat format21 = new PdfStringFormat(PdfTextAlignment.Center);
            s = String.Format("Periode: {0}", Periode);
            page.Canvas.DrawString(s, font2, brush2, width / 2, y, format21);

            PdfStringFormat format22 = new PdfStringFormat(PdfTextAlignment.Right); 
            s = String.Format("Bilag: {0}", ((int)Bilag.bilag).ToString());
            page.Canvas.DrawString(s, font2, brush2, width, y, format22);
            
            y = y + font2.MeasureString(s, format2).Height;
            y = y + 25;

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

            table.Style.CellPadding = 5; //2;
            table.Style.HeaderSource = PdfHeaderSource.Rows;
            table.Style.HeaderRowCount = 1;
            table.Style.HeaderStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
            table.Style.HeaderStyle.Font = new PdfTrueTypeFont(new Font("Arial", 9f, FontStyle.Bold));
            table.Style.ShowHeader = true;
            table.Style.RepeatHeader = true;
            table.DataSource = dataSource;


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

            //Save pdf file.
            string BilagPath = (from r in Program.karTrans2Summa where r.key == "BilagPath" select r.value).First();
            string BilagNavn = String.Format(@"Bilag {0}.pdf", ((int)Bilag.bilag).ToString());
            string filename = Path.Combine(BilagPath, BilagNavn);

            doc.SaveToFile(filename);
            doc.Close();
        }

    }

}

