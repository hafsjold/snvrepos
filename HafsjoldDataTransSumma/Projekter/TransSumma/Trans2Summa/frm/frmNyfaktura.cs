using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Trans2Summa
{
    public partial class FrmNyfaktura : Form
    {
        public FrmNyfaktura()
        {
            InitializeComponent();
        }

        private void FrmNyfaktura_Load(object sender, EventArgs e)
        {
            var qryTblwfak = from b in Program.dbDataTransSumma.tblwfaks select b;
            this.tblwfakBindingSource.DataSource = qryTblwfak;
            //this.tblwfakBindingSource.DataSource = Program.dbDataTransSumma.Tblwfak;

            if (Program.karRegnskab.MomsPeriode() == 2)
            {
                this.momskodeDataGridViewTextBoxColumn.Visible = false;
                this.momsDataGridViewTextBoxColumn.Visible = false;
                this.bruttobelobDataGridViewTextBoxColumn.Visible = false;
            }

            int antal_fak = this.tblwfakBindingSource.Count;
            if (antal_fak == 0)
            {
                AddNewFak();
            }
        }

        private void FrmNyfaktura_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dbDataTransSumma.SubmitChanges();
        }

        public void AddNyActebisFaktura(tblwfak recwFak)
        {
            this.tblwfakBindingSource.Add(recwFak);
            this.tblwfakBindingSource.MoveLast();
        }

        public void AddNyFaktura(tblfak recFak)
        {
            var qry = from k in recFak.tblfaklins
                      select new tblwfaklin
                      {
                          varenr = k.varenr,
                          tekst = k.tekst,
                          konto = k.konto,
                          momskode = k.momskode,
                          antal = k.antal,
                          enhed = k.enhed,
                          pris = k.pris,
                          rabat = k.rabat,
                          moms = k.moms,
                          nettobelob = k.nettobelob,
                          bruttobelob = k.bruttobelob,
                      };
            int antal = qry.Count();

            if (antal > 0)
            {
                tblwfak recwFak = new tblwfak
                {
                    sk = recFak.sk,
                    dato = recFak.dato,
                    konto = recFak.konto,
                };

                foreach (var k in qry)
                {
                    recwFak.tblwfaklins.Add(k);
                }
                this.tblwfakBindingSource.Add(recwFak);
            }
            this.tblwfakBindingSource.MoveLast();
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

        private void kontoTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point startPoint = kontoTextBox.PointToScreen(new Point(e.X, e.Y));
                KontoType ktp = KontoType.None;
                if (skComboBox.Text == "K")
                    ktp = KontoType.Kreditor;
                if (skComboBox.Text == "S")
                    ktp = KontoType.Debitor;
                FrmKontoplanList m_frmKontoplanList = new FrmKontoplanList(startPoint, ktp);
                m_frmKontoplanList.ShowDialog();
                int? selectedKontonr = m_frmKontoplanList.SelectedKontonr;
                m_frmKontoplanList.Close();
                if (selectedKontonr != null)
                {
                    kontoTextBox.Focus();
                    kontoTextBox.Text = selectedKontonr.ToString();
                }
            }
        }

        private void tblwfaklinDataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = tblwfaklinDataGridView.HitTest(e.X, e.Y);
                int hitcol = hit.ColumnIndex;
                if (hit.Type == DataGridViewHitTestType.Cell && hit.ColumnIndex == 2)
                {
                    tblwfaklinDataGridView.ClearSelection();
                    DataGridViewCell cellVarenr = tblwfaklinDataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                    cellVarenr.Selected = true;
                    Point startPoint = tblwfaklinDataGridView.PointToScreen(new Point(e.X, e.Y));
                    FrmVareList m_frmVareList = new FrmVareList(startPoint);
                    m_frmVareList.ShowDialog();
                    int? selectedVarenr = m_frmVareList.SelectedVarenr;
                    m_frmVareList.Close();
                    if (selectedVarenr != null)
                    {
                        tblwfak recWfak = tblwfakBindingSource.Current as tblwfak;
                        tblwfaklin recWfaklin = ((DataGridView)sender).Rows[hit.RowIndex].DataBoundItem as tblwfaklin;
                        if (recWfaklin != null)
                        {
                            try
                            {
                                recVarer rec = (from k in Program.karVarer where k.Varenr == selectedVarenr select k).First();
                                recWfaklin.varenr = rec.Varenr.ToString();
                                recWfaklin.tekst = rec.Varenavn;
                                recWfaklin.enhed = rec.Enhed;
                                if (recWfak.sk == "S")
                                {
                                    recWfaklin.konto = rec.Salgskonto;
                                    recWfaklin.momskode = KarKontoplan.getMomskode(rec.Salgskonto);
                                    recWfaklin.pris = rec.Salgspris;
                                } if (recWfak.sk == "K")
                                {
                                    recWfaklin.konto = rec.Kobskonto;
                                    recWfaklin.momskode = KarKontoplan.getMomskode(rec.Kobskonto);
                                    recWfaklin.pris = rec.Kobspris;
                                }
                            }
                            catch { }
                        }
                    }
                }
                else if (hit.Type == DataGridViewHitTestType.Cell && hit.ColumnIndex == 4)
                {
                    tblwfaklinDataGridView.ClearSelection();
                    tblwfaklinDataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    Point startPoint = tblwfaklinDataGridView.PointToScreen(new Point(e.X, e.Y));
                    FrmKontoplanList m_frmKontoplanList = new FrmKontoplanList(startPoint, KontoType.Drift | KontoType.Status);
                    m_frmKontoplanList.ShowDialog();
                    int? selectedKontonr = m_frmKontoplanList.SelectedKontonr;
                    string selectedMomskode = m_frmKontoplanList.SelectedMomskode;
                    m_frmKontoplanList.Close();
                    if (selectedKontonr != null)
                    {
                        tblwfaklin recWfaklin = ((DataGridView)sender).Rows[hit.RowIndex].DataBoundItem as tblwfaklin;
                        if (recWfaklin != null)
                        {
                            recWfaklin.konto = selectedKontonr;
                            recWfaklin.momskode = selectedMomskode;
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
            tblwfak recWfak = (tblwfak)tblwfakBindingSource.Current;
            String TargetType = recWfak.sk;
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    try
                    {
                        int i = 0;
                        int iMax = 12;
                        string[] value = new string[iMax];
                        foreach (Match m in regexCommaCvs.Matches(line + ",,"))
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
                            foreach (Match m in regexSimicolonCvs.Matches(line + ";;"))
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
                            tblwfaklin recWfaklin;
                            decimal? Omkostbelob;
                            if (Program.karRegnskab.MomsPeriode() == 2)
                            {
                                recWfaklin = new tblwfaklin
                                {
                                    varenr = value[1],
                                    tekst = value[2],
                                    konto = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? int.Parse(value[3]) : (int?)null,
                                    momskode = "",
                                    antal = Microsoft.VisualBasic.Information.IsNumeric(value[4]) ? decimal.Parse(value[4]) : (decimal?)null,
                                    enhed = value[5],
                                    pris = Microsoft.VisualBasic.Information.IsNumeric(value[6]) ? decimal.Parse(value[6]) : (decimal?)null,
                                    moms = 0,
                                    nettobelob = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                                    bruttobelob = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                                };
                                Omkostbelob = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? decimal.Parse(value[8]) : (decimal?)null;
                                if ((TargetType == "S") && (Omkostbelob != null))
                                {
                                    recWfaklin.konto = getVaresalgsKonto(recWfaklin.konto);
                                    decimal momspct = KarMoms.getMomspct(recWfaklin.momskode) / 100;
                                    recWfaklin.pris += decimal.Round((decimal)(Omkostbelob / recWfaklin.antal), 2);
                                    recWfaklin.nettobelob = decimal.Round((decimal)(recWfaklin.pris * recWfaklin.antal), 2);
                                    recWfaklin.moms = decimal.Round((decimal)(recWfaklin.nettobelob * momspct), 2);
                                    recWfaklin.bruttobelob = decimal.Round((decimal)(recWfaklin.nettobelob + recWfaklin.moms), 2);
                                }
                            }
                            else
                            {
                                recWfaklin = new tblwfaklin
                                {
                                    varenr = value[1],
                                    tekst = value[2],
                                    konto = Microsoft.VisualBasic.Information.IsNumeric(value[3]) ? int.Parse(value[3]) : (int?)null,
                                    momskode = value[4],
                                    antal = Microsoft.VisualBasic.Information.IsNumeric(value[5]) ? decimal.Parse(value[5]) : (decimal?)null,
                                    enhed = value[6],
                                    pris = Microsoft.VisualBasic.Information.IsNumeric(value[7]) ? decimal.Parse(value[7]) : (decimal?)null,
                                    moms = Microsoft.VisualBasic.Information.IsNumeric(value[8]) ? decimal.Parse(value[8]) : (decimal?)null,
                                    nettobelob = Microsoft.VisualBasic.Information.IsNumeric(value[9]) ? decimal.Parse(value[9]) : (decimal?)null,
                                    bruttobelob = Microsoft.VisualBasic.Information.IsNumeric(value[10]) ? decimal.Parse(value[10]) : (decimal?)null
                                };
                                Omkostbelob = Microsoft.VisualBasic.Information.IsNumeric(value[11]) ? decimal.Parse(value[11]) : (decimal?)null;
                                if ((TargetType == "S") && (Omkostbelob != null))
                                {
                                    recWfaklin.konto = getVaresalgsKonto(recWfaklin.konto);
                                    recWfaklin.momskode = "S25";
                                    decimal momspct = KarMoms.getMomspct(recWfaklin.momskode) / 100;
                                    recWfaklin.pris += decimal.Round((decimal)(Omkostbelob / recWfaklin.antal), 2);
                                    recWfaklin.nettobelob = decimal.Round((decimal)(recWfaklin.pris * recWfaklin.antal), 2);
                                    recWfaklin.moms = decimal.Round((decimal)(recWfaklin.nettobelob * momspct), 2);
                                    recWfaklin.bruttobelob = decimal.Round((decimal)(recWfaklin.nettobelob + recWfaklin.moms), 2);
                                }
                            }

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
                //var qryTblwfak = from b in Program.dbDataTransSumma.Tblwfak select b;
                //this.tblwfakBindingSource.DataSource = qryTblwfak;

            }
            catch { }
        }

        private void FakturaTilSummaSummarumToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\FAKTURA", true).SetValue("Kontant", "0");
                Registry.CurrentUser.OpenSubKey(@"SOFTWARE\STONE'S SOFTWARE\SUMMAPRO\FAKTURA", true).SetValue("SkjulDaekBD", "1");
            }
            catch (System.NullReferenceException)
            {
            }

            if (Valider())
            {
                clsFaktura objFaktura = new clsFaktura();
                objFaktura.SalgsOrder2Summa((IList<tblwfak>)this.tblwfakBindingSource.List);
                objFaktura.KøbsOrder2Summa((IList<tblwfak>)this.tblwfakBindingSource.List);
                objFaktura = null;

                int iMax = this.tblwfakBindingSource.List.Count - 1;
                for (int i = iMax; i >= 0; i--)
                {
                    try
                    {
                        this.tblwfakBindingSource.List.RemoveAt(i);
                    }
                    catch { }
                }
            }
        }

        private bool Valider()
        {
            bool OK = true;
            string[] aSk = { "S", "K" };
            var qry = from f in (IList<tblwfak>)this.tblwfakBindingSource.List select f;
            foreach (var f in qry)
            {
                if ((from l in f.tblwfaklins select l).Count() > 0)
                {
                    if (!aSk.Contains(f.sk))
                        OK = false;
                    if (f.dato == null)
                        OK = false;
                    try
                    {
                        string Kontonavn = (from k in Program.karKartotek where k.Kontonr == f.konto select k.Kontonavn).First();
                    }
                    catch
                    {
                        OK = false;
                    }
                    if (f.sk == "K")
                        if (f.kreditorbilagsnr == null)
                            OK = false;
                }
            }
            return OK;
        }

        private void visfejl(tblwfak f, string p)
        {
            throw new NotImplementedException();
        }



        private void Beregn()
        {
            decimal? fakturabelob = 0;
            try
            {
                var qry = from l in (this.tblwfakBindingSource.Current as tblwfak).tblwfaklins select l;
                foreach (var l in qry)
                {
                    decimal momspct;
                    if (KarMoms.isUdlandsmoms(l.momskode))
                        momspct = 0;
                    else
                        momspct = KarMoms.getMomspct(l.momskode) / 100;
                    l.nettobelob = l.pris * l.antal;
                    l.moms = decimal.Round((decimal)(l.nettobelob * momspct), 2);
                    l.bruttobelob = l.nettobelob + l.moms;
                    fakturabelob += l.bruttobelob;
                }
            }
            catch { }
            toolStripLabelFakturabelob.Text = fakturabelob.ToString();
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

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            AddNewFak();
        }

        private void AddNewFak()
        {
            DateTime SidsteDato;
            try
            {
                SidsteDato = (DateTime)(from b in ((IList<tblwfak>)this.tblwfakBindingSource.List) select b.dato).Last();
            }
            catch
            {
                SidsteDato = DateTime.Today;
            }
            String SidsteSk;
            try
            {
                SidsteSk = (from b in ((IList<tblwfak>)this.tblwfakBindingSource.List) select b.sk).Last();
            }
            catch
            {
                SidsteSk = "S";
            }

            tblwfak recwBilag = new tblwfak
            {
                dato = SidsteDato,
                sk = SidsteSk
            };
            try
            {
                this.tblwfakBindingSource.Add(recwBilag);
                this.tblwfakBindingSource.MoveLast();
            }
            catch
            {
                //throw;
            }
        }

        private void skComboBox_TextChanged(object sender, EventArgs e)
        {
            if (skComboBox.Text == "K")
            {
                this.kreditorbilagsnrLabel.Visible = true;
                this.kreditorbilagsnrTextBox.Visible = true;
            }
            else
            {
                this.kreditorbilagsnrLabel.Visible = false;
                this.kreditorbilagsnrTextBox.Visible = false;
            }

        }

        private int? getVaresalgsKonto(int? konto)
        {
            try
            {
                string Omktype = (from fl in Program.dbDataTransSumma.tblvareomkostningers where fl.kontonr == konto select fl.omktype).First();
                if (Omktype == "vareforb")
                    return 1000;
                else
                    return konto;
            }
            catch
            {
                return konto;
            }
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            Beregn();
        }

        private void toolStripcmdBeregn_Click(object sender, EventArgs e)
        {
            Beregn();
        }
    }
}
