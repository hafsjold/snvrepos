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
    public partial class FrmVareList : Form
    {
        public int? SelectedVarenr { get; set; }
        public string SelectedVarenavn { get; set; }

        public FrmVareList()
        {
            InitializeComponent();
        }

        public FrmVareList(Point Start, KontoType ktp)
        {
            global::nsPuls3060.Properties.Settings.Default.frmVareListLocation = Start;
            InitializeComponent();
        }

        private void FrmVareList_Load(object sender, EventArgs e)
        {
            getVarer();
        }

        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            getVarer();
        }

        private void getVarer()
        {
            bool bFind = false;
            if (toolStripTextBoxFind.Text.Length > 0)
                bFind = true;

            string strLike = toolStripTextBoxFind.Text;
            IEnumerable<recVarer> qry_Varer;

            if (bFind)
            {
                qry_Varer = from k in Program.karVarer
                            where k.Varenavn.ToUpper().Contains(strLike.ToUpper())
                            orderby k.Varenr ascending
                            select k;
            }
            else
            {
                qry_Varer = from k in Program.karVarer
                            orderby k.Varenr ascending
                            select k;
            }

            this.lvwVarer.Items.Clear();
            foreach (var b in qry_Varer)
            {
                ListViewItem it = lvwVarer.Items.Add(b.Varenr.ToString(), b.Varenr.ToString(), 0);
                it.SubItems.Add(b.Varenavn);
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            SelectedVarenr = null;
            this.Close();
        }

        private void lvwVarer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwVarer.SelectedItems.Count == 1)
            {
                SelectedVarenr = int.Parse(lvwVarer.SelectedItems[0].Name);
                SelectedVarenavn = lvwVarer.SelectedItems[0].SubItems[1].Text;
            }
        }

    }
}
