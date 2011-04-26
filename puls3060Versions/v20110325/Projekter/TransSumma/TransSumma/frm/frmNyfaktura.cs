using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace nsPuls3060
{
    public partial class FrmNyfaktura : Form
    {
        public FrmNyfaktura()
        {
            InitializeComponent();
        }

        private void FrmNyfaktura_Load(object sender, EventArgs e)
        {
            this.tblwfakBindingSource.DataSource = Program.dbDataTransSumma.Tblwfak;
        }

        private void FrmNyfaktura_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void tblwfaklinDataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.C && e.Control)
            {
                this.copyToClipboard();
                e.Handled = true; //otherwise the control itself tries to “copy”
            }
            if (e.KeyCode == Keys.V && e.Control)
            {
                this.pasteFromClipboard();
            }
        }

        private void tblwfaklinDataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = tblwfaklinDataGridView.HitTest(e.X, e.Y);
                int hitcol = hit.ColumnIndex;
                if (hit.Type == DataGridViewHitTestType.Cell && hit.ColumnIndex == 4)
                {
                    tblwfaklinDataGridView.ClearSelection();
                    tblwfaklinDataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    Point startPoint = tblwfaklinDataGridView.PointToScreen(new Point(e.X, e.Y));
                    FrmKontoplanList m_frmKontoplanList = new FrmKontoplanList(startPoint);
                    m_frmKontoplanList.ShowDialog();
                    int? selectedKontonr = m_frmKontoplanList.SelectedKontonr;
                    string selectedMomskode = m_frmKontoplanList.SelectedMomskode;
                    m_frmKontoplanList.Close();
                    if (selectedKontonr != null)
                    {
                        Tblwfaklin recWfaklin = ((DataGridView)sender).Rows[hit.RowIndex].DataBoundItem as Tblwfaklin;
                        if (recWfaklin != null)
                        {
                            recWfaklin.Konto = selectedKontonr;
                            recWfaklin.Momskode = selectedMomskode;
                        }
                    }
                }
                else if (hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    this.contextMenuLineCopyPaste.Show(this.tblwfaklinDataGridView, new Point(e.X, e.Y));
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.copyToClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pasteFromClipboard();
        }

        private void copyToClipboard()
        {
            IDataObject clipboardData = getDataObject();
            Clipboard.SetDataObject(clipboardData);
        }

        private IDataObject getDataObject()
        {
            DataObject clipboardData = this.tblwfaklinDataGridView.GetClipboardContent();
            return clipboardData;
        }

        private void pasteCsv(IDataObject dataObject)
        {
            object lDataObjectGetData = dataObject.GetData(DataFormats.CommaSeparatedValue);
            string csv = lDataObjectGetData as string;
            if (csv == null)
            {
                System.IO.MemoryStream stream = lDataObjectGetData as System.IO.MemoryStream;
                if (stream != null)
                    csv = new System.IO.StreamReader(stream).ReadToEnd();
            }
            if (csv == null)
                return;

            Regex regexCommaCvs = new Regex(@"""(.*?)"",|([^,]*),|(.*)$");
            Regex regexSimicolonCvs = new Regex(@"""(.*?)"";|([^;]*);|(.*)$");
            string[] sep = { "\r\n", "\n" };
            string[] lines = csv.TrimEnd('\0').Split(sep, StringSplitOptions.RemoveEmptyEntries);
            int row = tblwfaklinDataGridView.NewRowIndex;
            Tblwfak recWfak = (Tblwfak)tblwfakBindingSource.Current;
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    try
                    {
                        int i = 0;
                        int iMax = 12;
                        string[] value = new string[iMax];
                        foreach (Match m in regexCommaCvs.Matches(line))
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                if (m.Groups[j].Success)
                                {
                                    if (i < iMax)
                                    {
                                        value[i++] = m.Groups[j].ToString();
                                        break;
                                    }
                                }
                            }
                        }

                        if (value[3] == null) //konto
                        {
                            i = 0;
                            value = new string[iMax];
                            foreach (Match m in regexSimicolonCvs.Matches(line))
                            {
                                for (int j = 1; j <= 3; j++)
                                {
                                    if (m.Groups[j].Success)
                                    {
                                        if (i < iMax)
                                        {
                                            value[i++] = m.Groups[j].ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (value[3] != null) //konto
                        {
                            Tblwfaklin  recWfaklin = new Tblwfaklin
                            {
                                Varenr = value[1],
                                Tekst = value[2],
                                Konto = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? int.Parse(value[3]) : (int?)null,
                                Momskode = value[4],
                                Antal = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? int.Parse(value[5]) : (int?)null,
                                Enhed = value[6],
                                Pris = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                                Moms = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? decimal.Parse(value[8]) : (decimal?)null,
                                Nettobelob = Microsoft.VisualBasic.Information.IsNumeric(value[9]) ? decimal.Parse(value[9]) : (decimal?)null,
                                Bruttobelob = Microsoft.VisualBasic.Information.IsNumeric(value[10]) ? decimal.Parse(value[10]) : (decimal?)null
                            };
                            tblwfaklinBindingSource.Insert(row, recWfaklin);
                        }
                        row++;
                    }
                    catch (FormatException)
                    {    //TODO: log exceptions using a nice standard logging library  
                        tblwfaklinDataGridView.CancelEdit();
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void pasteFromClipboard()
        {
            if (tblwfaklinDataGridView.SelectedRows.Contains(tblwfaklinDataGridView.Rows[tblwfaklinDataGridView.NewRowIndex]))
            {
                tblwfaklinDataGridView.Rows[tblwfaklinDataGridView.NewRowIndex].Selected = false;
                tblwfaklinDataGridView.CancelEdit();
            }
            IDataObject clipboardData = Clipboard.GetDataObject();
            if (Clipboard.ContainsData(DataFormats.CommaSeparatedValue))
            {
                pasteCsv(clipboardData);
            }
        }

        private void tblwfakBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Program.dbDataTransSumma.SubmitChanges();
            }
            catch { }
        }

        private void FakturaTilSummaSummarumToolStripButton_Click(object sender, EventArgs e)
        {
            clsFaktura objFaktura = new clsFaktura();
            objFaktura.SalgsOrder2Summa((IList<Tblwfak>)this.tblwfakBindingSource.List);
            objFaktura.KøbsOrder2Summa((IList<Tblwfak>)this.tblwfakBindingSource.List);
            objFaktura = null;

            int iMax = this.tblwfakBindingSource.List.Count - 1;
            for (int i = iMax; i >= 0; i--)
            {
                this.tblwfakBindingSource.List.RemoveAt(i);
            }
        }


    }
}
