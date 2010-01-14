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

namespace nsPuls3060
{
    public partial class FrmMain : Form
    {
        private FileStream m_ts;
        private string m_path;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
#if (DEBUG)
            testToolStripMenuItem.Visible = true;
#endif   
            if (clsUtil.IsProcessOpen("Summa"))
            {
                DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                    "Medlem 3060", //titleString 
                    "Du skal afslutte SummaSummarum inden du kan starte Medlem 3060.", //bigString 
                    null, //smallString
                    null, //leftButton
                    "OK", //rightButton
                    global::nsPuls3060.Properties.Resources.Message_info); //iconSet
                this.Close();
            }

            DialogResult res = (new frmSelectRegnskab()).ShowDialog();
            if (res != DialogResult.OK)
            {
                DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                    "Medlem 3060", //titleString 
                    "Du har ikke valgt et regnskab. Medlem 3060 afsluttes.", //bigString 
                    null, //smallString
                    null, //leftButton
                    "OK", //rightButton
                    global::nsPuls3060.Properties.Resources.Message_info); //iconSet
                this.Close();
            }
            else
            {
                var rec_regnskab = Program.qryAktivRegnskab();
                this.toolStripStatusLabel1.Text = "Regnskab: " + rec_regnskab.Rid + " " + rec_regnskab.Navn;
                this.toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Right;
                this.toolStripStatusLabel2.Text = "Database: " + global::nsPuls3060.Properties.Settings.Default.DataBasePath;
                this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;
                m_path = rec_regnskab.Placering + "kontoplan.dat";
                m_ts = new FileStream(m_path, FileMode.Open, FileAccess.Read, FileShare.None);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dbData3060.SubmitChanges();
            Properties.Settings.Default.Save();
        }

        private bool FocusChild(string child)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Text.ToUpper() == child.ToUpper())
                {
                    frm.Focus();
                    return true;
                }
            }
            return false;
        }

        private void medlemmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Medlemmer"))
            {
                FrmMedlemmer frmMedlemmer = new FrmMedlemmer();
                frmMedlemmer.MdiParent = this;
                frmMedlemmer.Show();
            }
        }

        private void kerditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Kreditor"))
            {
                FrmKreditor m_frmKreditor = new FrmKreditor();
                m_frmKreditor.MdiParent = this;
                m_frmKreditor.Show();
            }
        }

        private void kontingentForslagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Kontingent Forslag"))
            {
                FrmKontingentForslag m_frmKontingentForslag = new FrmKontingentForslag();
                m_frmKontingentForslag.MdiParent = this;
                m_frmKontingentForslag.Show();
            }
        }
        private void pbsfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!FocusChild("Pbsfiles"))
            {
                FrmPbsfiles m_frmPbsfiles = new FrmPbsfiles();
                m_frmPbsfiles.MdiParent = this;
                m_frmPbsfiles.Show();
            }
        }
        
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if (DEBUG)
            //clsPbs objPbs = new clsPbs();
            //clsPbs601 objPbs601 = new clsPbs601();
            //clsPbs602 objPbs602 = new clsPbs602();
            //Program.karDkkonti.save();
            //Program.karKortnr.save();
            //clsSumma objSumma = new clsSumma();
            //objSumma.Order2Summa();
            //objSumma.OrderFaknrUpdate();
            //objSumma.BogforBetalinger();
            //objPbs601.faktura_601_action(1);
            //objPbs602.TestRead042();
            //objPbs602.ReadFraPbsFile();
            //objPbs602.betalinger_fra_pbs();
            //bool x = objPbs.erMedlem(93);
            //objPbs601.WriteTilPbsFile(606);
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
            int pNr = 3;
            DateTime pDate = DateTime.Now;
            var qryMedlemLog = from m in Program.dbData3060.TblMedlemLog
                        where m.Nr == pNr && m.Logdato <= pDate
                        select new
                        {
                            Id = (int)m.Id,
                            Nr = (int)m.Nr,
                            Logdato = (DateTime)m.Logdato,
                            Akt_id = (int)m.Akt_id,
                            Akt_dato = (DateTime)m.Akt_dato
                        };
            var qryFak = from f in Program.dbData3060.Tblfak
                       join p in Program.dbData3060.Tbltilpbs on f.Tilpbsid equals p.Id
                       where f.Nr == pNr && p.Bilagdato <= pDate
                       select new
                       {
                           Id = (int)f.Id,
                           Nr = (int)f.Nr,
                           Logdato = (DateTime)p.Bilagdato,
                           Akt_id = (int)20,
                           Akt_dato = (DateTime)f.Betalingsdato
                       };

            var qryUnion = qryMedlemLog.Union(qryFak).OrderByDescending(u => u.Logdato);

            foreach (var l in qryUnion)
            {
                var x = l.Logdato;
            }

            DateTime qryStart = DateTime.Now;
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

            var qry = from t in MedlemmerAll
                      select new
                      {
                          MNr = t.Nr,
                          t.Knr,
                          t.Kon,
                          t.FodtDato,
                          t.Navn,
                          log10 = (from w in Program.qryLog()
                                 where w.Nr == t.Nr && w.Akt_id == 10
                                orderby w.Logdato descending
                                select new
                                {
                                    w.Id,
                                    LNr = w.Nr,
                                    w.Logdato,
                                    w.Akt_id,
                                    w.Akt_dato
                                }).FirstOrDefault(),
                          log20 = (from w in Program.qryLog()
                                 where w.Nr == t.Nr && w.Akt_id == 20
                                 orderby w.Logdato descending
                                 select new
                                 {
                                     w.Id,
                                     LNr = w.Nr,
                                     w.Logdato,
                                     w.Akt_id,
                                     w.Akt_dato
                                 }).FirstOrDefault(),
                          log30 = (from w in Program.qryLog()
                                 where w.Nr == t.Nr && w.Akt_id == 30
                                orderby w.Logdato descending
                                select new
                                {
                                    w.Id,
                                    LNr = w.Nr,
                                    w.Logdato,
                                    w.Akt_id,
                                    w.Akt_dato
                                }).FirstOrDefault(),
                          log50 = (from w in Program.qryLog()
                                 where w.Nr == t.Nr && w.Akt_id == 50
                                orderby w.Logdato descending
                                select new
                                {
                                    w.Id,
                                    LNr = w.Nr,
                                    w.Logdato,
                                    w.Akt_id,
                                    w.Akt_dato
                                }).FirstOrDefault()

                      };
            foreach (var q in qry)
            {
                var x = q;
                //foreach (var y in x) 
                //{
                //    var z = y.Akt_dato;
                //}
            }

            DateTime qrySlut = DateTime.Now;
            TimeSpan Tid = qrySlut - qryStart;
        */
