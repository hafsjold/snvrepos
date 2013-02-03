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
            txtBoxDatabase.Text = @"C:\Users\mha\Documents\Visual Studio 2010\Projects\docdblite\docdblite\docdblite2.db3";
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
                Guid id = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(file);
                string navn = fileInfo.Name;

                frmAddDoc m_frmAddDoc = new frmAddDoc();
                m_frmAddDoc.Dokument = navn;
                m_frmAddDoc.ShowDialog();

                tbldoc rec_doc = new tbldoc
                {
                    id = id,
                    navn = navn,
                    selskab = m_frmAddDoc.Selskab,
                    år = m_frmAddDoc.År,
                    produkt = m_frmAddDoc.Produkt
                };
                dblite.tbldoc.AddObject(rec_doc);

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
                var qry = from doc in dblite.tbldoc select doc;
                tbldocBindingSource.DataSource = qry;
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
                "  [navn] NVARCHAR(256), " +
                "  [selskab] NVARCHAR(256), " +
                "  [år] INT, " +
                "  [produkt] NVARCHAR(256), " +
                "  CONSTRAINT [] PRIMARY KEY ([id]) ON CONFLICT ROLLBACK);";
                cmd.ExecuteNonQuery();
                conn.Close();
                dblite = new docdbliteEntities(connectionString);
            }
            else
            {
                dblite = new docdbliteEntities(connectionString);
            }
            var qry = from doc in dblite.tbldoc select doc;
            tbldocBindingSource.DataSource = qry;
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
                              Navn = doc.navn,
                              Data = data.data
                          };

                foreach (var rec in qry)
                {
                    FileInfo fi = new FileInfo(rec.Navn);
                    var Ext = fi.Extension;

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
                        saveFileDialog1.FileName = rec.Navn;
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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBoxDatabase.Text = openFileDialog1.FileName;
            }
        }

    }
}

