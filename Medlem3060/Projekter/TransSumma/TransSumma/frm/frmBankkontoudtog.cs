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
            var qryAfstemte = from u in Program.dbDataTransSumma.Tblbankkonto
                              where u.Afstem != null && (u.Skjul == null || u.Skjul == false)
                              orderby u.Dato descending
                              select u;
            this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte; 
            
            var qryUafstemte = from u in Program.dbDataTransSumma.Tblbankkonto
                               where u.Afstem == null && (u.Skjul == null || u.Skjul == false)
                               orderby u.Dato ascending
                               select u;
            this.tblbankkontoBindingSourceUafstemte.DataSource = qryUafstemte;
        }

        public Tblbankkonto GetrecBankkonto() 
        {
            Tblbankkonto recBankkonto = null;
            try
            {
                recBankkonto = this.tblbankkontoBindingSourceUafstemte.Current as Tblbankkonto;
            }
            catch { }
            return recBankkonto;
        }

        private void cmdSog_Click(object sender, EventArgs e)
        {
            string strLike = "%" + textBoxSogeord.Text + "%";
            var qryAfstemte = from u in Program.dbDataTransSumma.Tblbankkonto
                              where u.Afstem != null && (u.Skjul == null || u.Skjul == false) && SqlMethods.Like(u.Tekst, strLike)
                              orderby u.Dato descending
                              select u;
            this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte;
        }

        private void tblbankkonto2DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = this.ParentForm as FrmMain;
                FrmKladder frmKladder = frmMain.GetChild("Kladder") as FrmKladder;
                
                Tblbankkonto recBankkonto = this.tblbankkontoBindingSourceAfstemte.Current as Tblbankkonto;
                int? Bilagpid = (from t in Program.dbDataTransSumma.Tbltrans
                                 where t.Afstem == recBankkonto.Afstem
                                 select t.Bilagpid).First();
                frmKladder.findBilag(Bilagpid);
            }
            catch
            {
            }
        }

        private void tblbankkontoBindingSourceUafstemte_PositionChanged(object sender, EventArgs e)
        {
            try
            {
                Tblbankkonto recBankkonto = tblbankkontoBindingSourceUafstemte.Current as Tblbankkonto;
                this.textBoxSogeord.Text = recBankkonto.Tekst;
                string strLike = "%" + this.textBoxSogeord.Text + "%";
                var qryAfstemte = from u in Program.dbDataTransSumma.Tblbankkonto
                                  where u.Afstem != null && (u.Skjul == null || u.Skjul == false) && SqlMethods.Like(u.Tekst, strLike)
                                  orderby u.Dato descending
                                  select u;
                this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte;
            }
            catch {}
        }

    }

}