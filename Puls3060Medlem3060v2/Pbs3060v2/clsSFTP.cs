using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.Diagnostics;
using System.Data.SqlClient;


namespace nsPbs3060v2
{
    
    public class clsSFTP
    {
        public MemPbsnetdir m_memPbsnetdir;
        private SftpClient m_sftp;
        private tblpbsfile m_rec_pbsfile;
        private tblsftp m_rec_sftp;

        private clsSFTP() { }
        public clsSFTP(dbData3060 p_dbData3060)
        {
#if (DEBUG)
            //m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "TestHD36" select s).First();
            //m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Test" select s).First();
            m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Produktion" select s).First();
#else
            //m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Test" select s).First();
            m_rec_sftp = (from s in p_dbData3060.tblsftp where s.navn == "Produktion" select s).First();
#endif
            Program.Log(string.Format("host={0}, port={1}, user={2}", m_rec_sftp.host, int.Parse(m_rec_sftp.port), m_rec_sftp.user), "SFTP ConnectionString"); 
            byte[] bPK = Encoding.UTF8.GetBytes(m_rec_sftp.certificate);
            MemoryStream mPK = new MemoryStream(bPK);
            PrivateKeyFile PK = new PrivateKeyFile(mPK, m_rec_sftp.pincode);
            m_sftp = new SftpClient(m_rec_sftp.host, int.Parse(m_rec_sftp.port), m_rec_sftp.user, PK);
            m_sftp.Connect();
            Program.Log("SFTP Connected"); 
        }

        public void DisconnectSFtp()
        {
             m_sftp.Disconnect();
        }

        public string WriteTilSFtp(dbData3060 p_dbData3060, int lobnr)
        {
            string TilPBSFilename = "Unknown";
            int FilesSize;

            var qry_selectfiles =
                from h in p_dbData3060.tblpbsforsendelse
                join d1 in p_dbData3060.tblpbsfilename on h.id equals d1.pbsforsendelseid into details1
                from d1 in details1.DefaultIfEmpty()
                where d1.id != null && d1.filename == null
                join d2 in p_dbData3060.tbltilpbs on h.id equals d2.pbsforsendelseid into details2
                from d2 in details2.DefaultIfEmpty()
                where d2.id == lobnr
                select new
                {
                    tilpbsid = (int?)d2.id,
                    d2.leverancespecifikation,
                    d2.delsystem,
                    d2.leverancetype,
                    Bilagdato = (DateTime?)d2.bilagdato,
                    Pbsforsendelseid = (int?)d2.pbsforsendelseid,
                    Udtrukket = (DateTime?)d2.udtrukket,
                    pbsfilesid = (int?)d1.id,
                    Leveranceid = (int)h.leveranceid
                };

            int antal = qry_selectfiles.Count();
            if (antal > 0)
            {
                var rec_selecfiles = qry_selectfiles.First();

                var qry_pbsfiles = from h in p_dbData3060.tblpbsfilename
                                   where h.id == rec_selecfiles.pbsfilesid
                                   select h;
                if (qry_pbsfiles.Count() > 0)
                {
                    tblpbsfilename m_rec_pbsfiles = qry_pbsfiles.First();
                    TilPBSFilename = AddPbcnetRecords(p_dbData3060, rec_selecfiles.delsystem, rec_selecfiles.Leveranceid, m_rec_pbsfiles.id);

                    var qry_pbsfile =
                        from h in m_rec_pbsfiles.tblpbsfile
                        orderby h.seqnr
                        select h;

                    string TilPBSFile = "";
                    int i = 0;
                    foreach (var rec_pbsfile in qry_pbsfile)
                    {
                        if (i++ > 0) TilPBSFile += "\r\n";
                        TilPBSFile += rec_pbsfile.data;
                    }
                    char[] c_TilPBSFile = TilPBSFile.ToCharArray();
                    byte[] b_TilPBSFile = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_TilPBSFile);
                    FilesSize = b_TilPBSFile.Length;

                    sendAttachedFile(p_dbData3060, TilPBSFilename, b_TilPBSFile, true);

                    string fullpath = m_rec_sftp.inbound + "/" + TilPBSFilename;
                    MemoryStream ms_TilPBSFile = new MemoryStream(b_TilPBSFile);
                    
                    Program.Log(string.Format("Start Upload of {0}", fullpath)); 
                    m_sftp.UploadFile(ms_TilPBSFile, fullpath);
                    Program.Log(string.Format("{0} Upload Completed", fullpath)); 
                    
                    m_rec_pbsfiles.type = 8;
                    m_rec_pbsfiles.path = m_rec_sftp.inbound;
                    m_rec_pbsfiles.filename = TilPBSFilename;
                    m_rec_pbsfiles.size = FilesSize;
                    m_rec_pbsfiles.atime = DateTime.Now;
                    m_rec_pbsfiles.mtime = DateTime.Now;
                    m_rec_pbsfiles.transmittime = DateTime.Now;
                    p_dbData3060.SaveChanges();
                }
            }
            return TilPBSFilename;
        }

