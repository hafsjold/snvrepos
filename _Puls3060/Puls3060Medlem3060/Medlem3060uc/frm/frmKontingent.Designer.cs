namespace Medlem3060uc
{
    partial class FrmKontingent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKontingent));
            this.bsKontingent = new System.Windows.Forms.BindingSource(this.components);
            this.bnKontingent = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.tblKontingentBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tblKontingentDataGridView = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startdatoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slutdatoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startalderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.slutalderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aarskontingentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsKontingent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnKontingent)).BeginInit();
            this.bnKontingent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblKontingentDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // bsKontingent
            // 
            this.bsKontingent.DataSource = typeof(nsPbs3060.tblKontingent);
            // 
            // bnKontingent
            // 
            this.bnKontingent.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnKontingent.BindingSource = this.bsKontingent;
            this.bnKontingent.CountItem = this.bindingNavigatorCountItem;
            this.bnKontingent.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnKontingent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnKontingent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tblKontingentBindingNavigatorSaveItem});
            this.bnKontingent.Location = new System.Drawing.Point(0, 265);
            this.bnKontingent.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnKontingent.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnKontingent.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnKontingent.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnKontingent.Name = "bnKontingent";
            this.bnKontingent.PositionItem = this.bindingNavigatorPositionItem;
            this.bnKontingent.Size = new System.Drawing.Size(673, 25);
            this.bnKontingent.TabIndex = 0;
            this.bnKontingent.Text = "bindingNavigator1";
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
            // tblKontingentBindingNavigatorSaveItem
            // 
            this.tblKontingentBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblKontingentBindingNavigatorSaveItem.Enabled = false;
            this.tblKontingentBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblKontingentBindingNavigatorSaveItem.Image")));
            this.tblKontingentBindingNavigatorSaveItem.Name = "tblKontingentBindingNavigatorSaveItem";
            this.tblKontingentBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblKontingentBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tblKontingentDataGridView
            // 
            this.tblKontingentDataGridView.AutoGenerateColumns = false;
            this.tblKontingentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblKontingentDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.startdatoDataGridViewTextBoxColumn,
            this.slutdatoDataGridViewTextBoxColumn,
            this.startalderDataGridViewTextBoxColumn,
            this.slutalderDataGridViewTextBoxColumn,
            this.aarskontingentDataGridViewTextBoxColumn});
            this.tblKontingentDataGridView.DataSource = this.bsKontingent;
            this.tblKontingentDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblKontingentDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblKontingentDataGridView.Name = "tblKontingentDataGridView";
            this.tblKontingentDataGridView.Size = new System.Drawing.Size(673, 265);
            this.tblKontingentDataGridView.TabIndex = 1;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // startdatoDataGridViewTextBoxColumn
            // 
            this.startdatoDataGridViewTextBoxColumn.DataPropertyName = "startdato";
            this.startdatoDataGridViewTextBoxColumn.HeaderText = "startdato";
            this.startdatoDataGridViewTextBoxColumn.Name = "startdatoDataGridViewTextBoxColumn";
            // 
            // slutdatoDataGridViewTextBoxColumn
            // 
            this.slutdatoDataGridViewTextBoxColumn.DataPropertyName = "slutdato";
            this.slutdatoDataGridViewTextBoxColumn.HeaderText = "slutdato";
            this.slutdatoDataGridViewTextBoxColumn.Name = "slutdatoDataGridViewTextBoxColumn";
            // 
            // startalderDataGridViewTextBoxColumn
            // 
            this.startalderDataGridViewTextBoxColumn.DataPropertyName = "startalder";
            this.startalderDataGridViewTextBoxColumn.HeaderText = "startalder";
            this.startalderDataGridViewTextBoxColumn.Name = "startalderDataGridViewTextBoxColumn";
            // 
            // slutalderDataGridViewTextBoxColumn
            // 
            this.slutalderDataGridViewTextBoxColumn.DataPropertyName = "slutalder";
            this.slutalderDataGridViewTextBoxColumn.HeaderText = "slutalder";
            this.slutalderDataGridViewTextBoxColumn.Name = "slutalderDataGridViewTextBoxColumn";
            // 
            // aarskontingentDataGridViewTextBoxColumn
            // 
            this.aarskontingentDataGridViewTextBoxColumn.DataPropertyName = "aarskontingent";
            this.aarskontingentDataGridViewTextBoxColumn.HeaderText = "aarskontingent";
            this.aarskontingentDataGridViewTextBoxColumn.Name = "aarskontingentDataGridViewTextBoxColumn";
            // 
            // FrmKontingent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Medlem3060uc.Properties.Settings.Default.frmKontingentSize;
            this.Controls.Add(this.tblKontingentDataGridView);
            this.Controls.Add(this.bnKontingent);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Medlem3060uc.Properties.Settings.Default, "frmKontingentPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Medlem3060uc.Properties.Settings.Default, "frmKontingentSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Medlem3060uc.Properties.Settings.Default.frmKontingentPoint;
            this.Name = "FrmKontingent";
            this.Text = "Kontingent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmKontingent_FormClosing);
            this.Load += new System.EventHandler(this.FrmKontingent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsKontingent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnKontingent)).EndInit();
            this.bnKontingent.ResumeLayout(false);
            this.bnKontingent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblKontingentDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsKontingent;
        private System.Windows.Forms.BindingNavigator bnKontingent;
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
        private System.Windows.Forms.ToolStripButton tblKontingentBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView tblKontingentDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startdatoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn slutdatoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startalderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn slutalderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aarskontingentDataGridViewTextBoxColumn;
    }
}