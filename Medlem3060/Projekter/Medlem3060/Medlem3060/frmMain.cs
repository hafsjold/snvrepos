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
    public partial class frmMain : Form
    {
        private DbData3060 m_dbData3060;
        
        public frmMain()
        {
            InitializeComponent();
            m_dbData3060 = new DbData3060(global::nsPuls3060.Properties.Settings.Default.DataBasePath);
          

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_dbData3060.SubmitChanges();
        }

        public void Test_Click(object sender, EventArgs e)
        {
            clsPbs objPbs = new clsPbs(m_dbData3060);
            clsPbs601 objPbs601 = new clsPbs601(m_dbData3060);
            clsPbs602 objPbs602 = new clsPbs602(m_dbData3060);
            clsMedlemmer objMedlemmer = new clsMedlemmer(m_dbData3060);
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
            //KarStatus myKarStatus = new KarStatus(m_dbData3060);
            //myKarStatus.save();
            var qry_medlemmer = from k in objMedlemmer
                                join m in m_dbData3060.TblMedlem on k.Nr equals m.Nr
                                where m.FodtDato > DateTime.Parse("1980-01-01")
                                select new { k.Nr, k.Navn, k.Kaldenavn, k.Adresse, k.Postnr, k.Bynavn, k.Email, k.Telefon, m.Knr, m.Kon, m.FodtDato };

            var antal = qry_medlemmer.Count();
            foreach (var mx in qry_medlemmer) 
            {
                var medlem = mx;
            }
        
        
        }
    }
}
