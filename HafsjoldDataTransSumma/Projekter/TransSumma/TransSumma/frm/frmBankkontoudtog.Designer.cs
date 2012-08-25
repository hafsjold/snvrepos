namespace nsPuls3060
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
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
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblbankkontoBindingSourceAfstemte = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxTemplate = new System.Windows.Forms.ComboBox();
            this.labelcomboBoxTemplate = new System.Windows.Forms.Label();
            datoLabel = new System.Windows.Forms.Label();
            tekstLabel = new System.Windows.Forms.Label();
            belobLabel = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(381, 505);
            this.splitContainer1.SplitterDistance = 180;
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
            this.splitContainer3.Panel1.Controls.Add(this.labelcomboBoxTemplate);
            this.splitContainer3.Panel1.Controls.Add(this.comboBoxTemplate);
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
            this.splitContainer3.Size = new System.Drawing.Size(381, 180);
            this.splitContainer3.SplitterDistance = 142;
            this.splitContainer3.TabIndex = 0;
            // 
            // cmdPrivat
            // 
            this.cmdPrivat.Location = new System.Drawing.Point(292, 113);
            this.cmdPrivat.Name = "cmdPrivat";
            this.cmdPrivat.Size = new System.Drawing.Size(48, 23);
            this.cmdPrivat.TabIndex = 14;
            this.cmdPrivat.Text = "Privat";
            this.cmdPrivat.UseVisualStyleBackColor = true;
            this.cmdPrivat.Click += new System.EventHandler(this.cmdPrivat_Click);
            // 
            // cbBankkonto
            // 
            this.cbBankkonto.DataSource = this.tblkontoudtogBindingSource;
            this.cbBankkonto.DisplayMember = "Name";
            this.cbBankkonto.FormattingEnabled = true;
            this.cbBankkonto.Location = new System.Drawing.Point(15, 11);
            this.cbBankkonto.Name = "cbBankkonto";
            this.cbBankkonto.Size = new System.Drawing.Size(230, 21);
            this.cbBankkonto.TabIndex = 9;
            this.cbBankkonto.ValueMember = "Pid";
            this.cbBankkonto.SelectedValueChanged += new System.EventHandler(this.cbBankkonto_SelectedValueChanged);
            // 
            // tblkontoudtogBindingSource
            // 
            this.tblkontoudtogBindingSource.DataSource = typeof(nsPuls3060.Tblkontoudtog);
            // 
            // cmdSog
            // 
            this.cmdSog.Location = new System.Drawing.Point(292, 83);
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
            this.tblbankkontoBindingSourceUafstemte.DataSource = typeof(nsPuls3060.Tblbankkonto);
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
            this.tblbankkontoBindingNavigator.Size = new System.Drawing.Size(381, 34);
            this.tblbankkontoBindingNavigator.TabIndex = 8;
            this.tblbankkontoBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 31);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorMoveFirstItem.Text = "of {0}";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 34);
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
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorMoveNextItem.Text = "of {0}";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 31);
            this.bindingNavigatorMoveLastItem.Text = "of {0}";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // tblbankkonto2DataGridView
            // 
            this.tblbankkonto2DataGridView.AllowUserToAddRows = false;
            this.tblbankkonto2DataGridView.AllowUserToDeleteRows = false;
            this.tblbankkonto2DataGridView.AutoGenerateColumns = false;
            this.tblbankkonto2DataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.tblbankkonto2DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblbankkonto2DataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            this.tblbankkonto2DataGridView.DataSource = this.tblbankkontoBindingSourceAfstemte;
            this.tblbankkonto2DataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblbankkonto2DataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblbankkonto2DataGridView.Name = "tblbankkonto2DataGridView";
            this.tblbankkonto2DataGridView.ReadOnly = true;
            this.tblbankkonto2DataGridView.RowHeadersWidth = 15;
            this.tblbankkonto2DataGridView.Size = new System.Drawing.Size(381, 321);
            this.tblbankkonto2DataGridView.TabIndex = 0;
            this.tblbankkonto2DataGridView.SelectionChanged += new System.EventHandler(this.tblbankkonto2DataGridView_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Pid";
            this.dataGridViewTextBoxColumn9.HeaderText = "Pid";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Saldo";
            this.dataGridViewTextBoxColumn10.HeaderText = "Saldo";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Skjul";
            this.dataGridViewTextBoxColumn11.HeaderText = "Skjul";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Dato";
            this.dataGridViewTextBoxColumn12.HeaderText = "Dato";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 83;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Tekst";
            this.dataGridViewTextBoxColumn13.HeaderText = "Tekst";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 190;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Belob";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn14.HeaderText = "Belob";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 81;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Afstem";
            this.dataGridViewTextBoxColumn15.HeaderText = "Afstem";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Visible = false;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Tblafstem";
            this.dataGridViewTextBoxColumn16.HeaderText = "Tblafstem";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Visible = false;
            // 
            // tblbankkontoBindingSourceAfstemte
            // 
            this.tblbankkontoBindingSourceAfstemte.DataSource = typeof(nsPuls3060.Tblbankkonto);
            // 
            // comboBoxTemplate
            // 
            this.comboBoxTemplate.FormattingEnabled = true;
            this.comboBoxTemplate.Location = new System.Drawing.Point(97, 114);
            this.comboBoxTemplate.Name = "comboBoxTemplate";
            this.comboBoxTemplate.Size = new System.Drawing.Size(190, 21);
            this.comboBoxTemplate.TabIndex = 15;
            // 
            // labelcomboBoxTemplate
            // 
            this.labelcomboBoxTemplate.AutoSize = true;
            this.labelcomboBoxTemplate.Location = new System.Drawing.Point(11, 121);
            this.labelcomboBoxTemplate.Name = "labelcomboBoxTemplate";
            this.labelcomboBoxTemplate.Size = new System.Drawing.Size(51, 13);
            this.labelcomboBoxTemplate.TabIndex = 16;
            this.labelcomboBoxTemplate.Text = "Template";
            // 
            // FrmBankkontoudtog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmBankkontoudtogSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "FrmBankkontoudtogLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmBankkontoudtogSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.FrmBankkontoudtogLocation;
            this.Name = "FrmBankkontoudtog";
            this.Text = "Bank kontoudtog";
            this.Load += new System.EventHandler(this.FrmKladder_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.ComboBox cbBankkonto;
        private System.Windows.Forms.BindingSource tblkontoudtogBindingSource;
        private System.Windows.Forms.Button cmdPrivat;
        private System.Windows.Forms.Label labelcomboBoxTemplate;
        private System.Windows.Forms.ComboBox comboBoxTemplate;
    }
}