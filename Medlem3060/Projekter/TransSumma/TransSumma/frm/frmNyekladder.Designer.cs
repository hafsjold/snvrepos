﻿namespace nsPuls3060
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
            this.tblwbilagBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblwbilagBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.tblwbilagBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.tblwkladderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblwkladderDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bilagTextBox = new System.Windows.Forms.TextBox();
            this.datoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.karKontoplanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.karAfstemningskontiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PiddataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BilagpiddataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TekstdataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AfstemdataGridViewTextBox = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BelobdataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KontodataGridViewTextBox = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.MKdataGridViewComboBox = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FaknrdataGridViewTextBox = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bilagLabel = new System.Windows.Forms.Label();
            datoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingNavigator)).BeginInit();
            this.tblwbilagBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderDataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.karKontoplanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.karAfstemningskontiBindingSource)).BeginInit();
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
            this.tblwbilagBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.tblwbilagBindingNavigator.BindingSource = this.tblwbilagBindingSource;
            this.tblwbilagBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblwbilagBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
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
            this.tblwbilagBindingNavigatorSaveItem,
            this.newToolStripButton});
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
            // tblwbilagBindingNavigatorSaveItem
            // 
            this.tblwbilagBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblwbilagBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblwbilagBindingNavigatorSaveItem.Image")));
            this.tblwbilagBindingNavigatorSaveItem.Name = "tblwbilagBindingNavigatorSaveItem";
            this.tblwbilagBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblwbilagBindingNavigatorSaveItem.Text = "Save Data";
            this.tblwbilagBindingNavigatorSaveItem.Click += new System.EventHandler(this.tblwbilagBindingNavigatorSaveItem_Click);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
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
            this.tblwkladderDataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.tblwkladderDataGridView.DataSource = this.tblwkladderBindingSource;
            this.tblwkladderDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblwkladderDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblwkladderDataGridView.Name = "tblwkladderDataGridView";
            this.tblwkladderDataGridView.Size = new System.Drawing.Size(521, 174);
            this.tblwkladderDataGridView.TabIndex = 2;
            this.tblwkladderDataGridView.CellErrorTextNeeded += new System.Windows.Forms.DataGridViewCellErrorTextNeededEventHandler(this.tblwkladderDataGridView_CellErrorTextNeeded);
            this.tblwkladderDataGridView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.tblwkladderDataGridView_EditingControlShowing);
            this.tblwkladderDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.tblwkladderDataGridView_DataError);
            this.tblwkladderDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblwkladderDataGridView_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 70);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
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
            // karKontoplanBindingSource
            // 
            this.karKontoplanBindingSource.DataSource = typeof(nsPuls3060.KarKontoplan);
            // 
            // karAfstemningskontiBindingSource
            // 
            this.karAfstemningskontiBindingSource.DataSource = typeof(nsPuls3060.KarAfstemningskonti);
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
            // BelobdataGridViewTextBox
            // 
            this.BelobdataGridViewTextBox.DataPropertyName = "Belob";
            this.BelobdataGridViewTextBox.HeaderText = "Beløb";
            this.BelobdataGridViewTextBox.Name = "BelobdataGridViewTextBox";
            this.BelobdataGridViewTextBox.Width = 60;
            // 
            // KontodataGridViewTextBox
            // 
            this.KontodataGridViewTextBox.DataPropertyName = "Konto";
            this.KontodataGridViewTextBox.DataSource = this.karKontoplanBindingSource;
            this.KontodataGridViewTextBox.DisplayMember = "Kontonr";
            this.KontodataGridViewTextBox.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.KontodataGridViewTextBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KontodataGridViewTextBox.HeaderText = "Konto";
            this.KontodataGridViewTextBox.MaxDropDownItems = 25;
            this.KontodataGridViewTextBox.Name = "KontodataGridViewTextBox";
            this.KontodataGridViewTextBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KontodataGridViewTextBox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.KontodataGridViewTextBox.ValueMember = "Kontonr";
            this.KontodataGridViewTextBox.Width = 60;
            // 
            // MKdataGridViewComboBox
            // 
            this.MKdataGridViewComboBox.DataPropertyName = "Momskode";
            this.MKdataGridViewComboBox.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.MKdataGridViewComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MKdataGridViewComboBox.HeaderText = "MK";
            this.MKdataGridViewComboBox.Items.AddRange(new object[] {
            "",
            "S25",
            "K25",
            "U25"});
            this.MKdataGridViewComboBox.Name = "MKdataGridViewComboBox";
            this.MKdataGridViewComboBox.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MKdataGridViewComboBox.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MKdataGridViewComboBox.Width = 40;
            // 
            // FaknrdataGridViewTextBox
            // 
            this.FaknrdataGridViewTextBox.DataPropertyName = "Faktura";
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
            // FrmNyekladder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 259);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmNyekladder";
            this.Text = "Nye Kladder";
            this.Load += new System.EventHandler(this.FrmNyekladder_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNyekladder_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwbilagBindingNavigator)).EndInit();
            this.tblwbilagBindingNavigator.ResumeLayout(false);
            this.tblwbilagBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblwkladderDataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.karKontoplanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.karAfstemningskontiBindingSource)).EndInit();
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
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.BindingSource karKontoplanBindingSource;
        private System.Windows.Forms.BindingSource karAfstemningskontiBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn PiddataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn BilagpiddataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn TekstdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewComboBoxColumn AfstemdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn BelobdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewComboBoxColumn KontodataGridViewTextBox;
        private System.Windows.Forms.DataGridViewComboBoxColumn MKdataGridViewComboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn FaknrdataGridViewTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
}