namespace nsPuls3060
{
    partial class FrmFaktura
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
            System.Windows.Forms.Label skLabel;
            System.Windows.Forms.Label fakidLabel;
            System.Windows.Forms.Label faknrLabel;
            System.Windows.Forms.Label datoLabel;
            System.Windows.Forms.Label kontoLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFaktura));
            this.tblfakBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.tblfakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tblfakBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tblfaklinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udskrivCheckBox = new System.Windows.Forms.CheckBox();
            this.kontoTextBox = new System.Windows.Forms.TextBox();
            this.datoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.faknrTextBox = new System.Windows.Forms.TextBox();
            this.fakidTextBox = new System.Windows.Forms.TextBox();
            this.skComboBox = new System.Windows.Forms.ComboBox();
            this.tblfaklinDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn33 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            skLabel = new System.Windows.Forms.Label();
            fakidLabel = new System.Windows.Forms.Label();
            faknrLabel = new System.Windows.Forms.Label();
            datoLabel = new System.Windows.Forms.Label();
            kontoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingNavigator)).BeginInit();
            this.tblfakBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinBindingSource)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinDataGridView)).BeginInit();
            this.contextMenuLineCopyPaste.SuspendLayout();
            this.SuspendLayout();
            // 
            // skLabel
            // 
            skLabel.AutoSize = true;
            skLabel.Location = new System.Drawing.Point(11, 15);
            skLabel.Name = "skLabel";
            skLabel.Size = new System.Drawing.Size(29, 13);
            skLabel.TabIndex = 1;
            skLabel.Text = "K/S:";
            // 
            // fakidLabel
            // 
            fakidLabel.AutoSize = true;
            fakidLabel.Location = new System.Drawing.Point(82, 15);
            fakidLabel.Name = "fakidLabel";
            fakidLabel.Size = new System.Drawing.Size(36, 13);
            fakidLabel.TabIndex = 3;
            fakidLabel.Text = "Fakid:";
            // 
            // faknrLabel
            // 
            faknrLabel.AutoSize = true;
            faknrLabel.Location = new System.Drawing.Point(168, 15);
            faknrLabel.Name = "faknrLabel";
            faknrLabel.Size = new System.Drawing.Size(37, 13);
            faknrLabel.TabIndex = 5;
            faknrLabel.Text = "Faknr:";
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(254, 15);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(33, 13);
            datoLabel.TabIndex = 7;
            datoLabel.Text = "Dato:";
            // 
            // kontoLabel
            // 
            kontoLabel.AutoSize = true;
            kontoLabel.Location = new System.Drawing.Point(377, 15);
            kontoLabel.Name = "kontoLabel";
            kontoLabel.Size = new System.Drawing.Size(38, 13);
            kontoLabel.TabIndex = 9;
            kontoLabel.Text = "Konto:";
            // 
            // tblfakBindingNavigator
            // 
            this.tblfakBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.tblfakBindingNavigator.BindingSource = this.tblfakBindingSource;
            this.tblfakBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblfakBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.tblfakBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblfakBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.tblfakBindingNavigatorSaveItem});
            this.tblfakBindingNavigator.Location = new System.Drawing.Point(0, 235);
            this.tblfakBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblfakBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblfakBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblfakBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblfakBindingNavigator.Name = "tblfakBindingNavigator";
            this.tblfakBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblfakBindingNavigator.Size = new System.Drawing.Size(825, 25);
            this.tblfakBindingNavigator.TabIndex = 0;
            this.tblfakBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // tblfakBindingSource
            // 
            this.tblfakBindingSource.DataSource = typeof(nsPuls3060.Tblfak);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
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
            // tblfakBindingNavigatorSaveItem
            // 
            this.tblfakBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblfakBindingNavigatorSaveItem.Enabled = false;
            this.tblfakBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblfakBindingNavigatorSaveItem.Image")));
            this.tblfakBindingNavigatorSaveItem.Name = "tblfakBindingNavigatorSaveItem";
            this.tblfakBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblfakBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tblfaklinBindingSource
            // 
            this.tblfaklinBindingSource.DataMember = "Tblfaklin";
            this.tblfaklinBindingSource.DataSource = this.tblfakBindingSource;
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
            this.splitContainer1.Panel1.Controls.Add(this.udskrivCheckBox);
            this.splitContainer1.Panel1.Controls.Add(kontoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.kontoTextBox);
            this.splitContainer1.Panel1.Controls.Add(datoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.datoDateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(faknrLabel);
            this.splitContainer1.Panel1.Controls.Add(this.faknrTextBox);
            this.splitContainer1.Panel1.Controls.Add(fakidLabel);
            this.splitContainer1.Panel1.Controls.Add(this.fakidTextBox);
            this.splitContainer1.Panel1.Controls.Add(skLabel);
            this.splitContainer1.Panel1.Controls.Add(this.skComboBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tblfaklinDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(825, 235);
            this.splitContainer1.SplitterDistance = 41;
            this.splitContainer1.TabIndex = 3;
            // 
            // udskrivCheckBox
            // 
            this.udskrivCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.tblfakBindingSource, "Udskriv", true));
            this.udskrivCheckBox.Location = new System.Drawing.Point(490, 10);
            this.udskrivCheckBox.Name = "udskrivCheckBox";
            this.udskrivCheckBox.Size = new System.Drawing.Size(67, 24);
            this.udskrivCheckBox.TabIndex = 12;
            this.udskrivCheckBox.Text = "Udskriv";
            this.udskrivCheckBox.UseVisualStyleBackColor = true;
            // 
            // kontoTextBox
            // 
            this.kontoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Konto", true));
            this.kontoTextBox.Location = new System.Drawing.Point(421, 12);
            this.kontoTextBox.Name = "kontoTextBox";
            this.kontoTextBox.Size = new System.Drawing.Size(50, 20);
            this.kontoTextBox.TabIndex = 10;
            // 
            // datoDateTimePicker
            // 
            this.datoDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tblfakBindingSource, "Dato", true));
            this.datoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datoDateTimePicker.Location = new System.Drawing.Point(293, 11);
            this.datoDateTimePicker.Name = "datoDateTimePicker";
            this.datoDateTimePicker.Size = new System.Drawing.Size(81, 20);
            this.datoDateTimePicker.TabIndex = 8;
            // 
            // faknrTextBox
            // 
            this.faknrTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Faknr", true));
            this.faknrTextBox.Location = new System.Drawing.Point(211, 12);
            this.faknrTextBox.Name = "faknrTextBox";
            this.faknrTextBox.Size = new System.Drawing.Size(40, 20);
            this.faknrTextBox.TabIndex = 6;
            // 
            // fakidTextBox
            // 
            this.fakidTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Fakid", true));
            this.fakidTextBox.Location = new System.Drawing.Point(124, 12);
            this.fakidTextBox.Name = "fakidTextBox";
            this.fakidTextBox.Size = new System.Drawing.Size(40, 20);
            this.fakidTextBox.TabIndex = 4;
            // 
            // skComboBox
            // 
            this.skComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Sk", true));
            this.skComboBox.FormattingEnabled = true;
            this.skComboBox.Items.AddRange(new object[] {
            "K",
            "S"});
            this.skComboBox.Location = new System.Drawing.Point(40, 12);
            this.skComboBox.Name = "skComboBox";
            this.skComboBox.Size = new System.Drawing.Size(38, 21);
            this.skComboBox.TabIndex = 2;
            // 
            // tblfaklinDataGridView
            // 
            this.tblfaklinDataGridView.AllowUserToAddRows = false;
            this.tblfaklinDataGridView.AllowUserToDeleteRows = false;
            this.tblfaklinDataGridView.AutoGenerateColumns = false;
            this.tblfaklinDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblfaklinDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn32,
            this.dataGridViewTextBoxColumn33,
            this.dataGridViewTextBoxColumn34,
            this.dataGridViewTextBoxColumn35,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn38});
            this.tblfaklinDataGridView.ContextMenuStrip = this.contextMenuLineCopyPaste;
            this.tblfaklinDataGridView.DataSource = this.tblfaklinBindingSource;
            this.tblfaklinDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblfaklinDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblfaklinDataGridView.Name = "tblfaklinDataGridView";
            this.tblfaklinDataGridView.ReadOnly = true;
            this.tblfaklinDataGridView.Size = new System.Drawing.Size(825, 190);
            this.tblfaklinDataGridView.TabIndex = 2;
            this.tblfaklinDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblfaklinDataGridView_KeyDown);
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
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Fakpid";
            this.dataGridViewTextBoxColumn10.HeaderText = "Fakpid";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Regnskabid";
            this.dataGridViewTextBoxColumn11.HeaderText = "Regnskabid";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Sk";
            this.dataGridViewTextBoxColumn12.HeaderText = "Sk";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Visible = false;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Fakid";
            this.dataGridViewTextBoxColumn13.HeaderText = "Fakid";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Visible = false;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Faklinnr";
            this.dataGridViewTextBoxColumn14.HeaderText = "Faklinnr";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Visible = false;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Varenr";
            this.dataGridViewTextBoxColumn15.HeaderText = "Varenr";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.Width = 80;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Tekst";
            this.dataGridViewTextBoxColumn16.HeaderText = "Tekst";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.Width = 200;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "Konto";
            this.dataGridViewTextBoxColumn17.HeaderText = "Konto";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            this.dataGridViewTextBoxColumn17.Width = 60;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "Momskode";
            this.dataGridViewTextBoxColumn18.HeaderText = "MK";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.ReadOnly = true;
            this.dataGridViewTextBoxColumn18.Width = 40;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "Antal";
            this.dataGridViewTextBoxColumn19.HeaderText = "Antal";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.ReadOnly = true;
            this.dataGridViewTextBoxColumn19.Width = 40;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "Enhed";
            this.dataGridViewTextBoxColumn21.HeaderText = "Enhed";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.ReadOnly = true;
            this.dataGridViewTextBoxColumn21.Width = 40;
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.DataPropertyName = "Pris";
            this.dataGridViewTextBoxColumn32.HeaderText = "Pris";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.ReadOnly = true;
            this.dataGridViewTextBoxColumn32.Width = 60;
            // 
            // dataGridViewTextBoxColumn33
            // 
            this.dataGridViewTextBoxColumn33.DataPropertyName = "Rabat";
            this.dataGridViewTextBoxColumn33.HeaderText = "Rabat";
            this.dataGridViewTextBoxColumn33.Name = "dataGridViewTextBoxColumn33";
            this.dataGridViewTextBoxColumn33.ReadOnly = true;
            this.dataGridViewTextBoxColumn33.Visible = false;
            // 
            // dataGridViewTextBoxColumn34
            // 
            this.dataGridViewTextBoxColumn34.DataPropertyName = "Moms";
            this.dataGridViewTextBoxColumn34.HeaderText = "Moms";
            this.dataGridViewTextBoxColumn34.Name = "dataGridViewTextBoxColumn34";
            this.dataGridViewTextBoxColumn34.ReadOnly = true;
            this.dataGridViewTextBoxColumn34.Width = 60;
            // 
            // dataGridViewTextBoxColumn35
            // 
            this.dataGridViewTextBoxColumn35.DataPropertyName = "Nettobelob";
            this.dataGridViewTextBoxColumn35.HeaderText = "Nettobelob";
            this.dataGridViewTextBoxColumn35.Name = "dataGridViewTextBoxColumn35";
            this.dataGridViewTextBoxColumn35.ReadOnly = true;
            this.dataGridViewTextBoxColumn35.Width = 70;
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.DataPropertyName = "Bruttobelob";
            this.dataGridViewTextBoxColumn36.HeaderText = "Bruttobelob";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            this.dataGridViewTextBoxColumn36.ReadOnly = true;
            this.dataGridViewTextBoxColumn36.Width = 70;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.DataPropertyName = "Omkostbelob";
            this.dataGridViewTextBoxColumn37.HeaderText = "Omk";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            this.dataGridViewTextBoxColumn37.ReadOnly = true;
            this.dataGridViewTextBoxColumn37.Width = 50;
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.DataPropertyName = "Tblfak";
            this.dataGridViewTextBoxColumn38.HeaderText = "Tblfak";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            this.dataGridViewTextBoxColumn38.ReadOnly = true;
            this.dataGridViewTextBoxColumn38.Visible = false;
            // 
            // contextMenuLineCopyPaste
            // 
            this.contextMenuLineCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuLineCopyPaste.Name = "contextMenuLineCopyPaste";
            this.contextMenuLineCopyPaste.Size = new System.Drawing.Size(111, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // FrmFaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmFakturaerSize;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tblfakBindingNavigator);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmFakturaerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmFakturaerLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmFakturaerLocation;
            this.Name = "FrmFaktura";
            this.Text = "Faktura";
            this.Load += new System.EventHandler(this.FrmFaktura_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFaktura_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingNavigator)).EndInit();
            this.tblfakBindingNavigator.ResumeLayout(false);
            this.tblfakBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinDataGridView)).EndInit();
            this.contextMenuLineCopyPaste.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tblfakBindingSource;
        private System.Windows.Forms.BindingNavigator tblfakBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton tblfakBindingNavigatorSaveItem;
        private System.Windows.Forms.BindingSource tblfaklinBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox fakidTextBox;
        private System.Windows.Forms.ComboBox skComboBox;
        private System.Windows.Forms.CheckBox udskrivCheckBox;
        private System.Windows.Forms.TextBox kontoTextBox;
        private System.Windows.Forms.DateTimePicker datoDateTimePicker;
        private System.Windows.Forms.TextBox faknrTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridView tblfaklinDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        private System.Windows.Forms.ContextMenuStrip contextMenuLineCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
    }
}