using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;     // to use Missing.Value
using Outlook = Microsoft.Office.Interop.Outlook;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Diagnostics;

namespace nsVagtplan
{
    class Program
    {

        static void Main(string[] args)
        {
            VagtplanExcel("2013-09-30");
            //VagtplanOutlook("2011-09-30");
        }

        
        static void VagtplanOutlook(string pSlutDT)
        {
            DateTime SlutDT = DateTime.Parse(pSlutDT);
            clsTemplate objTemplate = new clsTemplate();
            Outlook.Application oApp = new Outlook.Application();
            Outlook.NameSpace oNS = oApp.GetNamespace("mapi");
            oNS.Logon(Missing.Value, Missing.Value, true, true);
            Outlook.MAPIFolder oCalendar = oNS.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderCalendar);
            Outlook.Items oItems = oCalendar.Items;
            string dayfilter = "[Start] >= '" + DateTime.Today.Date.ToString("g") + "' AND [End] < '" + SlutDT.Date.ToString("g") + "'";
            string subjectfilter;
            if (oApp.Session.DefaultStore.IsInstantSearchEnabled)
                subjectfilter = "@SQL=\"urn:schemas:httpmail:subject\" ci_startswith '" + objTemplate.Tekst + "'";
            else
                subjectfilter = "@SQL=\"urn:schemas:httpmail:subject\" like '%" + objTemplate.Tekst + "%'";
 
            Outlook.Items oDagItems = oItems.Restrict(dayfilter).Restrict(subjectfilter);
            for (int i = oDagItems.Count; i > 0; i--)
                oDagItems[i].Delete();

            for (DateTime dt = DateTime.Today; dt.Date <= SlutDT.Date; dt = dt.AddDays(1).Date)
            {
                recTemplate rec = objTemplate.getDag(dt);

                if (!rec.Fri)
                {
                    Outlook.AppointmentItem oAppt = (Outlook.AppointmentItem)oItems.Add(Outlook.OlItemType.olAppointmentItem);
                    oAppt.Subject = objTemplate.Tekst; // set the subject
                    oAppt.Start = dt.Date.Add((TimeSpan)rec.Start);
                    oAppt.End = dt.Date.Add((TimeSpan)rec.Slut);
                    oAppt.ReminderSet = false; // Set the reminder
                    oAppt.Importance = Outlook.OlImportance.olImportanceNormal; // appointment importance
                    oAppt.BusyStatus = Outlook.OlBusyStatus.olBusy;
                    oAppt.Save();
                }
            }
        
        }

        
        static void VagtplanExcel(string pSlutDT)
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "Vagtplan";
            string SaveAs = @"C:\Users\mha\Documents\" + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";

            Excel.Application oXL = null; ;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Window oWindow;
            Excel.Range oRng;
            Excel.Range oCell1;
            Excel.Range oCell2;

            //Start Excel and get Application object.
            oXL = new Excel.Application();
            oXL.Visible = true;

