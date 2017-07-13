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
using Uniconta.DataModel;
using Uniconta.Common;

namespace Medlem3060uc
{
    public partial class FrmMain : Form
    {
        private void excelManagement()
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "MedlemManagement";

            _Excel.Application oXL = null; ;
            _Excel._Workbook oWB;
            _Excel._Worksheet oSheet;
            _Excel.Window oWindow;
            _Excel.Range oRng;

            string rec_regnskab_Eksportmappe = @"%userprofile%\Documents\SummaSummarum"; // work
            rec_regnskab_Eksportmappe = Environment.ExpandEnvironmentVariables(rec_regnskab_Eksportmappe);
            string SaveAs = rec_regnskab_Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_HHmmss") + ".xlsx";

            puls3060_nyEntities jdb = new puls3060_nyEntities(true);

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
                                    if (tp.ToString() == "Medlem3060uc.Fieldattr")
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

        public void imapSaveExcelFile(string filename, string PSubjectBody)
        {
            FileInfo f = new FileInfo(filename);
            string local_filename = f.Name;
            FileStream fs = f.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            MimeMessage message = new MimeMessage();
            TextPart body;

            var To = new MailboxAddress( @"Regnskab Puls3060", @"regnskab@puls3060.dk");
            var From = new MailboxAddress(@"Regnskab Puls3060",@"regnskab@puls3060.dk");
            message.To.Add(To);
            message.From.Add(From);
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
                client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);

                var PBS = client.GetFolder("INBOX.PBS");
                PBS.Open(FolderAccess.ReadWrite);
                PBS.Append(message);
                PBS.Close();

                client.Disconnect(true);
            }

        }

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

        private void ecxelPoster()
        {
            var api = UCInitializer.GetBaseAPI;
            CompanyFinanceYear CurrentCompanyFinanceYear = null;
            var task1 = api.Query<CompanyFinanceYear>();
            task1.Wait();
            var cols1 = task1.Result;
            foreach (var col in cols1)
            {
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
            var task4 = api.Query<GLTrans>(null,crit);
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
                                    Udvalg = h._Dimension1,
                                    Aktivitet = h._Dimension2,
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

                    this.MainformProgressBar.Value = 0;
                    this.MainformProgressBar.Minimum = 0;
                    this.MainformProgressBar.Maximum = (from h in karGLTrans select h).Count();
                    this.MainformProgressBar.Step = 1;
                    this.MainformProgressBar.Visible = true;

                    ////////////////////////////////////////////////////////////////////////

                    oSheetPoster.Name = "Poster";

                    int row = 1;
                    foreach (clsJournalposter m in JournalPoster)
                    {
                        this.MainformProgressBar.PerformStep();
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
                                    if (tp.ToString() == "Medlem3060uc.Fieldattr")
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

                    string BottomRight = "G" + row.ToString();          //<------------------HUSK
                    oRng = oSheetPoster.get_Range("G2", BottomRight);
                    oRng.NumberFormat = "dd-mm-yyyy";

                    oSheetPoster.ListObjects.AddEx(_Excel.XlListObjectSourceType.xlSrcRange, oSheetPoster.UsedRange, System.Type.Missing, _Excel.XlYesNoGuess.xlYes).Name = "PosterList";
                    oSheetPoster.Cells.EntireColumn.AutoFit();

                    oWindow.SplitRow = 1;
                    oWindow.FreezePanes = true;

                    oSheetPoster.get_Range("A1", Missing.Value).Select();


                    oSheetRegnskab = (_Excel._Worksheet)oWB.Worksheets.Add(oSheetPoster, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                    //oXL.Visible = true; //For debug

                    _Excel.Range x1 = oSheetPoster.Cells[1, 1];
                    _Excel.Range x2 = oSheetPoster.Cells[row, 13]; //<--------------------HUSK
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

                    oSheetRegnskab.Cells[2, 3] = "Regnskab Puls 3060";
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

                    /*
                    for (var i = oWB.Worksheets.Count; i > 0; i--)
                    {
                        _Excel._Worksheet oSheetWrk = (_Excel._Worksheet)oWB.Worksheets.get_Item(i);
                        if ((oSheetWrk.Name != "Regnskab") && (oSheetWrk.Name != "Poster"))
                        {
                            oSheetWrk.Delete();
                        }
                    }
                    */

                    oSheetRegnskab_puls3060 = oSheetRegnskab;
                    oSheetRegnskab.get_Range("A1", Missing.Value).Select();

                    //////////////////////////////////////////////////////////////////////////////////////////////
                    oSheetRegnskab_puls3060.Activate();
                    oSheetRegnskab_puls3060.get_Range("A1", Missing.Value).Select();

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
        [Fieldattr(Heading = "Udvalg")]
        public string Udvalg { get; set; }
        [Fieldattr(Heading = "Aktivitet")]
        public string Aktivitet { get; set; }
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

}
