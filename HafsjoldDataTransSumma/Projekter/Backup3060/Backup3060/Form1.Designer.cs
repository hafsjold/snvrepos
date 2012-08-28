namespace Backup3060
{
    partial class frmBackup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BackupDir = new System.Windows.Forms.TextBox();
            this.label_BackupDir = new System.Windows.Forms.Label();
            this.SelectBackupDir = new System.Windows.Forms.Button();
            this.bf = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lvwFolders = new System.Windows.Forms.ListView();
            this.colHdFoldere = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testmenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BackupFolder = new System.Windows.Forms.TextBox();
            this.label_BackupFolder = new System.Windows.Forms.Label();
            this.SelectBackupFolder = new System.Windows.Forms.Button();
            this.cmdAddFolderToList = new System.Windows.Forms.Button();
            this.SelectBackupFile = new System.Windows.Forms.Button();
            this.bd = new System.Windows.Forms.OpenFileDialog();
            this.DB = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackupDir
            // 
            this.BackupDir.Location = new System.Drawing.Point(103, 17);
            this.BackupDir.Name = "BackupDir";
            this.BackupDir.Size = new System.Drawing.Size(362, 20);
            this.BackupDir.TabIndex = 0;
            // 
            // label_BackupDir
            // 
            this.label_BackupDir.AutoSize = true;
            this.label_BackupDir.Location = new System.Drawing.Point(9, 20);
            this.label_BackupDir.Name = "label_BackupDir";
            this.label_BackupDir.Size = new System.Drawing.Size(83, 13);
            this.label_BackupDir.TabIndex = 1;
            this.label_BackupDir.Text = "Backup til folder";
            // 
            // SelectBackupDir
            // 
            this.SelectBackupDir.Location = new System.Drawing.Point(471, 17);
            this.SelectBackupDir.Name = "SelectBackupDir";
            this.SelectBackupDir.Size = new System.Drawing.Size(24, 20);
            this.SelectBackupDir.TabIndex = 2;
            this.SelectBackupDir.Text = "....";
            this.SelectBackupDir.UseVisualStyleBackColor = true;
            this.SelectBackupDir.Click += new System.EventHandler(this.SelectBackupDir_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(289, 412);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(70, 21);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(194, 412);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(70, 21);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Fortryd";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lvwFolders
            // 
            this.lvwFolders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHdFoldere});
            this.lvwFolders.Location = new System.Drawing.Point(103, 80);
            this.lvwFolders.Name = "lvwFolders";
            this.lvwFolders.Size = new System.Drawing.Size(362, 326);
            this.lvwFolders.TabIndex = 4;
            this.lvwFolders.UseCompatibleStateImageBehavior = false;
            this.lvwFolders.View = System.Windows.Forms.View.Details;
            this.lvwFolders.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwFolders_MouseClick);
            // 
            // colHdFoldere
            // 
            this.colHdFoldere.Text = "Backup foldere";
            this.colHdFoldere.Width = 337;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testmenuToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 26);
            // 
            // testmenuToolStripMenuItem
            // 
            this.testmenuToolStripMenuItem.Name = "testmenuToolStripMenuItem";
            this.testmenuToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.testmenuToolStripMenuItem.Text = "Testmenu";
            this.testmenuToolStripMenuItem.Click += new System.EventHandler(this.testmenuToolStripMenuItem_Click);
            // 
            // BackupFolder
            // 
            this.BackupFolder.Location = new System.Drawing.Point(103, 56);
            this.BackupFolder.Name = "BackupFolder";
            this.BackupFolder.Size = new System.Drawing.Size(362, 20);
            this.BackupFolder.TabIndex = 0;
            // 
            // label_BackupFolder
            // 
            this.label_BackupFolder.AutoSize = true;
            this.label_BackupFolder.Location = new System.Drawing.Point(9, 59);
            this.label_BackupFolder.Name = "label_BackupFolder";
            this.label_BackupFolder.Size = new System.Drawing.Size(85, 13);
            this.label_BackupFolder.TabIndex = 1;
            this.label_BackupFolder.Text = "Backup af folder";
            // 
            // SelectBackupFolder
            // 
            this.SelectBackupFolder.Location = new System.Drawing.Point(471, 47);
            this.SelectBackupFolder.Name = "SelectBackupFolder";
            this.SelectBackupFolder.Size = new System.Drawing.Size(33, 20);
            this.SelectBackupFolder.TabIndex = 2;
            this.SelectBackupFolder.Text = "..d..";
            this.SelectBackupFolder.UseVisualStyleBackColor = true;
            this.SelectBackupFolder.Click += new System.EventHandler(this.SelectBackupFolder_Click);
            // 
            // cmdAddFolderToList
            // 
            this.cmdAddFolderToList.Location = new System.Drawing.Point(471, 101);
            this.cmdAddFolderToList.Name = "cmdAddFolderToList";
            this.cmdAddFolderToList.Size = new System.Drawing.Size(24, 20);
            this.cmdAddFolderToList.TabIndex = 2;
            this.cmdAddFolderToList.Text = "+";
            this.cmdAddFolderToList.UseVisualStyleBackColor = true;
            this.cmdAddFolderToList.Click += new System.EventHandler(this.cmdAddFolderToList_Click);
            // 
            // SelectBackupFile
            // 
            this.SelectBackupFile.Location = new System.Drawing.Point(471, 68);
            this.SelectBackupFile.Name = "SelectBackupFile";
            this.SelectBackupFile.Size = new System.Drawing.Size(33, 20);
            this.SelectBackupFile.TabIndex = 2;
            this.SelectBackupFile.Text = "..f..";
            this.SelectBackupFile.UseVisualStyleBackColor = true;
            this.SelectBackupFile.Click += new System.EventHandler(this.SelectBackupFile_Click);
            // 
            // bd
            // 
            this.bd.FileName = "openFileDialog1";
            // 
            // DB
            // 
            this.DB.Location = new System.Drawing.Point(387, 412);
            this.DB.Name = "DB";
            this.DB.Size = new System.Drawing.Size(75, 23);
            this.DB.TabIndex = 5;
            this.DB.Text = "DB";
            this.DB.UseVisualStyleBackColor = true;
            this.DB.Click += new System.EventHandler(this.DB_Click);
            // 
            // frmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 451);
            this.Controls.Add(this.DB);
            this.Controls.Add(this.lvwFolders);
            this.Controls.Add(this.SelectBackupDir);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.label_BackupDir);
            this.Controls.Add(this.label_BackupFolder);
            this.Controls.Add(this.SelectBackupFolder);
            this.Controls.Add(this.SelectBackupFile);
            this.Controls.Add(this.BackupFolder);
            this.Controls.Add(this.BackupDir);
            this.Controls.Add(this.cmdAddFolderToList);
            this.Name = "frmBackup";
            this.Text = "Puls 3060 Backup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBackup_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BackupDir;
        private System.Windows.Forms.Label label_BackupDir;
        private System.Windows.Forms.Button SelectBackupDir;
        private System.Windows.Forms.FolderBrowserDialog bf;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ListView lvwFolders;
        private System.Windows.Forms.ColumnHeader colHdFoldere;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testmenuToolStripMenuItem;
        private System.Windows.Forms.TextBox BackupFolder;
        private System.Windows.Forms.Label label_BackupFolder;
        private System.Windows.Forms.Button SelectBackupFolder;
        private System.Windows.Forms.Button cmdAddFolderToList;
        private System.Windows.Forms.Button SelectBackupFile;
        private System.Windows.Forms.OpenFileDialog bd;
        private System.Windows.Forms.Button DB;
    }
}

