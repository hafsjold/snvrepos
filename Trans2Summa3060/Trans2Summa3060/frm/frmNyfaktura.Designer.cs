namespace Trans2Summa3060
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNyfaktura));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.kreditorbilagsnrLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelKontonavn = new System.Windows.Forms.Label();
            this.kreditorbilagsnrTextBox = new System.Windows.Forms.TextBox();
            this.tblwfakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.skComboBox = new System.Windows.Forms.ComboBox();
            this.kontoTextBox = new System.Windows.Forms.TextBox();
            this.datoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.tblwfaklinDataGridView = new System.Windows.Forms.DataGridView();
            this.tblwfaklinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblwfakBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.tblwfakBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripcmdBeregn = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabelFakturabelob = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FakturaTilSummaSummarumToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fakpidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varenrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tekstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momskodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.antalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enhedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nettobelobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bruttobelobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblwfakDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            datoLabel = new System.Windows.Forms.Label();
            kontoLabel = new System.Windows.Forms.Label();
            skLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
            datoLabel.Location = new System.Drawing.Point(92, 15);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(30, 13);
            datoLabel.TabIndex = 3;
            datoLabel.Text = "Dato";
            // 
            // kontoLabel
            // 
            kontoLabel.AutoSize = true;
            kontoLabel.Location = new System.Drawing.Point(10, 42);
            kontoLabel.Name = "kontoLabel";
            kontoLabel.Size = new System.Drawing.Size(35, 13);
            kontoLabel.TabIndex = 5;
            kontoLabel.Text = "Konto";
            // 
            // skLabel
            // 
            skLabel.AutoSize = true;
            skLabel.Location = new System.Drawing.Point(19, 15);
            skLabel.Name = "skLabel";
            skLabel.Size = new System.Drawing.Size(26, 13);
            skLabel.TabIndex = 6;
            skLabel.Text = "K/S";
            // 
            // kreditorbilagsnrLabel
            // 
            this.kreditorbilagsnrLabel.AutoSize = true;
            this.kreditorbilagsnrLabel.Location = new System.Drawing.Point(211, 15);
            this.kreditorbilagsnrLabel.Name = "kreditorbilagsnrLabel";
            this.kreditorbilagsnrLabel.Size = new System.Drawing.Size(65, 13);
            this.kreditorbilagsnrLabel.TabIndex = 8;
            this.kreditorbilagsnrLabel.Text = "Kreditorbilag";
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
            this.splitContainer1.Panel1.Controls.Add(this.kreditorbilagsnrLabel);
            this.splitContainer1.Panel1.Controls.Add(this.kreditorbilagsnrTextBox);
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
            this.splitContainer1.SplitterDistance = 65;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelKontonavn
            // 
            this.labelKontonavn.AutoSize = true;
            this.labelKontonavn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKontonavn.Location = new System.Drawing.Point(104, 39);
            this.labelKontonavn.Name = "labelKontonavn";
            this.labelKontonavn.Size = new System.Drawing.Size(11, 15);
            this.labelKontonavn.TabIndex = 10;
            this.labelKontonavn.Text = " ";
            // 
            // kreditorbilagsnrTextBox
            // 
            this.kreditorbilagsnrTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblwfakBindingSource, "Kreditorbilagsnr", true));
            this.kreditorbilagsnrTextBox.Location = new System.Drawing.Point(280, 12);
            this.kreditorbilagsnrTextBox.Name = "kreditorbilagsnrTextBox";
            this.kreditorbilagsnrTextBox.Size = new System.Drawing.Size(89, 20);
            this.kreditorbilagsnrTextBox.TabIndex = 9;
            // 
            // tblwfakBindingSource
            // 
            this.tblwfakBindingSource.DataSource = typeof(Trans2Summa3060.tblwfak);
            // 
            // skComboBox
            // 
            this.skComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblwfakBindingSource, "Sk", true));
            this.skComboBox.FormattingEnabled = true;
            this.skComboBox.Items.AddRange(new object[] {
            "K",
            "S"});
            this.skComboBox.Location = new System.Drawing.Point(48, 12);
            this.skComboBox.Name = "skComboBox";
            this.skComboBox.Size = new System.Drawing.Size(39, 21);
            this.skComboBox.TabIndex = 7;
            this.skComboBox.TextChanged += new System.EventHandler(this.skComboBox_TextChanged);
            // 
            // kontoTextBox
            // 
            this.kontoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblwfakBindingSource, "Konto", true));
            this.kontoTextBox.Location = new System.Drawing.Point(48, 38);
            this.kontoTextBox.Name = "kontoTextBox";
            this.kontoTextBox.Size = new System.Drawing.Size(50, 20);
            this.kontoTextBox.TabIndex = 6;
            this.kontoTextBox.TextChanged += new System.EventHandler(this.kontoTextBox_TextChanged);
            this.kontoTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kontoTextBox_MouseDown);
            // 
            // datoDateTimePicker
            // 
            this.datoDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tblwfakBindingSource, "Dato", true));
            this.datoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datoDateTimePicker.Location = new System.Drawing.Point(124, 11);
            this.datoDateTimePicker.Name = "datoDateTimePicker";
            this.datoDateTimePicker.Size = new System.Drawing.Size(81, 20);
            this.datoDateTimePicker.TabIndex = 4;
            // 
            // tblwfaklinDataGridView
            // 
            this.tblwfaklinDataGridView.AutoGenerateColumns = false;
            this.tblwfaklinDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblwfaklinDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pidDataGridViewTextBoxColumn,
            this.fakpidDataGridViewTextBoxColumn,
            this.varenrDataGridViewTextBoxColumn,
            this.tekstDataGridViewTextBoxColumn,
            this.kontoDataGridViewTextBoxColumn,
            this.momskodeDataGridViewTextBoxColumn,
            this.antalDataGridViewTextBoxColumn,
            this.enhedDataGridViewTextBoxColumn,
            this.prisDataGridViewTextBoxColumn,
            this.rabatDataGridViewTextBoxColumn,
            this.momsDataGridViewTextBoxColumn,
            this.nettobelobDataGridViewTextBoxColumn,
            this.bruttobelobDataGridViewTextBoxColumn,
            this.tblwfakDataGridViewTextBoxColumn});
            this.tblwfaklinDataGridView.DataSource = this.tblwfaklinBindingSource;
            this.tblwfaklinDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblwfaklinDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblwfaklinDataGridView.Name = "tblwfaklinDataGridView";
            this.tblwfaklinDataGridView.Size = new System.Drawing.Size(803, 285);
            this.tblwfaklinDataGridView.TabIndex = 3;
            this.tblwfaklinDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblwfaklinDataGridView1_KeyDown);
            this.tblwfaklinDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblwfaklinDataGridView1_MouseDown);
            // 
            // tblwfaklinBindingSource
            // 
            this.tblwfaklinBindingSource.DataMember = "tblwfaklins";
            this.tblwfaklinBindingSource.DataSource = this.tblwfakBindingSource;
            // 
            // tblwfakBindingNavigator
            // 
            this.tblwfakBindingNavigator.AddNewItem = null;
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
            this.toolStripSeparator1,
            this.toolStripcmdBeregn,
            this.toolStripLabelFakturabelob,
            this.toolStripSeparator2,
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
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
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
            this.bindingNavigatorPositionItem.TextChanged += new System.EventHandler(this.bindingNavigatorPositionItem_TextChanged);
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
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripcmdBeregn
            // 
            this.toolStripcmdBeregn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripcmdBeregn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripcmdBeregn.Image")));
            this.toolStripcmdBeregn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripcmdBeregn.Name = "toolStripcmdBeregn";
            this.toolStripcmdBeregn.Size = new System.Drawing.Size(48, 22);
            this.toolStripcmdBeregn.Text = "Beregn";
            this.toolStripcmdBeregn.Click += new System.EventHandler(this.toolStripcmdBeregn_Click);
            // 
            // toolStripLabelFakturabelob
            // 
            this.toolStripLabelFakturabelob.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabelFakturabelob.Name = "toolStripLabelFakturabelob";
            this.toolStripLabelFakturabelob.Size = new System.Drawing.Size(14, 22);
            this.toolStripLabelFakturabelob.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // FakturaTilSummaSummarumToolStripButton
            // 
            this.FakturaTilSummaSummarumToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FakturaTilSummaSummarumToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("FakturaTilSummaSummarumToolStripButton.Image")));
            this.FakturaTilSummaSummarumToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FakturaTilSummaSummarumToolStripButton.Name = "FakturaTilSummaSummarumToolStripButton";
            this.FakturaTilSummaSummarumToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.FakturaTilSummaSummarumToolStripButton.Text = "Faktura Til SummaSummarum";
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
            // pidDataGridViewTextBoxColumn
            // 
            this.pidDataGridViewTextBoxColumn.DataPropertyName = "pid";
            this.pidDataGridViewTextBoxColumn.HeaderText = "pid";
            this.pidDataGridViewTextBoxColumn.Name = "pidDataGridViewTextBoxColumn";
            this.pidDataGridViewTextBoxColumn.Visible = false;
            // 
            // fakpidDataGridViewTextBoxColumn
            // 
            this.fakpidDataGridViewTextBoxColumn.DataPropertyName = "fakpid";
            this.fakpidDataGridViewTextBoxColumn.HeaderText = "fakpid";
            this.fakpidDataGridViewTextBoxColumn.Name = "fakpidDataGridViewTextBoxColumn";
            this.fakpidDataGridViewTextBoxColumn.Visible = false;
            // 
            // varenrDataGridViewTextBoxColumn
            // 
            this.varenrDataGridViewTextBoxColumn.DataPropertyName = "varenr";
            this.varenrDataGridViewTextBoxColumn.FillWeight = 80F;
            this.varenrDataGridViewTextBoxColumn.HeaderText = "Varenr";
            this.varenrDataGridViewTextBoxColumn.Name = "varenrDataGridViewTextBoxColumn";
            this.varenrDataGridViewTextBoxColumn.Width = 80;
            // 
            // tekstDataGridViewTextBoxColumn
            // 
            this.tekstDataGridViewTextBoxColumn.DataPropertyName = "tekst";
            this.tekstDataGridViewTextBoxColumn.FillWeight = 200F;
            this.tekstDataGridViewTextBoxColumn.HeaderText = "Tekst";
            this.tekstDataGridViewTextBoxColumn.Name = "tekstDataGridViewTextBoxColumn";
            this.tekstDataGridViewTextBoxColumn.Width = 200;
            // 
            // kontoDataGridViewTextBoxColumn
            // 
            this.kontoDataGridViewTextBoxColumn.DataPropertyName = "konto";
            this.kontoDataGridViewTextBoxColumn.FillWeight = 60F;
            this.kontoDataGridViewTextBoxColumn.HeaderText = "Konto";
            this.kontoDataGridViewTextBoxColumn.Name = "kontoDataGridViewTextBoxColumn";
            this.kontoDataGridViewTextBoxColumn.Width = 60;
            // 
            // momskodeDataGridViewTextBoxColumn
            // 
            this.momskodeDataGridViewTextBoxColumn.DataPropertyName = "momskode";
            this.momskodeDataGridViewTextBoxColumn.FillWeight = 50F;
            this.momskodeDataGridViewTextBoxColumn.HeaderText = "MK";
            this.momskodeDataGridViewTextBoxColumn.Name = "momskodeDataGridViewTextBoxColumn";
            this.momskodeDataGridViewTextBoxColumn.Width = 50;
            // 
            // antalDataGridViewTextBoxColumn
            // 
            this.antalDataGridViewTextBoxColumn.DataPropertyName = "antal";
            this.antalDataGridViewTextBoxColumn.FillWeight = 40F;
            this.antalDataGridViewTextBoxColumn.HeaderText = "Antal";
            this.antalDataGridViewTextBoxColumn.Name = "antalDataGridViewTextBoxColumn";
            this.antalDataGridViewTextBoxColumn.Width = 40;
            // 
            // enhedDataGridViewTextBoxColumn
            // 
            this.enhedDataGridViewTextBoxColumn.DataPropertyName = "enhed";
            this.enhedDataGridViewTextBoxColumn.FillWeight = 40F;
            this.enhedDataGridViewTextBoxColumn.HeaderText = "Enhed";
            this.enhedDataGridViewTextBoxColumn.Name = "enhedDataGridViewTextBoxColumn";
            this.enhedDataGridViewTextBoxColumn.Width = 40;
            // 
            // prisDataGridViewTextBoxColumn
            // 
            this.prisDataGridViewTextBoxColumn.DataPropertyName = "pris";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            this.prisDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.prisDataGridViewTextBoxColumn.FillWeight = 60F;
            this.prisDataGridViewTextBoxColumn.HeaderText = "Pris";
            this.prisDataGridViewTextBoxColumn.Name = "prisDataGridViewTextBoxColumn";
            this.prisDataGridViewTextBoxColumn.Width = 60;
            // 
            // rabatDataGridViewTextBoxColumn
            // 
            this.rabatDataGridViewTextBoxColumn.DataPropertyName = "rabat";
            this.rabatDataGridViewTextBoxColumn.HeaderText = "rabat";
            this.rabatDataGridViewTextBoxColumn.Name = "rabatDataGridViewTextBoxColumn";
            this.rabatDataGridViewTextBoxColumn.Visible = false;
            // 
            // momsDataGridViewTextBoxColumn
            // 
            this.momsDataGridViewTextBoxColumn.DataPropertyName = "moms";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.momsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.momsDataGridViewTextBoxColumn.FillWeight = 60F;
            this.momsDataGridViewTextBoxColumn.HeaderText = "Moms";
            this.momsDataGridViewTextBoxColumn.Name = "momsDataGridViewTextBoxColumn";
            this.momsDataGridViewTextBoxColumn.Width = 60;
            // 
            // nettobelobDataGridViewTextBoxColumn
            // 
            this.nettobelobDataGridViewTextBoxColumn.DataPropertyName = "nettobelob";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.nettobelobDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.nettobelobDataGridViewTextBoxColumn.FillWeight = 70F;
            this.nettobelobDataGridViewTextBoxColumn.HeaderText = "Nettobeløb";
            this.nettobelobDataGridViewTextBoxColumn.Name = "nettobelobDataGridViewTextBoxColumn";
            this.nettobelobDataGridViewTextBoxColumn.Width = 70;
            // 
            // bruttobelobDataGridViewTextBoxColumn
            // 
            this.bruttobelobDataGridViewTextBoxColumn.DataPropertyName = "bruttobelob";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            this.bruttobelobDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.bruttobelobDataGridViewTextBoxColumn.FillWeight = 70F;
            this.bruttobelobDataGridViewTextBoxColumn.HeaderText = "Bruttobeløb";
            this.bruttobelobDataGridViewTextBoxColumn.Name = "bruttobelobDataGridViewTextBoxColumn";
            this.bruttobelobDataGridViewTextBoxColumn.Width = 70;
            // 
            // tblwfakDataGridViewTextBoxColumn
            // 
            this.tblwfakDataGridViewTextBoxColumn.DataPropertyName = "tblwfak";
            this.tblwfakDataGridViewTextBoxColumn.HeaderText = "tblwfak";
            this.tblwfakDataGridViewTextBoxColumn.Name = "tblwfakDataGridViewTextBoxColumn";
            this.tblwfakDataGridViewTextBoxColumn.Visible = false;
            // 
            // FrmNyfaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Trans2Summa3060.Properties.Settings.Default.frmNyeFakturaerSize;
            this.Controls.Add(this.tblwfakBindingNavigator);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2Summa3060.Properties.Settings.Default, "frmNyeFakturaerLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Trans2Summa3060.Properties.Settings.Default, "frmNyeFakturaerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2Summa3060.Properties.Settings.Default.frmNyeFakturaerLocation;
            this.Name = "FrmNyfaktura";
            this.Text = "Ny faktura";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNyfaktura_FormClosed);
            this.Load += new System.EventHandler(this.FrmNyfaktura_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
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
        private System.Windows.Forms.ContextMenuStrip contextMenuLineCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton FakturaTilSummaSummarumToolStripButton;
        private System.Windows.Forms.TextBox kreditorbilagsnrTextBox;
        private System.Windows.Forms.Label labelKontonavn;
        public System.Windows.Forms.Label kreditorbilagsnrLabel;

        private System.Windows.Forms.ToolStripLabel toolStripLabelFakturabelob;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripcmdBeregn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fakpidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn varenrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tekstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn momskodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn antalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enhedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn momsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nettobelobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bruttobelobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblwfakDataGridViewTextBoxColumn;
    }
}