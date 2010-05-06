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
using System.Text.RegularExpressions;

namespace nsHafsjoldData
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
                    global::nsHafsjoldData.Properties.Resources.Message_info); //iconSet
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
                    global::nsHafsjoldData.Properties.Resources.Message_info); //iconSet
                this.Close();
            }
            else
            {
                var rec_regnskab = Program.qryAktivRegnskab();
                this.toolStripStatusLabel1.Text = "Regnskab: " + rec_regnskab.Rid + " " + rec_regnskab.Navn;
                this.toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Right;
                this.toolStripStatusLabel2.Text = "Database: " + global::nsHafsjoldData.Properties.Settings.Default.DataBasePath;
                this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;

                KarKontoplan karKontoplan = Program.karKontoplan;
                Program.path_to_lock_summasummarum_kontoplan = rec_regnskab.Placering + "kontoplan.dat";
                Program.filestream_to_lock_summasummarum_kontoplan = new FileStream(Program.path_to_lock_summasummarum_kontoplan, FileMode.Open, FileAccess.Read, FileShare.None);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dbHafsjoldData.SubmitChanges();
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

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if (DEBUG)

            //KarPosteringsjournal karPosteringsjournal = new KarPosteringsjournal();
            //karPosteringsjournal.open();

            //KarDanskeErhverv karDanskeErhverv = new KarDanskeErhverv();


            //KarPosteringer karPosteringer = new KarPosteringer();

            
#endif
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

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = (new AboutBox()).ShowDialog();
        }

        private void opretGendannelsespunktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsRecovery objRecovery = new clsRecovery();
            objRecovery.createRecoveryPoint();

        }

        private void tilbageTilGendannelsesPunktToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importBankkontoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsImport objImport = new clsImport();
            objImport.ImportDanskeErhverv();
        }

        private void importJournalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsImport objImport = new clsImport();
            objImport.ImportPosteringsjournal();
            objImport.ImportKladder();
        }

        private void bankafstemningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Bankafstemning"))
            {
                FrmBankafstemning frmBankafstemning = new FrmBankafstemning();
                frmBankafstemning.MdiParent = this;
                frmBankafstemning.Show();
            }
        }

        private void nyKladdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Ny Kladde"))
            {
                FrmNyKladde frmNyKladde = Program.frmNyKladde;
            }
        }

        private void bogførtKladdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Bogført Kladde"))
            {
                FrmBogfortKladde frmBogfortKladde = Program.frmBogfortKladde;
            }
        }
    }
}
