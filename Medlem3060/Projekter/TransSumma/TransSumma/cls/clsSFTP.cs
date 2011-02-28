using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chilkat;
using System.IO;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Xml.Linq;


namespace nsPuls3060
{

    public class clsSFTP
    {
        private SFtp m_sftp;

        private string m_SftpId;
        private string m_SftpNavn;
        private string m_Host;
        private string m_Port;
        private string m_Certificate;
        private string m_Pincode;
        private string m_User;
        private string m_Outbound;
        private string m_Inbound;

        private string m_SendqueueId;
        private string m_PbsfileId;
        private string m_TilPBSFilename;
        private DateTime m_Transmisionsdato;
        private string m_SendData;

        public clsSFTP()
        {
            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpGet2(clsRest.urlBaseType.data, "sftp/" + Program.sftpName);
            XDocument xdoc = XDocument.Parse(strxmldata);

            string Status = xdoc.Descendants("Status").First().Value;
            if (Status != "True")
            {
                throw new Exception("Getting sftp-data for " + Program.sftpName + " failed.");
            }

            m_SftpId = xdoc.Descendants("Id").First().Value;
            m_SftpNavn = xdoc.Descendants("Navn").First().Value;
            m_Host = xdoc.Descendants("Host").First().Value;
            m_Port = xdoc.Descendants("Port").First().Value;
            m_Certificate = xdoc.Descendants("Certificate").First().Value;
            m_Pincode = xdoc.Descendants("Pincode").First().Value;
            m_User = xdoc.Descendants("User").First().Value;
            m_Outbound = xdoc.Descendants("Outbound").First().Value;
            m_Inbound = xdoc.Descendants("Inbound").First().Value;


            m_sftp = new SFtp();
            bool success = m_sftp.UnlockComponent("HAFSJOSSH_6pspJCMP1QnW");
            if (!success) throw new Exception(m_sftp.LastErrorText);
            m_sftp.ConnectTimeoutMs = 60000;
            m_sftp.IdleTimeoutMs = 55000;
            success = m_sftp.Connect(m_Host, int.Parse(m_Port));
            if (!success) throw new Exception(m_sftp.LastErrorText);
            Chilkat.SshKey key = new Chilkat.SshKey();

            string privKey = m_Certificate;
            if (privKey == null) throw new Exception(m_sftp.LastErrorText);

            key.Password = m_Pincode;
            success = key.FromOpenSshPrivateKey(privKey);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            success = m_sftp.AuthenticatePk(m_User, key);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            //  After authenticating, the SFTP subsystem must be initialized:
            success = m_sftp.InitializeSftp();
            if (!success) throw new Exception(m_sftp.LastErrorText);
        }

        public void DisconnectSFtp()
        {
            m_sftp.Disconnect();
        }

        public bool WriteTilSFtp(XDocument xdoc)
        {
            Guid id1 = clsSQLite.insertStoreXML(Program.sftpName, false, Program.AppEngName, xdoc.ToString(), "");

            m_SendqueueId = xdoc.Descendants("Sendqueue").Descendants("Id").First().Value;
            m_PbsfileId = xdoc.Descendants("Pbsfile").Descendants("Id").First().Value;
            m_TilPBSFilename = xdoc.Descendants("Pbsfile").Descendants("TilPBSFilename").First().Value;
            m_Transmisionsdato = DateTime.Parse(xdoc.Descendants("Pbsfile").Descendants("Transmisionsdato").First().Value);
            m_SendData = xdoc.Descendants("Pbsfile").Descendants("SendData").First().Value;

            bool success;
            string TilPBSFilename = m_TilPBSFilename;
            string TilPBSFile = m_SendData;

            char[] c_TilPBSFile = TilPBSFile.ToCharArray();
            byte[] b_TilPBSFile = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_TilPBSFile);
            int FilesSize = b_TilPBSFile.Length;

