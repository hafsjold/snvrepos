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
        SortableBindingList<tbldoc> blSortableBindingList;
        bool bLastDatabase;

        public frmDocDblite()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            bLastDatabase = false;
            try
            {
                FileInfo LastDatabasefileInfo = new FileInfo(global::docdblite.Properties.Settings.Default.strLastDatabase);
                if (LastDatabasefileInfo.Exists) bLastDatabase = true;
            }
            catch { }

#if (DEBUG)
            if (bLastDatabase)
            {
                txtBoxDatabase.Text = global::docdblite.Properties.Settings.Default.strLastDatabase;
                DatabaseFile = global::docdblite.Properties.Settings.Default.strLastDatabase;
                openDatabase();
            }
            else
                txtBoxDatabase.Text = @"C:\Users\mha\Documents\Visual Studio 2010\Projects\docdblite\docdblite\docdblite.db3";
            //#else
            if (bLastDatabase)
            {
                txtBoxDatabase.Text = global::docdblite.Properties.Settings.Default.strLastDatabase;
                DatabaseFile = global::docdblite.Properties.Settings.Default.strLastDatabase;
                openDatabase();
            }
            else
                txtBoxDatabase.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\docdblite.db3";
#endif

        }

        private bool createNewDatabase(string DatabaseFile)
        {
            FileInfo DatabasefileInfo = new FileInfo(DatabaseFile);
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
                return true;
            }
            return false;
        }

        private bool openDatabase()
        {
            connectionString = @"metadata=res://*/dblite.csdl|res://*/dblite.ssdl|res://*/dblite.msl;provider=System.Data.SQLite;provider connection string='data source=""" + DatabaseFile + @""" '";
            dblite = new docdbliteEntities(connectionString);

            IEnumerable<tbldoc> qry = from doc in dblite.tbldoc select doc;
            blSortableBindingList = new SortableBindingList<tbldoc>();
            foreach (tbldoc rec in qry) blSortableBindingList.Add(rec);
            tbldocBindingSource.DataSource = blSortableBindingList;

            global::docdblite.Properties.Settings.Default.strLastDatabase = DatabaseFile; return false;
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            if (e.Data.GetDataPresent("FileGroupDescriptor")) e.Effect = DragDropEffects.All;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileGroupDescriptor"))
                Outlook_DragDrop(sender, e);
            else
                Stifinder_DragDrop(sender, e);
        }

        void Outlook_DragDrop(object sender, DragEventArgs e)
        {
            //wrap standard IDataObject in OutlookDataObject
            OutlookDataObject dataObject = new OutlookDataObject(e.Data);

            //get the names and data streams of the files dropped
            string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");
            MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");

            for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
            {
                //use the fileindex to get the name and data stream
                string file = filenames[fileIndex];
                MemoryStream fs = filestreams[fileIndex];

                FileInfo fileInfo = new FileInfo(file);
                string kilde_sti = fileInfo.Name;

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
                    blSortableBindingList.Add(rec_doc);

                    //FileStream fs = fileInfo.OpenRead();
                    long ln = fs.Length;
                    byte[] file_bytes = new byte[ln];
                    fs.Read(file_bytes, 0, (int)ln);

                    tblData rec_Data = new tblData
                    {
                        id = id,
                        data = file_bytes
                    };
                    dblite.tblData.AddObject(rec_Data);
                    dblite.SaveChanges();

                }
             }
        }

        void Stifinder_DragDrop(object sender, DragEventArgs e)
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
                    blSortableBindingList.Add(rec_doc);

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

                }
            }
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
                }
            }

        }


        private void tbldocDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = tbldocDataGridView.HitTest(e.X, e.Y);
                int hitcol = hit.ColumnIndex;
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    tbldocDataGridView.ClearSelection();
                    tbldocDataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                    this.contextMenuDoc.Show(this.tbldocDataGridView, new Point(e.X, e.Y));
                }

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cells = tbldocDataGridView.SelectedCells;
            if (cells.Count > 0)
            {
                try
                {
                    DataGridViewTextBoxCell cell = cells[0] as DataGridViewTextBoxCell;
                    tbldoc rec_doc_view = cell.OwningRow.DataBoundItem as tbldoc;
                    frmAddDoc m_frmAddDoc = new frmAddDoc();
                    m_frmAddDoc.Ref_nr = (int)rec_doc_view.ref_nr;
                    m_frmAddDoc.Dokument = rec_doc_view.kilde_sti;
                    m_frmAddDoc.Virksomhed = rec_doc_view.virksomhed;
                    m_frmAddDoc.Emne = rec_doc_view.emne;
                    m_frmAddDoc.Dokument_type = rec_doc_view.dokument_type;
                    m_frmAddDoc.År = (int)rec_doc_view.år;
                    m_frmAddDoc.Ekstern_kilde = rec_doc_view.ekstern_kilde;
                    m_frmAddDoc.Beskrivelse = rec_doc_view.beskrivelse;
                    m_frmAddDoc.Oprettet_af = rec_doc_view.oprettes_af;
                    m_frmAddDoc.Oprettet_dato = (DateTime)rec_doc_view.oprettet_dato;
                    m_frmAddDoc.Opret = false;
                    DialogResult Result = m_frmAddDoc.ShowDialog();
                    if (Result == System.Windows.Forms.DialogResult.OK)
                    {
                        tbldoc rec_doc_db = null;
                        try
                        {
                            rec_doc_db = (from doc in dblite.tbldoc where doc.id == rec_doc_view.id select doc).First();

                            rec_doc_db.virksomhed = m_frmAddDoc.Virksomhed;
                            rec_doc_db.emne = m_frmAddDoc.Emne;
                            rec_doc_db.dokument_type = m_frmAddDoc.Dokument_type;
                            rec_doc_db.år = m_frmAddDoc.År;
                            rec_doc_db.ekstern_kilde = m_frmAddDoc.Ekstern_kilde;
                            rec_doc_db.beskrivelse = m_frmAddDoc.Beskrivelse;

                            rec_doc_view.virksomhed = m_frmAddDoc.Virksomhed;
                            rec_doc_view.emne = m_frmAddDoc.Emne;
                            rec_doc_view.dokument_type = m_frmAddDoc.Dokument_type;
                            rec_doc_view.år = m_frmAddDoc.År;
                            rec_doc_view.ekstern_kilde = m_frmAddDoc.Ekstern_kilde;
                            rec_doc_view.beskrivelse = m_frmAddDoc.Beskrivelse;

                            dblite.SaveChanges();
                        }
                        catch { }
                    }

                }
                catch { }
            }
        }

        private void frmDocDblite_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void visDokumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cells = tbldocDataGridView.SelectedCells;
            if (cells.Count > 0)
            {
                try
                {
                    DataGridViewTextBoxCell cell = cells[0] as DataGridViewTextBoxCell;
                    tbldoc rec_doc_view = cell.OwningRow.DataBoundItem as tbldoc;

                    var rec = (from doc in dblite.tbldoc
                               where doc.id == rec_doc_view.id
                               join data in dblite.tblData on doc.id equals data.id
                               select new
                               {
                                   Id = doc.id,
                                   kilde_sti = doc.kilde_sti,
                                   Data = data.data
                               }).First();

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
                }
                catch { }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cells = tbldocDataGridView.SelectedCells;
            if (cells.Count > 0)
            {
                try
                {
                    DataGridViewTextBoxCell cell = cells[0] as DataGridViewTextBoxCell;
                    tbldoc rec_doc_view = cell.OwningRow.DataBoundItem as tbldoc;

                    var rec = (from doc in dblite.tbldoc
                               where doc.id == rec_doc_view.id
                               join data in dblite.tblData on doc.id equals data.id
                               select new
                               {
                                   Id = doc.id,
                                   kilde_sti = doc.kilde_sti,
                                   Data = data.data
                               }).First();

                    FileInfo fi = new FileInfo(rec.kilde_sti);
                    var Ext = fi.Extension;
                    var Name = fi.Name;

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
                catch { }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection cells = tbldocDataGridView.SelectedCells;
            if (cells.Count > 0)
            {
                try
                {
                    DataGridViewTextBoxCell cell = cells[0] as DataGridViewTextBoxCell;
                    tbldoc rec_doc_view = cell.OwningRow.DataBoundItem as tbldoc;

                    tbldoc rec = (from doc in dblite.tbldoc where doc.id == rec_doc_view.id select doc).First();
                    dblite.tbldoc.DeleteObject(rec);
                    dblite.SaveChanges();
                    blSortableBindingList.Remove(rec);
                }
                catch { }
            }
        }

        private void åbenDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Database Files|*.db3";
            openFileDialog1.Title = "Select a Database File";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.ValidateNames = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBoxDatabase.Text = openFileDialog1.FileName;
                DatabaseFile = openFileDialog1.FileName;
                openDatabase();
            }
        }

        private void nyDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
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
                DatabaseFile = txtBoxDatabase.Text;
                FileInfo DatabasefileInfo = new FileInfo(DatabaseFile);
                if (!DatabasefileInfo.Exists)
                    createNewDatabase(DatabaseFile);
                openDatabase();
            }

        }

        private void lukProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

