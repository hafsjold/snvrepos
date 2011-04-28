namespace nsPuls3060
{
    partial class FrmNyfaktura
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
            System.Windows.Forms.Label kontoLabel;
            System.Windows.Forms.Label skLabel;
            System.Windows.Forms.Label kreditorbilagsnrLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNyfaktura));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.kreditorbilagsnrTextBox = new System.Windows.Forms.TextBox();
            this.tblwfakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmdBeregn = new System.Windows.Forms.Button();
            this.skComboBox = new System.Windows.Forms.ComboBox();
            this.kontoTextBox = new System.Windows.Forms.TextBox();
            this.datoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tblwfaklinDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblwfaklinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblwfakBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
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
            this.tblwfakBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.FakturaTilSummaSummarumToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelKontonavn = new System.Windows.Forms.Label();
            datoLabel = new System.Windows.Forms.Label();
            kontoLabel = new System.Windows.Forms.Label();
            skLabel = new System.Windows.Forms.Label();
            kreditorbilagsnrLabel = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblwfakBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwfaklinDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwfaklinBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwfakBindingNavigator)).BeginInit();
            this.tblwfakBindingNavigator.SuspendLayout();
            this.contextMenuLineCopyPaste.SuspendLayout();
            this.SuspendLayout();
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(108, 15);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(33, 13);
            datoLabel.TabIndex = 3;
            datoLabel.Text = "Dato:";
            // 
            // kontoLabel
            // 
            kontoLabel.AutoSize = true;
            kontoLabel.Location = new System.Drawing.Point(105, 39);
            kontoLabel.Name = "kontoLabel";
            kontoLabel.Size = new System.Drawing.Size(38, 13);
            kontoLabel.TabIndex = 5;
            kontoLabel.Text = "Konto:";
            // 
            // skLabel
            // 
            skLabel.AutoSize = true;
            skLabel.Location = new System.Drawing.Point(15, 15);
            skLabel.Name = "skLabel";
            skLabel.Size = new System.Drawing.Size(23, 13);
            skLabel.TabIndex = 6;
            skLabel.Text = "Sk:";
            // 
            // kreditorbilagsnrLabel
            // 
            kreditorbilagsnrLabel.AutoSize = true;
            kreditorbilagsnrLabel.Location = new System.Drawing.Point(410, 38);
            kreditorbilagsnrLabel.Name = "kreditorbilagsnrLabel";
            kreditorbilagsnrLabel.Size = new System.Drawing.Size(68, 13);
            kreditorbilagsnrLabel.TabIndex = 8;
            kreditorbilagsnrLabel.Text = "Kreditorbilag:";
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
            this.splitContainer1.Panel1.Controls.Add(this.labelKontonavn);
            this.splitContainer1.Panel1.Controls.Add(kreditorbilagsnrLabel);
            this.splitContainer1.Panel1.Controls.Add(this.kreditorbilagsnrTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.cmdBeregn);
            this.splitContainer1.Panel1.Controls.Add(skLabel);
            this.splitContainer1.Panel1.Controls.Add(this.skComboBox);
            this.splitContainer1.Panel1.Controls.Add(kontoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.kontoTextBox);
            this.splitContainer1.Panel1.Controls.Add(datoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.datoDateTimePicker);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tblwfaklinDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(803, 354);
            this.splitContainer1.SplitterDistance = 62;
            this.splitContainer1.TabIndex = 0;
            // 
            // kreditorbilagsnrTextBox
            // 
            this.kreditorbilagsnrTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblwfakBindingSource, "Kreditorbilagsnr", true));
            this.kreditorbilagsnrTextBox.Location = new System.Drawing.Point(482, 35);
            this.kreditorbilagsnrTextBox.Name = "kreditorbilagsnrTextBox";
            this.kreditorbilagsnrTextBox.Size = new System.Drawing.Size(89, 20);
            this.kreditorbilagsnrTextBox.TabIndex = 9;
            this.kreditorbilagsnrTextBox.TextChanged += new System.EventHandler(this.kreditorbilagsnrTextBox_TextChanged);
            // 
            // tblwfakBindingSource
            // 
            this.tblwfakBindingSource.DataSource = typeof(nsPuls3060.Tblwfak);
            // 
            // cmdBeregn
            // 
            this.cmdBeregn.Location = new System.Drawing.Point(630, 10);
            this.cmdBeregn.Name = "cmdBeregn";
            this.cmdBeregn.Size = new System.Drawing.Size(50, 22);
            this.cmdBeregn.TabIndex = 8;
            this.cmdBeregn.Text = "Beregn";
            this.cmdBeregn.UseVisualStyleBackColor = true;
            this.cmdBeregn.Click += new System.EventHandler(this.cmdBeregn_Click);
            // 
            // skComboBox
            // 
            this.skComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblwfakBindingSource, "Sk", true));
            this.skComboBox.FormattingEnabled = true;
            this.skComboBox.Items.AddRange(new object[] {
            "K",
            "S"});
            this.skComboBox.Location = new System.Drawing.Point(44, 12);
            this.skComboBox.Name = "skComboBox";
            this.skComboBox.Size = new System.Drawing.Size(39, 21);
            this.skComboBox.TabIndex = 7;
            // 
            // kontoTextBox
            // 
            this.kontoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblwfakBindingSource, "Konto", true));
            this.kontoTextBox.Location = new System.Drawing.Point(148, 36);
            this.kontoTextBox.Name = "kontoTextBox";
            this.kontoTextBox.Size = new System.Drawing.Size(63, 20);
            this.kontoTextBox.TabIndex = 6;
            this.kontoTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kontoTextBox_MouseDown);
            // 
            // datoDateTimePicker
            // 
            this.datoDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tblwfakBindingSource, "Dato", true));
            this.datoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datoDateTimePicker.Location = new System.Drawing.Point(147, 11);
            this.datoDateTimePicker.Name = "datoDateTimePicker";
            this.datoDateTimePicker.Size = new System.Drawing.Size(81, 20);
            this.datoDateTimePicker.TabIndex = 4;
            // 
            // tblwfaklinDataGridView
            // 
            this.tblwfaklinDataGridView.AutoGenerateColumns = false;
            this.tblwfaklinDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblwfaklinDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn19,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.dataGridViewTextBoxColumn26});
            this.tblwfaklinDataGridView.DataSource = this.tblwfaklinBindingSource;
            this.tblwfaklinDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblwfaklinDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblwfaklinDataGridView.Name = "tblwfaklinDataGridView";
            this.tblwfaklinDataGridView.Size = new System.Drawing.Size(803, 288);
            this.tblwfaklinDataGridView.TabIndex = 3;
            this.tblwfaklinDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblwfaklinDataGridView1_MouseDown);
            this.tblwfaklinDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblwfaklinDataGridView1_KeyDown);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Pid";
            this.dataGridViewTextBoxColumn1.HeaderText = "Pid";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Fakpid";
            this.dataGridViewTextBoxColumn2.HeaderText = "Fakpid";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Varenr";
            this.dataGridViewTextBoxColumn3.FillWeight = 80F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Varenr";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Tekst";
            this.dataGridViewTextBoxColumn4.FillWeight = 200F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Tekst";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Konto";
            this.dataGridViewTextBoxColumn7.FillWeight = 60F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Konto";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "Momskode";
            this.dataGridViewTextBoxColumn17.FillWeight = 50F;
            this.dataGridViewTextBoxColumn17.HeaderText = "MK";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Width = 50;
            // 
            // dataGridViewTextBoxColumn19
            // 
            this.dataGridViewTextBoxColumn19.DataPropertyName = "Antal";
            this.dataGridViewTextBoxColumn19.FillWeight = 40F;
            this.dataGridViewTextBoxColumn19.HeaderText = "Antal";
            this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
            this.dataGridViewTextBoxColumn19.Width = 40;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "Enhed";
            this.dataGridViewTextBoxColumn20.FillWeight = 40F;
            this.dataGridViewTextBoxColumn20.HeaderText = "Enhed";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.Width = 40;
            // 
            // dataGridViewTextBoxColumn21
            // 
            this.dataGridViewTextBoxColumn21.DataPropertyName = "Pris";
            this.dataGridViewTextBoxColumn21.FillWeight = 60F;
            this.dataGridViewTextBoxColumn21.HeaderText = "Pris";
            this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
            this.dataGridViewTextBoxColumn21.Width = 60;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.DataPropertyName = "Rabat";
            this.dataGridViewTextBoxColumn22.FillWeight = 1F;
            this.dataGridViewTextBoxColumn22.HeaderText = "Rabat";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.Visible = false;
            this.dataGridViewTextBoxColumn22.Width = 5;
            // 
            // dataGridViewTextBoxColumn23
            // 
            this.dataGridViewTextBoxColumn23.DataPropertyName = "Moms";
            this.dataGridViewTextBoxColumn23.FillWeight = 60F;
            this.dataGridViewTextBoxColumn23.HeaderText = "Moms";
            this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
            this.dataGridViewTextBoxColumn23.Width = 60;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.DataPropertyName = "Nettobelob";
            this.dataGridViewTextBoxColumn24.FillWeight = 70F;
            this.dataGridViewTextBoxColumn24.HeaderText = "Nettobelob";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.Width = 70;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.DataPropertyName = "Bruttobelob";
            this.dataGridViewTextBoxColumn25.FillWeight = 70F;
            this.dataGridViewTextBoxColumn25.HeaderText = "Bruttobelob";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.Width = 70;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.DataPropertyName = "Tblwfak";
            this.dataGridViewTextBoxColumn26.HeaderText = "Tblwfak";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.Visible = false;
            // 
            // tblwfaklinBindingSource
            // 
            this.tblwfaklinBindingSource.DataMember = "Tblwfaklin";
            this.tblwfaklinBindingSource.DataSource = this.tblwfakBindingSource;
            // 
            // tblwfakBindingNavigator
            // 
            this.tblwfakBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.tblwfakBindingNavigator.BindingSource = this.tblwfakBindingSource;
            this.tblwfakBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblwfakBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.tblwfakBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblwfakBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tblwfakBindingNavigatorSaveItem,
            this.FakturaTilSummaSummarumToolStripButton});
            this.tblwfakBindingNavigator.Location = new System.Drawing.Point(0, 329);
            this.tblwfakBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblwfakBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblwfakBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblwfakBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblwfakBindingNavigator.Name = "tblwfakBindingNavigator";
            this.tblwfakBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblwfakBindingNavigator.Size = new System.Drawing.Size(803, 25);
            this.tblwfakBindingNavigator.TabIndex = 1;
            this.tblwfakBindingNavigator.Text = "bindingNavigator1";
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
            // tblwfakBindingNavigatorSaveItem
            // 
            this.tblwfakBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblwfakBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblwfakBindingNavigatorSaveItem.Image")));
            this.tblwfakBindingNavigatorSaveItem.Name = "tblwfakBindingNavigatorSaveItem";
            this.tblwfakBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblwfakBindingNavigatorSaveItem.Text = "Save Data";
            this.tblwfakBindingNavigatorSaveItem.Click += new System.EventHandler(this.tblwfakBindingNavigatorSaveItem_Click);
            // 
            // FakturaTilSummaSummarumToolStripButton
            // 
            this.FakturaTilSummaSummarumToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FakturaTilSummaSummarumToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("FakturaTilSummaSummarumToolStripButton.Image")));
            this.FakturaTilSummaSummarumToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FakturaTilSummaSummarumToolStripButton.Name = "FakturaTilSummaSummarumToolStripButton";
            this.FakturaTilSummaSummarumToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FakturaTilSummaSummarumToolStripButton.Text = "Faktura Ti lSummaSummarum";
            this.FakturaTilSummaSummarumToolStripButton.Click += new System.EventHandler(this.FakturaTilSummaSummarumToolStripButton_Click);
            // 
            // contextMenuLineCopyPaste
            // 
            this.contextMenuLineCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuLineCopyPaste.Name = "contextMenuLineCopyPaste";
            this.contextMenuLineCopyPaste.Size = new System.Drawing.Size(113, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // labelKontonavn
            // 
            this.labelKontonavn.AutoSize = true;
            this.labelKontonavn.Location = new System.Drawing.Point(215, 38);
            this.labelKontonavn.Name = "labelKontonavn";
            this.labelKontonavn.Size = new System.Drawing.Size(10, 13);
            this.labelKontonavn.TabIndex = 10;
            this.labelKontonavn.Text = " ";
            // 
            // FrmNyfaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmNyeFakturaerSize;
            this.Controls.Add(this.tblwfakBindingNavigator);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmNyeFakturaerLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmNyeFakturaerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmNyeFakturaerLocation;
            this.Name = "FrmNyfaktura";
            this.Text = "Ny faktura";
            this.Load += new System.EventHandler(this.FrmNyfaktura_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNyfaktura_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblwfakBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwfaklinDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwfaklinBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwfakBindingNavigator)).EndInit();
            this.tblwfakBindingNavigator.ResumeLayout(false);
            this.tblwfakBindingNavigator.PerformLayout();
            this.contextMenuLineCopyPaste.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox skComboBox;
        private System.Windows.Forms.BindingSource tblwfakBindingSource;
        private System.Windows.Forms.TextBox kontoTextBox;
        private System.Windows.Forms.DateTimePicker datoDateTimePicker;
        private System.Windows.Forms.BindingSource tblwfaklinBindingSource;
        private System.Windows.Forms.BindingNavigator tblwfakBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton tblwfakBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView tblwfaklinDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.ContextMenuStrip contextMenuLineCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton FakturaTilSummaSummarumToolStripButton;
        private System.Windows.Forms.Button cmdBeregn;
        private System.Windows.Forms.TextBox kreditorbilagsnrTextBox;
        private System.Windows.Forms.Label labelKontonavn;
    }
}