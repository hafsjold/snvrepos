﻿using System;
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
            if (Program.karRegnskab.MomsPeriode() == 2)
                this.MKdataGridViewTextBoxColumn.Visible = false;
        }

        private void copyMenuLineCopyPastItem_Click(object sender, EventArgs e)
        {
            this.copyToClipboard();
        }
        
        private void copyToClipboard()
        {
            IDataObject clipboardData = getDataObject();
            Clipboard.SetDataObject(clipboardData);
        }

        private IDataObject getDataObject()
        {
            DataObject clipboardData = this.tblkladderDataGridView.GetClipboardContent();
            return clipboardData;
        }

        private void bilagTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmdSog_Click(object sender, EventArgs e)
        {
            string strLike = "%" + textBoxSogeord.Text + "%";
            var qry = (from u in Program.dbDataTransSumma.Tbltrans
                       where SqlMethods.Like(u.Tekst, strLike)
                       join b in Program.dbDataTransSumma.Tblbilag on u.Bilagpid equals b.Pid
                       orderby b.Dato descending
                       select b).Distinct();
            var qryAfstemte = from b in qry orderby b.Dato descending select b;

            this.tblbilagBindingSource.DataSource = qryAfstemte;
        }

        public void findBilag(int? Bilagpid)
        {
            var qryAfstemte = from b in Program.dbDataTransSumma.Tblbilag
                              where b.Pid == Bilagpid
                              select b;

            this.tblbilagBindingSource.DataSource = qryAfstemte;
        }

        private void cmdKopier_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = this.ParentForm as FrmMain;
            Tblbankkonto recBankkonto = null;
            try
            {
                FrmBankkontoudtog frmBankkontoudtog = frmMain.GetChild("Bank kontoudtog") as FrmBankkontoudtog;
                recBankkonto = frmBankkontoudtog.GetrecBankkonto();
            }
            catch { }

            try
            {
                FrmNyekladder frmNyekladder = frmMain.GetChild("Nye kladder") as FrmNyekladder;
                Tblbilag recBilag = this.tblbilagBindingSource.Current as Tblbilag;
                frmNyekladder.AddNyKladde(recBilag, recBankkonto);
            }
            catch { }
        }

    }

}