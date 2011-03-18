using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Globalization;
using System.Diagnostics;
using d = System.Drawing;


namespace nsPuls3060
{
    class clsBilagsudskrift
    {
        public static void Bilagsudskrift(bool bDialog)
        {
            Tblbilag[] Bilag;
            var qry = from b in Program.dbDataTransSumma.Tblbilag
                      where b.Udskriv == true
                      && b.Tbltrans.Count() > 0
                      select b;
            int count = qry.Count();
            int iMax = 1;
            if (count > iMax)
            {
                Bilag = new Tblbilag[iMax];
            }
            else
            {
                Bilag = new Tblbilag[count];
            }

            int i = 0;
            foreach (var b in qry)
            {
                Bilag[i++] = b;
                if (i >= iMax) break;
            }

            PrintDialog dialog = new PrintDialog();
            if (bDialog)
                if (dialog.ShowDialog() != true) 
                    return;            
            var paginator = new BilagsudskriftPaginator(Bilag, new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight));
            dialog.PrintDocument(paginator, "Bilagsudskrift");
        }
    }

    public class BilagsudskriftPaginator : DocumentPaginator
    {
        private int _RowsPerPage;
        private Size _PageSize;
        private Tblbilag[] _Bilag;
        private int[] _BilagPages;

        public BilagsudskriftPaginator(Tblbilag[] Bilag, Size pageSize)
        {
            PageSize = pageSize;
            _Bilag = Bilag;
            _BilagPages = new int[Bilag.Length];
            for (var i = 0; i < Bilag.Length; i++)
            {
                int wRows = Bilag[i].Tbltrans.Count;
                _BilagPages[i] = (wRows / _RowsPerPage) + 1;
            }
        }

        public override DocumentPage GetPage(int pageNumber)
        {

            Tblbilag Bilag = null;
            int wpageNumber = 0;
            for (int i = 0; i < _BilagPages.Length; i++)
            {
                if ((pageNumber >= wpageNumber) && (pageNumber < wpageNumber + _BilagPages[i]))
                {
                    Bilag = new Tblbilag
                    {
                        Pid = _Bilag[i].Pid,
                        Regnskabid = _Bilag[i].Regnskabid,
                        Bilag = _Bilag[i].Bilag,
                        Dato = _Bilag[i].Dato,
                        Udskriv = _Bilag[i].Udskriv
                    };

                    int jMin = (pageNumber - wpageNumber) * _RowsPerPage;
                    int jMax = Math.Min(_Bilag[i].Tbltrans.Count(), jMin + _RowsPerPage);
                    Debug.Assert(jMin >= 0);
                    for (int j = jMin; j < jMax; j++)
                    {
                        Tbltrans Trans = new Tbltrans
                        {
                            Pid = _Bilag[i].Tbltrans[j].Pid,
                            Regnskabid = _Bilag[i].Tbltrans[j].Regnskabid,
                            Skjul = _Bilag[i].Tbltrans[j].Skjul,
                            Bilagpid = _Bilag[i].Tbltrans[j].Bilagpid,
                            Tekst = _Bilag[i].Tbltrans[j].Tekst,
                            Kontonr = _Bilag[i].Tbltrans[j].Kontonr,
                            Kontonavn = _Bilag[i].Tbltrans[j].Kontonavn,
                            Moms = _Bilag[i].Tbltrans[j].Moms,
                            Debet = _Bilag[i].Tbltrans[j].Debet,
                            Kredit = _Bilag[i].Tbltrans[j].Kredit,
                            Id = _Bilag[i].Tbltrans[j].Id,
                            Nr = _Bilag[i].Tbltrans[j].Nr,
                            Belob = _Bilag[i].Tbltrans[j].Belob,
                            Afstem = _Bilag[i].Tbltrans[j].Afstem,
                        };
                        Bilag.Tbltrans.Add(Trans);
                    }
                    break;
                }
                else
                {
                    wpageNumber += _BilagPages[i];
                }

            }

            var page = new PageElement(Bilag)
            {
                Width = PageSize.Width,
                Height = PageSize.Height,
            };

            page.Measure(PageSize);
            page.Arrange(new Rect(new Point(0, 0), PageSize));

            return new DocumentPage(page);
        }

        public override bool IsPageCountValid
        {
            get { return true; }
        }

        public override int PageCount
        {
            get { return (int)_BilagPages.Sum(); }
        }

        public override Size PageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = value;

                _RowsPerPage = PageElement.RowsPerPage(PageSize.Height);

                //Can't print anything if you can't fit a row on a page
                Debug.Assert(_RowsPerPage > 0);
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return null; }
        }

    }

    public class PageElement : UserControl
    {
        private const int PageMargin = 75;
        private const int HeaderHeight = 200;
        private const int LineHeight = 30;

        private Tblbilag _Bilag;
        private Pen _Pen;
        public PageElement(Tblbilag Bilag)
        {
            Margin = new Thickness(PageMargin);
            _Bilag = Bilag;
        }

        public static int RowsPerPage(double height)
        {
            return (int)Math.Floor((height - (2 * PageMargin) - HeaderHeight) / LineHeight);
        }

        private static FormattedText MakeHeader0(string text, double len, TextAlignment alm)
        {
            FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 26, Brushes.Black);
            ft.MaxLineCount = 1; ;
            ft.MaxTextWidth = len;
            ft.TextAlignment = alm;
            return ft;
        }

        private static FormattedText MakeHeader1(string text, double len, TextAlignment alm)
        {
            FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Calibri"), 14, Brushes.Black);
            ft.MaxLineCount = 1;
            ft.MaxTextWidth = len;
            ft.TextAlignment = alm;
            return ft;
        }

        private static FormattedText MakeHeader2(string text, double len, TextAlignment alm)
        {
            FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Calibri"), 12, Brushes.Black);
            ft.MaxLineCount = 1;
            ft.MaxTextWidth = len;
            ft.TextAlignment = alm;
            return ft;
        }

        private static FormattedText MakeText(string text, double len, TextAlignment alm)
        {
            FormattedText ft = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Calibri"), 12, Brushes.Black);
            ft.MaxLineCount = 1;
            ft.MaxTextWidth = len;
            ft.TextAlignment = alm;
            return ft;
        }

        public static int CalculateBitLength(string strData, d.Font font)
        {
            using (d.Graphics graphics = d.Graphics.FromImage(new d.Bitmap(1, 1)))
            {
                d.SizeF dtsize = graphics.MeasureString(strData, font);

                return Convert.ToInt32(dtsize.Width);
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            Point textPoint = new Point(0, 32);
            Point rectPoint = new Point(0, 32);
            // Create a Pen to add to the GeometryDrawing created above.
            _Pen = new Pen();
            _Pen.Thickness = 1;
            _Pen.LineJoin = PenLineJoin.Miter;
            _Pen.EndLineCap = PenLineCap.Square;

            // Create a gradient to use as a value for the Pen's Brush property.
            GradientStop firstStop = new GradientStop();
            firstStop.Color = Colors.Black;
            GradientStop secondStop = new GradientStop();
            secondStop.Offset = 1.0;
            secondStop.Color = Colors.Black;

            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
            myLinearGradientBrush.GradientStops.Add(firstStop);
            myLinearGradientBrush.GradientStops.Add(secondStop);

            _Pen.Brush = myLinearGradientBrush;

            int Regnskabid = (int)_Bilag.Regnskabid;
            recMemRegnskab rec_regnskab = (from r in Program.memRegnskab where r.Rid == Regnskabid select r).First();
            string firma = rec_regnskab.Firmanavn;
            DateTime startdato = (DateTime)rec_regnskab.Start;
            DateTime slutdato = (DateTime)rec_regnskab.Slut;
            DateTime bilagsdato = (DateTime)_Bilag.Dato;

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
            DateTime PeriodeMonthYear = new DateTime(slutdato.Year, pnr,1);
            string Periode = PeriodeMonthYear.ToString("MM-yyyy");

            dc.DrawText(MakeHeader0("Bogføringsbilag " + firma, (int)(Width - (2 * PageMargin)), TextAlignment.Left), textPoint);
            textPoint.Y += 72;

            textPoint.X = 0;
            dc.DrawText(MakeHeader1("Dato", 50, TextAlignment.Left), textPoint);
            textPoint.X += 50;
            dc.DrawText(MakeHeader1(((DateTime)_Bilag.Dato).ToShortDateString(), 80, TextAlignment.Left), textPoint);

            textPoint.X = ((Width - 2 * PageMargin) / 2) - 60;
            dc.DrawText(MakeHeader1("Periode", 60, TextAlignment.Left), textPoint);
            textPoint.X += 60;
            dc.DrawText(MakeHeader1(Periode, 60, TextAlignment.Right), textPoint);

            textPoint.X = Width - (2 * PageMargin + 30 + 30);
            dc.DrawText(MakeHeader1("Bilag", 30, TextAlignment.Left), textPoint);
            textPoint.X += 30;
            dc.DrawText(MakeHeader1(((int)_Bilag.Bilag).ToString(), 30, TextAlignment.Right), textPoint);

            textPoint.Y += 72;
            textPoint.X = 5.5;

            TextinBox(dc, "Tekst", 248, TextAlignment.Left, textPoint);
            textPoint.X += 248;

            TextinBox(dc, "Kontonr", 50, TextAlignment.Right, textPoint);
            textPoint.X += 50;

            TextinBox(dc, "Kontonavn", 172, TextAlignment.Left, textPoint);
            textPoint.X += 172;

            TextinBox(dc, "Debet", 85, TextAlignment.Right, textPoint);
            textPoint.X += 85;

            TextinBox(dc, "Kredit", 85, TextAlignment.Right, textPoint);



            foreach (var t in _Bilag.Tbltrans)
            {
                textPoint.Y += LineHeight;
                textPoint.X = 5.5;

                TextinBox(dc, t.Tekst, 248, TextAlignment.Left, textPoint);
                textPoint.X += 248;

                TextinBox(dc, t.Kontonr.ToString(), 50, TextAlignment.Right, textPoint);
                textPoint.X += 50;

                TextinBox(dc, t.Kontonavn, 172, TextAlignment.Left, textPoint);
                textPoint.X += 172;

                TextinBox(dc, t.Debet.ToString(), 85, TextAlignment.Right, textPoint);
                textPoint.X += 85;

                TextinBox(dc, t.Kredit.ToString(), 85, TextAlignment.Right, textPoint);
            }
        }
        private void TextinBox(DrawingContext dc, string txt, double width, TextAlignment al, Point textPoint)
        {
            Point rectPoint = textPoint;
            rectPoint.X -= 5;
            rectPoint.Y -= 12;
            if (al == TextAlignment.Right)
                textPoint.X -= 5;
            dc.DrawRectangle(null, _Pen, new Rect(rectPoint, new Size(width, LineHeight)));
            dc.DrawText(MakeHeader2(txt, width - 4, al), textPoint);

        }
    }
}
