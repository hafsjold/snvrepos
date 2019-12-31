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
using Uniconta.DataModel;
using Uniconta.Common;

namespace Trans2SummaHDC
{
    public class clsExcel
    {
         private string IUAP(GLAccountTypes Type)
        {
            switch (Type)
            {
                case GLAccountTypes.PL:
                case GLAccountTypes.Revenue:
                case GLAccountTypes.Income:
                    return "I";

                case GLAccountTypes.Cost:
                case GLAccountTypes.CostOfGoodSold:
                case GLAccountTypes.Expense:
                case GLAccountTypes.Depreciasions:
                    return "U";

                case GLAccountTypes.BalanceSheet:
                    return "X";

                case GLAccountTypes.Asset:
                case GLAccountTypes.FixedAssets:
                case GLAccountTypes.CurrentAsset:
                case GLAccountTypes.Inventory:
                case GLAccountTypes.Debtor:
                case GLAccountTypes.LiquidAsset:
                case GLAccountTypes.Bank:
                    return "A";

                case GLAccountTypes.Liability:
                case GLAccountTypes.Equity:
                case GLAccountTypes.Creditor:
                    return "P";

                case GLAccountTypes.Header:
                case GLAccountTypes.Sum:
                case GLAccountTypes.CalculationExpression:
                default:
                    return "Y";
            }
        }

        private string DS(GLAccountTypes Type)
        {
            switch (Type)
            {
                case GLAccountTypes.PL:
                case GLAccountTypes.Revenue:
                case GLAccountTypes.Income:
                case GLAccountTypes.Cost:
                case GLAccountTypes.CostOfGoodSold:
                case GLAccountTypes.Expense:
                case GLAccountTypes.Depreciasions:
                    return "D";

                case GLAccountTypes.BalanceSheet:
                    return "X";

                case GLAccountTypes.Asset:
                case GLAccountTypes.FixedAssets:
                case GLAccountTypes.CurrentAsset:
                case GLAccountTypes.Inventory:
                case GLAccountTypes.Debtor:
                case GLAccountTypes.LiquidAsset:
                case GLAccountTypes.Bank:
                case GLAccountTypes.Liability:
                case GLAccountTypes.Equity:
                case GLAccountTypes.Creditor:
                    return "S";

                case GLAccountTypes.Header:
                case GLAccountTypes.Sum:
                case GLAccountTypes.CalculationExpression:
                default:
                    return "Y";
            }
        }

