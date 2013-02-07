using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Mime;
using System.Data.SQLite;

namespace docdblite
{
    public partial class frmDocDblite : Form
    {
        docdbliteEntities dblite;
        string DatabaseFile;
        string connectionString;
        frmIE m_frmIE;

        public frmDocDblite()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
#if (DEBUG)
            txtBoxDatabase.Text = @"C:\Users\mha\Documents\Visual Studio 2010\Projects\docdblite\docdblite\docdblite.db3";
#else
            txtBoxDatabase.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\docdblite.db3";
#endif
        }
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                string kilde_sti = fileInfo.FullName;

                frmAddDoc m_frmAddDoc = new frmAddDoc();
                m_frmAddDoc.Dokument = kilde_sti;
                DialogResult Result = m_frmAddDoc.ShowDialog();
                if (Result == System.Windows.Forms.DialogResult.OK)
                {
                    Guid id = Guid.NewGuid();
                    int ref_nr = 0;
                    try
                    {
                        tblrefnr rec_refnr = (from n in dblite.tblrefnr where n.keyname == "ref_nr" select n).First();
                        rec_refnr.nr++;
                        ref_nr = rec_refnr.nr;
                        dblite.SaveChanges();
                    }
                    catch
                    {
                        ref_nr = 1;
                        tblrefnr rec_refnr = new tblrefnr { keyname = "ref_nr", nr = ref_nr };
                        dblite.tblrefnr.AddObject(rec_refnr);
                        dblite.SaveChanges();
                    }

                    tbldoc rec_doc = new tbldoc
                    {
                        id = id,
                        ref_nr = ref_nr,
                        virksomhed = m_frmAddDoc.Virksomhed,
                        emne = m_frmAddDoc.Emne,
                        dokument_type = m_frmAddDoc.Dokument_type,
                        år = m_frmAddDoc.År,
                        ekstern_kilde = m_frmAddDoc.Ekstern_kilde,
                        beskrivelse = m_frmAddDoc.Beskrivelse,
                        oprettes_af = m_frmAddDoc.Oprettet_af,
                        oprettet_dato = m_frmAddDoc.Oprettet_dato,
                        kilde_sti = kilde_sti
                    };
                    dblite.tbldoc.AddObject(rec_doc);
                    dblite.SaveChanges();

                    FileStream fs = fileInfo.OpenRead();
                    long ln = fileInfo.Length;
                    byte[] file_bytes = new byte[ln];
                    fs.Read(file_bytes, 0, (int)ln);

                    tblData rec_Data = new tblData
                    {
                        id = id,
                        data = file_bytes
                    };
                    dblite.tblData.AddObject(rec_Data);
                    dblite.SaveChanges();

                    List<tbldoc> lst = (from doc in dblite.tbldoc select doc).ToList<tbldoc>();
                    SortableBindingList<tbldoc> lbindingList = new SortableBindingList<tbldoc>();
                    foreach (tbldoc c in lst) lbindingList.Add(c);
                    tbldocBindingSource.DataSource = lbindingList;
                }
            }
        }

        private void butDatabase_Click(object sender, EventArgs e)
        {
            DatabaseFile = txtBoxDatabase.Text;
            FileInfo DatabasefileInfo = new FileInfo(DatabaseFile);
            connectionString = @"metadata=res://*/dblite.csdl|res://*/dblite.ssdl|res://*/dblite.msl;provider=System.Data.SQLite;provider connection string='data source=""" + DatabaseFile + @""" '";
            if (!DatabasefileInfo.Exists)
            {
                SQLiteConnection.CreateFile(DatabaseFile);
                string datasource = "Data Source=" + DatabaseFile + ";Version=3";
                SQLiteConnection conn = new SQLiteConnection(datasource);
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                "CREATE TABLE [tblData] ( " +
                "  [id] GUID NOT NULL REFERENCES [tbldoc]([id]) ON DELETE CASCADE ON UPDATE CASCADE, " +
                "  [data] IMAGE, " +
                "  CONSTRAINT [] PRIMARY KEY ([id]) ON CONFLICT ROLLBACK); " +
                " " +
                " " +
                "CREATE TABLE [tbldoc] ( " +
                "  [id] GUID NOT NULL, " +
                "  [ref_nr] INT, " +
                "  [virksomhed] NVARCHAR(50), " +
                "  [emne] VARCHAR(50), " +
                "  [dokument_type] VARCHAR(50), " +
                "  [år] INT, " +
                "  [ekstern_kilde] VARCHAR(50), " +
                "  [beskrivelse] NVARCHAR(100), " +
                "  [oprettes_af] VARCHAR(25), " +
                "  [oprettet_dato] DATETIME, " +
                "  [kilde_sti] NVARCHAR(512), " +
                "  CONSTRAINT [] PRIMARY KEY ([id]) ON CONFLICT ROLLBACK); " +
                " " +
                " " +
                "CREATE TABLE [tblrefnr] ( " +
                "  [keyname] NVARCHAR(10) NOT NULL, " +
                "  [nr] INT NOT NULL DEFAULT (0), " +
                "  CONSTRAINT [] PRIMARY KEY ([keyname]));";
                cmd.ExecuteNonQuery();
                conn.Close();
                dblite = new docdbliteEntities(connectionString);
            }
            else
            {
                dblite = new docdbliteEntities(connectionString);
            }
            var qry = from doc in dblite.tbldoc select doc;

            List<tbldoc> lst = (from doc in dblite.tbldoc select doc).ToList<tbldoc>();
            SortableBindingList<tbldoc> lbindingList = new SortableBindingList<tbldoc>();
            foreach (tbldoc c in lst) lbindingList.Add(c);
            tbldocBindingSource.DataSource = lbindingList;
        }
        private void tbldocDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = tbldocDataGridView.Rows[e.RowIndex];
                tbldoc selectedrow = selectedRow.DataBoundItem as tbldoc;

                var qry = from doc in dblite.tbldoc
                          where doc.id == selectedrow.id
                          join data in dblite.tblData on doc.id equals data.id
                          select new
                          {
                              Id = doc.id,
                              kilde_sti = doc.kilde_sti,
                              Data = data.data
                          };

                foreach (var rec in qry)
                {
                    FileInfo fi = new FileInfo(rec.kilde_sti);
                    var Ext = fi.Extension;
                    var Name = fi.Name;

                    if (Ext.ToLower() == ".pdf")
                    {
                        byte[] bytes = rec.Data;
                        m_frmIE = new frmIE();
                        m_frmIE.WebBrowser1.LoadBytes(bytes, MediaTypeNames.Application.Pdf);
                        m_frmIE.Show();
                    }
                    else
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        saveFileDialog1.Filter = "|*" + Ext;
                        saveFileDialog1.Title = "Save File";
                        saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        saveFileDialog1.FileName = Name;
                        saveFileDialog1.ShowDialog();

                        string path = saveFileDialog1.FileName;
                        byte[] file_byte = rec.Data.ToArray();

                        FileInfo fileInfo = new FileInfo(path);
                        FileStream fs = fileInfo.OpenWrite();
                        fs.Write(file_byte, 0, file_byte.Length);
                        fs.Flush();
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Database Files|*.db3";
            openFileDialog1.Title = "Select a Database File";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.ValidateNames = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBoxDatabase.Text = openFileDialog1.FileName;
            }
        }
        
        private void tbldocDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            int column = e.ColumnIndex;
            DataGridViewColumn newColumn = tbldocDataGridView.Columns[e.ColumnIndex];

            DataGridViewColumn oldColumn = tbldocDataGridView.SortedColumn;
            ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not currently sorted.
            if (oldColumn != null)
            {
                // Sort the same column again, reversing the SortOrder.
                if (oldColumn == newColumn &&
                    tbldocDataGridView.SortOrder == SortOrder.Ascending)
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = ListSortDirection.Ascending;
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
            else
            {
                direction = ListSortDirection.Ascending;
            }

            tbldocDataGridView.Sort(newColumn, direction);

            newColumn.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;

        }
  

    }
}

