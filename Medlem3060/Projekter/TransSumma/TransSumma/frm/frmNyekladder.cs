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
    public partial class FrmNyekladder : Form
    {
        public FrmNyekladder()
        {
            InitializeComponent();
        }

        private void FrmNyekladder_Load(object sender, EventArgs e)
        {
            this.tblwbilagBindingSource.DataSource = Program.dbDataTransSumma.Tblwbilag;
            this.karKontoplanBindingSource.DataSource = Program.karKontoplan;
            this.karAfstemningskontiBindingSource.DataSource = Program.karAfstemningskonti;
        }

        private void FrmNyekladder_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void tblwbilagBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cutToClipboard();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.copyToClipboard();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pasteFromClipboard();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            cutToClipboard();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            copyToClipboard();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            pasteFromClipboard();
        }

        private void deleteCurrentSelection()
        {
            this.tblwkladderBindingSource.RemoveCurrent();
        }

        private void cutToClipboard()
        {
            copyToClipboard();
            deleteCurrentSelection();
        }

        private void copyToClipboard()
        {
            IDataObject clipboardData = getDataObject();
            Clipboard.SetDataObject(clipboardData);
        }

        private IDataObject getDataObject()
        {
            DataObject clipboardData = this.tblwkladderDataGridView.GetClipboardContent();
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
            string[] sep = {"\r\n","\n"};
            string[] lines = csv.TrimEnd('\0').Split(sep, StringSplitOptions.RemoveEmptyEntries);
            int row = tblwkladderDataGridView.NewRowIndex;
            Tblwbilag recWbilag = (Tblwbilag)tblwbilagBindingSource.Current;
            foreach (string line in lines)
            {
                if (row < tblwkladderDataGridView.RowCount && line.Length > 0)
                {
                    try
                    {
                        int i = 0;
                        int iMax = 8;
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
                        
                        if (value[3] == null) //belob
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
                        
                        if (value[3] != null) //belob
                        {
                            Tblwkladder recWkladder = new Tblwkladder
                                {
                                    Bilagpid = recWbilag.Pid,
                                    Tekst = value[1],
                                    Afstemningskonto = value[2],
                                    Belob = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                                    Konto = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? int.Parse(value[4]) : (int?)null,
                                    Momskode = value[5],
                                    Faktura = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? int.Parse(value[6]) : (int?)null
                                };
                            tblwkladderBindingSource.Add(recWkladder);
                        }
                        row++;
                    }
                    catch (FormatException)
                    {    //TODO: log exceptions using a nice standard logging library  
                        tblwkladderDataGridView.CancelEdit();
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
            if (tblwkladderDataGridView.SelectedRows.Contains(tblwkladderDataGridView.Rows[tblwkladderDataGridView.NewRowIndex]))
            {
                tblwkladderDataGridView.Rows[tblwkladderDataGridView.NewRowIndex].Selected = false;
                tblwkladderDataGridView.CancelEdit();
            }
            IDataObject clipboardData = Clipboard.GetDataObject();
            if (Clipboard.ContainsData(DataFormats.CommaSeparatedValue))
            {
                pasteCsv(clipboardData);
            }
        }
     
        private void tblwkladderDataGridView_KeyDown(object sender, KeyEventArgs e)
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

        void tblwkladderDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
                cbo.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        private void tblwkladderDataGridView_CellErrorTextNeeded(object sender, DataGridViewCellErrorTextNeededEventArgs e)
        {
            //DataGridViewCell cell = tblwkladderDataGridView.CurrentCell;
            //object yyy = tblwkladderDataGridView.Rows[1].Cells[2];
        }

        private void tblwkladderDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // If the data source raises an exception when a cell value is 
            // commited, display an error message.
            if (e.Exception != null && e.Context == (DataGridViewDataErrorContexts.Parsing | DataGridViewDataErrorContexts.Commit))
            {
                MessageBox.Show(e.Exception.Message);
            }

            if (e.Exception != null && e.Context == (DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.Display))
            {
                MessageBox.Show(e.Exception.Message);
            }

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            var qry = from k in Program.dbDataTransSumma.Tblwkladder
                      join b in Program.dbDataTransSumma.Tblwbilag on k.Bilagpid equals b.Pid 
                      select new recKladde 
                      {
                        Dato = b.Dato,
                        Bilag = b.Bilag,
                        Tekst = k.Tekst,
                        Afstemningskonto = k.Afstemningskonto,
                        Belob = k.Belob,
                        Kontonr = k.Konto,
                        Momskode = k.Momskode,
                        Faknr = k.Faktura
                      };
            
            KarKladde karKladde = new KarKladde();
            foreach (var k in qry) 
            {
                karKladde.Add(k);
            }
            karKladde.save();
        }
    }

}