            sendAttachedFile(TilPBSFilename, b_TilPBSFile, true);

            string fullpath = m_Inbound + "/" + TilPBSFilename;
            string handle = m_sftp.OpenFile(fullpath, "writeOnly", "createTruncate");
            if (handle == null) throw new Exception(m_sftp.LastErrorText);

            success = m_sftp.WriteFileBytes(handle, b_TilPBSFile);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            success = m_sftp.CloseHandle(handle);
            if (success != true) throw new Exception(m_sftp.LastErrorText);

            clsSQLite.updateStoreXML(id1, true);

            XElement xmlPbsfilesUpdate = new XElement("Pbsfiles");
            xmlPbsfilesUpdate.Add(new XElement("SendqueueId", m_SendqueueId));
            xmlPbsfilesUpdate.Add(new XElement("Id", m_PbsfileId));
            xmlPbsfilesUpdate.Add(new XElement("Type", 8));
            xmlPbsfilesUpdate.Add(new XElement("Path", m_Inbound));
            xmlPbsfilesUpdate.Add(new XElement("Filename", TilPBSFilename));
            xmlPbsfilesUpdate.Add(new XElement("Size", FilesSize));
            xmlPbsfilesUpdate.Add(new XElement("Atime", DateTime.Now));
            xmlPbsfilesUpdate.Add(new XElement("Mtime", DateTime.Now));
            xmlPbsfilesUpdate.Add(new XElement("Transmittime", m_Transmisionsdato));
            string strxmlPbsfilesUpdate = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xmlPbsfilesUpdate.ToString(); ;

            Guid id2 = clsSQLite.insertStoreXML(Program.AppEngName, false, Program.sftpName, strxmlPbsfilesUpdate, "");

