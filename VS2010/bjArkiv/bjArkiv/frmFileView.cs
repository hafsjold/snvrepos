using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicNP.FileViewControl;
using System.IO;
using LogicNP.ShComboBoxControl;

namespace bjArkiv
{
    public partial class frmFileView : Form
    {
        private string arkivpath { get; set; }
        private string m_lastFolder;
        private bool m_lastFolderIsArkiv;

        public frmFileView()
        {
            InitializeComponent();
            arkivpath = string.Empty;
            m_lastFolder = string.Empty;
            m_lastFolderIsArkiv = false;
        }

        private void btnGoUp_Click(object sender, EventArgs e)
        {
            flView.GoUp();
        }

        private void flView_CurrentFolderChanged(object sender, EventArgs e)
        {
            //string columnName = flView.GetColumnName("IsFolder", -1); 

            //flView.SetColumnDisplayIndex("Date Modified", -1, 1);

            for (var i = 0; i < 6; i++)
            {
                Guid guidColumn = Guid.Empty;
                int pidColumn = 0;
                // Get GUID and pid for current column being added  
                // First two parameters are empty string and -1, which denotes 'current column' 
                flView.GetColumndIDFromColumn(string.Empty, i, ref guidColumn, ref pidColumn);
                int DisplayIndex = flView.GetColumnDisplayIndex(string.Empty, i);
                string colunmName = flView.GetColumnName(string.Empty, i);
            }

        }

        private void flView_BeforeColumnAdd(object sender, LogicNP.FileViewControl.ColumnAddEventArgs e)
        {
            // GUID and pid for 'Name' column 
            Guid guidNameColumn = new Guid("B725F130-47EF-101A-A5F1-02608C9EEBAC");
            int pidNameColumn = 10;

            Guid guidColumn = Guid.Empty;
            int pidColumn = 0;
            // Get GUID and pid for current column being added  
            // First two parameters are empty string and -1, which denotes 'current column' 
            flView.GetColumndIDFromColumn(string.Empty, -1, ref guidColumn, ref pidColumn);

            // If current column being added is NOT the 'Name' column, do not add it 
            if (!(guidColumn == guidNameColumn && pidColumn == pidNameColumn))
            {
                e.Cancel = true;
            }

        }

        private int testCash(string path)
        {
            string folder = new FileInfo(path).DirectoryName;
            if (folder.ToUpperInvariant() == m_lastFolder.ToUpperInvariant())
                if (m_lastFolderIsArkiv) return 1; // found arkiv
                else return 2; // found not Arkiv 
            else return 3; // not found
        }

        private void flView_AfterItemAdd(object sender, LogicNP.FileViewControl.FileViewEventArgs e)
        {
            if (e.Item.IsFolder()) return;
            if (e.Item.IsSpecialFolder(LogicNP.FileViewControl.SpecialFolders.DESKTOPDIRECTORY)) return;
            if (e.Item.IsCustom()) return;
            {
                string file = e.Item.Path;
                int test = testCash(file);
                if (test == 2) return;
                if (test == 3)
                {
                    m_lastFolder = new FileInfo(file).DirectoryName;
                    m_lastFolderIsArkiv = false;
                }
                clsArkiv arkiv = new clsArkiv();
                tbldoc rec = arkiv.GetMetadata(file);
                try
                {
                    e.Item.SetColumnText("Virksomhed", -1, rec.virksomhed);
                    e.Item.SetColumnText("Emne", -1, rec.emne);
                    e.Item.SetColumnText("Doktype", -1, rec.dokument_type);
                    e.Item.SetColumnText("År", -1, rec.år.ToString());
                    e.Item.SetColumnText("Ekstern kilde", -1, rec.ekstern_kilde);
                    e.Item.SetColumnText("Beskrivelse", -1, rec.beskrivelse);
                    if (test == 3) m_lastFolderIsArkiv = true;
                }
                catch
                {

                }

            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //IsbjArkiv(@"C:\Users\mha\Documents\mha_test_arkiv2\Svampeangreb0001.pdf");
        }

        private string GetbjArkiv(string path)
        {
            if (path.Length == 0) return string.Empty;
            DirectoryInfo curDI = null;
            DirectoryInfo di = null;
            try
            {
                di = new DirectoryInfo(path);
            }
            catch 
            {
                return string.Empty;
            }
            if (di.Exists)
                curDI = di;
            else if (di.Parent.Exists)
                curDI = di.Parent;
            if (curDI == null) return string.Empty;
            while (true)
            {
                if (curDI == null) return string.Empty;
                FileInfo fi = new FileInfo(curDI.FullName + Program.BJARKIV);
                if (fi.Exists) return curDI.FullName;
                curDI = curDI.Parent;
            }
        }

  
        private void flView_ItemDblClick(object sender, FileViewCancelEventArgs e)
        {
            if (e.Item.IsFolder()) { }
            arkivpath = GetbjArkiv(e.Item.Path);
            if (arkivpath == string.Empty)
            {
                flView.DeleteCustomColumn("Virksomhed");
                flView.DeleteCustomColumn("Emne");
                flView.DeleteCustomColumn("Doktype");
                flView.DeleteCustomColumn("År");
                flView.DeleteCustomColumn("Ekstern kilde");
                flView.DeleteCustomColumn("Beskrivelse");
            }
            else
            {
                flView.AddCustomColumn("Virksomhed", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Emne", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Doktype", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("År", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Ekstern kilde", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Beskrivelse", ColumnTextJustificationStyles.Left, 100);
            }
        }

        private void fldrView_NodeClick(object sender, LogicNP.FolderViewControl.FolderViewEventArgs e)
        {
            if (e.Node.IsFolder()) { }
            arkivpath = GetbjArkiv(e.Node.Path);
            if (arkivpath == string.Empty)
            {
                flView.DeleteCustomColumn("Virksomhed");
                flView.DeleteCustomColumn("Emne");
                flView.DeleteCustomColumn("Doktype");
                flView.DeleteCustomColumn("År");
                flView.DeleteCustomColumn("Ekstern kilde");
                flView.DeleteCustomColumn("Beskrivelse");
            }
            else
            {
                flView.AddCustomColumn("Virksomhed", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Emne", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Doktype", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("År", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Ekstern kilde", ColumnTextJustificationStyles.Left, 100);
                flView.AddCustomColumn("Beskrivelse", ColumnTextJustificationStyles.Left, 100);
            }
        }

    }
}
