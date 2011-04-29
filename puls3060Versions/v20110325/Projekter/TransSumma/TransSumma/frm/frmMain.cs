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
using System.Data.Common;
using System.Xml.Linq;
using System.Data.Linq.SqlClient;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


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

        public Form GetChild(string child)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Text.ToUpper() == child.ToUpper())
                {
                    return frm;
                }
            }
            return null;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if (DEBUG)
            KarActebisordre objActebisordre = new KarActebisordre();
            objActebisordre.load();
            string test = "Test";
            //object xx = Program.karFakturaer_k;
            //KarKartotek recKartotek = new KarKartotek();
            //int testx = 1;
            //KarBankafstemning recBankafstemning = new KarBankafstemning();
            //recBankafstemning.load();
            /*
            var qry = from t in Program.dbDataTransSumma.Tbltrans
                      join a in Program.dbDataTransSumma.Tblbankafsteminit on new Xrec { Xrid = t.Regnskabid, Xid = t.Id, Xnr = t.Nr } equals new Xrec { Xrid = a.Rid, Xid = a.Tid, Xnr = a.Tnr } into bankafsteminit
                      from a in bankafsteminit
                      select t;
            Tblafstem recAfstem;
            foreach (Tbltrans recTrans in qry) 
            {
                try
                {
                    recAfstem = (from a in Program.dbDataTransSumma.Tblafstem
                                 join i in Program.dbDataTransSumma.Tblbankafsteminit on a.Pid equals i.Bid
                                 where i.Rid == recTrans.Regnskabid && i.Tid == recTrans.Id && i.Tnr == recTrans.Nr
                                 select a).First();
                    recAfstem.Tbltrans.Add(recTrans);
                }
                catch
                {
                    int test = 1;
                }
            }
            Program.dbDataTransSumma.SubmitChanges();
            */

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

        private void omToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = (new AboutBox()).ShowDialog();
        }

        private void BankafstemningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Bankafstemning"))
            {
                FrmBankafstemning m_frmBankafstemning = new FrmBankafstemning();
                m_frmBankafstemning.MdiParent = this;
                m_frmBankafstemning.Show();
            }
        }


        private void importerTransaktionerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? lastBilag = null;
            string Kontonavn = null;
            Tblbilag recBilag = null;
            var qryPosteringer = from p in Program.karPosteringer
                                 join b in Program.dbDataTransSumma.Tbltrans on new { p.Regnskabid, p.Id, p.Nr } equals new { b.Regnskabid, b.Id, b.Nr } into tbltrans
                                 from b in tbltrans.DefaultIfEmpty(new Tbltrans { Pid = 0, Regnskabid = null, Id = null, Nr = null })
                                 where b.Pid == 0
                                 orderby p.Regnskabid, p.Bilag, p.Id, p.Nr
                                 select p;
            int antal = qryPosteringer.Count();
            foreach (var p in qryPosteringer)
            {
                if ((!(p.Bilag == null)) && (lastBilag != p.Bilag))
                {
                    try
                    {
                        recBilag = (from b in Program.dbDataTransSumma.Tblbilag
                                    where b.Regnskabid == p.Regnskabid && b.Bilag == p.Bilag
                                    select b).First();
                    }
                    catch
                    {
                        recBilag = new Tblbilag
                        {
                            Regnskabid = p.Regnskabid,
                            Bilag = p.Bilag,
                            Dato = p.Dato,
                            Udskriv = true,
                        };
                        Program.dbDataTransSumma.Tblbilag.InsertOnSubmit(recBilag);
                    }
                }

                try
                {
                    Kontonavn = (from b in Program.karKontoplan where b.Kontonr == p.Konto select b.Kontonavn).First();
                }
                catch
                {
                    Kontonavn = null;
                }

                Tbltrans recTrans = new Tbltrans
                {
                    Regnskabid = p.Regnskabid,
                    Tekst = p.Tekst,
                    Kontonr = p.Konto,
                    Kontonavn = Kontonavn,
                    Id = p.Id,
                    Nr = p.Nr,
                    Belob = p.Nettobeløb + p.Momsbeløb,
                    Moms = p.Momsbeløb,
                    Debet = (p.Nettobeløb >= 0) ? p.Nettobeløb : (decimal?)null,
                    Kredit = (p.Nettobeløb < 0) ? -p.Nettobeløb : (decimal?)null,
                };
                Program.dbDataTransSumma.Tbltrans.InsertOnSubmit(recTrans);
                if (!(p.Bilag == 0)) recBilag.Tbltrans.Add(recTrans);
                lastBilag = p.Bilag;
            }
            Program.dbDataTransSumma.SubmitChanges();


            lastBilag = 0;
            var qryKladder = from k in Program.karKladder
                             join b in Program.dbDataTransSumma.Tblkladder on new { k.Regnskabid, k.Id } equals new { b.Regnskabid, b.Id } into tblkladder
                             from b in tblkladder.DefaultIfEmpty(new Tblkladder { Pid = 0, Regnskabid = null, Id = null })
                             where b.Pid == 0
                             orderby k.Regnskabid, k.Bilag, k.Id
                             select k;
            antal = qryKladder.Count();
            foreach (var k in qryKladder)
            {
                if ((!(k.Bilag == null)) && (lastBilag != k.Bilag))
                {
                    try
                    {
                        recBilag = (from b in Program.dbDataTransSumma.Tblbilag
                                    where b.Regnskabid == k.Regnskabid && b.Bilag == k.Bilag
                                    select b).First();
                    }
                    catch
                    {
                        recBilag = new Tblbilag
                        {
                            Regnskabid = k.Regnskabid,
                            Bilag = k.Bilag,
                            Dato = k.Dato,
                            Udskriv = true,
                        };
                        Program.dbDataTransSumma.Tblbilag.InsertOnSubmit(recBilag);
                    }
                }

                Tblkladder recKladder = new Tblkladder
                {
                    Regnskabid = k.Regnskabid,
                    Tekst = k.Tekst,
                    Afstemningskonto = k.Afstemningskonto,
                    Belob = k.Belob,
                    Konto = k.Konto,
                    Momskode = k.Momskode,
                    Faktura = k.Faktura,
                    Id = k.Id,
                };
                Program.dbDataTransSumma.Tblkladder.InsertOnSubmit(recKladder);
                if (!(k.Bilag == 0)) recBilag.Tblkladder.Add(recKladder);
                lastBilag = k.Bilag;
            }
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void nyeKladderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Nye Kladder"))
            {
                FrmNyekladder m_frmNyekladder = new FrmNyekladder();
                m_frmNyekladder.MdiParent = this;
                m_frmNyekladder.Show();
            }
        }

        private void kladderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Kladder"))
            {
                FrmKladder m_frmKladder = new FrmKladder();
                m_frmKladder.MdiParent = this;
                m_frmKladder.Show();
            }
        }

        private void bankKontoudtogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Bank kontoudtog"))
            {
                FrmBankkontoudtog m_Bankkontoudtog = new FrmBankkontoudtog();
                m_Bankkontoudtog.MdiParent = this;
                m_Bankkontoudtog.Show();
            }
        }

        private void bilagsindtastningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nyeKladderToolStripMenuItem_Click(sender, e);
            kladderToolStripMenuItem_Click(sender, e);
            bankKontoudtogToolStripMenuItem_Click(sender, e);
        }

        private void importerNordeaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KarBankkontoudtogNordea recBankkontoudtogNordea = new KarBankkontoudtogNordea();
            recBankkontoudtogNordea.load();
        }

        private void importDanskebankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KarBankkontoudtogDanskebank recBankkontoudtogDanskebank = new KarBankkontoudtogDanskebank();
            recBankkontoudtogDanskebank.load();
        }
        private void printBilagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsBilagsudskrift.Bilagsudskrift(false);
        }

        private void bilagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Bilag"))
            {
                FrmBilag m_Bilag = new FrmBilag();
                m_Bilag.MdiParent = this;
                m_Bilag.Show();
            }
        }

        private void excelRegnskabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsExcel objExcel = new clsExcel();
            objExcel.ecxelPoster();
        }

        private void indtastFakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nyFakturaToolStripMenuItem1_Click(sender, e);
            fakturaToolStripMenuItem1_Click(sender, e);
        }

        private void nyFakturaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Ny faktura"))
            {
                FrmNyfaktura m_Nyfaktura = new FrmNyfaktura();
                m_Nyfaktura.MdiParent = this;
                m_Nyfaktura.Show();
            }
        }

        private void fakturaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Faktura"))
            {
                FrmFaktura m_Faktura = new FrmFaktura();
                m_Faktura.MdiParent = this;
                m_Faktura.Show();
            }
        }

        private void importFakturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsFaktura objFaktura = new clsFaktura();
            objFaktura.ImportSalgsfakturaer();
            objFaktura.ImportKøbsfakturaer();
            objFaktura.KøbsOrderOmk();
        }

        private void varekontoTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("VarekontoType"))
            {
                FrmVarekontoType m_VarekontoType = new FrmVarekontoType();
                m_VarekontoType.MdiParent = this;
                m_VarekontoType.Show();
            }
        }

    }

    public class Xrec
    {
        public int? Xrid { get; set; }
        public int? Xid { get; set; }
        public int? Xnr { get; set; }
    }



}
