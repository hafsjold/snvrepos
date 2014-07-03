using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml.Linq;

namespace Trans2SummaHDA
{
    public partial class FrmBankafstemning : Form
    {
        ColumnSorter lvwBank_ColumnSorter;
        ColumnSorter lvwTrans_ColumnSorter;
        ColumnSorter lvwAfstemBank_ColumnSorter;
        ColumnSorter lvwAfstemTrans_ColumnSorter;
        private string DragDropKey;
        private decimal sumAfstemBank;
        private decimal sumAfstemTrans;
        private tblkontoudtog m_recKontoudtog;

        public FrmBankafstemning()
        {

            InitializeComponent();

            this.lvwBank_ColumnSorter = new ColumnSorter();
            this.lvwBank.ListViewItemSorter = lvwBank_ColumnSorter;
            this.lvwTrans_ColumnSorter = new ColumnSorter();
            this.lvwTrans.ListViewItemSorter = lvwTrans_ColumnSorter;
            this.lvwAfstemBank_ColumnSorter = new ColumnSorter();
            this.lvwAfstemBank.ListViewItemSorter = lvwAfstemBank_ColumnSorter;
            this.lvwAfstemTrans_ColumnSorter = new ColumnSorter();
            this.lvwAfstemTrans.ListViewItemSorter = lvwAfstemTrans_ColumnSorter;

            this.bsTblkontoudtog.DataSource = Program.dbDataTransSumma.tblkontoudtogs;
            m_recKontoudtog = (from w in Program.dbDataTransSumma.tblkontoudtogs select w).First();
        }

        private void setKontoudtog(int bankkontoid)
        {
            try
            {
                m_recKontoudtog = (from w in Program.dbDataTransSumma.tblkontoudtogs where w.pid == bankkontoid select w).First();
            }
            catch
            {
                m_recKontoudtog = null;
            }
        }

        private void lvwBank_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwBank_ColumnSorter.CurrentColumn = e.Column;
            this.lvwBank.Sort();
        }

