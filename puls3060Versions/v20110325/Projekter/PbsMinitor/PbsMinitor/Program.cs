using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PbsMinitor
{
    public class clsSysinfo
    {
        public string vkey = "";
        public string val = "";
    }

    class Program
    {
        private static string m_Smtphost;
        private static string m_Smtpport;
        private static string m_Smtpssl;
        private static string m_Smtpuser;
        private static string m_Smtppasswd;
        private static string m_MailToName;
        private static string m_MailToAddr;
        private static string m_MailFrom;
        private static string m_MailReply;

        private static MemPbsnetdir m_memPbsnetdir;
        private static string m_dbData3060File;

        public static string Smtphost
        {
            get
            {
                if (m_Smtphost == null) m_Smtphost = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'SMTPHOST';")).First().val;
                return m_Smtphost;
            }
            set
            {
                m_Smtphost = value;
            }
        }
        public static string Smtpport
        {
            get
            {
                if (m_Smtpport == null) m_Smtpport = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'SMTPPORT';")).First().val;
                return m_Smtpport;
            }
            set
            {
                m_Smtpport = value;
            }
        }
        public static string Smtpssl
        {
            get
            {
                if (m_Smtpssl == null) m_Smtpssl = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'Smtpssl';")).First().val;
                return m_Smtpssl;
            }
            set
            {
                m_Smtpssl = value;
            }
        }
        public static string Smtpuser
        {
            get
            {
                if (m_Smtpuser == null) m_Smtpuser = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'Smtpuser';")).First().val;
                return m_Smtpuser;
            }
            set
            {
                m_Smtpuser = value;
            }
        }
        public static string Smtppasswd
        {
            get
            {
                if (m_Smtppasswd == null) m_Smtppasswd = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'Smtppasswd';")).First().val;
                return m_Smtppasswd;
            }
            set
            {
                m_Smtppasswd = value;
            }
        }
        public static string MailToName
        {
            get
            {
                if (m_MailToName == null) m_MailToName = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailToName';")).First().val;
                return m_MailToName;
            }
            set
            {
                m_MailToName = value;
            }
        }
        public static string MailToAddr
        {
            get
            {
                if (m_MailToAddr == null) m_MailToAddr = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailToAddr';")).First().val;
                return m_MailToAddr;
            }
            set
            {
                m_MailToAddr = value;
            }
        }
        public static string MailFrom
        {
            get
            {
                if (m_MailFrom == null) m_MailFrom = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailFrom';")).First().val;
                return m_MailFrom;
            }
            set
            {
                m_MailFrom = value;
            }
        }
        public static string MailReply
        {
            get
            {
                if (m_MailReply == null) m_MailReply = (Program.dbData3060.ExecuteQuery<clsSysinfo>("SELECT * FROM [tblSysinfo] WHERE [vkey] = N'MailReply';")).First().val;
                return m_MailReply;
            }
            set
            {
                m_MailReply = value;
            }
        }

        private static DbData3060 m_dbData3060;
        public static DbData3060 dbData3060
        {
            get
            {
                if (m_dbData3060 == null)
                {
                    m_dbData3060 = new DbData3060(m_dbData3060File);
                }
                return m_dbData3060;
            }
            set
            {
                m_dbData3060 = value;
            }
        }

        public static MemPbsnetdir memPbsnetdir
        {
            get
            {
                if (m_memPbsnetdir == null) m_memPbsnetdir = new MemPbsnetdir();
                return m_memPbsnetdir;
            }
            set
            {
                m_memPbsnetdir = value;
            }
        }
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                m_dbData3060File = @"C:\Documents and Settings\mha\Dokumenter\SummaSummarum\dbData3060.sdf";
            }
            else 
            {
                m_dbData3060File = args[0];
            }
            clsSFTP objSFTP = new clsSFTP();
            objSFTP.ReadDirectoryFraSFtp();
        }
    }
}
