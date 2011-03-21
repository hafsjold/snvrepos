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
    public partial class FrmKontoplanList : Form
    {
        public Point MyPoint { get; set; }
        
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
            IEnumerable<recKontoplan> qry_Kontoplan;
            qry_Kontoplan = from k in Program.karKontoplan
                            where k.Saldo != null && k.Saldo != 0
                           orderby k.Kontonr ascending
                           select k;
            foreach (var b in qry_Kontoplan)
            {
                ListViewItem it = lvwKontoplan.Items.Add(b.Kontonr.ToString(), b.Kontonr.ToString(), 0);
                it.SubItems.Add(b.Kontonavn);
                it.SubItems.Add(b.Moms);
            }

        }

    }
}