        private void lvwTrans_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwTrans_ColumnSorter.CurrentColumn = e.Column;
            this.lvwTrans.Sort();
        }

        private void lvwAfstemBank_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwAfstemBank_ColumnSorter.CurrentColumn = e.Column;
            this.lvwAfstemBank.Sort();
        }

        private void lvwAfstemTrans_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvwAfstemTrans_ColumnSorter.CurrentColumn = e.Column;
            this.lvwAfstemTrans.Sort();
        }

        private void FrmBankafstemning_Load(object sender, EventArgs e)
        {
            getBankkonto();
            getTrans();
            lvwBank_ColumnSorter.CurrentColumn = 0;
            this.lvwBank.Sort();
            lvwTrans_ColumnSorter.CurrentColumn = 0;
            this.lvwTrans.Sort();

        }

        private void cmdForslag_Click(object sender, EventArgs e)
        {
            getBankkonto();
            getTrans();
        }

        private void getBankkonto()
        {
            int AntalForslag = 0;
            IEnumerable<clsqry_bank> qry_bank;
            if (this.AfstemtTidligere.Checked)
            {
                qry_bank = from b in Program.dbDataTransSumma.tblbankkontos
                           where b.afstem > 0 && b.bankkontoid == m_recKontoudtog.pid && (b.skjul == null || b.skjul == false)
                           orderby b.dato ascending
                           select new clsqry_bank
                           {
                               Pid = b.pid,
                               Dato = b.dato,
                               Tekst = b.tekst,
                               Belob = b.belob
                           };
            }
            else
            {

                qry_bank = from b in Program.dbDataTransSumma.tblbankkontos
                           where b.afstem == null && b.bankkontoid == m_recKontoudtog.pid && (b.skjul == null || b.skjul == false)
                           orderby b.dato ascending
                           select new clsqry_bank
                           {
                               Pid = b.pid,
                               Dato = b.dato,
                               Tekst = b.tekst,
                               Belob = b.belob
                           };
            }

            var antal = qry_bank.Count();
            this.lvwBank.Items.Clear();
            this.lvwAfstemBank.Items.Clear();
            this.lvwSumBank.Items.Clear();
            this.sumAfstemBank = 0;


            foreach (var b in qry_bank)
            {
                AntalForslag++;
                ListViewItem it = lvwBank.Items.Add(b.Pid.ToString(), string.Format("{0:yyyy-MM-dd}", b.Dato), 0);
                //it.Tag = b;
                it.SubItems.Add(b.Tekst);
                it.SubItems.Add(b.Belob.ToString());
            }
            this.lvwBank.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwBank.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwBank.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);

            this.columnHeaderABDato.Width = this.columnHeaderBDato.Width;
            this.columnHeaderABTekst.Width = this.columnHeaderBTekst.Width;
            this.columnHeaderABBelob.Width = this.columnHeaderBBelob.Width;

            this.columnHeaderSBDato.Width = this.columnHeaderBDato.Width;
            this.columnHeaderSBTekst.Width = this.columnHeaderBTekst.Width;
            this.columnHeaderSBBelob.Width = this.columnHeaderBBelob.Width;

            lvwAfstemBank_Sum();

        }

        private void getTrans()
        {
            int AntalForslag = 0;
            IEnumerable<clsqry_trans> qry_trans;
            if (this.AfstemtTidligere.Checked)
            {
                qry_trans = from t in Program.dbDataTransSumma.tbltrans
                            join b in Program.dbDataTransSumma.tblbilags on t.bilagpid equals b.pid
                            where t.afstem > 0 && t.kontonr == m_recKontoudtog.bogfkonto
                            orderby b.dato ascending
                            select new clsqry_trans
                            {
                                Pid = t.pid,
                                Dato = b.dato,
                                Bilag = b.bilag,
                                Tekst = t.tekst,
                                Belob = t.belob
                            };
            }
            else
            {

                qry_trans = from t in Program.dbDataTransSumma.tbltrans
                            join b in Program.dbDataTransSumma.tblbilags on t.bilagpid equals b.pid
                            where t.afstem == null && t.kontonr == m_recKontoudtog.bogfkonto
                            orderby b.dato ascending
                            select new clsqry_trans
                            {
                                Pid = t.pid,
                                Dato = b.dato,
                                Bilag = b.bilag,
                                Tekst = t.tekst,
                                Belob = t.belob
                            };
            }

            var antal = qry_trans.Count();
            this.lvwTrans.Items.Clear();
            this.lvwAfstemTrans.Items.Clear();
            this.lvwSumTrans.Items.Clear();
            this.sumAfstemTrans = 0;


            foreach (var t in qry_trans)
            {
                AntalForslag++;
                ListViewItem it = lvwTrans.Items.Add(t.Pid.ToString(), string.Format("{0:yyyy-MM-dd}", t.Dato), 0);
                //it.Tag = t;
                it.SubItems.Add(t.Bilag.ToString());
                it.SubItems.Add(t.Tekst);
                it.SubItems.Add(t.Belob.ToString());
            }
            this.lvwTrans.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwTrans.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
            this.lvwTrans.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwTrans.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);

            this.columnHeaderATDato.Width = this.columnHeaderTDato.Width;
            this.columnHeaderATBilag.Width = this.columnHeaderTBilag.Width;
            this.columnHeaderATTekst.Width = this.columnHeaderTTekst.Width;
            this.columnHeaderATBelob.Width = this.columnHeaderTBelob.Width;

            this.columnHeaderSTDato.Width = this.columnHeaderTDato.Width;
            this.columnHeaderSTBilag.Width = this.columnHeaderTBilag.Width;
            this.columnHeaderSTTekst.Width = this.columnHeaderTTekst.Width;
            this.columnHeaderSTBelob.Width = this.columnHeaderTBelob.Width;

            lvwAfstemTrans_Sum();

        }

        private void lvwBank_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "B" + random.Next(1000, 64000).ToString();
            lvwBank.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwBank_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("AB"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwBank_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwAfstemBank.SelectedItems)
            {
                ListViewItem it = lvwBank.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwAfstemBank.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwBank.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwBank.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwBank.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);

            this.columnHeaderABDato.Width = this.columnHeaderBDato.Width;
            this.columnHeaderABTekst.Width = this.columnHeaderBTekst.Width;
            this.columnHeaderABBelob.Width = this.columnHeaderBBelob.Width;

            this.columnHeaderSBDato.Width = this.columnHeaderBDato.Width;
            this.columnHeaderSBTekst.Width = this.columnHeaderBTekst.Width;
            this.columnHeaderSBBelob.Width = this.columnHeaderBBelob.Width;

            lvwAfstemBank_Sum();

        }

        private void lvwTrans_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "T" + random.Next(1000, 64000).ToString();
            lvwTrans.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwTrans_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("AT"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwTrans_DragDrop(object sender, DragEventArgs e)
        {
            foreach (ListViewItem lvi in lvwAfstemTrans.SelectedItems)
            {
                ListViewItem it = lvwTrans.Items.Add(lvi.Name, lvi.Text, 0);

                for (int i = 1; i < lvi.SubItems.Count; i++)
                {
                    string SubTekst = lvi.SubItems[i].Text;
                    it.SubItems.Add(SubTekst);

                }
            }
            foreach (ListViewItem lvi in lvwAfstemTrans.SelectedItems)
            {
                lvi.Remove();
            }
            this.lvwTrans.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwTrans.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
            this.lvwTrans.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            this.lvwTrans.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);

            this.columnHeaderATDato.Width = this.columnHeaderTDato.Width;
            this.columnHeaderATBilag.Width = this.columnHeaderTBilag.Width;
            this.columnHeaderATTekst.Width = this.columnHeaderTTekst.Width;
            this.columnHeaderATBelob.Width = this.columnHeaderTBelob.Width;

            this.columnHeaderSTDato.Width = this.columnHeaderTDato.Width;
            this.columnHeaderSTBilag.Width = this.columnHeaderTBilag.Width;
            this.columnHeaderSTTekst.Width = this.columnHeaderTTekst.Width;
            this.columnHeaderSTBelob.Width = this.columnHeaderTBelob.Width;

            lvwAfstemTrans_Sum();
        }


        private void lvwAfstemBank_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "AB" + random.Next(1000, 64000).ToString();
            lvwAfstemBank.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwAfstemBank_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("B"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwAfstemBank_DragDrop(object sender, DragEventArgs e)
        {
            if (DragDropKey.StartsWith("B"))
            {
                foreach (ListViewItem lvi in lvwBank.SelectedItems)
                {
                    ListViewItem it = lvwAfstemBank.Items.Add(lvi.Name, lvi.Text, 0);

                    for (int i = 1; i < lvi.SubItems.Count; i++)
                    {
                        string SubTekst = lvi.SubItems[i].Text;
                        it.SubItems.Add(SubTekst);

                    }
                }
                foreach (ListViewItem lvi in lvwBank.SelectedItems)
                {
                    lvi.Remove();
                }
            }
            this.columnHeaderABDato.Width = this.columnHeaderBDato.Width;
            this.columnHeaderABTekst.Width = this.columnHeaderBTekst.Width;
            this.columnHeaderABBelob.Width = this.columnHeaderBBelob.Width;

            this.columnHeaderSBDato.Width = this.columnHeaderBDato.Width;
            this.columnHeaderSBTekst.Width = this.columnHeaderBTekst.Width;
            this.columnHeaderSBBelob.Width = this.columnHeaderBBelob.Width;

            lvwAfstemBank_Sum();
        }

        private void lvwAfstemTrans_ItemDrag(object sender, ItemDragEventArgs e)
        {
            Random random = new Random();
            DragDropKey = "AT" + random.Next(1000, 64000).ToString();
            lvwAfstemBank.DoDragDrop(DragDropKey, DragDropEffects.Move);
        }

        private void lvwAfstemTrans_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                if (e.Data.GetData(DataFormats.Text).ToString() == DragDropKey)
                {
                    if (DragDropKey.StartsWith("T"))
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

            else
                e.Effect = DragDropEffects.None;
        }

        private void lvwAfstemTrans_DragDrop(object sender, DragEventArgs e)
        {
            if (DragDropKey.StartsWith("T"))
            {
                foreach (ListViewItem lvi in lvwTrans.SelectedItems)
                {
                    ListViewItem it = lvwAfstemTrans.Items.Add(lvi.Name, lvi.Text, 0);

                    for (int i = 1; i < lvi.SubItems.Count; i++)
                    {
                        string SubTekst = lvi.SubItems[i].Text;
                        it.SubItems.Add(SubTekst);

                    }
                }
                foreach (ListViewItem lvi in lvwTrans.SelectedItems)
                {
                    lvi.Remove();
                }
            }
            this.columnHeaderATDato.Width = this.columnHeaderTDato.Width;
            this.columnHeaderATBilag.Width = this.columnHeaderTBilag.Width;
            this.columnHeaderATTekst.Width = this.columnHeaderTTekst.Width;
            this.columnHeaderATBelob.Width = this.columnHeaderTBelob.Width;

            this.columnHeaderSTDato.Width = this.columnHeaderTDato.Width;
            this.columnHeaderSTBilag.Width = this.columnHeaderTBilag.Width;
            this.columnHeaderSTTekst.Width = this.columnHeaderTTekst.Width;
            this.columnHeaderSTBelob.Width = this.columnHeaderTBelob.Width;

            lvwAfstemTrans_Sum();
        }

        private decimal lvwAfstemBank_Sum()
        {
            sumAfstemBank = 0;
            decimal belob = 0;
            foreach (ListViewItem lvi in lvwAfstemBank.Items)
            {
                string SubTekst = lvi.SubItems[2].Text;
                try
                {
                    belob = decimal.Parse(SubTekst);
                }
                catch
                {

                    belob = 0;
                }
                sumAfstemBank += belob;
            }
            this.lvwSumBank.Items.Clear();
            ListViewItem it = lvwSumBank.Items.Add("1", "", 0);
            //it.Tag = t;
            it.SubItems.Add("");
            it.SubItems.Add(sumAfstemBank.ToString());
            cmdAfstemt_Enable();
            return sumAfstemBank;
        }

        private decimal lvwAfstemTrans_Sum()
        {
            sumAfstemTrans = 0;
            decimal belob = 0;
            foreach (ListViewItem lvi in lvwAfstemTrans.Items)
            {
                string SubTekst = lvi.SubItems[3].Text;
                try
                {
                    belob = decimal.Parse(SubTekst);
                }
                catch
                {

                    belob = 0;
                }
                sumAfstemTrans += belob;
            }
            this.lvwSumTrans.Items.Clear();
            ListViewItem it = lvwSumTrans.Items.Add("1", "", 0);
            //it.Tag = t;
            it.SubItems.Add("");
            it.SubItems.Add("");
            it.SubItems.Add(sumAfstemTrans.ToString());
            cmdAfstemt_Enable();
            return sumAfstemTrans;
        }

        private void cmdAfstemt_Enable()
        {
            decimal delta = sumAfstemBank - sumAfstemTrans;
            if (delta == 0)
            {
                cmdAfstemt.Enabled = true;
            }
            else
            {
                cmdAfstemt.Enabled = false;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdAfstemt_Click(object sender, EventArgs e)
        {
            int count = 0;
            tblafstem recAfstem = new tblafstem
            {
                udskriv = true
            };

            foreach (ListViewItem lvi in lvwAfstemBank.Items)
            {
                string keyval = lvi.Name;
                int pid = int.Parse(keyval);
                tblbankkonto recBankkonto = (from b in Program.dbDataTransSumma.tblbankkontos where b.pid == pid select b).First();
                recAfstem.tblbankkontos.Add(recBankkonto);
                count++;
            }
            foreach (ListViewItem lvi in lvwAfstemTrans.Items)
            {
                string keyval = lvi.Name;
                int pid = int.Parse(keyval);
                tbltran recTrans = (from b in Program.dbDataTransSumma.tbltrans where b.pid == pid select b).First();
                recAfstem.tbltrans.Add(recTrans);
                count++;
            }
            if (count > 0)
            {
                Program.dbDataTransSumma.tblafstems.InsertOnSubmit(recAfstem);
                Program.dbDataTransSumma.SubmitChanges();
                this.lvwAfstemBank.Items.Clear();
                this.lvwAfstemTrans.Items.Clear();
                this.lvwSumBank.Items.Clear();
                this.lvwSumTrans.Items.Clear();
            }
        }

        private void cbBankkonto_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int pid = (int)cbBankkonto.SelectedValue;
                if (pid != m_recKontoudtog.pid)
                {
                    setKontoudtog(pid);
                    getBankkonto();
                    getTrans();
                }
            }
            catch { }
        }
    }

    public class clsqry_bank
    {
        public int? Pid { get; set; }
        public DateTime? Dato { get; set; }
        public string Tekst { get; set; }
        public decimal? Belob { get; set; }
    }

    public class clsqry_trans
    {
        public int? Pid { get; set; }
        public DateTime? Dato { get; set; }
        public int? Bilag { get; set; }
        public string Tekst { get; set; }
        public decimal? Belob { get; set; }
    }
}
