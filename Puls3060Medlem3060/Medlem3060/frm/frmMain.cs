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
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Collections;
using PHPSerializationLibrary;


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
                this.toolStripStatusLabel1.Text = "Regnskab: " + rec_regnskab.rid + " " + rec_regnskab.Navn;
                this.toolStripStatusLabel1.Alignment = ToolStripItemAlignment.Right;
                this.toolStripStatusLabel2.Text = Program.ConnectStringWithoutPassword;
                this.toolStripStatusLabel2.Alignment = ToolStripItemAlignment.Right;

                KarKontoplan ReadKontoplan = Program.karKontoplan;
                Program.path_to_lock_summasummarum_kontoplan = rec_regnskab.Placering + "kontoplan.dat";
                Program.filestream_to_lock_summasummarum_kontoplan = new FileStream(Program.path_to_lock_summasummarum_kontoplan, FileMode.Open, FileAccess.Read, FileShare.None);
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
            puls3060_dkEntities jdb = new puls3060_dkEntities();
            clsPbs601 objPbs601 = new clsPbs601();
            Tuple<int, int> tresultc = objPbs601.rsmembeshhip_fakturer_auto(Program.dbData3060, jdb);
            int AntalKontingent = tresultc.Item1;
            int lobnrc = tresultc.Item2;
            //AntalKontingent = 1;
            //lobnrc = 5039; 
            if ((AntalKontingent > 0))
            {
                objPbs601.faktura_og_rykker_601_action(Program.dbData3060, lobnrc, fakType.fdrsmembership);
                clsSFTP objSFTPc = new clsSFTP(Program.dbData3060);
                objSFTPc.WriteTilSFtp(Program.dbData3060, lobnrc);
                objSFTPc.DisconnectSFtp();
                objSFTPc = null;
            }

            /*
            WMemRSMembershipTransactions cls = new WMemRSMembershipTransactions();
            
            var qryusers = from u in jdb.ecpwt_users
                           join m in jdb.ecpwt_rsmembership_membership_subscribers on u.id equals m.user_id
                           where m.membership_id == 6
                           join a in jdb.ecpwt_rsmembership_subscribers on u.id equals a.user_id
                           join t in jdb.ecpwt_rsmembership_transactions on m.last_transaction_id equals t.id 
                           select new {
                                id = u.id,
                                name = u.name,
                                adresse = a.f1,
                                postnr = a.f4,
                                Bynavn = a.f2,
                                Telefon = a.f6,
                                email = u.email,
                                Nr = a.f14,
                                membership_start = m.membership_start,
                                membership_end = m.membership_end,
                                user_data = t.user_data
                           };

            int antal = qryusers.Count();
            int x = 0;
            foreach (var transactions in qryusers)
            {
                x++;
            }
            */
            /*
            string TilPBSFile = "Mogens Hafsjold";
            char[] c_TilPBSFile = TilPBSFile.ToCharArray();
            byte[] b_TilPBSFile = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_TilPBSFile);
            clsAzure objAzure = new clsAzure();
            objAzure.uploadBlob("test_med_metadata2.txt", "Puls3060", b_TilPBSFile);


            // START TEST TEST TEST TEST TEST TEST
            clsBankUdbetalingsUdskrift objBankUdbetalingsUdskrift = new clsBankUdbetalingsUdskrift();
            objBankUdbetalingsUdskrift.BankUdbetalingsUdskrifter(Program.dbData3060, 894);
            

            var rstPusteruns = from p in Program.dbData3060.vPusterummets select p;
            int count = rstPusteruns.Count();
            if (count > 0)
            {
                foreach (var rstPusterun in rstPusteruns)
                {
                    int Nr = (int)rstPusterun.Nr;
                    string Navn = rstPusterun.Navn;
                    string Adresse = rstPusterun.Adresse;
                    string Postnr = rstPusterun.Postnr;
                }
            }
            */
            /*
            string bigString = "BIGSTRING";
            string smallString = "smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString smallString";
            DialogResult result = DotNetPerls.BetterDialog.ShowDialog(
                "Betalinger fra PBS", //titleString 
                bigString, //bigString 
                smallString, //smallString
                null, //leftButton
                "OK", //rightButton
                global::nsPuls3060.Properties.Resources.Message_info); //iconSet
            */
            /*
            clsPbs686 objPbs686 = new clsPbs686();
            int Antal686Filer = objPbs686.aftaleoplysninger_fra_pbs(Program.dbData3060);
            objPbs686 = null;
            */

            /*
            // case enumTask.KontingentNyeMedlemmer:
            clsPbs601 objPbs601c = new clsPbs601();
            Tuple<int, int> tresultc = objPbs601c.kontingent_fakturer_auto(Program.dbData3060);
            int AntalKontingent = tresultc.Item1;
            int lobnrc = tresultc.Item2;
            if ((AntalKontingent > 0))
            {
                objPbs601c.faktura_og_rykker_601_action(Program.dbData3060, lobnrc, fakType.fdfaktura);
                clsSFTP objSFTPc = new clsSFTP(Program.dbData3060);
                objSFTPc.WriteTilSFtp(Program.dbData3060, lobnrc);
                objSFTPc.DisconnectSFtp();
                objSFTPc = null;
            }
            objPbs601c = null;
            */

            /*
            //case enumTask.SendEmailRykker:
            clsPbs601 objPbs601b = new clsPbs601();
            Tuple<int, int> tresultb = objPbs601b.rykker_auto(Program.dbData3060);
            int AntalRykker = tresultb.Item1;
            int lobnrb = tresultb.Item2;
            if ((AntalRykker > 0))
                objPbs601b.rykker_email(Program.dbData3060, lobnrb);
            objPbs601b = null;
            */


            // SLUT TEST TEST TEST TEST TEST TEST
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

        private void betalingerFraPBSToolStripMenuItem_Click(object sender, EventArgs e)
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
            //AntalImportFiler = objPbs602.ReadFraPbsFile(); //Læs fra Directory FraPBS

            int Antal602Filer = objPbs602.betalinger_fra_pbs(Program.dbData3060);
            int Antal603Filer = objPbs603.aftaleoplysninger_fra_pbs(Program.dbData3060);
            int Antal686Filer = objPbs686.aftaleoplysninger_fra_pbs(Program.dbData3060);

            Tuple<int, int> tresult = objPbs601.advis_auto(Program.dbData3060);
            int AntalAdvis = tresult.Item1;
            int lobnr = tresult.Item2;
            if ((AntalAdvis > 0))
                objPbs601.advis_email(Program.dbData3060, lobnr);

            clsSumma objSumma = new clsSumma();
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

        private void databasePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Indtast Password"))
            {
                FrmPassword m_frmPassword = new FrmPassword();
                m_frmPassword.MdiParent = this;
                m_frmPassword.Show();
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

        private void pusterummetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Pusterummet"))
            {
                FrmPusterummet frmPusterummet = new FrmPusterummet();
                frmPusterummet.MdiParent = this;
                frmPusterummet.Show();
            }
        }

        private void opdaterMedlemsstausToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.dbData3060.UpdateMedlemStatus();
        }

        private void medlemExtraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("MedlemExtra"))
            {
                FrmMedlemExtra frmMedlemExtra = new FrmMedlemExtra();
                frmMedlemExtra.MdiParent = this;
                frmMedlemExtra.Show();
            }
        }

        private void regnearkManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            excelManagement();
        }

        private void vedligeholdNyeMedlemmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Nye Medlemmer"))
            {
                FrmNyeMedlemmer frmNyeMedlemmer = new FrmNyeMedlemmer();
                frmNyeMedlemmer.MdiParent = this;
                frmNyeMedlemmer.Show();
            }
        }

        private void regnearkMailSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mailSync();
        }

        private void payPalTilPBSNyeMedlemmerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puls3060_dkEntities jdb = new puls3060_dkEntities();
            clsPbs601 objPbs601 = new clsPbs601();
            Tuple<int, int> tresultc = objPbs601.rsmembeshhip_fakturer_auto(Program.dbData3060, jdb);
            int AntalKontingent = tresultc.Item1;
            int lobnrc = tresultc.Item2;
            if ((AntalKontingent > 0))
            {
                objPbs601.faktura_og_rykker_601_action(Program.dbData3060, lobnrc, fakType.fdrsmembership);
                clsSFTP objSFTPc = new clsSFTP(Program.dbData3060);
                objSFTPc.WriteTilSFtp(Program.dbData3060, lobnrc);
                objSFTPc.DisconnectSFtp();
                objSFTPc = null;
            }
        }

        private void opdaterRSMembershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puls3060_dkEntities jdb = new puls3060_dkEntities();
            clsPbs602 objPbs602 = new clsPbs602();
            objPbs602.betalinger_til_rsmembership(Program.dbData3060, jdb);
        }

        private void testEmailRykkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            puls3060_dkEntities jdb = new puls3060_dkEntities();
            clsPbs601 objPbs601 = new clsPbs601();
            Tuple<int, int> tresult = objPbs601.rykker_auto(Program.dbData3060, jdb);
            int AntalRykker = tresult.Item1;
            int lobnr = tresult.Item2;
            if ((AntalRykker > 0))
                objPbs601.rykker_email(Program.dbData3060, lobnr);
            objPbs601 = null;
        }

    }
}
