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
        public xmldoc docrec { get; set; }
        private xmldocs docdb { get; set; }
        private string XmlFilepath { get { return arkivpath + Program.BJARKIV; } }
        private string file { get; set; }
        private string file_local { get { return ArkivLocalPath(file); } }
        private string arkivpath { get; set; }

        public clsArkiv()
        {

        }

         private bool OpenArkiv()
        {
            FileInfo fi;
            try
            {
                fi = new FileInfo(file);
            }
            catch
            {
                return false;
            }

            if (!fi.Exists) return false;
            DirectoryInfo di = fi.Directory;
            if (!dbPathExist(di)) return false;

            try
            {
                docdb = new xmldocs(XmlFilepath);
                Program.bjArkivWatcher.Path = arkivpath;
                Program.bjArkivWatcher.EnableRaisingEvents = true;
                return true;
            }
            catch
            {
                Program.bjArkivWatcher.EnableRaisingEvents = false;
                return false;
            }
        }

        public xmldoc GetMetadata(string pfile)
        {
            file = pfile;
            if (OpenArkiv())
            {
                docrec = null;
                try
                {
                    return (from doc in docdb where doc.kilde_sti.ToLower() == file_local.ToLower() select doc).First();
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
            file = pfile;
            if (OpenArkiv())
            {
                docrec = null;
                try
                {
                    docrec = (from doc in docdb where doc.kilde_sti.ToLower() == file_local.ToLower() select doc).First();
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
                        docdb.SaveChanges();
                    }
                }
                catch
                {
                    frmAddDoc m_frmAddDoc = new frmAddDoc();
                    m_frmAddDoc.Dokument = file_local;
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
                            kilde_sti = file_local
                        };
                        docdb.Add(docrec);
                        docdb.SaveChanges();
                    }
                }
            }
            else
            {
                string messageBoxText = "Der findes ikke noget Arkiv til: " + file;
                string caption = "bjArkiv";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }

            return false;
        }


        private bool dbPathExist(DirectoryInfo di)
        {
            if (!di.Exists) return false;
            FileInfo fi = new FileInfo(di.FullName + Program.BJARKIV);
            if (!fi.Exists)
            {
                DirectoryInfo pdi = di.Parent;
                if (pdi == null) return false;
                if (!pdi.Exists) return false;
                return dbPathExist(pdi);
            }
            else
            {
                arkivpath = di.FullName;
                return true;
            }
        }

        private string ArkivLocalPath(string path)
        {
            if (file.StartsWith(arkivpath, StringComparison.CurrentCultureIgnoreCase))
                return file.Substring(arkivpath.Length + 1);
            else
                return "";
        }
 
        public static bool createNewbjArkiv(string DatabaseFile)
        {
            FileInfo DatabasefileInfo = new FileInfo(DatabaseFile);
            CreateMissingFolders(DatabasefileInfo.Directory);

            DirectoryInfo di = DatabasefileInfo.Directory;
            di.Attributes |= FileAttributes.Hidden;
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
