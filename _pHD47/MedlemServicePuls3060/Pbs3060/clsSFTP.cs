using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using MimeKit;
using MailKit.Net.Imap;
using MailKit;
using Renci.SshNet.Sftp;
using System.Data.SqlClient;

namespace Pbs3060
{
    public class clsSFTP
    {
        public MemPbsnetdir m_memPbsnetdir;
        private SftpClient m_sftp;
        private Tblpbsfile m_rec_pbsfile;
        private tblsftp m_rec_sftp;

        private clsSFTP() { }
        public clsSFTP(dbData3060DataContext p_dbData3060)
        {
#if (DEBUG)
            //m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "TestHD36" select s).First();
            //m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Test" select s).First();
            m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Produktion" select s).First();
#else
            //m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Test" select s).First();
            m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Produktion" select s).First();
#endif
            Console.WriteLine(string.Format("host={0}, port={1}, user={2}", m_rec_sftp.host, int.Parse(m_rec_sftp.port), m_rec_sftp.user), "SFTP ConnectionString");
            byte[] bPK = Encoding.UTF8.GetBytes(m_rec_sftp.certificate);
            MemoryStream mPK = new MemoryStream(bPK);
            PrivateKeyFile PK = new PrivateKeyFile(mPK, m_rec_sftp.pincode);
            m_sftp = new SftpClient(m_rec_sftp.host, int.Parse(m_rec_sftp.port), m_rec_sftp.user, PK);
            m_sftp.Connect();
            Console.WriteLine("SFTP Connected");
        }

        public void DisconnectSFtp()
        {
            m_sftp.Disconnect();
        }

        public string WriteTilSFtp(dbData3060DataContext p_dbData3060, int lobnr)
        {
            string TilPBSFilename = "Unknown";
            int FilesSize;

            var qry_selectfiles =
                from h in p_dbData3060.Tblpbsforsendelse
                join d1 in p_dbData3060.Tblpbsfilename on h.Id equals d1.Pbsforsendelseid into details1
                from d1 in details1.DefaultIfEmpty()
                where d1.Id != null && d1.Filename == null
                join d2 in p_dbData3060.Tbltilpbs on h.Id equals d2.Pbsforsendelseid into details2
                from d2 in details2.DefaultIfEmpty()
                where d2.Id == lobnr
                select new
                {
                    tilpbsid = (int?)d2.Id,
                    d2.Leverancespecifikation,
                    d2.Delsystem,
                    d2.Leverancetype,
                    Bilagdato = (DateTime?)d2.Bilagdato,
                    Pbsforsendelseid = (int?)d2.Pbsforsendelseid,
                    Udtrukket = (DateTime?)d2.Udtrukket,
                    pbsfilesid = (int?)d1.Id,
                    Leveranceid = (int)h.Leveranceid
                };

            int antal = qry_selectfiles.Count();
            if (antal > 0)
            {
                var rec_selecfiles = qry_selectfiles.First();

                var qry_pbsfiles = from h in p_dbData3060.Tblpbsfilename
                                   where h.Id == rec_selecfiles.pbsfilesid
                                   select h;
                if (qry_pbsfiles.Count() > 0)
                {
                    Tblpbsfilename m_rec_pbsfiles = qry_pbsfiles.First();
                    TilPBSFilename = AddPbcnetRecords(p_dbData3060, rec_selecfiles.Delsystem, rec_selecfiles.Leveranceid, m_rec_pbsfiles.Id);

                    var qry_pbsfile =
                        from h in m_rec_pbsfiles.Tblpbsfile
                        orderby h.Seqnr
                        select h;

                    string TilPBSFile = "";
                    int i = 0;
                    foreach (var rec_pbsfile in qry_pbsfile)
                    {
                        if (i++ > 0) TilPBSFile += "\r\n";
                        TilPBSFile += rec_pbsfile.Data;
                    }
                    char[] c_TilPBSFile = TilPBSFile.ToCharArray();
                    byte[] b_TilPBSFile = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_TilPBSFile);
                    FilesSize = b_TilPBSFile.Length;

                    //clsAzure objAzure = new clsAzure();
                    //objAzure.uploadBlob(TilPBSFilename, b_TilPBSFile, true);

                    imapSaveAttachedFile(p_dbData3060, TilPBSFilename, b_TilPBSFile, true);

                    string fullpath = m_rec_sftp.inbound + "/" + TilPBSFilename;
                    MemoryStream ms_TilPBSFile = new MemoryStream(b_TilPBSFile);

                    Console.WriteLine(string.Format("Start Upload of {0}", fullpath));
                    m_sftp.UploadFile(ms_TilPBSFile, fullpath);
                    Console.WriteLine(string.Format("{0} Upload Completed", fullpath));

                    m_rec_pbsfiles.Type = 8;
                    m_rec_pbsfiles.Path = m_rec_sftp.inbound;
                    m_rec_pbsfiles.Filename = TilPBSFilename;
                    m_rec_pbsfiles.Size = FilesSize;
                    m_rec_pbsfiles.Atime = DateTime.Now;
                    m_rec_pbsfiles.Mtime = DateTime.Now;
                    m_rec_pbsfiles.Transmittime = DateTime.Now;
                    p_dbData3060.SaveChanges();
                }
            }
            return TilPBSFilename;
        }

