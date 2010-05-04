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
using Excel;

namespace nsPuls3060
{
    public partial class FrmMain : Form
    {
        private void excelInternt()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "MedlemIntern";

            Excel.Application oXL = null; ;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Window oWindow;
            Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";

            var MedlemmerAll = from h in Program.karMedlemmer
                               join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                               from x in details1.DefaultIfEmpty()  //new TblMedlem { Nr = -1, Knr = -1, Kon = "X", FodtDato = new DateTime(1900, 1, 1) })
                               select new clsMedlemInternAll
                               {
                                   Nr = h.Nr,
                                   Navn = h.Navn,
                                   Kaldenavn = h.Kaldenavn,
                                   Adresse = h.Adresse,
                                   Postnr = h.Postnr,
                                   Bynavn = h.Bynavn,
                                   Telefon = h.Telefon,
                                   Email = h.Email,
                                   Knr = x.Knr,
                                   Kon = x.Kon,
                                   FodtDato = x.FodtDato,
                                   erMedlem = (h.erMedlem()) ? 1 : 0,
                                   indmeldelsesDato = h.indmeldelsesDato,
                                   udmeldelsesDato = h.udmeldelsesDato,
                                   kontingentBetaltTilDato = h.kontingentBetaltTilDato,
                                   opkrævningsDato = h.opkrævningsDato,
                                   kontingentTilbageførtDato = h.kontingentTilbageførtDato,
                               };


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = false;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.toolStripProgressBar1.Value = 0;
                    this.toolStripProgressBar1.Minimum = 0;
                    this.toolStripProgressBar1.Maximum = (from h in Program.karMedlemmer select h).Count();
                    this.toolStripProgressBar1.Step = 1;
                    this.toolStripProgressBar1.Visible = true;
                    foreach (clsMedlemInternAll m in MedlemmerAll)
                    {
                        this.toolStripProgressBar1.PerformStep();
                        row++;
                        Type objectType = m.GetType();
                        PropertyInfo[] properties = objectType.GetProperties();
                        int col = 0;
                        foreach (PropertyInfo property in properties)
                        {
                            col++;
                            string Name = property.Name;
                            //string NamePropertyType = property.GetValue(m, null).GetType().ToString();
                            oSheet.Cells[row, col] = property.GetValue(m, null);
                            if (row == 2)
                            {
                                object[] CustomAttributes = property.GetCustomAttributes(false);
                                foreach (var att in CustomAttributes)
                                {
                                    Type tp = att.GetType();
                                    if (tp.ToString() == "nsPuls3060.Fieldattr")
                                    {
                                        Fieldattr attr = (Fieldattr)att;
                                        string heading = attr.Heading;
                                        oSheet.Cells[1, col] = heading;

                                    }
                                }
                            }
                        }
                    }
                    oRng = (Excel.Range)oSheet.Rows[1, Missing.Value];
                    oRng.Font.Name = "Arial";
                    oRng.Font.Size = 12;
                    oRng.Font.Strikethrough = false;
                    oRng.Font.Superscript = false;
                    oRng.Font.Subscript = false;
                    oRng.Font.OutlineFont = false;
                    oRng.Font.Shadow = false;
                    oRng.Font.Bold = true;
                    oRng.HorizontalAlignment = Excel.Constants.xlCenter;
                    oRng.VerticalAlignment = Excel.Constants.xlBottom;
                    oRng.WrapText = false;
                    oRng.Orientation = 0;
                    oRng.AddIndent = false;
                    oRng.IndentLevel = 0;
                    oRng.ShrinkToFit = false;
                    oRng.MergeCells = false;

                    oRng = oSheet.get_Range("K2", "K1024");
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oRng = oSheet.get_Range("M2", "Q1024");
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheet.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.SplitColumn = 2;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.toolStripProgressBar1.Visible = false;


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

        private void excelExternt()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "MedlemEkstern";

            Excel.Application oXL = null; ;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Window oWindow;
            Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";

