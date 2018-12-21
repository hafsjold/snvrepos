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
        private List<clsServer> m_servers;


        private string[] filter =
        {
            @"#recycle"
        };

        public clsReadDrive()
        {
            m_servers = Program.m_servers;
        }

        public void load(string pServerPathType)
        {
            dbSharePointEntities db = new dbSharePointEntities();
            db.tblPath.Load();

            foreach (var s in m_servers)
            {
                string[] files;
                if (pServerPathType == "File")
                {
                    files = System.IO.Directory.GetFiles(s.serverDrev, "*", System.IO.SearchOption.AllDirectories);
                }
                else // == "Folder"
                {
                    files = System.IO.Directory.GetDirectories(s.serverDrev, "*", System.IO.SearchOption.AllDirectories);
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
                            serverDrev = s.serverDrev,
                            localDrevLetter = s.localDrevLetter,
                            serverPath = file,
                            spSite = s.spSite,
                            spSiteType = s.spSiteType,
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

    }

    public class clsServer
    {
        public string serverDrev { get; set; }
        public string localDrevLetter { get; set; }
        public string spSite { get; set; }
        public string spSiteType { get; set; }
    }
}
