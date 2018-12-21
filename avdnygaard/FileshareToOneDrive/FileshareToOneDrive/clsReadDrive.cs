using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileshareToOneDrive
{
    public class clsReadDrive
    {
        private string[] filter =
        {
            @"#recycle"
        };

        public void load(string pServerPathType, clsServer recServer)
        {
            dbSharePointEntities db = new dbSharePointEntities();
            db.tblPath.Load();


            string[] files;
            if (recServer.topFoldersExclude == null)
            {
                if (pServerPathType == "File")
                {
                    files = System.IO.Directory.GetFiles(recServer.serverDrev, "*", System.IO.SearchOption.AllDirectories);
                }
                else // == "Folder"
                {
                    files = System.IO.Directory.GetDirectories(recServer.serverDrev, "*", System.IO.SearchOption.AllDirectories);
                }
            }
            else
            {
                string[] tffiles;
                List<string> filesList = new List<string>();
                var topFoldersExclude = recServer.topFoldersExclude;

                var topFolders = System.IO.Directory.GetDirectories(recServer.serverDrev, "*", System.IO.SearchOption.TopDirectoryOnly);
                var topFiles = System.IO.Directory.GetFiles(recServer.serverDrev, "*", System.IO.SearchOption.TopDirectoryOnly);

                foreach (var tf in topFolders)
                {
                    var bfilter = false;
                    foreach (var f in topFoldersExclude)
                    {
                        if (tf.ToUpperInvariant().Contains(f.ToUpperInvariant()))
                        {
                            bfilter = true;
                            break;
                        }
                    }
                    if (bfilter) continue;

                    if (pServerPathType == "File")
                    {
                        tffiles = System.IO.Directory.GetFiles(tf, "*", System.IO.SearchOption.AllDirectories);
                        filesList.AddRange(topFiles);
                    }
                    else // == "Folder"
                    {
                        tffiles = System.IO.Directory.GetDirectories(tf, "*", System.IO.SearchOption.AllDirectories);
                        filesList.Add(tf);
                   }
                    filesList.AddRange(tffiles);
                }
                files = filesList.ToArray();
            }

            var files1 = files.ToList();
            files1.Sort();
            int i = 0;
            foreach (var file in files1)
            {
                var bfilter = false;
                foreach (var f in filter)
                {
                    if (file.ToUpperInvariant().Contains(f.ToUpperInvariant()))
                    {
                        bfilter = true;
                        break;
                    }
                }
                if (bfilter) continue;

                Console.WriteLine((++i).ToString());
                var qryfile = db.tblPath.Local.Where(f => f.serverPath == file);
                if (qryfile.Count() == 0)
                {
                    tblPath rec = new tblPath()
                    {
                        serverDrev = recServer.serverDrev,
                        localDrevLetter = recServer.localDrevLetter,
                        serverPath = file,
                        spSite = recServer.spSite,
                        spSiteType = recServer.spSiteType,
                        build = true,
                        serverPathType = pServerPathType
                    };
                    try
                    {
                        db.tblPath.Local.Add(rec);
                        db.SaveChanges();
                        Console.WriteLine(file);
                    }
                    catch (Exception e)
                    {
                        int x = 1;
                        //duplicate
                    }
                }

            }
        }




    }

    public class clsServer
    {
        public string serverDrev { get; set; }
        public string localDrevLetter { get; set; }
        public string spSite { get; set; }
        public string spSiteType { get; set; }
        public List<string> topFoldersExclude { get; set; }

    }
}
