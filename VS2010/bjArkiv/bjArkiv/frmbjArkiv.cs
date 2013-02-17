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
using System.Diagnostics;

namespace bjArkiv
{
    public partial class frmbjArkiv : Form
    {
        docdbliteEntities dblite;
        string connectionString;
        SortableBindingList<tbldoc> blSortableBindingList;
        bool bLastArkiv;
        string arkiv_root_folder;

        public string Database
        {
            get { return arkiv_root_folder + Program.BJARKIV; }
        }


        public frmbjArkiv()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(frmbjArkiv_DragEnter);
            this.DragDrop += new DragEventHandler(frmbjArkiv_DragDrop);
            bLastArkiv = false;
            try
            {
                DirectoryInfo LastbjArkivdirInfo = new DirectoryInfo(global::bjArkiv.Properties.Settings.Default.strLastbjArkiv);
                if (LastbjArkivdirInfo.Exists)
                {
                    FileInfo fi = new FileInfo(LastbjArkivdirInfo.FullName + Program.BJARKIV);
                    if (fi.Exists) bLastArkiv = true;
                }
            }
            catch { }

#if (DEBUG)
            if (bLastArkiv)
            {
                arkiv_root_folder = global::bjArkiv.Properties.Settings.Default.strLastbjArkiv;
                txtBoxbjArkiv.Text = arkiv_root_folder;
                openDatabase();
            }
            else
            {
                arkiv_root_folder = "";
                txtBoxbjArkiv.Text = arkiv_root_folder;
            }
#else
            if (bLastDatabase)
            {
                arkiv_root_folder = global::bjArkiv.Properties.Settings.Default.strLastbjArkiv;
                bjArkivWatcher.Path = arkiv_root_folder;
                txtBoxbjArkiv.Text = arkiv_root_folder;
                openDatabase();
            }
            else
            {
                arkiv_root_folder = "";
                bjArkivWatcher.Path = arkiv_root_folder;
                txtBoxbjArkiv.Text = arkiv_root_folder;
            }
#endif

        }

        private void CreateMissingFolders(DirectoryInfo di)
        {
            if (!di.Exists)
            {
                CreateMissingFolders(di.Parent);
                di.Create();
            }
        }

