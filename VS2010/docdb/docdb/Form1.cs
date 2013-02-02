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

namespace docdb
{
    public partial class Form1 : Form
    {
        Docdb_Sdf db;
        string Database;
        frmIE m_frmIE;

        public Form1()
        {
            InitializeComponent();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
#if (DEBUG)            
            txtBoxDatabase.Text = @"C:\Users\mha\Documents\Visual Studio 2010\Projects\docdb\docdb\docdb.sdf";
#else
            txtBoxDatabase.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\docdb.sdf";
#endif
            //db = new Docdb_Sdf(Database);
            //txtBoxDatabase.Text = Database;
            //var qry = from doc in db.Tbldoc select doc;
            //tbldocBindingSource.DataSource = qry;
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

                Tbldoc rec_doc = new Tbldoc 
                {
                    Id = id,
                    Navn = navn,
                    Selskab = m_frmAddDoc.Selskab,
                    åR = m_frmAddDoc.År,
                    Produkt = m_frmAddDoc.Produkt
                };
                db.Tbldoc.InsertOnSubmit(rec_doc);
                
                FileStream fs = fileInfo.OpenRead();
                long ln = fileInfo.Length;
                byte[] file_byte = new byte[ln];
                fs.Read(file_byte,0,(int)ln);
                System.Data.Linq.Binary file_binary = new System.Data.Linq.Binary(file_byte);

                TblData rec_Data = new TblData 
                {
                    Id = id,
                    Data = file_binary
            
                };
                db.TblData.InsertOnSubmit(rec_Data);
                db.SubmitChanges();
                var qry = from doc in db.Tbldoc select doc;
                tbldocBindingSource.DataSource = qry;
          
            }
        }

        private void butDatabase_Click(object sender, EventArgs e)
        {
            Database = txtBoxDatabase.Text;
            FileInfo DatabasefileInfo = new FileInfo(Database);
            if (!DatabasefileInfo.Exists)
            {
                db = new Docdb_Sdf(Database);
                db.CreateDatabase();
            }
            else
            {
                db = new Docdb_Sdf(Database);
            }
            var qry = from doc in db.Tbldoc select doc;
            tbldocBindingSource.DataSource = qry;
        }

        private void tbldocDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = tbldocDataGridView.Rows[e.RowIndex];
                Tbldoc selectedrow = selectedRow.DataBoundItem as Tbldoc;

                var qry = from doc in db.Tbldoc
                          where doc.Id == selectedrow.Id
                          join data in db.TblData on doc.Id equals data.Id
                          select new
                          {
                              Id = doc.Id,
                              Navn = doc.Navn,
                              Data = data.Data
                          };

                foreach (var rec in qry)
                {
                    FileInfo fi = new FileInfo(rec.Navn);
                    var Ext = fi.Extension;

                    if (Ext.ToLower() == ".pdf")
                    {
                        byte[] bytes = rec.Data.ToArray();
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
            OpenFileDialog  openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Database Files|*.sdf";
            openFileDialog1.Title = "Select a Database File";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
             if (openFileDialog1.ShowDialog() == DialogResult.OK)
             {
                 txtBoxDatabase.Text = openFileDialog1.FileName;
             }
        }

    }
}
    
