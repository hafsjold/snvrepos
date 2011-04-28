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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNyfaktura));
            this.kreditorbilagsnrLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelKontonavn = new System.Windows.Forms.Label();
            this.kreditorbilagsnrTextBox = new System.Windows.Forms.TextBox();
            this.tblwfakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmdBeregn = new System.Windows.Forms.Button();
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
            this.FakturaTilSummaSummarumToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxPid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxFakpid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxVarenr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxTekst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxKonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxMK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxAntal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxEnhed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxPris = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxRabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxMoms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxNettobelob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxBruttobelob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxTblwfak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            datoLabel = new System.Windows.Forms.Label();
            kontoLabel = new System.Windows.Forms.Label();
            skLabel = new System.Windows.Forms.Label();
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
            this.dataGridViewTextBoxPid,
            this.dataGridViewTextBoxFakpid,
            this.dataGridViewTextBoxVarenr,
            this.dataGridViewTextBoxTekst,
            this.dataGridViewTextBoxKonto,
            this.dataGridViewTextBoxMK,
            this.dataGridViewTextBoxAntal,
            this.dataGridViewTextBoxEnhed,
            this.dataGridViewTextBoxPris,
            this.dataGridViewTextBoxRabat,
            this.dataGridViewTextBoxMoms,
            this.dataGridViewTextBoxNettobelob,
            this.dataGridViewTextBoxBruttobelob,
            this.dataGridViewTextBoxTblwfak});
            this.tblwfaklinDataGridView.DataSource = this.tblwfaklinBindingSource;
            this.tblwfaklinDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblwfaklinDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblwfaklinDataGridView.Name = "tblwfaklinDataGridView";
            this.tblwfaklinDataGridView.Size = new System.Drawing.Size(803, 285);
            this.tblwfaklinDataGridView.TabIndex = 3;
            this.tblwfaklinDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblwfaklinDataGridView1_MouseDown);
            this.tblwfaklinDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblwfaklinDataGridView1_KeyDown);
            // 
            // tblwfaklinBindingSource
            // 
            this.tblwfaklinBindingSource.DataMember = "Tblwfaklin";
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
            // dataGridViewTextBoxPid
            // 
            this.dataGridViewTextBoxPid.DataPropertyName = "Pid";
            this.dataGridViewTextBoxPid.HeaderText = "Pid";
            this.dataGridViewTextBoxPid.Name = "dataGridViewTextBoxPid";
            this.dataGridViewTextBoxPid.Visible = false;
            // 
            // dataGridViewTextBoxFakpid
            // 
            this.dataGridViewTextBoxFakpid.DataPropertyName = "Fakpid";
            this.dataGridViewTextBoxFakpid.HeaderText = "Fakpid";
            this.dataGridViewTextBoxFakpid.Name = "dataGridViewTextBoxFakpid";
            this.dataGridViewTextBoxFakpid.Visible = false;
            // 
            // dataGridViewTextBoxVarenr
            // 
            this.dataGridViewTextBoxVarenr.DataPropertyName = "Varenr";
            this.dataGridViewTextBoxVarenr.FillWeight = 80F;
            this.dataGridViewTextBoxVarenr.HeaderText = "Varenr";
            this.dataGridViewTextBoxVarenr.Name = "dataGridViewTextBoxVarenr";
            this.dataGridViewTextBoxVarenr.Width = 80;
            // 
            // dataGridViewTextBoxTekst
            // 
            this.dataGridViewTextBoxTekst.DataPropertyName = "Tekst";
            this.dataGridViewTextBoxTekst.FillWeight = 200F;
            this.dataGridViewTextBoxTekst.HeaderText = "Tekst";
            this.dataGridViewTextBoxTekst.Name = "dataGridViewTextBoxTekst";
            this.dataGridViewTextBoxTekst.Width = 200;
            // 
            // dataGridViewTextBoxKonto
            // 
            this.dataGridViewTextBoxKonto.DataPropertyName = "Konto";
            this.dataGridViewTextBoxKonto.FillWeight = 60F;
            this.dataGridViewTextBoxKonto.HeaderText = "Konto";
            this.dataGridViewTextBoxKonto.Name = "dataGridViewTextBoxKonto";
            this.dataGridViewTextBoxKonto.Width = 60;
            // 
            // dataGridViewTextBoxMK
            // 
            this.dataGridViewTextBoxMK.DataPropertyName = "Momskode";
            this.dataGridViewTextBoxMK.FillWeight = 50F;
            this.dataGridViewTextBoxMK.HeaderText = "MK";
            this.dataGridViewTextBoxMK.Name = "dataGridViewTextBoxMK";
            this.dataGridViewTextBoxMK.Width = 50;
            // 
            // dataGridViewTextBoxAntal
            // 
            this.dataGridViewTextBoxAntal.DataPropertyName = "Antal";
            this.dataGridViewTextBoxAntal.FillWeight = 40F;
            this.dataGridViewTextBoxAntal.HeaderText = "Antal";
            this.dataGridViewTextBoxAntal.Name = "dataGridViewTextBoxAntal";
            this.dataGridViewTextBoxAntal.Width = 40;
            // 
            // dataGridViewTextBoxEnhed
            // 
            this.dataGridViewTextBoxEnhed.DataPropertyName = "Enhed";
            this.dataGridViewTextBoxEnhed.FillWeight = 40F;
            this.dataGridViewTextBoxEnhed.HeaderText = "Enhed";
            this.dataGridViewTextBoxEnhed.Name = "dataGridViewTextBoxEnhed";
            this.dataGridViewTextBoxEnhed.Width = 40;
            // 
            // dataGridViewTextBoxPris
            // 
            this.dataGridViewTextBoxPris.DataPropertyName = "Pris";
            this.dataGridViewTextBoxPris.FillWeight = 60F;
            this.dataGridViewTextBoxPris.HeaderText = "Pris";
            this.dataGridViewTextBoxPris.Name = "dataGridViewTextBoxPris";
            this.dataGridViewTextBoxPris.Width = 60;
            // 
            // dataGridViewTextBoxRabat
            // 
            this.dataGridViewTextBoxRabat.DataPropertyName = "Rabat";
            this.dataGridViewTextBoxRabat.FillWeight = 1F;
            this.dataGridViewTextBoxRabat.HeaderText = "Rabat";
            this.dataGridViewTextBoxRabat.Name = "dataGridViewTextBoxRabat";
            this.dataGridViewTextBoxRabat.Visible = false;
            this.dataGridViewTextBoxRabat.Width = 5;
            // 
            // dataGridViewTextBoxMoms
            // 
            this.dataGridViewTextBoxMoms.DataPropertyName = "Moms";
            this.dataGridViewTextBoxMoms.FillWeight = 60F;
            this.dataGridViewTextBoxMoms.HeaderText = "Moms";
            this.dataGridViewTextBoxMoms.Name = "dataGridViewTextBoxMoms";
            this.dataGridViewTextBoxMoms.Width = 60;
            // 
            // dataGridViewTextBoxNettobelob
            // 
            this.dataGridViewTextBoxNettobelob.DataPropertyName = "Nettobelob";
            this.dataGridViewTextBoxNettobelob.FillWeight = 70F;
            this.dataGridViewTextBoxNettobelob.HeaderText = "Nettobelob";
            this.dataGridViewTextBoxNettobelob.Name = "dataGridViewTextBoxNettobelob";
            this.dataGridViewTextBoxNettobelob.Width = 70;
            // 
            // dataGridViewTextBoxBruttobelob
            // 
            this.dataGridViewTextBoxBruttobelob.DataPropertyName = "Bruttobelob";
            this.dataGridViewTextBoxBruttobelob.FillWeight = 70F;
            this.dataGridViewTextBoxBruttobelob.HeaderText = "Bruttobelob";
            this.dataGridViewTextBoxBruttobelob.Name = "dataGridViewTextBoxBruttobelob";
            this.dataGridViewTextBoxBruttobelob.Width = 70;
            // 
            // dataGridViewTextBoxTblwfak
            // 
            this.dataGridViewTextBoxTblwfak.DataPropertyName = "Tblwfak";
            this.dataGridViewTextBoxTblwfak.HeaderText = "Tblwfak";
            this.dataGridViewTextBoxTblwfak.Name = "dataGridViewTextBoxTblwfak";
            this.dataGridViewTextBoxTblwfak.Visible = false;
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
        private System.Windows.Forms.ContextMenuStrip contextMenuLineCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton FakturaTilSummaSummarumToolStripButton;
        private System.Windows.Forms.Button cmdBeregn;
        private System.Windows.Forms.TextBox kreditorbilagsnrTextBox;
        private System.Windows.Forms.Label labelKontonavn;
        public System.Windows.Forms.Label kreditorbilagsnrLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxPid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxFakpid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxVarenr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxTekst;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxKonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxMK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxAntal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxEnhed;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxPris;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxRabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxMoms;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxNettobelob;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxBruttobelob;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxTblwfak;
    }
}