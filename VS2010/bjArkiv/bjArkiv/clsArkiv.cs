using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

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
                    frmAddDoc m_frmAddDoc = new frmAddDoc();
                    m_frmAddDoc.Ref_nr = (int)docrec.ref_nr;
                    m_frmAddDoc.Dokument = docrec.kilde_sti;
                    m_frmAddDoc.Virksomhed = docrec.virksomhed;
                    m_frmAddDoc.Emne = docrec.emne;
                    m_frmAddDoc.Dokument_type = docrec.dokument_type;
                    m_frmAddDoc.År = (int)docrec.år;
                    m_frmAddDoc.Ekstern_kilde = docrec.ekstern_kilde;
                    m_frmAddDoc.Beskrivelse = docrec.beskrivelse;
                    m_frmAddDoc.Oprettet_af = docrec.oprettes_af;
                    m_frmAddDoc.Oprettet_dato = (DateTime)docrec.oprettet_dato;
                    m_frmAddDoc.Opret = false;
                    DialogResult Result = m_frmAddDoc.ShowDialog();
                    if (Result == System.Windows.Forms.DialogResult.OK)
                    {
                        docrec.virksomhed = m_frmAddDoc.Virksomhed;
                        docrec.emne = m_frmAddDoc.Emne;
                        docrec.dokument_type = m_frmAddDoc.Dokument_type;
                        docrec.år = m_frmAddDoc.År;
                        docrec.ekstern_kilde = m_frmAddDoc.Ekstern_kilde;
                        docrec.beskrivelse = m_frmAddDoc.Beskrivelse;
                        docdb.Save();
                    }
                }
                catch
                {
                    frmAddDoc m_frmAddDoc = new frmAddDoc();
                    m_frmAddDoc.Dokument = getArkivLocalPath(pfile);
                    m_frmAddDoc.Opret = true;
                    DialogResult Result = m_frmAddDoc.ShowDialog();
                    if (Result == System.Windows.Forms.DialogResult.OK)
                    {
                        Guid id = Guid.NewGuid();
                        int ref_nr = 0;
                        try
                        {
                            ref_nr = (from n in docdb orderby n.ref_nr descending select n.ref_nr).First();
                        }
                        catch { }
                        ref_nr++;
 
                        docrec = new xmldoc
                        {
                            id = id,
                            ref_nr = ref_nr,
                            virksomhed = m_frmAddDoc.Virksomhed,
                            emne = m_frmAddDoc.Emne,
                            dokument_type = m_frmAddDoc.Dokument_type,
                            år = m_frmAddDoc.År,
                            ekstern_kilde = m_frmAddDoc.Ekstern_kilde,
                            beskrivelse = m_frmAddDoc.Beskrivelse,
                            oprettes_af = m_frmAddDoc.Oprettet_af,
                            oprettet_dato = m_frmAddDoc.Oprettet_dato,
                            kilde_sti = getArkivLocalPath(pfile)
                        };
                        docdb.Add(docrec);
                        docdb.Save();
                    }
                }
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
