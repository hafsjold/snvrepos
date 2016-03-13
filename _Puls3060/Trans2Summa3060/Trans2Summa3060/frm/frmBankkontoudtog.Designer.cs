namespace Trans2Summa3060
{
    partial class FrmBankkontoudtog
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
            System.Windows.Forms.Label tekstLabel;
            System.Windows.Forms.Label belobLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBankkontoudtog));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.labelcbBankkonto = new System.Windows.Forms.Label();
            this.labelcbTemplate = new System.Windows.Forms.Label();
            this.cbTemplate = new System.Windows.Forms.ComboBox();
            this.tbltemplateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmdPrivat = new System.Windows.Forms.Button();
            this.cbBankkonto = new System.Windows.Forms.ComboBox();
            this.tblkontoudtogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmdSog = new System.Windows.Forms.Button();
            this.labeltextBoxSogeord = new System.Windows.Forms.Label();
            this.textBoxSogeord = new System.Windows.Forms.TextBox();
            this.belobTextBox = new System.Windows.Forms.TextBox();
            this.tblbankkontoBindingSourceUafstemte = new System.Windows.Forms.BindingSource(this.components);
            this.tekstTextBox = new System.Windows.Forms.TextBox();
            this.datoTextBox = new System.Windows.Forms.TextBox();
            this.tblbankkontoBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tblbankkonto2DataGridView = new System.Windows.Forms.DataGridView();
            this.pidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skjulDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tekstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.belobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afstemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankkontoidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblafstemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblbankkontoBindingSourceAfstemte = new System.Windows.Forms.BindingSource(this.components);
            datoLabel = new System.Windows.Forms.Label();
            tekstLabel = new System.Windows.Forms.Label();
            belobLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbltemplateBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblkontoudtogBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkontoBindingSourceUafstemte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkontoBindingNavigator)).BeginInit();
            this.tblbankkontoBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkonto2DataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkontoBindingSourceAfstemte)).BeginInit();
            this.SuspendLayout();
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(12, 38);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(30, 13);
            datoLabel.TabIndex = 1;
            datoLabel.Text = "Dato";
            // 
            // tekstLabel
            // 
            tekstLabel.AutoSize = true;
            tekstLabel.Location = new System.Drawing.Point(91, 38);
            tekstLabel.Name = "tekstLabel";
            tekstLabel.Size = new System.Drawing.Size(34, 13);
            tekstLabel.TabIndex = 2;
            tekstLabel.Text = "Tekst";
            // 
            // belobLabel
            // 
            belobLabel.AutoSize = true;
            belobLabel.Location = new System.Drawing.Point(279, 38);
            belobLabel.Name = "belobLabel";
            belobLabel.Size = new System.Drawing.Size(34, 13);
            belobLabel.TabIndex = 4;
            belobLabel.Text = "Beløb";
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tblbankkonto2DataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(393, 416);
            this.splitContainer1.SplitterDistance = 146;
            this.splitContainer1.TabIndex = 7;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.AutoScroll = true;
            this.splitContainer3.Panel1.Controls.Add(this.labelcbBankkonto);
            this.splitContainer3.Panel1.Controls.Add(this.labelcbTemplate);
            this.splitContainer3.Panel1.Controls.Add(this.cbTemplate);
            this.splitContainer3.Panel1.Controls.Add(this.cmdPrivat);
            this.splitContainer3.Panel1.Controls.Add(this.cbBankkonto);
            this.splitContainer3.Panel1.Controls.Add(this.cmdSog);
            this.splitContainer3.Panel1.Controls.Add(this.labeltextBoxSogeord);
            this.splitContainer3.Panel1.Controls.Add(this.textBoxSogeord);
            this.splitContainer3.Panel1.Controls.Add(belobLabel);
            this.splitContainer3.Panel1.Controls.Add(this.belobTextBox);
            this.splitContainer3.Panel1.Controls.Add(tekstLabel);
            this.splitContainer3.Panel1.Controls.Add(this.tekstTextBox);
            this.splitContainer3.Panel1.Controls.Add(datoLabel);
            this.splitContainer3.Panel1.Controls.Add(this.datoTextBox);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tblbankkontoBindingNavigator);
            this.splitContainer3.Size = new System.Drawing.Size(393, 146);
            this.splitContainer3.SplitterDistance = 116;
            this.splitContainer3.TabIndex = 0;
            // 
            // labelcbBankkonto
            // 
            this.labelcbBankkonto.AutoSize = true;
            this.labelcbBankkonto.Location = new System.Drawing.Point(11, 14);
            this.labelcbBankkonto.Name = "labelcbBankkonto";
            this.labelcbBankkonto.Size = new System.Drawing.Size(62, 13);
            this.labelcbBankkonto.TabIndex = 17;
            this.labelcbBankkonto.Text = "Kontoudtog";
            // 
            // labelcbTemplate
            // 
            this.labelcbTemplate.AutoSize = true;
            this.labelcbTemplate.Location = new System.Drawing.Point(11, 121);
            this.labelcbTemplate.Name = "labelcbTemplate";
            this.labelcbTemplate.Size = new System.Drawing.Size(51, 13);
            this.labelcbTemplate.TabIndex = 16;
            this.labelcbTemplate.Text = "Template";
            // 
            // cbTemplate
            // 
            this.cbTemplate.DataSource = this.tbltemplateBindingSource;
            this.cbTemplate.DisplayMember = "Navn";
            this.cbTemplate.FormattingEnabled = true;
            this.cbTemplate.Location = new System.Drawing.Point(97, 114);
            this.cbTemplate.Name = "cbTemplate";
            this.cbTemplate.Size = new System.Drawing.Size(190, 21);
            this.cbTemplate.TabIndex = 15;
            this.cbTemplate.ValueMember = "Pid";
            // 
            // tbltemplateBindingSource
            // 
            this.tbltemplateBindingSource.DataSource = typeof(Trans2Summa3060.tbltemplate);
            // 
            // cmdPrivat
            // 
            this.cmdPrivat.Location = new System.Drawing.Point(301, 113);
            this.cmdPrivat.Name = "cmdPrivat";
            this.cmdPrivat.Size = new System.Drawing.Size(48, 23);
            this.cmdPrivat.TabIndex = 14;
            this.cmdPrivat.Text = "Kopier";
            this.cmdPrivat.UseVisualStyleBackColor = true;
            this.cmdPrivat.Click += new System.EventHandler(this.cmdPrivat_Click);
            // 
            // cbBankkonto
            // 
            this.cbBankkonto.DataSource = this.tblkontoudtogBindingSource;
            this.cbBankkonto.DisplayMember = "Name";
            this.cbBankkonto.FormattingEnabled = true;
            this.cbBankkonto.Location = new System.Drawing.Point(98, 11);
            this.cbBankkonto.Name = "cbBankkonto";
            this.cbBankkonto.Size = new System.Drawing.Size(189, 21);
            this.cbBankkonto.TabIndex = 9;
            this.cbBankkonto.ValueMember = "Pid";
            this.cbBankkonto.SelectedValueChanged += new System.EventHandler(this.cbBankkonto_SelectedValueChanged);
            // 
            // tblkontoudtogBindingSource
            // 
            this.tblkontoudtogBindingSource.DataSource = typeof(Trans2Summa3060.tblkontoudtog);
            // 
            // cmdSog
            // 
            this.cmdSog.Location = new System.Drawing.Point(301, 83);
            this.cmdSog.Name = "cmdSog";
            this.cmdSog.Size = new System.Drawing.Size(48, 23);
            this.cmdSog.TabIndex = 8;
            this.cmdSog.Text = "Søg";
            this.cmdSog.UseVisualStyleBackColor = true;
            this.cmdSog.Click += new System.EventHandler(this.cmdSog_Click);
            // 
            // labeltextBoxSogeord
            // 
            this.labeltextBoxSogeord.AutoSize = true;
            this.labeltextBoxSogeord.Location = new System.Drawing.Point(11, 91);
            this.labeltextBoxSogeord.Name = "labeltextBoxSogeord";
            this.labeltextBoxSogeord.Size = new System.Drawing.Size(47, 13);
            this.labeltextBoxSogeord.TabIndex = 7;
            this.labeltextBoxSogeord.Text = "Søgeord";
            // 
            // textBoxSogeord
            // 
            this.textBoxSogeord.Location = new System.Drawing.Point(97, 84);
            this.textBoxSogeord.Name = "textBoxSogeord";
            this.textBoxSogeord.Size = new System.Drawing.Size(190, 20);
            this.textBoxSogeord.TabIndex = 6;
            // 
            // belobTextBox
            // 
            this.belobTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblbankkontoBindingSourceUafstemte, "Belob", true));
            this.belobTextBox.Location = new System.Drawing.Point(284, 54);
            this.belobTextBox.Name = "belobTextBox";
            this.belobTextBox.Size = new System.Drawing.Size(81, 20);
            this.belobTextBox.TabIndex = 5;
            this.belobTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tblbankkontoBindingSourceUafstemte
            // 
            this.tblbankkontoBindingSourceUafstemte.DataSource = typeof(Trans2Summa3060.tblbankkonto);
            this.tblbankkontoBindingSourceUafstemte.PositionChanged += new System.EventHandler(this.tblbankkontoBindingSourceUafstemte_PositionChanged);
            // 
            // tekstTextBox
            // 
            this.tekstTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblbankkontoBindingSourceUafstemte, "Tekst", true));
            this.tekstTextBox.Location = new System.Drawing.Point(97, 54);
            this.tekstTextBox.Name = "tekstTextBox";
            this.tekstTextBox.Size = new System.Drawing.Size(190, 20);
            this.tekstTextBox.TabIndex = 3;
            // 
            // datoTextBox
            // 
            this.datoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblbankkontoBindingSourceUafstemte, "Dato", true));
            this.datoTextBox.Location = new System.Drawing.Point(15, 54);
            this.datoTextBox.Name = "datoTextBox";
            this.datoTextBox.Size = new System.Drawing.Size(83, 20);
            this.datoTextBox.TabIndex = 2;
            // 
            // tblbankkontoBindingNavigator
            // 
            this.tblbankkontoBindingNavigator.AddNewItem = null;
            this.tblbankkontoBindingNavigator.BindingSource = this.tblbankkontoBindingSourceUafstemte;
            this.tblbankkontoBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblbankkontoBindingNavigator.DeleteItem = null;
            this.tblbankkontoBindingNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblbankkontoBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.tblbankkontoBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.tblbankkontoBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblbankkontoBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblbankkontoBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblbankkontoBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblbankkontoBindingNavigator.Name = "tblbankkontoBindingNavigator";
            this.tblbankkontoBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblbankkontoBindingNavigator.Size = new System.Drawing.Size(393, 26);
            this.tblbankkontoBindingNavigator.TabIndex = 8;
            this.tblbankkontoBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 23);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 23);
            this.bindingNavigatorMoveFirstItem.Text = "of {0}";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 23);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 26);
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
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 23);
            this.bindingNavigatorMoveNextItem.Text = "of {0}";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 23);
            this.bindingNavigatorMoveLastItem.Text = "of {0}";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // tblbankkonto2DataGridView
            // 
            this.tblbankkonto2DataGridView.AllowUserToAddRows = false;
            this.tblbankkonto2DataGridView.AllowUserToDeleteRows = false;
            this.tblbankkonto2DataGridView.AutoGenerateColumns = false;
            this.tblbankkonto2DataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.tblbankkonto2DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblbankkonto2DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pidDataGridViewTextBoxColumn,
            this.saldoDataGridViewTextBoxColumn,
            this.skjulDataGridViewTextBoxColumn,
            this.datoDataGridViewTextBoxColumn,
            this.tekstDataGridViewTextBoxColumn,
            this.belobDataGridViewTextBoxColumn,
            this.afstemDataGridViewTextBoxColumn,
            this.bankkontoidDataGridViewTextBoxColumn,
            this.tblafstemDataGridViewTextBoxColumn});
            this.tblbankkonto2DataGridView.DataSource = this.tblbankkontoBindingSourceAfstemte;
            this.tblbankkonto2DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblbankkonto2DataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblbankkonto2DataGridView.Name = "tblbankkonto2DataGridView";
            this.tblbankkonto2DataGridView.ReadOnly = true;
            this.tblbankkonto2DataGridView.RowHeadersWidth = 15;
            this.tblbankkonto2DataGridView.Size = new System.Drawing.Size(393, 266);
            this.tblbankkonto2DataGridView.TabIndex = 0;
            this.tblbankkonto2DataGridView.SelectionChanged += new System.EventHandler(this.tblbankkonto2DataGridView_SelectionChanged);
            // 
            // pidDataGridViewTextBoxColumn
            // 
            this.pidDataGridViewTextBoxColumn.DataPropertyName = "pid";
            this.pidDataGridViewTextBoxColumn.HeaderText = "pid";
            this.pidDataGridViewTextBoxColumn.Name = "pidDataGridViewTextBoxColumn";
            this.pidDataGridViewTextBoxColumn.ReadOnly = true;
            this.pidDataGridViewTextBoxColumn.Visible = false;
            // 
            // saldoDataGridViewTextBoxColumn
            // 
            this.saldoDataGridViewTextBoxColumn.DataPropertyName = "saldo";
            this.saldoDataGridViewTextBoxColumn.HeaderText = "saldo";
            this.saldoDataGridViewTextBoxColumn.Name = "saldoDataGridViewTextBoxColumn";
            this.saldoDataGridViewTextBoxColumn.ReadOnly = true;
            this.saldoDataGridViewTextBoxColumn.Visible = false;
            // 
            // skjulDataGridViewTextBoxColumn
            // 
            this.skjulDataGridViewTextBoxColumn.DataPropertyName = "skjul";
            this.skjulDataGridViewTextBoxColumn.HeaderText = "skjul";
            this.skjulDataGridViewTextBoxColumn.Name = "skjulDataGridViewTextBoxColumn";
            this.skjulDataGridViewTextBoxColumn.ReadOnly = true;
            this.skjulDataGridViewTextBoxColumn.Visible = false;
            // 
            // datoDataGridViewTextBoxColumn
            // 
            this.datoDataGridViewTextBoxColumn.DataPropertyName = "dato";
            this.datoDataGridViewTextBoxColumn.HeaderText = "Dato";
            this.datoDataGridViewTextBoxColumn.Name = "datoDataGridViewTextBoxColumn";
            this.datoDataGridViewTextBoxColumn.ReadOnly = true;
            this.datoDataGridViewTextBoxColumn.Width = 83;
            // 
            // tekstDataGridViewTextBoxColumn
            // 
            this.tekstDataGridViewTextBoxColumn.DataPropertyName = "tekst";
            this.tekstDataGridViewTextBoxColumn.HeaderText = "Tekst";
            this.tekstDataGridViewTextBoxColumn.Name = "tekstDataGridViewTextBoxColumn";
            this.tekstDataGridViewTextBoxColumn.ReadOnly = true;
            this.tekstDataGridViewTextBoxColumn.Width = 190;
            // 
            // belobDataGridViewTextBoxColumn
            // 
            this.belobDataGridViewTextBoxColumn.DataPropertyName = "belob";
            this.belobDataGridViewTextBoxColumn.HeaderText = "Beløb";
            this.belobDataGridViewTextBoxColumn.Name = "belobDataGridViewTextBoxColumn";
            this.belobDataGridViewTextBoxColumn.ReadOnly = true;
            this.belobDataGridViewTextBoxColumn.Width = 81;
            // 
            // afstemDataGridViewTextBoxColumn
            // 
            this.afstemDataGridViewTextBoxColumn.DataPropertyName = "afstem";
            this.afstemDataGridViewTextBoxColumn.HeaderText = "afstem";
            this.afstemDataGridViewTextBoxColumn.Name = "afstemDataGridViewTextBoxColumn";
            this.afstemDataGridViewTextBoxColumn.ReadOnly = true;
            this.afstemDataGridViewTextBoxColumn.Visible = false;
            // 
            // bankkontoidDataGridViewTextBoxColumn
            // 
            this.bankkontoidDataGridViewTextBoxColumn.DataPropertyName = "bankkontoid";
            this.bankkontoidDataGridViewTextBoxColumn.HeaderText = "bankkontoid";
            this.bankkontoidDataGridViewTextBoxColumn.Name = "bankkontoidDataGridViewTextBoxColumn";
            this.bankkontoidDataGridViewTextBoxColumn.ReadOnly = true;
            this.bankkontoidDataGridViewTextBoxColumn.Visible = false;
            // 
            // tblafstemDataGridViewTextBoxColumn
            // 
            this.tblafstemDataGridViewTextBoxColumn.DataPropertyName = "tblafstem";
            this.tblafstemDataGridViewTextBoxColumn.HeaderText = "tblafstem";
            this.tblafstemDataGridViewTextBoxColumn.Name = "tblafstemDataGridViewTextBoxColumn";
            this.tblafstemDataGridViewTextBoxColumn.ReadOnly = true;
            this.tblafstemDataGridViewTextBoxColumn.Visible = false;
            // 
            // tblbankkontoBindingSourceAfstemte
            // 
            this.tblbankkontoBindingSourceAfstemte.DataSource = typeof(Trans2Summa3060.tblbankkonto);
            // 
            // FrmBankkontoudtog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Trans2Summa3060.Properties.Settings.Default.frmBankkontoudtogSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2Summa3060.Properties.Settings.Default, "FrmBankkontoudtogLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Trans2Summa3060.Properties.Settings.Default, "frmBankkontoudtogSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2Summa3060.Properties.Settings.Default.FrmBankkontoudtogLocation;
            this.Name = "FrmBankkontoudtog";
            this.Text = "Bank kontoudtog";
            this.Load += new System.EventHandler(this.FrmKladder_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbltemplateBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblkontoudtogBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkontoBindingSourceUafstemte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkontoBindingNavigator)).EndInit();
            this.tblbankkontoBindingNavigator.ResumeLayout(false);
            this.tblbankkontoBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkonto2DataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbankkontoBindingSourceAfstemte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.BindingSource tblbankkontoBindingSourceUafstemte;
        private System.Windows.Forms.BindingNavigator tblbankkontoBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView tblbankkonto2DataGridView;
        private System.Windows.Forms.BindingSource tblbankkontoBindingSourceAfstemte;
        private System.Windows.Forms.TextBox belobTextBox;
        private System.Windows.Forms.TextBox tekstTextBox;
        private System.Windows.Forms.TextBox datoTextBox;
        private System.Windows.Forms.Button cmdSog;
        private System.Windows.Forms.Label labeltextBoxSogeord;
        private System.Windows.Forms.TextBox textBoxSogeord;
        private System.Windows.Forms.ComboBox cbBankkonto;
        private System.Windows.Forms.BindingSource tblkontoudtogBindingSource;
        private System.Windows.Forms.Button cmdPrivat;
        private System.Windows.Forms.Label labelcbTemplate;
        private System.Windows.Forms.ComboBox cbTemplate;
        private System.Windows.Forms.BindingSource tbltemplateBindingSource;
        private System.Windows.Forms.Label labelcbBankkonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skjulDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn datoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tekstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn belobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn afstemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankkontoidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblafstemDataGridViewTextBoxColumn;
    }
}