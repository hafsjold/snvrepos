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
            */
            KarFakturaer_s objFakturaer_s = new KarFakturaer_s();
            objFakturaer_s.save();
        }


        private void excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

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


                    //Add table headers going cell by cell.
                    oSheet.Cells[1, 1] = "First Name";
                    oSheet.Cells[1, 2] = "Last Name";
                    oSheet.Cells[1, 3] = "Full Name";
                    oSheet.Cells[1, 4] = "Salary";

                    //Format A1:D1 as bold, vertical alignment = center.
                    oSheet.get_Range("A1", "D1").Font.Bold = true;
                    oSheet.get_Range("A1", "D1").VerticalAlignment =
                        Excel.XlVAlign.xlVAlignCenter;

                    // Create an array to multiple values at once.
                    string[,] saNames = new string[5, 2];

                    saNames[0, 0] = "John";
                    saNames[0, 1] = "Smith";
                    saNames[1, 0] = "Tom";
                    saNames[1, 1] = "Brown";
                    saNames[2, 0] = "Sue";
                    saNames[2, 1] = "Thomas";
                    saNames[3, 0] = "Jane";
                    saNames[3, 1] = "Jones";
                    saNames[4, 0] = "Adam";
                    saNames[4, 1] = "Johnson";

                    //Fill A2:B6 with an array of values (First and Last Names).
                    oSheet.get_Range("A2", "B6").Value2 = saNames;

                    //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                    oRng = oSheet.get_Range("C2", "C6");
                    oRng.Formula = "=A2 & \" \" & B2";

                    //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                    oRng = oSheet.get_Range("D2", "D6");
                    oRng.Formula = "=RAND()*100000";
                    oRng.NumberFormat = "$0.00";

                    //AutoFit columns A:D.
                    oRng = oSheet.get_Range("A1", "D1");
                    oRng.EntireColumn.AutoFit();

                    //Manipulate a variable number of columns for Quarterly Sales Data.
                    //DisplayQuarterlySales(oSheet);

                    //Make sure Excel is visible and give the user control
                    //of Microsoft Excel's lifetime.
                    oXL.Visible = true;
                    oXL.UserControl = true;

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
