using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace BJDoc2
{
    public partial class FrmBruger : Form
    {
        private dbBuusjensenEntities db;

        public FrmBruger()
        {
            InitializeComponent();
            db = new dbBuusjensenEntities();
        }

        private void FrmBruger_Load(object sender, EventArgs e)
        {
            db.tblBrugers.Load();
            this.bsBrugers.DataSource = db.tblBrugers.Local;
        }

        private void FrmBruger_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