            var MedlemmerAll = from h in Program.karMedlemmer
                               join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                               from x in details1.DefaultIfEmpty()  //new TblMedlem { Nr = -1, Knr = -1, Kon = "X", FodtDato = new DateTime(1900, 1, 1) })
                               select new clsMedlemExternAll
                               {
                                   Nr = h.Nr,
                                   Navn = h.Navn,
                                   Kaldenavn = h.Kaldenavn,
                                   Adresse = h.Adresse,
                                   Postnr = h.Postnr,
                                   Bynavn = h.Bynavn,
                                   Telefon = h.Telefon,
                                   Email = h.Email,
                                   Kon = x.Kon,
                                   FodtDato = x.FodtDato,
                                   erMedlem = (h.erMedlem()) ? 1 : 0,
                               };


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = false;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.toolStripProgressBar1.Value = 0;
                    this.toolStripProgressBar1.Minimum = 0;
                    this.toolStripProgressBar1.Maximum = (from h in Program.karMedlemmer select h).Count();
                    this.toolStripProgressBar1.Step = 1;
                    this.toolStripProgressBar1.Visible = true;
                    foreach (clsMedlemExternAll m in MedlemmerAll)
                    {
                        this.toolStripProgressBar1.PerformStep();
                        row++;
                        Type objectType = m.GetType();
                        PropertyInfo[] properties = objectType.GetProperties();
                        int col = 0;
                        foreach (PropertyInfo property in properties)
                        {
                            col++;
                            string Name = property.Name;
                            //string NamePropertyType = property.GetValue(m, null).GetType().ToString();
                            oSheet.Cells[row, col] = property.GetValue(m, null);
                            if (row == 2)
                            {
                                object[] CustomAttributes = property.GetCustomAttributes(false);
                                foreach (var att in CustomAttributes)
                                {
                                    Type tp = att.GetType();
                                    if (tp.ToString() == "nsPuls3060.Fieldattr")
                                    {
                                        Fieldattr attr = (Fieldattr)att;
                                        string heading = attr.Heading;
                                        oSheet.Cells[1, col] = heading;

                                    }
                                }
                            }
                        }
                    }
                    oRng = (Excel.Range)oSheet.Rows[1, Missing.Value];
                    oRng.Font.Name = "Arial";
                    oRng.Font.Size = 12;
                    oRng.Font.Strikethrough = false;
                    oRng.Font.Superscript = false;
                    oRng.Font.Subscript = false;
                    oRng.Font.OutlineFont = false;
                    oRng.Font.Shadow = false;
                    oRng.Font.Bold = true;
                    oRng.HorizontalAlignment = Excel.Constants.xlCenter;
                    oRng.VerticalAlignment = Excel.Constants.xlBottom;
                    oRng.WrapText = false;
                    oRng.Orientation = 0;
                    oRng.AddIndent = false;
                    oRng.IndentLevel = 0;
                    oRng.ShrinkToFit = false;
                    oRng.MergeCells = false;

                    oRng = oSheet.get_Range("J2", "J1024");
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheet.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.SplitColumn = 2;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.toolStripProgressBar1.Visible = false;


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

        private void ecxelPoster()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "Poster";

            Excel.Application oXL = null; ;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel._Worksheet oSheet2;
            Excel._Worksheet oSheet3;
            Excel.Window oWindow;
            Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";


