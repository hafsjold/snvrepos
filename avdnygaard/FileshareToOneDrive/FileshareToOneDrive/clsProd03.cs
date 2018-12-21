using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileshareToOneDrive
{
    public class clsProd03
    {
        private string m_sourceStartDir = @"\\SYNOLOGY_N\KLIENTRELATERET\"; //OLD

        private int m_totfiles = 0;
        private int m_ifile = 0;
        private int m_totfolders = 0;
        private int m_ifolder = 0;
        private string m_targetSiteId = string.Empty;
        private string m_targetUser = string.Empty;

        public async Task CreateFoldersAsync(clsServer recServer)
        {
            try
            {
                if (recServer.spSiteType == "SharePoint")
                {
                    var site = await clsContent.graphClient.Sites.GetByPath(recServer.spSite, "advnygaard.sharepoint.com").Request().GetAsync();
                    m_targetSiteId = site.Id;
                }
                else
                {
                    m_targetUser = recServer.spSite;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                clsContent.logger.Error(e.Message);
            }



            var qryfiles = clsContent.m_db.tblPath.Local.Where(f => f.serverPathType == "Folder").Where(f => f.spSite == recServer.spSite).Where(f => f.build == true).Where(f => f.error == null).OrderBy(f => f.serverPath);
            m_totfolders = qryfiles.Count();
            m_ifolder = 0;
            foreach (var rec in qryfiles)
            {
                m_ifolder++;
                clsContent.RenewAccessToken().Wait();
                var tt = (int)DateTime.Now.Subtract(clsContent.TokenAcquired).TotalSeconds;

                GetspPath(rec);

                DriveItem item;
                try
                {
                    if (recServer.spSiteType == "SharePoint")
                    {
                        item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(rec.spPath).Request().GetAsync();
                    }
                    else // OneDrive
                    {
                        item = await clsContent.graphClient.Users[m_targetUser].Drive.Root.ItemWithPath(rec.spPath).Request().GetAsync();
                    }
                    Console.WriteLine(string.Format("{0}-{1}/{2} Exists: {3} {4}", tt, m_ifolder, m_totfolders, recServer.spSite, rec.spPath));
                    rec.spPathExists = true;
                    rec.build = false;
                    rec.error = null;
                }
                catch (ServiceException e)
                {
                    Console.WriteLine(string.Format("{0}-{1}/{2} {3} {4}", tt, m_ifolder, m_totfolders, recServer.spSite, rec.spPath));
                    char[] sp = { '/' };
                    string[] arrPath = rec.spPath.Split(sp);
                    var arrCount = arrPath.Count();
                    var arrPathLast = arrPath[arrCount - 1];

                    DriveItem myfolder1 = new DriveItem()
                    {
                        Name = arrPathLast,
                        Folder = new Folder(),
                        File = null
                    };

                    try
                    {
                        if (recServer.spSiteType == "SharePoint")
                        {
                            if (arrCount == 1)
                            {
                                item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.Children.Request().AddAsync(myfolder1);
                            }
                            else
                            {
                                var arrPathFirsts = Path.GetDirectoryName(rec.spPath);
                                item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(arrPathFirsts).Children.Request().AddAsync(myfolder1);
                            }
                        }
                        else // OneDrive
                        {
                            if (arrCount == 1)
                            {
                                item = await clsContent.graphClient.Users[m_targetUser].Drive.Root.Children.Request().AddAsync(myfolder1);
                            }
                            else
                            {
                                var arrPathFirsts = Path.GetDirectoryName(rec.spPath);
                                item = await clsContent.graphClient.Users[m_targetUser].Drive.Root.ItemWithPath(arrPathFirsts).Children.Request().AddAsync(myfolder1);
                            }
                        }
                        rec.spPathExists = true;
                        rec.build = false;
                        rec.error = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        clsContent.logger.Error(rec.spPath);
                        clsContent.logger.Error(ex.Message);
                        rec.spPathExists = false;
                        rec.error = ex.Message;
                    }
                }
                clsContent.m_db.SaveChanges();
            }
        }

        public async Task CreateFilesAsync(clsServer recServer)
        {
            try
            {
                if (recServer.spSiteType == "SharePoint")
                {
                    var site = await clsContent.graphClient.Sites.GetByPath(recServer.spSite, "advnygaard.sharepoint.com").Request().GetAsync();
                    m_targetSiteId = site.Id;
                }
                else
                {
                    m_targetUser = recServer.spSite;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                clsContent.logger.Error(e.Message);
            }


            var qryfiles = clsContent.m_db.tblPath.Local.Where(f => f.serverPathType == "File").Where(f => f.spSite == recServer.spSite).Where(f => f.build == true).Where(f => f.error == null).OrderBy(f => f.serverPath);
            //var qryfiles = clsContent.m_db.tblPath.Local.Where(f => f.serverPathType == "File").Where(f => f.spSite == recServer.spSite).Where(f => f.error != null).Where(f => f.spPathExists != true).OrderBy(f => f.serverPath);
            m_totfiles = qryfiles.Count();
            m_ifile = 0;
            foreach (var rec in qryfiles)
            {
                m_ifile++;
                clsContent.RenewAccessToken().Wait();
                var tt = (int)DateTime.Now.Subtract(clsContent.TokenAcquired).TotalSeconds;

                GetspPath(rec);

                try
                {
                    if (recServer.spSiteType == "SharePoint")
                    {
                        var item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(rec.spPath).Request().GetAsync();
                    }
                    else // OneDrive
                    {
                        var item = await clsContent.graphClient.Users[m_targetUser].Drive.Root.ItemWithPath(rec.spPath).Request().GetAsync();
                    }
                    Console.WriteLine(string.Format("{0}-{1}/{2} Exists: {3} {4}", tt, m_ifile, m_totfiles, recServer.spSite, rec.spPath));
                    rec.spPathExists = true;
                    rec.build = false;
                }
                catch (ServiceException e)
                {
                    Console.WriteLine(string.Format("{0}-{1}/{2} {3} {4}", tt, m_ifile, m_totfiles, recServer.spSite, rec.spPath));
                    UploadSession uploadSession = null;
                    try
                    {
                        var localPath = rec.serverPath.Replace(rec.serverDrev, rec.localDrevLetter + @":\");
                        using (FileStream stream = System.IO.File.Open(localPath, FileMode.Open, FileAccess.Read))
                        {
                            try
                            {
                                if (recServer.spSiteType == "SharePoint")
                                {
                                    uploadSession = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(rec.spPath).CreateUploadSession().Request().PostAsync();
                                }
                                else
                                {
                                    uploadSession = await clsContent.graphClient.Users[m_targetUser].Drive.Root.ItemWithPath(rec.spPath).CreateUploadSession().Request().PostAsync();
                                }
                                var maxChunkSize = 32 * 320 * 1024; // 10 MB - Change this to your chunk size. 5MB is the default.
                                var provider = new ChunkedUploadProvider(uploadSession, clsContent.graphClient, stream, maxChunkSize);

                                // Setup the chunk request necessities
                                var chunkRequests = provider.GetUploadChunkRequests();
                                var readBuffer = new byte[maxChunkSize];
                                var trackedExceptions = new List<Exception>();
                                DriveItem itemResult = null;

                                //upload the chunks
                                foreach (var request in chunkRequests)
                                {
                                    // Do your updates here: update progress bar, etc.
                                    // ...
                                    // Send chunk request
                                    var result = await provider.GetChunkRequestResponseAsync(request, readBuffer, trackedExceptions);

                                    if (result.UploadSucceeded)
                                    {
                                        itemResult = result.ItemResponse;
                                        rec.spPathExists = true;
                                        rec.build = false;
                                        rec.error = null;
                                    }
                                }

                                // Check that upload succeeded
                                if (itemResult == null)
                                {
                                    Console.WriteLine("Upload failed: " + rec.spPath);
                                    clsContent.logger.Error("Upload failed: " + rec.spPath);
                                    rec.spPathExists = false;
                                    rec.error = "Upload failed";
                                }

                            }
                            catch (ServiceException ex)
                            {
                                Console.WriteLine(ex.Message);
                                clsContent.logger.Error(rec.spPath);
                                clsContent.logger.Error(ex.Message);
                                rec.spPathExists = false;
                                rec.error = ex.Message;
                            }
                        }
                    }
                    catch (Exception ez)
                    {
                        Console.WriteLine(ez.Message);
                        clsContent.logger.Error(rec.spPath);
                        clsContent.logger.Error(ez.Message);
                        rec.spPathExists = false;
                        rec.error = ez.Message;
                    }
                }
                clsContent.m_db.SaveChanges();
            }
        }

        private void GetspPath(tblPath rec)
        {
            var spPath = rec.serverPath.Substring(rec.serverDrev.Length).Replace(@"\", @"/");

            if (spPath.Contains(@"#"))
                rec.spPathRenamed = true;
            else
                rec.spPathRenamed = false;

            spPath = spPath.Replace(@"#", @"_");

            var spDirectory = Path.GetDirectoryName(spPath);
            if ((spPath.Length < 260) && (spDirectory.Length < 248))
            {
                rec.spPath = spPath;
                return;
            }

            if (spDirectory.Length < 248)
            {
                var spFile = Path.GetFileNameWithoutExtension(spPath);
                var spExt = Path.GetExtension(spPath);
                int maxLen = 259 - spDirectory.Length - spExt.Length - 1;
                rec.spPath = spDirectory + "/" + spFile.Substring(0, maxLen) + "." + spExt;
                rec.spPathRenamed = true;
                return;
            }

            rec.spPath = spPath;
        }

        //**********************************************************************************************************
        //**********************************************************************************************************
        public async Task GetSiteAsync()
        {
            try
            {
                var site = await clsContent.graphClient.Sites.GetByPath("/klient", "advnygaard.sharepoint.com").Request().GetAsync();
                m_targetSiteId = site.Id;
            }
            catch (Exception e) { }
        }
        public async Task CreateFoldersAsyncOLD()
        {
            var folders = System.IO.Directory.GetDirectories(m_sourceStartDir, "*", System.IO.SearchOption.AllDirectories);
            m_totfolders = folders.Length;
            List<string> folder1 = new List<string>();
            foreach (var s in folders)
            {
                if (s.ToUpperInvariant().StartsWith(m_sourceStartDir))
                {
                    var s2 = s.Substring(m_sourceStartDir.Length).Replace(@"\", @"/").Replace(@"#", @"_");
                    folder1.Add(s2);
                }
            }
            folder1.Sort();
            foreach (var f in folder1)
            {
                clsContent.RenewAccessToken().Wait();
                m_ifolder++;
                await CreateFolderAsyncOLD(f);
            }
        }
        public async Task CreateFolderAsyncOLD(string pPath)
        {
            DriveItem item = null;
            var tt = (int)DateTime.Now.Subtract(clsContent.TokenAcquired).TotalSeconds;
            try
            {
                item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(pPath).Request().GetAsync();
                Console.WriteLine(string.Format("{0}-{1}/{2} Exists: {3}", tt, m_ifolder, m_totfolders, pPath));
            }
            catch (ServiceException e)
            {
                Console.WriteLine(string.Format("{0}-{1}/{2} {3}", tt, m_ifolder, m_totfolders, pPath));
                char[] sp = { '/' };
                string[] arrPath = pPath.Split(sp);
                var arrCount = arrPath.Count();
                var arrPathLast = arrPath[arrCount - 1];
                //Dictionary<string, object> additionalData = new Dictionary<string, object>();
                //additionalData.Add("kunde", "Kunde 1234");
                DriveItem myfolder1 = new DriveItem()
                {
                    Name = arrPathLast,
                    Folder = new Folder(),
                    File = null,
                    //AdditionalData = additionalData                    
                };

                try
                {
                    if (arrCount == 1)
                    {
                        item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.Children.Request().AddAsync(myfolder1);
                    }
                    else
                    {
                        var arrPathFirsts = Path.GetDirectoryName(pPath);
                        item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(arrPathFirsts).Children.Request().AddAsync(myfolder1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    clsContent.logger.Error(pPath);
                    clsContent.logger.Error(ex.Message);
                    return;
                }
            }
        }
        public async Task CreateFilesAsyncOLD()
        {
            var files = System.IO.Directory.GetFiles(m_sourceStartDir, "*", System.IO.SearchOption.AllDirectories);
            m_totfiles = files.Length;
            Dictionary<string, string> file1 = new Dictionary<string, string>();

            foreach (var s in files)
            {
                if (s.ToUpperInvariant().StartsWith(m_sourceStartDir))
                {
                    var s2 = s.Substring(m_sourceStartDir.Length).Replace(@"\", @"/").Replace(@"#", @"_");
                    file1.Add(s, s2);
                }
            }

            m_ifile = 0;
            foreach (var f in file1.OrderBy(key => key.Value))
            {
                clsContent.RenewAccessToken().Wait();
                m_ifile++;
                await CreateFileAsyncOLD(f);
            }

        }
        public async Task CreateFileAsyncOLD(KeyValuePair<string, string> f)
        {
            var sPath = f.Key;
            var tPath = f.Value;
            var tt = (int)DateTime.Now.Subtract(clsContent.TokenAcquired).TotalSeconds;

            tblPath rec = new tblPath()
            {
                serverDrev = m_sourceStartDir,
                serverPath = sPath,
                spSite = "/klient",
                spPath = tPath,
            };

            try
            {
                var item = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(tPath).Request().GetAsync();
                Console.WriteLine(string.Format("{0}-{1}/{2} Exists: {3}", tt, m_ifile, m_totfiles, tPath));
                rec.spPathExists = true;
            }
            catch (ServiceException e)
            {
                Console.WriteLine(string.Format("{0}-{1}/{2} {3}", tt, m_ifile, m_totfiles, tPath));
                UploadSession uploadSession = null;
                try
                {
                    using (FileStream stream = System.IO.File.Open(sPath, FileMode.Open, FileAccess.Read))
                    {
                        try
                        {
                            uploadSession = await clsContent.graphClient.Sites[m_targetSiteId].Drive.Root.ItemWithPath(tPath).CreateUploadSession().Request().PostAsync();

                            var maxChunkSize = 32 * 320 * 1024; // 10 MB - Change this to your chunk size. 5MB is the default.
                            var provider = new ChunkedUploadProvider(uploadSession, clsContent.graphClient, stream, maxChunkSize);

                            // Setup the chunk request necessities
                            var chunkRequests = provider.GetUploadChunkRequests();
                            var readBuffer = new byte[maxChunkSize];
                            var trackedExceptions = new List<Exception>();
                            DriveItem itemResult = null;

                            //upload the chunks
                            foreach (var request in chunkRequests)
                            {
                                // Do your updates here: update progress bar, etc.
                                // ...
                                // Send chunk request
                                var result = await provider.GetChunkRequestResponseAsync(request, readBuffer, trackedExceptions);

                                if (result.UploadSucceeded)
                                {
                                    itemResult = result.ItemResponse;
                                    rec.spPathExists = true;
                                }
                            }

                            // Check that upload succeeded
                            if (itemResult == null)
                            {
                                Console.WriteLine("Upload failed: " + tPath);
                                clsContent.logger.Error("Upload failed: " + tPath);
                                rec.spPathExists = false;
                                rec.error = "Upload failed";
                            }

                        }
                        catch (ServiceException ex)
                        {
                            Console.WriteLine(ex.Message);
                            clsContent.logger.Error(tPath);
                            clsContent.logger.Error(ex.Message);
                            rec.spPathExists = false;
                            rec.error = ex.Message;
                        }
                    }
                }
                catch (Exception ez)
                {
                    Console.WriteLine(ez.Message);
                    clsContent.logger.Error(tPath);
                    clsContent.logger.Error(ez.Message);
                    rec.spPathExists = false;
                    rec.error = ez.Message;
                }
            }
            clsContent.m_db.tblPath.Local.Add(rec);
            clsContent.m_db.SaveChanges();


        }

    }
}
