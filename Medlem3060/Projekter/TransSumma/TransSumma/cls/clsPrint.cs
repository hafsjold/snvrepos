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



namespace nsPuls3060
{
    class clsPrint
    {
        public static void testPrint()
        {
            PrintDialog dialog = new PrintDialog();
            if (dialog.ShowDialog() != true)
                return;

            int rows = 75;
            var paginator = new RandomTabularPaginator(rows, new Size(dialog.PrintableAreaWidth, dialog.PrintableAreaHeight));

            dialog.PrintDocument(paginator, "My Random Data Table");
        }
    }

    public class RandomTabularPaginator : DocumentPaginator
    {
        private int _RowsPerPage;
        private Size _PageSize;
        private int _Rows;

        public RandomTabularPaginator(int rows, Size pageSize)
        {
            _Rows = rows;
            PageSize = pageSize;
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            int currentRow = _RowsPerPage * pageNumber;

            var page = new PageElement(currentRow,
              Math.Min(_RowsPerPage, _Rows - currentRow))
            {
                Width = PageSize.Width,
                Height = PageSize.Height,
            };

            page.Measure(PageSize);
            page.Arrange(new Rect(new Point(0, 0), PageSize));

            return new DocumentPage(page);
        }

        public override bool IsPageCountValid
        { get { return true; } }

        public override int PageCount
        { get { return (int)Math.Ceiling(_Rows / (double)_RowsPerPage); } }

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
        { get { return null; } }

    }

    public class PageElement : UserControl
    {
        private const int PageMargin = 75;
        private const int HeaderHeight = 25;
        private const int LineHeight = 20;
        private const int ColumnWidth = 140;

        private int _CurrentRow;
        private int _Rows;

        public PageElement(int currentRow, int rows)
        {
            Margin = new Thickness(PageMargin);
            _CurrentRow = currentRow;
            _Rows = rows;
        }

        public static int RowsPerPage(double height)
        {
            return (int)Math.Floor((height - (2 * PageMargin)
              - HeaderHeight) / LineHeight);
        }

        private static FormattedText MakeText(string text)
        {
            return new FormattedText(text, CultureInfo.CurrentCulture,
              FlowDirection.LeftToRight, new Typeface("Tahoma"), 14, Brushes.Black);
        }

        protected override void OnRender(DrawingContext dc)
        {
            Point curPoint = new Point(0, 0);

            dc.DrawText(MakeText("Row Number"), curPoint);
            curPoint.X += ColumnWidth;
            for (int i = 1; i < 4; i++)
            {
                dc.DrawText(MakeText("Column " + i), curPoint);
                curPoint.X += ColumnWidth;
            }

            curPoint.X = 0;
            curPoint.Y += LineHeight;

            dc.DrawRectangle(Brushes.Black, null, new Rect(curPoint, new Size(Width, 2)));
            curPoint.Y += HeaderHeight - LineHeight;

            Random numberGen = new Random();
            for (int i = _CurrentRow; i < _CurrentRow + _Rows; i++)
            {
                dc.DrawText(MakeText(i.ToString()), curPoint);
                curPoint.X += ColumnWidth;
                for (int j = 1; j < 4; j++)
                {
                    dc.DrawText(MakeText(numberGen.Next().ToString()), curPoint);
                    curPoint.X += ColumnWidth;
                }
                curPoint.Y += LineHeight;
                curPoint.X = 0;
            }
        }
    }
}
