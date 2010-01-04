namespace nsPuls3060
{
    partial class FrmMain
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medlemmerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kreditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regnskabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pBSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kontingentForslagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 772);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1024, 28);
            this.statusStrip1.TabIndex = 1;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Info;
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 23);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.SystemColors.Info;
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(509, 23);
            this.toolStripStatusLabel2.Text = "C:\\Documents and Settings\\mha\\Dokumenter\\Medlem3060\\Databaser\\SQLCompact\\dbData30" +
                "60.sdf";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.pBSToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.medlemmerToolStripMenuItem,
            this.testToolStripMenuItem,
            this.excelToolStripMenuItem,
            this.kreditorToolStripMenuItem,
            this.regnskabToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // medlemmerToolStripMenuItem
            // 
            this.medlemmerToolStripMenuItem.Name = "medlemmerToolStripMenuItem";
            this.medlemmerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.medlemmerToolStripMenuItem.Text = "Medlemmer";
            this.medlemmerToolStripMenuItem.Click += new System.EventHandler(this.medlemmerToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.excelToolStripMenuItem.Text = "Excel";
            this.excelToolStripMenuItem.Click += new System.EventHandler(this.excelToolStripMenuItem_Click);
            // 
            // kreditorToolStripMenuItem
            // 
            this.kreditorToolStripMenuItem.Name = "kreditorToolStripMenuItem";
            this.kreditorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.kreditorToolStripMenuItem.Text = "Kreditor";
            this.kreditorToolStripMenuItem.Click += new System.EventHandler(this.kerditorToolStripMenuItem_Click);
            // 
            // regnskabToolStripMenuItem
            // 
            this.regnskabToolStripMenuItem.Name = "regnskabToolStripMenuItem";
            this.regnskabToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.regnskabToolStripMenuItem.Text = "Regnskab";
            this.regnskabToolStripMenuItem.Click += new System.EventHandler(this.regnskabToolStripMenuItem_Click);
            // 
            // pBSToolStripMenuItem
            // 
            this.pBSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kontingentForslagToolStripMenuItem});
            this.pBSToolStripMenuItem.Name = "pBSToolStripMenuItem";
            this.pBSToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.pBSToolStripMenuItem.Text = "PBS";
            // 
            // kontingentForslagToolStripMenuItem
            // 
            this.kontingentForslagToolStripMenuItem.Name = "kontingentForslagToolStripMenuItem";
            this.kontingentForslagToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.kontingentForslagToolStripMenuItem.Text = "Kontingent Forslag";
            this.kontingentForslagToolStripMenuItem.Click += new System.EventHandler(this.kontingentForslagToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmMainSize;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmMainPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmMainSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IsMdiContainer = true;
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmMainPoint;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Puls 3060";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medlemmerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kreditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem regnskabToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pBSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kontingentForslagToolStripMenuItem;
   }
}

