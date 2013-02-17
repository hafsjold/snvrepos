using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;

namespace bjArkiv
{
    public class clsArkiv
    {
        public tbldoc rec { get; set; }
        private string file { get; set; }
        private string file_local { get { return ArkivLocalPath(file); } }
        private docdbliteEntities dblite { get; set; }
        private string arkivpath { get; set; }
        private string dbpath { get { return arkivpath + Program.BJARKIV; } }

        private string connectionString { get; set; }

        public clsArkiv()
        {

        }

        public tbldoc GetMetadata(string pfile)
        {
            file = pfile;
            if (OpenArkiv())
            {
                rec = null;
                try
                {
                    return (from doc in dblite.tbldoc where doc.kilde_sti.ToLower() == file_local.ToLower() select doc).First();
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
                rec = null;
                try
                {
                    rec = (from doc in dblite.tbldoc where doc.kilde_sti.ToLower() == file_local.ToLower() select doc).First();
                    frmAddDoc m_frmAddDoc = new frmAddDoc();
                    m_frmAddDoc.Ref_nr = (int)rec.ref_nr;
                    m_frmAddDoc.Dokument = rec.kilde_sti;
                    m_frmAddDoc.Virksomhed = rec.virksomhed;
                    m_frmAddDoc.Emne = rec.emne;
                    m_frmAddDoc.Dokument_type = rec.dokument_type;
                    m_frmAddDoc.År = (int)rec.år;
                    m_frmAddDoc.Ekstern_kilde = rec.ekstern_kilde;
                    m_frmAddDoc.Beskrivelse = rec.beskrivelse;
                    m_frmAddDoc.Oprettet_af = rec.oprettes_af;
                    m_frmAddDoc.Oprettet_dato = (DateTime)rec.oprettet_dato;
                    m_frmAddDoc.Opret = false;
                    DialogResult Result = m_frmAddDoc.ShowDialog();
                    if (Result == System.Windows.Forms.DialogResult.OK)
                    {
                        rec.virksomhed = m_frmAddDoc.Virksomhed;
                        rec.emne = m_frmAddDoc.Emne;
                        rec.dokument_type = m_frmAddDoc.Dokument_type;
                        rec.år = m_frmAddDoc.År;
                        rec.ekstern_kilde = m_frmAddDoc.Ekstern_kilde;
                        rec.beskrivelse = m_frmAddDoc.Beskrivelse;
                        dblite.SaveChanges();
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
                            tblrefnr rec_refnr = (from n in dblite.tblrefnr where n.keyname == "ref_nr" select n).First();
                            rec_refnr.nr++;
                            ref_nr = rec_refnr.nr;
                            dblite.SaveChanges();
                        }
                        catch
                        {
                            ref_nr = 1;
                            tblrefnr rec_refnr = new tblrefnr { keyname = "ref_nr", nr = ref_nr };
                            dblite.tblrefnr.AddObject(rec_refnr);
                            dblite.SaveChanges();
                        }

                        rec = new tbldoc
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
                        dblite.tbldoc.AddObject(rec);
                        dblite.SaveChanges();
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
            connectionString = @"metadata=res://*/dblite.csdl|res://*/dblite.ssdl|res://*/dblite.msl;provider=System.Data.SQLite;provider connection string='data source=""" + dbpath + @""" '";
            try
            {
                dblite = new docdbliteEntities(connectionString);
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
        
        private void CreateMissingFolders(DirectoryInfo di)
        {
            if (!di.Exists)
            {
                CreateMissingFolders(di.Parent);
                di.Create();
            }
        }

        public bool createNewbjArkiv(string DatabaseFile)
        {
            FileInfo DatabasefileInfo = new FileInfo(DatabaseFile);
            CreateMissingFolders(DatabasefileInfo.Directory);

            if (!DatabasefileInfo.Exists)
            {
                SQLiteConnection.CreateFile(DatabaseFile);
                string datasource = "Data Source=" + DatabaseFile + ";Version=3";
                SQLiteConnection conn = new SQLiteConnection(datasource);
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                "CREATE TABLE [tbldoc] ( " +
                "  [id] GUID NOT NULL, " +
                "  [ref_nr] INT, " +
                "  [virksomhed] NVARCHAR(50), " +
                "  [emne] VARCHAR(50), " +
                "  [dokument_type] VARCHAR(50), " +
                "  [år] INT, " +
                "  [ekstern_kilde] VARCHAR(50), " +
                "  [beskrivelse] NVARCHAR(100), " +
                "  [oprettes_af] VARCHAR(25), " +
                "  [oprettet_dato] DATETIME, " +
                "  [kilde_sti] NVARCHAR(512), " +
                "  CONSTRAINT [] PRIMARY KEY ([id]) ON CONFLICT ROLLBACK); " +
                " " +
                " " +
                "CREATE TABLE [tblrefnr] ( " +
                "  [keyname] NVARCHAR(10) NOT NULL, " +
                "  [nr] INT NOT NULL DEFAULT (0), " +
                "  CONSTRAINT [] PRIMARY KEY ([keyname]));";
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            return false;
        }

    }
}
