using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace bjViewer
{
    public partial class Form1 : Form
    {
        private xmldocs db = null;
        string SelectedPath; 
        Assembly ass = null;
       
        public Form1()
        {
            InitializeComponent();
            SelectedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ass = Assembly.GetExecutingAssembly();
            string[] res = ass.GetManifestResourceNames();

            try
            {
                string name = "bjArkiv.xml.gz"; 
                Stream rs = ass.GetManifestResourceStream(name);
                using (Stream gzip = new GZipStream(rs, CompressionMode.Decompress, true))
                {
                    using (MemoryStream menfile = new MemoryStream())
                    {
                        for (int b = gzip.ReadByte(); b != -1; b = gzip.ReadByte())
                        {
                            menfile.WriteByte((byte)b);
                        }
                        db = xmldocs.Load(menfile);
                        xmldocsBindingSource.DataSource = db;                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ass.GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                xmldoc doc = selectedRow.DataBoundItem as xmldoc;
                //MessageBox.Show("Debug1:  " + doc.kilde_sti);
                
                string name = doc.id.ToString() + @".gz";

                DirectoryInfo di = new DirectoryInfo(SelectedPath);
                try { di.Create(); } catch { }
                string path = Path.Combine(SelectedPath, doc.kilde_sti); // remove ".gz";
                
                // ?????? Create output folder 

                try
                {
                    FileInfo fi = new FileInfo(path);
                    if (!fi.Exists)
                    {
                        Stream rs = ass.GetManifestResourceStream(name);
                        using (Stream gzip = new GZipStream(rs, CompressionMode.Decompress, true))
                        {
                            using (Stream file = File.Create(path))
                            {
                                for (int b = gzip.ReadByte(); b != -1; b = gzip.ReadByte())
                                {
                                    file.WriteByte((byte)b);
                                }
                            }

                        }
                    }
                    Process.Start(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, ass.GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
 
    }
}
