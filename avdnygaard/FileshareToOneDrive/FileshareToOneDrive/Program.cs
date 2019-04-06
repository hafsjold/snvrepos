using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileshareToOneDrive
{
    class Program
    {
        static public List<clsServer> m_servers = new List<clsServer>()
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
          
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\DRIFT\",
                 localDrevLetter = "V",
                 spSite = "/drift",
                 spSiteType = "SharePoint",
                 topFoldersExclude = new List<string>()
                 {
                     @"ActiveBackupForOffice365"
                 }
            },             
               
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

        static void Main(string[] args)
        {

            clsServer recServer = null;

            if (true)
            {
                clsReadDrive objReadDrive = new clsReadDrive();
  
                //recServer = m_servers.Where(s => s.spSite == "/hn").First();
                //objReadDrive.load("Folder", recServer);
                //objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/hj").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/cm").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/mr").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/holding").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/regnskab").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);
 
                recServer = m_servers.Where(s => s.spSite == "/billeder").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);
  
                recServer = m_servers.Where(s => s.spSite == "/drift").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/notater").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/intranet").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/skabeloner").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

                recServer = m_servers.Where(s => s.spSite == "/klient").First();
                objReadDrive.load("Folder", recServer);
                objReadDrive.load("File", recServer);

            }

            if (true)
            {
                clsContent.init();
                clsProd03 objProd03 = new clsProd03();
             
                //recServer = m_servers.Where(s => s.spSite == "/hn").First();
                //objProd03.CreateFoldersAsync(recServer).Wait();
                //objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/hj").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/cm").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/mr").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/holding").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/regnskab").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();
                
                recServer = m_servers.Where(s => s.spSite == "/billeder").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();
                
                recServer = m_servers.Where(s => s.spSite == "/drift").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/notater").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/intranet").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/skabeloner").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/klient").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();
                
            }
        }
    }
}
