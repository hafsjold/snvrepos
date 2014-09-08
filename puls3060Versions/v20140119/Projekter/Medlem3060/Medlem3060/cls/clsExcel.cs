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
using System.Net;
using System.Net.Mail;
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

            var MedlemmerAll = from h in Program.dbData3060.tblMedlems
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
                                   Kon = h.Kon.ToString(),
                                   FodtDato = h.FodtDato,
                                   Bank = h.Bank,
                                   erMedlem = ((bool)Program.dbData3060.erMedlem(h.Nr)) ? 1 : 0,
                                   indmeldelsesDato = Program.dbData3060.indmeldtdato(h.Nr),
                                   udmeldelsesDato = Program.dbData3060.udmeldtdato(h.Nr),
                                   kontingentBetaltTilDato = Program.dbData3060.kontingentdato(h.Nr),
                                   opkrævningsDato = Program.dbData3060.forfaldsdato(h.Nr),
                                   kontingentTilbageførtDato = Program.dbData3060.tilbageførtkontingentdato(h.Nr),
                               };


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.MainformProgressBar.Value = 0;
                    this.MainformProgressBar.Minimum = 0;
                    this.MainformProgressBar.Maximum = (from h in Program.dbData3060.tblMedlems select h).Count();
                    this.MainformProgressBar.Step = 1;
                    this.MainformProgressBar.Visible = true;
                    foreach (clsMedlemInternAll m in MedlemmerAll)
                    {
                        this.MainformProgressBar.PerformStep();
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

                    string BottomRight = "J" + row.ToString();
                    oRng = oSheet.get_Range("J2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    BottomRight = "R" + row.ToString();
                    oRng = oSheet.get_Range("M2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheet.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.SplitColumn = 2;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    for (var i = oWB.Worksheets.Count; i > 0; i--)
                    {
                        Excel._Worksheet oSheetWrk = (Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemIntern")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    
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

        [Flags]
        public enum statusMedlem
        {
            Medlem = 0x1,
            NytMedlem = 0x2,
            Restance = 0x4,
            Rykket = 0x8,
            Pusterummet = 0x10
        }        
        private string GetStatusText(int? Status) 
        {
            if (Status == null) return "";
            statusMedlem e = (statusMedlem)Status;
            string s;

            if ((e & statusMedlem.Medlem) == statusMedlem.Medlem)
                s = "Medlem";
            else if ((e & statusMedlem.NytMedlem) == statusMedlem.NytMedlem)
                s = "NytMedlem";
            else
                s = "IkkeMedlem";

            if ((e & statusMedlem.Pusterummet) == statusMedlem.Pusterummet)
                s += " gennem Pusterummet";	

            if ((e & statusMedlem.Restance) == statusMedlem.Restance)
                s += " i Restance";

            if ((e & statusMedlem.Rykket) == statusMedlem.Rykket)
                s += " er Rykket";
           
            return s;
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

            var MedlemmerAll = from h in Program.dbData3060.tblMedlems
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
                                   Kon = h.Kon.ToString(),
                                   FodtDato = h.FodtDato,
                                   stStatus = GetStatusText(h.Status)
                               };


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.MainformProgressBar.Value = 0;
                    this.MainformProgressBar.Minimum = 0;
                    this.MainformProgressBar.Maximum = (from h in Program.dbData3060.tblMedlems select h).Count();
                    this.MainformProgressBar.Step = 1;
                    this.MainformProgressBar.Visible = true;
                    foreach (clsMedlemExternAll m in MedlemmerAll)
                    {
                        this.MainformProgressBar.PerformStep();
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

                    string BottomRight = "J" + row.ToString();
                    oRng = oSheet.get_Range("J2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheet.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.SplitColumn = 2;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    for (var i = oWB.Worksheets.Count; i > 0; i--)
                    {
                        Excel._Worksheet oSheetWrk = (Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemEkstern")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    this.emailExcelFile(SaveAs, "Puls3060 Medlems-oplysninger");

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

        private void excelManagement()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "MedlemManagement";

            Excel.Application oXL = null; ;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Window oWindow;
            Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";

            var MedlemmerAll = from h in Program.dbData3060.tblMedlems
                               where ((((statusMedlem)(h.Status)) & statusMedlem.Medlem) == statusMedlem.Medlem)
                               || ((((statusMedlem)(h.Status)) & statusMedlem.NytMedlem) == statusMedlem.NytMedlem)
                               orderby h.Status, h.Kaldenavn
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
                                   Kon = h.Kon.ToString(),
                                   FodtDato = h.FodtDato,
                                   stStatus = GetStatusText(h.Status)
                               };


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.MainformProgressBar.Value = 0;
                    this.MainformProgressBar.Minimum = 0;
                    this.MainformProgressBar.Maximum = (from h in Program.dbData3060.tblMedlems select h).Count();
                    this.MainformProgressBar.Step = 1;
                    this.MainformProgressBar.Visible = true;
                    foreach (clsMedlemExternAll m in MedlemmerAll)
                    {
                        this.MainformProgressBar.PerformStep();
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

                    string BottomRight = "J" + row.ToString();
                    oRng = oSheet.get_Range("J2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheet.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.SplitColumn = 2;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    for (var i = oWB.Worksheets.Count; i > 0; i--)
                    {
                        Excel._Worksheet oSheetWrk = (Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemManagement")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    this.emailExcelFile(SaveAs, "Puls3060 Medlems-oplysninger");

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

        private void excelNotPBS()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "MedlemNotPBS";

            Excel.Application oXL = null; ;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Window oWindow;
            Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";

            var MedlemmerAll = from h in Program.dbData3060.tblMedlems
                               select new clsMedlemNotPBSAll
                               {
                                   Nr = h.Nr,
                                   Navn = h.Navn,
                                   Kaldenavn = h.Kaldenavn,
                                   Adresse = h.Adresse,
                                   Postnr = h.Postnr,
                                   Bynavn = h.Bynavn,
                                   Telefon = h.Telefon,
                                   Email = h.Email,
                                   Kon = h.Kon.ToString(),
                                   FodtDato = h.FodtDato,
                                   erMedlem = ((bool)Program.dbData3060.erMedlem(h.Nr)) ? 1 : 0,
                                   erPBS = ((bool)Program.dbData3060.erPBS(h.Nr)) ? 1 : 0,
                               };
            
            var MedlemmerNotPBS = from h in MedlemmerAll
                               where h.erMedlem == 1 && h.erPBS == 0
                               select new clsMedlemNotPBS
                                {
                                    Nr = h.Nr,
                                    Navn = h.Navn,
                                    Kaldenavn = h.Kaldenavn,
                                    Adresse = h.Adresse,
                                    Postnr = h.Postnr,
                                    Bynavn = h.Bynavn,
                                    Telefon = h.Telefon,
                                    Email = h.Email,
                                    Kon = h.Kon,
                                    PBSnr = "03985644",
                                    Debgrnr = "00001",
                                    Kundenr = 032001610000000 + (int)h.Nr
                                };

            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.MainformProgressBar.Value = 0;
                    this.MainformProgressBar.Minimum = 0;
                    this.MainformProgressBar.Maximum = (from h in Program.dbData3060.tblMedlems select h).Count();
                    this.MainformProgressBar.Step = 1;
                    this.MainformProgressBar.Visible = true;
                    foreach (clsMedlemNotPBS m in MedlemmerNotPBS)
                    {
                        this.MainformProgressBar.PerformStep();
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

                    string BottomRight = "L" + row.ToString();
                    oRng = oSheet.get_Range("L2", BottomRight);
                    oRng.NumberFormat = "##############";

                    oSheet.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.SplitColumn = 2;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    for (var i = oWB.Worksheets.Count; i > 0; i--)
                    {
                        Excel._Worksheet oSheetWrk = (Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemNotPBS")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    this.emailExcelFile(SaveAs, "Puls3060 Medlemmer ikke tilmeldt PBS");

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
            Excel._Worksheet oSheetPoster;
            Excel._Worksheet oSheetRegnskab;
            Excel.Window oWindow;
            Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";


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
                                   Beløb = h.Bruttobeløb,
                               };

            var erMedlem = from h in Program.dbData3060.tblMedlems
                           where h.Status == 1
                           select h;


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new Excel.Application();
                    oXL.Visible = true;
                    //oXL.Visible = true; //For debug

                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheetPoster = (Excel._Worksheet)oWB.ActiveSheet;
                    oWindow = oXL.ActiveWindow;

                    if (pSheetName.Length > 0) oSheetPoster.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
                    int row = 1;
                    this.MainformProgressBar.Value = 0;
                    this.MainformProgressBar.Minimum = 0;
                    this.MainformProgressBar.Maximum = (from h in Program.karPosteringer select h).Count();
                    this.MainformProgressBar.Step = 1;
                    this.MainformProgressBar.Visible = true;
                    foreach (clsJournalposter m in JournalPoster)
                    {
                        this.MainformProgressBar.PerformStep();
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
                                    if (tp.ToString() == "nsPuls3060.Fieldattr")
                                    {
                                        Fieldattr attr = (Fieldattr)att;
                                        string heading = attr.Heading;
                                        oSheetPoster.Cells[1, col] = heading;

                                    }
                                }
                            }
                        }
                    }

                    oRng = (Excel.Range)oSheetPoster.Rows[1, Missing.Value];
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
                    oRng = oSheetPoster.get_Range("D2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheetPoster.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.FreezePanes = true;

                    oSheetPoster.get_Range("A1", Missing.Value).Select();


                    oSheetRegnskab = (Excel._Worksheet)oWB.Worksheets.Add(System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                    oRng = oSheetRegnskab.get_Range("C2", Missing.Value);
                    oRng.Formula = "Antal medlemmer: " + erMedlem.Count().ToString();

                    //oXL.Visible = true; //For debug

                    PivotField _pvtField = null;
                    PivotTable _pivot = oSheetPoster.PivotTableWizard(
                        XlPivotTableSourceType.xlDatabase,  //SourceType
                        oSheetPoster.get_Range(oSheetPoster.Cells[1, 1], oSheetPoster.Cells[row, 9]), //SourceData
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

                    oSheetRegnskab.Name = "Regnskab";
                    oRng = oSheetRegnskab.get_Range("D3", Missing.Value);
                    oRng.Select();
                    bool[] Periods = { false, false, false, false, true, false, false};
                    oRng.Group(true, true, Missing.Value, Periods);

                    oRng = oSheetRegnskab.get_Range("D4", "P4");
                    oRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    oSheetRegnskab.PageSetup.LeftHeader = "&14Regnskab Puls 3060";
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
                    oSheetRegnskab.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                    oSheetRegnskab.PageSetup.Draft = false;
                    oSheetRegnskab.PageSetup.PaperSize = XlPaperSize.xlPaperA4;
                    oSheetRegnskab.PageSetup.FirstPageNumber = 1;
                    oSheetRegnskab.PageSetup.Order = XlOrder.xlDownThenOver;
                    oSheetRegnskab.PageSetup.BlackAndWhite = false;
                    oSheetRegnskab.PageSetup.Zoom = 100;
                    oSheetRegnskab.PageSetup.PrintErrors = XlPrintErrors.xlPrintErrorsDisplayed;
                    
                    oWB.ShowPivotTableFieldList = false;

                    for (var i = oWB.Worksheets.Count; i > 0; i--)
                    {
                        Excel._Worksheet oSheetWrk = (Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if ((oSheetWrk.Name != "Regnskab") && (oSheetWrk.Name != "Poster")) 
                        {
                            oSheetWrk.Delete();
                        }
                    }

                    oSheetRegnskab.get_Range("A1", Missing.Value).Select();
                    
                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    this.emailExcelFile(SaveAs, "Puls3060 Regnskab");


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

        public void emailExcelFile(string filename, string PSubjectBody)
        {
            FileInfo f = new FileInfo(filename);
            string local_filename = f.Name;
            FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            string SmtpUsername = Program.dbData3060.GetSysinfo("SMTPUSER");
            string SmtpPassword = Program.dbData3060.GetSysinfo("SMTPPASSWD");
            var smtp = new SmtpClient
            {
                Host = Program.dbData3060.GetSysinfo("SMTPHOST"),
                Port = int.Parse(Program.dbData3060.GetSysinfo("SMTPPORT")),
                EnableSsl = bool.Parse(Program.dbData3060.GetSysinfo("SMTPSSL")),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SmtpUsername, SmtpPassword)
            };

            MailMessage email = new MailMessage();
            email.Subject = PSubjectBody + ": " + local_filename;
            email.Body = PSubjectBody + ": " + local_filename;

            email.From = new MailAddress(Program.dbData3060.GetSysinfo("MAILFROM"));
            email.ReplyToList.Add( new MailAddress(Program.dbData3060.GetSysinfo("MAILREPLY")));
            email.Bcc.Add(new MailAddress(Program.dbData3060.GetSysinfo("MAILTOADDR"), Program.dbData3060.GetSysinfo("MAILTONAME")));
            email.Attachments.Add(new Attachment(fs, local_filename, "application/vnd.ms-excel"));
            smtp.Send(email);
        }
    }
}