        public void ReWriteTilSFtp(dbData3060 p_dbData3060, int ppbsfilesid)
        {
            string TilPBSFilename = "Unknown";
            int FilesSize;

            var qry_selectfiles =
                from h in p_dbData3060.tblpbsforsendelse
                join d1 in p_dbData3060.tblpbsfilename on h.id equals d1.pbsforsendelseid into details1
                from d1 in details1.DefaultIfEmpty()
                where d1.id == ppbsfilesid
                join d2 in p_dbData3060.tbltilpbs on h.id equals d2.pbsforsendelseid into details2
                from d2 in details2.DefaultIfEmpty()
                select new
                {
                    tilpbsid = (int?)d2.id,
                    d2.leverancespecifikation,
                    d2.delsystem,
                    d2.leverancetype,
                    Bilagdato = (DateTime?)d2.bilagdato,
                    Pbsforsendelseid = (int?)d2.pbsforsendelseid,
                    Udtrukket = (DateTime?)d2.udtrukket,
                    pbsfilesid = (int?)d1.id,
                    Leveranceid = (int)h.leveranceid,
                };

            int antal = qry_selectfiles.Count();
            if (antal > 0)
            {
                var rec_selecfiles = qry_selectfiles.First();

                var qry_pbsfiles = from h in p_dbData3060.tblpbsfilename
                                   where h.id == rec_selecfiles.pbsfilesid
                                   select h;
                if (qry_pbsfiles.Count() > 0)
                {
                    tblpbsfilename m_rec_pbsfiles = qry_pbsfiles.First();
                    if (m_rec_pbsfiles.filename != null)
                    {
                        if (m_rec_pbsfiles.filename.Length > 0) TilPBSFilename = m_rec_pbsfiles.filename;
                    }
 
                    var qry_pbsfile =
                        from h in m_rec_pbsfiles.tblpbsfile
                        orderby h.seqnr
                        select h;

                    string TilPBSFile = "";
                    int i = 0;
                    foreach (var rec_pbsfile in qry_pbsfile)
                    {
                        if (i++ > 0) TilPBSFile += "\r\n";
                        TilPBSFile += rec_pbsfile.data;
                    }
                    char[] c_TilPBSFile = TilPBSFile.ToCharArray();
                    byte[] b_TilPBSFile = System.Text.Encoding.GetEncoding("windows-1252").GetBytes(c_TilPBSFile);
                    FilesSize = b_TilPBSFile.Length;

                    sendAttachedFile(p_dbData3060, TilPBSFilename, b_TilPBSFile, true);

                    string fullpath = m_rec_sftp.inbound + "/" + TilPBSFilename;
                    MemoryStream ms_TilPBSFile = new MemoryStream(b_TilPBSFile);
                    
                    Program.Log(string.Format("Start Upload of {0}", fullpath)); 
                    m_sftp.UploadFile(ms_TilPBSFile, fullpath);
                    Program.Log(string.Format("{0} Upload Completed", fullpath)); 
                   
                    m_rec_pbsfiles.type = 8;
                    m_rec_pbsfiles.path = m_rec_sftp.inbound;
                    m_rec_pbsfiles.filename = TilPBSFilename;
                    m_rec_pbsfiles.size = FilesSize;
                    m_rec_pbsfiles.atime = DateTime.Now;
                    m_rec_pbsfiles.mtime = DateTime.Now;
                    m_rec_pbsfiles.transmittime = DateTime.Now;
                    p_dbData3060.SaveChanges();
                }
            }
        }

