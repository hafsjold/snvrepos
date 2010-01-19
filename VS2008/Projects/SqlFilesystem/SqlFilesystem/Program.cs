﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;

namespace SqlFilesystem
{
    class Program
    {
        private static DbFilesystem m_db;
        static void Main(string[] args)
        {
            m_db = new DbFilesystem(@"C:\Documents and Settings\mha\Dokumenter\dbFilesystem.sdf");

            //TblRceoveryPoint rec_RceoveryPoint = setupRecoveryPoint();
            //createRecoveryPoint(rec_RceoveryPoint);

            TblRceoveryPoint rec_RestorePoint = setupRestorePoint();
            restoreRecoveryPoint(rec_RestorePoint );

            //deleteContentNotUsed();
        }

        private static TblRceoveryPoint setupRecoveryPoint()
        {
            TblRceoveryPoint rec_RceoveryPoint = new TblRceoveryPoint
            {
                Name = "MHATest",
                Rptime = DateTime.Now
            };
            m_db.TblRceoveryPoint.InsertOnSubmit(rec_RceoveryPoint);

            TblRecoveryPointLine rec_RecoveryPointLine = new TblRecoveryPointLine
            {
                Recoverypath = @"C:\Documents and Settings\mha\Application Data\SummaSummarum\4",
                Recoveryisfile = false
            };
            rec_RceoveryPoint.TblRecoveryPointLine.Add(rec_RecoveryPointLine);

            rec_RecoveryPointLine = new TblRecoveryPointLine
            {
                Recoverypath = @"C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\dbData3060.sdf",
                Recoveryisfile = true
            };
            rec_RceoveryPoint.TblRecoveryPointLine.Add(rec_RecoveryPointLine);
            m_db.SubmitChanges();
            return rec_RceoveryPoint;
        }

        private static void createRecoveryPoint(TblRceoveryPoint rec_RceoveryPoint)
        {
            rec_RceoveryPoint.Rptime = DateTime.Now;
            var qry_Recoverypointlines = from l in rec_RceoveryPoint.TblRecoveryPointLine select l;
            foreach (TblRecoveryPointLine rpl in qry_Recoverypointlines)
            {
                if (rpl.Recoveryisfile == true)
                {
                    backupfile(rpl, rpl.Recoverypath);

                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(rpl.Recoverypath);
                    foreach (var w in di.GetFiles())
                    {
                        backupfile(rpl, w.FullName);
                    }
                }
            }
        }

        private static void backupfile(TblRecoveryPointLine rec_RceoveryPointLine, string filepath)
        {
            string backupFilepath = filepath;

            FileInfo fi = new FileInfo(backupFilepath);

            //Directories ------------------------------------------------------------
            string[] strPaths = fi.DirectoryName.Split(new char[] { '\\' });
            TblDirectory per_rec_Directory;
            TblDirectory rec_Directory;
            TblDirectory rec_File;
            try
            {
                per_rec_Directory = (from d in m_db.TblDirectory where d.Name == strPaths[0] && d.Parentid == null select d).First();
            }
            catch (InvalidOperationException)
            {
                per_rec_Directory = new TblDirectory
                {

                    Name = strPaths[0],
                    Isfile = false
                };
                m_db.TblDirectory.InsertOnSubmit(per_rec_Directory);
                m_db.SubmitChanges();
            }

            for (int i = 1; i < strPaths.Length; i++)
            {
                try
                {
                    rec_Directory = (from d in per_rec_Directory.Dirindir where d.Name == strPaths[i] select d).First();
                }
                catch (InvalidOperationException)
                {

                    rec_Directory = new TblDirectory
                    {
                        Name = strPaths[i],
                        Isfile = false
                    };
                    per_rec_Directory.Dirindir.Add(rec_Directory);
                    m_db.SubmitChanges();
                }
                per_rec_Directory = rec_Directory;
            }

            //File ------------------------------------------------------------------
            try
            {
                rec_File = (from d in per_rec_Directory.Dirindir where d.Name == fi.Name select d).First();
            }
            catch (InvalidOperationException)
            {
                rec_File = new TblDirectory
                {
                    Name = fi.Name,
                    Isfile = true
                };
                per_rec_Directory.Dirindir.Add(rec_File);
                m_db.SubmitChanges();
            }

            //Content ---------------------------------------------------------------

            MD5 md5 = MD5.Create();
            StringBuilder sb = new StringBuilder();
            byte[] bData = File.ReadAllBytes(fi.FullName);
            foreach (byte b in md5.ComputeHash(bData)) sb.Append(b.ToString("x2").ToLower());


            TblContent rec_Content;
            try
            {
                rec_Content = (from d in rec_File.TblContent where d.Checksum == sb.ToString() select d).First();
            }
            catch (InvalidOperationException)
            {
                rec_Content = new TblContent
                {
                    Checksum = sb.ToString(),
                    Size = fi.Length,
                    Mtime = fi.LastWriteTime,
                    Atime = fi.LastAccessTime,
                    Data = ZipCompressStream(bData, fi.Name, fi.LastWriteTime)
                };
                rec_File.TblContent.Add(rec_Content);
                m_db.SubmitChanges();
            }

            //RecoveryPoint_Content -------------------------------------------------
            TblRecoveryPointContent rec_RecoveryPointContent = new TblRecoveryPointContent { };
            rec_Content.TblRecoveryPointContent.Add(rec_RecoveryPointContent);
            rec_RceoveryPointLine.TblRecoveryPointContent.Add(rec_RecoveryPointContent);
            m_db.SubmitChanges();
        }


