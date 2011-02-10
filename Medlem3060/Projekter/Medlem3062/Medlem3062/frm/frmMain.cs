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
            clsPbs objPbs = new clsPbs();
            objPbs.DatabaseUpdate();
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
            clsAppEngSFTP objAppEngSFTP = new clsAppEngSFTP();
            objAppEngSFTP.ReadFraSFtp();
            
            /*
            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "tilpbs");
            XDocument xmldata = XDocument.Parse(strxmldata);
            string Status = xmldata.Descendants("Status").First().Value;
            if (Status == "True")
            {
                clsAppEngSFTP objAppEngSFTP = new clsAppEngSFTP();
                objAppEngSFTP.WriteTilSFtp(xmldata);
            }
            */
            /*
            dsMedlem objMedlem= new dsMedlem();
            objMedlem.filldsMedlem();
            objMedlem.fillPerson();
            var PersonNotInSyncMedlem = ((System.Data.DataTable)objMedlem.tblSyncMedlem).AsEnumerable().Except(((System.Data.DataTable)objMedlem.tblSyncPerson).AsEnumerable(), DataRowComparer.Default);
            int antalPersonNotInSyncMedlem = PersonNotInSyncMedlem.Count();
            foreach( var p in PersonNotInSyncMedlem) 
            {
                short Nr = p.Field<short>("Nr");
                DataRow row1 = null;
                DataRow row2 = null;
                try
                {
                    row1 = objMedlem.Kartotek.Rows.Find(Nr);
                    row2 = objMedlem.tblPerson.Rows.Find(Nr);
                    if (row2 == null)
                    {
                        row2 = objMedlem.tblPerson.Rows.Add(row1.ItemArray);
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
            objMedlem.savePerson();
            
            var MedlemNotInSyncPerson = ((System.Data.DataTable)objMedlem.tblSyncPerson).AsEnumerable().Except(((System.Data.DataTable)objMedlem.tblSyncMedlem).AsEnumerable(), DataRowComparer.Default);
            int antalMedlemNotInSyncPerson = MedlemNotInSyncPerson.Count();

            objMedlem.fillMedlog();
            */
            /*
            int cxd = 2;
            clsConvert objConvert = new clsConvert();
            objConvert.cvnPerson();
            objConvert.cvnMedlog();
            objConvert.cvnPbsforsendelse(); 
            objConvert.cvnTilpbs();
            objConvert.cvnFak();
            objConvert.cvnRykker();
            objConvert.cvnOverforsel();
            objConvert.cvnPbsfiles();
            objConvert.cvnFrapbs();
            objConvert.cvnBet();
            objConvert.cvnBetlin();
            objConvert.cvnAftalelin();
            objConvert.cvnIndbetalingskort();
            objConvert.cvnSftp();
            objConvert.cvnInfotekst();
            objConvert.cvnSysinfo();
            objConvert.cvnKreditor();
            */
            /*
            //clsSFTP objSFTP = new clsSFTP();
            //objSFTP.ReWriteTilSFtp(986);
            //clsRest objRest = new clsRest();
            //string resp = objRest.HttpGet2("Medlem");
            int xys = 1;
            //decimal tal = 50;
            //string uden1 = tal.ToString();
            //string uden2 = tal.ToString("0.00");
            //string infotxt = clsPbs.getinfotekst(11, null, null, null,null,null,"Mogens Hafsjold");
            //bool tilmeldtpbs = clsPbs.gettilmeldtpbs(386);
            //clsSumma objSumma = new clsSumma();
            //objSumma.BogforUdBetalinger(11);
            //string txt = @"p0: {0}, p1: {1},p5: {5}";
            //string ptxt = string.Format(txt, "x0", "x1", "x2", "x3", "x4", "x5", "x6");
            //DateTime dato = new DateTime(2010, 5, 20, 
            //    13, 25, 00);
            //int plusdage = 2;
            //DateTime wdispositionsdato = clsOverfoersel.bankdageplus(dato, plusdage);
            //clsPbs603 objPbs603 = new clsPbs603();
            //objPbs603.aftaleoplysninger_fra_pbs();
            //int xx = 0;

            //clsOverfoersel objOverfoersel = new clsOverfoersel();
            //objOverfoersel.overfoersel_mail(617);
            //KarFakturastr_k objKarFakturastr_k = new KarFakturastr_k();
            //objKarFakturastr_k.open();
            //int xx = 1;
            //clsImportMedlem objImportMedlem = new clsImportMedlem();
            //clsGoogle objGoogle = new clsGoogle();
            //objGoogle.test();
            //clsRecovery objRecovery = new clsRecovery();
            //objRecovery.TestRecovery();
            //clsSFTP objSFTP = new clsSFTP();
            //byte[] data ={96,97,98};
            //objSFTP.sendAttachedFile("testfile", data, false);
            //objSFTP.WriteTilSFtp(606);
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
            //                    select new { k.Nr, k.Navn, k.Kaldenavn, k.Adresse, k.Postnr, k.Bynavn, k.Email, k.Telefon, m.Kon, m.FodtDato };
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
                               from x in details1.DefaultIfEmpty(new TblMedlem { Nr = -1, Kon = "X", FodtDato = new DateTime(1900, 1, 1) })
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
                                   Kon = x.Kon == null ? "X" : x.Kon,
                                   FodtDato = (DateTime)(x.FodtDato == null ? new DateTime(1900, 01, 01) : x.FodtDato)
                               };

            var qry = from t in MedlemmerAll
                      select new
                      {
                          MNr = t.Nr,
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

            clsPbs602 objPbs602 = new clsPbs602();
            clsPbs603 objPbs603 = new clsPbs603();

            clsSFTP objSFTP = new clsSFTP();
            AntalImportFiler = objSFTP.ReadFraSFtp();  //Læs direkte SFTP
            objSFTP.DisconnectSFtp();
            objSFTP = null;
            //AntalImportFiler = objPbs602.ReadFraPbsFile(); //Læs fra Directory FraPBS

            int Antal602Filer = objPbs602.betalinger_fra_pbs();
            int Antal603Filer = objPbs603.aftaleoplysninger_fra_pbs();

            clsSumma objSumma = new clsSumma();
            int AntalOrdre = objSumma.Order2Summa();

            bigString = String.Format("Antal indlæste filer fra PBS: {0} \nAntal nye 602 filer: {1}\nAntal nye 603 filer: {3}\nAntal nye ordre: {2}.", AntalImportFiler, Antal602Filer, AntalOrdre, Antal603Filer);
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

        private void reSendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FocusChild("Resend til PBS"))
            {
                FrmResend m_frmResend = new FrmResend();
                m_frmResend.MdiParent = this;
                m_frmResend.Show();
            }
        }
    }
}
