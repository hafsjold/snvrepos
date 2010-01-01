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
            this.taTblMedlem = new AccessToSQL.AccessDataSetTableAdapters.tblMedlemTableAdapter();
            this.dsAccess = new AccessToSQL.AccessDataSet();
            this.taTblMedlemLog = new AccessToSQL.AccessDataSetTableAdapters.tblMedlemLogTableAdapter();
            this.tblMedlemLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsAccess)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblMedlemmToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(509, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tblMedlemmToolStripMenuItem
            // 
            this.tblMedlemmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tblMedlemmToolStripMenuItem1,
            this.tblMedlemLogToolStripMenuItem});
            this.tblMedlemmToolStripMenuItem.Name = "tblMedlemmToolStripMenuItem";
            this.tblMedlemmToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.tblMedlemmToolStripMenuItem.Text = "File";
            // 
            // tblMedlemmToolStripMenuItem1
            // 
            this.tblMedlemmToolStripMenuItem1.Name = "tblMedlemmToolStripMenuItem1";
            this.tblMedlemmToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.tblMedlemmToolStripMenuItem1.Text = "tblMedlemm";
            this.tblMedlemmToolStripMenuItem1.Click += new System.EventHandler(this.tblMedlemToolStripMenuItem1_Click);
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
            // tblMedlemLogToolStripMenuItem
            // 
            this.tblMedlemLogToolStripMenuItem.Name = "tblMedlemLogToolStripMenuItem";
            this.tblMedlemLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.tblMedlemLogToolStripMenuItem.Text = "tblMedlemLog";
            this.tblMedlemLogToolStripMenuItem.Click += new System.EventHandler(this.tblMedlemLogToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 380);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsAccess)).EndInit();
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
    }
}

