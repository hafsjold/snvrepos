namespace Trans2Summa3060
{
    partial class FrmSagList
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdFind = new System.Windows.Forms.Button();
            this.toolStripTextBoxFind = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvwSag = new System.Windows.Forms.ListView();
            this.columnHeaderSagnr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSagnavn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.checkBoxMedsaldo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(436, 413);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Find";
            // 
            // cmdFind
            // 
            this.cmdFind.Location = new System.Drawing.Point(136, 6);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(39, 23);
            this.cmdFind.TabIndex = 4;
            this.cmdFind.Text = "Søg";
            this.cmdFind.UseVisualStyleBackColor = true;
            this.cmdFind.Click += new System.EventHandler(this.cmdFind_Click);
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.Location = new System.Drawing.Point(34, 7);
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(100, 20);
            this.toolStripTextBoxFind.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvwSag);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer2.Panel2.Controls.Add(this.cmdOK);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxMedsaldo);
            this.splitContainer2.Size = new System.Drawing.Size(436, 375);
            this.splitContainer2.SplitterDistance = 344;
            this.splitContainer2.TabIndex = 0;
            // 
            // lvwSag
            // 
            this.lvwSag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSagnr,
            this.columnHeaderSagnavn});
            this.lvwSag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSag.FullRowSelect = true;
            this.lvwSag.GridLines = true;
            this.lvwSag.Location = new System.Drawing.Point(0, 0);
            this.lvwSag.MultiSelect = false;
            this.lvwSag.Name = "lvwSag";
            this.lvwSag.Size = new System.Drawing.Size(344, 375);
            this.lvwSag.TabIndex = 1;
            this.lvwSag.UseCompatibleStateImageBehavior = false;
            this.lvwSag.View = System.Windows.Forms.View.Details;
            this.lvwSag.SelectedIndexChanged += new System.EventHandler(this.lvwSag_SelectedIndexChanged);
            // 
            // columnHeaderSagnr
            // 
            this.columnHeaderSagnr.Text = "Sagnr";
            // 
            // columnHeaderSagnavn
            // 
            this.columnHeaderSagnavn.Text = "Sagnavn";
            this.columnHeaderSagnavn.Width = 250;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(6, 57);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Annuler";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(6, 21);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 8;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // checkBoxMedsaldo
            // 
            this.checkBoxMedsaldo.AutoSize = true;
            this.checkBoxMedsaldo.Checked = global::Trans2Summa3060.Properties.Settings.Default.frmKontoplanListCheckboxmedsaldo;
            this.checkBoxMedsaldo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMedsaldo.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Trans2Summa3060.Properties.Settings.Default, "frmKontoplanListCheckboxmedsaldo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxMedsaldo.Location = new System.Drawing.Point(6, 87);
            this.checkBoxMedsaldo.Name = "checkBoxMedsaldo";
            this.checkBoxMedsaldo.Size = new System.Drawing.Size(75, 43);
            this.checkBoxMedsaldo.TabIndex = 6;
            this.checkBoxMedsaldo.Text = "Vis kun\r\nsager med\r\nsaldo";
            this.checkBoxMedsaldo.UseVisualStyleBackColor = true;
            this.checkBoxMedsaldo.CheckedChanged += new System.EventHandler(this.checkBoxMedsaldo_CheckedChanged);
            // 
            // FrmSagList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 413);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2Summa3060.Properties.Settings.Default, "frmSagListLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2Summa3060.Properties.Settings.Default.frmSagListLocation;
            this.Name = "FrmSagList";
            this.Text = "Sag";
            this.Load += new System.EventHandler(this.FrmSagList_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.TextBox toolStripTextBoxFind;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.CheckBox checkBoxMedsaldo;
        private System.Windows.Forms.ListView lvwSag;
        private System.Windows.Forms.ColumnHeader columnHeaderSagnr;
        private System.Windows.Forms.ColumnHeader columnHeaderSagnavn;
    }
}