        public int ReadFraSFtp(dbData3060 p_dbData3060)
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
                    tblpbsfilename m_rec_pbsfiles = new tblpbsfilename
                    {
                        type = rec_pbsnetdir.Type,
                        path = rec_pbsnetdir.Path,
                        filename = rec_pbsnetdir.Filename,
                        size = rec_pbsnetdir.Size,
                        atime = rec_pbsnetdir.Atime,
                        mtime = rec_pbsnetdir.Mtime,
                        perm = rec_pbsnetdir.Perm,
                        uid = rec_pbsnetdir.Uid,
                        gid = rec_pbsnetdir.Gid
                    };
                    p_dbData3060.tblpbsfilename.Add(m_rec_pbsfiles);

                    //***********************************************************************
                    //  Open a file on the server:
                    string fullpath = rec_pbsnetdir.Path + "/" + rec_pbsnetdir.Filename;
                    int numBytes = (int)rec_pbsnetdir.Size;
 
                    byte[] b_data = new byte[numBytes];
                    MemoryStream stream = new MemoryStream(b_data, true);

                    Program.Log(string.Format("Start Download of {0}", fullpath)); 
                    m_sftp.DownloadFile(fullpath, stream);
                    Program.Log(string.Format("{0} Download Completed", fullpath)); 

