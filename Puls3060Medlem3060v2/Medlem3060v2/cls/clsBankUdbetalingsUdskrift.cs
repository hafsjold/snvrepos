using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System.Drawing;
using nsPbs3060v2;
using System.IO;


namespace nsPuls3060v2
{
    class clsBankUdbetalingsUdskrift
    {
        public clsBankUdbetalingsUdskrift() { }

        public void BankUdbetalingsUdskrifter(dbData3060 p_dbData3060, int lobnr) 
        {
            var antal = (from c in p_dbData3060.tbltilpbs
                         where c.id == lobnr
                         select c).Count();
            if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }

                       var qrykrd = from k in p_dbData3060.tblMedlem
                         join h in p_dbData3060.tbloverforsel on k.Nr equals h.Nr
                         where h.tilpbsid == lobnr
                         select new 
                         {
                           k.Nr,
                           k.Navn,
                           h.betalingsdato,
                           h.advistekst,
                           h.advisbelob,
                           h.bankregnr,
                           h.bankkontonr,
                           h.SFaknr,
                         };


            // Start loop over betalinger i tbloverforsel
            int testantal = qrykrd.Count();
            foreach (var krd in qrykrd)
            {
                //Create a pdf document.
                PdfDocument doc = new PdfDocument();

                doc.DocumentInformation.Author = "Løbeklubben Puls 3060";
                doc.DocumentInformation.Title = String.Format("Bankudbetaling {0}", "");
                doc.DocumentInformation.Creator = "Medlem3060";
                doc.DocumentInformation.Subject = String.Format("Faktura {0}", ((int)krd.SFaknr).ToString());
                doc.DocumentInformation.CreationDate = DateTime.Now;

                //margin
                PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
                PdfMargins margin = new PdfMargins();
                margin.Top = unitCvtr.ConvertUnits(2.54f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
                margin.Bottom = margin.Top;
                margin.Left = unitCvtr.ConvertUnits(2f, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point);
                margin.Right = margin.Left;

                // Create one page
                PdfPageBase page = doc.Pages.Add(PdfPageSize.A5, margin);
                float width = page.Canvas.ClientSize.Width;

                float y = 5;

                //title
                PdfBrush brush1 = PdfBrushes.Black;
                PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
                PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Left);
                string s = String.Format("Bankudbetaling {0}", ((int)krd.SFaknr).ToString());
                page.Canvas.DrawString(s, font1, brush1, 1, y, format1);

                y = y + 35; 

                String[][] dataSource = new String[7][];
                int i = 0;
                String[] datarow = new String[2];
                datarow[0] = "Tekst på eget kontoudtog";
                datarow[1] = krd.advistekst;
                dataSource[i++] = datarow;

                datarow = new String[2];
                datarow[0] = "Navn/kendenavn";
                datarow[1] = krd.Navn;
                dataSource[i++] = datarow;

                datarow = new String[2];
                datarow[0] = "Regnr";
                datarow[1] = krd.bankregnr;
                dataSource[i++] = datarow;

                datarow = new String[2];
                datarow[0] = "Konto";
                datarow[1] = krd.bankkontonr;
                dataSource[i++] = datarow;

                datarow = new String[2];
                datarow[0] = "Tekst til beløbsmodtager";
                datarow[1] = krd.advistekst;
                dataSource[i++] = datarow;

                datarow = new String[2];
                datarow[0] = "Beløb";
                datarow[1] = ((decimal)(krd.advisbelob)).ToString("#0.00;-#0.00");
                dataSource[i++] = datarow;

                datarow = new String[2];
                datarow[0] = "Betalingsdato";
                datarow[1] = ((DateTime)krd.betalingsdato).ToShortDateString();
                dataSource[i++] = datarow;

                PdfTable table = new PdfTable();

                table.Style.CellPadding = 5; //2;
                table.Style.HeaderSource = PdfHeaderSource.Rows;
                table.Style.HeaderRowCount = 0;
                table.Style.HeaderStyle.StringFormat = new PdfStringFormat(PdfTextAlignment.Center);
                table.Style.HeaderStyle.Font = new PdfTrueTypeFont(new Font("Arial", 9f, FontStyle.Bold));
                table.Style.ShowHeader = true;
                table.Style.RepeatHeader = true;
                table.DataSource = dataSource;

                table.Columns[0].Width = 100;// width * 0.30f * width;
                table.Columns[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                table.Columns[1].Width = 100;// width * 0.10f * width;
                table.Columns[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
 
                PdfLayoutResult result = table.Draw(page, new PointF(0, y));
                y = y + result.Bounds.Height + 5;
                
                //Save pdf file.
                string BilagPath = (from r in Program.karMedlemPrivat where r.key == "BankudbetalingPath" select r.value).First();
                string BilagNavn = String.Format(@"Faktura {0}.pdf", ((int)krd.SFaknr).ToString());
                string filename = Path.Combine(BilagPath, BilagNavn);

                doc.SaveToFile(filename);
                doc.Close();
           
            }
        }

 
    }
}
