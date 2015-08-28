using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Trans2SummaHDC
{
    public partial class FrmNyekladder : Form
    {
        public FrmNyekladder()
        {
            InitializeComponent();
        }

        private void FrmNyekladder_Load(object sender, EventArgs e)
        {
            this.tblwbilagBindingSource.DataSource = Program.dbDataTransSumma.tblwbilags;
            this.karKontoplanBindingSource.DataSource = Program.karKontoplan;
            this.karAfstemningskontiBindingSource.DataSource = Program.karAfstemningskonti;
            this.karMomsBindingSource.DataSource = Program.karMoms;
            if (Program.karRegnskab.MomsPeriode() == 2)
                this.momskodeDataGridViewTextBoxColumn.Visible = false;
        }

        public void AddNyKladde(tblbilag recBilag, tblbankkonto recBankkonto)
        {
            var qry = from k in recBilag.tblkladders
                      select new tblwkladder
                      {
                          tekst = k.tekst,
                          afstemningskonto = k.afstemningskonto,
                          belob = k.belob,
                          konto = k.konto,
                          momskode = k.momskode,
                          faktura = k.faktura
                      };
            int antal = qry.Count();

            int bilagnr = 0;

            try
            {
                bilagnr = (from b in ((IList<tblwbilag>)this.tblwbilagBindingSource.List) select b.bilag).Max();
                bilagnr++;
            }
            catch
            {
                bilagnr = Program.karStatus.BS1_NæsteNr();
            }

            DateTime BankDato;
            try
            {
                BankDato = (DateTime)recBankkonto.dato;
            }
            catch
            {
                BankDato = DateTime.Today;
            }

            tblwbilag recwBilag = new tblwbilag
            {
                bilag = bilagnr,
                dato = BankDato
            };

            if (!ReducerBilag(recwBilag, recBilag, recBankkonto))
            {
                foreach (var k in qry)
                {
                    recwBilag.tblwkladders.Add(k);
                }
                this.tblwbilagBindingSource.Add(recwBilag);
            }
            this.tblwbilagBindingSource.MoveLast();

        }

        public void AddNyTemplateKladde(tbltemplate recTemplate, tblbankkonto recBankkonto)
        {
            int bilagnr = 0;

            try
            {
                bilagnr = (from b in ((IList<tblwbilag>)this.tblwbilagBindingSource.List) select b.bilag).Max();
                bilagnr++;
            }
            catch
            {
                bilagnr = Program.karStatus.BS1_NæsteNr();
            }

            DateTime BankDato;
            try
            {
                BankDato = (DateTime)recBankkonto.dato;
            }
            catch
            {
                BankDato = DateTime.Today;
            }

            tblwbilag recwBilag = new tblwbilag
            {
                bilag = bilagnr,
                dato = BankDato
            };

            string WrkTekst;
            if ((recTemplate.tekst != null) && (recTemplate.tekst.Length > 0))
            {
                WrkTekst = recTemplate.tekst;
            }
            else
            {
                WrkTekst = recBankkonto.tekst;
            }


            string WrkAfstemningskonto;
            if ((recTemplate.afstemningskonto != null) && (recTemplate.afstemningskonto.Length > 0))
            {
                WrkAfstemningskonto = recTemplate.afstemningskonto;
            }
            else
            {
                try
                {
                    WrkAfstemningskonto = (from w in Program.dbDataTransSumma.tblkontoudtogs where w.pid == recBankkonto.bankkontoid select w).First().afstemningskonto;
                }
                catch
                {
                    WrkAfstemningskonto = "";
                }
            }

            string WrkMomskode;
            if ((recTemplate.momskode != null) && (recTemplate.momskode.Length > 0))
            {
                WrkMomskode = recTemplate.momskode;
            }
            else
            {
                try
                {
                    WrkMomskode = (from w in Program.karKontoplan where w.Kontonr == recTemplate.konto select w).First().Moms;
                }
                catch
                {
                    WrkMomskode = "";
                }
            }

            tblwkladder recWkladder = new tblwkladder
            {
                tekst = WrkTekst,
                afstemningskonto = WrkAfstemningskonto,
                belob = (decimal)recBankkonto.belob,
                konto = recTemplate.konto,
                momskode = WrkMomskode
            };
            recwBilag.tblwkladders.Add(recWkladder);
            this.tblwbilagBindingSource.Add(recwBilag);
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
            tblwbilag recWbilag = (tblwbilag)tblwbilagBindingSource.Current;
            foreach (string line in lines)
            {
                if (line.Length > 0)
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
                            tblwkladder recWkladder;
                            if (Program.karRegnskab.MomsPeriode() == 2) // Ingen moms
                            {
                                recWkladder = new tblwkladder
                                    {

                                        tekst = value[1],
                                        afstemningskonto = value[2],
                                        belob = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                                        konto = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? int.Parse(value[4]) : (int?)null,
                                        faktura = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? int.Parse(value[5]) : (int?)null
                                    };
                            }
                            else
                            {
                                recWkladder = new tblwkladder
                                {

                                    tekst = value[1],
                                    afstemningskonto = value[2],
                                    belob = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? decimal.Parse(value[3]) : (decimal?)null,
                                    konto = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? int.Parse(value[4]) : (int?)null,
                                    momskode = value[5],
                                    faktura = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? int.Parse(value[6]) : (int?)null
                                };
                            }
                            tblwkladderBindingSource.Insert(row, recWkladder);
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
            var qry = from wb in ((IList<tblwbilag>)this.tblwbilagBindingSource.List) select wb;
            foreach (tblwbilag wb in qry)
            {
                foreach (tblwkladder wk in wb.tblwkladders)
                {
                    recKladde k = new recKladde
                             {
                                 Dato = wb.dato,
                                 Bilag = wb.bilag,
                                 Tekst = wk.tekst,
                                 Afstemningskonto = wk.afstemningskonto,
                                 Belob = wk.belob,
                                 Kontonr = wk.konto,
                                 Momskode = wk.momskode,
                                 Faknr = wk.faktura
                             };
                    karKladde.Add(k);
                }
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
                var qry2 = from k in (this.tblwbilagBindingSource.Current as tblwbilag).tblwkladders select k;
                foreach (var k in qry2)
                {
                    if (!((k.afstemningskonto != null)
                    && (k.afstemningskonto != "")
                    && (k.konto != null)))
                    {
                        if (k.konto != null)
                            Balance -= (k.belob != null) ? (decimal)k.belob : 0;
                        else
                            Balance += (k.belob != null) ? (decimal)k.belob : 0;
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
                    FrmKontoplanList m_frmKontoplanList = new FrmKontoplanList(startPoint, KontoType.Drift | KontoType.Status | KontoType.Debitor | KontoType.Kreditor);
                    m_frmKontoplanList.ShowDialog();
                    int? selectedKontonr = m_frmKontoplanList.SelectedKontonr;
                    string selectedMomskode = m_frmKontoplanList.SelectedMomskode;
                    m_frmKontoplanList.Close();
                    if (selectedKontonr != null)
                    {
                        tblwkladder recWkladder = ((DataGridView)sender).Rows[hit.RowIndex].DataBoundItem as tblwkladder;
                        if (recWkladder != null)
                        {
                            recWkladder.konto = selectedKontonr;
                            recWkladder.momskode = selectedMomskode;
                        }
                    }
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
                    tblwkladder recWkladder = ((DataGridView)sender).Rows[e.RowIndex].DataBoundItem as tblwkladder;
                    if (recWkladder != null)
                    {
                        string kontonavn;
                        try
                        {
                            IEnumerable<recKontoplan> qry_Kontoplan = from k in Program.karKontoplan select k;
                            IEnumerable<recKontoplan> qry_Kartotek = from k in Program.karKartotek
                                                                     select new recKontoplan
                                                                     {
                                                                         DK = k.DK,
                                                                         Kontonavn = k.Kontonavn,
                                                                         Kontonr = k.Kontonr,
                                                                         Moms = k.Moms,
                                                                         Saldo = k.Saldo,
                                                                         Type = k.Type
                                                                     };
                            IEnumerable<recKontoplan> qry_Join = qry_Kontoplan.Union(qry_Kartotek);
                            kontonavn = (from k in qry_Join where k.Kontonr == recWkladder.konto select k.Kontonavn).First();
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
                    tblwkladder recWkladder = cell.OwningRow.DataBoundItem as tblwkladder;
                    decimal momspct = 1 + KarMoms.getMomspct(recWkladder.momskode) / 100;
                    recWkladder.belob *= momspct;

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
                    tblwkladder recWkladder = cell.OwningRow.DataBoundItem as tblwkladder;
                    decimal momspct = 1 + KarMoms.getMomspct(recWkladder.momskode) / 100;
                    recWkladder.belob /= momspct;
                }
                catch { }
            }
        }

        private bool ReducerBilag(tblwbilag recwBilag, tblbilag recBilag, tblbankkonto recBankkonto)
        {
            bool IsFound_BankKontoudtog = (recBankkonto != null);
            decimal BankBelob = 0;
            decimal MomsBelob = 0;
            decimal AndenKontoBelob = 0;
            int? AndenKontoKonto = null;
            string AndenKontoTekst = "";
            string AndenKontoMomskode = "";
            string AndenKontoAfstemningskonto = "";
            string MK = "";
            bool bBankKonto = false;
            bool bMomsKonto = false;
            bool bAndenKonto = false;
            bool bAfstem = false;
            bool bMomskode = false;

            var qry = from k in recBilag.tblkladders
                      select new tblwkladder
                      {
                          tekst = k.tekst,
                          afstemningskonto = k.afstemningskonto,
                          belob = k.belob,
                          konto = k.konto,
                          momskode = k.momskode,
                          faktura = k.faktura
                      };

            int AntalLinier = recBilag.tblkladders.Count;


            if ((!IsFound_BankKontoudtog) || (IsFound_BankKontoudtog && ((recBankkonto.bankkontoid == 1) || (recBankkonto.bankkontoid == 3)))) //BANK
            {
                if (AntalLinier <= 3)
                {
                    foreach (tblkladder recKladder in recBilag.tblkladders)
                    {
                        if ((recKladder.afstemningskonto != null) && (recKladder.afstemningskonto != ""))
                            bAfstem = true;

                        if (recKladder.konto != null)
                        {
                            switch (recKladder.konto)
                            {
                                case 58000:
                                    bBankKonto = true;
                                    BankBelob = (decimal)recKladder.belob;
                                    break;

                                case 66100:
                                    bMomsKonto = true;
                                    MomsBelob = (decimal)recKladder.belob;
                                    MK = "S25";
                                    break;

                                case 66200:
                                    bMomsKonto = true;
                                    MomsBelob = (decimal)recKladder.belob;
                                    MK = "K25";
                                    break;

                                default:
                                    bAndenKonto = true;
                                    AndenKontoBelob = (decimal)recKladder.belob;
                                    AndenKontoTekst = recKladder.tekst;
                                    AndenKontoKonto = (int)recKladder.konto;
                                    if ((recKladder.afstemningskonto != null) && (recKladder.afstemningskonto != ""))
                                        AndenKontoAfstemningskonto = recKladder.afstemningskonto;
                                    if ((recKladder.momskode != null) && (recKladder.momskode != ""))
                                        AndenKontoMomskode = recKladder.momskode;
                                    break;
                            }
                        }

                        if ((recKladder.momskode != null) && (recKladder.momskode != ""))
                            bMomskode = true;
                    }

                    if ((AntalLinier == 3)
                    && (bBankKonto)
                    && (bMomsKonto)
                    && (bAndenKonto)
                    && (!bAfstem)
                    && (!bMomskode))
                    {
                        //decimal MomsBelobDif = -MomsBelob + (AndenKontoBelob * decimal.Parse(" 0,25"));
                        decimal momspct = KarMoms.getMomspct(MK) / 100;
                        decimal MomsBelobDif = -MomsBelob + (AndenKontoBelob * momspct);
                        if ((MomsBelobDif > -decimal.Parse(" 0,01"))
                        && (MomsBelobDif < decimal.Parse(" 0,01")))
                        {
                            tblwkladder recWkladder = new tblwkladder
                            {
                                tekst = AndenKontoTekst,
                                afstemningskonto = "Bank",
                                belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : BankBelob,
                                konto = AndenKontoKonto,
                                momskode = MK
                            };
                            recwBilag.tblwkladders.Add(recWkladder);
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
                        tblwkladder recWkladder = new tblwkladder
                        {
                            tekst = AndenKontoTekst,
                            afstemningskonto = "Bank",
                            belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : AndenKontoBelob,
                            konto = AndenKontoKonto
                        };
                        recwBilag.tblwkladders.Add(recWkladder);
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }


                    if ((AntalLinier == 2)
                    && (bBankKonto)
                    && (bAndenKonto)
                    && (!bAfstem))
                    {
                        foreach (tblwkladder k in qry)
                        {
                            if (IsFound_BankKontoudtog)
                            {
                                if (k.konto == 58000)
                                    k.belob = -(decimal)recBankkonto.belob;
                                else
                                    k.belob = (decimal)recBankkonto.belob;
                            }
                            recwBilag.tblwkladders.Add(k);
                        }
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }

                    if ((AntalLinier == 1)
                    && (!bBankKonto)
                    && (bAndenKonto)
                    && (bAfstem))
                    {
                        string WrkAfstemningskonto;
                        if (AndenKontoAfstemningskonto == "MasterCard")
                            WrkAfstemningskonto = "Bank";
                        else
                            WrkAfstemningskonto = AndenKontoAfstemningskonto;
                        tblwkladder recWkladder = new tblwkladder
                        {
                            tekst = AndenKontoTekst,
                            afstemningskonto = WrkAfstemningskonto,
                            belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : AndenKontoBelob,
                            konto = AndenKontoKonto,
                            momskode = AndenKontoMomskode
                        };
                        recwBilag.tblwkladders.Add(recWkladder);
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }
                }
            }

            else if (IsFound_BankKontoudtog && recBankkonto.bankkontoid == 2)  //MASTERCARD
            {
                if (AntalLinier <= 3)
                {
                    foreach (tblkladder recKladder in recBilag.tblkladders)
                    {
                        if ((recKladder.afstemningskonto != null) && (recKladder.afstemningskonto != ""))
                            bAfstem = true;

                        if (recKladder.konto != null)
                        {
                            switch (recKladder.konto)
                            {
                                case 58310:
                                    bBankKonto = true;
                                    BankBelob = (decimal)recKladder.belob;
                                    break;

                                case 66100:
                                    bMomsKonto = true;
                                    MomsBelob = (decimal)recKladder.belob;
                                    MK = "S25";
                                    break;

                                case 66200:
                                    bMomsKonto = true;
                                    MomsBelob = (decimal)recKladder.belob;
                                    MK = "K25";
                                    break;

                                default:
                                    bAndenKonto = true;
                                    AndenKontoBelob = (decimal)recKladder.belob;
                                    AndenKontoTekst = recKladder.tekst;
                                    AndenKontoKonto = (int)recKladder.konto;
                                    if ((recKladder.afstemningskonto != null) && (recKladder.afstemningskonto != ""))
                                        AndenKontoAfstemningskonto = recKladder.afstemningskonto;
                                    if ((recKladder.momskode != null) && (recKladder.momskode != ""))
                                        AndenKontoMomskode = recKladder.momskode;
                                    break;
                            }
                        }

                        if ((recKladder.momskode != null) && (recKladder.momskode != ""))
                            bMomskode = true;
                    }

                    if ((AntalLinier == 3)
                    && (bBankKonto)
                    && (bMomsKonto)
                    && (bAndenKonto)
                    && (!bAfstem)
                    && (!bMomskode))
                    {
                        //decimal MomsBelobDif = -MomsBelob + (AndenKontoBelob * decimal.Parse(" 0,25"));
                        decimal momspct = KarMoms.getMomspct(MK) / 100;
                        decimal MomsBelobDif = -MomsBelob + (AndenKontoBelob * momspct);
                        if ((MomsBelobDif > -decimal.Parse(" 0,01"))
                        && (MomsBelobDif < decimal.Parse(" 0,01")))
                        {
                            tblwkladder recWkladder = new tblwkladder
                            {
                                tekst = AndenKontoTekst,
                                afstemningskonto = "MasterCard",
                                belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : BankBelob,
                                konto = AndenKontoKonto,
                                momskode = MK
                            };
                            recwBilag.tblwkladders.Add(recWkladder);
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
                        tblwkladder recWkladder = new tblwkladder
                        {
                            tekst = AndenKontoTekst,
                            afstemningskonto = "MasterCard",
                            belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : AndenKontoBelob,
                            konto = AndenKontoKonto
                        };
                        recwBilag.tblwkladders.Add(recWkladder);
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }

                    if ((AntalLinier == 2)
                    && (bBankKonto)
                    && (bAndenKonto)
                    && (!bAfstem))
                    {
                        foreach (tblwkladder k in qry)
                        {
                            if (IsFound_BankKontoudtog)
                            {
                                if (k.konto == 58310)
                                    k.belob = -(decimal)recBankkonto.belob;
                                else
                                    k.belob = (decimal)recBankkonto.belob;
                            }
                            recwBilag.tblwkladders.Add(k);
                        }
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }
                    if ((AntalLinier == 1)
                    && (!bBankKonto)
                    && (bAndenKonto)
                    && (bAfstem))
                    {
                        string WrkAfstemningskonto;
                        if (AndenKontoAfstemningskonto == "Bank")
                            WrkAfstemningskonto = "MasterCard";
                        else
                            WrkAfstemningskonto = AndenKontoAfstemningskonto;

                        tblwkladder recWkladder = new tblwkladder
                        {
                            tekst = AndenKontoTekst,
                            afstemningskonto = WrkAfstemningskonto,
                            belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : AndenKontoBelob,
                            konto = AndenKontoKonto,
                            momskode = AndenKontoMomskode
                        };
                        recwBilag.tblwkladders.Add(recWkladder);
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }
                }
            }
            //*******************************************************************************************
            else if (IsFound_BankKontoudtog && recBankkonto.bankkontoid == 5)  //PAYPAL
            {
                if (AntalLinier <= 3)
                {
                    foreach (tblkladder recKladder in recBilag.tblkladders)
                    {
                        if ((recKladder.afstemningskonto != null) && (recKladder.afstemningskonto != ""))
                            bAfstem = true;

                        if (recKladder.konto != null)
                        {
                            switch (recKladder.konto)
                            {
                                case 58300:
                                    if (!bAfstem)
                                    {
                                        bBankKonto = true;
                                        BankBelob = (decimal)recKladder.belob;
                                    }
                                    else
                                    {
                                        bAndenKonto = true;
                                        AndenKontoBelob = (decimal)recKladder.belob;
                                        AndenKontoTekst = recKladder.tekst;
                                        AndenKontoKonto = (int)recKladder.konto;
                                        if ((recKladder.afstemningskonto != null) && (recKladder.afstemningskonto != ""))
                                            AndenKontoAfstemningskonto = recKladder.afstemningskonto;
                                        if ((recKladder.momskode != null) && (recKladder.momskode != ""))
                                            AndenKontoMomskode = recKladder.momskode;
                                    }
                                    break;

                                case 66100:
                                    bMomsKonto = true;
                                    MomsBelob = (decimal)recKladder.belob;
                                    MK = "S25";
                                    break;

                                case 66200:
                                    bMomsKonto = true;
                                    MomsBelob = (decimal)recKladder.belob;
                                    MK = "K25";
                                    break;

                                default:
                                    bAndenKonto = true;
                                    AndenKontoBelob = (decimal)recKladder.belob;
                                    AndenKontoTekst = recKladder.tekst;
                                    AndenKontoKonto = (int)recKladder.konto;
                                    if ((recKladder.afstemningskonto != null) && (recKladder.afstemningskonto != ""))
                                        AndenKontoAfstemningskonto = recKladder.afstemningskonto;
                                    if ((recKladder.momskode != null) && (recKladder.momskode != ""))
                                        AndenKontoMomskode = recKladder.momskode;
                                    break;
                            }
                        }

                        if ((recKladder.momskode != null) && (recKladder.momskode != ""))
                            bMomskode = true;
                    }

                    if ((AntalLinier == 3)
                    && (bBankKonto)
                    && (bMomsKonto)
                    && (bAndenKonto)
                    && (!bAfstem)
                    && (!bMomskode))
                    {
                        //decimal MomsBelobDif = -MomsBelob + (AndenKontoBelob * decimal.Parse(" 0,25"));
                        decimal momspct = KarMoms.getMomspct(MK) / 100;
                        decimal MomsBelobDif = -MomsBelob + (AndenKontoBelob * momspct);
                        if ((MomsBelobDif > -decimal.Parse(" 0,01"))
                        && (MomsBelobDif < decimal.Parse(" 0,01")))
                        {
                            tblwkladder recWkladder = new tblwkladder
                            {
                                tekst = AndenKontoTekst,
                                afstemningskonto = "PayPal",
                                belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : BankBelob,
                                konto = AndenKontoKonto,
                                momskode = MK
                            };
                            recwBilag.tblwkladders.Add(recWkladder);
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
                        tblwkladder recWkladder = new tblwkladder
                        {
                            tekst = AndenKontoTekst,
                            afstemningskonto = "PayPal",
                            belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : AndenKontoBelob,
                            konto = AndenKontoKonto
                        };
                        recwBilag.tblwkladders.Add(recWkladder);
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }

                    if ((AntalLinier == 2)
                    && (bBankKonto)
                    && (bAndenKonto)
                    && (!bAfstem))
                    {
                        foreach (tblwkladder k in qry)
                        {
                            if (IsFound_BankKontoudtog)
                            {
                                if (k.konto == 58300)
                                    k.belob = -(decimal)recBankkonto.belob;
                                else
                                    k.belob = (decimal)recBankkonto.belob;
                            }
                            recwBilag.tblwkladders.Add(k);
                        }
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }
                    if ((AntalLinier == 1)
                    && (!bBankKonto)
                    && (bAndenKonto)
                    && (bAfstem))
                    {
                        string WrkAfstemningskonto;
                        int? WrkAndenKontoKonto = AndenKontoKonto;

                        if ((AndenKontoAfstemningskonto == "Bank") && (AndenKontoKonto == 58300))
                        {
                            WrkAfstemningskonto = "PayPal";
                            WrkAndenKontoKonto = 58000;
                        }
                        else if (AndenKontoAfstemningskonto == "Bank")
                            WrkAfstemningskonto = "PayPal";
                        else
                            WrkAfstemningskonto = AndenKontoAfstemningskonto;

                        tblwkladder recWkladder = new tblwkladder
                        {
                            tekst = AndenKontoTekst,
                            afstemningskonto = WrkAfstemningskonto,
                            belob = (IsFound_BankKontoudtog) ? (decimal)recBankkonto.belob : AndenKontoBelob,
                            konto = WrkAndenKontoKonto,
                            momskode = AndenKontoMomskode
                        };
                        recwBilag.tblwkladders.Add(recWkladder);
                        this.tblwbilagBindingSource.Add(recwBilag);
                        return true;
                    }

                }
            }
            //*******************************************************************************************

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
                bilagnr = (from b in ((IList<tblwbilag>)this.tblwbilagBindingSource.List) select b.bilag).Max();
                bilagnr++;
            }
            catch
            {
                bilagnr = Program.karStatus.BS1_NæsteNr();
            }

            DateTime SidsteDato;
            try
            {
                SidsteDato = (from b in ((IList<tblwbilag>)this.tblwbilagBindingSource.List) select b.dato).Last();
            }
            catch
            {
                SidsteDato = DateTime.Today;
            }

            tblwbilag recwBilag = new tblwbilag
            {
                bilag = bilagnr,
                dato = SidsteDato
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