                    sendAttachedFile(p_dbData3060,rec_pbsnetdir.Filename, b_data, false);
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
                            m_rec_pbsfile = new tblpbsfile
                            {
                                seqnr = seqnr,
                                data = ln
                            };
                            m_rec_pbsfiles.tblpbsfile.Add(m_rec_pbsfile);
                        }
                    }

                    m_rec_pbsfiles.transmittime = DateTime.Now;
                    //var cb = new SqlConnectionStringBuilder(p_dbData3060.Connection.ConnectionString);
                    Program.Log(string.Format("Start Write {0} to SQL Database {1} on {2}", rec_pbsnetdir.Filename, "cb.InitialCatalog", "cb.DataSource"));
                    p_dbData3060.SaveChanges();
                    Program.Log(string.Format("{0} Write to SQL Database Completed", rec_pbsnetdir.Filename)); 
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

        public int ReadFromLocalFile(dbData3060 p_dbData3060, string FilePath)
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

            Program.Log(string.Format("Start Reading of {0}", fullpath));
            using (FileStream ts = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
            {
                ts.Read(b_data, 0, numBytes);
            }
            Program.Log(string.Format("{0} Reading Completed", fullpath));

            sendAttachedFile(p_dbData3060, file.Name, b_data, false);
            char[] c_data = System.Text.Encoding.GetEncoding("windows-1252").GetString(b_data).ToCharArray();
            string filecontens = new string(c_data);

            string filecontens2 = filecontens.TrimEnd('\n');
            string filecontens3 = filecontens2.TrimEnd('\r');
            string filecontens4 = filecontens3.TrimEnd('\n');

            string[] lines = filecontens4.Split('\n');
            string ln = null;
            int seqnr = 0;
            string ln0_6;
            
            tblpbsfilename m_rec_pbsfiles = new tblpbsfilename
            {
                type = 8,
                path = file.Directory.FullName,
                filename = file.Name,
                size = (int)file.Length,
                atime = file.LastAccessTime,
                mtime = file.LastWriteTime
            };
            p_dbData3060.tblpbsfilename.Add(m_rec_pbsfiles);           
            for (int idx = 0; idx < lines.Count(); idx++)
            {
                ln = lines[idx].TrimEnd('\r');
                try { ln0_6 = ln.Substring(0, 6); }
                catch { ln0_6 = ""; }
                if (((seqnr == 0) && !(ln0_6 == "PBCNET")) || (seqnr > 0)) { seqnr++; }
                if (ln.Length > 0)
                {
                    m_rec_pbsfile = new tblpbsfile
                    {
                        seqnr = seqnr,
                        data = ln
                    };
                    m_rec_pbsfiles.tblpbsfile.Add(m_rec_pbsfile);
                }
            }

            m_rec_pbsfiles.transmittime = DateTime.Now;
            //var cb = new SqlConnectionStringBuilder(p_dbData3060.Connection.ConnectionString);
            Program.Log(string.Format("Start Write {0} to SQL Database {1} on {2}", file.Name, "cb.InitialCatalog", "cb.DataSource"));
            p_dbData3060.SaveChanges();
            Program.Log(string.Format("{0} Write to SQL Database Completed", file.Name));
            return 1;
        }

        public void sendAttachedFile(dbData3060 p_dbData3060, string filename, byte[] data, bool bTilPBS)
        {
            string local_filename = filename.Replace('.', '_') + ".txt";

            string SmtpUsername = p_dbData3060.GetSysinfo("SMTPUSER");
            string SmtpPassword = p_dbData3060.GetSysinfo("SMTPPASSWD"); 
            var smtp = new SmtpClient
            {
                Host = p_dbData3060.GetSysinfo("SMTPHOST"),
                Port = int.Parse(p_dbData3060.GetSysinfo("SMTPPORT")),
                EnableSsl = bool.Parse(p_dbData3060.GetSysinfo("SMTPSSL")),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SmtpUsername, SmtpPassword)
            };

            MailMessage email = new MailMessage();
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

            email.To.Add(new MailAddress( p_dbData3060.GetSysinfo("MAILTOADDR"),p_dbData3060.GetSysinfo("MAILTONAME")));
            email.From = new MailAddress(p_dbData3060.GetSysinfo("MAILFROM"));
            email.ReplyToList.Add(new MailAddress(p_dbData3060.GetSysinfo("MAILREPLY")));
            email.Attachments.Add(new Attachment(new MemoryStream(data),local_filename,"text/plain"));
            smtp.Send(email);
        }



        public string AddPbcnetRecords(dbData3060 p_dbData3060, string delsystem, int leveranceid, int pbsfilesid)
        {
            int antal;
            int pbcnetrecords;
            DateTime transmisionsdato;
            int idlev;
            string filename;
            string rec;

            pbcnetrecords = (from h in p_dbData3060.tblpbsfile
                             where h.pbsfilesid == pbsfilesid & (h.seqnr == 0 | h.seqnr == 9999)
                             select h).Count();

            if (pbcnetrecords == 0)
            {
                // Find antal records
                antal = (from h in p_dbData3060.tblpbsfile
                         where h.pbsfilesid == pbsfilesid & h.seqnr != 0 & h.seqnr != 9999
                         select h).Count();
                transmisionsdato = DateTime.Now;

                idlev = p_dbData3060.nextval("idlev");  

                tblpbsfilename rec_pbsfiles = (from h in p_dbData3060.tblpbsfilename where h.id == pbsfilesid select h).First();

                rec = write00(delsystem, transmisionsdato, idlev, leveranceid);
                tblpbsfile rec_pbsfile = new tblpbsfile { seqnr = 0, data = rec };
                rec_pbsfiles.tblpbsfile.Add(rec_pbsfile);

                rec = write90(delsystem, transmisionsdato, idlev, leveranceid, antal);
                rec_pbsfile = new tblpbsfile { seqnr = 9999, data = rec };
                rec_pbsfiles.tblpbsfile.Add(rec_pbsfile);

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
