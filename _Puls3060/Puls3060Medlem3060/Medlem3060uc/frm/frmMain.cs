using System;
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
using nsPbs3060;
using System.Diagnostics;
using System.Collections;
using PHPSerializationLibrary;
using MimeKit;
using Uniconta.Common;
using Uniconta.DataModel;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;

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

#if (DEBUG)
            testToolStripMenuItem.Visible = true;
#endif
            if (!FocusChild("pipeServer"))
            {
                pipeServer m_pipeServer = new pipeServer();
                m_pipeServer.MdiParent = this;
                m_pipeServer.Show();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_PipeThread.Join();
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

#endif
        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void betalingerFraPBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            betalingerFraPBS(sender, e);
        }

        private void betalingerFraPBS(object sender, EventArgs e)
        {
            string bigString = null;
            string smallString = null;
            int AntalImportFiler = 0;

            clsPbs601 objPbs601 = new clsPbs601();
            clsPbs602 objPbs602 = new clsPbs602();
            clsPbs603 objPbs603 = new clsPbs603();
            clsPbs686 objPbs686 = new clsPbs686();

            clsSFTP objSFTP = new clsSFTP(Program.dbData3060);
            AntalImportFiler = objSFTP.ReadFraSFtp(Program.dbData3060);  //Læs direkte SFTP
            objSFTP.DisconnectSFtp();
            objSFTP = null;

            int Antal602Filer = objPbs602.betalinger_fra_pbs(Program.dbData3060);
            int Antal603Filer = objPbs603.aftaleoplysninger_fra_pbs(Program.dbData3060);
            int Antal686Filer = objPbs686.aftaleoplysninger_fra_pbs(Program.dbData3060);

            Tuple<int, int> tresult = objPbs601.advis_auto(Program.dbData3060);
            int AntalAdvis = tresult.Item1;
            int lobnr = tresult.Item2;
            if ((AntalAdvis > 0))
                objPbs601.advis_email_lobnr(Program.dbData3060, lobnr);

            clsUniconta objSumma = new clsUniconta(Program.dbData3060, UCInitializer.GetBaseAPI);
            int AntalBetalinger = objSumma.BogforIndBetalinger();

            if (AntalBetalinger > 0)
            {
                bigString = String.Format("Der er {0} bogførte betalinger.", AntalBetalinger);
            }
            else
            {
                bigString = "Der er ingen nye betalinger";
            }
            smallString = String.Format("Antal indlæste filer fra PBS: {0} \nAntal nye 602 filer: {1}\nAntal nye 603 filer: {3}\nAntal nye 686 filer: {4}\nAntal nye betalinger: {2}.", AntalImportFiler, Antal602Filer, AntalBetalinger, Antal603Filer, Antal686Filer);



            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Filer fra PBS", //titleString 
                bigString, //bigString 
                smallString, //smallString
                null, //leftButton
                "OK", //rightButton
                global::Medlem3060uc.Properties.Resources.Message_info); //iconSet

        }

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = (new AboutBox()).ShowDialog();
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

        private void reSendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Resend til PBS"))
            {
                FrmResend m_frmResend = new FrmResend();
                m_frmResend.MdiParent = this;
                m_frmResend.Show();
            }
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

        private void importerFileFraPBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Importer file fra PBS"))
            {
                FrmImportPBSFile m_frmImportPBSFile = new FrmImportPBSFile();
                m_frmImportPBSFile.MdiParent = this;
                m_frmImportPBSFile.Show();
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

        private void payPalTilPBSNyeMedlemmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puls3060_nyEntities jdb = new puls3060_nyEntities(true);
            clsPbs601 objPbs601 = new clsPbs601();
            Tuple<int, int> tresultc = objPbs601.paypal_pending_rsmembeshhip_fakturer_auto(Program.dbData3060, jdb);
            int AntalKontingent = tresultc.Item1;
            int lobnrc = tresultc.Item2;
            if ((AntalKontingent > 0))
            {
                //pbsType.indbetalingskort
                objPbs601.faktura_og_rykker_601_action_lobnr(Program.dbData3060, lobnrc);
                clsSFTP objSFTPc = new clsSFTP(Program.dbData3060);
                objSFTPc.WriteTilSFtp(Program.dbData3060, lobnrc);
                objSFTPc.DisconnectSFtp();
                objSFTPc = null;
            }
        }

        private void opdaterRSMembershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puls3060_nyEntities jdb = new puls3060_nyEntities(true);
            clsPbs602 objPbs602 = new clsPbs602();
            objPbs602.betalinger_til_rsmembership(Program.dbData3060, jdb);
        }

        private void testEmailRykkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puls3060_nyEntities jdb = new puls3060_nyEntities(true);
            clsPbs601 objPbs601 = new clsPbs601();
            Tuple<int, int> tresult = objPbs601.rykker_auto(Program.dbData3060, jdb);
            int AntalRykker = tresult.Item1;
            int lobnr = tresult.Item2;
            if ((AntalRykker > 0))
                objPbs601.rykker_email_lobnr(Program.dbData3060, lobnr);
            objPbs601 = null;
        }

        private void payPalBetalingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payPalBetalinger(sender, e);
        }

        private void payPalBetalinger(object sender, EventArgs e)
        {
            string bigString = null;
            string smallString = null;
            clsUniconta objSumma = new clsUniconta(Program.dbData3060, UCInitializer.GetBaseAPI);
            int AntalBetalinger = objSumma.BogforPaypalBetalinger();
            if (AntalBetalinger > 0)
            {
                bigString = String.Format("Der er {0} bogførte betalinger.", AntalBetalinger);
            }
            else
            {
                bigString = "Der er ingen nye betalinger";
            }
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "PayPal Betalinger", //titleString 
                bigString, //bigString 
                smallString, //smallString
                null, //leftButton
                "OK", //rightButton
                global::Medlem3060uc.Properties.Resources.Message_info); //iconSet
        }

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //clsPbsHelper.Verifye_rsmembership_transactions_data();
            clsPbsHelper.update_rsmembership_transactions_user_data();

            /*
            //case enumTask.KontingentNyeMedlemmer:
            puls3060_nyEntities cjdb = new puls3060_nyEntities(true);
            clsPbs601 objPbs601c = new clsPbs601();
            Tuple<int, int> tresultc = objPbs601c.paypal_pending_rsmembeshhip_fakturer_auto(Program.dbData3060, cjdb);
            int AntalKontingent = tresultc.Item1;
            int lobnrc = tresultc.Item2;
            if ((AntalKontingent > 0))
            {
                //pbsType.indbetalingskort
                objPbs601c.faktura_og_rykker_601_action_lobnr(Program.dbData3060, lobnrc);
                clsSFTP objSFTPc = new clsSFTP(Program.dbData3060);
                objSFTPc.WriteTilSFtp(Program.dbData3060, lobnrc);
                objSFTPc.DisconnectSFtp();
                objSFTPc = null;

                Tuple<int, int> tresultd = objPbs601c.advis_auto_lobnr(Program.dbData3060, lobnrc);
                int AntalAdvisd = tresultd.Item1;
                int lobnrd = tresultd.Item2;
                if ((AntalAdvisd > 0))
                    objPbs601c.advis_email_lobnr(Program.dbData3060, lobnrd);
                objPbs601c = null;
            }
            */
            //puls3060_nyEntities djdb = new puls3060_nyEntities(true);
            //clsPbs601 objPbs601d = new clsPbs601();
            //objPbs601d.rsmembeshhip_betalinger_auto(Program.dbData3060, djdb);

            //puls3060_nyEntities p_dbPuls3060_dk = new puls3060_nyEntities(true);
            //clsPbs601 objPbs601d = new clsPbs601();
            //objPbs601d.rsmembeshhip_betalinger_auto(Program.dbData3060, p_dbPuls3060_dk);

            //clsRSMembership2UniConta obj = new nsPbs3060.clsRSMembership2UniConta(Program.dbData3060, p_dbPuls3060_dk, UCInitializer.GetBaseAPI);
            //obj.testBetalinger();
            //clsUniconta obj = new clsUniconta(Program.dbData3060, UCInitializer.GetBaseAPI);
            //obj.TestFakturering();
            //clsPbsHelper obj = new clsPbsHelper();
            //obj.Work_OpdateringAfSlettet_rsmembership_transaction(Program.dbData3060);
            var xtest = 1;
        }

        private void opdaterKanSlettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPbsHelper objPbsHelper = new clsPbsHelper();
            objPbsHelper.opdaterKanSlettes();
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

            private void exportPayPalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KarPaypal objPaypal = new nsPbs3060.KarPaypal(Program.dbData3060, UCInitializer.GetBaseAPI, 5, KarPaypal.action.import);
            objPaypal.load_paypal();
            objPaypal.load_bankkonto();
            objPaypal.export();
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Medlem3060uc", //titleString   
                "PayPal Kontoudtog uploaded til UniConta.", //bigString 
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

        private void toolStripExportPayPal_Click(object sender, EventArgs e)
        {
            KarPaypal objPaypal = new nsPbs3060.KarPaypal(Program.dbData3060, UCInitializer.GetBaseAPI, 5, KarPaypal.action.import);
            objPaypal.load_paypal();
            objPaypal.load_bankkonto();
            objPaypal.export();
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Medlem3060uc", //titleString   
                "PayPal Kontoudtog uploaded til UniConta.", //bigString 
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
            ecxelPoster();
        }

        private void toolStripBogførPBSFiler_Click(object sender, EventArgs e)
        {
            betalingerFraPBS(sender, e);
        }

        private void toolStripPayPalBetalinger_Click(object sender, EventArgs e)
        {
            payPalBetalinger(sender, e);
        }

        private void opdaterAppDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("App Data"))
            {
                FrmApp frmApp = new FrmApp();
                frmApp.MdiParent = this;
                frmApp.Show();
            }
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
    }
}
