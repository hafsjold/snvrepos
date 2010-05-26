﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;


// Add a Reference to COM Microsoft Access 11.0 Object Library (Access 2003)

namespace Backup3060
{
    public partial class frmBackup : Form
    {
        private object moMissing = System.Reflection.Missing.Value;
        private int selectedIndes = -1;
        private string regKey = @"Software\Hafsjold\Puls3060\Start";

        public frmBackup()
        {
            InitializeComponent();
            RegistryKey masterKey = Registry.CurrentUser.OpenSubKey(regKey);
            this.BackupDir.Text = (string)masterKey.GetValue("BackupDir");
            string[] folders = (string[])masterKey.GetValue("BackupFolders");
            foreach (string folder in folders)
            {
                this.lvwFolders.Items.Add(folder);
            }
            masterKey.Close();
        }

        private void frmBackup_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveReg();
        }
        
        private void SaveReg()
        {
            RegistryKey masterKey = Registry.CurrentUser.CreateSubKey(regKey);
            masterKey.SetValue("BackupDir", this.BackupDir.Text, RegistryValueKind.String);
            int iCount = this.lvwFolders.Items.Count;
            string[] folders = new string[iCount];
            for (int i = 0; i < iCount; i++)
            {
                folders[i] = this.lvwFolders.Items[i].Text;
            }
            masterKey.SetValue("BackupFolders", folders, RegistryValueKind.MultiString);
            masterKey.Close();
        }
        
        private void SelectBackupDir_Click(object sender, EventArgs e)
        {
            this.bf.SelectedPath = this.BackupDir.Text;
            this.bf.ShowNewFolderButton = true;
            this.bf.ShowDialog();
            this.BackupDir.Text = this.bf.SelectedPath;
        }

        private void SelectBackupFolder_Click(object sender, EventArgs e)
        {
            this.bf.SelectedPath = this.BackupFolder.Text;
            this.bf.ShowNewFolderButton = false;
            this.bf.ShowDialog();
            this.BackupFolder.Text = this.bf.SelectedPath;
        }
        
        private void SelectBackupFile_Click(object sender, EventArgs e)
        {
            this.bd.FileName = this.BackupFolder.Text;
            this.bd.ShowDialog();
            this.BackupFolder.Text = this.bd.FileName;
        }
        
        private void cmdAddFolderToList_Click(object sender, EventArgs e)
        {
            if (selectedIndes == -1)
            {
                if (this.BackupFolder.Text.Length > 0)
                {
                    this.lvwFolders.Items.Add(this.BackupFolder.Text);
                }
            }
            else
            {
                if (this.BackupFolder.Text.Length == 0)
                {
                    this.lvwFolders.Items[selectedIndes].Remove();
                }
                else
                {
                    this.lvwFolders.Items[selectedIndes].Text = this.BackupFolder.Text;
                }
            }
            this.BackupFolder.Clear();
            selectedIndes = -1;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            SaveReg();
            execZip();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void testmenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem Item;
            Item = this.lvwFolders.Items.Add("aaa");
        }

        private void lvwFolders_MouseClick(object sender, MouseEventArgs e)
        {
            ListView lv = (ListView)sender;
            string value = lv.FocusedItem.Text;
            selectedIndes = lv.FocusedItem.Index;
            this.BackupFolder.Text = lv.FocusedItem.Text;
            //int X = 20 + this.Location.X + lvwFolders.Location.X + e.X;
            //int Y = 40 + this.Location.Y + lvwFolders.Location.Y + e.Y;
            //this.contextMenuStrip1.Show(X,Y);
        }

        private void execZip()
        {
            System.Diagnostics.Process objProcess;

            RegistryKey masterKey = Registry.CurrentUser.OpenSubKey(regKey);
            string BackupDir = (string)masterKey.GetValue("BackupDir");
            DateTime Nu = DateTime.Now;
            string fdt = Nu.ToString("_yyyyMMdd_HHmm");
            string Target = BackupDir.TrimEnd('\\') + @"\SUMMA" + fdt + @".zip";
            string BkArg = @"-r -$ -S -9 -n zip """ + Target + @"""";
            string[] folders = (string[])masterKey.GetValue("BackupFolders");
            foreach (string folder in folders)
            {
                BkArg += @" """ + folder + @"""";
            }
            masterKey.Close();

            logdata(BkArg);

            try
            {
                objProcess = new System.Diagnostics.Process();
                objProcess.StartInfo.FileName = @"zip.exe";
                objProcess.StartInfo.Arguments = BkArg;
                objProcess.StartInfo.UseShellExecute = true;
                objProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                objProcess.Start();

                //Wait until the process passes back an exit code 
                objProcess.WaitForExit();

                //Read ExitCode
                int codeExit = objProcess.ExitCode;

                //Free resources associated with this process 
                objProcess.Close();
                if (codeExit == 0)
                {
                    WriteToEventLog("Backup3060 ended normaly", EventLogEntryType.Information, 49101);
                }
                else
                {
                    WriteToEventLog("Backup3060 Error-1", EventLogEntryType.Error, 49102);
                }
            }
            catch
            {
                WriteToEventLog("Backup3060 Error-2", EventLogEntryType.Error, 49103);
            }

        }

        public static bool WriteToEventLog(string Entry, EventLogEntryType EventType, int EventNumber)
        {
            string AppName = "Backup3060";
            string LogName = "Application";

            System.Diagnostics.EventLog objEventLog = new System.Diagnostics.EventLog();

            try
            {
                //Register the App as an Event Source 
                if (!EventLog.SourceExists(AppName))
                {
                    EventLog.CreateEventSource(AppName, LogName);
                }
                objEventLog.Source = AppName;

                //WriteEntry is overloaded; this is one 
                //of 10 ways to call it 
                objEventLog.WriteEntry(Entry, EventType, EventNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void logdata(string Data)
        {
            FileStream ts = new FileStream(@"Backup3060Log.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            using (StreamWriter sr = new StreamWriter(ts, Encoding.Default))
            {
                sr.WriteLine(Data);
            }

        }


    }
}