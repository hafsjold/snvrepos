using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace nsHafsjoldData
{
    public partial class FrmNyKladde : Form
    {
        public FrmNyKladde()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {
            bsXWbilag.DataSource = from b in Program.dbHafsjoldData.Tblwbilag orderby b.Pid select b;
            bsXWbilag.MoveLast();
        }

        private void TestMD_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void FrmNyKladde_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dbHafsjoldData.SubmitChanges();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.bilagTextBox.Text = ((int)(++Program.BS1_SidsteNr)).ToString();
        }

        private void KassekladdeButton_Click(object sender, EventArgs e)
        {
            var wkladder = from b in Program.dbHafsjoldData.Tblwbilag
                           join k in Program.dbHafsjoldData.Tblwkladder on b.Pid equals k.Bilagpid into details
                           from k in details.DefaultIfEmpty()
                           orderby k.Pid
                           select new
                           {
                               b.Bilag,
                               b.Dato,
                               k.Tekst,
                               k.Afstemningskonto,
                               k.Belob,
                               k.Konto,
                               k.Momskode,
                               k.Faktura
                           };
            
            foreach (var b in wkladder)
            {
                recKladde kl = new recKladde
                {
                    Dato = b.Dato,
                    Bilag = b.Bilag,
                    Tekst = b.Tekst,
                    Afstemningskonto = b.Afstemningskonto,
                    Belob = b.Belob,
                    Kontonr = b.Konto,
                    //Momskode = b.Momskode,
                    Faknr = b.Faktura
                };
                Program.karKladde.Add(kl);
            }
            Program.karKladde.save();
            Program.dbHafsjoldData.ExecuteCommand("DELETE [Tblwbilag];");
            RefreshData();
        }
    }
}