            //Get a new workbook
            oWB = oXL.Workbooks.Add((Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;
            oWindow = oXL.ActiveWindow;


            if (pSheetName.Length > 0) oSheet.Name = pSheetName.Substring(0, pSheetName.Length > 34 ? 34 : pSheetName.Length);
            int row = 1;
            int col = 1;

            DateTime StartDT = DateTime.Now;
            DateTime SlutDT = DateTime.Parse(pSlutDT);

            DateTime StartMD = new DateTime(StartDT.Year, StartDT.Month, 1);

            clsTemplate objTemplate = new clsTemplate();

            int Maxi = (SlutDT.Subtract(StartDT).Days / 28) + 1;
            Dictionary<DateTime, int> dicMD = new Dictionary<DateTime, int>();
            for (int i = 0; i < Maxi; i++)
                dicMD.Add(StartMD.AddMonths(i), i);

            oRng = oSheet.Columns[1];
            oRng.ColumnWidth = 0.5;

            for (DateTime dt = StartMD.Date; dt.Date <= SlutDT.Date; dt = dt.AddDays(1).Date)
            {
                DateTime dtMD = new DateTime(dt.Year, dt.Month, 1);
                int spalte;
                if (dicMD.TryGetValue(dtMD, out spalte))
                {
                    col = 2 + spalte * 3;
                    if (dt.Day == 1)
                    {
                        row = 1;
                        oSheet.Cells[row, col] = dtMD;
                        oSheet.Cells[row, col].NumberFormat = "mmm-åååå";
                        oCell1 = oSheet.Cells[row, col] as Excel.Range;
                        oCell2 = oSheet.Cells[row, col + 1] as Excel.Range;
                        oRng = oSheet.get_Range(oCell1, oCell2);
                        oRng.MergeCells = true;
                        oRng.Font.Name = "Arial";
                        oRng.Font.Size = 10;
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

                        oCell1 = oSheet.Cells[row + 1, col] as Excel.Range;
                        oCell2 = oSheet.Cells[row + 31, col] as Excel.Range;
                        oRng = oSheet.get_Range(oCell1, oCell2);
                        oRng.NumberFormat = "ddd d";

                        oCell1 = oSheet.Cells[row + 1, col + 1] as Excel.Range;
                        oCell2 = oSheet.Cells[row + 31, col + 1] as Excel.Range;
                        oRng = oSheet.get_Range(oCell1, oCell2);
                        oRng.HorizontalAlignment = Excel.Constants.xlCenter;

                        oCell1 = oSheet.Cells[row + 1, col] as Excel.Range;
                        oCell2 = oSheet.Cells[row + 31, col + 1] as Excel.Range;
                        oRng = oSheet.get_Range(oCell1, oCell2);

                        oRng.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeLeft].ColorIndex = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeLeft].TintAndShade = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThin;

                        oRng.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeTop].ColorIndex = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeTop].TintAndShade = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThin;

                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].ColorIndex = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].TintAndShade = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThin;

                        oRng.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeRight].ColorIndex = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeRight].TintAndShade = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThin;

                        oRng = oSheet.Columns[col + 2];
                        oRng.ColumnWidth = 0.5;
                    }
                    row = dt.Day + 1;

                    recTemplate rec = objTemplate.getDag(dt);
                    oSheet.Cells[row, col] = dt.Date;
                    if (!rec.Fri)
                        oSheet.Cells[row, col + 1] = string.Format("'{0}-{1}", rec.Start.Value.Hours, rec.Slut.Value.Hours);
                    else
                        oSheet.Cells[row, col + 1] = "F";

                    if (dt.DayOfWeek == DayOfWeek.Sunday)
                    {
                        oCell1 = oSheet.Cells[row, col] as Excel.Range;
                        oCell2 = oSheet.Cells[row, col + 1] as Excel.Range;
                        oRng = oSheet.get_Range(oCell1, oCell2);
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].ColorIndex = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].TintAndShade = 0;
                        oRng.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThin;
                    }
                }
            }

            oSheet.Cells.EntireColumn.AutoFit();

            oSheet.get_Range("A1", Missing.Value).Select();

            for (var i = oWB.Worksheets.Count; i > 0; i--)
            {
                Excel._Worksheet oSheetWrk = (Excel._Worksheet)oWB.Worksheets.get_Item(i);
                if (oSheetWrk.Name != pSheetName)
                {
                    oSheetWrk.Delete();
                }
            }

            oSheet.PageSetup.LeftHeader = "&14Vagtplan for Alice";
            oSheet.PageSetup.CenterHeader = "";
            oSheet.PageSetup.RightHeader = "&P af &N";
            oSheet.PageSetup.LeftFooter = "&Z&F";
            oSheet.PageSetup.CenterFooter = "";
            oSheet.PageSetup.RightFooter = "&D&T";
            oSheet.PageSetup.LeftMargin = oXL.InchesToPoints(0.75);
            oSheet.PageSetup.RightMargin = oXL.InchesToPoints(0.75);
            oSheet.PageSetup.TopMargin = oXL.InchesToPoints(1.00);
            oSheet.PageSetup.BottomMargin = oXL.InchesToPoints(0.75);
            oSheet.PageSetup.HeaderMargin = oXL.InchesToPoints(0.5);
            oSheet.PageSetup.FooterMargin = oXL.InchesToPoints(0.5);
            oSheet.PageSetup.PrintHeadings = false;
            oSheet.PageSetup.PrintGridlines = false;
            oSheet.PageSetup.CenterHorizontally = false;
            oSheet.PageSetup.CenterVertically = false;
            oSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
            oSheet.PageSetup.Draft = false;
            oSheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
            oSheet.PageSetup.FirstPageNumber = 1;
            oSheet.PageSetup.Order = Excel.XlOrder.xlDownThenOver;
            oSheet.PageSetup.BlackAndWhite = false;
            oSheet.PageSetup.Zoom = 100;
            oSheet.PageSetup.PrintErrors = Excel.XlPrintErrors.xlPrintErrorsDisplayed;

            oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            oWB.Saved = true;
        }
    }
}

