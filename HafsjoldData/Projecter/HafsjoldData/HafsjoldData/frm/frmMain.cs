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

            //clsGoogle objGoogle = new clsGoogle();
            //objGoogle.test();
            //clsRecovery objRecovery = new clsRecovery();
            //objRecovery.TestRecovery();
            //clsSFTP objSFTP = new clsSFTP();
            //objSFTP.ReadFraSFtp();
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
            //                    join m in Program.dbHafsjoldData.TblMedlem on k.Nr equals m.Nr
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
            var qryMedlemLog = from m in Program.dbHafsjoldData.TblMedlemLog
                        where m.Nr == pNr && m.Logdato <= pDate
                        select new
                        {
                            Id = (int)m.Id,
                            Nr = (int)m.Nr,
                            Logdato = (DateTime)m.Logdato,
                            Akt_id = (int)m.Akt_id,
                            Akt_dato = (DateTime)m.Akt_dato
                        };
            var qryFak = from f in Program.dbHafsjoldData.Tblfak
                       join p in Program.dbHafsjoldData.Tbltilpbs on f.Tilpbsid equals p.Id
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
                               join d1 in Program.dbHafsjoldData.TblMedlem on h.Nr equals d1.Nr into details1
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

    }
}
