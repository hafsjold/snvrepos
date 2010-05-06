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
    public partial class FrmBogfortKladde : Form
    {

        public FrmBogfortKladde()
        {
            InitializeComponent();
        }

        private void frmBogfortKladde_Load(object sender, EventArgs e)
        {
            tblbilagBindingSource.DataSource = from b in Program.dbHafsjoldData.Tblbilag 
                                               where b.Bilag > 0
                                               orderby b.Dato descending
                                               select b;
        }

        private void findBotton_Click(object sender, EventArgs e)
        {
            var qry = 
                      (from b in Program.dbHafsjoldData.Tblbilag
                      where b.Bilag > 0
                      join t in Program.dbHafsjoldData.Tbltrans on b.Pid equals t.Bilagpid into details
                      from t in details.DefaultIfEmpty()
                      where t.Tekst.Contains(this.FindTextBox.Text.ToString())
                      select b).Distinct();

            tblbilagBindingSource.DataSource = from b in qry orderby b.Dato descending select b;

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Tblwbilag rec_wbilag = new Tblwbilag
            {
                Bilag = ++Program.BS1_SidsteNr,
                Dato = DateTime.Now
            };
            Program.dbHafsjoldData.Tblwbilag.InsertOnSubmit(rec_wbilag);

            int Pid = int.Parse(this.AddButton.Tag.ToString());
            var qry = from k in Program.dbHafsjoldData.Tblkladder
                      where k.Bilagpid == Pid
                      orderby k.Id
                      select k;

            foreach (Tblkladder rec_kladder in qry)
            {
                Tblwkladder rec_wkladder = new Tblwkladder
                {
                    Tekst = rec_kladder.Tekst,
                    Afstemningskonto = rec_kladder.Afstemningskonto,
                    Belob = rec_kladder.Belob,
                    Konto = rec_kladder.Konto,
                    Momskode = rec_kladder.Momskode,
                    Faktura = rec_kladder.Faktura
                };
                rec_wbilag.Tblwkladder.Add(rec_wkladder);
            }
            Program.dbHafsjoldData.SubmitChanges();

            Program.frmNyKladde.RefreshData();

        }

    }
}
