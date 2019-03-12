using System;
using System.Linq;
using System.Windows.Forms;
using nsPbs3060;

namespace Medlem3060uc
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
            try
            {
                UCInitializer.UnicontaLogin();
                var CurrentCompany = UCInitializer.CurrentCompany;
                this.toolStripStatusLabel1.Text = "Firma: " + CurrentCompany.CompanyId + " " + CurrentCompany.Name;
                this.toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Right;
                this.toolStripStatusLabel2.Text = Program.ConnectStringWithoutPassword;
                this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;
                this.toolStripStatusLabel2.AutoSize = true;
            }
            catch (Exception)
            {
                this.toolStripStatusLabel1.Text = "LogIn to UniConta Failed";
                this.toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Right;
                this.toolStripStatusLabel2.Text = Program.ConnectStringWithoutPassword;
                this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;
                this.toolStripStatusLabel2.AutoSize = true;
            }

            try
            {
                var antalBogføringer = Program.dbData3060.tblbets.Where(b => b.bogforingsdato > DateTime.Now.AddDays(-30)).Count();
            }
            catch
            {
                DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                    "Medlem3060uc", //titleString   
                    "Ingen forbindelse til Database Server", //bigString 
                    null, //smallString
                    null, //leftButton
                    "OK", //rightButton
                    global::Medlem3060uc.Properties.Resources.Message_info); //iconSet   
            }

#if (DEBUG)
            testToolStripMenuItem.Visible = true;
#endif
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Program.dbData3060.SubmitChanges();
                Properties.Settings.Default.Save();
            }
            catch { }
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

        private void kerditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Kreditor"))
            {
                FrmKreditor m_frmKreditor = new FrmKreditor();
                m_frmKreditor.MdiParent = this;
                m_frmKreditor.Show();
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

#endif
        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = (new AboutBox()).ShowDialog();
        }

        private void kontingentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Kontingent"))
            {
                FrmKontingent m_frmKontingent = new FrmKontingent();
                m_frmKontingent.MdiParent = this;
                m_frmKontingent.Show();
            }
        }

        private void opdaterMedlemsstausToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.dbData3060.UpdateMedlemStatus();
        }

        private void regnearkManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelManagement();
        }


        private void impoerEmailBilagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            impoerEmailBilag(sender, e);
        }

        private void impoerEmailBilag(object sender, EventArgs e)
        {
            clsUnicontaHelp obj = new clsUnicontaHelp(UCInitializer.GetBaseAPI);
            int antalbilag = obj.ImportEmailBilag();
            string bigString;
            if (antalbilag == 0)
            {
                bigString = "Der er ingen Email Bilag klar til uploaded til UniConta";
            }
            else
            {
                bigString = string.Format("Der er uploaded {0} Email Bilag til UniConta.", antalbilag);
            }

            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Medlem3060uc", //titleString   
                bigString, //bigString 
                null, //smallString
                null, //leftButton
                "OK", //rightButton
                global::Medlem3060uc.Properties.Resources.Message_info); //iconSet   
        }

        private void exportDanskeErhvervToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KarBankkontoudtogDanskebank recBankkontoudtogDanskebank = new KarBankkontoudtogDanskebank(Program.dbData3060, UCInitializer.GetBaseAPI, 1, KarBankkontoudtogDanskebank.action.import);
            recBankkontoudtogDanskebank.load();
            recBankkontoudtogDanskebank.export();
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Medlem3060uc", //titleString   
                "Danske Erhverv Kontoudtog uploaded til UniConta.", //bigString 
                null, //smallString
                null, //leftButton
                "OK", //rightButton
                global::Medlem3060uc.Properties.Resources.Message_info); //iconSet   
        }

        private void toolStripDanskeErhvervExport_Click(object sender, EventArgs e)
        {
            KarBankkontoudtogDanskebank recBankkontoudtogDanskebank = new KarBankkontoudtogDanskebank(Program.dbData3060, UCInitializer.GetBaseAPI, 1, KarBankkontoudtogDanskebank.action.import);
            recBankkontoudtogDanskebank.load();
            recBankkontoudtogDanskebank.export();
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Medlem3060uc", //titleString   
                "Danske Erhverv Kontoudtog uploaded til UniConta.", //bigString 
                null, //smallString
                null, //leftButton
                "OK", //rightButton
                global::Medlem3060uc.Properties.Resources.Message_info); //iconSet     
        }

        private void toolStripImpoetEmailBilag_Click(object sender, EventArgs e)
        {
            impoerEmailBilag(sender, e);
        }

        private void posterTilRegnskabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ecxelPoster(DateTime.Now.Year);
        }

        private void sysInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("frmSysInfo"))
            {
                FrmSysInfo frmSysInfo = new FrmSysInfo();
                frmSysInfo.MdiParent = this;
                frmSysInfo.Show();
            }
        }

        private void exportMobilePayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exportMobilePayTool(sender, e);
        }

        private void toolExportMobilePay_Click(object sender, EventArgs e)
        {
            exportMobilePayTool(sender, e);
        }

        private void exportMobilePayTool(object sender, EventArgs e)
        {
            KarMobilepay objMobilepay = new KarMobilepay(Program.dbData3060, UCInitializer.GetBaseAPI, 7, KarMobilepay.action.import);
            objMobilepay.load_Mobilepay();
            objMobilepay.load_bankkonto();
            int GLDailyJournalLines = objMobilepay.export();
            string smallString = string.Format("{0} bilag uploaded til UniConta.", GLDailyJournalLines.ToString());
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Medlem3060uc", //titleString   
                "MobilePay Kontoudtog uploaded til UniConta.", //bigString 
                smallString, //smallString
                null, //leftButton
                "OK", //rightButton
                global::Medlem3060uc.Properties.Resources.Message_info); //iconSet  
        }

        private void medlemsListeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelManagement();
        }

        private void posterTilRegnskabSidsteRegnskabsårToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ecxelPoster(DateTime.Now.Year - 1);
        }
    }
}
