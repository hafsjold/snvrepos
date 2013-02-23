using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace bjExtract
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DirectoryInfo di = new DirectoryInfo(Program.temp);
            try { Program.CreateMissingFolders(di); } catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SelfExtractor objSelfExtractor = new SelfExtractor();
            string bjViewerpath = Path.Combine(Program.temp, "bjViewer");
            Directory.CreateDirectory(bjViewerpath);
            string zipfile = Path.Combine(bjViewerpath, "bjViewer.zip");
            using (Stream file = File.Create(zipfile)) file.Write(Resource1.bjViewer, 0, Resource1.bjViewer.Length);

            using (var zip = ZipArchive.OpenOnFile(zipfile))
            {
                foreach (ZipArchive.ZipFileInfo zipfileinfo in zip.Files)
                {
                    string path1 = zipfileinfo.Name;
                    if (!zipfileinfo.FolderFlag)
                    {
                        string sourcefilepath = Path.Combine(bjViewerpath, zipfileinfo.Name.Replace(@"/", @"\"));
                        string sourcepath = Path.GetDirectoryName(sourcefilepath);
                        DirectoryInfo di = new DirectoryInfo(sourcepath);
                        try { Program.CreateMissingFolders(di); } catch { }
                        if (Path.GetExtension(sourcefilepath) == @".cs")
                        {
                            objSelfExtractor.AddSourceName(sourcefilepath);
                        }
                        using (Stream file = File.Create(sourcefilepath))
                        {
                            var xx = zipfileinfo.GetStream();
                            for (int b = xx.ReadByte(); b != -1; b = xx.ReadByte())
                            {
                                file.WriteByte((byte)b);
                            }
                        }
                    }
                }
            }

            string bjArkiv = @"C:\Users\mha\Documents\mha_test_arkiv25";
            string bjArkivDatabase = bjArkiv + Program.BJARKIV;

            objSelfExtractor.AddFile(bjArkivDatabase);

            xmldocs db = xmldocs.Load(bjArkivDatabase);
            foreach(xmldoc doc in db)
            {
                string path = Path.Combine(bjArkiv, doc.kilde_sti);
                objSelfExtractor.AddFile(path, doc.id.ToString());
            } 
            objSelfExtractor.CompileArchive(@"C:\Users\mha\Documents\Visual Studio 2010\Projects\bjOutput\bjViewer.exe");
        }
    }
}
