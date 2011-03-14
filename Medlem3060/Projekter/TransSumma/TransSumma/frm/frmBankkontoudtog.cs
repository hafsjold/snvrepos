using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;

namespace nsPuls3060
{
    public partial class FrmBankkontoudtog : Form
    {
        public FrmBankkontoudtog()
        {
            InitializeComponent();
        }

        private void FrmKladder_Load(object sender, EventArgs e)
        {
            var qryUafstemte = from u in Program.dbDataTransSumma.Tblbankkonto
                               where u.Afstem == null && u.Skjul == null
                               select u;
            this.tblbankkontoBindingSourceUafstemte.DataSource = qryUafstemte;
            var qryAfstemte = from u in Program.dbDataTransSumma.Tblbankkonto
                               where u.Afstem != null && u.Skjul == null
                               select u;
            this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte;
            
        }

        private void cmdSog_Click(object sender, EventArgs e)
        {
            string strLike = "%" + textBoxSogeord.Text + "%";
            var qryAfstemte = from u in Program.dbDataTransSumma.Tblbankkonto
                              where u.Afstem != null && u.Skjul == null && SqlMethods.Like(u.Tekst, strLike)
                              select u;
            this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte;
        }

    }

}