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
            Tblsftp rec_sftp = (from s in Program.dbData3060.Tblsftp where s.Navn == "Test" select s).First();
            
            SFtp m_sftp = new SFtp();
            bool success = m_sftp.UnlockComponent("BUUSJESSH_qKCpze49oVnG");
            if (!success) throw new Exception(m_sftp.LastErrorText);
            m_sftp.ConnectTimeoutMs = 5000;
            m_sftp.IdleTimeoutMs = 15000;
            success = m_sftp.Connect(rec_sftp.Host, int.Parse(rec_sftp.Port));
            if (!success) throw new Exception(m_sftp.LastErrorText);

            Chilkat.SshKey key = new Chilkat.SshKey();

            string privKey = rec_sftp.Certificate;
            if (privKey == null) throw new Exception(m_sftp.LastErrorText);

            key.Password = rec_sftp.Pincode;
            success = key.FromOpenSshPrivateKey(privKey);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            success = m_sftp.AuthenticatePk(rec_sftp.User, key);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            //  After authenticating, the SFTP subsystem must be initialized:
            success = m_sftp.InitializeSftp();
            if (!success) throw new Exception(m_sftp.LastErrorText);

        }
    }

}
