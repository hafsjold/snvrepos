namespace Trans2SummaHDA
{
    partial class FrmBilag
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBilag));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tblbilagDataGridView = new System.Windows.Forms.DataGridView();
            this.pidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regnskabidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bilagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.udskrivDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tblbilagBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbltransDataGridView = new System.Windows.Forms.DataGridView();
            this.tbltransBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblbilagBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regnskabidDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skjulDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bilagpidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tekstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontonrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontonavnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kreditDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.belobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afstemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblafstemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblbilagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbltransDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbltransBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingNavigator)).BeginInit();
            this.tblbilagBindingNavigator.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tblbilagBindingNavigator);
            this.splitContainer1.Size = new System.Drawing.Size(870, 346);
            this.splitContainer1.SplitterDistance = 317;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.DataBindings.Add(new System.Windows.Forms.Binding("SplitterDistance", global::Trans2SummaHDA.Properties.Settings.Default, "frmBilagSplitDistance", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.tblbilagDataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.tbltransDataGridView);
            this.splitContainer2.Size = new System.Drawing.Size(870, 317);
            this.splitContainer2.SplitterDistance = global::Trans2SummaHDA.Properties.Settings.Default.frmBilagSplitDistance;
            this.splitContainer2.TabIndex = 0;
            // 
            // tblbilagDataGridView
            // 
            this.tblbilagDataGridView.AllowUserToAddRows = false;
            this.tblbilagDataGridView.AllowUserToDeleteRows = false;
            this.tblbilagDataGridView.AutoGenerateColumns = false;
            this.tblbilagDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblbilagDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pidDataGridViewTextBoxColumn,
            this.regnskabidDataGridViewTextBoxColumn,
            this.bilagDataGridViewTextBoxColumn,
            this.datoDataGridViewTextBoxColumn,
            this.udskrivDataGridViewTextBoxColumn});
            this.tblbilagDataGridView.DataSource = this.tblbilagBindingSource;
            this.tblbilagDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblbilagDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblbilagDataGridView.Name = "tblbilagDataGridView";
            this.tblbilagDataGridView.Size = new System.Drawing.Size(246, 317);
            this.tblbilagDataGridView.TabIndex = 0;
            // 
            // pidDataGridViewTextBoxColumn
            // 
            this.pidDataGridViewTextBoxColumn.DataPropertyName = "pid";
            this.pidDataGridViewTextBoxColumn.HeaderText = "pid";
            this.pidDataGridViewTextBoxColumn.Name = "pidDataGridViewTextBoxColumn";
            this.pidDataGridViewTextBoxColumn.Visible = false;
            // 
            // regnskabidDataGridViewTextBoxColumn
            // 
            this.regnskabidDataGridViewTextBoxColumn.DataPropertyName = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn.HeaderText = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn.Name = "regnskabidDataGridViewTextBoxColumn";
            this.regnskabidDataGridViewTextBoxColumn.Visible = false;
            // 
            // bilagDataGridViewTextBoxColumn
            // 
            this.bilagDataGridViewTextBoxColumn.DataPropertyName = "bilag";
            this.bilagDataGridViewTextBoxColumn.HeaderText = "Bilag";
            this.bilagDataGridViewTextBoxColumn.Name = "bilagDataGridViewTextBoxColumn";
            this.bilagDataGridViewTextBoxColumn.Width = 35;
            // 
            // datoDataGridViewTextBoxColumn
            // 
            this.datoDataGridViewTextBoxColumn.DataPropertyName = "dato";
            this.datoDataGridViewTextBoxColumn.HeaderText = "Dato";
            this.datoDataGridViewTextBoxColumn.Name = "datoDataGridViewTextBoxColumn";
            this.datoDataGridViewTextBoxColumn.Width = 80;
            // 
            // udskrivDataGridViewTextBoxColumn
            // 
            this.udskrivDataGridViewTextBoxColumn.DataPropertyName = "udskriv";
            this.udskrivDataGridViewTextBoxColumn.HeaderText = "Udskriv";
            this.udskrivDataGridViewTextBoxColumn.Name = "udskrivDataGridViewTextBoxColumn";
            this.udskrivDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.udskrivDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.udskrivDataGridViewTextBoxColumn.Width = 60;
            // 
            // tblbilagBindingSource
            // 
            this.tblbilagBindingSource.DataSource = typeof(Trans2SummaHDA.tblbilag);
            // 
            // tbltransDataGridView
            // 
            this.tbltransDataGridView.AllowUserToAddRows = false;
            this.tbltransDataGridView.AllowUserToDeleteRows = false;
            this.tbltransDataGridView.AutoGenerateColumns = false;
            this.tbltransDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbltransDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pidDataGridViewTextBoxColumn1,
            this.regnskabidDataGridViewTextBoxColumn1,
            this.skjulDataGridViewTextBoxColumn,
            this.bilagpidDataGridViewTextBoxColumn,
            this.tekstDataGridViewTextBoxColumn,
            this.kontonrDataGridViewTextBoxColumn,
            this.kontonavnDataGridViewTextBoxColumn,
            this.momsDataGridViewTextBoxColumn,
            this.debetDataGridViewTextBoxColumn,
            this.kreditDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn,
            this.nrDataGridViewTextBoxColumn,
            this.belobDataGridViewTextBoxColumn,
            this.afstemDataGridViewTextBoxColumn,
            this.tblafstemDataGridViewTextBoxColumn,
            this.tblbilagDataGridViewTextBoxColumn});
            this.tbltransDataGridView.DataSource = this.tbltransBindingSource;
            this.tbltransDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbltransDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tbltransDataGridView.Name = "tbltransDataGridView";
            this.tbltransDataGridView.ReadOnly = true;
            this.tbltransDataGridView.Size = new System.Drawing.Size(620, 317);
            this.tbltransDataGridView.TabIndex = 0;
            // 
            // tbltransBindingSource
            // 
            this.tbltransBindingSource.DataMember = "tbltrans";
            this.tbltransBindingSource.DataSource = this.tblbilagBindingSource;
            // 
            // tblbilagBindingNavigator
            // 
            this.tblbilagBindingNavigator.AddNewItem = null;
            this.tblbilagBindingNavigator.BindingSource = this.tblbilagBindingSource;
            this.tblbilagBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblbilagBindingNavigator.DeleteItem = null;
            this.tblbilagBindingNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblbilagBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.tblbilagBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.tblbilagBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblbilagBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblbilagBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblbilagBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblbilagBindingNavigator.Name = "tblbilagBindingNavigator";
            this.tblbilagBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblbilagBindingNavigator.Size = new System.Drawing.Size(870, 25);
            this.tblbilagBindingNavigator.TabIndex = 1;
            this.tblbilagBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // pidDataGridViewTextBoxColumn1
            // 
            this.pidDataGridViewTextBoxColumn1.DataPropertyName = "pid";
            this.pidDataGridViewTextBoxColumn1.HeaderText = "pid";
            this.pidDataGridViewTextBoxColumn1.Name = "pidDataGridViewTextBoxColumn1";
            this.pidDataGridViewTextBoxColumn1.ReadOnly = true;
            this.pidDataGridViewTextBoxColumn1.Visible = false;
            // 
            // regnskabidDataGridViewTextBoxColumn1
            // 
            this.regnskabidDataGridViewTextBoxColumn1.DataPropertyName = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn1.HeaderText = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn1.Name = "regnskabidDataGridViewTextBoxColumn1";
            this.regnskabidDataGridViewTextBoxColumn1.ReadOnly = true;
            this.regnskabidDataGridViewTextBoxColumn1.Visible = false;
            // 
            // skjulDataGridViewTextBoxColumn
            // 
            this.skjulDataGridViewTextBoxColumn.DataPropertyName = "skjul";
            this.skjulDataGridViewTextBoxColumn.HeaderText = "skjul";
            this.skjulDataGridViewTextBoxColumn.Name = "skjulDataGridViewTextBoxColumn";
            this.skjulDataGridViewTextBoxColumn.ReadOnly = true;
            this.skjulDataGridViewTextBoxColumn.Visible = false;
            // 
            // bilagpidDataGridViewTextBoxColumn
            // 
            this.bilagpidDataGridViewTextBoxColumn.DataPropertyName = "bilagpid";
            this.bilagpidDataGridViewTextBoxColumn.HeaderText = "bilagpid";
            this.bilagpidDataGridViewTextBoxColumn.Name = "bilagpidDataGridViewTextBoxColumn";
            this.bilagpidDataGridViewTextBoxColumn.ReadOnly = true;
            this.bilagpidDataGridViewTextBoxColumn.Visible = false;
            // 
            // tekstDataGridViewTextBoxColumn
            // 
            this.tekstDataGridViewTextBoxColumn.DataPropertyName = "tekst";
            this.tekstDataGridViewTextBoxColumn.HeaderText = "Tekst";
            this.tekstDataGridViewTextBoxColumn.Name = "tekstDataGridViewTextBoxColumn";
            this.tekstDataGridViewTextBoxColumn.ReadOnly = true;
            this.tekstDataGridViewTextBoxColumn.Width = 200;
            // 
            // kontonrDataGridViewTextBoxColumn
            // 
            this.kontonrDataGridViewTextBoxColumn.DataPropertyName = "kontonr";
            this.kontonrDataGridViewTextBoxColumn.HeaderText = "Konto";
            this.kontonrDataGridViewTextBoxColumn.Name = "kontonrDataGridViewTextBoxColumn";
            this.kontonrDataGridViewTextBoxColumn.ReadOnly = true;
            this.kontonrDataGridViewTextBoxColumn.Width = 50;
            // 
            // kontonavnDataGridViewTextBoxColumn
            // 
            this.kontonavnDataGridViewTextBoxColumn.DataPropertyName = "kontonavn";
            this.kontonavnDataGridViewTextBoxColumn.HeaderText = "Kontonavn";
            this.kontonavnDataGridViewTextBoxColumn.Name = "kontonavnDataGridViewTextBoxColumn";
            this.kontonavnDataGridViewTextBoxColumn.ReadOnly = true;
            this.kontonavnDataGridViewTextBoxColumn.Width = 150;
            // 
            // momsDataGridViewTextBoxColumn
            // 
            this.momsDataGridViewTextBoxColumn.DataPropertyName = "moms";
            this.momsDataGridViewTextBoxColumn.HeaderText = "Moms";
            this.momsDataGridViewTextBoxColumn.Name = "momsDataGridViewTextBoxColumn";
            this.momsDataGridViewTextBoxColumn.ReadOnly = true;
            this.momsDataGridViewTextBoxColumn.Visible = false;
            // 
            // debetDataGridViewTextBoxColumn
            // 
            this.debetDataGridViewTextBoxColumn.DataPropertyName = "debet";
            this.debetDataGridViewTextBoxColumn.HeaderText = "Debet";
            this.debetDataGridViewTextBoxColumn.Name = "debetDataGridViewTextBoxColumn";
            this.debetDataGridViewTextBoxColumn.ReadOnly = true;
            this.debetDataGridViewTextBoxColumn.Width = 80;
            // 
            // kreditDataGridViewTextBoxColumn
            // 
            this.kreditDataGridViewTextBoxColumn.DataPropertyName = "kredit";
            this.kreditDataGridViewTextBoxColumn.HeaderText = "Kredit";
            this.kreditDataGridViewTextBoxColumn.Name = "kreditDataGridViewTextBoxColumn";
            this.kreditDataGridViewTextBoxColumn.ReadOnly = true;
            this.kreditDataGridViewTextBoxColumn.Width = 80;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // nrDataGridViewTextBoxColumn
            // 
            this.nrDataGridViewTextBoxColumn.DataPropertyName = "nr";
            this.nrDataGridViewTextBoxColumn.HeaderText = "nr";
            this.nrDataGridViewTextBoxColumn.Name = "nrDataGridViewTextBoxColumn";
            this.nrDataGridViewTextBoxColumn.ReadOnly = true;
            this.nrDataGridViewTextBoxColumn.Visible = false;
            // 
            // belobDataGridViewTextBoxColumn
            // 
            this.belobDataGridViewTextBoxColumn.DataPropertyName = "belob";
            this.belobDataGridViewTextBoxColumn.HeaderText = "belob";
            this.belobDataGridViewTextBoxColumn.Name = "belobDataGridViewTextBoxColumn";
            this.belobDataGridViewTextBoxColumn.ReadOnly = true;
            this.belobDataGridViewTextBoxColumn.Visible = false;
            // 
            // afstemDataGridViewTextBoxColumn
            // 
            this.afstemDataGridViewTextBoxColumn.DataPropertyName = "afstem";
            this.afstemDataGridViewTextBoxColumn.HeaderText = "afstem";
            this.afstemDataGridViewTextBoxColumn.Name = "afstemDataGridViewTextBoxColumn";
            this.afstemDataGridViewTextBoxColumn.ReadOnly = true;
            this.afstemDataGridViewTextBoxColumn.Visible = false;
            // 
            // tblafstemDataGridViewTextBoxColumn
            // 
            this.tblafstemDataGridViewTextBoxColumn.DataPropertyName = "tblafstem";
            this.tblafstemDataGridViewTextBoxColumn.HeaderText = "tblafstem";
            this.tblafstemDataGridViewTextBoxColumn.Name = "tblafstemDataGridViewTextBoxColumn";
            this.tblafstemDataGridViewTextBoxColumn.ReadOnly = true;
            this.tblafstemDataGridViewTextBoxColumn.Visible = false;
            // 
            // tblbilagDataGridViewTextBoxColumn
            // 
            this.tblbilagDataGridViewTextBoxColumn.DataPropertyName = "tblbilag";
            this.tblbilagDataGridViewTextBoxColumn.HeaderText = "tblbilag";
            this.tblbilagDataGridViewTextBoxColumn.Name = "tblbilagDataGridViewTextBoxColumn";
            this.tblbilagDataGridViewTextBoxColumn.ReadOnly = true;
            this.tblbilagDataGridViewTextBoxColumn.Visible = false;
            // 
            // FrmBilag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Trans2SummaHDA.Properties.Settings.Default.frmBilagSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2SummaHDA.Properties.Settings.Default, "frmBilagLoacation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Trans2SummaHDA.Properties.Settings.Default, "frmBilagSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2SummaHDA.Properties.Settings.Default.frmBilagLoacation;
            this.Name = "FrmBilag";
            this.Text = "Bilag";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmBilag_FormClosed);
            this.Load += new System.EventHandler(this.FrmBilag_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbltransDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbltransBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingNavigator)).EndInit();
            this.tblbilagBindingNavigator.ResumeLayout(false);
            this.tblbilagBindingNavigator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView tblbilagDataGridView;
        private System.Windows.Forms.BindingSource tblbilagBindingSource;
        private System.Windows.Forms.BindingNavigator tblbilagBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView tbltransDataGridView;
        private System.Windows.Forms.BindingSource tbltransBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regnskabidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bilagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn udskrivDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn regnskabidDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn skjulDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bilagpidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tekstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontonrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontonavnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn momsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn debetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kreditDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn belobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn afstemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblafstemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblbilagDataGridViewTextBoxColumn;

    }
}