        public void ecxelPoster()
        {
            var api = UCInitializer.GetBaseAPI;
            CompanyFinanceYear CurrentCompanyFinanceYear = null;
            var task1 = api.Query<CompanyFinanceYear>();
            task1.Wait();
            var cols1 = task1.Result;
            foreach (var col in cols1)
            {
                //if (col._FromDate.Year == 2019)
                if (col._Current)
                {
                    CurrentCompanyFinanceYear = col;
                }
            }
            var task2a = api.Query<Debtor>();
            task2a.Wait();
            var karDebtor = task2a.Result;
            var task2b = api.Query<Creditor>();
            task2b.Wait();
            var karCreditor = task2b.Result;
            KarDebCred karDebCred = new KarDebCred();
            foreach (var d in karDebtor)
            {
                RecDebCred recDebCred = new RecDebCred()
                {
                    _Account = d._Account,
                    _Name = d._Name
                };
                karDebCred.Add(recDebCred);
            }
            foreach (var k in karCreditor)
            {
                RecDebCred recDebCred = new RecDebCred()
                {
                    _Account = k._Account,
                    _Name = k._Name
                };
                karDebCred.Add(recDebCred);
            }

            var task3 = api.Query<GLAccount>();
            task3.Wait();
            var karGLAccount = task3.Result;

            var crit = new List<PropValuePair>();
            string dateinterval = string.Format("{0}..{1}", CurrentCompanyFinanceYear._FromDate.ToShortDateString(), CurrentCompanyFinanceYear._ToDate.ToShortDateString());
            var pair = PropValuePair.GenereteWhereElements("Date", typeof(DateTime), dateinterval);
            crit.Add(pair);
            var task4 = api.Query<GLTrans>(crit);
            task4.Wait();
            var karGLTrans = task4.Result;

            DateTime pReadDate = DateTime.Now;
            string pSheetName = "Poster";
            char[] dash = { '-' };

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheetPoster;
            _Excel._Worksheet oSheetRegnskab;
            _Excel._Worksheet oSheetRegnskab_puls3060 = null;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            string rec_regnskab_Eksportmappe = @"%userprofile%\Documents\SummaSummarum\"; // work
            rec_regnskab_Eksportmappe = Environment.ExpandEnvironmentVariables(rec_regnskab_Eksportmappe);
            string SaveAs = rec_regnskab_Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".xlsx";

            var JournalPoster = from h in karGLTrans
                                join d1 in karGLAccount on h._Account equals d1._Account into details1
                                from x1 in details1.DefaultIfEmpty()
                                join d2 in karDebCred on h._DCAccount equals d2._Account into details2
                                from x2 in details2.DefaultIfEmpty(new RecDebCred() { _Account = null, _Name = null })
                                orderby h._JournalPostedId, h._Voucher, h._VoucherLine
                                select new clsJournalposter
                                {
                                    ds = DS(x1.AccountTypeEnum),
                                    k = IUAP(x1.AccountTypeEnum),
                                    Konto = h._Account + "-" + x1._Name,
                                    DebKrd = x2._Name,
                                    //DebKrd = h._DCAccount,
                                    Dato = h._Date,
                                    Klade = h._JournalPostedId,
                                    Serie = h._NumberSerie,
                                    Bilag = h._Voucher,
                                    Linie = h._VoucherLine,
                                    Tekst = h._Text,
                                    Beløb = h._Amount,
                                };
            var count = JournalPoster.Count();
            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new _Excel.Application();
                    oXL.Visible = true;
                    //oXL.Visible = true; //For debug

                    //Get a new workbook.
                    oWB = oXL.Workbooks.Add((Missing.Value));

                    oSheetPoster = (_Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheetPoster.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    ////////////////////////////////////////////////////////////////////////

                    oSheetPoster.Name = "Poster";

                    int row = 1;
                    foreach (clsJournalposter m in JournalPoster)
                    {
                        row++;
                        //if (row > 500) break; //<----------------------------------------------
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
                                    if (tp.ToString() == "Trans2SummaHDC.Fieldattr")
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

                    string BottomRight = "E" + row.ToString();          //<------------------HUSK
                    oRng = oSheetPoster.get_Range("E2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheetPoster.ListObjects.AddEx(_Excel.XlListObjectSourceType.xlSrcRange, oSheetPoster.UsedRange, System.Type.Missing, _Excel.XlYesNoGuess.xlYes).Name = "PosterList";
                    oSheetPoster.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.FreezePanes = true;

                    oSheetPoster.get_Range("A1", Missing.Value).Select();


                    oSheetRegnskab = (_Excel._Worksheet)oWB.Worksheets.Add(oSheetPoster, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                    //oXL.Visible = true; //For debug

                    _Excel.Range x1 = oSheetPoster.Cells[1, 1];
                    _Excel.Range x2 = oSheetPoster.Cells[row, 11]; //<--------------------HUSK
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

                    oSheetRegnskab.Cells[2, 3] = "Regnskab Hafsjold Data Consult";
                    oRng = oSheetRegnskab.get_Range("D3", Missing.Value);
                    oRng.Select();
                    bool[] Periods = { false, false, false, false, true, false, false };
                    oRng.Group(true, true, Missing.Value, Periods);

                    oRng = oSheetRegnskab.get_Range("D4", "P4");
                    oRng.HorizontalAlignment = _Excel.XlHAlign.xlHAlignRight;

                    oSheetRegnskab.PageSetup.LeftHeader = "&14Regnskab Hafsjold Data Consult";
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

                    oSheetRegnskab_puls3060 = oSheetRegnskab;
                    oSheetRegnskab.get_Range("A1", Missing.Value).Select();

                    //////////////////////////////////////////////////////////////////////////////////////////////
                    oSheetRegnskab_puls3060.Activate();
                    oSheetRegnskab_puls3060.get_Range("A1", Missing.Value).Select();

                    oWB.SaveAs(SaveAs, _Excel.XlFileFormat.xlWorkbookDefault, "", "", false, false, _Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;


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
    }

    public class clsJournalposter
    {
        [Fieldattr(Heading = "ds")]
        public string ds { get; set; }
        [Fieldattr(Heading = "k")]
        public string k { get; set; }
        [Fieldattr(Heading = "Konto")]
        public string Konto { get; set; }
        [Fieldattr(Heading = "Deb/Krd")]
        public string DebKrd { get; set; }
        [Fieldattr(Heading = "Dato")]
        public DateTime? Dato { get; set; }
        [Fieldattr(Heading = "Klade")]
        public int? Klade { get; set; }
        [Fieldattr(Heading = "Serie")]
        public string Serie { get; set; }
        [Fieldattr(Heading = "Bilag")]
        public int? Bilag { get; set; }
        [Fieldattr(Heading = "Linie")]
        public int? Linie { get; set; }
        [Fieldattr(Heading = "Tekst")]
        public string Tekst { get; set; }
        [Fieldattr(Heading = "Beløb")]
        public double? Beløb { get; set; }
    }

    public class RecDebCred
    {
        public string _Account { get; set; }
        public string _Name { get; set; }
    }
    public class KarDebCred : List<RecDebCred>
    {

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

}
