using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;

namespace nsPuls3060
{
    public partial class FrmFaktura : Form
    {
        public FrmFaktura()
        {
            InitializeComponent();
        }

        private void FrmFaktura_Load(object sender, EventArgs e)
        {
            this.tblfakBindingSource.DataSource = Program.dbDataTransSumma.Tblfak;
            if (Program.karRegnskab.MomsPeriode() == 2)
            {
                this.dataGridViewTextBoxMK.Visible = false;
                this.dataGridViewTextBoxMoms.Visible = false;
                this.dataGridViewTextBoxBruttobelob.Visible = false;
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
            IEnumerable<Tblfak> qry;
            if (checkBoxVareforbrug.Checked)
            {
                checkBoxSalg.Checked = false;
                aSk[1] = "x";
                checkBoxKøb.Checked = true;
                aSk[1] = "K";
                qry = (from u in Program.dbDataTransSumma.Tblfaklin
                       where SqlMethods.Like(u.Tekst, strLike)
                       join fl in Program.dbDataTransSumma.Tblvareomkostninger on u.Konto equals fl.Kontonr
                       where fl.Omktype == "vareforb"
                       join b in Program.dbDataTransSumma.Tblfak on u.Fakpid equals b.Pid
                       where aSk.Contains(b.Sk)
                       orderby b.Dato descending
                       select b).Distinct();
            }
            else 
            {
                qry = (from u in Program.dbDataTransSumma.Tblfaklin
                       where SqlMethods.Like(u.Tekst, strLike)
                       join b in Program.dbDataTransSumma.Tblfak on u.Fakpid equals b.Pid
                       where aSk.Contains(b.Sk)
                       orderby b.Dato descending
                       select b).Distinct();
            }

            var qryAfstemte = from b in qry orderby b.Dato descending select b;

            this.tblfakBindingSource.DataSource = qryAfstemte;
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

    }
}
