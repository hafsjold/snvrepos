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
            this.Label_accessDB = new System.Windows.Forms.Label();
            this.label_SQLDB = new System.Windows.Forms.Label();
            this.label_SqlCeCmd = new System.Windows.Forms.Label();
            this.label_Script = new System.Windows.Forms.Label();
            this.Script = new System.Windows.Forms.TextBox();
            this.SqlCeCmd = new System.Windows.Forms.TextBox();
            this.SQLDB = new System.Windows.Forms.TextBox();
            this.accessDB = new System.Windows.Forms.TextBox();
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
            this.Run = new System.Windows.Forms.Button();
            this.Returkode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dsAccess)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_accessDB
            // 
            this.Label_accessDB.AutoEllipsis = true;
            this.Label_accessDB.AutoSize = true;
            this.Label_accessDB.Location = new System.Drawing.Point(12, 26);
            this.Label_accessDB.Name = "Label_accessDB";
            this.Label_accessDB.Size = new System.Drawing.Size(91, 13);
            this.Label_accessDB.TabIndex = 3;
            this.Label_accessDB.Text = "Access Database";
            // 
            // label_SQLDB
            // 
            this.label_SQLDB.AutoEllipsis = true;
            this.label_SQLDB.AutoSize = true;
            this.label_SQLDB.Location = new System.Drawing.Point(12, 52);
            this.label_SQLDB.Name = "label_SQLDB";
            this.label_SQLDB.Size = new System.Drawing.Size(77, 13);
            this.label_SQLDB.TabIndex = 3;
            this.label_SQLDB.Text = "SQL Database";
            // 
            // label_SqlCeCmd
            // 
            this.label_SqlCeCmd.AutoEllipsis = true;
            this.label_SqlCeCmd.AutoSize = true;
            this.label_SqlCeCmd.Location = new System.Drawing.Point(12, 78);
            this.label_SqlCeCmd.Name = "label_SqlCeCmd";
            this.label_SqlCeCmd.Size = new System.Drawing.Size(100, 13);
            this.label_SqlCeCmd.TabIndex = 3;
            this.label_SqlCeCmd.Text = "SqlCeCmd.exe path";
            // 
            // label_Script
            // 
            this.label_Script.AutoEllipsis = true;
            this.label_Script.AutoSize = true;
            this.label_Script.Location = new System.Drawing.Point(12, 104);
            this.label_Script.Name = "label_Script";
            this.label_Script.Size = new System.Drawing.Size(50, 13);
            this.label_Script.TabIndex = 3;
            this.label_Script.Text = "Script file";
            // 
            // Script
            // 
            this.Script.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AccessToSQL.Properties.Settings.Default, "Script", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Script.Location = new System.Drawing.Point(123, 101);
            this.Script.Name = "Script";
            this.Script.Size = new System.Drawing.Size(724, 20);
            this.Script.TabIndex = 2;
            this.Script.Text = global::AccessToSQL.Properties.Settings.Default.Script;
            this.Script.DoubleClick += new System.EventHandler(this.Script_DoubleClick);
            // 
            // SqlCeCmd
            // 
            this.SqlCeCmd.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AccessToSQL.Properties.Settings.Default, "SqlCeCmd", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SqlCeCmd.Location = new System.Drawing.Point(123, 75);
            this.SqlCeCmd.Name = "SqlCeCmd";
            this.SqlCeCmd.Size = new System.Drawing.Size(724, 20);
            this.SqlCeCmd.TabIndex = 2;
            this.SqlCeCmd.Text = global::AccessToSQL.Properties.Settings.Default.SqlCeCmd;
            this.SqlCeCmd.DoubleClick += new System.EventHandler(this.SqlCeCmd_DoubleClick);
            // 
            // SQLDB
            // 
            this.SQLDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AccessToSQL.Properties.Settings.Default, "SQLDB", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SQLDB.Location = new System.Drawing.Point(123, 49);
            this.SQLDB.Name = "SQLDB";
            this.SQLDB.Size = new System.Drawing.Size(724, 20);
            this.SQLDB.TabIndex = 2;
            this.SQLDB.Text = global::AccessToSQL.Properties.Settings.Default.SQLDB;
            this.SQLDB.DoubleClick += new System.EventHandler(this.SQLDB_DoubleClick);
            // 
            // accessDB
            // 
            this.accessDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::AccessToSQL.Properties.Settings.Default, "accessDB", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.accessDB.Location = new System.Drawing.Point(123, 23);
            this.accessDB.Name = "accessDB";
            this.accessDB.Size = new System.Drawing.Size(724, 20);
            this.accessDB.TabIndex = 2;
            this.accessDB.Text = global::AccessToSQL.Properties.Settings.Default.accessDB;
            this.accessDB.DoubleClick += new System.EventHandler(this.accessDB_DoubleClick);
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
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(123, 144);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(89, 22);
            this.Run.TabIndex = 4;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Run_MouseClick);
            // 
            // Returkode
            // 
            this.Returkode.AutoSize = true;
            this.Returkode.Location = new System.Drawing.Point(244, 148);
            this.Returkode.Name = "Returkode";
            this.Returkode.Size = new System.Drawing.Size(35, 13);
            this.Returkode.TabIndex = 5;
            this.Returkode.Text = "label1";
            this.Returkode.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 452);
            this.Controls.Add(this.Returkode);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.label_Script);
            this.Controls.Add(this.label_SqlCeCmd);
            this.Controls.Add(this.label_SQLDB);
            this.Controls.Add(this.Label_accessDB);
            this.Controls.Add(this.Script);
            this.Controls.Add(this.SqlCeCmd);
            this.Controls.Add(this.SQLDB);
            this.Controls.Add(this.accessDB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsAccess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AccessToSQL.AccessDataSetTableAdapters.tblMedlemTableAdapter taTblMedlem;
        private AccessDataSet dsAccess;
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
        private System.Windows.Forms.TextBox accessDB;
        private System.Windows.Forms.Label Label_accessDB;
        private System.Windows.Forms.TextBox SQLDB;
        private System.Windows.Forms.Label label_SQLDB;
        private System.Windows.Forms.TextBox SqlCeCmd;
        private System.Windows.Forms.Label label_SqlCeCmd;
        private System.Windows.Forms.TextBox Script;
        private System.Windows.Forms.Label label_Script;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.Label Returkode;
    }
}

