namespace Trans2SummaHDA
{
    partial class FrmKladder
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
            System.Windows.Forms.Label datoLabel;
            System.Windows.Forms.Label bilagLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKladder));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmdKopier = new System.Windows.Forms.Button();
            this.cmdSog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSogeord = new System.Windows.Forms.TextBox();
            this.datoTextBox = new System.Windows.Forms.TextBox();
            this.tblbilagBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bilagTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tblkladderDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tblkladderBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.pidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regnskabidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bilagpidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tekstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afstemningskontoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.belobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momskodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fakturaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblbilagDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            datoLabel = new System.Windows.Forms.Label();
            bilagLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderDataGridView)).BeginInit();
            this.contextMenuLineCopyPaste.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingNavigator)).BeginInit();
            this.tblbilagBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(4, 19);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(33, 13);
            datoLabel.TabIndex = 4;
            datoLabel.Text = "Dato:";
            // 
            // bilagLabel
            // 
            bilagLabel.AutoSize = true;
            bilagLabel.Location = new System.Drawing.Point(117, 19);
            bilagLabel.Name = "bilagLabel";
            bilagLabel.Size = new System.Drawing.Size(33, 13);
            bilagLabel.TabIndex = 6;
            bilagLabel.Text = "Bilag:";
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
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.cmdKopier);
            this.splitContainer1.Panel1.Controls.Add(this.cmdSog);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSogeord);
            this.splitContainer1.Panel1.Controls.Add(this.datoTextBox);
            this.splitContainer1.Panel1.Controls.Add(bilagLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bilagTextBox);
            this.splitContainer1.Panel1.Controls.Add(datoLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(530, 259);
            this.splitContainer1.SplitterDistance = 52;
            this.splitContainer1.TabIndex = 7;
            // 
            // cmdKopier
            // 
            this.cmdKopier.Location = new System.Drawing.Point(416, 16);
            this.cmdKopier.Name = "cmdKopier";
            this.cmdKopier.Size = new System.Drawing.Size(45, 23);
            this.cmdKopier.TabIndex = 12;
            this.cmdKopier.Text = "Kopier";
            this.cmdKopier.UseVisualStyleBackColor = true;
            this.cmdKopier.Click += new System.EventHandler(this.cmdKopier_Click);
            // 
            // cmdSog
            // 
            this.cmdSog.Location = new System.Drawing.Point(364, 15);
            this.cmdSog.Name = "cmdSog";
            this.cmdSog.Size = new System.Drawing.Size(43, 23);
            this.cmdSog.TabIndex = 11;
            this.cmdSog.Text = "Søg";
            this.cmdSog.UseVisualStyleBackColor = true;
            this.cmdSog.Click += new System.EventHandler(this.cmdSog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Søgeord";
            // 
            // textBoxSogeord
            // 
            this.textBoxSogeord.Location = new System.Drawing.Point(254, 16);
            this.textBoxSogeord.Name = "textBoxSogeord";
            this.textBoxSogeord.Size = new System.Drawing.Size(102, 20);
            this.textBoxSogeord.TabIndex = 9;
            // 
            // datoTextBox
            // 
            this.datoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.datoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblbilagBindingSource, "Dato", true));
            this.datoTextBox.Location = new System.Drawing.Point(37, 16);
            this.datoTextBox.Name = "datoTextBox";
            this.datoTextBox.ReadOnly = true;
            this.datoTextBox.Size = new System.Drawing.Size(75, 20);
            this.datoTextBox.TabIndex = 8;
            // 
            // tblbilagBindingSource
            // 
            this.tblbilagBindingSource.DataSource = typeof(Trans2SummaHDA.tblbilag);
            // 
            // bilagTextBox
            // 
            this.bilagTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.bilagTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblbilagBindingSource, "Bilag", true));
            this.bilagTextBox.Location = new System.Drawing.Point(154, 16);
            this.bilagTextBox.Name = "bilagTextBox";
            this.bilagTextBox.ReadOnly = true;
            this.bilagTextBox.Size = new System.Drawing.Size(46, 20);
            this.bilagTextBox.TabIndex = 7;
            this.bilagTextBox.TextChanged += new System.EventHandler(this.bilagTextBox_TextChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.Controls.Add(this.tblkladderDataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tblbilagBindingNavigator);
            this.splitContainer2.Size = new System.Drawing.Size(530, 203);
            this.splitContainer2.SplitterDistance = 174;
            this.splitContainer2.TabIndex = 0;
            // 
            // tblkladderDataGridView
            // 
            this.tblkladderDataGridView.AllowUserToAddRows = false;
            this.tblkladderDataGridView.AllowUserToDeleteRows = false;
            this.tblkladderDataGridView.AutoGenerateColumns = false;
            this.tblkladderDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.tblkladderDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblkladderDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pidDataGridViewTextBoxColumn,
            this.regnskabidDataGridViewTextBoxColumn,
            this.bilagpidDataGridViewTextBoxColumn,
            this.tekstDataGridViewTextBoxColumn,
            this.afstemningskontoDataGridViewTextBoxColumn,
            this.belobDataGridViewTextBoxColumn,
            this.kontoDataGridViewTextBoxColumn,
            this.momskodeDataGridViewTextBoxColumn,
            this.fakturaDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn,
            this.tblbilagDataGridViewTextBoxColumn});
            this.tblkladderDataGridView.ContextMenuStrip = this.contextMenuLineCopyPaste;
            this.tblkladderDataGridView.DataSource = this.tblkladderBindingSource;
            this.tblkladderDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblkladderDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblkladderDataGridView.Name = "tblkladderDataGridView";
            this.tblkladderDataGridView.ReadOnly = true;
            this.tblkladderDataGridView.Size = new System.Drawing.Size(530, 174);
            this.tblkladderDataGridView.TabIndex = 0;
            // 
            // contextMenuLineCopyPaste
            // 
            this.contextMenuLineCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuLineCopyPaste.Name = "contextMenuLineCopyPaste";
            this.contextMenuLineCopyPaste.Size = new System.Drawing.Size(103, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyMenuLineCopyPastItem_Click);
            // 
            // tblkladderBindingSource
            // 
            this.tblkladderBindingSource.DataMember = "tblkladders";
            this.tblkladderBindingSource.DataSource = this.tblbilagBindingSource;
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
            this.tblbilagBindingNavigator.Size = new System.Drawing.Size(530, 25);
            this.tblbilagBindingNavigator.TabIndex = 8;
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
            // pidDataGridViewTextBoxColumn
            // 
            this.pidDataGridViewTextBoxColumn.DataPropertyName = "pid";
            this.pidDataGridViewTextBoxColumn.HeaderText = "pid";
            this.pidDataGridViewTextBoxColumn.Name = "pidDataGridViewTextBoxColumn";
            this.pidDataGridViewTextBoxColumn.ReadOnly = true;
            this.pidDataGridViewTextBoxColumn.Visible = false;
            // 
            // regnskabidDataGridViewTextBoxColumn
            // 
            this.regnskabidDataGridViewTextBoxColumn.DataPropertyName = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn.HeaderText = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn.Name = "regnskabidDataGridViewTextBoxColumn";
            this.regnskabidDataGridViewTextBoxColumn.ReadOnly = true;
            this.regnskabidDataGridViewTextBoxColumn.Visible = false;
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
            // afstemningskontoDataGridViewTextBoxColumn
            // 
            this.afstemningskontoDataGridViewTextBoxColumn.DataPropertyName = "afstemningskonto";
            this.afstemningskontoDataGridViewTextBoxColumn.HeaderText = "Afstem";
            this.afstemningskontoDataGridViewTextBoxColumn.Name = "afstemningskontoDataGridViewTextBoxColumn";
            this.afstemningskontoDataGridViewTextBoxColumn.ReadOnly = true;
            this.afstemningskontoDataGridViewTextBoxColumn.Width = 60;
            // 
            // belobDataGridViewTextBoxColumn
            // 
            this.belobDataGridViewTextBoxColumn.DataPropertyName = "belob";
            this.belobDataGridViewTextBoxColumn.HeaderText = "Beløb";
            this.belobDataGridViewTextBoxColumn.Name = "belobDataGridViewTextBoxColumn";
            this.belobDataGridViewTextBoxColumn.ReadOnly = true;
            this.belobDataGridViewTextBoxColumn.Width = 60;
            // 
            // kontoDataGridViewTextBoxColumn
            // 
            this.kontoDataGridViewTextBoxColumn.DataPropertyName = "konto";
            this.kontoDataGridViewTextBoxColumn.HeaderText = "Konto";
            this.kontoDataGridViewTextBoxColumn.Name = "kontoDataGridViewTextBoxColumn";
            this.kontoDataGridViewTextBoxColumn.ReadOnly = true;
            this.kontoDataGridViewTextBoxColumn.Width = 60;
            // 
            // momskodeDataGridViewTextBoxColumn
            // 
            this.momskodeDataGridViewTextBoxColumn.DataPropertyName = "momskode";
            this.momskodeDataGridViewTextBoxColumn.HeaderText = "MK";
            this.momskodeDataGridViewTextBoxColumn.Name = "momskodeDataGridViewTextBoxColumn";
            this.momskodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.momskodeDataGridViewTextBoxColumn.Width = 40;
            // 
            // fakturaDataGridViewTextBoxColumn
            // 
            this.fakturaDataGridViewTextBoxColumn.DataPropertyName = "faktura";
            this.fakturaDataGridViewTextBoxColumn.HeaderText = "Faktura";
            this.fakturaDataGridViewTextBoxColumn.Name = "fakturaDataGridViewTextBoxColumn";
            this.fakturaDataGridViewTextBoxColumn.ReadOnly = true;
            this.fakturaDataGridViewTextBoxColumn.Width = 40;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // tblbilagDataGridViewTextBoxColumn
            // 
            this.tblbilagDataGridViewTextBoxColumn.DataPropertyName = "tblbilag";
            this.tblbilagDataGridViewTextBoxColumn.HeaderText = "tblbilag";
            this.tblbilagDataGridViewTextBoxColumn.Name = "tblbilagDataGridViewTextBoxColumn";
            this.tblbilagDataGridViewTextBoxColumn.ReadOnly = true;
            this.tblbilagDataGridViewTextBoxColumn.Visible = false;
            // 
            // FrmKladder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Trans2SummaHDA.Properties.Settings.Default.frmKladderSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2SummaHDA.Properties.Settings.Default, "frmKladderLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Trans2SummaHDA.Properties.Settings.Default, "frmKladderSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2SummaHDA.Properties.Settings.Default.frmKladderLocation;
            this.Name = "FrmKladder";
            this.Text = "Kladder";
            this.Load += new System.EventHandler(this.FrmKladder_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingSource)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderDataGridView)).EndInit();
            this.contextMenuLineCopyPaste.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingNavigator)).EndInit();
            this.tblbilagBindingNavigator.ResumeLayout(false);
            this.tblbilagBindingNavigator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox bilagTextBox;
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
        private System.Windows.Forms.DataGridView tblkladderDataGridView;
        private System.Windows.Forms.BindingSource tblkladderBindingSource;
        private System.Windows.Forms.TextBox datoTextBox;
        private System.Windows.Forms.Button cmdKopier;
        private System.Windows.Forms.Button cmdSog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSogeord;

        private System.Windows.Forms.ContextMenuStrip contextMenuLineCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regnskabidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bilagpidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tekstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn afstemningskontoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn belobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn momskodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fakturaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblbilagDataGridViewTextBoxColumn;
    }
}