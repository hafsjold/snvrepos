using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trans2SummaHDC
{
    public partial class FrmVarekontoType : Form
    {
        public FrmVarekontoType()
        {
            InitializeComponent();
        }

        private void FrmVarekontoType_Load(object sender, EventArgs e)
        {
            this.tblvareomkostningerBindingSource.DataSource = Program.dbDataTransSumma.tblvareomkostningers;
        }

        private void FrmVarekontoType_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void kontonrTextBox_TextChanged(object sender, EventArgs e)
        {
            int kontonr;
            if (int.TryParse(kontonrTextBox.Text, out kontonr))
            {
                labelKontotekst.Text = KarKontoplan.getKontonavn(kontonr);
            }
            else
                labelKontotekst.Text = "Konto findes ikke";
        }

        private void kontonrTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point startPoint = kontonrTextBox.PointToScreen(new Point(e.X, e.Y));
                FrmKontoplanList m_frmKontoplanList = new FrmKontoplanList(startPoint, KontoType.Drift);
                m_frmKontoplanList.ShowDialog();
                int? selectedKontonr = m_frmKontoplanList.SelectedKontonr;
                m_frmKontoplanList.Close();
                if (selectedKontonr != null)
                {
                    kontonrTextBox.Text = selectedKontonr.ToString();
                }
            }
        }
    }
}
