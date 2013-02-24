using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace bjArkiv
{
    class clsExportArkiv
    {
        public string temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        public clsExportArkiv() 
        {
            DirectoryInfo di = new DirectoryInfo(temp);
            try { Program.CreateMissingFolders(di); }
            catch { }
        }

        public void ExsportArkiv(string ParamArkivFolder, string ParamOutputPath)
        {
            string OutFolder = Path.GetDirectoryName(ParamOutputPath);
            string OutFileName = Path.GetFileNameWithoutExtension(ParamOutputPath);
            
            SelfExtractor objSelfExtractor = new SelfExtractor();
            objSelfExtractor.temp = temp;
            string bjViewerpath = Path.Combine(temp, "bjViewer");
            Directory.CreateDirectory(bjViewerpath);
            string zipfile = Path.Combine(bjViewerpath, "bjViewer.zip");
            using (Stream file = File.Create(zipfile)) file.Write(bjExtract.Resource1.bjViewer, 0, bjExtract.Resource1.bjViewer.Length);

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
                        try { Program.CreateMissingFolders(di); }
                        catch { }
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


            string bjArkivDatabase = ParamArkivFolder + Program.BJARKIV;

            objSelfExtractor.AddFile(bjArkivDatabase);

            xmldocs db = xmldocs.Load(bjArkivDatabase);
            foreach (xmldoc doc in db)
            {
                string path = Path.Combine(ParamArkivFolder, doc.kilde_sti);
                objSelfExtractor.AddFile(path, doc.id.ToString());
            }
            string program = Path.Combine(temp, OutFileName + @".exe");
            objSelfExtractor.CompileArchive(program);
            objSelfExtractor.Dispose();
            objSelfExtractor = null;


            string newZip = Path.Combine(OutFolder, OutFileName + @".zip");
            using (var zip = ZipArchive.OpenOnFile(newZip, FileMode.Create, FileAccess.ReadWrite, FileShare.None, false))
            {
                byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
                int bytesRead;
                var fs = zip.AddFile(OutFileName + @".exe", ZipArchive.CompressionMethodEnum.Stored);
                var outstream = fs.GetStream(FileMode.Open, FileAccess.ReadWrite);

                using (FileStream instream = new FileStream(program, FileMode.Open))
                {
                    while ((bytesRead = instream.Read(buffer, 0, buffer.Length)) > 0) { outstream.Write(buffer, 0, bytesRead); }
                }
                outstream.Dispose();
            }

            try
            {
                DirectoryInfo di = new DirectoryInfo(temp);
                di.Delete(true);
            }
            catch { }

            string messageBoxText = "Arkiv: " + ParamArkivFolder + "\n\n Er nu eksporteret til\n\n" + newZip;
            string caption = "bjArkiv";
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