        private static TblRceoveryPoint setupRestorePoint()
        {
            TblRceoveryPoint rec_RestorePoint = (from r in m_db.TblRceoveryPoint where r.Name == "MHATest" select r).First();
            var RestorePointLines = from l in rec_RestorePoint.TblRecoveryPointLine select l;
            foreach (TblRecoveryPointLine RecoveryPointLine in RestorePointLines)
            {
                switch (RecoveryPointLine.Recoverypath)
                {
                    case @"C:\Documents and Settings\mha\Application Data\SummaSummarum\4":
                        RecoveryPointLine.Restorepath = @"C:\Documents and Settings\mha\Dokumenter\SummaSummarum\4";
                        break;

                    case @"C:\Documents and Settings\mha\Dokumenter\Medlem3060\Databaser\SQLCompact\dbData3060.sdf":
                        RecoveryPointLine.Restorepath = @"C:\Documents and Settings\mha\Dokumenter\dbData3060.sdf";
                        break;

                    default:
                        RecoveryPointLine.Restorepath = RecoveryPointLine.Recoverypath;
                        break;
                }
            }
            return rec_RestorePoint;
        }

        static void restoreRecoveryPoint(TblRceoveryPoint rec_RestorePoint)
        {
            var RecoveryPointLines = from rp in rec_RestorePoint.TblRecoveryPointLine select rp;
            foreach (TblRecoveryPointLine RecoveryPointLine in RecoveryPointLines)
            {
                if (RecoveryPointLine.Recoveryisfile == true) //File restore
                {
                    restorefile(RecoveryPointLine);
                }
                else //Directory restore
                {
                    DirectoryInfo rdi = new DirectoryInfo(RecoveryPointLine.Restorepath);
                    if (!rdi.Exists)
                    {
                        rdi.Create();
                        restorefile(RecoveryPointLine);
                    }
                    else
                    {
                        string movetodir = rdi.Parent.FullName + @"\" + rdi.Name + String.Format("{0:_Backup_yyyyMMddHHmmss}", DateTime.Now);
                        rdi.MoveTo(movetodir);
                        rdi = new DirectoryInfo(RecoveryPointLine.Restorepath);
                        rdi.Create();
                        restorefile(RecoveryPointLine);
                        rdi = new DirectoryInfo(movetodir);
                        rdi.Delete(true);
                    }
                }
            }
        }

        private static void restorefile(TblRecoveryPointLine RecoveryPointLine)
        {
            var files = from rc in RecoveryPointLine.TblRecoveryPointContent
                        join c in m_db.TblContent on rc.Contentid equals c.Id
                        join f in m_db.TblDirectory on c.Directoryid equals f.Id
                        select new
                        {
                            RecoveryIsfile = RecoveryPointLine.Recoveryisfile,
                            RestorePath = RecoveryPointLine.Restorepath,
                            fileName = f.Name,
                            parent = f.Parentid,
                            Data = c.Data,
                            Mtime = c.Mtime,
                            Atime = c.Atime
                        };
            int antal = files.Count();
            foreach (var fil in files)
            {
                string RestoreFileName = fil.fileName;
                string RestorePathOnly;
                if (fil.RecoveryIsfile == true)
                {
                    FileInfo fi = new FileInfo(fil.RestorePath);
                    RestorePathOnly = fi.DirectoryName;
                }
                else
                {
                    RestorePathOnly = fil.RestorePath;
                }
                string RestorePathAndName = RestorePathOnly + @"\" + RestoreFileName;
                File.WriteAllBytes(RestorePathAndName, ZipUncompressStream(fil.Data.ToArray()));
                FileInfo nfi = new FileInfo(RestorePathAndName);
                nfi.LastWriteTime = fil.Mtime;
                nfi.LastAccessTime = fil.Atime;
            }
        }

        private static void deleteContentNotUsed()
        {
            var qry = from c in m_db.TblContent
                      join rpc in m_db.TblRecoveryPointContent on c.Id equals rpc.Contentid into jrpc
                      from x in jrpc.DefaultIfEmpty()
                      where x.Id == null
                      select c;

            foreach (var rec in qry)
            {
                int id = rec.Id;
                m_db.TblContent.DeleteOnSubmit(rec);
            }
            m_db.SubmitChanges();
        }

        private static byte[] ZipCompressStream(byte[] bData, string fileName, DateTime fileDate)
        {
            MemoryStream streamIm = new MemoryStream(bData);
            MemoryStream streamOut = new MemoryStream();
            ZipOutputStream zipOut = new ZipOutputStream(streamOut);
            ZipEntry entry = new ZipEntry(fileName);
            entry.DateTime = fileDate;
            entry.Size = streamIm.Length;
            zipOut.PutNextEntry(entry);
            zipOut.Write(streamIm.ToArray(), 0, (int)streamIm.Length);

            zipOut.Finish();
            zipOut.IsStreamOwner = false;
            zipOut.Close();
            return streamOut.ToArray();
        }

        private static byte[] ZipUncompressStream(byte[] bData)
        {
            MemoryStream streamIn = new MemoryStream(bData);
            MemoryStream streamOut = new MemoryStream();
            ZipInputStream zipIn = new ZipInputStream(streamIn);
            ZipEntry entry;
            entry = zipIn.GetNextEntry();
            long size = entry.Size;
            byte[] buffer = new byte[size];
            while (true)
            {
                size = zipIn.Read(buffer, 0, buffer.Length);
                if (size > 0) streamOut.Write(buffer, 0, (int)size);
                else break;
            }
            streamOut.Flush();
            return streamOut.ToArray();
        }
    }
}