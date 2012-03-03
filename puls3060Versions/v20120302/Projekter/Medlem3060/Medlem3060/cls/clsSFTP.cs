﻿using System;
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


namespace nsPuls3060
{

    public class clsSFTP
    {
        private SFtp m_sftp;
        private tblpbsfile m_rec_pbsfile;
        private tblsftp m_rec_sftp;

        public clsSFTP()
        {
#if (DEBUG)
            m_rec_sftp = (from s in Program.dbData3060.tblsftps where s.navn == "TestHD36" select s).First();
            //m_rec_sftp = (from s in Program.dbData3060.tblsftps where s.navn == "Test" select s).First();
            //m_rec_sftp = (from s in Program.dbData3060.tblsftps where s.navn == "Produktion" select s).First();
#else
            m_rec_sftp = (from s in Program.dbData3060.tblsftps where s.navn == "Produktion" select s).First();
#endif

            m_sftp = new SFtp();
            bool success = m_sftp.UnlockComponent("HAFSJOSSH_6pspJCMP1QnW");
            if (!success) throw new Exception(m_sftp.LastErrorText);
            m_sftp.ConnectTimeoutMs = 60000;
            m_sftp.IdleTimeoutMs = 55000;
            success = m_sftp.Connect(m_rec_sftp.host, int.Parse(m_rec_sftp.port));
            if (!success) throw new Exception(m_sftp.LastErrorText);
            Chilkat.SshKey key = new Chilkat.SshKey();

            string privKey = m_rec_sftp.certificate;
            if (privKey == null) throw new Exception(m_sftp.LastErrorText);

            key.Password = m_rec_sftp.pincode;
            success = key.FromOpenSshPrivateKey(privKey);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            success = m_sftp.AuthenticatePk(m_rec_sftp.user, key);
            if (!success) throw new Exception(m_sftp.LastErrorText);

            //  After authenticating, the SFTP subsystem must be initialized:
            success = m_sftp.InitializeSftp();
            if (!success) throw new Exception(m_sftp.LastErrorText);
        }

        public void DisconnectSFtp()
        {
            m_sftp.Disconnect();
        }

        public string WriteTilSFtp(int lobnr)
        {
            string TilPBSFilename = "Unknown";
            int FilesSize;

            var rec_regnskab = Program.qryAktivRegnskab();

            var qry_selectfiles =
                from h in Program.dbData3060.tblpbsforsendelses
                join d1 in Program.dbData3060.tblpbsfilenames on h.id equals d1.pbsforsendelseid into details1
                from d1 in details1.DefaultIfEmpty()
                where d1.id != null && d1.filename == null
                join d2 in Program.dbData3060.tbltilpbs on h.id equals d2.pbsforsendelseid into details2
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
                
                var qry_pbsfiles = from h in Program.dbData3060.tblpbsfilenames
                                   where h.id == rec_selecfiles.pbsfilesid
                                   select h;
                if (qry_pbsfiles.Count() > 0)
                {
                    tblpbsfilename m_rec_pbsfiles = qry_pbsfiles.First();
                    TilPBSFilename = AddPbcnetRecords(rec_selecfiles.delsystem, rec_selecfiles.Leveranceid, m_rec_pbsfiles.id);
                    bool success;


                    var qry_pbsfile =
                        from h in m_rec_pbsfiles.tblpbsfiles
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

                    sendAttachedFile(TilPBSFilename, b_TilPBSFile, true);

                    string fullpath = m_rec_sftp.inbound + "/" + TilPBSFilename;
                    string handle = m_sftp.OpenFile(fullpath, "writeOnly", "createTruncate");
                    if (handle == null) throw new Exception(m_sftp.LastErrorText);

                    success = m_sftp.WriteFileBytes(handle, b_TilPBSFile);
                    if (!success) throw new Exception(m_sftp.LastErrorText);

                    success = m_sftp.CloseHandle(handle);
                    if (success != true) throw new Exception(m_sftp.LastErrorText);

                    m_rec_pbsfiles.type = 8;
                    m_rec_pbsfiles.path = m_rec_sftp.inbound;
                    m_rec_pbsfiles.filename = TilPBSFilename;
                    m_rec_pbsfiles.size = FilesSize;
                    m_rec_pbsfiles.atime = DateTime.Now;
                    m_rec_pbsfiles.mtime = DateTime.Now;
                    m_rec_pbsfiles.transmittime = DateTime.Now;
                    Program.dbData3060.SubmitChanges();
                }
            }
            return TilPBSFilename;
        }

