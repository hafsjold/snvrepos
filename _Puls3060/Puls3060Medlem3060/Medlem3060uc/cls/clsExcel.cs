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

            //var rec_regnskab = Program.qryAktivRegnskab();
            string rec_regnskab_Eksportmappe = @"C:\Users\regns\Documents\SummaSummarum"; // work
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
}
