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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X + 50, this.Location.Y);
        }
    }
}
