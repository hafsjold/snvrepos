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
using _Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Net.Imap;
using nsPbs3060;

namespace nsPuls3060
{
    public partial class FrmMain : Form
    {


        private void excelInternt()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "MedlemIntern";

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheet;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".xlsx";

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
                    oXL = new _Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (_Excel._Worksheet)oWB.ActiveSheet;
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
                    oRng = (_Excel.Range)oSheet.Rows[1, Missing.Value];
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
                        _Excel._Worksheet oSheetWrk = (_Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemIntern")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, _Excel.XlFileFormat.xlWorkbookDefault, "", "", false, false, _Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheet;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".xlsx";

            var MedlemmerAll = from h in Program.dbData3060.tblMedlems
                               select new clsMedlemExternAll
                               {
                                   Nr = h.Nr,
                                   Navn = h.Navn,
                                   Adresse = h.Adresse,
                                   Postnr = h.Postnr,
                                   Bynavn = h.Bynavn,
                                   Telefon = h.Telefon,
                                   Email = h.Email,
                                   Kon = h.Kon.ToString(),
                                   FodtAar = h.FodtDato.ToString(),
                               };


            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new _Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (_Excel._Worksheet)oWB.ActiveSheet;
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
                    oRng = (_Excel.Range)oSheet.Rows[1, Missing.Value];
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
                        _Excel._Worksheet oSheetWrk = (_Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemEkstern")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, _Excel.XlFileFormat.xlWorkbookDefault, "", "", false, false, _Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    this.imapSaveExcelFile(SaveAs, "Puls3060 Medlems-oplysninger");

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

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheet;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".xlsx";
            
            puls3060_dkEntities jdb = new puls3060_dkEntities(true);

            var qry = from u in jdb.ecpwt_users
                      join m in jdb.ecpwt_rsmembership_membership_subscribers on u.id equals m.user_id
                      where m.membership_id == 6 && m.status == 0
                      join t in jdb.ecpwt_rsmembership_transactions on m.last_transaction_id equals t.id
                      join a in jdb.ecpwt_rsmembership_subscribers on m.user_id equals a.user_id
                      orderby u.name
                      select new
                      {
                          u.id,
                          u.name,
                          a.f1,
                          a.f4,
                          a.f2,
                          a.f6,
                          u.email,
                          a.f14,
                          m.membership_start,
                          m.membership_end,
                          t.user_data
                      };

            List<clsMedlemExternAll> MedlemmerAll = new List<clsMedlemExternAll>();
            foreach (var m in qry)
            {

                User_data recud = clsHelper.unpack_UserData(m.user_data);
                clsMedlemExternAll recMedlem = new clsMedlemExternAll
                {
                    Nr = m.id,
                    Navn = m.name,
                    Adresse = m.f1,
                    Postnr = m.f4,
                    Bynavn = m.f2,
                    Telefon = m.f6,
                    Email = m.email,
                    Kon = recud.kon,
                    FodtAar = recud.fodtaar,
                    MedlemTil = m.membership_end
                };
                MedlemmerAll.Add(recMedlem);
            }            

            using (new ExcelUILanguageHelper())
            {
                try
                {
                    //Start Excel and get Application object.
                    oXL = new _Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (_Excel._Worksheet)oWB.ActiveSheet;
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
                    oRng = (_Excel.Range)oSheet.Rows[1, Missing.Value];
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
                        _Excel._Worksheet oSheetWrk = (_Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemManagement")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, _Excel.XlFileFormat.xlWorkbookDefault, "", "", false, false, _Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    this.imapSaveExcelFile(SaveAs, "Puls3060 Medlems-oplysninger");

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

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheet;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".xlsx";

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
                    oXL = new _Excel.Application();
                    oXL.Visible = true;
                    //Get a new workbook.

                    oWB = oXL.Workbooks.Add((Missing.Value));
                    oSheet = (_Excel._Worksheet)oWB.ActiveSheet;
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
                    oRng = (_Excel.Range)oSheet.Rows[1, Missing.Value];
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
                        _Excel._Worksheet oSheetWrk = (_Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if (oSheetWrk.Name != "MedlemNotPBS")
                        {
                            oSheetWrk.Delete();
                        }
                    }


                    oWB.SaveAs(SaveAs, _Excel.XlFileFormat.xlWorkbookDefault, "", "", false, false, _Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    oWB.Saved = true;
                    oXL.Visible = true;
                    this.MainformProgressBar.Visible = false;

                    this.imapSaveExcelFile(SaveAs, "Puls3060 Medlemmer ikke tilmeldt PBS");

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

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheetPoster;
            _Excel._Worksheet oSheetRegnskab;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".xlsx";


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
                    bool[] Periods = { false, false, false, false, true, false, false };
                    oRng.Group(true, true, Missing.Value, Periods);

                    oRng = oSheetRegnskab.get_Range("D4", "P4");
                    oRng.HorizontalAlignment = _Excel.XlHAlign.xlHAlignRight;

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
                    this.MainformProgressBar.Visible = false;

                    this.imapSaveExcelFile(SaveAs, "Puls3060 Regnskab");


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

        private void mailSync()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "MedlemMailSync";
            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".csv";

            var MedlemmerAll = from h in Program.dbData3060.tblMedlems
                               where ((((statusMedlem)(h.Status)) & statusMedlem.Medlem) == statusMedlem.Medlem)
                               || ((((statusMedlem)(h.Status)) & statusMedlem.NytMedlem) == statusMedlem.NytMedlem)
                               orderby h.Nr
                               select new clsMedlemMailSync
                               {
                                   Email = h.Email,
                                   Navn = h.Navn,
                                   stKey = String.Format("Medlem-{0:#0000}", h.Nr),
                                   stSource = "sync_" + pReadDate.ToString("yyyyMMdd_HHmmss")
                               };

            try
            {
                List<string> list = new List<string>();
                string heading = "";
                char[] simikolon = {';'};

                int row = 0;
                foreach (clsMedlemMailSync m in MedlemmerAll)
                {
                    string line = "";
                    row++;
                    Type objectType = m.GetType();
                    PropertyInfo[] properties = objectType.GetProperties();
                    int col = 0;
                    foreach (PropertyInfo property in properties)
                    {
                        col++;
                        if (row == 1)
                        {
                            object[] CustomAttributes = property.GetCustomAttributes(false);
                            foreach (var att in CustomAttributes)
                            {
                                Type tp = att.GetType();
                                if (tp.ToString() == "nsPuls3060.Fieldattr")
                                {
                                    Fieldattr attr = (Fieldattr)att;
                                    heading += @"""" + attr.Heading.ToString().TrimEnd() + @""";";
                                }
                            }
                        }
                        string Name = property.Name;
                        line += @"""" + property.GetValue(m, null).ToString().TrimEnd() + @""";";
                    }
                    if (row == 1) list.Add(heading.TrimEnd(simikolon));
                    list.Add(line.TrimEnd(simikolon));
                }

                FileStream ts = new FileStream(SaveAs, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
                {
                    foreach (var ln in list)
                    {
                         sr.WriteLine(ln);
                    }
                }

                this.imapSaveExcelFile(SaveAs, "Puls3060 Medlem MailSync");
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
        public void emailExcelFile(string filename, string PSubjectBody)
        {
            FileInfo f = new FileInfo(filename);
            string local_filename = f.Name;
            FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            string SmtpUsername = Program.dbData3060.GetSysinfo("SMTPUSER");
            string SmtpPassword = Program.dbData3060.GetSysinfo("SMTPPASSWD");
            var smtp = new System.Net.Mail.SmtpClient
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
            email.ReplyToList.Add(new MailAddress(Program.dbData3060.GetSysinfo("MAILREPLY")));
            email.Bcc.Add(new MailAddress(Program.dbData3060.GetSysinfo("MAILTOADDR"), Program.dbData3060.GetSysinfo("MAILTONAME")));
            email.Attachments.Add(new Attachment(fs, local_filename, "application/vnd.ms-excel"));
            smtp.Send(email);
        }

        public void imapSaveExcelFile(string filename, string PSubjectBody)
        {
            FileInfo f = new FileInfo(filename);
            string local_filename = f.Name;
            FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            MimeMessage message = new MimeMessage();
            TextPart body;

            message.To.Add(new MailboxAddress(@"regnskab@puls3060.dk", @"Regnskab Puls3060"));
            message.From.Add(new MailboxAddress(@"regnskab@puls3060.dk", @"Regnskab Puls3060"));
            message.Subject = PSubjectBody + ": " + local_filename;
            body = new TextPart("plain") { Text = PSubjectBody + ": " + local_filename };


            var attachment = new MimePart("application", "vnd.ms-excel")
            {
                ContentObject = new ContentObject(fs, ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = local_filename
            };

            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;

            using (var client = new ImapClient())
            {
                client.Connect("imap.gigahost.dk", 993, true);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                client.Authenticate(@"regnskab@puls3060.dk", "1234West+");

                var PBS = client.GetFolder("INBOX.PBS");
                PBS.Open(FolderAccess.ReadWrite);
                PBS.Append(message);
                PBS.Close();

                client.Disconnect(true);
            }

        }
    }

    public class clsMedlemInternAll
    {
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Navn")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "Kaldenavn")]
        public string Kaldenavn { get; set; }
        [Fieldattr(Heading = "Adresse")]
        public string Adresse { get; set; }
        [Fieldattr(Heading = "Postnr")]
        public string Postnr { get; set; }
        [Fieldattr(Heading = "By")]
        public string Bynavn { get; set; }
        [Fieldattr(Heading = "Email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "Telefon")]
        public string Telefon { get; set; }
        [Fieldattr(Heading = "Køn")]
        public string Kon { get; set; }
        [Fieldattr(Heading = "Født")]
        public DateTime? FodtDato { get; set; }
        [Fieldattr(Heading = "Bank")]
        public string Bank { get; set; }
        [Fieldattr(Heading = "erMedlem")]
        public int? erMedlem { get; set; }
        [Fieldattr(Heading = "Kontingent")]
        public DateTime? kontingentBetaltTilDato { get; set; }
        [Fieldattr(Heading = "Indmeldt")]
        public DateTime? indmeldelsesDato { get; set; }
        [Fieldattr(Heading = "Udmeldt")]
        public DateTime? udmeldelsesDato { get; set; }
        [Fieldattr(Heading = "Opkrævning")]
        public DateTime? opkrævningsDato { get; set; }
        [Fieldattr(Heading = "Tilbageført")]
        public DateTime? kontingentTilbageførtDato { get; set; }
        [Fieldattr(Heading = "erMedlemPusterummet")]
        public int? erMedlemPusterummet { get; set; }
    }

    public class clsMedlemNotPBSAll
    {
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Navn")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "Kaldenavn")]
        public string Kaldenavn { get; set; }
        [Fieldattr(Heading = "Adresse")]
        public string Adresse { get; set; }
        [Fieldattr(Heading = "Postnr")]
        public string Postnr { get; set; }
        [Fieldattr(Heading = "By")]
        public string Bynavn { get; set; }
        [Fieldattr(Heading = "Email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "Telefon")]
        public string Telefon { get; set; }
        [Fieldattr(Heading = "Køn")]
        public string Kon { get; set; }
        [Fieldattr(Heading = "Født")]
        public DateTime? FodtDato { get; set; }
        [Fieldattr(Heading = "erMedlem")]
        public int? erMedlem { get; set; }
        [Fieldattr(Heading = "tilmeldtPBS")]
        public int? erPBS { get; set; }
    }

    public class clsMedlemExternAll
    {
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Navn")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "Adresse")]
        public string Adresse { get; set; }
        [Fieldattr(Heading = "Postnr")]
        public string Postnr { get; set; }
        [Fieldattr(Heading = "By")]
        public string Bynavn { get; set; }
        [Fieldattr(Heading = "Email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "Telefon")]
        public string Telefon { get; set; }
        [Fieldattr(Heading = "Køn")]
        public string Kon { get; set; }
        [Fieldattr(Heading = "Født")]
        public string FodtAar { get; set; }
        [Fieldattr(Heading = "Medlem Til")]
        public DateTime? MedlemTil { get; set; }

    }

    public class clsMedlemNotPBS
    {
        [Fieldattr(Heading = "Nr")]
        public int? Nr { get; set; }
        [Fieldattr(Heading = "Navn")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "Kaldenavn")]
        public string Kaldenavn { get; set; }
        [Fieldattr(Heading = "Adresse")]
        public string Adresse { get; set; }
        [Fieldattr(Heading = "Postnr")]
        public string Postnr { get; set; }
        [Fieldattr(Heading = "By")]
        public string Bynavn { get; set; }
        [Fieldattr(Heading = "Email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "Telefon")]
        public string Telefon { get; set; }
        [Fieldattr(Heading = "Køn")]
        public string Kon { get; set; }
        [Fieldattr(Heading = "PBSnr")]
        public string PBSnr { get; set; }
        [Fieldattr(Heading = "Deb.gr.nr")]
        public string Debgrnr { get; set; }
        [Fieldattr(Heading = "Kundenr")]
        public Int64 Kundenr { get; set; }
    }

    public class clsMedlemMailSync
    {
        [Fieldattr(Heading = "email")]
        public string Email { get; set; }
        [Fieldattr(Heading = "name")]
        public string Navn { get; set; }
        [Fieldattr(Heading = "key")]
        public string stKey { get; set; }
        [Fieldattr(Heading = "source")]
        public string stSource { get; set; }
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
