﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Excel;
using System.Data.Common;
using System.Data.SQLite;
using System.Xml.Linq;


namespace nsPuls3060
{
    public partial class FrmMain : Form
    {

        public FrmMain()
        {
            InitializeComponent();
            Program.frmMain = this;
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

                object ReadKontoplan = Program.karKontoplan;
                object ReadMedlog = Program.dsMedlemGlobal;
                Program.path_to_lock_summasummarum_kontoplan = rec_regnskab.Placering + "kontoplan.dat";
                Program.filestream_to_lock_summasummarum_kontoplan = new FileStream(Program.path_to_lock_summasummarum_kontoplan, FileMode.Open, FileAccess.Read, FileShare.None);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            //clsAppEngSFTP objAppEngSFTP = new clsAppEngSFTP();
            //objAppEngSFTP.ReadFraSFtp();



#endif
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

        private void SendModtagPBSfilertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bigString = null;
            string smallString = null;

            clsSFTP objAppEngSFTP = new clsSFTP();

            //Send til PBS
            string Status = "True";
            int AntalFilerSendt = 0;
            clsRest objRest = new clsRest();
            while (Status == "True")
            {
                string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "tilpbs");
                XDocument xmldata = XDocument.Parse(strxmldata);
                Status = xmldata.Descendants("Status").First().Value;
                if (Status == "True")
                {
                    bool bSendt = objAppEngSFTP.WriteTilSFtp(xmldata);
                    if (bSendt) AntalFilerSendt++;
                }
            }

            //Modtag fra PBS
            int AntalFilerModtaget = objAppEngSFTP.ReadFraSFtp();

