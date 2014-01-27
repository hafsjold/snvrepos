using System;
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
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;




namespace Backup3060
{
    public partial class frmBackup : Form
    {
        private int selectedIndes = -1;
        private string regKey = @"Software\Hafsjold\Puls3060\Start";

        public frmBackup()
        {
            InitializeComponent();
            RegistryKey masterKey = Registry.CurrentUser.OpenSubKey(regKey);
            if (masterKey == null)
            {
                RegistryKey masterKeyCreate = Registry.CurrentUser.OpenSubKey(@"Software", true);
                masterKey = masterKeyCreate.CreateSubKey(@"Hafsjold\Puls3060\Start");
            }
            this.BackupDir.Text = (string)masterKey.GetValue("BackupDir", "");
            string[] folders = (string[])masterKey.GetValue("BackupFolders");
            if (folders != null)
            {
                foreach (string folder in folders)
                {
                    this.lvwFolders.Items.Add(folder);
                }
            }

            int bBackupDatabase = (int)masterKey.GetValue("BackupDatabase", 0);
            this.BackupDatabase.Checked = bBackupDatabase == 0 ? false : true;

            this.DBBackupFolder.Text = (string)masterKey.GetValue("DBBackupFolder", "");
            this.SQLServer.Text = (string)masterKey.GetValue("SQLServer", "");
            this.Database.Text = (string)masterKey.GetValue("Database", "");
            masterKey.Close();
        }

