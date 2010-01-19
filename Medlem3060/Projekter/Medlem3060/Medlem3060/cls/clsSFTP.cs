using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chilkat;

namespace nsPuls3060
{

    public class clsSFTP
    {
        private SFtp m_sftp;
        
        public clsSFTP()
        {
            SFtp m_sftp = new SFtp();
            bool success = m_sftp.UnlockComponent("BUUSJESSH_qKCpze49oVnG");
            if (!success) throw new Exception(m_sftp.LastErrorText);
            //  Set some timeouts, in milliseconds:
            m_sftp.ConnectTimeoutMs = 5000;
            m_sftp.IdleTimeoutMs = 15000;
            int port;
            string hostname = "194.239.133.112"; //Test
            //string hostname = "194.239.133.111"; //Produktion
            port = 10022;
            success = m_sftp.Connect(hostname, port);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            success = m_sftp.AuthenticatePw("myLogin", "myPassword");
            if (!success) throw new Exception(m_sftp.LastErrorText);
        }
    }

}