            bigString = String.Format("{0} filer sendt til PBS.\n{1} filer modtaget fra PBS", AntalFilerSendt, AntalFilerModtaget);
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Send/Modtag PBS filer", //titleString 
                bigString, //bigString 
                smallString, //smallString
                null, //leftButton
                "OK", //rightButton
                global::nsPuls3060.Properties.Resources.Message_info); //iconSet
        }

        private void betalingerFraPBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bigString = null;
            string smallString = null;

            clsSumma objSumma = new clsSumma();
            int AntalOrdre = objSumma.Order2Summa();

            bigString = String.Format("Antal nye ordre: {0}.", AntalOrdre);
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
            int AntalBetalinger = objSumma.BogforIndBetalinger();

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

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = (new AboutBox()).ShowDialog();
        }

        private void excelInterntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelInternt();
        }

        private void excelExterntToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelExternt();
        }

        private void posterTilExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ecxelPoster();
        }

        private void betalingsForslagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Betalings Forslag"))
            {
                FrmBetalingsForslag m_frmBetalingsForslag = new FrmBetalingsForslag();
                m_frmBetalingsForslag.MdiParent = this;
                m_frmBetalingsForslag.Show();
            }
        }

        private void emailRykkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Rykker Forslag"))
            {
                FrmRykkerForslag m_frmRykkerForslag = new FrmRykkerForslag();
                m_frmRykkerForslag.MdiParent = this;
                m_frmRykkerForslag.Show();
            }
        }

        private void infoTekstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Info tekst"))
            {
                FrmInfotekst m_frmInfotekst = new FrmInfotekst();
                m_frmInfotekst.MdiParent = this;
                m_frmInfotekst.Show();
            }
        }

        private void pbsFilertoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("PBS Filer"))
            {
                FrmPbsnetdir m_frmPbsnetdir = new FrmPbsnetdir();
                m_frmPbsnetdir.MdiParent = this;
                m_frmPbsnetdir.Show();
            }
        }

        private void regnearkNotPBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelNotPBS();
        }

        private void data2AppEngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsConvert objConvert = new clsConvert();
            //objConvert.cvnPerson();
            //objConvert.cvnMedlog();
            //objConvert.cvnPbsforsendelse();
            //objConvert.cvnTilpbs();
            //objConvert.cvnFak();
            //objConvert.cvnRykker();
            //objConvert.cvnOverforsel();
            //objConvert.cvnPbsfiles();
            //objConvert.cvnFrapbs();
            //objConvert.cvnBet();
            //objConvert.cvnBetlin();
            //objConvert.cvnAftalelin();
            //objConvert.cvnIndbetalingskort();
            //objConvert.cvnSftp();
            objConvert.cvnInfotekst();
            //objConvert.cvnSysinfo();
            //objConvert.cvnKreditor();
            //objConvert.linkFak();
            //objConvert.NrSerieSetupAll();
            //objConvert.reindexPerson();
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
              "data2AppEng", //titleString 
              "Konvertering slut", //bigString 
              "", //smallString
              null, //leftButton
              "OK", //rightButton
              global::nsPuls3060.Properties.Resources.Message_info); //iconSet
        }

        
        private void syncMedlemmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Person -> Medlemmer sync/update
            var MedlemNotInSyncPerson = ((System.Data.DataTable)Program.dsMedlemGlobal.tblSyncPerson).AsEnumerable().Except(((System.Data.DataTable)Program.dsMedlemGlobal.tblSyncMedlem).AsEnumerable(), DataRowComparer.Default);
            int antalMedlemNotInSyncPerson = MedlemNotInSyncPerson.Count();
            if (antalMedlemNotInSyncPerson > 0)
            {
                foreach (var p in MedlemNotInSyncPerson)
                {
                    short Nr = p.Field<short>("Nr");
                    DataRow row1 = null;
                    DataRow row2 = null;
                    try
                    {
                        row1 = Program.dsMedlemGlobal.tblPerson.Rows.Find(Nr);
                        row2 = Program.dsMedlemGlobal.Kartotek.Rows.Find(Nr);
                        if (row2 == null)
                        {
                            row2 = Program.dsMedlemGlobal.Kartotek.Rows.Add(row1.ItemArray);
                        }
                        else
                        {
                            row2.BeginEdit();
                            row2.ItemArray = row1.ItemArray;
                            row2.EndEdit();
                        }

                    }
                    catch (MissingPrimaryKeyException)
                    {

                    }
                }
                Program.dsMedlemGlobal.savedsMedlem();
            }

            // Medlemmer -> Person sync/update
            var PersonNotInSyncMedlem = ((System.Data.DataTable)Program.dsMedlemGlobal.tblSyncMedlem).AsEnumerable().Except(((System.Data.DataTable)Program.dsMedlemGlobal.tblSyncPerson).AsEnumerable(), DataRowComparer.Default);
            int antalPersonNotInSyncMedlem = PersonNotInSyncMedlem.Count();
            if (antalPersonNotInSyncMedlem > 0)
            {
                foreach (var p in PersonNotInSyncMedlem)
                {
                    short Nr = p.Field<short>("Nr");
                    DataRow row1 = null;
                    DataRow row2 = null;
                    try
                    {
                        row1 = Program.dsMedlemGlobal.Kartotek.Rows.Find(Nr);
                        row2 = Program.dsMedlemGlobal.tblPerson.Rows.Find(Nr);
                        if (row2 == null)
                        {
                            object[] val = row1.ItemArray;
                            val[8] = "X";
                            val[9] = new DateTime(1900, 1, 1);
                            row2 = Program.dsMedlemGlobal.tblPerson.Rows.Add(val);
                        }
                        else
                        {
                            object[] val = row2.ItemArray;
                            for (var i = 0; i < val.Count(); i++)
                            {
                                if ((i != 8) && (i != 9))
                                {
                                    val[i] = row1.ItemArray[i];
                                }
                            }
                            row2.BeginEdit();
                            row2.ItemArray = val;
                            row2.EndEdit();
                        }

                    }
                    catch (MissingPrimaryKeyException)
                    {

                    }
                }
                Program.dsMedlemGlobal.savePerson();
            }
        }
    }
}