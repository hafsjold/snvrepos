using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace nsPuls3060
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dbData3060.SubmitChanges();
        }


        private void medlemmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.frmMedlemmer.MdiParent = this; 
            Program.frmMedlemmer.Focus();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPbs objPbs = new clsPbs();
            clsPbs601 objPbs601 = new clsPbs601();
            clsPbs602 objPbs602 = new clsPbs602();
            KarMedlemmer objMedlemmer = new KarMedlemmer();
            //objPbs601.faktura_601_action(1);
            //objPbs602.TestRead042();
            //objPbs602.ReadFraPbsFile();
            //objPbs601.WriteTilPbsFile(615);
            //objPbs.ReadRegnskaber();
            //objPbs.SetAktivRegnskaber();
            //DateTime dt = new DateTime(2009, 10, 31);
            //double ssdate = clsUtil.SummaDateTime2Serial(dt);
            //double testdaynr = objPbs.GregorianDate2JulianDayNumber(dt);
            //DateTime testdate = objPbs.JulianDayNumber2GregorianDate(testdaynr);
            //KarStatus myKarStatus = new KarStatus();
            //myKarStatus.save();
            //var qry_medlemmer = from k in objMedlemmer
            //                    join m in Program.dbData3060.TblMedlem on k.Nr equals m.Nr
            //                    where m.FodtDato > DateTime.Parse("1980-01-01")
            //                    select new { k.Nr, k.Navn, k.Kaldenavn, k.Adresse, k.Postnr, k.Bynavn, k.Email, k.Telefon, m.Knr, m.Kon, m.FodtDato };
            //
            //var antal = qry_medlemmer.Count();
            //foreach (var mx in qry_medlemmer) 
            //{
            //    var medlem = mx;
            //}
            /*
             clsMedlem nytmedlem = new clsMedlem() 
            {
                Nr = 483,
                Navn = "Nyt Medlem",
                //Kaldenavn = "Nyt",
                Adresse = "Ny adresse 25",
                Postnr = "3060",
                Bynavn = "Espergærde",
                Telefon = "1234 5432",
                Email = "dex@dfres.dk"
            };
            string nystring = nytmedlem.getNewCvsString();
            KarFakturaer_s objFakturaer_s = new KarFakturaer_s();
            objFakturaer_s.save();
            */
            bool running = clsUtil.IsProcessOpen("Summax");
        }


        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime pReadDate = DateTime.Now;
            string pSheetName = "Medlem";

            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Window oWindow;
            Excel.Range oRng;

            var rec_regnskab = (from r in Program.dbData3060.TblRegnskab
                                join a in Program.dbData3060.TblAktivtRegnskab on r.Rid equals a.Rid
                                select r).First();

            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";

            var MedlemmerAll = from h in Program.karMedlemmer
                               join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                               from x in details1.DefaultIfEmpty(new TblMedlem { Nr = -1, Knr = -1, Kon = "X", FodtDato = new DateTime(1900, 1, 1) })
                               select new clsMedlemAll
                               {
                                   Nr = h.Nr,
                                   Navn = h.Navn,
                                   Kaldenavn = h.Kaldenavn,
                                   Adresse = h.Adresse,
                                   Postnr = h.Postnr,
                                   Bynavn = h.Bynavn,
                                   Telefon = h.Telefon,
                                   Email = h.Email,
                                   Knr = (int)(x.Knr == null ? -1 : x.Knr),
                                   Kon = x.Kon == null ? "X" : x.Kon,
                                   FodtDato = (DateTime)(x.FodtDato == null ? new DateTime(1900, 01, 01) : x.FodtDato)
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
                    foreach (clsMedlemAll m in MedlemmerAll)
                    {
                        row++;
                        Type objectType = m.GetType();
                        PropertyInfo[] properties = objectType.GetProperties();
                        int col = 0;
                        foreach (PropertyInfo property in properties)
                        {
                            col++;
                            string Name = property.Name;
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

                    oSheet.Cells.EntireColumn.AutoFit();
                    
                    oWindow.SplitRow = 1;
                    oWindow.SplitColumn = 2;
                    oWindow.FreezePanes = true;

                    oSheet.get_Range("A1", Missing.Value).Select();

                    oWB.SaveAs(SaveAs, Excel.XlFileFormat.xlWorkbookNormal, "", "", false, false, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

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
