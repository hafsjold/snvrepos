namespace nsPuls3060
{
    partial class FrmMedlemExtra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMedlemExtra));
            this.bntblMedlemExtra = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bstblMedlemExtra = new System.Windows.Forms.BindingSource(this.components);
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
            this.tblMedlemExtraBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tblMedlemExtraDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bntblMedlemExtra)).BeginInit();
            this.bntblMedlemExtra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bstblMedlemExtra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMedlemExtraDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // bntblMedlemExtra
            // 
            this.bntblMedlemExtra.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bntblMedlemExtra.BindingSource = this.bstblMedlemExtra;
            this.bntblMedlemExtra.CountItem = this.bindingNavigatorCountItem;
            this.bntblMedlemExtra.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bntblMedlemExtra.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tblMedlemExtraBindingNavigatorSaveItem});
            this.bntblMedlemExtra.Location = new System.Drawing.Point(0, 0);
            this.bntblMedlemExtra.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bntblMedlemExtra.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bntblMedlemExtra.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bntblMedlemExtra.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bntblMedlemExtra.Name = "bntblMedlemExtra";
            this.bntblMedlemExtra.PositionItem = this.bindingNavigatorPositionItem;
            this.bntblMedlemExtra.Size = new System.Drawing.Size(352, 25);
            this.bntblMedlemExtra.TabIndex = 0;
            this.bntblMedlemExtra.Text = "bindingNavigator1";
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
            // bstblMedlemExtra
            // 
            this.bstblMedlemExtra.DataSource = typeof(nsPbs3060v2.tblMedlemExtra);
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
            // tblMedlemExtraBindingNavigatorSaveItem
            // 
            this.tblMedlemExtraBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblMedlemExtraBindingNavigatorSaveItem.Enabled = false;
            this.tblMedlemExtraBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblMedlemExtraBindingNavigatorSaveItem.Image")));
            this.tblMedlemExtraBindingNavigatorSaveItem.Name = "tblMedlemExtraBindingNavigatorSaveItem";
            this.tblMedlemExtraBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblMedlemExtraBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tblMedlemExtraDataGridView
            // 
            this.tblMedlemExtraDataGridView.AutoGenerateColumns = false;
            this.tblMedlemExtraDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblMedlemExtraDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.tblMedlemExtraDataGridView.DataSource = this.bstblMedlemExtra;
            this.tblMedlemExtraDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMedlemExtraDataGridView.Location = new System.Drawing.Point(0, 25);
            this.tblMedlemExtraDataGridView.Name = "tblMedlemExtraDataGridView";
            this.tblMedlemExtraDataGridView.Size = new System.Drawing.Size(352, 279);
            this.tblMedlemExtraDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Nr";
            this.dataGridViewTextBoxColumn1.HeaderText = "Nr";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Pusterummet";
            this.dataGridViewTextBoxColumn2.HeaderText = "Medlem Pusterummet";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // FrmMedlemExtra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 304);
            this.Controls.Add(this.tblMedlemExtraDataGridView);
            this.Controls.Add(this.bntblMedlemExtra);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmMedlemExtraPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmMedlemExtraPoint;
            this.Name = "FrmMedlemExtra";
            this.Text = "MedlemExtra";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMedlemExtra_FormClosing);
            this.Load += new System.EventHandler(this.FrmMedlemExtra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bntblMedlemExtra)).EndInit();
            this.bntblMedlemExtra.ResumeLayout(false);
            this.bntblMedlemExtra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bstblMedlemExtra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMedlemExtraDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bstblMedlemExtra;
        private System.Windows.Forms.BindingNavigator bntblMedlemExtra;
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
        private System.Windows.Forms.ToolStripButton tblMedlemExtraBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView tblMedlemExtraDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewTextBoxColumn2;
    }
}