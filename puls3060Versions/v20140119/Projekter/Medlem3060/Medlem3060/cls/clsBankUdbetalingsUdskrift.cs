using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Tables;
using System.Drawing;
using nsPbs3060;
using System.IO;

namespace nsPuls3060
{
    class clsBankUdbetalingsUdskrift
    {
        public clsBankUdbetalingsUdskrift() { }

        public void BankUdbetalingsUdskrifter(dbData3060DataContext p_dbData3060, int lobnr) 
        {
            var antal = (from c in p_dbData3060.tbltilpbs
                         where c.id == lobnr
                         select c).Count();
            if (antal == 0) { throw new Exception("101 - Der er ingen PBS forsendelse for id: " + lobnr); }

                       var qrykrd = from k in p_dbData3060.tblMedlems
                         join h in p_dbData3060.tbloverforsels on k.Nr equals h.Nr
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
                PdfPageBase page = doc.Pages.Add(PdfPageSize.A4, margin);
                float width = page.Canvas.ClientSize.Width;

                float y = 10;

                //title
                PdfBrush brush1 = PdfBrushes.Black;
                PdfTrueTypeFont font1 = new PdfTrueTypeFont(new Font("Arial", 16f, FontStyle.Bold));
                PdfStringFormat format1 = new PdfStringFormat(PdfTextAlignment.Left);
                string s = String.Format("Bankudbetaling {0}", ((int)krd.SFaknr).ToString());
                page.Canvas.DrawString(s, font1, brush1, 1, y, format1);
                
                y = y + font1.MeasureString(s, format1).Height;
                y = y + 25;
                PdfBrush brush2 = PdfBrushes.Black;
                PdfTrueTypeFont font2 = new PdfTrueTypeFont(new Font("Arial", 10f, FontStyle.Bold));

                PdfStringFormat format2 = new PdfStringFormat(PdfTextAlignment.Left);
                s = String.Format("Tekst på eget kontoudtog: {0}", krd.advistekst);
                page.Canvas.DrawString(s, font2, brush2, 1, y, format2);

                y = y + font2.MeasureString(s, format2).Height;
                y = y + 5;
                s = String.Format("Navn/kendenavn: {0}", krd.Navn);
                page.Canvas.DrawString(s, font2, brush2, 1, y, format2);
                
                y = y + font2.MeasureString(s, format2).Height;
                y = y + 5;
                s = String.Format("Regnr: {0}", krd.bankregnr);
                page.Canvas.DrawString(s, font2, brush2, 1, y, format2);
                
                y = y + font2.MeasureString(s, format2).Height;
                y = y + 5;
                s = String.Format("Konto: {0}", krd.bankkontonr);
                page.Canvas.DrawString(s, font2, brush2, 1, y, format2);

                y = y + font2.MeasureString(s, format2).Height;
                y = y + 5;
                s = String.Format("Tekst til beløbsmodtager: {0}", krd.advistekst);
                page.Canvas.DrawString(s, font2, brush2, 1, y, format2);

                y = y + font2.MeasureString(s, format2).Height;
                y = y + 5;
                s = String.Format("Beløb: {0}", ((decimal)(krd.advisbelob)).ToString("#,0.00;-#,0.00"));
                page.Canvas.DrawString(s, font2, brush2, 1, y, format2);

                y = y + font2.MeasureString(s, format2).Height;
                y = y + 5; 
                s = String.Format("Betalingsdato: {0}", ((DateTime)krd.betalingsdato).ToShortDateString());
                page.Canvas.DrawString(s, font2, brush2, 1, y, format2);
               
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
