namespace nsPuls3060
{
    partial class FrmNyekladder
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
            System.Windows.Forms.Label bilagLabel;
            System.Windows.Forms.Label datoLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNyekladder));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblwbilagBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblwbilagBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tblwbilagBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.KladderTilSummaSummarumToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tblwkladderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblwkladderDataGridView = new System.Windows.Forms.DataGridView();
            this.PiddataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BilagpiddataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TekstdataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AfstemdataGridViewTextBox = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.karAfstemningskontiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BelobdataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KontodataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MKdataGridViewComboBox = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.karMomsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FaknrdataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.karKontoplanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bilagTextBox = new System.Windows.Forms.TextBox();
            this.datoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblBalanceBilag = new System.Windows.Forms.Label();
            this.cmdTest = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.contextMenuMoms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tillægMomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fratrækMomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            bilagLabel = new System.Windows.Forms.Label();
            datoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingNavigator)).BeginInit();
            this.tblwbilagBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.karAfstemningskontiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.karMomsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.karKontoplanBindingSource)).BeginInit();
            this.contextMenuLineCopyPaste.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.contextMenuMoms.SuspendLayout();
            this.SuspendLayout();
            // 
            // bilagLabel
            // 
            bilagLabel.AutoSize = true;
            bilagLabel.Location = new System.Drawing.Point(162, 11);
            bilagLabel.Name = "bilagLabel";
            bilagLabel.Size = new System.Drawing.Size(33, 13);
            bilagLabel.TabIndex = 3;
            bilagLabel.Text = "Bilag:";
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(24, 11);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(33, 13);
            datoLabel.TabIndex = 5;
            datoLabel.Text = "Dato:";
            // 
            // tblwbilagBindingSource
            // 
            this.tblwbilagBindingSource.DataSource = typeof(nsPuls3060.Tblwbilag);
            // 
            // tblwbilagBindingNavigator
            // 
            this.tblwbilagBindingNavigator.AddNewItem = null;
            this.tblwbilagBindingNavigator.BindingSource = this.tblwbilagBindingSource;
            this.tblwbilagBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblwbilagBindingNavigator.DeleteItem = null;
            this.tblwbilagBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblwbilagBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.toolStripSeparator1,
            this.tblwbilagBindingNavigatorSaveItem,
            this.KladderTilSummaSummarumToolStripButton});
            this.tblwbilagBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.tblwbilagBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblwbilagBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblwbilagBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblwbilagBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblwbilagBindingNavigator.Name = "tblwbilagBindingNavigator";
            this.tblwbilagBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblwbilagBindingNavigator.Size = new System.Drawing.Size(521, 25);
            this.tblwbilagBindingNavigator.TabIndex = 0;
            this.tblwbilagBindingNavigator.Text = "bindingNavigator1";
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
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.ToolTipText = "Nyt Bilag";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.ToolTipText = "Delete Bilag";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tblwbilagBindingNavigatorSaveItem
            // 
            this.tblwbilagBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblwbilagBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblwbilagBindingNavigatorSaveItem.Image")));
            this.tblwbilagBindingNavigatorSaveItem.Name = "tblwbilagBindingNavigatorSaveItem";
            this.tblwbilagBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblwbilagBindingNavigatorSaveItem.Text = "Save Data";
            this.tblwbilagBindingNavigatorSaveItem.ToolTipText = "Save to Database";
            this.tblwbilagBindingNavigatorSaveItem.Click += new System.EventHandler(this.tblwbilagBindingNavigatorSaveItem_Click);
            // 
            // KladderTilSummaSummarumToolStripButton
            // 
            this.KladderTilSummaSummarumToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.KladderTilSummaSummarumToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("KladderTilSummaSummarumToolStripButton.Image")));
            this.KladderTilSummaSummarumToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.KladderTilSummaSummarumToolStripButton.Name = "KladderTilSummaSummarumToolStripButton";
            this.KladderTilSummaSummarumToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.KladderTilSummaSummarumToolStripButton.Text = "&New";
            this.KladderTilSummaSummarumToolStripButton.ToolTipText = "Kladder til SummaSummarum";
            this.KladderTilSummaSummarumToolStripButton.Click += new System.EventHandler(this.KladderTilSummaSummarumToolStripButton_Click);
            // 
            // tblwkladderBindingSource
            // 
            this.tblwkladderBindingSource.DataMember = "Tblwkladder";
            this.tblwkladderBindingSource.DataSource = this.tblwbilagBindingSource;
            // 
            // tblwkladderDataGridView
            // 
            this.tblwkladderDataGridView.AllowDrop = true;
            this.tblwkladderDataGridView.AutoGenerateColumns = false;
            this.tblwkladderDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.tblwkladderDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblwkladderDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PiddataGridViewTextBox,
            this.BilagpiddataGridViewTextBox,
            this.TekstdataGridViewTextBox,
            this.AfstemdataGridViewTextBox,
            this.BelobdataGridViewTextBox,
            this.KontodataGridViewTextBox,
            this.MKdataGridViewComboBox,
            this.FaknrdataGridViewTextBox,
            this.dataGridViewTextBoxColumn12});
            this.tblwkladderDataGridView.DataSource = this.tblwkladderBindingSource;
            this.tblwkladderDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblwkladderDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblwkladderDataGridView.Name = "tblwkladderDataGridView";
            this.tblwkladderDataGridView.Size = new System.Drawing.Size(521, 174);
            this.tblwkladderDataGridView.TabIndex = 2;
            this.tblwkladderDataGridView.CellErrorTextNeeded += new System.Windows.Forms.DataGridViewCellErrorTextNeededEventHandler(this.tblwkladderDataGridView_CellErrorTextNeeded);
            this.tblwkladderDataGridView.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.myDGV_CellToolTipTextNeeded);
            this.tblwkladderDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.tblwkladderDataGridView_DataError);
            this.tblwkladderDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.tblwkladderDataGridView_EditingControlShowing);
            this.tblwkladderDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblwkladderDataGridView_KeyDown);
            this.tblwkladderDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblwkladderDataGridView_MouseDown);
            // 
            // PiddataGridViewTextBox
            // 
            this.PiddataGridViewTextBox.DataPropertyName = "Pid";
            this.PiddataGridViewTextBox.HeaderText = "Pid";
            this.PiddataGridViewTextBox.Name = "PiddataGridViewTextBox";
            this.PiddataGridViewTextBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PiddataGridViewTextBox.Visible = false;
            // 
            // BilagpiddataGridViewTextBox
            // 
            this.BilagpiddataGridViewTextBox.DataPropertyName = "Bilagpid";
            this.BilagpiddataGridViewTextBox.HeaderText = "Bilagpid";
            this.BilagpiddataGridViewTextBox.Name = "BilagpiddataGridViewTextBox";
            this.BilagpiddataGridViewTextBox.Visible = false;
            // 
            // TekstdataGridViewTextBox
            // 
            this.TekstdataGridViewTextBox.DataPropertyName = "Tekst";
            this.TekstdataGridViewTextBox.HeaderText = "Tekst";
            this.TekstdataGridViewTextBox.Name = "TekstdataGridViewTextBox";
            this.TekstdataGridViewTextBox.Width = 200;
            // 
            // AfstemdataGridViewTextBox
            // 
            this.AfstemdataGridViewTextBox.DataPropertyName = "Afstemningskonto";
            this.AfstemdataGridViewTextBox.DataSource = this.karAfstemningskontiBindingSource;
            this.AfstemdataGridViewTextBox.DisplayMember = "Kontonavn";
            this.AfstemdataGridViewTextBox.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.AfstemdataGridViewTextBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AfstemdataGridViewTextBox.HeaderText = "Afstem";
            this.AfstemdataGridViewTextBox.Name = "AfstemdataGridViewTextBox";
            this.AfstemdataGridViewTextBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AfstemdataGridViewTextBox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AfstemdataGridViewTextBox.ValueMember = "Kontonavn";
            this.AfstemdataGridViewTextBox.Width = 60;
            // 
            // karAfstemningskontiBindingSource
            // 
            this.karAfstemningskontiBindingSource.DataSource = typeof(nsPuls3060.KarAfstemningskonti);
            // 
            // BelobdataGridViewTextBox
            // 
            this.BelobdataGridViewTextBox.DataPropertyName = "Belob";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.BelobdataGridViewTextBox.DefaultCellStyle = dataGridViewCellStyle1;
            this.BelobdataGridViewTextBox.HeaderText = "Beløb";
            this.BelobdataGridViewTextBox.Name = "BelobdataGridViewTextBox";
            this.BelobdataGridViewTextBox.Width = 60;
            // 
            // KontodataGridViewTextBox
            // 
            this.KontodataGridViewTextBox.DataPropertyName = "Konto";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KontodataGridViewTextBox.DefaultCellStyle = dataGridViewCellStyle2;
            this.KontodataGridViewTextBox.HeaderText = "Konto";
            this.KontodataGridViewTextBox.Name = "KontodataGridViewTextBox";
            this.KontodataGridViewTextBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KontodataGridViewTextBox.ToolTipText = "XYZABC";
            this.KontodataGridViewTextBox.Width = 60;
            // 
            // MKdataGridViewComboBox
            // 
            this.MKdataGridViewComboBox.DataPropertyName = "Momskode";
            this.MKdataGridViewComboBox.DataSource = this.karMomsBindingSource;
            this.MKdataGridViewComboBox.DisplayMember = "Momskode";
            this.MKdataGridViewComboBox.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.MKdataGridViewComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MKdataGridViewComboBox.HeaderText = "MK";
            this.MKdataGridViewComboBox.Name = "MKdataGridViewComboBox";
            this.MKdataGridViewComboBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MKdataGridViewComboBox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MKdataGridViewComboBox.ValueMember = "Momskode";
            this.MKdataGridViewComboBox.Width = 40;
            // 
            // karMomsBindingSource
            // 
            this.karMomsBindingSource.DataSource = typeof(nsPuls3060.KarMoms);
            // 
            // FaknrdataGridViewTextBox
            // 
            this.FaknrdataGridViewTextBox.DataPropertyName = "Faktura";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FaknrdataGridViewTextBox.DefaultCellStyle = dataGridViewCellStyle3;
            this.FaknrdataGridViewTextBox.HeaderText = "Faknr";
            this.FaknrdataGridViewTextBox.Name = "FaknrdataGridViewTextBox";
            this.FaknrdataGridViewTextBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FaknrdataGridViewTextBox.Width = 40;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Tblwbilag";
            this.dataGridViewTextBoxColumn12.HeaderText = "Tblwbilag";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Visible = false;
            // 
            // karKontoplanBindingSource
            // 
            this.karKontoplanBindingSource.DataSource = typeof(nsPuls3060.KarKontoplan);
            // 
            // contextMenuLineCopyPaste
            // 
            this.contextMenuLineCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuLineCopyPaste.Name = "contextMenuStrip1";
            this.contextMenuLineCopyPaste.Size = new System.Drawing.Size(113, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyMenuLineCopyPastItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteMenuLineCopyPastItem_Click);
            // 
            // bilagTextBox
            // 
            this.bilagTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblwbilagBindingSource, "Bilag", true));
            this.bilagTextBox.Location = new System.Drawing.Point(201, 8);
            this.bilagTextBox.Name = "bilagTextBox";
            this.bilagTextBox.Size = new System.Drawing.Size(51, 20);
            this.bilagTextBox.TabIndex = 4;
            // 
            // datoDateTimePicker
            // 
            this.datoDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.tblwbilagBindingSource, "Dato", true));
            this.datoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datoDateTimePicker.Location = new System.Drawing.Point(63, 7);
            this.datoDateTimePicker.Name = "datoDateTimePicker";
            this.datoDateTimePicker.Size = new System.Drawing.Size(87, 20);
            this.datoDateTimePicker.TabIndex = 6;
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
            this.splitContainer1.Panel1.Controls.Add(this.lblBalanceBilag);
            this.splitContainer1.Panel1.Controls.Add(this.cmdTest);
            this.splitContainer1.Panel1.Controls.Add(datoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bilagTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.datoDateTimePicker);
            this.splitContainer1.Panel1.Controls.Add(bilagLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(521, 259);
            this.splitContainer1.SplitterDistance = 52;
            this.splitContainer1.TabIndex = 7;
            // 
            // lblBalanceBilag
            // 
            this.lblBalanceBilag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceBilag.ForeColor = System.Drawing.Color.Black;
            this.lblBalanceBilag.Location = new System.Drawing.Point(294, 12);
            this.lblBalanceBilag.Name = "lblBalanceBilag";
            this.lblBalanceBilag.Size = new System.Drawing.Size(70, 16);
            this.lblBalanceBilag.TabIndex = 8;
            this.lblBalanceBilag.Text = "0,00";
            this.lblBalanceBilag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(370, 9);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(50, 20);
            this.cmdTest.TabIndex = 7;
            this.cmdTest.Text = "Afstem";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
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
            this.splitContainer2.Panel1.Controls.Add(this.tblwkladderDataGridView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tblwbilagBindingNavigator);
            this.splitContainer2.Size = new System.Drawing.Size(521, 203);
            this.splitContainer2.SplitterDistance = 174;
            this.splitContainer2.TabIndex = 0;
            // 
            // contextMenuMoms
            // 
            this.contextMenuMoms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tillægMomsToolStripMenuItem,
            this.fratrækMomsToolStripMenuItem});
            this.contextMenuMoms.Name = "contextMenuMoms";
            this.contextMenuMoms.Size = new System.Drawing.Size(155, 48);
            // 
            // tillægMomsToolStripMenuItem
            // 
            this.tillægMomsToolStripMenuItem.Name = "tillægMomsToolStripMenuItem";
            this.tillægMomsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.tillægMomsToolStripMenuItem.Text = "Tillæg moms";
            this.tillægMomsToolStripMenuItem.Click += new System.EventHandler(this.tillægMomsToolStripMenuItem_Click);
            // 
            // fratrækMomsToolStripMenuItem
            // 
            this.fratrækMomsToolStripMenuItem.Name = "fratrækMomsToolStripMenuItem";
            this.fratrækMomsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.fratrækMomsToolStripMenuItem.Text = "Fratræk moms";
            this.fratrækMomsToolStripMenuItem.Click += new System.EventHandler(this.fratrækMomsToolStripMenuItem_Click);
            // 
            // FrmNyekladder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmNyeKladderSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmNyeKladderLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmNyeKladderSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmNyeKladderLocation;
            this.Name = "FrmNyekladder";
            this.Text = "Nye Kladder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNyekladder_FormClosed);
            this.Load += new System.EventHandler(this.FrmNyekladder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingNavigator)).EndInit();
            this.tblwbilagBindingNavigator.ResumeLayout(false);
            this.tblwbilagBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.karAfstemningskontiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.karMomsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.karKontoplanBindingSource)).EndInit();
            this.contextMenuLineCopyPaste.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.contextMenuMoms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource tblwbilagBindingSource;
        private System.Windows.Forms.BindingNavigator tblwbilagBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton tblwbilagBindingNavigatorSaveItem;
        private System.Windows.Forms.BindingSource tblwkladderBindingSource;
        private System.Windows.Forms.DataGridView tblwkladderDataGridView;
        private System.Windows.Forms.TextBox bilagTextBox;
        private System.Windows.Forms.DateTimePicker datoDateTimePicker;
        private System.Windows.Forms.ToolStripButton KladderTilSummaSummarumToolStripButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ContextMenuStrip contextMenuLineCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.BindingSource karKontoplanBindingSource;
        private System.Windows.Forms.BindingSource karAfstemningskontiBindingSource;
        private System.Windows.Forms.BindingSource karMomsBindingSource;
        private System.Windows.Forms.Button cmdTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuMoms;
        private System.Windows.Forms.ToolStripMenuItem tillægMomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fratrækMomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lblBalanceBilag;
        private System.Windows.Forms.DataGridViewTextBoxColumn PiddataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn BilagpiddataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn TekstdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewComboBoxColumn AfstemdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn BelobdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn KontodataGridViewTextBox;
        private System.Windows.Forms.DataGridViewComboBoxColumn MKdataGridViewComboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn FaknrdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
}