            var MedlemmerAll = from h in Program.karPosteringsjournal
                               join d1 in Program.karKontoplan on h.Konto equals d1.Kontonr into details1
                               from x in details1.DefaultIfEmpty()
                               select new clsJournalposter
                               {
                                   ds = (x.Type == "Drift") ? "D" : "S",
                                   k = IUAP(x.Type, x.DK),
                                   Konto = h.Konto.ToString() + "-" + h.Kontonavn,
                                   Dato = h.Dato,
                                   Bilag = h.Bilag,
                                   Nr = h.Nr,
                                   Id = h.Id,
                                   Tekst = h.Tekst,
                                   Beløb = h.Bruttobeløb,
                               };


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = false;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.toolStripProgressBar1.Value = 0;
                    this.toolStripProgressBar1.Minimum = 0;
                    this.toolStripProgressBar1.Maximum = (from h in Program.karMedlemmer select h).Count();
                    this.toolStripProgressBar1.Step = 1;
                    this.toolStripProgressBar1.Visible = true;
                    foreach (clsJournalposter m in MedlemmerAll)
                    {
                        this.toolStripProgressBar1.PerformStep();
                        row++;
                        Type objectType = m.GetType();
                        PropertyInfo[] properties = objectType.GetProperties();
                        int col = 0;
                        foreach (PropertyInfo property in properties)
                        {
                            col++;
                            string Name = property.Name;
                            //string NamePropertyType = property.GetValue(m, null).GetType().ToString();
                            oSheet.Cells[row, col] = property.GetValue(m, null);
                            if (row == 2)
                            {
                                object[] CustomAttributes = property.GetCustomAttributes(false);
                                foreach (var att in CustomAttributes)
                                {
                                    Type tp = att.GetType();
                                    if (tp.ToString() == "nsPuls3060.Fieldattr")
                                    {
                                        Fieldattr attr = (Fieldattr)att;
                                        string heading = attr.Heading;
                                        oSheet.Cells[1, col] = heading;

                                    }
                                }
                            }
                        }
                    }
                    oRng = (Excel.Range)oSheet.Rows[1, Missing.Value];
                    oRng.Font.Name = "Arial";
                    oRng.Font.Size = 12;
                    oRng.Font.Strikethrough = false;
                    oRng.Font.Superscript = false;
                    oRng.Font.Subscript = false;
                    oRng.Font.OutlineFont = false;
                    oRng.Font.Shadow = false;
                    oRng.Font.Bold = true;
                    oRng.HorizontalAlignment = Excel.Constants.xlCenter;
                    oRng.VerticalAlignment = Excel.Constants.xlBottom;
                    oRng.WrapText = false;
                    oRng.Orientation = 0;
                    oRng.AddIndent = false;
                    oRng.IndentLevel = 0;
                    oRng.ShrinkToFit = false;
                    oRng.MergeCells = false;

                    string BottomRight = "D" + row.ToString();
                    oRng = oSheet.get_Range("D2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheet.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    oXL.Visible = true;

                    PivotField _pvtField = null;
                    PivotTable _pivot = oSheet.PivotTableWizard(XlPivotTableSourceType.xlDatabase, 
                        oSheet.get_Range(oSheet.Cells[1, 1], oSheet.Cells[row, 9]),
                        System.Type.Missing, "PivotTable1", System.Type.Missing, System.Type.Missing, System.Type.Missing,
                        System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing,
                        System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                    
                    _pvtField = (PivotField)_pivot.PivotFields("ds");
                    _pvtField.Orientation = XlPivotFieldOrientation.xlRowField;
                    
                    _pvtField = (PivotField)_pivot.PivotFields("k");
                    _pvtField.Orientation = XlPivotFieldOrientation.xlRowField;
                    
                    _pvtField = (PivotField)_pivot.PivotFields("Konto");
                    _pvtField.Orientation = XlPivotFieldOrientation.xlRowField;
                    
                    _pvtField = (PivotField)_pivot.PivotFields("Dato");
                    _pvtField.Orientation = XlPivotFieldOrientation.xlColumnField;
                    
                    _pvtField = (PivotField)_pivot.PivotFields("Beløb");
                    _pvtField.Orientation = XlPivotFieldOrientation.xlDataField;
                    _pvtField.Function = XlConsolidationFunction.xlSum;
                    _pvtField.NumberFormat = "#,##0";

                    oSheet2 = (Excel._Worksheet)oWB.ActiveSheet;
                    oSheet2.Name = "Regnskab";
                    oRng = oSheet2.get_Range("D1", Missing.Value);
                    oRng.Select();
                    bool[] Periods = { false, false, false, false, true, false, false};
                    oRng.Group(true, true, Missing.Value, Periods);

                    oRng = oSheet2.get_Range("D2", "P2");
                    oRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    oWB.ShowPivotTableFieldList = false;

                    oSheet3 = (Excel._Worksheet)oWB.Worksheets.get_Item("Sheet2");
                    oSheet3.Delete();
                    oSheet3 = (Excel._Worksheet)oWB.Worksheets.get_Item("Sheet3");
                    oSheet3.Delete();

                    oSheet2.get_Range("A1", Missing.Value).Select();
                    
                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.toolStripProgressBar1.Visible = false;

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
}
