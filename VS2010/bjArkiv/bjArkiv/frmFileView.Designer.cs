namespace bjArkiv
{
    partial class frmFileView
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
            this.flView = new LogicNP.FileViewControl.FileView(this.components);
            this.splitHorisontal = new System.Windows.Forms.SplitContainer();
            this.labelPath = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opretNytArkivToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.afslutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitVertical = new System.Windows.Forms.SplitContainer();
            this.fldrView = new LogicNP.FolderViewControl.FolderView();
            ((System.ComponentModel.ISupportInitialize)(this.splitHorisontal)).BeginInit();
            this.splitHorisontal.Panel1.SuspendLayout();
            this.splitHorisontal.Panel2.SuspendLayout();
            this.splitHorisontal.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitVertical)).BeginInit();
            this.splitVertical.Panel1.SuspendLayout();
            this.splitVertical.Panel2.SuspendLayout();
            this.splitVertical.SuspendLayout();
            this.SuspendLayout();
            // 
            // flView
            // 
            this.flView.AllowColumnHeaderDragdrop = true;
            this.flView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flView.FullRowSelect = true;
            this.flView.Location = new System.Drawing.Point(0, 0);
            this.flView.Name = "flView";
            this.flView.ShowHiddenItems = false;
            this.flView.ShowSpecialFolders = false;
            this.flView.Size = new System.Drawing.Size(532, 433);
            this.flView.TabIndex = 0;
            this.flView.Text = "fileView1";
            this.flView.ViewStyle = LogicNP.FileViewControl.ViewStyles.Report;
            this.flView.AfterFill += new LogicNP.FileViewControl.AfterFillEventHandler(this.flView_AfterFill);
            this.flView.AfterItemAdd += new LogicNP.FileViewControl.AfterItemAddEventHandler(this.flView_AfterItemAdd);
            this.flView.BeforeColumnAdd += new LogicNP.FileViewControl.BeforeColumnAddEventHandler(this.flView_BeforeColumnAdd);
            this.flView.BeforeFill += new LogicNP.FileViewControl.BeforeFillEventHandler(this.flView_BeforeFill);
            this.flView.CustomContextMenuItemSelect += new LogicNP.FileViewControl.CustomContextMenuItemSelectEventHandler(this.flView_CustomContextMenuItemSelect);
            this.flView.ItemDblClick += new LogicNP.FileViewControl.ItemDblClickEventHandler(this.flView_ItemDblClick);
            this.flView.CurrentFolderChanged += new LogicNP.FileViewControl.CurrentFolderChangedEventHandler(this.flView_CurrentFolderChanged);
            this.flView.PopupContextMenu += new LogicNP.FileViewControl.PopupContextMenuEventHandler(this.flView_PopupContextMenu);
            // 
            // splitHorisontal
            // 
            this.splitHorisontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitHorisontal.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitHorisontal.IsSplitterFixed = true;
            this.splitHorisontal.Location = new System.Drawing.Point(0, 0);
            this.splitHorisontal.Name = "splitHorisontal";
            this.splitHorisontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitHorisontal.Panel1
            // 
            this.splitHorisontal.Panel1.AccessibleName = "";
            this.splitHorisontal.Panel1.Controls.Add(this.labelPath);
            this.splitHorisontal.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitHorisontal.Panel2
            // 
            this.splitHorisontal.Panel2.Controls.Add(this.splitVertical);
            this.splitHorisontal.Size = new System.Drawing.Size(686, 462);
            this.splitHorisontal.SplitterDistance = 25;
            this.splitHorisontal.TabIndex = 4;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPath.Location = new System.Drawing.Point(154, 7);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(0, 18);
            this.labelPath.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opretNytArkivToolStripMenuItem,
            this.afslutToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // opretNytArkivToolStripMenuItem
            // 
            this.opretNytArkivToolStripMenuItem.Name = "opretNytArkivToolStripMenuItem";
            this.opretNytArkivToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.opretNytArkivToolStripMenuItem.Text = "Opret Nyt Arkiv";
            this.opretNytArkivToolStripMenuItem.Click += new System.EventHandler(this.opretNytArkivToolStripMenuItem_Click);
            // 
            // afslutToolStripMenuItem
            // 
            this.afslutToolStripMenuItem.Name = "afslutToolStripMenuItem";
            this.afslutToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.afslutToolStripMenuItem.Text = "Afslut";
            this.afslutToolStripMenuItem.Click += new System.EventHandler(this.afslutToolStripMenuItem_Click);
            // 
            // splitVertical
            // 
            this.splitVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitVertical.Location = new System.Drawing.Point(0, 0);
            this.splitVertical.Name = "splitVertical";
            // 
            // splitVertical.Panel1
            // 
            this.splitVertical.Panel1.Controls.Add(this.fldrView);
            // 
            // splitVertical.Panel2
            // 
            this.splitVertical.Panel2.Controls.Add(this.flView);
            this.splitVertical.Size = new System.Drawing.Size(686, 433);
            this.splitVertical.SplitterDistance = 150;
            this.splitVertical.TabIndex = 0;
            this.splitVertical.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitVertical_SplitterMoved);
            // 
            // fldrView
            // 
            this.fldrView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fldrView.FileView = this.flView;
            this.fldrView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fldrView.Location = new System.Drawing.Point(0, 0);
            this.fldrView.Name = "fldrView";
            this.fldrView.ShowHiddenObjects = false;
            this.fldrView.ShowSpecialFolders = false;
            this.fldrView.Size = new System.Drawing.Size(150, 433);
            this.fldrView.TabIndex = 0;
            this.fldrView.Text = "folderView1";
            this.fldrView.AfterSelect += new LogicNP.FolderViewControl.AfterSelectHandler(this.fldrView_AfterSelect);
            // 
            // frmFileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::bjArkiv.Properties.Settings.Default.frmFileVirwSize;
            this.Controls.Add(this.splitHorisontal);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::bjArkiv.Properties.Settings.Default, "frmFileViewLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::bjArkiv.Properties.Settings.Default, "frmFileVirwSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::bjArkiv.Properties.Settings.Default.frmFileViewLocation;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "frmFileView";
            this.Text = "bjArkiv";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFileView_FormClosing);
            this.Load += new System.EventHandler(this.frmFileView_Load);
            this.splitHorisontal.Panel1.ResumeLayout(false);
            this.splitHorisontal.Panel1.PerformLayout();
            this.splitHorisontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitHorisontal)).EndInit();
            this.splitHorisontal.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitVertical.Panel1.ResumeLayout(false);
            this.splitVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitVertical)).EndInit();
            this.splitVertical.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LogicNP.FileViewControl.FileView flView;
        private System.Windows.Forms.SplitContainer splitHorisontal;
        private System.Windows.Forms.SplitContainer splitVertical;
        private LogicNP.FolderViewControl.FolderView fldrView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem opretNytArkivToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem afslutToolStripMenuItem;
        private System.Windows.Forms.Label labelPath;
    }
}