            clsRest objRest = new clsRest();
            string strxmldata = objRest.HttpPost2(clsRest.urlBaseType.data, "tilpbs", strxmlPbsfilesUpdate);
            XDocument xmldata = XDocument.Parse(strxmldata);
            string Status = xmldata.Descendants("Status").First().Value;
            if (Status == "True")
            {
                clsSQLite.updateStoreXML(id2, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ReadFraSFtp()
        {
            string homedir = m_sftp.RealPath(".", "");
            //  Open a directory on the server...
            string handle = m_sftp.OpenDir(m_Outbound);
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
                        DateTime testLastAccessTime = fileObj.LastAccessTime;
                        recPbsnetdir rec = new recPbsnetdir
                        {
                            Type = 8,
                            Path = dirListing.OriginalPath,
                            Filename = fileObj.Filename,
                            Size = (int)fileObj.Size32,
                            Atime = Unspecified2Utc(fileObj.LastAccessTime),
                            Mtime = Unspecified2Utc(fileObj.LastModifiedTime),
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

            var leftqry_pbsnetdir = from h in Program.memPbsnetdir select h;

            int AntalFiler = leftqry_pbsnetdir.Count();
            if (leftqry_pbsnetdir.Count() > 0)
            {
                foreach (var rec_pbsnetdir in leftqry_pbsnetdir)
                {
                    //  Open a file on the server:
                    string fullpath = rec_pbsnetdir.Path + "/" + rec_pbsnetdir.Filename;
                    string filehandle = m_sftp.OpenFile(fullpath, "readOnly", "openExisting");
                    if (filehandle == null) throw new Exception(m_sftp.LastErrorText);

                    int numBytes = (int)rec_pbsnetdir.Size;
                    if (numBytes < 0) throw new Exception(m_sftp.LastErrorText);

                    byte[] b_data = null;
                    b_data = m_sftp.ReadFileBytes(handle, numBytes);
                    if (b_data == null) throw new Exception(m_sftp.LastErrorText);
                    sendAttachedFile(rec_pbsnetdir.Filename, b_data, false);
                    char[] c_data = System.Text.Encoding.GetEncoding("windows-1252").GetString(b_data).ToCharArray();
                    string filecontens = new string(c_data);

                    string filecontens2 = filecontens.TrimEnd('\n');
                    string filecontens3 = filecontens2.TrimEnd('\r');
                    string filecontens4 = filecontens3.TrimEnd('\n');

                    XElement xmlPbsfilesAdd = new XElement("Pbsfiles");
                    xmlPbsfilesAdd.Add(new XElement("Type", rec_pbsnetdir.Type));
                    xmlPbsfilesAdd.Add(new XElement("Path", rec_pbsnetdir.Path));
                    xmlPbsfilesAdd.Add(new XElement("Filename", rec_pbsnetdir.Filename));
                    xmlPbsfilesAdd.Add(new XElement("Size", rec_pbsnetdir.Size));
                    xmlPbsfilesAdd.Add(new XElement("Atime", rec_pbsnetdir.Atime));
                    xmlPbsfilesAdd.Add(new XElement("Mtime", rec_pbsnetdir.Mtime));
                    xmlPbsfilesAdd.Add(new XElement("Transmittime", DateTime.Now));
                    xmlPbsfilesAdd.Add(new XElement("Data", filecontens4));
                    string strxmlPbsfilesAdd = @"<?xml version=""1.0"" encoding=""utf-8"" ?> " + xmlPbsfilesAdd.ToString();

                    Guid id1 = clsSQLite.insertStoreXML(Program.AppEngName, false, Program.sftpName, strxmlPbsfilesAdd, "");

                    clsRest objRest = new clsRest();
                    string strxmldata = objRest.HttpPost2(clsRest.urlBaseType.data, "frapbs", strxmlPbsfilesAdd);
                    XDocument xmldata = XDocument.Parse(strxmldata);
                    string Status = xmldata.Descendants("Status").First().Value;
                    if (Status == "True")
                    {
                        clsSQLite.updateStoreXML(id1, true);
                    }

                    //  Close the file.
                    success = m_sftp.CloseHandle(filehandle);
                    if (success != true) throw new Exception(m_sftp.LastErrorText);
                }
            }
            return AntalFiler;
        }

        public DateTime Unspecified2Utc(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Unspecified)
            {
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, DateTimeKind.Utc);
            }
            else
                return dt;
        }

        public DateTime Unspecified2Local(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Unspecified)
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, DateTimeKind.Local);
            else
                return dt;
        }

        public void ReadDirFraSFtp()
        {
            string homedir = m_sftp.RealPath(".", "");
            //  Open a directory on the server...
            string handle = m_sftp.OpenDir(m_Outbound);
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
                            Atime = Unspecified2Utc(fileObj.LastAccessTime),
                            Mtime = Unspecified2Utc(fileObj.LastModifiedTime),
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
        }

        public void sendAttachedFile(string filename, byte[] data, bool bTilPBS)
        {
            string local_filename = filename.Replace('.', '_') + ".txt";
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

            if (bTilPBS)
            {
#if (DEBUG)
                email.Subject = "Test Til PBS: " + local_filename;
                email.Body = "Test Til PBS: " + local_filename;
#else
                email.Subject = "Til PBS: " + local_filename;
                email.Body = "Til PBS: " + local_filename;
#endif
            }
            else
            {
#if (DEBUG)
                email.Subject = "Test Fra PBS: " + local_filename;
                email.Body = "Test Fra PBS: " + local_filename;
#else
                email.Subject = "Fra PBS: " + local_filename;
                email.Body = "Fra PBS: " + local_filename;
#endif
            }
            email.AddTo(Program.MailToName, Program.MailToAddr);
            email.From = Program.MailFrom;
            email.ReplyTo = Program.MailReply;
            email.AddDataAttachment2(local_filename, data, "text/plain");
            email.UnzipAttachments();

            success = mailman.SendEmail(email);
            if (success != true) throw new Exception(email.LastErrorText);

        }
    }

}
