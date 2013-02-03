﻿namespace docdblite
{
    partial class frmDocDblite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocDblite));
            this.tbldocBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.tbldocBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.tbldocBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tbldocDataGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selskabDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.årDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.produktDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblDataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.navnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.butDatabase = new System.Windows.Forms.Button();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.txtBoxDatabase = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbldocBindingNavigator)).BeginInit();
            this.tbldocBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbldocBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbldocDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbldocBindingNavigator
            // 
            this.tbldocBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.tbldocBindingNavigator.BindingSource = this.tbldocBindingSource;
            this.tbldocBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tbldocBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.tbldocBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbldocBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tbldocBindingNavigatorSaveItem});
            this.tbldocBindingNavigator.Location = new System.Drawing.Point(0, 374);
            this.tbldocBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tbldocBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tbldocBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tbldocBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tbldocBindingNavigator.Name = "tbldocBindingNavigator";
            this.tbldocBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tbldocBindingNavigator.Size = new System.Drawing.Size(823, 25);
            this.tbldocBindingNavigator.TabIndex = 0;
            this.tbldocBindingNavigator.Text = "bindingNavigator1";
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
            // tbldocBindingSource
            // 
            this.tbldocBindingSource.DataSource = typeof(docdblite.tbldoc);
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
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
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
            // tbldocBindingNavigatorSaveItem
            // 
            this.tbldocBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbldocBindingNavigatorSaveItem.Enabled = false;
            this.tbldocBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tbldocBindingNavigatorSaveItem.Image")));
            this.tbldocBindingNavigatorSaveItem.Name = "tbldocBindingNavigatorSaveItem";
            this.tbldocBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tbldocBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tbldocDataGridView
            // 
            this.tbldocDataGridView.AllowUserToAddRows = false;
            this.tbldocDataGridView.AllowUserToDeleteRows = false;
            this.tbldocDataGridView.AutoGenerateColumns = false;
            this.tbldocDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbldocDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.selskabDataGridViewTextBoxColumn,
            this.årDataGridViewTextBoxColumn,
            this.produktDataGridViewTextBoxColumn,
            this.tblDataDataGridViewTextBoxColumn,
            this.navnDataGridViewTextBoxColumn});
            this.tbldocDataGridView.DataSource = this.tbldocBindingSource;
            this.tbldocDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbldocDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tbldocDataGridView.Name = "tbldocDataGridView";
            this.tbldocDataGridView.ReadOnly = true;
            this.tbldocDataGridView.Size = new System.Drawing.Size(823, 283);
            this.tbldocDataGridView.TabIndex = 1;
            this.tbldocDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tbldocDataGridView_CellMouseDoubleClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // selskabDataGridViewTextBoxColumn
            // 
            this.selskabDataGridViewTextBoxColumn.DataPropertyName = "selskab";
            this.selskabDataGridViewTextBoxColumn.HeaderText = "Selskab";
            this.selskabDataGridViewTextBoxColumn.Name = "selskabDataGridViewTextBoxColumn";
            this.selskabDataGridViewTextBoxColumn.ReadOnly = true;
            this.selskabDataGridViewTextBoxColumn.Width = 200;
            // 
            // årDataGridViewTextBoxColumn
            // 
            this.årDataGridViewTextBoxColumn.DataPropertyName = "år";
            this.årDataGridViewTextBoxColumn.HeaderText = "År";
            this.årDataGridViewTextBoxColumn.MaxInputLength = 4;
            this.årDataGridViewTextBoxColumn.Name = "årDataGridViewTextBoxColumn";
            this.årDataGridViewTextBoxColumn.ReadOnly = true;
            this.årDataGridViewTextBoxColumn.Width = 50;
            // 
            // produktDataGridViewTextBoxColumn
            // 
            this.produktDataGridViewTextBoxColumn.DataPropertyName = "produkt";
            this.produktDataGridViewTextBoxColumn.HeaderText = "Produkt";
            this.produktDataGridViewTextBoxColumn.Name = "produktDataGridViewTextBoxColumn";
            this.produktDataGridViewTextBoxColumn.ReadOnly = true;
            this.produktDataGridViewTextBoxColumn.Width = 200;
            // 
            // tblDataDataGridViewTextBoxColumn
            // 
            this.tblDataDataGridViewTextBoxColumn.DataPropertyName = "tblData";
            this.tblDataDataGridViewTextBoxColumn.HeaderText = "tblData";
            this.tblDataDataGridViewTextBoxColumn.Name = "tblDataDataGridViewTextBoxColumn";
            this.tblDataDataGridViewTextBoxColumn.ReadOnly = true;
            this.tblDataDataGridViewTextBoxColumn.Visible = false;
            // 
            // navnDataGridViewTextBoxColumn
            // 
            this.navnDataGridViewTextBoxColumn.DataPropertyName = "navn";
            this.navnDataGridViewTextBoxColumn.HeaderText = "Fil";
            this.navnDataGridViewTextBoxColumn.Name = "navnDataGridViewTextBoxColumn";
            this.navnDataGridViewTextBoxColumn.ReadOnly = true;
            this.navnDataGridViewTextBoxColumn.Width = 200;
            // 
            // butDatabase
            // 
            this.butDatabase.Location = new System.Drawing.Point(561, 27);
            this.butDatabase.Name = "butDatabase";
            this.butDatabase.Size = new System.Drawing.Size(106, 23);
            this.butDatabase.TabIndex = 2;
            this.butDatabase.Text = "Åben Database";
            this.butDatabase.UseVisualStyleBackColor = true;
            this.butDatabase.Click += new System.EventHandler(this.butDatabase_Click);
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(17, 33);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(53, 13);
            this.labelDatabase.TabIndex = 1;
            this.labelDatabase.Text = "Database";
            // 
            // txtBoxDatabase
            // 
            this.txtBoxDatabase.Enabled = false;
            this.txtBoxDatabase.Location = new System.Drawing.Point(76, 30);
            this.txtBoxDatabase.Name = "txtBoxDatabase";
            this.txtBoxDatabase.Size = new System.Drawing.Size(430, 20);
            this.txtBoxDatabase.TabIndex = 0;
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
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.txtBoxDatabase);
            this.splitContainer1.Panel1.Controls.Add(this.butDatabase);
            this.splitContainer1.Panel1.Controls.Add(this.labelDatabase);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbldocDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(823, 374);
            this.splitContainer1.SplitterDistance = 87;
            this.splitContainer1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(518, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmDocDblite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 399);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tbldocBindingNavigator);
            this.Name = "frmDocDblite";
            this.Text = "DocDblite";
            ((System.ComponentModel.ISupportInitialize)(this.tbldocBindingNavigator)).EndInit();
            this.tbldocBindingNavigator.ResumeLayout(false);
            this.tbldocBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbldocBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbldocDataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tbldocBindingSource;
        private System.Windows.Forms.BindingNavigator tbldocBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton tbldocBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView tbldocDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.TextBox txtBoxDatabase;
        private System.Windows.Forms.Button butDatabase;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn selskabDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn årDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn produktDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblDataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn navnDataGridViewTextBoxColumn;


    }
}

