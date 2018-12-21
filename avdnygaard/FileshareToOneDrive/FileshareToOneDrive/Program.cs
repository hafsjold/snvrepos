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
                 serverDrev = @"\\SYNOLOGY_N\CM\",
                 localDrevLetter = "P",
                 spSite = "cm@adv-nygaard.dk",
                 spSiteType = "OneDrive",
            },

            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\MR\",
                 localDrevLetter = "Q",
                 spSite = "mr@adv-nygaard.dk",
                 spSiteType = "OneDrive",
            },

            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\HJ\",
                 localDrevLetter = "R",
                 spSite = "hj@adv-nygaard.dk",
                 spSiteType = "OneDrive",
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

            /*
            new clsServer()
            {
                 serverDrev = @"\\SYNOLOGY_N\BILLEDER\",
                 localDrevLetter = "U",
                 spSite = "/billeder",
                 spSiteType = "SharePoint",
            }, 
            */

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

        static void Main(string[] args)
        {

            clsServer recServer = null;

            if (false)
            {
                clsReadDrive objReadDrive = new clsReadDrive();
                objReadDrive.load("Folder");
                objReadDrive.load("File");
            }

            if (true)
            {
                clsContent.init();
                clsProd03 objProd03 = new clsProd03();

                recServer = m_servers.Where(s => s.spSite == "/notater").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();

                recServer = m_servers.Where(s => s.spSite == "/klient").First();
                objProd03.CreateFoldersAsync(recServer).Wait();
                objProd03.CreateFilesAsync(recServer).Wait();
            }
        }
    }
}
