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

namespace Trans2SummaHDC
{
    public partial class FrmBankkontoudtog : Form
    {
        private tblkontoudtog m_recKontoudtog;

        public FrmBankkontoudtog()
        {
            InitializeComponent();
        }

        private void FrmKladder_Load(object sender, EventArgs e)
        {
            this.tbltemplateBindingSource.DataSource = Program.dbDataTransSumma.tbltemplates;
            this.tblkontoudtogBindingSource.DataSource = Program.dbDataTransSumma.tblkontoudtogs;
            m_recKontoudtog = (from w in Program.dbDataTransSumma.tblkontoudtogs select w).First();
            setBindingsorces();
        }

        private void setKontoudtog(int bankkontoid)
        {
            try
            {
                m_recKontoudtog = (from w in Program.dbDataTransSumma.tblkontoudtogs where w.pid == bankkontoid select w).First();
            }
            catch
            {
                m_recKontoudtog = null;
            }
        }

        private void setBindingsorces()
        {
            var qryAfstemte = from u in Program.dbDataTransSumma.tblbankkontos
                              where u.afstem != null && u.bankkontoid == m_recKontoudtog.pid && (u.skjul == null || u.skjul == false)
                              orderby u.dato descending
                              select u;
            this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte;

            var qryUafstemte = from u in Program.dbDataTransSumma.tblbankkontos
                               where u.afstem == null && u.bankkontoid == m_recKontoudtog.pid && (u.skjul == null || u.skjul == false)
                               orderby u.dato ascending
                               select u;
            this.tblbankkontoBindingSourceUafstemte.DataSource = qryUafstemte;
        }

        public tblbankkonto GetrecBankkonto()
        {
            tblbankkonto recBankkonto = null;
            try
            {
                recBankkonto = this.tblbankkontoBindingSourceUafstemte.Current as tblbankkonto;
            }
            catch { }
            return recBankkonto;
        }

        private void cmdSog_Click(object sender, EventArgs e)
        {
            string strLike = "%" + textBoxSogeord.Text + "%";
            var qryAfstemte = from u in Program.dbDataTransSumma.tblbankkontos
                              where u.afstem != null && (u.skjul == null || u.skjul == false) && SqlMethods.Like(u.tekst, strLike)
                              orderby u.dato descending
                              select u;
            this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte;
        }

        private void tblbankkonto2DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                FrmMain frmMain = this.ParentForm as FrmMain;
                FrmKladder frmKladder = frmMain.GetChild("Kladder") as FrmKladder;

                tblbankkonto recBankkonto = this.tblbankkontoBindingSourceAfstemte.Current as tblbankkonto;
                int? Bilagpid = (from t in Program.dbDataTransSumma.tbltrans
                                 where t.afstem == recBankkonto.afstem
                                 select t.bilagpid).First();
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
                tblbankkonto recBankkonto = tblbankkontoBindingSourceUafstemte.Current as tblbankkonto;
                this.textBoxSogeord.Text = recBankkonto.tekst;
                string strLike = "%" + this.textBoxSogeord.Text + "%";
                var qryAfstemte = from u in Program.dbDataTransSumma.tblbankkontos
                                  where u.afstem != null && u.bankkontoid == m_recKontoudtog.pid && (u.skjul == null || u.skjul == false) && SqlMethods.Like(u.tekst, strLike)
                                  orderby u.dato descending
                                  select u;
                this.tblbankkontoBindingSourceAfstemte.DataSource = qryAfstemte;
            }
            catch { }
        }

        private void cbBankkonto_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int pid = (int)cbBankkonto.SelectedValue;
                if (pid != m_recKontoudtog.pid)
                {
                    setKontoudtog(pid);
                    setBindingsorces();
                    tblbankkontoBindingSourceUafstemte_PositionChanged(sender, e);
                }
            }
            catch { }
        }

        private void cmdPrivat_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = this.ParentForm as FrmMain;
            try
            {
                FrmNyekladder frmNyekladder = frmMain.GetChild("Nye kladder") as FrmNyekladder;
                tbltemplate recTemplate = this.tbltemplateBindingSource.Current as tbltemplate;
                tblbankkonto recBankkonto = this.tblbankkontoBindingSourceUafstemte.Current as tblbankkonto;
                frmNyekladder.AddNyTemplateKladde(recTemplate, recBankkonto);
            }
            catch { }
        }

    }

}