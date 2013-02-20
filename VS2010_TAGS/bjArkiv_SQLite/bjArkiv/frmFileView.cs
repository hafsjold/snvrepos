﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicNP.FileViewControl;
using System.IO;

namespace bjArkiv
{
    public partial class frmFileView : Form
    {
        private string arkivpath { get; set; }
        private string m_lastFolder;
        private bool m_lastFolderIsArkiv;
        private Columns m_customColumns;
        private string m_lastFolderVisited;

        public bool IsArkivFolder
        {
            get
            {
                if (arkivpath == string.Empty)
                    return false;
                else
                    return true;
            }
        }

        public frmFileView()
        {

            InitializeComponent();
            if (global::bjArkiv.Properties.Settings.Default.frmFileViewColoumns == null)
                m_customColumns = Program.customColumns;
            else
            {
                m_customColumns = global::bjArkiv.Properties.Settings.Default.frmFileViewColoumns;
                foreach (string key in Program.customColumns.Keys)
                    if (!m_customColumns.ContainsKey(key))
                        m_customColumns[key] = Program.customColumns[key];
            }
            m_lastFolderVisited = global::bjArkiv.Properties.Settings.Default.lastFolderVisited;

            arkivpath = string.Empty;
            m_lastFolder = string.Empty;
            m_lastFolderIsArkiv = false;
 
            // bjArkivWatcher
            Program.bjArkivWatcher = new System.IO.FileSystemWatcher();
            Program.bjArkivWatcher.EnableRaisingEvents = false;
            Program.bjArkivWatcher.IncludeSubdirectories = true;
            Program.bjArkivWatcher.NotifyFilter = System.IO.NotifyFilters.FileName;
            Program.bjArkivWatcher.SynchronizingObject = this;
            Program.bjArkivWatcher.Created += new System.IO.FileSystemEventHandler(this.bjArkivWatcher_Created);
        }

        private void flView_CurrentFolderChanged(object sender, EventArgs e)
        {
            /*
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
            */
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
            string navn = e.ColumnName;
            if (!IsArkivFolder)
            {
                if (!Program.explorerColumns.Exists(guidColumn, pidColumn))
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if (!(guidColumn == guidNameColumn && pidColumn == pidNameColumn))
                {
                    e.Cancel = true;
                }
            }
        }

        private int testCash(string path)
        {
            string folder = string.Empty;
            try
            {
                folder = new FileInfo(path).DirectoryName;
            }
            catch
            {
                return 4; // not a valid file file
            }
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
                if (test == 4) return;
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
            if (e.Item.IsFolder())
            {

                m_lastFolderVisited = e.Item.Path;
                arkivpath = GetbjArkiv(e.Item.Path);
 
                if (!IsArkivFolder)
                {
                    setlabelPath(e.Item.Path, System.Drawing.SystemColors.Control);
                    m_customColumns.DeleteCustomColumn(flView);
                }
                else
                {
                    setlabelPath(e.Item.Path, System.Drawing.Color.Lime); 
                    m_customColumns.AddCustomColumn(flView);
                }
            }
        }

        private void fldrView_AfterSelect(object sender, LogicNP.FolderViewControl.FolderViewEventArgs e)
        {
            if (e.Node.IsFolder())
            {

                m_lastFolderVisited = e.Node.Path;
                arkivpath = GetbjArkiv(e.Node.Path);
 
                if (!IsArkivFolder)
                {
                    setlabelPath(e.Node.Path, System.Drawing.SystemColors.Control);
                    m_customColumns.DeleteCustomColumn(flView);
                }
                else
                {
                    setlabelPath(e.Node.Path, System.Drawing.Color.Lime); 
                    m_customColumns.AddCustomColumn(flView);
                }
            }
        }

        private void bjArkivWatcher_Created(object sender, FileSystemEventArgs e)
        {
            AddToArkiv(e.FullPath);
        }

        private void AddToArkiv(string file)
        {
            if (file.StartsWith(Program.bjArkivWatcher.Path + @"\.bja", StringComparison.CurrentCultureIgnoreCase))
                return;
            clsArkiv arkiv = new clsArkiv();
            arkiv.EditMetadata(file);
        }

        private void flView_PopupContextMenu(object sender, PopupContextMenuEventArgs e)
        {
            if (!e.BackgroundMenu) // background menu 
            {
                e.ShellMenu.InsertItem("Edit Metadata",1);
            }
        }

        private void flView_CustomContextMenuItemSelect(object sender, CustomContextMenuItemSelectEventArgs e)
        {
            if (e.Caption.Contains("Edit Metadata")) // custom menu item added to item menu 
            {
                // Write path of each selected item in separate lines
                foreach (ListItem item in flView.SelectedItems)
                {
                    if (item.IsFolder()) return;
                    string file = item.Path;
                    if (file.StartsWith(Program.bjArkivWatcher.Path + @"\.bja", StringComparison.CurrentCultureIgnoreCase))
                        return;
                    clsArkiv arkiv = new clsArkiv();
                    arkiv.EditMetadata(file);
                }
            }
        }

        private void frmFileView_FormClosing(object sender, FormClosingEventArgs e)
        {
            global::bjArkiv.Properties.Settings.Default.frmFileViewColoumns = m_customColumns;
            global::bjArkiv.Properties.Settings.Default.lastFolderVisited = m_lastFolderVisited;
            Properties.Settings.Default.Save();
        }

        private void opretNytArkivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();
            openFileDialog1.Description = "Vælg Arkiv Folder";
            openFileDialog1.SelectedPath = fldrView.SelectedNode.Path;
            openFileDialog1.ShowNewFolderButton = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string Databasefile = openFileDialog1.SelectedPath + Program.BJARKIV;
                clsArkiv arkiv = new clsArkiv();
                arkiv.createNewbjArkiv(Databasefile);
            }
        }

        private void afslutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flView_AfterFill(object sender, EventArgs e)
        {
            if (IsArkivFolder)
                m_customColumns.SetColoumnWidthAndDisplayindex(flView);
            else
                Program.explorerColumns.SetColoumnWidthAndDisplayindex(flView);
        }

        private void flView_BeforeFill(object sender, EventArgs e)
        {

        }

        private void frmFileView_Load(object sender, EventArgs e)
        {
            //flView.CurrentFolder = m_lastFolderVisited;
        }

        private void splitVertical_SplitterMoved(object sender, SplitterEventArgs e)
        {
            labelPath.Location = new Point(splitVertical.SplitterDistance + 5, labelPath.Location.Y);
        }

        private void setlabelPath(string path, System.Drawing.Color color)
        {
            if (path.StartsWith("::"))
                labelPath.Text = "";
            else
                labelPath.Text = path;

            labelPath.BackColor = color; // System.Drawing.Color.Red;
        }

     }
}