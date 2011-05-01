namespace nsPuls3060
{
    partial class FrmVareList
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
            this.lvwVarer = new System.Windows.Forms.ListView();
            this.columnHeaderVarenr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVarenavn = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdFind = new System.Windows.Forms.Button();
            this.toolStripTextBoxFind = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwVarer
            // 
            this.lvwVarer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderVarenr,
            this.columnHeaderVarenavn});
            this.lvwVarer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwVarer.FullRowSelect = true;
            this.lvwVarer.GridLines = true;
            this.lvwVarer.Location = new System.Drawing.Point(0, 0);
            this.lvwVarer.MultiSelect = false;
            this.lvwVarer.Name = "lvwVarer";
            this.lvwVarer.Size = new System.Drawing.Size(264, 375);
            this.lvwVarer.TabIndex = 0;
            this.lvwVarer.UseCompatibleStateImageBehavior = false;
            this.lvwVarer.View = System.Windows.Forms.View.Details;
            this.lvwVarer.SelectedIndexChanged += new System.EventHandler(this.lvwVarer_SelectedIndexChanged);
            // 
            // columnHeaderVarenr
            // 
            this.columnHeaderVarenr.Text = "Varenr";
            // 
            // columnHeaderVarenavn
            // 
            this.columnHeaderVarenavn.Text = "Varenavn";
            this.columnHeaderVarenavn.Width = 195;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cmdFind);
            this.splitContainer1.Panel1.Controls.Add(this.toolStripTextBoxFind);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(358, 413);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Find";
            // 
            // cmdFind
            // 
            this.cmdFind.Location = new System.Drawing.Point(136, 6);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(39, 23);
            this.cmdFind.TabIndex = 1;
            this.cmdFind.Text = "Søg";
            this.cmdFind.UseVisualStyleBackColor = true;
            this.cmdFind.Click += new System.EventHandler(this.toolStripButtonFind_Click);
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.Location = new System.Drawing.Point(34, 7);
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(100, 20);
            this.toolStripTextBoxFind.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvwVarer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer2.Panel2.Controls.Add(this.cmdOK);
            this.splitContainer2.Size = new System.Drawing.Size(358, 375);
            this.splitContainer2.SplitterDistance = 264;
            this.splitContainer2.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(9, 56);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Annuler";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(9, 20);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // FrmVareList
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 413);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmVareListLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmVareListLocation;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVareList";
            this.ShowIcon = false;
            this.Text = "Varer";
            this.Load += new System.EventHandler(this.FrmVareList_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwVarer;
        private System.Windows.Forms.ColumnHeader columnHeaderVarenr;
        private System.Windows.Forms.ColumnHeader columnHeaderVarenavn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.TextBox toolStripTextBoxFind;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
    }
}