namespace Trans2SummaHDA
{
    partial class FrmKontoplanList
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
            this.lvwKontoplan = new System.Windows.Forms.ListView();
            this.columnHeaderKontonr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKontonavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMoms = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdFind = new System.Windows.Forms.Button();
            this.toolStripTextBoxFind = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.checkBoxMedsaldo = new System.Windows.Forms.CheckBox();
            this.checkBoxDrift = new System.Windows.Forms.CheckBox();
            this.checkBoxDebitor = new System.Windows.Forms.CheckBox();
            this.checkBoxKreditor = new System.Windows.Forms.CheckBox();
            this.checkBoxStatus = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwKontoplan
            // 
            this.lvwKontoplan.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderKontonr,
            this.columnHeaderKontonavn,
            this.columnHeaderMoms});
            this.lvwKontoplan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwKontoplan.FullRowSelect = true;
            this.lvwKontoplan.GridLines = true;
            this.lvwKontoplan.Location = new System.Drawing.Point(0, 0);
            this.lvwKontoplan.MultiSelect = false;
            this.lvwKontoplan.Name = "lvwKontoplan";
            this.lvwKontoplan.Size = new System.Drawing.Size(344, 375);
            this.lvwKontoplan.TabIndex = 0;
            this.lvwKontoplan.UseCompatibleStateImageBehavior = false;
            this.lvwKontoplan.View = System.Windows.Forms.View.Details;
            this.lvwKontoplan.SelectedIndexChanged += new System.EventHandler(this.lvwKontoplan_SelectedIndexChanged);
            // 
            // columnHeaderKontonr
            // 
            this.columnHeaderKontonr.Text = "Kontonr";
            // 
            // columnHeaderKontonavn
            // 
            this.columnHeaderKontonavn.Text = "Kontonavn";
            this.columnHeaderKontonavn.Width = 194;
            // 
            // columnHeaderMoms
            // 
            this.columnHeaderMoms.Text = "Moms";
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
            this.splitContainer2.Panel1.Controls.Add(this.lvwKontoplan);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxKreditor);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxDebitor);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxStatus);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxDrift);
            this.splitContainer2.Panel2.Controls.Add(this.cmdCancel);
            this.splitContainer2.Panel2.Controls.Add(this.cmdOK);
            this.splitContainer2.Panel2.Controls.Add(this.checkBoxMedsaldo);
            this.splitContainer2.Size = new System.Drawing.Size(436, 375);
            this.splitContainer2.SplitterDistance = 344;
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
            // checkBoxMedsaldo
            // 
            this.checkBoxMedsaldo.AutoSize = true;
            this.checkBoxMedsaldo.Checked = global::Trans2SummaHDA.Properties.Settings.Default.frmKontoplanListCheckboxmedsaldo;
            this.checkBoxMedsaldo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMedsaldo.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::Trans2SummaHDA.Properties.Settings.Default, "frmKontoplanListCheckboxmedsaldo", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxMedsaldo.Location = new System.Drawing.Point(9, 144);
            this.checkBoxMedsaldo.Name = "checkBoxMedsaldo";
            this.checkBoxMedsaldo.Size = new System.Drawing.Size(72, 43);
            this.checkBoxMedsaldo.TabIndex = 1;
            this.checkBoxMedsaldo.Text = "Vis kun\r\nkonti med\r\nsaldo";
            this.checkBoxMedsaldo.UseVisualStyleBackColor = true;
            this.checkBoxMedsaldo.CheckedChanged += new System.EventHandler(this.checkBoxMedsaldo_CheckedChanged);
            // 
            // checkBoxDrift
            // 
            this.checkBoxDrift.AutoSize = true;
            this.checkBoxDrift.Checked = true;
            this.checkBoxDrift.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrift.Location = new System.Drawing.Point(9, 87);
            this.checkBoxDrift.Name = "checkBoxDrift";
            this.checkBoxDrift.Size = new System.Drawing.Size(45, 17);
            this.checkBoxDrift.TabIndex = 3;
            this.checkBoxDrift.Text = "Drift";
            this.checkBoxDrift.UseVisualStyleBackColor = true;
            this.checkBoxDrift.CheckedChanged += new System.EventHandler(this.checkBoxDrift_CheckedChanged);
            // 
            // checkBoxDebitor
            // 
            this.checkBoxDebitor.AutoSize = true;
            this.checkBoxDebitor.Checked = true;
            this.checkBoxDebitor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDebitor.Location = new System.Drawing.Point(9, 114);
            this.checkBoxDebitor.Name = "checkBoxDebitor";
            this.checkBoxDebitor.Size = new System.Drawing.Size(60, 17);
            this.checkBoxDebitor.TabIndex = 4;
            this.checkBoxDebitor.Text = "Debitor";
            this.checkBoxDebitor.UseVisualStyleBackColor = true;
            this.checkBoxDebitor.CheckedChanged += new System.EventHandler(this.checkBoxDebitor_CheckedChanged);
            // 
            // checkBoxKreditor
            // 
            this.checkBoxKreditor.AutoSize = true;
            this.checkBoxKreditor.Checked = true;
            this.checkBoxKreditor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKreditor.Location = new System.Drawing.Point(9, 128);
            this.checkBoxKreditor.Name = "checkBoxKreditor";
            this.checkBoxKreditor.Size = new System.Drawing.Size(62, 17);
            this.checkBoxKreditor.TabIndex = 5;
            this.checkBoxKreditor.Text = "Kreditor";
            this.checkBoxKreditor.UseVisualStyleBackColor = true;
            this.checkBoxKreditor.CheckedChanged += new System.EventHandler(this.checkBoxKreditor_CheckedChanged);
            // 
            // checkBoxStatus
            // 
            this.checkBoxStatus.AutoSize = true;
            this.checkBoxStatus.Checked = true;
            this.checkBoxStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStatus.Location = new System.Drawing.Point(9, 100);
            this.checkBoxStatus.Name = "checkBoxStatus";
            this.checkBoxStatus.Size = new System.Drawing.Size(56, 17);
            this.checkBoxStatus.TabIndex = 3;
            this.checkBoxStatus.Text = "Status";
            this.checkBoxStatus.UseVisualStyleBackColor = true;
            this.checkBoxStatus.CheckedChanged += new System.EventHandler(this.checkBoxStatus_CheckedChanged);
            // 
            // FrmKontoplanList
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 413);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2SummaHDA.Properties.Settings.Default, "frmKontoplanListLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2SummaHDA.Properties.Settings.Default.frmKontoplanListLocation;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmKontoplanList";
            this.ShowIcon = false;
            this.Text = "Kontoplan";
            this.Load += new System.EventHandler(this.FrmKontoplanList_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwKontoplan;
        private System.Windows.Forms.ColumnHeader columnHeaderKontonr;
        private System.Windows.Forms.ColumnHeader columnHeaderKontonavn;
        private System.Windows.Forms.ColumnHeader columnHeaderMoms;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBoxMedsaldo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.TextBox toolStripTextBoxFind;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.CheckBox checkBoxDrift;
        private System.Windows.Forms.CheckBox checkBoxKreditor;
        private System.Windows.Forms.CheckBox checkBoxDebitor;
        private System.Windows.Forms.CheckBox checkBoxStatus;
    }
}