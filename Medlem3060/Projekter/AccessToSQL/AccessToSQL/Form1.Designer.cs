namespace AccessToSQL
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tblMedlemmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tblMedlemmToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tblMedlemLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taTblMedlem = new AccessToSQL.AccessDataSetTableAdapters.tblMedlemTableAdapter();
            this.dsAccess = new AccessToSQL.AccessDataSet();
            this.taTblMedlemLog = new AccessToSQL.AccessDataSetTableAdapters.tblMedlemLogTableAdapter();
            this.taTblpbsforsendelse = new AccessToSQL.AccessDataSetTableAdapters.tblpbsforsendelseTableAdapter();
            this.taTbltilpbs = new AccessToSQL.AccessDataSetTableAdapters.tbltilpbsTableAdapter();
            this.taTblfak = new AccessToSQL.AccessDataSetTableAdapters.tblfakTableAdapter();
            this.taTblpbsfiles = new AccessToSQL.AccessDataSetTableAdapters.tblpbsfilesTableAdapter();
            this.taTblpbsfile = new AccessToSQL.AccessDataSetTableAdapters.tblpbsfileTableAdapter();
            this.taTblfrapbs = new AccessToSQL.AccessDataSetTableAdapters.tblfrapbsTableAdapter();
            this.taTblbet = new AccessToSQL.AccessDataSetTableAdapters.tblbetTableAdapter();
            this.taTblbetlin = new AccessToSQL.AccessDataSetTableAdapters.tblbetlinTableAdapter();
            this.tableAdapterManager1 = new AccessToSQL.AccessDataSetTableAdapters.TableAdapterManager();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.accessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsAccess)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblMedlemmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(938, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tblMedlemmToolStripMenuItem
            // 
            this.tblMedlemmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accessToolStripMenuItem,
            this.tblMedlemmToolStripMenuItem1,
            this.tblMedlemLogToolStripMenuItem});
            this.tblMedlemmToolStripMenuItem.Name = "tblMedlemmToolStripMenuItem";
            this.tblMedlemmToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.tblMedlemmToolStripMenuItem.Text = "File";
            // 
            // tblMedlemmToolStripMenuItem1
            // 
            this.tblMedlemmToolStripMenuItem1.Name = "tblMedlemmToolStripMenuItem1";
            this.tblMedlemmToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.tblMedlemmToolStripMenuItem1.Text = "Script Medlem";
            this.tblMedlemmToolStripMenuItem1.Click += new System.EventHandler(this.MedlemToolStripMenuItem1_Click);
            // 
            // tblMedlemLogToolStripMenuItem
            // 
            this.tblMedlemLogToolStripMenuItem.Name = "tblMedlemLogToolStripMenuItem";
            this.tblMedlemLogToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.tblMedlemLogToolStripMenuItem.Text = "Script PBS";
            this.tblMedlemLogToolStripMenuItem.Click += new System.EventHandler(this.PBSToolStripMenuItem_Click);
            // 
            // taTblMedlem
            // 
            this.taTblMedlem.ClearBeforeFill = true;
            // 
            // dsAccess
            // 
            this.dsAccess.DataSetName = "AccessDataSet";
            this.dsAccess.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // taTblMedlemLog
            // 
            this.taTblMedlemLog.ClearBeforeFill = true;
            // 
            // taTblpbsforsendelse
            // 
            this.taTblpbsforsendelse.ClearBeforeFill = true;
            // 
            // taTbltilpbs
            // 
            this.taTbltilpbs.ClearBeforeFill = true;
            // 
            // taTblfak
            // 
            this.taTblfak.ClearBeforeFill = true;
            // 
            // taTblpbsfiles
            // 
            this.taTblpbsfiles.ClearBeforeFill = true;
            // 
            // taTblpbsfile
            // 
            this.taTblpbsfile.ClearBeforeFill = true;
            // 
            // taTblfrapbs
            // 
            this.taTblfrapbs.ClearBeforeFill = true;
            // 
            // taTblbet
            // 
            this.taTblbet.ClearBeforeFill = true;
            // 
            // taTblbetlin
            // 
            this.taTblbetlin.ClearBeforeFill = true;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.tblbetlinTableAdapter = this.taTblbetlin;
            this.tableAdapterManager1.tblbetTableAdapter = this.taTblbet;
            this.tableAdapterManager1.tblfakTableAdapter = this.taTblfak;
            this.tableAdapterManager1.tblfrapbsTableAdapter = this.taTblfrapbs;
            this.tableAdapterManager1.tblMedlemLogTableAdapter = this.taTblMedlemLog;
            this.tableAdapterManager1.tblMedlemTableAdapter = this.taTblMedlem;
            this.tableAdapterManager1.tblpbsfilesTableAdapter = this.taTblpbsfiles;
            this.tableAdapterManager1.tblpbsfileTableAdapter = this.taTblpbsfile;
            this.tableAdapterManager1.tblpbsforsendelseTableAdapter = this.taTblpbsforsendelse;
            this.tableAdapterManager1.tbltilpbsTableAdapter = this.taTbltilpbs;
            this.tableAdapterManager1.UpdateOrder = AccessToSQL.AccessDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 430);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(938, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // accessToolStripMenuItem
            // 
            this.accessToolStripMenuItem.Name = "accessToolStripMenuItem";
            this.accessToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.accessToolStripMenuItem.Text = "Vælg Access DB";
            this.accessToolStripMenuItem.Click += new System.EventHandler(this.accessToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 452);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsAccess)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tblMedlemmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tblMedlemmToolStripMenuItem1;
        private AccessToSQL.AccessDataSetTableAdapters.tblMedlemTableAdapter taTblMedlem;
        private AccessDataSet dsAccess;
        private System.Windows.Forms.ToolStripMenuItem tblMedlemLogToolStripMenuItem;
        private AccessToSQL.AccessDataSetTableAdapters.tblMedlemLogTableAdapter taTblMedlemLog;
        private AccessToSQL.AccessDataSetTableAdapters.tblpbsforsendelseTableAdapter taTblpbsforsendelse;
        private AccessToSQL.AccessDataSetTableAdapters.tbltilpbsTableAdapter taTbltilpbs;
        private AccessToSQL.AccessDataSetTableAdapters.tblfakTableAdapter taTblfak;
        private AccessToSQL.AccessDataSetTableAdapters.tblpbsfilesTableAdapter taTblpbsfiles;
        private AccessToSQL.AccessDataSetTableAdapters.tblpbsfileTableAdapter taTblpbsfile;
        private AccessToSQL.AccessDataSetTableAdapters.tblfrapbsTableAdapter taTblfrapbs;
        private AccessToSQL.AccessDataSetTableAdapters.tblbetTableAdapter taTblbet;
        private AccessToSQL.AccessDataSetTableAdapters.tblbetlinTableAdapter taTblbetlin;
        private AccessToSQL.AccessDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem accessToolStripMenuItem;
    }
}

