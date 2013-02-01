using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace docdb
{
    public partial class Form1 : Form
    {
        Docdb_Sdf db;
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            db = new Docdb_Sdf(@"C:\Users\mha\Documents\Visual Studio 2010\Projects\docdb\docdb\docdb.sdf");
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
                Console.WriteLine(file);
                Guid id = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(file);
                
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
          
            }
        }

        private void butReadDoc_Click(object sender, EventArgs e)
        {
            TblData rec_Data = (from data in db.TblData select data).Single();
            byte[] file_byte = rec_Data.Data.ToArray();
            string path = @"c:\mhatest\testfile";
            FileInfo fileInfo = new FileInfo(path);
            FileStream fs = fileInfo.OpenWrite();
            fs.Write(file_byte, 0, file_byte.Length);
            fs.Flush();
        }
    }
}
    
