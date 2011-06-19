using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chilkat;
using System.IO;

using System.ComponentModel;
using System.Data;
using Microsoft.Win32;
using Microsoft.VisualBasic;


namespace PbsMinitor
{

    public class clsSFTP
    {
        private SFtp m_sftp;
        private Tblsftp m_rec_sftp;

        public clsSFTP()
        {
#if (DEBUG)
            m_rec_sftp = (from s in Program.dbData3060.Tblsftp where s.Navn == "TestHD35" select s).First();
            //m_rec_sftp = (from s in Program.dbData3060.Tblsftp where s.Navn == "Test" select s).First();
            //m_rec_sftp = (from s in Program.dbData3060.Tblsftp where s.Navn == "Produktion" select s).First();
#else
            m_rec_sftp = (from s in Program.dbData3060.Tblsftp where s.Navn == "Produktion" select s).First();
#endif

            m_sftp = new SFtp();
            bool success = m_sftp.UnlockComponent("HAFSJOSSH_6pspJCMP1QnW");
            if (!success) throw new Exception(m_sftp.LastErrorText);
            m_sftp.ConnectTimeoutMs = 5000;
            m_sftp.IdleTimeoutMs = 15000;
            success = m_sftp.Connect(m_rec_sftp.Host, int.Parse(m_rec_sftp.Port));
            if (!success) throw new Exception(m_sftp.LastErrorText);

            Chilkat.SshKey key = new Chilkat.SshKey();

            string privKey = m_rec_sftp.Certificate;
            if (privKey == null) throw new Exception(m_sftp.LastErrorText);

            key.Password = m_rec_sftp.Pincode;
            success = key.FromOpenSshPrivateKey(privKey);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            success = m_sftp.AuthenticatePk(m_rec_sftp.User, key);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            //  After authenticating, the SFTP subsystem must be initialized:
            success = m_sftp.InitializeSftp();
            if (!success) throw new Exception(m_sftp.LastErrorText);
        }


        public void ReadDirectoryFraSFtp()
        {
            string homedir = m_sftp.RealPath(".", "");
            //  Open a directory on the server...
            string handle = m_sftp.OpenDir(m_rec_sftp.Outbound);
            if (handle == null) throw new Exception(m_sftp.LastErrorText);

            //  Download the directory listing:
            Chilkat.SFtpDir dirListing = null;
            dirListing = m_sftp.ReadDir(handle);
            if (dirListing == null) throw new Exception(m_sftp.LastErrorText);

            Program.memPbsnetdir = null; //opret ny memPbsnetdir

            //  Iterate over the files.
            int i;
            int n = dirListing.NumFilesAndDirs;
            if (n > 0)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    Chilkat.SFtpFile fileObj = null;
                    fileObj = dirListing.GetFileObject(i);
                    if (!fileObj.IsDirectory)
                    {
                        recPbsnetdir rec = new recPbsnetdir
                        {
                            Type = 8,
                            Path = dirListing.OriginalPath,
                            Filename = fileObj.Filename,
                            Size = (int)fileObj.Size32,
                            Atime = fileObj.LastAccessTime,
                            Mtime = fileObj.LastModifiedTime,
                            Gid = fileObj.Gid,
                            Uid = fileObj.Uid,
                            Perm = fileObj.Permissions.ToString()

                        };
                        Program.memPbsnetdir.Add(rec);
                    }
                }
            }

            //  Close the directory
            bool success = m_sftp.CloseHandle(handle);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            var leftqry_pbsnetdir =
                from h in Program.memPbsnetdir
                join d1 in Program.dbData3060.Tblpbsnetdir on new { h.Path, h.Filename } equals new { d1.Path, d1.Filename } into details
                from d1 in details.DefaultIfEmpty(new Tblpbsnetdir { Id = -1, Type = (int?)null, Path = null, Filename = null, Size = (int?)null, Atime = (DateTime?)null, Mtime = (DateTime?)null, Perm = null, Uid = (int?)null, Gid = (int?)null })
                where d1.Path == null && d1.Filename == null
                select h;

            int AntalFiler = leftqry_pbsnetdir.Count();
            string MailBody = "";
            if (leftqry_pbsnetdir.Count() > 0)
            {
                foreach (var rec_pbsnetdir in leftqry_pbsnetdir)
                {
                    MailBody += "\n" + rec_pbsnetdir.Filename + " - " + rec_pbsnetdir.Mtime.ToString() + " - " + rec_pbsnetdir.Size.ToString();

                    Tblpbsnetdir m_rec_pbsnetdir = new Tblpbsnetdir
                    {
                        Type = rec_pbsnetdir.Type,
                        Path = rec_pbsnetdir.Path,
                        Filename = rec_pbsnetdir.Filename,
                        Size = rec_pbsnetdir.Size,
                        Atime = rec_pbsnetdir.Atime,
                        Mtime = rec_pbsnetdir.Mtime,
                        Perm = rec_pbsnetdir.Perm,
                        Uid = rec_pbsnetdir.Uid,
                        Gid = rec_pbsnetdir.Gid
                    };
                    Program.dbData3060.Tblpbsnetdir.InsertOnSubmit(m_rec_pbsnetdir);
                }
                sendAttachedFile(MailBody);
            }
            Program.dbData3060.SubmitChanges();
            return;
        }

        public void sendAttachedFile(string MailBody)
        {
            Chilkat.MailMan mailman = new Chilkat.MailMan();
            bool success;
            success = mailman.UnlockComponent("HAFSJOMAILQ_9QYSMgP0oR1h");
            if (success != true) throw new Exception(mailman.LastErrorText);

            //  Use the GMail SMTP server
            mailman.SmtpHost = Program.Smtphost;
            mailman.SmtpPort = int.Parse(Program.Smtpport);
            mailman.SmtpSsl = bool.Parse(Program.Smtpssl);

            //  Set the SMTP login/password.
            mailman.SmtpUsername = Program.Smtpuser;
            mailman.SmtpPassword = Program.Smtppasswd;

            //  Create a new email object
            Chilkat.Email email = new Chilkat.Email();

#if (DEBUG)
            email.Subject = "Filer klar til afhentning Fra PBS Test";
            email.Body = MailBody;
#else
            email.Subject = "Filer klar til afhentning Fra PBS";
            email.Body = MailBody;
#endif
            email.AddTo(Program.MailToName, Program.MailToAddr);
            email.From = Program.MailFrom;
            email.ReplyTo = Program.MailReply;

            success = mailman.SendEmail(email);
            if (success != true) throw new Exception(email.LastErrorText);

        }

    }

}