        private bool createNewbjArkiv(string DatabaseFile)
        {
            FileInfo DatabasefileInfo = new FileInfo(DatabaseFile);
            CreateMissingFolders(DatabasefileInfo.Directory);

            if (!DatabasefileInfo.Exists)
            {
                SQLiteConnection.CreateFile(DatabaseFile);
                string datasource = "Data Source=" + DatabaseFile + ";Version=3";
                SQLiteConnection conn = new SQLiteConnection(datasource);
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText =
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
            DirectoryInfo LastbjArkivdirInfo = new DirectoryInfo(arkiv_root_folder);
            if (LastbjArkivdirInfo.Exists)
            {
                FileInfo fi = new FileInfo(Database);
                if (fi.Exists)
                {
                    try
                    {
                        connectionString = @"metadata=res://*/dblite.csdl|res://*/dblite.ssdl|res://*/dblite.msl;provider=System.Data.SQLite;provider connection string='data source=""" + Database + @""" '";
                        dblite = new docdbliteEntities(connectionString);

                        IEnumerable<tbldoc> qry = from doc in dblite.tbldoc select doc;
                        blSortableBindingList = new SortableBindingList<tbldoc>();
                        foreach (tbldoc rec in qry) blSortableBindingList.Add(rec);
                        tbldocBindingSource.DataSource = blSortableBindingList;

                        global::bjArkiv.Properties.Settings.Default.strLastbjArkiv = arkiv_root_folder;
                        return true;
                    }
                    catch
                    {
                        string messageBoxText = "Kan ikke åbne metadata i " + arkiv_root_folder;
                        string caption = "bjArkiv";
                        MessageBoxButtons button = MessageBoxButtons.OK;
                        MessageBoxIcon icon = MessageBoxIcon.Warning;
                        MessageBox.Show(messageBoxText, caption, button, icon);
                    }
                }
                else
                {
                    string messageBoxText = arkiv_root_folder + " er ikke et bjArkiv";
                    string caption = "bjArkiv";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Warning;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                }
            }
            return false;
        }

        void frmbjArkiv_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            if (e.Data.GetDataPresent("FileGroupDescriptor")) e.Effect = DragDropEffects.All;
        }

        void frmbjArkiv_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileGroupDescriptor"))
                Outlook_DragDrop(sender, e);
            else
                Stifinder_DragDrop(sender, e);
        }

        void Outlook_DragDrop(object sender, DragEventArgs e)
        {
            OutlookDataObject dataObject = new OutlookDataObject(e.Data);
            string[] filenames = (string[])dataObject.GetData("FileGroupDescriptor");
            MemoryStream[] filestreams = (MemoryStream[])dataObject.GetData("FileContents");
            for (int fileIndex = 0; fileIndex < filenames.Length; fileIndex++)
            {
                string file = filenames[fileIndex];
                MemoryStream fs = filestreams[fileIndex];

                FileInfo from_fileinfo = new FileInfo(file);
                string to_path = arkiv_root_folder + @"\" + from_fileinfo.Name;
                //********************************************************************
                FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
                openFileDialog1.Description = "Vælg Arkiv Folder";
                openFileDialog1.RootFolder = Environment.SpecialFolder.MyDocuments;
                openFileDialog1.ShowNewFolderButton = true;
                openFileDialog1.SelectedPath = arkiv_root_folder;
                bool bFound = false;
                while (!bFound)
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (openFileDialog1.SelectedPath.StartsWith(arkiv_root_folder, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (!openFileDialog1.SelectedPath.StartsWith(arkiv_root_folder + @"\.bja", StringComparison.CurrentCultureIgnoreCase))
                            {
                                to_path = openFileDialog1.SelectedPath + @"\" + from_fileinfo.Name;
                                bFound = true;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //********************************************************************
                FileInfo to_fileinfo = new FileInfo(to_path);
                DirectoryInfo to_dir = new DirectoryInfo(to_fileinfo.DirectoryName);
                try
                {
                    if (!to_dir.Exists) to_dir.Create();
                    FileStream outputStream = File.Create(to_path);
                    fs.WriteTo(outputStream);
                    outputStream.Close();
                }
                catch
                {
                    string messageBoxText = "Kan ikke tilføje  " + file + " til Arkiv";
                    string caption = "bjArkiv";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Warning;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                    return;
                }
            }
        }

        void Stifinder_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                FileInfo from_fileinfo = new FileInfo(file);
                string to_path = arkiv_root_folder + @"\" + from_fileinfo.Name;
                //********************************************************************
                FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
                openFileDialog1.Description = "Vælg Arkiv Folder";
                openFileDialog1.RootFolder = Environment.SpecialFolder.MyDocuments;
                openFileDialog1.ShowNewFolderButton = true;
                openFileDialog1.SelectedPath = arkiv_root_folder;
                bool bFound = false;
                while (!bFound)
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (openFileDialog1.SelectedPath.StartsWith(arkiv_root_folder, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (!openFileDialog1.SelectedPath.StartsWith(arkiv_root_folder + @"\.bja", StringComparison.CurrentCultureIgnoreCase))
                            {
                                to_path = openFileDialog1.SelectedPath + @"\" + from_fileinfo.Name;
                                bFound = true;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //********************************************************************
                FileInfo to_fileinfo = new FileInfo(to_path);
                DirectoryInfo to_dir = new DirectoryInfo(to_fileinfo.DirectoryName);
                try
                {
                    if (!to_dir.Exists) to_dir.Create();
                    from_fileinfo.CopyTo(to_path);
                }
                catch
                {
                    string messageBoxText = "Kan ikke tilføje  " + file + " til Arkiv";
                    string caption = "bjArkiv";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Warning;
                    MessageBox.Show(messageBoxText, caption, button, icon);
                    return;
                }
            }
        }

        private void tbldocDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = tbldocDataGridView.Rows[e.RowIndex];
                tbldoc selectedrow = selectedRow.DataBoundItem as tbldoc;

                var qry = from doc in dblite.tbldoc where doc.id == selectedrow.id select doc;

                foreach (var rec in qry)
                {
                    try
                    {
                        Process cc = Process.Start(arkiv_root_folder + @"\" + rec.kilde_sti);
                    }
                    catch { }
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

                    var rec = (from doc in dblite.tbldoc where doc.id == rec_doc_view.id select doc).First();
                    Process cc = Process.Start(arkiv_root_folder + @"\" + rec.kilde_sti);
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

                    var rec = (from doc in dblite.tbldoc where doc.id == rec_doc_view.id select doc).First();

                    FileInfo fi_intern = new FileInfo(rec.kilde_sti);
                    var Ext = fi_intern.Extension;
                    var Name = fi_intern.Name;

                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "|*" + Ext;
                    saveFileDialog1.Title = "Save File";
                    saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFileDialog1.FileName = Name;
                    saveFileDialog1.ShowDialog();

                    string from_path = arkiv_root_folder + @"\" + Name;
                    string to_path = saveFileDialog1.FileName;

                    FileInfo from_file = new FileInfo(from_path);
                    FileInfo to_file = new FileInfo(to_path);
                    DirectoryInfo to_dir = new DirectoryInfo(to_file.DirectoryName);
                    if (!to_dir.Exists)
                        to_dir.Create();
                    from_file.CopyTo(to_path);
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
                    FileInfo fi_extern = new FileInfo(arkiv_root_folder + @"\" + rec.kilde_sti);
                    fi_extern.Delete();

                    dblite.tbldoc.DeleteObject(rec);
                    dblite.SaveChanges();
                    blSortableBindingList.Remove(rec);
                }
                catch { }
            }
        }

        private void åbenArkivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            openFileDialog1.Description = "Vælg Arkiv Folder";
            openFileDialog1.RootFolder = Environment.SpecialFolder.MyDocuments;
            openFileDialog1.ShowNewFolderButton = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                arkiv_root_folder = openFileDialog1.SelectedPath;
                 txtBoxbjArkiv.Text = arkiv_root_folder;
                openDatabase();
            }
        }

        private void NytArkivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            openFileDialog1.Description = "Vælg Arkiv Folder";
            openFileDialog1.RootFolder = Environment.SpecialFolder.MyDocuments;
            openFileDialog1.ShowNewFolderButton = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                arkiv_root_folder = openFileDialog1.SelectedPath;
                txtBoxbjArkiv.Text = arkiv_root_folder;
                FileInfo DatabasefileInfo = new FileInfo(Database);
                if (!DatabasefileInfo.Exists)
                    createNewbjArkiv(Database);
                openDatabase();
            }
        }

        private void lukProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmFileView fileView = new frmFileView();
            fileView.Show();

            //clsArkiv arkiv = new clsArkiv();
            //arkiv.EditMetadata(@"C:\Users\mha\Documents\Visual Studio 2010\Projects\testclient\testclient\testclient.csproj");
            //arkiv.EditMetadata(@"C:\Users\mha\Documents\mha_test_arkiv2\NYSvampeangreb0001.pdf");
        }

     }
}

