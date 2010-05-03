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
        private Tblpbsfile m_rec_pbsfile;
        private Tblsftp m_rec_sftp;

        public clsSFTP()
        {
            m_rec_sftp = (from s in Program.dbData3060.Tblsftp where s.Navn == "Produktion" select s).First();

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
        
        public int ReadFraSFtp(){
            //  Open a directory on the server...
            string handle = m_sftp.OpenDir(".");
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

                    recPbsnetdir rec = new recPbsnetdir
                    {
                        Type = 8,
                        Path = m_rec_sftp.Outbound,
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

            //  Close the directory
            bool success = m_sftp.CloseHandle(handle);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            var leftqry_pbsnetdir =
                from h in Program.memPbsnetdir
                //join d1 in Program.dbData3060.Tblpbsfiles on new { h.Path, h.Filename } equals new { d1.Path, d1.Filename } into details
                //from d1 in details.DefaultIfEmpty(new Tblpbsfiles { Id = -1, Type = (int?)null, Path = null, Filename = null, Size = (int?)null, Atime = (DateTime?)null, Mtime = (DateTime?)null, Perm = null, Uid = (int?)null, Gid = (int?)null })
                //where d1.Path == null && d1.Filename == null
                select h;

            int AntalFiler = leftqry_pbsnetdir.Count();
            if (leftqry_pbsnetdir.Count() > 0)
            {
                foreach (var rec_pbsnetdir in leftqry_pbsnetdir)
                {
                    Tblpbsfiles m_rec_pbsfiles = new Tblpbsfiles
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
                    Program.dbData3060.Tblpbsfiles.InsertOnSubmit(m_rec_pbsfiles);

                    //***********************************************************************
                    //  Open a file on the server:
                    string filehandle = m_sftp.OpenFile(rec_pbsnetdir.Filename, "readOnly", "openExisting");
                    if (filehandle == null) throw new Exception(m_sftp.LastErrorText);

                    int numBytes = (int)rec_pbsnetdir.Size;
                    if (numBytes < 0) throw new Exception(m_sftp.LastErrorText);

                    byte[] b_data = null;
                    b_data = m_sftp.ReadFileBytes(handle, numBytes);
                    if (b_data == null) throw new Exception(m_sftp.LastErrorText);
                    sendAttachedFile(rec_pbsnetdir.Filename, b_data);
                    char[] c_data = System.Text.Encoding.GetEncoding("windows-1252").GetString(b_data).ToCharArray();
                    string filecontens = new string(c_data);
                    
                    string[] lines = filecontens.Split('\n');
                    string ln = null;
                    int seqnr = 0;
                    for (int idx = 0; seqnr < lines.Count(); idx++)
                    {
                        ln = lines[idx];    
                        if (((seqnr == 0) && !(ln.Substring(0, 6) == "PBCNET")) || (seqnr > 0)) { seqnr++; }
                            m_rec_pbsfile = new Tblpbsfile
                            {
                                Seqnr = seqnr,
                                Data = ln
                            };
                            m_rec_pbsfiles.Tblpbsfile.Add(m_rec_pbsfile);
                    }

                    m_rec_pbsfiles.Transmittime = DateTime.Now;
                    Program.dbData3060.SubmitChanges();
                    
                    //  Close the file.
                    success = m_sftp.CloseHandle(filehandle);
                    if (success != true) throw new Exception(m_sftp.LastErrorText);
                    //***********************************************************************************
                }
            }
            Program.dbData3060.SubmitChanges();
            return AntalFiler;
        }

        private void sendAttachedFile(string filename, byte[] data)
        {
            string local_filename = filename.Replace('.', '_') + ".txt";
            Chilkat.MailMan mailman = new Chilkat.MailMan();
            bool success;
            success = mailman.UnlockComponent("HAFSJOMAILQ_9QYSMgP0oR1h");
            if (success != true) throw new Exception(mailman.LastErrorText);

            //  Use the GMail SMTP server
            mailman.SmtpHost = "smtp.gmail.com";
            mailman.SmtpPort = 465;
            mailman.SmtpSsl = true;

            //  Set the SMTP login/password.
            mailman.SmtpUsername = "kasserer.puls3060@gmail.com";
            mailman.SmtpPassword = "n4vWYkAKsfRFcuLW";

            //  Create a new email object
            Chilkat.Email email = new Chilkat.Email();

            email.Subject = local_filename + " fra PBS";
            email.Body = local_filename + " fra PBS"; ;
            email.From = "kasserer <kasserer.puls3060@gmail.com>";
            email.AddTo("kasserer", "kasserer.puls3060@gmail.com");
            email.AddDataAttachment2(local_filename, data, "text/plain");
            email.UnzipAttachments();

            success = mailman.SendEmail(email);
            if (success != true) throw new Exception(email.LastErrorText);

        }
    }

}
