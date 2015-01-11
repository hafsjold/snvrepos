using nsPbs3060;
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
    public partial class FrmNyeMedlemmer : Form
    {
        private MemNytMedlem memNytMedlem;

        public FrmNyeMedlemmer()
        {
            InitializeComponent();
        }

        private void FrmNyeMedlemmer_Load(object sender, EventArgs e)
        {
            this.fillDatasource();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (recNytMedlem m in this.bsNytMedlem.DataSource as MemNytMedlem)
            {
                if (m.import == 1) 
                {
                    tblNytMedlem recNytMedlem = (from t in Program.dbData3060.tblNytMedlems where t.id == m.id select t).First();
                    int tblMedlem_nr = tblMedlemsNextval();
                    tblMedlem recMedlem = new tblMedlem 
                    {
                        Nr = tblMedlem_nr,
                        Navn = recNytMedlem.Fornavn + " " + recNytMedlem.Efternavn,
                        Kaldenavn = recNytMedlem.Fornavn,
                        Adresse = recNytMedlem.Adresse,
                        Postnr = recNytMedlem.Postnr,
                        Bynavn = recNytMedlem.Bynavn,
                        Telefon = (recNytMedlem.Mobil == null) ? recNytMedlem.Telefon : recNytMedlem.Mobil,
                        Email = recNytMedlem.Email,
                        FodtDato = recNytMedlem.FodtDato,
                        Kon = (recNytMedlem.Kon.ToUpper() == "MAND") ? "M" : "K",
                        Status = 1
                    };
                    Program.dbData3060.tblMedlems.InsertOnSubmit(recMedlem);

                    DateTime nu = DateTime.Now;
                    int next_id = (int)(from r in Program.dbData3060.nextval("tblMedlemlog") select r.id).First();
                    nsPbs3060.tblMedlemLog recLog = new nsPbs3060.tblMedlemLog
                    {
                        id = next_id,
                        Nr = tblMedlem_nr,
                        logdato = new DateTime(nu.Year, nu.Month, nu.Day),
                        akt_id = 10,
                        akt_dato = recNytMedlem.MessageDate
                    };
                    Program.dbData3060.tblMedlemLogs.InsertOnSubmit(recLog);

                    recNytMedlem.Nr = tblMedlem_nr;

                    Program.dbData3060.SubmitChanges();
                }
                else if (m.delete == 1)
                {
                    tblNytMedlem recNytMedlem = (from t in Program.dbData3060.tblNytMedlems where t.id == m.id select t).First();
                    recNytMedlem.Nr = -1;
                    Program.dbData3060.SubmitChanges();
                }  
              
            }
            this.fillDatasource();
        }
        
        private void FrmNyeMedlemmer_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void fillDatasource()
        {
            IQueryable<recNytMedlem> qryNytMedlem = from m in Program.dbData3060.tblNytMedlems
                      where m.Nr == null
                      select new recNytMedlem
                      {
                          import = 0,
                          delete = 0,
                          id = m.id,
                          MessageDate = m.MessageDate,
                          MessageFrom = m.MessageFrom,
                          MessageID = m.MessageID,
                          Fornavn = m.Fornavn,
                          Efternavn = m.Efternavn,
                          Adresse = m.Adresse,
                          Postnr = m.Postnr,
                          Bynavn = m.Bynavn,
                          Telefon = m.Telefon,
                          Mobil = m.Mobil,
                          Email = m.Email,
                          FodtDato = m.FodtDato,
                          Besked = m.Besked,
                          Kon = m.Kon,
                          Nr = m.Nr
                      };

            memNytMedlem = new MemNytMedlem();
            foreach (recNytMedlem r in qryNytMedlem) {memNytMedlem.Add(r);}
            this.bsNytMedlem.DataSource = memNytMedlem;
        }

        private int tblMedlemsNextval()
        {
            try
            {
                int maxNr2 = (from k in Program.dbData3060.tblMedlems orderby k.Nr descending select k.Nr).First();
                int maxNr = ++maxNr2;
                return maxNr;
            }
            catch (Exception)
            {
                int maxNr = 1;
                return maxNr;
            }
        }

    }

    public class MemNytMedlem : List<recNytMedlem> { }   
    public class recNytMedlem
    {
        public int import { get; set; }
        public int delete { get; set; }
        public int id { get; set; }
        public System.DateTime MessageDate { get; set; }
        public string MessageFrom { get; set; }
        public string MessageID { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Adresse { get; set; }
        public string Postnr { get; set; }
        public string Bynavn { get; set; }
        public string Telefon { get; set; }
        public string Mobil { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> FodtDato { get; set; }
        public string Besked { get; set; }
        public string Kon { get; set; }
        public Nullable<int> Nr { get; set; }
    }
 
}
