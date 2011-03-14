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
    public partial class FrmKladder : Form
    {
        public FrmKladder()
        {
            InitializeComponent();
        }

        private void FrmKladder_Load(object sender, EventArgs e)
        {
            this.tblbilagBindingSource.DataSource = Program.dbDataTransSumma.Tblbilag;
        }

        private void bilagTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdSog_Click(object sender, EventArgs e)
        {
            string strLike = "%" + textBoxSogeord.Text + "%";
            var qry= (from u in Program.dbDataTransSumma.Tbltrans
                              where SqlMethods.Like(u.Tekst, strLike)
                              join b in Program.dbDataTransSumma.Tblbilag on u.Bilagpid equals b.Pid
                              orderby b.Dato descending
                              select b).Distinct();
            var qryAfstemte = from b in qry orderby b.Dato descending select b;

            this.tblbilagBindingSource.DataSource = qryAfstemte;
        }

        public void findBilag(int? Bilagpid)
        {
            var qryAfstemte = from  b in Program.dbDataTransSumma.Tblbilag
                              where b.Pid == Bilagpid
                              select b;

            this.tblbilagBindingSource.DataSource = qryAfstemte;
        }

    }

}