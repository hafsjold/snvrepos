using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Security.Principal;

namespace bjArkiv
{
    public class clsArkiv
    {
        public xmldocs docdb { get; set; }

        public bool OpenArkiv(string pfile)
        {
            string stDbFolderpath = getDbFolderpath(pfile);
            if (stDbFolderpath == string.Empty) return false;

            //Arkiv allready open ? 
            if (docdb != null)
                if (docdb.path == (stDbFolderpath + Program.BJARKIV))
                {
                    return true;
                }
            try
            {
                docdb = xmldocs.Load(stDbFolderpath + Program.BJARKIV);

                Program.bjArkivWatcher.Path = stDbFolderpath;
                Program.bjArkivWatcher.EnableRaisingEvents = true;
                return true;
            }
            catch
            {
                Program.bjArkivWatcher.EnableRaisingEvents = false;
                return false;
            }
        }

        private bool? isFolder(string pfile)
        {
            DirectoryInfo di = null;
            try
            {
                di = new DirectoryInfo(pfile);
            }
            catch { }
            if (di != null && di.Exists) return true;
            FileInfo fi = null;
            try
            {
                fi = new FileInfo(pfile);
            }
            catch { }
            if (fi != null && fi.Exists) return false;
            return null;
        }


        private string getDbFolderpath(string pfile)
        {
            string dir = null;
            switch (isFolder(pfile))
            {
                case null:
                    return null;

                case false:
                    try
                    {
                        dir = new FileInfo(pfile).DirectoryName;
                    }
                    catch
                    {
                        return null;
                    }
                    break;

                case true:
                    dir = pfile;
                    break;
            }
            DirectoryInfo di = new DirectoryInfo(dir);
            if (!di.Exists) return string.Empty;

            FileInfo fi = new FileInfo(di.FullName + Program.BJARKIV);
            if (!fi.Exists)
            {
                DirectoryInfo pdi = di.Parent;
                if (pdi == null) return string.Empty;
                if (!pdi.Exists) return string.Empty;
                return getDbFolderpath(pdi.FullName);
            }
            else
            {
                return di.FullName;
            }
        }


        private string getArkivLocalPath(string path)
        {
            string stDbFolderpath = getDbFolderpath(path);
            if (stDbFolderpath == string.Empty) return "";
            if (path.StartsWith(stDbFolderpath, StringComparison.CurrentCultureIgnoreCase))
                return path.Substring(stDbFolderpath.Length + 1);
            else
                return "";
        }

        public xmldoc GetMetadata(string pfile)
        {

            if (OpenArkiv(pfile))
            {
                try
                {
                    return (from doc in docdb where doc.kilde_sti.ToLower() == getArkivLocalPath(pfile).ToLower() select doc).First();
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public bool EditMetadata(string pfile)
        {
            if (OpenArkiv(pfile))
            {
                xmldoc docrec = null;
                try
                {
                    docrec = (from doc in docdb where doc.kilde_sti.ToLower() == getArkivLocalPath(pfile).ToLower() select doc).First();
                }
                catch
                {
                    Guid id = Guid.NewGuid();
                    int ref_nr = 0;
                    try
                    {
                        ref_nr = (from n in docdb orderby n.ref_nr descending select n.ref_nr).First();
                    }
                    catch { }
                    ref_nr++;
                    AppDomain appDomain = Thread.GetDomain();
                    appDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                    WindowsPrincipal windowsPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
                    string Oprettet_af = windowsPrincipal.Identity.Name;

                    DateTime Oprettet_dato = DateTime.Now;

                    docrec = new xmldoc
                    {
                        id = id,
                        ref_nr = ref_nr,
                        oprettes_af = Oprettet_af,
                        oprettet_dato = Oprettet_dato,
                        kilde_sti = getArkivLocalPath(pfile)
                    };
                    docdb.Add(docrec);
                }
                if (true)
                {
                    frmUpdDoc m_frmUpdDoc = new frmUpdDoc();
                    m_frmUpdDoc.arkiv = this;
                    m_frmUpdDoc.startrec = docrec;
                    DialogResult Result = m_frmUpdDoc.ShowDialog(); 
                }
                else
                {
                    frmAddDoc m_frmAddDoc = new frmAddDoc();
                    m_frmAddDoc.arkiv = this;
                    m_frmAddDoc.startrec = docrec;
                    DialogResult Result = m_frmAddDoc.ShowDialog();
                }
                docdb.Save();
            }
            else
            {
                string messageBoxText = "Der findes ikke noget Arkiv til: " + pfile;
                string caption = "bjArkiv";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }

            return false;
        }

        public static bool createNewbjArkiv(string DatabaseFile)
        {
            FileInfo DatabasefileInfo = new FileInfo(DatabaseFile);
            CreateMissingFolders(DatabasefileInfo.Directory);

            DirectoryInfo di = DatabasefileInfo.Directory;
            di.Attributes |= FileAttributes.Hidden;
            try
            {
                using (FileStream fs = new FileStream(DatabaseFile, FileMode.CreateNew))
                {
                    using (StreamWriter wt = new StreamWriter(fs, UTF8Encoding.UTF8))
                    {
                        string txt = "<?xml version=\"1.0\"?><xmldocs/>";
                        wt.Write(txt);
                    }
                }
            }
            catch { }
            return true;
        }

        private static void CreateMissingFolders(DirectoryInfo di)
        {
            if (!di.Exists)
            {
                CreateMissingFolders(di.Parent);
                di.Create();
            }
        }
    }
}
