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
    public partial class FrmKontoplanList : Form
    {
        public int? SelectedKontonr { get; set; }
        
        public FrmKontoplanList()
        {
            InitializeComponent();
        }
        
        public FrmKontoplanList(Point Start)
        {
            global::nsPuls3060.Properties.Settings.Default.frmKontoplanListLocation = Start;
            InitializeComponent();
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
            bool bFind = false;
            if (toolStripTextBoxFind.Text.Length > 0)
                bFind = true;

            string strLike = toolStripTextBoxFind.Text;
            IEnumerable<recKontoplan> qry_Kontoplan;
            IEnumerable<recKontoplan> qry_Kontoplan2;
            if (checkBoxMedsaldo.Checked)
            {
                qry_Kontoplan = from k in Program.karKontoplan
                                where k.Saldo != null && k.Saldo != 0
                                select k;
            }
            else
            {
                qry_Kontoplan = from k in Program.karKontoplan
                                select k;
            }

            if (bFind)
            {
                qry_Kontoplan2 = from k in qry_Kontoplan
                                 where k.Kontonavn.ToUpper().Contains(strLike.ToUpper())
                                 orderby k.Kontonr ascending
                                 select k;
            }
            else
            {
                qry_Kontoplan2 = from k in qry_Kontoplan
                                orderby k.Kontonr ascending
                                select k;
            }

            this.lvwKontoplan.Items.Clear();
            foreach (var b in qry_Kontoplan2)
            {
                ListViewItem it = lvwKontoplan.Items.Add(b.Kontonr.ToString(), b.Kontonr.ToString(), 0);
                it.SubItems.Add(b.Kontonavn);
                it.SubItems.Add(b.Moms);
            }

        }

         private void cmdOK_Click(object sender, EventArgs e)
        {
            SelectedKontonr = 54321;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            SelectedKontonr = null;
            this.Close();
        }

    }
}
