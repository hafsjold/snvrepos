using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;

namespace nsPuls3060
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

    public partial class FrmKontoplanList : Form
    {
        public int? SelectedKontonr { get; set; }
        public string SelectedMomskode { get; set; }
        
        public FrmKontoplanList()
        {
            InitializeComponent();
        }
        
        public FrmKontoplanList(Point Start, KontoType ktp)
        {
            global::nsPuls3060.Properties.Settings.Default.frmKontoplanListLocation = Start;
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
        private void FrmKontoplanList_Load(object sender, EventArgs e)
        {
            getKontoplan();
        }

        private void checkBoxMedsaldo_CheckedChanged(object sender, EventArgs e)
        {
            getKontoplan();
        }        
        
        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            getKontoplan();
        }
        
        private void getKontoplan()
        {
            String[] aType = { "Drift", "Status", "Debitor", "Kreditor" };
            if (!checkBoxDrift.Checked)
                aType[0] = "w";
            if (!checkBoxStatus.Checked)
                aType[1] = "x";
            if (!checkBoxDebitor.Checked)
                aType[2] = "y";
            if (!checkBoxKreditor.Checked)
                aType[3] = "z";

            bool bFind = false;
            if (toolStripTextBoxFind.Text.Length > 0)
                bFind = true;

            string strLike = toolStripTextBoxFind.Text;
            IEnumerable<recKontoplan> qry_Kontoplan;
            IEnumerable<recKontoplan> qry_Kartotek;
            IEnumerable<recKontoplan> qry_Kontoplan2;
            IEnumerable<recKontoplan> qry_Kontoplan3;
            IEnumerable<recKontoplan> qry_Join;
            if (checkBoxMedsaldo.Checked)
            {
                qry_Kontoplan = from k in Program.karKontoplan
                                where k.Saldo != null && k.Saldo != 0
                                select k;
                qry_Kartotek = from k in Program.karKartotek
                               where k.Saldo != null && k.Saldo != 0
                               select new recKontoplan 
                               {
                                    DK = k.DK,
                                    Kontonavn = k.Kontonavn,
                                    Kontonr = k.Kontonr,
                                    Moms = k.Moms,
                                    Saldo = k.Saldo,
                                    Type = k.Type
                               };
            }
            else
            {
                qry_Kontoplan = from k in Program.karKontoplan
                                select k;
                qry_Kartotek = from k in Program.karKartotek
                               select new recKontoplan
                               {
                                   DK = k.DK,
                                   Kontonavn = k.Kontonavn,
                                   Kontonr = k.Kontonr,
                                   Moms = k.Moms,
                                   Saldo = k.Saldo,
                                   Type = k.Type
                               };
            }
            
            qry_Join = qry_Kontoplan.Union(qry_Kartotek);
            
            if (bFind)
            {
                qry_Kontoplan2 = from k in qry_Join
                                 where k.Kontonavn.ToUpper().Contains(strLike.ToUpper())
                                 orderby k.Kontonr ascending
                                 select k;
            }
            else
            {
                qry_Kontoplan2 = from k in qry_Join
                                orderby k.Kontonr ascending
                                select k;
            }

            qry_Kontoplan3 = from k in qry_Kontoplan2
                             where aType.Contains(k.Type)
                             orderby k.Kontonr ascending
                             select k;

            this.lvwKontoplan.Items.Clear();
            foreach (var b in qry_Kontoplan3)
            {
                ListViewItem it = lvwKontoplan.Items.Add(b.Kontonr.ToString(), b.Kontonr.ToString(), 0);
                it.SubItems.Add(b.Kontonavn);
                it.SubItems.Add(b.Moms);
            }

        }

         private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            SelectedKontonr = null;
            this.Close();
        }

        private void lvwKontoplan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwKontoplan.SelectedItems.Count == 1) 
            {
                SelectedKontonr = int.Parse(lvwKontoplan.SelectedItems[0].Name);
                SelectedMomskode = lvwKontoplan.SelectedItems[0].SubItems[2].Text;
            }
        }

        private void checkBoxDrift_CheckedChanged(object sender, EventArgs e)
        {
            getKontoplan();
        }

        private void checkBoxStatus_CheckedChanged(object sender, EventArgs e)
        {
            getKontoplan();
        }

        private void checkBoxDebitor_CheckedChanged(object sender, EventArgs e)
        {
            getKontoplan();
        }

        private void checkBoxKreditor_CheckedChanged(object sender, EventArgs e)
        {
            getKontoplan();
        }

    }
}