        public void ReWriteTilSFtp(int ppbsfilesid)
        {
            string TilPBSFilename = "Unknown";
            int FilesSize;

            var rec_regnskab = Program.qryAktivRegnskab();

            var qry_selectfiles =
                from h in Program.dbData3060.tblpbsforsendelses
                join d1 in Program.dbData3060.tblpbsfilenames on h.id equals d1.pbsforsendelseid into details1
                from d1 in details1.DefaultIfEmpty()
                where d1.id == ppbsfilesid
                join d2 in Program.dbData3060.tbltilpbs on h.id equals d2.pbsforsendelseid into details2
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

                var qry_pbsfiles = from h in Program.dbData3060.tblpbsfilenames
                                   where h.id == rec_selecfiles.pbsfilesid
                                   select h;
                if (qry_pbsfiles.Count() > 0)
                {
                    tblpbsfilename m_rec_pbsfiles = qry_pbsfiles.First();
                    if (m_rec_pbsfiles.filename != null)
                    {
                        if (m_rec_pbsfiles.filename.Length > 0) TilPBSFilename = m_rec_pbsfiles.filename;
                    }
                    bool success;

                    var qry_pbsfile =
                        from h in m_rec_pbsfiles.tblpbsfiles
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

                    sendAttachedFile(TilPBSFilename, b_TilPBSFile, true);

                    string fullpath = m_rec_sftp.inbound + "/" + TilPBSFilename;
                    string handle = m_sftp.OpenFile(fullpath, "writeOnly", "createTruncate");
                    if (handle == null) throw new Exception(m_sftp.LastErrorText);

                    success = m_sftp.WriteFileBytes(handle, b_TilPBSFile);
                    if (!success) throw new Exception(m_sftp.LastErrorText);

                    success = m_sftp.CloseHandle(handle);
                    if (success != true) throw new Exception(m_sftp.LastErrorText);

                    m_rec_pbsfiles.type = 8;
                    m_rec_pbsfiles.path = m_rec_sftp.inbound;
                    m_rec_pbsfiles.filename = TilPBSFilename;
                    m_rec_pbsfiles.size = FilesSize;
                    m_rec_pbsfiles.atime = DateTime.Now;
                    m_rec_pbsfiles.mtime = DateTime.Now;
                    m_rec_pbsfiles.transmittime = DateTime.Now;
                    Program.dbData3060.SubmitChanges();
                }
            }
        }

        public int ReadFraSFtp()
        {
            string homedir = m_sftp.RealPath(".", "");
            //  Open a directory on the server...
            string handle = m_sftp.OpenDir(m_rec_sftp.outbound);
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
                //join d1 in Program.dbData3060.Tblpbsfiles on new { h.Path, h.Filename } equals new { d1.Path, d1.Filename } into details
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
                    Program.dbData3060.tblpbsfilenames.InsertOnSubmit(m_rec_pbsfiles);

                    //***********************************************************************
                    //  Open a file on the server:
                    string fullpath = rec_pbsnetdir.Path + "/" + rec_pbsnetdir.Filename;
                    string filehandle = m_sftp.OpenFile(fullpath, "readOnly", "openExisting");
                    if (filehandle == null) throw new Exception(m_sftp.LastErrorText);

                    int numBytes = (int)rec_pbsnetdir.Size;
                    if (numBytes < 0) throw new Exception(m_sftp.LastErrorText);

                    //---------------------------------------------------------------------
                    byte[] b_data = null;
                    bool bEof = false; 
                    int chunkSizeGet = 10240;
                    int chunkSizeRead = 0;
                    m_sftp.ClearAccumulateBuffer();
                    while (bEof == false)
                    {
                        chunkSizeRead = m_sftp.AccumulateBytes(handle, chunkSizeGet);
                        if (chunkSizeRead == -1)
                            throw new Exception(m_sftp.LastErrorText); 
                        bEof = m_sftp.Eof(handle);
                    }
                    b_data = m_sftp.AccumulateBuffer;
                    //---------------------------------------------------------------------
                    if (b_data == null) throw new Exception(m_sftp.LastErrorText);
                    sendAttachedFile(rec_pbsnetdir.Filename, b_data, false);
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
                        try { ln0_6 = ln.Substring(0, 6); } catch { ln0_6 = ""; }
                        if (((seqnr == 0) && !(ln0_6 == "PBCNET")) || (seqnr > 0)) { seqnr++; }
                        if (ln.Length > 0)
                        {
                            m_rec_pbsfile = new tblpbsfile
                            {
                                seqnr = seqnr,
                                data = ln
                            };
                            m_rec_pbsfiles.tblpbsfiles.Add(m_rec_pbsfile);
                        }
                    }

                    m_rec_pbsfiles.transmittime = DateTime.Now;
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

        public void ReadDirFraSFtp()
        {
            string homedir = m_sftp.RealPath(".", "");
            //  Open a directory on the server...
            string handle = m_sftp.OpenDir(m_rec_sftp.outbound);
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

        public string AddPbcnetRecords(string delsystem, int leveranceid, int pbsfilesid)
        {
            int antal;
            int pbcnetrecords;
            DateTime transmisionsdato;
            int idlev;
            string filename;
            string rec;

            pbcnetrecords = (from h in Program.dbData3060.tblpbsfiles
                             where h.pbsfilesid == pbsfilesid & (h.seqnr == 0 | h.seqnr == 9999)
                             select h).Count();

            if (pbcnetrecords == 0)
            {
                // Find antal records
                antal = (from h in Program.dbData3060.tblpbsfiles
                         where h.pbsfilesid == pbsfilesid & h.seqnr != 0 & h.seqnr != 9999
                         select h).Count();
                transmisionsdato = DateTime.Now;
                idlev = clsPbs.nextval("idlev");

                tblpbsfilename rec_pbsfiles = (from h in Program.dbData3060.tblpbsfilenames where h.id == pbsfilesid select h).First();

                rec = write00(delsystem, transmisionsdato, idlev, leveranceid);
                tblpbsfile rec_pbsfile = new tblpbsfile { seqnr = 0, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

                rec = write90(delsystem, transmisionsdato, idlev, leveranceid, antal);
                rec_pbsfile = new tblpbsfile { seqnr = 9999, data = rec };
                rec_pbsfiles.tblpbsfiles.Add(rec_pbsfile);

                Program.dbData3060.SubmitChanges();

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