        public void ReWriteTilSFtp(dbData3060DataContext p_dbData3060, int ppbsfilesid)
        {
            string TilPBSFilename = "Unknown";
            int FilesSize;

            var qry_selectfiles =
                from h in p_dbData3060.Tblpbsforsendelse
                join d1 in p_dbData3060.Tblpbsfilename on h.Id equals d1.Pbsforsendelseid into details1
                from d1 in details1.DefaultIfEmpty()
                where d1.Id == ppbsfilesid
                join d2 in p_dbData3060.Tbltilpbs on h.Id equals d2.Pbsforsendelseid into details2
                from d2 in details2.DefaultIfEmpty()
                select new
                {
                    tilpbsid = (int?)d2.Id,
                    d2.Leverancespecifikation,
                    d2.Delsystem,
                    d2.Leverancetype,
                    Bilagdato = (DateTime?)d2.Bilagdato,
                    Pbsforsendelseid = (int?)d2.Pbsforsendelseid,
                    Udtrukket = (DateTime?)d2.Udtrukket,
                    pbsfilesid = (int?)d1.Id,
                    Leveranceid = (int)h.Leveranceid,
                };

            int antal = qry_selectfiles.Count();
            if (antal > 0)
            {
                var rec_selecfiles = qry_selectfiles.First();

                var qry_pbsfiles = from h in p_dbData3060.Tblpbsfilename
                                   where h.Id == rec_selecfiles.pbsfilesid
                                   select h;
                if (qry_pbsfiles.Count() > 0)
                {
                    Tblpbsfilename m_rec_pbsfiles = qry_pbsfiles.First();
                    if (m_rec_pbsfiles.Filename != null)
                    {
                        if (m_rec_pbsfiles.Filename.Length > 0) TilPBSFilename = m_rec_pbsfiles.Filename;
                    }

                    var qry_pbsfile =
                        from h in m_rec_pbsfiles.Tblpbsfile
                        orderby h.Seqnr
                        select h;

                    string TilPBSFile = "";
                    int i = 0;
                    foreach (var rec_pbsfile in qry_pbsfile)
                    {
                        if (i++ > 0) TilPBSFile += "\r\n";
                        TilPBSFile += rec_pbsfile.Data;
                    }
                    char[] c_TilPBSFile = TilPBSFile.ToCharArray();
                    byte[] b_TilPBSFile = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_TilPBSFile);
                    FilesSize = b_TilPBSFile.Length;

                    //clsAzure objAzure = new clsAzure();
                    //objAzure.uploadBlob(TilPBSFilename, b_TilPBSFile, true);

                    imapSaveAttachedFile(p_dbData3060, TilPBSFilename, b_TilPBSFile, true);

                    string fullpath = m_rec_sftp.inbound + "/" + TilPBSFilename;
                    MemoryStream ms_TilPBSFile = new MemoryStream(b_TilPBSFile);

                    Console.WriteLine(string.Format("Start Upload of {0}", fullpath));
                    m_sftp.UploadFile(ms_TilPBSFile, fullpath);
                    Console.WriteLine(string.Format("{0} Upload Completed", fullpath));

                    m_rec_pbsfiles.Type = 8;
                    m_rec_pbsfiles.Path = m_rec_sftp.inbound;
                    m_rec_pbsfiles.Filename = TilPBSFilename;
                    m_rec_pbsfiles.Size = FilesSize;
                    m_rec_pbsfiles.Atime = DateTime.Now;
                    m_rec_pbsfiles.Mtime = DateTime.Now;
                    m_rec_pbsfiles.Transmittime = DateTime.Now;
                    p_dbData3060.SaveChanges();
                }
            }
        }

        public int ReadFraSFtp(dbData3060DataContext p_dbData3060)
        {
            m_memPbsnetdir = new MemPbsnetdir(); //opret ny memPbsnetdir

            //  Iterate over the files.
            foreach (SftpFile fileObj in m_sftp.ListDirectory(m_rec_sftp.outbound))
            {
                if (fileObj.Name != "." && fileObj.Name != "..")
                {
                    recPbsnetdir rec = new recPbsnetdir
                    {
                        Type = 8,
                        Path = m_rec_sftp.outbound,
                        Filename = fileObj.Name,
                        Size = (int)fileObj.Length,
                        Atime = fileObj.LastAccessTime,
                        Mtime = fileObj.LastWriteTime,
                        Gid = fileObj.GroupId,
                        Uid = fileObj.UserId,
                        Perm = ""

                    };
                    m_memPbsnetdir.Add(rec);
                }
            }

            var leftqry_pbsnetdir =
                from h in m_memPbsnetdir
                    //join d1 in p_dbData3060.Tblpbsfiles on new { h.Path, h.Filename } equals new { d1.Path, d1.Filename } into details
                    //from d1 in details.DefaultIfEmpty(new Tblpbsfiles { Id = -1, Type = (int?)null, Path = null, Filename = null, Size = (int?)null, Atime = (DateTime?)null, Mtime = (DateTime?)null, Perm = null, Uid = (int?)null, Gid = (int?)null })
                    //where d1.Path == null && d1.Filename == null
                select h;

            int AntalFiler = leftqry_pbsnetdir.Count();
            if (leftqry_pbsnetdir.Count() > 0)
            {
                foreach (var rec_pbsnetdir in leftqry_pbsnetdir)
                {
                    Tblpbsfilename m_rec_pbsfiles = new Tblpbsfilename
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
                    p_dbData3060.Tblpbsfilename.Add(m_rec_pbsfiles);

                    //***********************************************************************
                    //  Open a file on the server:
                    string fullpath = rec_pbsnetdir.Path + "/" + rec_pbsnetdir.Filename;
                    int numBytes = (int)rec_pbsnetdir.Size;

                    byte[] b_data = new byte[numBytes];
                    MemoryStream stream = new MemoryStream(b_data, true);

                    Console.WriteLine(string.Format("Start Download of {0}", fullpath));
                    m_sftp.DownloadFile(fullpath, stream);
                    Console.WriteLine(string.Format("{0} Download Completed", fullpath));

                    //clsAzure objAzure = new clsAzure();
                    //objAzure.uploadBlob(rec_pbsnetdir.Filename, b_data, false);

                    imapSaveAttachedFile(p_dbData3060, rec_pbsnetdir.Filename, b_data, false);
                    char[] c_data = System.Text.Encoding.GetEncoding("windows-1252").GetString(b_data).ToCharArray();
                    string filecontens = new string(c_data);

                    string filecontens2 = filecontens.TrimEnd('\n');
                    string filecontens3 = filecontens2.TrimEnd('\r');
                    string filecontens4 = filecontens3.TrimEnd('\n');

                    string[] lines = filecontens4.Split('\n');
                    string ln = null;
                    int seqnr = 0;
                    string ln0_6;
                    for (int idx = 0; idx < lines.Count(); idx++)
                    {
                        ln = lines[idx].TrimEnd('\r');
                        try { ln0_6 = ln.Substring(0, 6); }
                        catch { ln0_6 = ""; }
                        if (((seqnr == 0) && !(ln0_6 == "PBCNET")) || (seqnr > 0)) { seqnr++; }
                        if (ln.Length > 0)
                        {
                            m_rec_pbsfile = new Tblpbsfile
                            {
                                Seqnr = seqnr,
                                Data = ln
                            };
                            m_rec_pbsfiles.Tblpbsfile.Add(m_rec_pbsfile);
                        }
                    }
                    //this.Database.GetDbConnection()
                    m_rec_pbsfiles.Transmittime = DateTime.Now;
                    //var cb = new SqlConnectionStringBuilder(p_dbData3060.Connection.ConnectionString);
                    Console.WriteLine(string.Format("Start Write {0} to SQL Database {1} on {2}", rec_pbsnetdir.Filename, "cb.InitialCatalog", "cb.DataSource"));
                    try
                    {
                        p_dbData3060.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("p_dbData3060.SaveChanges() failed with message: {0}", e.Message));
                        throw;
                    }
                    Console.WriteLine(string.Format("{0} Write to SQL Database Completed", rec_pbsnetdir.Filename));
                }
            }
            p_dbData3060.SaveChanges();
            return AntalFiler;
        }

        public int ReadDirFraSFtp()
        {
            int AntalFiler = 0;

            m_memPbsnetdir = new MemPbsnetdir(); //opret ny memPbsnetdir

            //  Iterate over the files. 
            foreach (SftpFile fileObj in m_sftp.ListDirectory(m_rec_sftp.outbound))
            {
                if (fileObj.Name != "." && fileObj.Name != "..")
                {
                    recPbsnetdir rec = new recPbsnetdir
                    {
                        Type = 8,
                        Path = m_rec_sftp.outbound,
                        Filename = fileObj.Name,
                        Size = (int)fileObj.Length,
                        Atime = fileObj.LastAccessTime,
                        Mtime = fileObj.LastWriteTime,
                        Gid = fileObj.GroupId,
                        Uid = fileObj.UserId,
                        Perm = ""

                    };
                    m_memPbsnetdir.Add(rec);
                    AntalFiler++;

                }
            }

            return AntalFiler;
        }

        public int ReadFromLocalFile(dbData3060DataContext p_dbData3060, string FilePath)
        {
            FileInfo file;
            try
            {
                file = new FileInfo(FilePath);
                if (!(file.Exists))
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }

            //***********************************************************************
            //  Open a local file:
            string fullpath = file.FullName;
            int numBytes = (int)file.Length;
            byte[] b_data = new byte[numBytes];

            Console.WriteLine(string.Format("Start Reading of {0}", fullpath));
            using (FileStream ts = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
            {
                ts.Read(b_data, 0, numBytes);
            }
            Console.WriteLine(string.Format("{0} Reading Completed", fullpath));

            //clsAzure objAzure = new clsAzure();
            //objAzure.uploadBlob(file.Name, b_data, false);

            imapSaveAttachedFile(p_dbData3060, file.Name, b_data, false);
            char[] c_data = System.Text.Encoding.GetEncoding("windows-1252").GetString(b_data).ToCharArray();
            string filecontens = new string(c_data);

            string filecontens2 = filecontens.TrimEnd('\n');
            string filecontens3 = filecontens2.TrimEnd('\r');
            string filecontens4 = filecontens3.TrimEnd('\n');

            string[] lines = filecontens4.Split('\n');
            string ln = null;
            int seqnr = 0;
            string ln0_6;

            Tblpbsfilename m_rec_pbsfilename = new Tblpbsfilename
            {
                Type = 8,
                Path = file.Directory.FullName,
                Filename = file.Name,
                Size = (int)file.Length,
                Atime = file.LastAccessTime,
                Mtime = file.LastWriteTime
            };

            for (int idx = 0; idx < lines.Count(); idx++)
            {
                ln = lines[idx].TrimEnd('\r');
                try { ln0_6 = ln.Substring(0, 6); }
                catch { ln0_6 = ""; }
                if (((seqnr == 0) && !(ln0_6 == "PBCNET")) || (seqnr > 0)) { seqnr++; }
                if (ln.Length > 0)
                {
                    m_rec_pbsfile = new Tblpbsfile
                    {
                        Seqnr = seqnr,
                        Data = ln
                    };
                    m_rec_pbsfilename.Tblpbsfile.Add(m_rec_pbsfile);
                }
            }
            m_rec_pbsfilename.Transmittime = DateTime.Now;
            p_dbData3060.Tblpbsfilename.Add(m_rec_pbsfilename);
            //var cb = new SqlConnectionStringBuilder(p_dbData3060.Connection.ConnectionString);
            Console.WriteLine(string.Format("Start Write {0} to SQL Database {1} on {2}", file.Name, "cb.InitialCatalog", "cb.DataSource"));
            try
            {
                p_dbData3060.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("p_dbData3060.SaveChanges() failed with message: {0}", e.Message));
                throw;
            }
            Console.WriteLine(string.Format("{0} Write to SQL Database Completed", file.Name));
            return 1;
        }

        public void imapSaveAttachedFile(dbData3060DataContext p_dbData3060, string filename, byte[] data, bool bTilPBS)
        {
            string local_filename = filename.Replace('.', '_') + ".txt";

            MimeMessage message = new MimeMessage();
            TextPart body;

            message.To.Add(new MailboxAddress(@"Regnskab Puls3060", @"regnskab@puls3060.dk"));
            message.From.Add(new MailboxAddress(@"Regnskab Puls3060", @"regnskab@puls3060.dk"));

            if (bTilPBS)
            {
#if (DEBUG)
                message.Subject = "Test Til PBS: " + local_filename;
                body = new TextPart("plain") { Text = @"Test Til PBS: " + local_filename };
#else
                message.Subject = "Til PBS: " + local_filename;
                body = new TextPart("plain") { Text = @"Til PBS: " + local_filename};
#endif
            }
            else
            {
#if (DEBUG)
                message.Subject = "Test Fra PBS: " + local_filename;
                body = new TextPart("plain") { Text = @"Test Fra PBS: " + local_filename };
#else
                message.Subject = "Fra PBS: " + local_filename;
                body = new TextPart("plain") { Text = @"Fra PBS: " + local_filename };
#endif
            }

            var attachment = new MimePart("text", "plain")
            {
                ContentObject = new ContentObject(new MemoryStream(data), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = local_filename
            };

            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);

            message.Body = multipart;

            using (var client = new ImapClient())
            {
                client.Connect("imap.gigahost.dk", 993, true);
                client.AuthenticationMechanisms.Remove("XOAUTH");
                client.Authenticate(clsApp.GigaHostImapUser, clsApp.GigaHostImapPW);

                var PBS = client.GetFolder("INBOX.PBS");
                PBS.Open(FolderAccess.ReadWrite);
                PBS.Append(message);
                PBS.Close();

                client.Disconnect(true);
            }

        }

        public string AddPbcnetRecords(dbData3060DataContext p_dbData3060, string delsystem, int leveranceid, int pbsfilesid)
        {
            int antal;
            int pbcnetrecords;
            DateTime transmisionsdato;
            int idlev;
            string filename;
            string rec;

            pbcnetrecords = (from h in p_dbData3060.Tblpbsfile
                             where h.Pbsfilesid == pbsfilesid & (h.Seqnr == 0 | h.Seqnr == 9999)
                             select h).Count();

            if (pbcnetrecords == 0)
            {
                // Find antal records
                antal = (from h in p_dbData3060.Tblpbsfile
                         where h.Pbsfilesid == pbsfilesid & h.Seqnr != 0 & h.Seqnr != 9999
                         select h).Count();
                transmisionsdato = DateTime.Now;

                idlev = p_dbData3060.nextval_v2("idlev");

                Tblpbsfilename rec_pbsfiles = (from h in p_dbData3060.Tblpbsfilename where h.Id == pbsfilesid select h).First();

                rec = write00(delsystem, transmisionsdato, idlev, leveranceid);
                Tblpbsfile rec_pbsfile = new Tblpbsfile { Seqnr = 0, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                rec = write90(delsystem, transmisionsdato, idlev, leveranceid, antal);
                rec_pbsfile = new Tblpbsfile { Seqnr = 9999, Data = rec };
                rec_pbsfiles.Tblpbsfile.Add(rec_pbsfile);

                p_dbData3060.SaveChanges();

                filename = "D";
                filename += lpad(String.Format("{0:yyMMdd}", transmisionsdato), 6, '?');
                filename += lpad(idlev, 2, '0');
                filename += ".";
                filename += rpad(delsystem, 3, '_');

            }
            else
                filename = "Unknown";

            return filename;
        }

        private string write00(string delsystem, DateTime transmisionsdato, int idlev, int idfri)
        {
            string rec = null;

            rec = "PBCNET00";
            rec += lpad(delsystem, 3, '?');
            rec += rpad("", 1, ' ');
            rec += lpad(String.Format("{0:yyMMdd}", transmisionsdato), 6, '?');
            rec += lpad(idlev, 2, '0');
            rec += rpad("", 2, ' ');
            rec += lpad(idfri, 6, '0');
            rec += rpad("", 8, ' ');
            rec += rpad("", 8, ' ');

            return rec;
        }

        private string write90(string delsystem, DateTime transmisiondato, int idlev, int idfri, int antal)
        {
            string rec = null;

            rec = "PBCNET90";
            rec += lpad(delsystem, 3, '?');
            rec += rpad("", 1, ' ');
            rec += lpad(String.Format("{0:yyMMdd}", transmisiondato), 6, '?');
            rec += lpad(idlev, 2, '0');
            rec += rpad("", 2, ' ');
            rec += lpad(idfri, 6, '0');
            rec += lpad(antal, 6, '0');

            return rec;
        }

        public object lpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadLeft(Length, PadChar);
        }

        public object rpad(Object oVal, int Length, char PadChar)
        {
            string Val = oVal.ToString();
            return Val.PadRight(Length, PadChar);
        }
    }
}
