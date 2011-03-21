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
            this.karMomsBindingSource.DataSource = Program.karMoms;
            if (Program.karRegnskab.MomsPeriode() == 2)
                this.MKdataGridViewComboBox.Visible = false;
        }

        public void AddNyKladde(Tblbilag recBilag, Tblbankkonto recBankkonto)
        {
            var qry = from k in recBilag.Tblkladder
                      select new Tblwkladder
                      {
                          Tekst = k.Tekst,
                          Afstemningskonto = k.Afstemningskonto,
                          Belob = k.Belob,
                          Konto = k.Konto,
                          Momskode = k.Momskode,
                          Faktura = k.Faktura
                      };
            int antal = qry.Count();

            int bilagnr = 0;

            try
            {
                bilagnr = (from b in ((IList<Tblwbilag>)this.tblwbilagBindingSource.List) select b.Bilag).Max();
                bilagnr++;
            }
            catch
            {
                bilagnr = Program.karStatus.BS1_NæsteNr();
            }

            DateTime BankDato;
            try
            {
                BankDato = (DateTime)recBankkonto.Dato;
            }
            catch
            {
                BankDato = DateTime.Today;
            }

            Tblwbilag recwBilag = new Tblwbilag
            {
                Bilag = bilagnr,
                Dato = BankDato
            };

            if (!ReducerBilag(recwBilag, recBilag, recBankkonto))
            {

                foreach (var k in qry)
                {
                    recwBilag.Tblwkladder.Add(k);
                }
                this.tblwbilagBindingSource.Add(recwBilag);
            }
            this.tblwbilagBindingSource.MoveLast();

        }

        private void FrmNyekladder_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        private void tblwbilagBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                Program.dbDataTransSumma.SubmitChanges();
            }
            catch { }
        }

        private void copyMenuLineCopyPastItem_Click(object sender, EventArgs e)
        {
            this.copyToClipboard();
        }

        private void pasteMenuLineCopyPastItem_Click(object sender, EventArgs e)
        {
            this.pasteFromClipboard();
        }

        private void tblwkladderDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Delete)
            {
                Tblwbilag recWbilag = tblwbilagBindingSource.Current as Tblwbilag;
                DataGridViewSelectedRowCollection rows = tblwkladderDataGridView.SelectedRows;
                foreach (DataGridViewRow row in rows)
                {
                    Tblwkladder recWkladder = row.DataBoundItem as Tblwkladder;
                    recWbilag.Tblwkladder.Remove(recWkladder);
                }
                e.Handled = true;
            }    
            */

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
            string[] sep = { "\r\n", "\n" };
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
                            Tblwkladder recWkladder;
                            if (Program.karRegnskab.MomsPeriode() == 2) // Ingen moms
                            {
                                recWkladder = new Tblwkladder
                                    {

                                        Tekst = value[1],
                                        Afstemningskonto = value[2],
                                        Belob = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                                        Konto = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? int.Parse(value[4]) : (int?)null,
                                        Faktura = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? int.Parse(value[5]) : (int?)null
                                    };
                            }
                            else
                            {
                                recWkladder = new Tblwkladder
                                {

                                    Tekst = value[1],
                                    Afstemningskonto = value[2],
                                    Belob = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                                    Konto = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? int.Parse(value[4]) : (int?)null,
                                    Momskode = value[5],
                                    Faktura = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? int.Parse(value[6]) : (int?)null
                                };
                            }
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
                tblwkladderBindingSource.MoveFirst();
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

        private void KladderTilSummaSummarumToolStripButton_Click(object sender, EventArgs e)
        {

            KarKladde karKladde = new KarKladde(); 
            var qry = from wb in ((IList<Tblwbilag>)this.tblwbilagBindingSource.List) select wb;
            foreach (Tblwbilag wb in qry)
            {
                foreach (Tblwkladder wk in wb.Tblwkladder)
                {
                    recKladde k = new recKladde
                             {
                                 Dato = wb.Dato,
                                 Bilag = wb.Bilag,
                                 Tekst = wk.Tekst,
                                 Afstemningskonto = wk.Afstemningskonto,
                                 Belob = wk.Belob,
                                 Kontonr = wk.Konto,
                                 Momskode = wk.Momskode,
                                 Faknr = wk.Faktura
                             };
                    karKladde.Add(k);}
            }
            karKladde.save();
            
            int iMax = this.tblwbilagBindingSource.List.Count - 1;
            for (int i = iMax; i >= 0; i--)
            {
                this.tblwbilagBindingSource.List.RemoveAt(i);
            }
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            decimal Balance = 0;
            try
            {
                var qry2 = from k in (this.tblwbilagBindingSource.Current as Tblwbilag).Tblwkladder select k;
                foreach (var k in qry2)
                {
                    if (!((k.Afstemningskonto != null)
                    && (k.Afstemningskonto != "")
                    && (k.Konto != null)))
                    {
                        if (k.Konto != null)
                            Balance -= (k.Belob != null) ? (decimal)k.Belob : 0;
                        else
                            Balance += (k.Belob != null) ? (decimal)k.Belob : 0;
                    }
                }
            }
            catch { }
            if (Balance == 0)
            {
                this.lblBalanceBilag.Text = "0,00";
                this.lblBalanceBilag.ForeColor = Color.Black;  //System.Drawing.SystemColors.Control;
            }
            else
            {
                this.lblBalanceBilag.Text = Balance.ToString("#0.00");
                this.lblBalanceBilag.ForeColor = Color.Red;
            }
        }

        private void tblwkladderDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = tblwkladderDataGridView.HitTest(e.X, e.Y);
                int hitcol = hit.ColumnIndex;
                if (hit.Type == DataGridViewHitTestType.Cell && hit.ColumnIndex == 4)
                {
                    tblwkladderDataGridView.ClearSelection();
                    tblwkladderDataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    this.contextMenuMoms.Show(this.tblwkladderDataGridView, new Point(e.X, e.Y));
                }
                else if (hit.Type == DataGridViewHitTestType.Cell && hit.ColumnIndex == 5)
                {
                    tblwkladderDataGridView.ClearSelection();
                    tblwkladderDataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    Point startPoint = tblwkladderDataGridView.PointToScreen(new Point(e.X, e.Y));
                    FrmKontoplanList m_frmKontoplanList = new FrmKontoplanList(startPoint);
                    m_frmKontoplanList.ShowDialog();
                }
                else if (hit.Type == DataGridViewHitTestType.RowHeader)
                {
                    this.contextMenuLineCopyPaste.Show(this.tblwkladderDataGridView, new Point(e.X, e.Y));
                }
            }
        }

        private void myDGV_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.ColumnIndex == 5) 
            {
                if (e.RowIndex >= 0)
                {
                    Tblwkladder recWkladder = ((DataGridView)sender).Rows[e.RowIndex].DataBoundItem as Tblwkladder;
                    if (recWkladder != null)
                    {
                        string kontonavn;
                        try
                        {
                            kontonavn = (from k in Program.karKontoplan where k.Kontonr == recWkladder.Konto select k.Kontonavn).First();
                        }
                        catch 
                        {
                            kontonavn = "Not found";
                        }
                        e.ToolTipText = kontonavn;
                    }
                }
            }
        }

        private void tillægMomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cells = tblwkladderDataGridView.SelectedCells;
            if (cells.Count == 1)
            {
                try
                {
                    DataGridViewTextBoxCell cell = cells[0] as DataGridViewTextBoxCell;
                    Tblwkladder recWkladder = cell.OwningRow.DataBoundItem as Tblwkladder;
                    decimal momspct = decimal.Parse("1,25");
                    recWkladder.Belob *= momspct;

                }
                catch { }
            }

        }

        private void fratrækMomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cells = tblwkladderDataGridView.SelectedCells;
            if (cells.Count == 1)
            {
                try
                {
                    DataGridViewTextBoxCell cell = cells[0] as DataGridViewTextBoxCell;
                    Tblwkladder recWkladder = cell.OwningRow.DataBoundItem as Tblwkladder;
                    decimal momspct = decimal.Parse("1,25");
                    recWkladder.Belob /= momspct;
                }
                catch { }
            }
        }

        private bool ReducerBilag(Tblwbilag recwBilag, Tblbilag recBilag, Tblbankkonto recBankkonto)
        {
            bool IsFound_BankKontoudtog = (recBankkonto != null);
            decimal BankBelob = 0;
            decimal MomsBelob = 0;
            decimal AndenKontoBelob = 0;
            int? AndenKontoKonto = null;
            string AndenKontoTekst = "";
            string AndenKontoMomskode;
            string AndenKontoAfstemningskonto = "";
            string MK = "";
            bool bBankKonto = false;
            bool bMomsKonto = false;
            bool bAndenKonto = false;
            bool bAfstem = false;
            bool bMomskode = false;

            int AntalLinier = recBilag.Tblkladder.Count;
            if (AntalLinier <= 3)
            {
                foreach (Tblkladder recKladder in recBilag.Tblkladder)
                {
                    if ((recKladder.Afstemningskonto != null) && (recKladder.Afstemningskonto != ""))
                        bAfstem = true;

                    if (recKladder.Konto != null)
                    {
                        switch (recKladder.Konto)
                        {
                            case 58000:
                                bBankKonto = true;
                                BankBelob = (decimal)recKladder.Belob;
                                break;

                            case 66100:
                                bMomsKonto = true;
                                MomsBelob = (decimal)recKladder.Belob;
                                MK = "S25";
                                break;

                            case 66200:
                                bMomsKonto = true;
                                MomsBelob = (decimal)recKladder.Belob;
                                MK = "K25";
                                break;

                            default:
                                bAndenKonto = true;
                                AndenKontoBelob = (decimal)recKladder.Belob;
                                AndenKontoTekst = recKladder.Tekst;
                                AndenKontoKonto = (int)recKladder.Konto;
                                if ((recKladder.Afstemningskonto != null) && (recKladder.Afstemningskonto != ""))
                                    AndenKontoAfstemningskonto = recKladder.Afstemningskonto;
                                if ((recKladder.Momskode != null) && (recKladder.Momskode != ""))
                                    AndenKontoMomskode = recKladder.Momskode;
                                break;
                        }
                    }

                    if ((recKladder.Momskode != null) && (recKladder.Momskode != ""))
                        bMomskode = true;
                }

                if ((AntalLinier == 3)
                && (bBankKonto)
                && (bMomsKonto)
                && (bAndenKonto)
                && (!bAfstem)
                && (!bMomskode))
                {
                    decimal MomsBelobDif = -MomsBelob + (AndenKontoBelob * decimal.Parse(" 0,25"));
                    if ((MomsBelobDif > -decimal.Parse(" 0,01"))
                    && (MomsBelobDif < decimal.Parse(" 0,01")))
                    {
                        Tblwkladder recWkladder = new Tblwkladder
                        {
                            Tekst = AndenKontoTekst,
                            Afstemningskonto = "Bank",
                            Belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.Belob : BankBelob,
                            Konto = AndenKontoKonto,
                            Momskode = MK
                        };
                        recwBilag.Tblwkladder.Add(recWkladder);
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }
                }

                if ((AntalLinier == 2)
                && (bBankKonto)
                && (!bMomsKonto)
                && (bAndenKonto)
                && (!bAfstem)
                && (!bMomskode))
                {
                    Tblwkladder recWkladder = new Tblwkladder
                    {
                        Tekst = AndenKontoTekst,
                        Afstemningskonto = "Bank",
                        Belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.Belob : BankBelob,
                        Konto = AndenKontoKonto
                    };
                    recwBilag.Tblwkladder.Add(recWkladder);
                    this.tblwbilagBindingSource.Add(recwBilag);
                    return true;
                }

                if ((AntalLinier == 1)
                && (!bBankKonto)
                && (bAndenKonto)
                && (bAfstem))
                {
                    Tblwkladder recWkladder = new Tblwkladder
                    {
                        Tekst = AndenKontoTekst,
                        Afstemningskonto = AndenKontoAfstemningskonto,
                        Belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.Belob : BankBelob,
                        Konto = AndenKontoKonto,
                        Momskode = MK
                    };
                    recwBilag.Tblwkladder.Add(recWkladder);
                    this.tblwbilagBindingSource.Add(recwBilag);
                    return true;
                }
            }
            return false;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.tblwbilagBindingSource.RemoveCurrent();
            }
            catch
            {
                //throw;
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            int bilagnr = 0;

            try
            {
                bilagnr = (from b in ((IList<Tblwbilag>)this.tblwbilagBindingSource.List) select b.Bilag).Max();
                bilagnr++;
            }
            catch
            {
                bilagnr = Program.karStatus.BS1_NæsteNr();
            }

            DateTime SidsteDato;
            try
            {
                SidsteDato = (from b in ((IList<Tblwbilag>)this.tblwbilagBindingSource.List) select b.Dato).Last();
            }
            catch 
            {
                SidsteDato = DateTime.Today;
            }
            
            Tblwbilag recwBilag = new Tblwbilag
            {
                Bilag = bilagnr,
                Dato = SidsteDato
            };
            try
            {
                this.tblwbilagBindingSource.Add(recwBilag);
                this.tblwbilagBindingSource.MoveLast();
            }
            catch
            {
                //throw;
            }
        }
    }

}