        private void ExportDB1()
        {
            string strDatabase = @"dbPuls3060Medlem";
            string strDatabaseBackupfile = this.DBBackupFolder.Text + @"\" + strDatabase + @".bacpac";
            string ConnectionString = @"Server=qynhbd9h4f.database.windows.net;Database=dbPuls3060Medlem;User ID=sqlUser;Password=Puls3060;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;";
            DateTime Nu = DateTime.Now; 
            string fdt = Nu.ToString("dd-MM-yyyy HH:mm"); 
            try
            {
                Microsoft.SqlServer.Dac.DacServices Services = new Microsoft.SqlServer.Dac.DacServices(ConnectionString);
                Services.ExportBacpac(strDatabaseBackupfile, @"dbPuls3060Medlem");
                this.DBMessage2.Text = strDatabase + " Backup OK " + fdt;
                this.DBMessage2.ForeColor = System.Drawing.Color.DodgerBlue;
            }
            catch 
            {
                this.DBMessage2.Text = strDatabase + " Not Backed up " + fdt;
                this.DBMessage2.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void ExportDB2()
        {
            string strSQLServer = this.SQLServer.Text;  //@"(localdb)\localdb";
            string strDatabase = this.Database.Text; ;
            string strDatabaseBackupfile = this.DBBackupFolder.Text + @"\" + strDatabase + @".bacpac";
            string ConnectionString = @"Server=" + strSQLServer + @";Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;";
            DateTime Nu = DateTime.Now;
            string fdt = Nu.ToString("dd-MM-yyyy HH:mm");
            try
            {
                Microsoft.SqlServer.Dac.DacServices Services = new Microsoft.SqlServer.Dac.DacServices(ConnectionString);
                Services.ExportBacpac(strDatabaseBackupfile, strDatabase);
                this.DBMessage.Text = strDatabase + " Backup OK " + fdt;
                this.DBMessage.ForeColor = System.Drawing.Color.DodgerBlue;
            }
            catch
            {
                this.DBMessage.Text = strDatabase + " Not Backed up " + fdt;
                this.DBMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void DB_Restore()
        {

            /*
            RESTORE DATABASE dbDataTransSummaCopy
               FROM DISK = 'C:\Users\mha\Documents\WORK\dbDataTransSumma_Backup.bak'
               WITH MOVE 'dbDataTransSumma' TO 'C:\Users\mha\Documents\WORK\dbDataTransSummaCopy.MDF',
                  MOVE 'dbDataTransSumma_log' TO 'C:\Users\mha\Documents\WORK\dbDataTransSummaCopy_log.LDF', REPLACE;
            */
            String databaseNameNew = @"dbDataTransSummaCopy2";
            String databaseNameFrom = @"dbDataTransSumma";
            String filePath = @"C:\Users\mha\Documents\WORK\dbDataTransSumma_Backup.bak";
            String serverName = @"(localdb)\localdb";
            String dataFilePath = @"C:\Users\mha\Documents\WORK";
            String logFilePath = @"C:\Users\mha\Documents\WORK";

            // Create Restore instance
            Restore sqlRestore = new Restore();
            BackupDeviceItem deviceItem = new BackupDeviceItem(filePath, DeviceType.File);
            sqlRestore.Devices.Add(deviceItem);
            sqlRestore.Database = databaseNameNew;

            // Connect to DB Server
            SqlConnection sqlCon = new SqlConnection(@"Data Source=" + serverName + @";Integrated Security=True;");
            ServerConnection connection = new ServerConnection(sqlCon);

            // Restoring
            Server sqlServer = new Server(connection);
            Database db = sqlServer.Databases[databaseNameNew];
            sqlRestore.Action = RestoreActionType.Database;
            String dataFileLocation = dataFilePath + databaseNameNew + ".mdf";
            String logFileLocation = logFilePath + databaseNameNew + "_Log.ldf";
            db = sqlServer.Databases[databaseNameNew];
            sqlRestore.RelocateFiles.Add(new RelocateFile(databaseNameFrom, dataFileLocation));
            sqlRestore.RelocateFiles.Add(new RelocateFile(databaseNameFrom + "_log", logFileLocation));
            sqlRestore.ReplaceDatabase = true;
            sqlRestore.PercentCompleteNotification = 10;
            try
            {
                sqlRestore.SqlRestore(sqlServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }

            db = sqlServer.Databases[databaseNameNew];
            db.SetOnline();
            sqlServer.Refresh();
        }

        private void DB_Backup()
        {
            string strSQLServer = this.SQLServer.Text;  //@"(localdb)\localdb";
            string sqlConnectionString = @"Data Source=" + strSQLServer + @";Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            string strDatabase = this.Database.Text; //@"dbDataTransSumma";
            string strDatabaseBackupfile = this.DBBackupFolder.Text + @"\" + strDatabase + @"_Backup.bak";
            string script = @"BACKUP DATABASE [" + strDatabase + @"] TO DISK = N'" + strDatabaseBackupfile + @"' WITH NOFORMAT, INIT,  NAME = N'dbDataTransSumma-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            DateTime Nu = DateTime.Now;
            string fdt = Nu.ToString("dd-MM-yyyy HH:mm");
            try
            {
                SqlConnection conn = new SqlConnection(sqlConnectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(script, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                this.DBMessage.Text = strDatabase + " Backup OK " + fdt;
                this.DBMessage.ForeColor = System.Drawing.Color.DodgerBlue;
            }
            catch
            {
                this.DBMessage.Text = strDatabase + " Not Backed up " + fdt;
                this.DBMessage.ForeColor = System.Drawing.Color.Red;
            }
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
            int bBackupDatabase = this.BackupDatabase.Checked ? 1 : 0;
            masterKey.SetValue("BackupDatabase", bBackupDatabase, RegistryValueKind.DWord);
            masterKey.SetValue("DBBackupFolder", this.DBBackupFolder.Text, RegistryValueKind.String);
            masterKey.SetValue("SQLServer", this.SQLServer.Text, RegistryValueKind.String);
            masterKey.SetValue("Database", this.Database.Text, RegistryValueKind.String);
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
            if (this.BackupDatabase.Checked) ExportDB1();
            if (this.BackupDatabase.Checked) ExportDB2();
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

        private void SelectDBBackupFile_Click(object sender, EventArgs e)
        {
            this.bf.SelectedPath = this.DBBackupFolder.Text;
            this.bf.ShowNewFolderButton = true;
            this.bf.ShowDialog();
            this.DBBackupFolder.Text = this.bf.SelectedPath;
        }

 
    }
}
