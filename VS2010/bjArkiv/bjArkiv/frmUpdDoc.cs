using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bjArkiv
{
    public partial class frmUpdDoc : Form
    {
        public string arkivpath { get; set; }
        private xmldocs db = null;

        public frmUpdDoc()
        {
            InitializeComponent();
        }

        private void frmUpdDoc_Load(object sender, EventArgs e)
        {
            db = xmldocs.Load(arkivpath + Program.BJARKIV);
            xmldocsBindingSource.DataSource = db;    
        }

        private void frmUpdDoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Save();
        }
    }
}
