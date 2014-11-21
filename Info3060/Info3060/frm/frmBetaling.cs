using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nsInfo3060
{
    public partial class FrmBetaling : Form
    {
        public FrmBetaling()
        {
            InitializeComponent();
        }

        private void FrmBetaling_Load(object sender, EventArgs e)
        {
            this.tblbetalingsidentifikationBindingSource.DataSource = Program.dbData3060.tblbetalingsidentifikations;
            this.tblMedlemBindingSource.DataSource = Program.dbData3060.tblMedlems;
        }

        private void FrmBetaling_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dbData3060.SubmitChanges();
        }

        private void tblbetalingsidentifikationDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = tblbetalingsidentifikationDataGridView.HitTest(e.X, e.Y);
                int hitcol = hit.ColumnIndex;
                 if (hit.Type == DataGridViewHitTestType.Cell && hit.ColumnIndex == 1)
                {
                    tblbetalingsidentifikationDataGridView.ClearSelection();
                    tblbetalingsidentifikationDataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    Point startPoint = tblbetalingsidentifikationDataGridView.PointToScreen(new Point(e.X, e.Y));

                    FrmMedlemList m_frmKontoplanList = new FrmMedlemList(startPoint, KontoType.Drift | KontoType.Status | KontoType.Debitor | KontoType.Kreditor);
                    m_frmKontoplanList.ShowDialog();
                    int? selectedNr = m_frmKontoplanList.SelectedNr;
                    string selectedNavn = m_frmKontoplanList.SelectedNavn;
                    m_frmKontoplanList.Close();
                    if (selectedNr != null)
                    {
                        tblbetalingsidentifikation recWkladder = ((DataGridView)sender).Rows[hit.RowIndex].DataBoundItem as tblbetalingsidentifikation;
                        if (recWkladder != null)
                        {
                            recWkladder.Nr = selectedNr;
                            recWkladder.Navn = selectedNavn;
                        }
                    }

                }

            }
        }
    }
}