#endif
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

            var rec_regnskab = Program.qryAktivRegnskab();
            string SaveAs = rec_regnskab.Eksportmappe + pSheetName + pReadDate.ToString("_yyyyMMdd_hhmmss") + ".xls";

            var MedlemmerAll = from h in Program.karMedlemmer
                               join d1 in Program.dbData3060.TblMedlem on h.Nr equals d1.Nr into details1
                               from x in details1.DefaultIfEmpty()  //new TblMedlem { Nr = -1, Knr = -1, Kon = "X", FodtDato = new DateTime(1900, 1, 1) })
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
                    oWB.Saved = true;
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

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void vælgRegnskabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new frmSelectRegnskab()).ShowDialog();
        }

        private void regnskabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Regnskab"))
            {
                FrmRegnskab frmRegnskab = new FrmRegnskab();
                frmRegnskab.MdiParent = this;
                frmRegnskab.Show();
            }
        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void betalingerFraPBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bigString = null;
            string smallString = null;

            clsPbs602 objPbs602 = new clsPbs602();
            int AntalImportFiler = objPbs602.ReadFraPbsFile();
            int Antal602Filer = objPbs602.betalinger_fra_pbs();

            clsSumma objSumma = new clsSumma();
            int AntalOrdre = objSumma.Order2Summa();
            
            bigString = String.Format("Antal indlæste filer fra PBS: {0} \nAntal nye betalings filer: {1}\nAntal nye ordre: {2}.", AntalImportFiler, Antal602Filer, AntalOrdre);
            if (AntalOrdre > 0)
            {
                smallString = String.Format("Åben SummaSummarum\nTryk på ikonet Bilag i venstre side\nbogfør de {0} nye ordre.", AntalOrdre);
            }
            else
            {
                smallString = "Der er ingen nye odre";
            }

            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Betalinger fra PBS", //titleString 
                bigString, //bigString 
                smallString, //smallString
                null, //leftButton
                "OK", //rightButton
                global::nsPuls3060.Properties.Resources.Message_info); //iconSet

        }

        private void betalingerTilKassekladeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bigString = null;
            string smallString = null;
            
            clsSumma objSumma = new clsSumma();
            int AntalFakturaer = objSumma.OrderFaknrUpdate();
            int AntalBetalinger = objSumma.BogforBetalinger();

            bigString = String.Format("Antal bogførte fakturear: {0} \nAntal nye betalinger i kassekladde: {1}.", AntalFakturaer, AntalBetalinger);
            if (AntalBetalinger > 0)
            {
                smallString = "Åben SummaSummarum\nTryk på ikonet Kassekladde i venstre side\nbogfør den nye kassekladde.";
            }
            else
            {
                smallString = "Der er ingen ny kassekladde";
            }

            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Betalinger til Kassekladde", //titleString 
                bigString, //bigString 
                smallString, //smallString
                null, //leftButton
                "OK", //rightButton
                global::nsPuls3060.Properties.Resources.Message_info); //iconSet

        }

    }
}
