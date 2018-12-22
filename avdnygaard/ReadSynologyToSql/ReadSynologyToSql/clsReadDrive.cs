using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadSynologyToSql
{
    public class clsReadDrive
    {
        private List<clsServer> m_servers = new List<clsServer>()
        {
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\HN\",
                 localDrevLetter = "O",
                 spSite = "/hn",
                 spSiteType = "SharePoint",
            },

            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\CM\",
                 localDrevLetter = "P",
                 spSite = "/cm",
                 spSiteType = "SharePoint",
            },
            
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\MR\",
                 localDrevLetter = "Q",
                 spSite = "/mr",
                 spSiteType = "SharePoint",
            },
            
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\HJ\",
                 localDrevLetter = "R",
                 spSite = "/hj",
                 spSiteType = "SharePoint",
            },
            
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\NYGAARD HOLDING\",
                 localDrevLetter = "S",
                 spSite = "/holding",
                 spSiteType = "SharePoint",
            },
            
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\REGNSKAB\",
                 localDrevLetter = "T",
                 spSite = "/regnskab",
                 spSiteType = "SharePoint",
            },

            
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\BILLEDER\",
                 localDrevLetter = "U",
                 spSite = "/billeder",
                 spSiteType = "SharePoint",
            }, 
            

            /*
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\DRIFT\",
                 localDrevLetter = "V",
                 spSite = "/drift",
                 spSiteType = "SharePoint",
            },             
            */
            
             new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\NOTATER\",
                 localDrevLetter = "W",
                 spSite = "/notater",
                 spSiteType = "SharePoint",
            },              
            
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\INTRANET\",
                 localDrevLetter = "X",
                 spSite = "/intranet",
                 spSiteType = "SharePoint",
            },              
             
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\DOKUMENTSKABELONER\",
                 localDrevLetter = "Y",
                 spSite = "/skabeloner",
                 spSiteType = "SharePoint",
            },

            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\KLIENTRELATERET\",
                 localDrevLetter = "Z",
                 spSite = "/klient",
                 spSiteType = "SharePoint",
            },          
        };

        private string[] filter =
        {
            @"#recycle"
        };

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

        public void DviveSize()
        {
            var s = m_servers.Where(x => x.spSite == "/billeder").First();
            var files = System.IO.Directory.GetFiles(s.localDrevLetter + @":\", "*", System.IO.SearchOption.AllDirectories);
            Int64 driveSize = 0;
            var i = 0;
            var j = 0;
            var imax = files.Length;
            foreach (var f in files)
            {
                i++;
                j++;

                if (j > 999) 
                {
                    Console.WriteLine(string.Format("{0}/{1}  {2} GB", i ,imax, driveSize / (1024 * 1024 * 1024)));
                    j = 0;
                }
                driveSize += new FileInfo(f).Length;
            }
            var driveSizeGB = driveSize / (1024 * 1024 * 1024);
            Console.WriteLine(string.Format("{0} files {1} GB", imax, driveSizeGB));
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
