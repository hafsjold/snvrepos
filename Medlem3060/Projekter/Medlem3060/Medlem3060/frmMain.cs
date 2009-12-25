using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
