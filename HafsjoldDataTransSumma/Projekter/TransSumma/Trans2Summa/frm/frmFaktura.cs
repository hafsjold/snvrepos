using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;

namespace Trans2Summa
{
    public partial class FrmFaktura : Form
    {
        public FrmFaktura()
        {
            InitializeComponent();
        }

        private void FrmFaktura_Load(object sender, EventArgs e)
        {
            this.bsTblfak.DataSource = Program.dbDataTransSumma.tblfaks;
            if (Program.karRegnskab.MomsPeriode() == 2)
            {
                this.momskodeDataGridViewTextBoxColumn.Visible = false;
                this.momsDataGridViewTextBoxColumn.Visible = false;
                this.bruttobelobDataGridViewTextBoxColumn.Visible = false;
            }
        }

        private void FrmFaktura_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void tblfaklinDataGridView_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.C && e.Control)
            {
                this.copyToClipboard();
                e.Handled = true; //otherwise the control itself tries to “copy”
            }
        }

        private void copyToClipboard()
        {
            IDataObject clipboardData = getDataObject();
            Clipboard.SetDataObject(clipboardData);
        }

        private IDataObject getDataObject()
        {
            DataObject clipboardData = this.tblfaklinDataGridView.GetClipboardContent();
            return clipboardData;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.copyToClipboard();
        }

        private void cmdSog_Click(object sender, EventArgs e)
        {
            Sog();
        }

        private void Sog()
        {
            String[] aSk = { "S", "K" };
            if (!checkBoxSalg.Checked)
                aSk[0] = "x";
            if (!checkBoxKøb.Checked)
                aSk[1] = "y";
            string strLike = "%" + textBoxSogeord.Text + "%";
            IEnumerable<tblfak> qry;
            if (checkBoxVareforbrug.Checked)
            {
                checkBoxSalg.Checked = false;
                aSk[1] = "x";
                checkBoxKøb.Checked = true;
                aSk[1] = "K";
                qry = (from u in Program.dbDataTransSumma.tblfaklins
                       where SqlMethods.Like(u.tekst, strLike)
                       join fl in Program.dbDataTransSumma.tblvareomkostningers on u.konto equals fl.kontonr
                       where fl.omktype == "vareforb"
                       join b in Program.dbDataTransSumma.tblfaks on u.fakpid equals b.pid
                       where aSk.Contains(b.sk)
                       orderby b.dato descending
                       select b).Distinct();
            }
            else
            {
                qry = (from u in Program.dbDataTransSumma.tblfaklins
                       where SqlMethods.Like(u.tekst, strLike)
                       join b in Program.dbDataTransSumma.tblfaks on u.fakpid equals b.pid
                       where aSk.Contains(b.sk)
                       orderby b.dato descending
                       select b).Distinct();
            }

            var qryAfstemte = from b in qry orderby b.dato descending select b;

            this.bsTblfak.DataSource = qryAfstemte;
        }

        private void kontoTextBox_TextChanged(object sender, EventArgs e)
        {
            getKontonavn();
        }

        private void getKontonavn()
        {
            int Konto;
            if (int.TryParse(kontoTextBox.Text, out Konto))
            {
                try
                {
                    labelKontonavn.Text = (from k in Program.karKartotek where k.Kontonr == Konto select k.Kontonavn).First();
                }
                catch
                {
                    labelKontonavn.Text = "Skal udfyldes";
                }
            }
            else
                labelKontonavn.Text = "Skal udfyldes";
        }

        private void checkBoxKøb_CheckedChanged(object sender, EventArgs e)
        {
            Sog();
        }

        private void checkBoxSalg_CheckedChanged(object sender, EventArgs e)
        {
            Sog();
        }

        private void checkBoxVareforbrug_CheckedChanged(object sender, EventArgs e)
        {
            Sog();
        }

        private void cmdKopier_Click(object sender, EventArgs e)
        {
            FrmMain frmMain = this.ParentForm as FrmMain;
            try
            {
                FrmNyfaktura frmNyfaktura = frmMain.GetChild("Ny faktura") as FrmNyfaktura;
                tblfak recFak = this.bsTblfak.Current as tblfak;
                frmNyfaktura.AddNyFaktura(recFak);
            }
            catch { }
        }

    }
}
