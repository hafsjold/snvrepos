namespace nsInfo3060
{
    partial class FrmMedlemList
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBoxKreditor = new System.Windows.Forms.CheckBox();
            this.checkBoxDebitor = new System.Windows.Forms.CheckBox();
            this.checkBoxStatus = new System.Windows.Forms.CheckBox();
            this.checkBoxDrift = new System.Windows.Forms.CheckBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.checkBoxMedsaldo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdFind = new System.Windows.Forms.Button();
            this.toolStripTextBoxFind = new System.Windows.Forms.TextBox();
            this.lvwMedlemmer = new System.Windows.Forms.ListView();
            this.columnHeaderNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNavn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKaldenavn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAdresse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderPostnr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBynavn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTelefon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEmail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.splitContainer1.Size = new System.Drawing.Size(976, 413);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvwMedlemmer);
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
            this.splitContainer2.Size = new System.Drawing.Size(976, 375);
            this.splitContainer2.SplitterDistance = 880;
            this.splitContainer2.TabIndex = 0;
            // 
            // checkBoxKreditor
            // 
            this.checkBoxKreditor.AutoSize = true;
            this.checkBoxKreditor.Checked = true;
            this.checkBoxKreditor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKreditor.Location = new System.Drawing.Point(7, 126);
            this.checkBoxKreditor.Name = "checkBoxKreditor";
            this.checkBoxKreditor.Size = new System.Drawing.Size(62, 17);
            this.checkBoxKreditor.TabIndex = 12;
            this.checkBoxKreditor.Text = "Kreditor";
            this.checkBoxKreditor.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebitor
            // 
            this.checkBoxDebitor.AutoSize = true;
            this.checkBoxDebitor.Checked = true;
            this.checkBoxDebitor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDebitor.Location = new System.Drawing.Point(7, 112);
            this.checkBoxDebitor.Name = "checkBoxDebitor";
            this.checkBoxDebitor.Size = new System.Drawing.Size(60, 17);
            this.checkBoxDebitor.TabIndex = 11;
            this.checkBoxDebitor.Text = "Debitor";
            this.checkBoxDebitor.UseVisualStyleBackColor = true;
            // 
            // checkBoxStatus
            // 
            this.checkBoxStatus.AutoSize = true;
            this.checkBoxStatus.Checked = true;
            this.checkBoxStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxStatus.Location = new System.Drawing.Point(7, 98);
            this.checkBoxStatus.Name = "checkBoxStatus";
            this.checkBoxStatus.Size = new System.Drawing.Size(56, 17);
            this.checkBoxStatus.TabIndex = 9;
            this.checkBoxStatus.Text = "Status";
            this.checkBoxStatus.UseVisualStyleBackColor = true;
            // 
            // checkBoxDrift
            // 
            this.checkBoxDrift.AutoSize = true;
            this.checkBoxDrift.Checked = true;
            this.checkBoxDrift.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrift.Location = new System.Drawing.Point(7, 85);
            this.checkBoxDrift.Name = "checkBoxDrift";
            this.checkBoxDrift.Size = new System.Drawing.Size(45, 17);
            this.checkBoxDrift.TabIndex = 10;
            this.checkBoxDrift.Text = "Drift";
            this.checkBoxDrift.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(7, 54);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Annuler";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(7, 18);
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
            this.checkBoxMedsaldo.Checked = true;
            this.checkBoxMedsaldo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMedsaldo.Location = new System.Drawing.Point(7, 142);
            this.checkBoxMedsaldo.Name = "checkBoxMedsaldo";
            this.checkBoxMedsaldo.Size = new System.Drawing.Size(72, 43);
            this.checkBoxMedsaldo.TabIndex = 6;
            this.checkBoxMedsaldo.Text = "Vis kun\r\nkonti med\r\nsaldo";
            this.checkBoxMedsaldo.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Find";
            // 
            // cmdFind
            // 
            this.cmdFind.Location = new System.Drawing.Point(137, 6);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(39, 23);
            this.cmdFind.TabIndex = 4;
            this.cmdFind.Text = "Søg";
            this.cmdFind.UseVisualStyleBackColor = true;
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.Location = new System.Drawing.Point(35, 7);
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(100, 20);
            this.toolStripTextBoxFind.TabIndex = 3;
            // 
            // lvwMedlemmer
            // 
            this.lvwMedlemmer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderNr,
            this.columnHeaderNavn,
            this.columnHeaderKaldenavn,
            this.columnHeaderAdresse,
            this.columnHeaderPostnr,
            this.columnHeaderBynavn,
            this.columnHeaderTelefon,
            this.columnHeaderEmail});
            this.lvwMedlemmer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwMedlemmer.FullRowSelect = true;
            this.lvwMedlemmer.GridLines = true;
            this.lvwMedlemmer.Location = new System.Drawing.Point(0, 0);
            this.lvwMedlemmer.MultiSelect = false;
            this.lvwMedlemmer.Name = "lvwMedlemmer";
            this.lvwMedlemmer.Size = new System.Drawing.Size(880, 375);
            this.lvwMedlemmer.TabIndex = 1;
            this.lvwMedlemmer.UseCompatibleStateImageBehavior = false;
            this.lvwMedlemmer.View = System.Windows.Forms.View.Details;
            this.lvwMedlemmer.SelectedIndexChanged += new System.EventHandler(this.lvwMedlemmer_SelectedIndexChanged);
            // 
            // columnHeaderNr
            // 
            this.columnHeaderNr.Text = "Nr";
            this.columnHeaderNr.Width = 41;
            // 
            // columnHeaderNavn
            // 
            this.columnHeaderNavn.Text = "Navn";
            this.columnHeaderNavn.Width = 172;
            // 
            // columnHeaderKaldenavn
            // 
            this.columnHeaderKaldenavn.Text = "Kaldenavn";
            this.columnHeaderKaldenavn.Width = 82;
            // 
            // columnHeaderAdresse
            // 
            this.columnHeaderAdresse.Text = "Adresse";
            this.columnHeaderAdresse.Width = 170;
            // 
            // columnHeaderPostnr
            // 
            this.columnHeaderPostnr.Text = "Postnr";
            this.columnHeaderPostnr.Width = 47;
            // 
            // columnHeaderBynavn
            // 
            this.columnHeaderBynavn.Text = "Bynavn";
            this.columnHeaderBynavn.Width = 105;
            // 
            // columnHeaderTelefon
            // 
            this.columnHeaderTelefon.Text = "Telefon";
            this.columnHeaderTelefon.Width = 83;
            // 
            // columnHeaderEmail
            // 
            this.columnHeaderEmail.Text = "Email";
            this.columnHeaderEmail.Width = 157;
            // 
            // FrmMedlemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 413);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsInfo3060.Properties.Settings.Default, "frmMedlemListLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsInfo3060.Properties.Settings.Default.frmMedlemListLocation;
            this.Name = "FrmMedlemList";
            this.Text = "Medlemmer";
            this.Load += new System.EventHandler(this.FrmMedlemList_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.TextBox toolStripTextBoxFind;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBoxKreditor;
        private System.Windows.Forms.CheckBox checkBoxDebitor;
        private System.Windows.Forms.CheckBox checkBoxStatus;
        private System.Windows.Forms.CheckBox checkBoxDrift;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.CheckBox checkBoxMedsaldo;
        private System.Windows.Forms.ListView lvwMedlemmer;
        private System.Windows.Forms.ColumnHeader columnHeaderNr;
        private System.Windows.Forms.ColumnHeader columnHeaderNavn;
        private System.Windows.Forms.ColumnHeader columnHeaderKaldenavn;
        private System.Windows.Forms.ColumnHeader columnHeaderAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderBynavn;
        private System.Windows.Forms.ColumnHeader columnHeaderTelefon;
        private System.Windows.Forms.ColumnHeader columnHeaderEmail;
    }
}