using MailKit;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nsPbs3060
{
    public class clsStatus
    {
        private static clsStatusData m_statusdata;

        public static clsStatusData GetStatus(string dbConnectionString)
        {
            m_statusdata = new clsStatusData();
            getUniContaStatus();
            getGigaHostImap();
            getPuls3060Medlem(dbConnectionString);
            getPuls3060_dk();
            getSFTP(dbConnectionString);
            return m_statusdata;
        }

        private static void getUniContaStatus()
        {
            m_statusdata.bUniConta_Online = false;
            try
            {
                var testCurrentCompany = UCInitializer.CurrentCompany;
                m_statusdata.bUniConta_Online = true;
                Program.Log("Medlem3060Service getUniContaStatus() OK");
            }
            catch
            {
                m_statusdata.bUniConta_Online = false;
                Program.Log("Medlem3060Service getUniContaStatus() ERROR");
            }
        }

        private static void getGigaHostImap()
        {
            m_statusdata.bGigaHostImap_Online = false;
            try
            {
                using (var imap_client = new ImapClient())
                {
                    imap_client.Connect("imap.gigahost.dk", 993, true);
                    imap_client.AuthenticationMechanisms.Remove("XOAUTH");
                    imap_client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);
                    var Puls3060Bilag = imap_client.GetFolder("_Puls3060Bilag");
                    Puls3060Bilag.Open(FolderAccess.ReadWrite);
                    m_statusdata.bGigaHostImap_Online = true;
                    Program.Log("Medlem3060Service getGigaHostImap() OK");
                }
            }
            catch 
            {
                m_statusdata.bGigaHostImap_Online = false;
                Program.Log("Medlem3060Service getGigaHostImap() ERROR");
            }
        }

        private static void getPuls3060Medlem(string dbConnectionString)
        {
            m_statusdata.bdbPuls3060Medlem_Online = false;
            try
            {
                dbData3060DataContext m_dbData3060 = new dbData3060DataContext(dbConnectionString);
                var qry = from b in m_dbData3060.tblbankkontos select b;
                int antal = qry.Count();
                m_statusdata.bdbPuls3060Medlem_Online = true;
                Program.Log("Medlem3060Service getPuls3060Medlem() OK");
            }
            catch
            {
                m_statusdata.bdbPuls3060Medlem_Online = false;
                Program.Log("Medlem3060Service getPuls3060Medlem() ERROR");
            }
        }

        private static void getPuls3060_dk()
        {
            m_statusdata.bpuls3060_dk_Online = false;
            try
            {
                puls3060_nyEntities cjdb = new puls3060_nyEntities(true);
                var qry = from u in cjdb.ecpwt_users select u;
                int antal = qry.Count();
                m_statusdata.bpuls3060_dk_Online = true;
                Program.Log("Medlem3060Service getPuls3060_dk() OK");
            }
            catch
            {
                m_statusdata.bpuls3060_dk_Online = false;
                Program.Log("Medlem3060Service getPuls3060_dk() ERROR");
            }
        }

        private static void getSFTP(string dbConnectionString)
        {
            m_statusdata.bSFTP_Online = false;
            try
            {
                dbData3060DataContext m_dbData3060 = new dbData3060DataContext(dbConnectionString);
                clsSFTP objSFTP = new clsSFTP(m_dbData3060);
                int antal = objSFTP.ReadDirFraSFtp();
                objSFTP.DisconnectSFtp();
                objSFTP = null;
                m_statusdata.bSFTP_Online = true;
                Program.Log("Medlem3060Service getSFTP() OK");
            }
            catch 
            {
                m_statusdata.bSFTP_Online = false;
                Program.Log("Medlem3060Service getSFTP() ERROR");
            }
        }
    }
}
