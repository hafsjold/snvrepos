using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Threading;

namespace Trans2Summa3060
{
    public class clsExcel
    {
        public void ecxelPoster()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "Poster";

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheetPoster;
            _Excel._Worksheet oSheetRegnskab;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xlsx";


            var JournalPoster = from h in Program.karPosteringer
                                join d1 in Program.karKontoplan on h.Konto equals d1.Kontonr into details1
                                from x in details1.DefaultIfEmpty()
                                orderby h.Nr
                                select new clsJournalposter
                                {
                                    ds = (x.Type == "Drift") ? "D" : "S",
                                    k = IUAP(x.Type, x.DK),
                                    Konto = h.Konto.ToString() + "-" + x.Kontonavn,
                                    Dato = h.Dato,
                                    Bilag = h.Bilag,
                                    Nr = h.Nr,
                                    Id = h.Id,
                                    Tekst = h.Tekst,
                                    Beløb = h.Nettobeløb,
                                };



            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new _Excel.Application();
                    //oXL.Visible = false;
                    oXL.Visible = true; //For debug

                    //Get a new workbook.
                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheetPoster = (_Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheetPoster.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    Program.frmMain.MainformProgressBar.Value = 0;
                    Program.frmMain.MainformProgressBar.Minimum = 0;
                    Program.frmMain.MainformProgressBar.Maximum = (from h in Program.karPosteringer select h).Count();
                    Program.frmMain.MainformProgressBar.Step = 1;
                    Program.frmMain.MainformProgressBar.Visible = true;
                    foreach (clsJournalposter m in JournalPoster)
                    {
                        Program.frmMain.MainformProgressBar.PerformStep();
                        row++;
                        Type objectType = m.GetType();
                        PropertyInfo[] properties = objectType.GetProperties();
                        int col = 0;
                        foreach (PropertyInfo property in properties)
                        {
                            col++;
                            string Name = property.Name;
                            //string NamePropertyType = property.GetValue(m, null).GetType().ToString();
                            oSheetPoster.Cells[row, col] = property.GetValue(m, null);
                            if (row == 2)
                            {
                                object[] CustomAttributes = property.GetCustomAttributes(false);
                                foreach (var att in CustomAttributes)
                                {
                                    Type tp = att.GetType();
                                    if (tp.ToString() == "Trans2Summa3060.Fieldattr")
                                    {
                                        Fieldattr attr = (Fieldattr)att;
                                        string heading = attr.Heading;
                                        oSheetPoster.Cells[1, col] = heading;

                                    }
                                }
                            }
                        }
                    }

                    oRng = (_Excel.Range)oSheetPoster.Rows[1, Missing.Value];
                    oRng.Font.Name = "Arial";
                    oRng.Font.Size = 12;
                    oRng.Font.Strikethrough = false;
                    oRng.Font.Superscript = false;
                    oRng.Font.Subscript = false;
                    oRng.Font.OutlineFont = false;
                    oRng.Font.Shadow = false;
                    oRng.Font.Bold = true;
                    oRng.HorizontalAlignment = _Excel.Constants.xlCenter;
                    oRng.VerticalAlignment = _Excel.Constants.xlBottom;
                    oRng.WrapText = false;
                    oRng.Orientation = 0;
                    oRng.AddIndent = false;
                    oRng.IndentLevel = 0;
                    oRng.ShrinkToFit = false;
                    oRng.MergeCells = false;

                    string BottomRight = "D" + row.ToString();
                    oRng = oSheetPoster.get_Range("D2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheetPoster.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.FreezePanes = true;

                    oSheetPoster.get_Range("A1", Missing.Value).Select();


                    oSheetRegnskab = (_Excel._Worksheet)oWB.Worksheets.Add(System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);

                    //oXL.Visible = true; //For debug

                    _Excel.Range x1 = oSheetPoster.Cells[1, 1];
                    _Excel.Range x2 = oSheetPoster.Cells[row, 9];
                    _Excel.Range xx = oSheetPoster.get_Range(x1, x2);
                    _Excel.PivotField _pvtField = null;
                    _Excel.PivotTable _pivot = oSheetPoster.PivotTableWizard(
                        _Excel.XlPivotTableSourceType.xlDatabase,  //SourceType
                        xx, //SourceData
                        oSheetRegnskab.get_Range("A3", Missing.Value),  //TableDestination
                        "PivotTable1", //TableName
                        System.Type.Missing, //RowGrand
                        System.Type.Missing, //CollumnGrand
                        System.Type.Missing, //SaveData
                        System.Type.Missing, //HasAutoformat
                        System.Type.Missing, //AutoPage
                        System.Type.Missing, //Reserved
                        System.Type.Missing, //BackgroundQuery
                        System.Type.Missing, //OptimizeCache 
                        System.Type.Missing, //PageFieldOrder 
                        System.Type.Missing, //PageFieldWrapCount 
                        System.Type.Missing, //ReadData 
                        System.Type.Missing);//Connection 

                    _pvtField = (_Excel.PivotField)_pivot.PivotFields("ds");
                    _pvtField.Orientation = _Excel.XlPivotFieldOrientation.xlRowField;

                    _pvtField = (_Excel.PivotField)_pivot.PivotFields("k");
                    _pvtField.Orientation = _Excel.XlPivotFieldOrientation.xlRowField;

                    _pvtField = (_Excel.PivotField)_pivot.PivotFields("Konto");
                    _pvtField.Orientation = _Excel.XlPivotFieldOrientation.xlRowField;

                    _pvtField = (_Excel.PivotField)_pivot.PivotFields("Dato");
                    _pvtField.Orientation = _Excel.XlPivotFieldOrientation.xlColumnField;

                    _pvtField = (_Excel.PivotField)_pivot.PivotFields("Beløb");
                    _pvtField.Orientation = _Excel.XlPivotFieldOrientation.xlDataField;
                    _pvtField.Function = _Excel.XlConsolidationFunction.xlSum;
                    _pvtField.NumberFormat = "#,##0";

                    oSheetRegnskab.Name = "Regnskab";
                    oRng = oSheetRegnskab.get_Range("D3", Missing.Value);
                    oRng.Select();
                    bool[] Periods = { false, false, false, false, true, false, true };
                    oRng.Group(true, true, Missing.Value, Periods);

                    oRng = oSheetRegnskab.get_Range("D4", "P4");
                    oRng.HorizontalAlignment = _Excel.XlHAlign.xlHAlignRight;

                    oSheetRegnskab.PageSetup.LeftHeader = "&14" + rec_regnskab.Navn;
                    oSheetRegnskab.PageSetup.CenterHeader = "";
                    oSheetRegnskab.PageSetup.RightHeader = "&P af &N";
                    oSheetRegnskab.PageSetup.LeftFooter = "&Z&F";
                    oSheetRegnskab.PageSetup.CenterFooter = "";
                    oSheetRegnskab.PageSetup.RightFooter = "&D&T";
                    oSheetRegnskab.PageSetup.LeftMargin = oXL.InchesToPoints(0.75);
                    oSheetRegnskab.PageSetup.RightMargin = oXL.InchesToPoints(0.75);
                    oSheetRegnskab.PageSetup.TopMargin = oXL.InchesToPoints(1);
                    oSheetRegnskab.PageSetup.BottomMargin = oXL.InchesToPoints(1);
                    oSheetRegnskab.PageSetup.HeaderMargin = oXL.InchesToPoints(0.5);
                    oSheetRegnskab.PageSetup.FooterMargin = oXL.InchesToPoints(0.5);
                    oSheetRegnskab.PageSetup.PrintHeadings = false;
                    oSheetRegnskab.PageSetup.PrintGridlines = true;
                    oSheetRegnskab.PageSetup.CenterHorizontally = false;
                    oSheetRegnskab.PageSetup.CenterVertically = false;
                    oSheetRegnskab.PageSetup.Orientation = _Excel.XlPageOrientation.xlLandscape;
                    oSheetRegnskab.PageSetup.Draft = false;
                    oSheetRegnskab.PageSetup.PaperSize = _Excel.XlPaperSize.xlPaperA4;
                    oSheetRegnskab.PageSetup.FirstPageNumber = 1;
                    oSheetRegnskab.PageSetup.Order = _Excel.XlOrder.xlDownThenOver;
                    oSheetRegnskab.PageSetup.BlackAndWhite = false;
                    oSheetRegnskab.PageSetup.Zoom = 100;
                    oSheetRegnskab.PageSetup.PrintErrors = _Excel.XlPrintErrors.xlPrintErrorsDisplayed;

                    oWB.ShowPivotTableFieldList = false;

                    for (var i = oWB.Worksheets.Count; i > 0; i--)
                    {
                        _Excel._Worksheet oSheetWrk = (_Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if ((oSheetWrk.Name != "Regnskab") && (oSheetWrk.Name != "Poster"))
                        {
                            oSheetWrk.Delete();
                        }
                    }

                    oSheetRegnskab.get_Range("A1", Missing.Value).Select();

                    oWB.SaveAs(SaveAs, _Excel.XlFileFormat.xlWorkbookDefault, "", "", false, false, _Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    Program.frmMain.MainformProgressBar.Visible = false;

                    //oXL.Quit();
                    //oXL = null;
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
            }
        }

        private string IUAP(string Type, string DK)
        {
            if (Type == "Drift")
            {
                return (DK == "1") ? "I" : "U";
            }
            else
            {
                return (DK == "1") ? "P" : "A";
            }
        }
    }

    public class Fieldattr : Attribute
    {
        public string Heading { get; set; }
    }
    class ExcelUILanguageHelper : IDisposable
    {
        private CultureInfo m_CurrentCulture;

        public ExcelUILanguageHelper()
        {
            // save current culture and set culture to en-US 
            m_CurrentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        #region IDisposable Members

        public void Dispose()
        {
            // return to normal culture 
            Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
        }

        #endregion
    }
    public class clsJournalposter
    {
        [Fieldattr(Heading = "ds")]
        public string ds { get; set; }
        [Fieldattr(Heading = "k")]
        public string k { get; set; }
        [Fieldattr(Heading = "Konto")]
        public string Konto { get; set; }
        [Fieldattr(Heading = "Dato")]
        public DateTime? Dato { get; set; }
        [Fieldattr(Heading = "Bilag")]
        public int? Bilag { get; set; }
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Id")]
        public int? Id { get; set; }
        [Fieldattr(Heading = "Tekst")]
        public string Tekst { get; set; }
        [Fieldattr(Heading = "Beløb")]
        public decimal? Beløb { get; set; }
    }
}
