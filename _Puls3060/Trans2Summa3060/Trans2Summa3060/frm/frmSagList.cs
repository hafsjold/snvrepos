using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trans2Summa3060
{
    public partial class FrmSagList : Form
    {
        public int? SelectedSagnr { get; set; }

        public FrmSagList()
        {
            InitializeComponent();
        }

        public FrmSagList(Point Start)
        {
            global::Trans2Summa3060.Properties.Settings.Default.frmSagListLocation = Start;
            InitializeComponent();
        }

        private void getSag()
        {
            bool bFind = false;
            if (toolStripTextBoxFind.Text.Length > 0)
                bFind = true;

            string strLike = toolStripTextBoxFind.Text;
            IEnumerable<recSag> qry_Sag;
            IEnumerable<recSag> qry_Sag2;
            if (checkBoxMedsaldo.Checked)
            {
                qry_Sag = from k in Program.karSag
                                //where k.Saldo != null && k.Saldo != 0
                                select k;
             }
            else
            {
                qry_Sag = from k in Program.karSag
                                select k;
            }

            if (bFind)
            {
                qry_Sag2 = from k in qry_Sag
                                 where k.Sagnavn.ToUpper().Contains(strLike.ToUpper())
                                 orderby k.Sagnr ascending
                                 select k;
            }
            else
            {
                qry_Sag2 = from k in qry_Sag
                                 orderby k.Sagnr ascending
                                 select k;
            }

 
            this.lvwSag.Items.Clear();
            foreach (var b in qry_Sag2)
            {
                ListViewItem it = lvwSag.Items.Add(b.Sagnr.ToString(), b.Sagnr.ToString(), 0);
                it.SubItems.Add(b.Sagnavn);
            }

        }

        private void FrmSagList_Load(object sender, EventArgs e)
        {
            getSag();
        }

        private void checkBoxMedsaldo_CheckedChanged(object sender, EventArgs e)
        {
            getSag();
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            getSag();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            SelectedSagnr = null;
            this.Close();
        }

        private void lvwSag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwSag.SelectedItems.Count == 1)
            {
                SelectedSagnr = int.Parse(lvwSag.SelectedItems[0].Name);
             }
        }
    }
}
