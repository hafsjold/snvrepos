namespace Trans2SummaHDA
{
    partial class FrmTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTemplate));
            this.bsTbltemplate = new System.Windows.Forms.BindingSource(this.components);
            this.bnTbltemplate = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.tbltemplateBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tbltemplateDataGridView = new System.Windows.Forms.DataGridView();
            this.pidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.navnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tekstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momskodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.afstemningskontoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsTbltemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnTbltemplate)).BeginInit();
            this.bnTbltemplate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbltemplateDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // bsTbltemplate
            // 
            this.bsTbltemplate.DataSource = typeof(Trans2SummaHDA.tbltemplate);
            // 
            // bnTbltemplate
            // 
            this.bnTbltemplate.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnTbltemplate.BindingSource = this.bsTbltemplate;
            this.bnTbltemplate.CountItem = this.bindingNavigatorCountItem;
            this.bnTbltemplate.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnTbltemplate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnTbltemplate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tbltemplateBindingNavigatorSaveItem});
            this.bnTbltemplate.Location = new System.Drawing.Point(0, 272);
            this.bnTbltemplate.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnTbltemplate.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnTbltemplate.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnTbltemplate.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnTbltemplate.Name = "bnTbltemplate";
            this.bnTbltemplate.PositionItem = this.bindingNavigatorPositionItem;
            this.bnTbltemplate.Size = new System.Drawing.Size(730, 25);
            this.bnTbltemplate.TabIndex = 0;
            this.bnTbltemplate.Text = "bindingNavigator1";
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
            // tbltemplateBindingNavigatorSaveItem
            // 
            this.tbltemplateBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbltemplateBindingNavigatorSaveItem.Enabled = false;
            this.tbltemplateBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tbltemplateBindingNavigatorSaveItem.Image")));
            this.tbltemplateBindingNavigatorSaveItem.Name = "tbltemplateBindingNavigatorSaveItem";
            this.tbltemplateBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tbltemplateBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tbltemplateDataGridView
            // 
            this.tbltemplateDataGridView.AllowUserToOrderColumns = true;
            this.tbltemplateDataGridView.AutoGenerateColumns = false;
            this.tbltemplateDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbltemplateDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pidDataGridViewTextBoxColumn,
            this.navnDataGridViewTextBoxColumn,
            this.tekstDataGridViewTextBoxColumn,
            this.kontoDataGridViewTextBoxColumn,
            this.momskodeDataGridViewTextBoxColumn,
            this.afstemningskontoDataGridViewTextBoxColumn});
            this.tbltemplateDataGridView.DataSource = this.bsTbltemplate;
            this.tbltemplateDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbltemplateDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tbltemplateDataGridView.Name = "tbltemplateDataGridView";
            this.tbltemplateDataGridView.Size = new System.Drawing.Size(730, 272);
            this.tbltemplateDataGridView.TabIndex = 1;
            // 
            // pidDataGridViewTextBoxColumn
            // 
            this.pidDataGridViewTextBoxColumn.DataPropertyName = "pid";
            this.pidDataGridViewTextBoxColumn.HeaderText = "Nr";
            this.pidDataGridViewTextBoxColumn.Name = "pidDataGridViewTextBoxColumn";
            this.pidDataGridViewTextBoxColumn.Width = 50;
            // 
            // navnDataGridViewTextBoxColumn
            // 
            this.navnDataGridViewTextBoxColumn.DataPropertyName = "navn";
            this.navnDataGridViewTextBoxColumn.HeaderText = "Navn";
            this.navnDataGridViewTextBoxColumn.Name = "navnDataGridViewTextBoxColumn";
            this.navnDataGridViewTextBoxColumn.Width = 150;
            // 
            // tekstDataGridViewTextBoxColumn
            // 
            this.tekstDataGridViewTextBoxColumn.DataPropertyName = "tekst";
            this.tekstDataGridViewTextBoxColumn.HeaderText = "Tekst";
            this.tekstDataGridViewTextBoxColumn.Name = "tekstDataGridViewTextBoxColumn";
            this.tekstDataGridViewTextBoxColumn.Width = 150;
            // 
            // kontoDataGridViewTextBoxColumn
            // 
            this.kontoDataGridViewTextBoxColumn.DataPropertyName = "konto";
            this.kontoDataGridViewTextBoxColumn.HeaderText = "Konto";
            this.kontoDataGridViewTextBoxColumn.Name = "kontoDataGridViewTextBoxColumn";
            this.kontoDataGridViewTextBoxColumn.Width = 50;
            // 
            // momskodeDataGridViewTextBoxColumn
            // 
            this.momskodeDataGridViewTextBoxColumn.DataPropertyName = "momskode";
            this.momskodeDataGridViewTextBoxColumn.HeaderText = "MK";
            this.momskodeDataGridViewTextBoxColumn.Name = "momskodeDataGridViewTextBoxColumn";
            this.momskodeDataGridViewTextBoxColumn.Width = 50;
            // 
            // afstemningskontoDataGridViewTextBoxColumn
            // 
            this.afstemningskontoDataGridViewTextBoxColumn.DataPropertyName = "afstemningskonto";
            this.afstemningskontoDataGridViewTextBoxColumn.HeaderText = "Afstemning";
            this.afstemningskontoDataGridViewTextBoxColumn.Name = "afstemningskontoDataGridViewTextBoxColumn";
            this.afstemningskontoDataGridViewTextBoxColumn.Width = 150;
            // 
            // FrmTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Trans2SummaHDA.Properties.Settings.Default.frmTemplateSize;
            this.Controls.Add(this.tbltemplateDataGridView);
            this.Controls.Add(this.bnTbltemplate);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2SummaHDA.Properties.Settings.Default, "frmTemplateLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Trans2SummaHDA.Properties.Settings.Default, "frmTemplateSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::Trans2SummaHDA.Properties.Settings.Default, "frmTemplateWinState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2SummaHDA.Properties.Settings.Default.frmTemplateLocation;
            this.Name = "FrmTemplate";
            this.Text = "Template";
            this.WindowState = global::Trans2SummaHDA.Properties.Settings.Default.frmTemplateWinState;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTemplate_FormClosing);
            this.Load += new System.EventHandler(this.FrmTemplate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsTbltemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnTbltemplate)).EndInit();
            this.bnTbltemplate.ResumeLayout(false);
            this.bnTbltemplate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbltemplateDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsTbltemplate;
        private System.Windows.Forms.BindingNavigator bnTbltemplate;
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
        private System.Windows.Forms.ToolStripButton tbltemplateBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView tbltemplateDataGridView;

        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn navnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tekstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn momskodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn afstemningskontoDataGridViewTextBoxColumn;
    }
}