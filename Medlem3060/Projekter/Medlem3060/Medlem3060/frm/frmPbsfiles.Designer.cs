namespace nsPuls3060
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
            this.tblpbsfilesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filenameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sizeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.atimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mtimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.permDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transmittimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pbsforsendelseidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblpbsforsendelseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblpbsfilesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.pathDataGridViewTextBoxColumn,
            this.filenameDataGridViewTextBoxColumn,
            this.sizeDataGridViewTextBoxColumn,
            this.atimeDataGridViewTextBoxColumn,
            this.mtimeDataGridViewTextBoxColumn,
            this.permDataGridViewTextBoxColumn,
            this.uidDataGridViewTextBoxColumn,
            this.gidDataGridViewTextBoxColumn,
            this.transmittimeDataGridViewTextBoxColumn,
            this.pbsforsendelseidDataGridViewTextBoxColumn,
            this.tblpbsforsendelseDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tblpbsfilesBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(856, 511);
            this.dataGridView1.TabIndex = 0;
            // 
            // tblpbsfilesBindingSource
            // 
            this.tblpbsfilesBindingSource.DataSource = typeof(nsPuls3060.Tblpbsfiles);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeDataGridViewTextBoxColumn.Visible = false;
            // 
            // pathDataGridViewTextBoxColumn
            // 
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            this.pathDataGridViewTextBoxColumn.HeaderText = "Path";
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            this.pathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filenameDataGridViewTextBoxColumn
            // 
            this.filenameDataGridViewTextBoxColumn.DataPropertyName = "Filename";
            this.filenameDataGridViewTextBoxColumn.HeaderText = "Filename";
            this.filenameDataGridViewTextBoxColumn.Name = "filenameDataGridViewTextBoxColumn";
            this.filenameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sizeDataGridViewTextBoxColumn
            // 
            this.sizeDataGridViewTextBoxColumn.DataPropertyName = "Size";
            this.sizeDataGridViewTextBoxColumn.HeaderText = "Size";
            this.sizeDataGridViewTextBoxColumn.Name = "sizeDataGridViewTextBoxColumn";
            this.sizeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // atimeDataGridViewTextBoxColumn
            // 
            this.atimeDataGridViewTextBoxColumn.DataPropertyName = "Atime";
            this.atimeDataGridViewTextBoxColumn.HeaderText = "Atime";
            this.atimeDataGridViewTextBoxColumn.Name = "atimeDataGridViewTextBoxColumn";
            this.atimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // mtimeDataGridViewTextBoxColumn
            // 
            this.mtimeDataGridViewTextBoxColumn.DataPropertyName = "Mtime";
            this.mtimeDataGridViewTextBoxColumn.HeaderText = "Mtime";
            this.mtimeDataGridViewTextBoxColumn.Name = "mtimeDataGridViewTextBoxColumn";
            this.mtimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // permDataGridViewTextBoxColumn
            // 
            this.permDataGridViewTextBoxColumn.DataPropertyName = "Perm";
            this.permDataGridViewTextBoxColumn.HeaderText = "Perm";
            this.permDataGridViewTextBoxColumn.Name = "permDataGridViewTextBoxColumn";
            this.permDataGridViewTextBoxColumn.ReadOnly = true;
            this.permDataGridViewTextBoxColumn.Visible = false;
            // 
            // uidDataGridViewTextBoxColumn
            // 
            this.uidDataGridViewTextBoxColumn.DataPropertyName = "Uid";
            this.uidDataGridViewTextBoxColumn.HeaderText = "Uid";
            this.uidDataGridViewTextBoxColumn.Name = "uidDataGridViewTextBoxColumn";
            this.uidDataGridViewTextBoxColumn.ReadOnly = true;
            this.uidDataGridViewTextBoxColumn.Visible = false;
            // 
            // gidDataGridViewTextBoxColumn
            // 
            this.gidDataGridViewTextBoxColumn.DataPropertyName = "Gid";
            this.gidDataGridViewTextBoxColumn.HeaderText = "Gid";
            this.gidDataGridViewTextBoxColumn.Name = "gidDataGridViewTextBoxColumn";
            this.gidDataGridViewTextBoxColumn.ReadOnly = true;
            this.gidDataGridViewTextBoxColumn.Visible = false;
            // 
            // transmittimeDataGridViewTextBoxColumn
            // 
            this.transmittimeDataGridViewTextBoxColumn.DataPropertyName = "Transmittime";
            this.transmittimeDataGridViewTextBoxColumn.HeaderText = "Transmittime";
            this.transmittimeDataGridViewTextBoxColumn.Name = "transmittimeDataGridViewTextBoxColumn";
            this.transmittimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pbsforsendelseidDataGridViewTextBoxColumn
            // 
            this.pbsforsendelseidDataGridViewTextBoxColumn.DataPropertyName = "Pbsforsendelseid";
            this.pbsforsendelseidDataGridViewTextBoxColumn.HeaderText = "Pbsforsendelseid";
            this.pbsforsendelseidDataGridViewTextBoxColumn.Name = "pbsforsendelseidDataGridViewTextBoxColumn";
            this.pbsforsendelseidDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tblpbsforsendelseDataGridViewTextBoxColumn
            // 
            this.tblpbsforsendelseDataGridViewTextBoxColumn.DataPropertyName = "Tblpbsforsendelse";
            this.tblpbsforsendelseDataGridViewTextBoxColumn.HeaderText = "Tblpbsforsendelse";
            this.tblpbsforsendelseDataGridViewTextBoxColumn.Name = "tblpbsforsendelseDataGridViewTextBoxColumn";
            this.tblpbsforsendelseDataGridViewTextBoxColumn.ReadOnly = true;
            this.tblpbsforsendelseDataGridViewTextBoxColumn.Visible = false;
            // 
            // FrmPbsfiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmPbsfilesSize;
            this.Controls.Add(this.dataGridView1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmPbsfilesPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmPbsfilesSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmPbsfilesPoint;
            this.Name = "FrmPbsfiles";
            this.Text = "Pbsfiles";
            this.Load += new System.EventHandler(this.FrmPbsfiles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblpbsfilesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource tblpbsfilesBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filenameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn atimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mtimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn permDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn uidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transmittimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pbsforsendelseidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblpbsforsendelseDataGridViewTextBoxColumn;
    }
}