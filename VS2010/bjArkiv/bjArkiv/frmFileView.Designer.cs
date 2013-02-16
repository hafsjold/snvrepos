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
            this.btnTest = new System.Windows.Forms.Button();
            this.splitHorisontal = new System.Windows.Forms.SplitContainer();
            this.splitVertical = new System.Windows.Forms.SplitContainer();
            this.fldrView = new LogicNP.FolderViewControl.FolderView();
            ((System.ComponentModel.ISupportInitialize)(this.splitHorisontal)).BeginInit();
            this.splitHorisontal.Panel1.SuspendLayout();
            this.splitHorisontal.Panel2.SuspendLayout();
            this.splitHorisontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitVertical)).BeginInit();
            this.splitVertical.Panel1.SuspendLayout();
            this.splitVertical.Panel2.SuspendLayout();
            this.splitVertical.SuspendLayout();
            this.SuspendLayout();
            // 
            // flView
            // 
            this.flView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flView.FullRowSelect = true;
            this.flView.Location = new System.Drawing.Point(0, 0);
            this.flView.Name = "flView";
            this.flView.ShowHiddenItems = false;
            this.flView.ShowSpecialFolders = false;
            this.flView.Size = new System.Drawing.Size(532, 388);
            this.flView.TabIndex = 0;
            this.flView.Text = "fileView1";
            this.flView.ViewStyle = LogicNP.FileViewControl.ViewStyles.Report;
            this.flView.AfterItemAdd += new LogicNP.FileViewControl.AfterItemAddEventHandler(this.flView_AfterItemAdd);
            this.flView.BeforeColumnAdd += new LogicNP.FileViewControl.BeforeColumnAddEventHandler(this.flView_BeforeColumnAdd);
            this.flView.ItemDblClick += new LogicNP.FileViewControl.ItemDblClickEventHandler(this.flView_ItemDblClick);
            this.flView.CurrentFolderChanged += new LogicNP.FileViewControl.CurrentFolderChangedEventHandler(this.flView_CurrentFolderChanged);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(635, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(39, 24);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
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
            this.splitHorisontal.Panel1.Controls.Add(this.btnTest);
            // 
            // splitHorisontal.Panel2
            // 
            this.splitHorisontal.Panel2.Controls.Add(this.splitVertical);
            this.splitHorisontal.Size = new System.Drawing.Size(686, 442);
            this.splitHorisontal.TabIndex = 4;
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
            this.splitVertical.Size = new System.Drawing.Size(686, 388);
            this.splitVertical.SplitterDistance = 150;
            this.splitVertical.TabIndex = 0;
            // 
            // fldrView
            // 
            this.fldrView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fldrView.FileView = this.flView;
            this.fldrView.Location = new System.Drawing.Point(0, 0);
            this.fldrView.Name = "fldrView";
            this.fldrView.ShowHiddenObjects = false;
            this.fldrView.ShowSpecialFolders = false;
            this.fldrView.Size = new System.Drawing.Size(150, 388);
            this.fldrView.TabIndex = 0;
            this.fldrView.Text = "folderView1";
            // 
            // frmFileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 442);
            this.Controls.Add(this.splitHorisontal);
            this.Name = "frmFileView";
            this.Text = "frmFileView";
            this.splitHorisontal.Panel1.ResumeLayout(false);
            this.splitHorisontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitHorisontal)).EndInit();
            this.splitHorisontal.ResumeLayout(false);
            this.splitVertical.Panel1.ResumeLayout(false);
            this.splitVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitVertical)).EndInit();
            this.splitVertical.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LogicNP.FileViewControl.FileView flView;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.SplitContainer splitHorisontal;
        private System.Windows.Forms.SplitContainer splitVertical;
        private LogicNP.FolderViewControl.FolderView fldrView;
    }
}