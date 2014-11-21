using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nsInfo3060
{
    [Flags]
    public enum KontoType
    {
        None = 0x0,
        Drift = 0x1,
        Status = 0x2,
        Debitor = 0x4,
        Kreditor = 0x8
    }

    public partial class FrmMedlemList : Form
    {
        public int? SelectedNr { get; set; }
        public string SelectedNavn { get; set; }

        public FrmMedlemList()
        {
            InitializeComponent();
        }

        public FrmMedlemList(Point Start, KontoType ktp)
        {
            global::nsInfo3060.Properties.Settings.Default.frmMedlemListLocation = Start;
            InitializeComponent();

            if ((ktp & KontoType.Drift) == KontoType.Drift)
                checkBoxDrift.Checked = true;
            else
                checkBoxDrift.Checked = false;

            if ((ktp & KontoType.Status) == KontoType.Status)
                checkBoxStatus.Checked = true;
            else
                checkBoxStatus.Checked = false;

            if ((ktp & KontoType.Debitor) == KontoType.Debitor)
                checkBoxDebitor.Checked = true;
            else
                checkBoxDebitor.Checked = false;

            if ((ktp & KontoType.Kreditor) == KontoType.Kreditor)
                checkBoxKreditor.Checked = true;
            else
                checkBoxKreditor.Checked = false;
        }
       
        private void FrmMedlemList_Load(object sender, EventArgs e)
        {
            getMedlemmer();
        }
        
        private void getMedlemmer()
        {
            IEnumerable<tblMedlem> qry_Medlem;

            qry_Medlem = from k in Program.dbData3060.tblMedlems select k;

            this.lvwMedlemmer.Items.Clear();
            foreach (var b in qry_Medlem)
            {
                ListViewItem it = lvwMedlemmer.Items.Add(b.Nr.ToString(), b.Nr.ToString(), 0);
                it.SubItems.Add(b.Navn);
                it.SubItems.Add(b.Kaldenavn);
                it.SubItems.Add(b.Adresse);
                it.SubItems.Add(b.Postnr);
                it.SubItems.Add(b.Bynavn) ;
                it.SubItems.Add(b.Telefon );
                it.SubItems.Add(b.Email);
            }
        }
        
        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            SelectedNr = null;
            this.Close();
        }

        private void lvwMedlemmer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwMedlemmer.SelectedItems.Count == 1)
            {
                SelectedNr = int.Parse(lvwMedlemmer.SelectedItems[0].Name);
                SelectedNavn = lvwMedlemmer.SelectedItems[0].SubItems[1].Text;
            }
        }

 
    }
}
