namespace nsPuls3060v2
{
    partial class FrmPbsfiles
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabellerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbsfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbsfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbsforsendelseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richData = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(749, 330);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabellerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(749, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabellerToolStripMenuItem
            // 
            this.tabellerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbsfilesToolStripMenuItem,
            this.logToolStripMenuItem,
            this.pbsfileToolStripMenuItem,
            this.pbsforsendelseToolStripMenuItem});
            this.tabellerToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.tabellerToolStripMenuItem.Name = "tabellerToolStripMenuItem";
            this.tabellerToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.tabellerToolStripMenuItem.Text = "Tables";
            // 
            // pbsfilesToolStripMenuItem
            // 
            this.pbsfilesToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.pbsfilesToolStripMenuItem.Name = "pbsfilesToolStripMenuItem";
            this.pbsfilesToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.pbsfilesToolStripMenuItem.Text = "Pbsfiles";
            this.pbsfilesToolStripMenuItem.Click += new System.EventHandler(this.pbsfilesToolStripMenuItem_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.logToolStripMenuItem.Text = "Log";
            this.logToolStripMenuItem.Click += new System.EventHandler(this.logToolStripMenuItem_Click);
            // 
            // pbsfileToolStripMenuItem
            // 
            this.pbsfileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.pbsfileToolStripMenuItem.Name = "pbsfileToolStripMenuItem";
            this.pbsfileToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.pbsfileToolStripMenuItem.Text = "Pbsfile";
            this.pbsfileToolStripMenuItem.Click += new System.EventHandler(this.pbsfileToolStripMenuItem_Click);
            // 
            // pbsforsendelseToolStripMenuItem
            // 
            this.pbsforsendelseToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.pbsforsendelseToolStripMenuItem.Name = "pbsforsendelseToolStripMenuItem";
            this.pbsforsendelseToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.pbsforsendelseToolStripMenuItem.Text = "Pbsforsendelse";
            this.pbsforsendelseToolStripMenuItem.Click += new System.EventHandler(this.pbsforsendelseToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richData);
            this.splitContainer1.Size = new System.Drawing.Size(749, 487);
            this.splitContainer1.SplitterDistance = 330;
            this.splitContainer1.TabIndex = 2;
            // 
            // richData
            // 
            this.richData.DetectUrls = false;
            this.richData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richData.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richData.Location = new System.Drawing.Point(0, 0);
            this.richData.Name = "richData";
            this.richData.Size = new System.Drawing.Size(749, 153);
            this.richData.TabIndex = 0;
            this.richData.Text = "";
            this.richData.WordWrap = false;
            // 
            // FrmPbsfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 511);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPbsfiles";
            this.Text = "Pbsfiles";
            this.Load += new System.EventHandler(this.pbsfilesToolStripMenuItem_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tabellerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pbsfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pbsfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pbsforsendelseToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richData;
    }
}