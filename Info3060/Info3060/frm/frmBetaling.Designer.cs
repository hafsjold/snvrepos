namespace nsInfo3060
{
    partial class FrmBetaling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBetaling));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblbetalingsidentifikationBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.tblbetalingsidentifikationBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tblbetalingsidentifikationDataGridView = new System.Windows.Forms.DataGridView();
            this.tblbetalingsidentifikationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblMedlemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Navn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sagsnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tblbetalingsidentifikationBindingNavigator)).BeginInit();
            this.tblbetalingsidentifikationBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblbetalingsidentifikationDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbetalingsidentifikationBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMedlemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tblbetalingsidentifikationBindingNavigator
            // 
            this.tblbetalingsidentifikationBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.tblbetalingsidentifikationBindingNavigator.BindingSource = this.tblbetalingsidentifikationBindingSource;
            this.tblbetalingsidentifikationBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblbetalingsidentifikationBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.tblbetalingsidentifikationBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblbetalingsidentifikationBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tblbetalingsidentifikationBindingNavigatorSaveItem});
            this.tblbetalingsidentifikationBindingNavigator.Location = new System.Drawing.Point(0, 584);
            this.tblbetalingsidentifikationBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblbetalingsidentifikationBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblbetalingsidentifikationBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblbetalingsidentifikationBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblbetalingsidentifikationBindingNavigator.Name = "tblbetalingsidentifikationBindingNavigator";
            this.tblbetalingsidentifikationBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblbetalingsidentifikationBindingNavigator.Size = new System.Drawing.Size(734, 25);
            this.tblbetalingsidentifikationBindingNavigator.TabIndex = 0;
            this.tblbetalingsidentifikationBindingNavigator.Text = "bindingNavigator1";
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
            // tblbetalingsidentifikationBindingNavigatorSaveItem
            // 
            this.tblbetalingsidentifikationBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblbetalingsidentifikationBindingNavigatorSaveItem.Enabled = false;
            this.tblbetalingsidentifikationBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblbetalingsidentifikationBindingNavigatorSaveItem.Image")));
            this.tblbetalingsidentifikationBindingNavigatorSaveItem.Name = "tblbetalingsidentifikationBindingNavigatorSaveItem";
            this.tblbetalingsidentifikationBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblbetalingsidentifikationBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tblbetalingsidentifikationDataGridView
            // 
            this.tblbetalingsidentifikationDataGridView.AutoGenerateColumns = false;
            this.tblbetalingsidentifikationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblbetalingsidentifikationDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn5,
            this.Navn,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.sagsnr,
            this.dataGridViewTextBoxColumn2});
            this.tblbetalingsidentifikationDataGridView.DataSource = this.tblbetalingsidentifikationBindingSource;
            this.tblbetalingsidentifikationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblbetalingsidentifikationDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblbetalingsidentifikationDataGridView.Name = "tblbetalingsidentifikationDataGridView";
            this.tblbetalingsidentifikationDataGridView.Size = new System.Drawing.Size(734, 584);
            this.tblbetalingsidentifikationDataGridView.TabIndex = 1;
            this.tblbetalingsidentifikationDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tblbetalingsidentifikationDataGridView_MouseDown);
            // 
            // tblbetalingsidentifikationBindingSource
            // 
            this.tblbetalingsidentifikationBindingSource.DataSource = typeof(nsInfo3060.tblbetalingsidentifikation);
            // 
            // tblMedlemBindingSource
            // 
            this.tblMedlemBindingSource.DataSource = typeof(nsInfo3060.tblMedlem);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Nr";
            this.dataGridViewTextBoxColumn5.HeaderText = "Nr";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // Navn
            // 
            this.Navn.DataPropertyName = "Navn";
            this.Navn.HeaderText = "Navn";
            this.Navn.Name = "Navn";
            this.Navn.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "betalingsdato";
            this.dataGridViewTextBoxColumn3.HeaderText = "Betalings dato";
            this.dataGridViewTextBoxColumn3.MaxInputLength = 10;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "belob";
            this.dataGridViewTextBoxColumn4.HeaderText = "Beløb";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // sagsnr
            // 
            this.sagsnr.DataPropertyName = "sagsnr";
            this.sagsnr.HeaderText = "Sagsnr";
            this.sagsnr.Name = "sagsnr";
            this.sagsnr.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "betalingsidentifikation";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn2.HeaderText = "Betalings ID";
            this.dataGridViewTextBoxColumn2.MaxInputLength = 15;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // FrmBetaling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 609);
            this.Controls.Add(this.tblbetalingsidentifikationDataGridView);
            this.Controls.Add(this.tblbetalingsidentifikationBindingNavigator);
            this.Name = "FrmBetaling";
            this.Text = "Betaling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBetaling_FormClosing);
            this.Load += new System.EventHandler(this.FrmBetaling_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblbetalingsidentifikationBindingNavigator)).EndInit();
            this.tblbetalingsidentifikationBindingNavigator.ResumeLayout(false);
            this.tblbetalingsidentifikationBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblbetalingsidentifikationDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbetalingsidentifikationBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblMedlemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tblbetalingsidentifikationBindingSource;
        private System.Windows.Forms.BindingNavigator tblbetalingsidentifikationBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton tblbetalingsidentifikationBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView tblbetalingsidentifikationDataGridView;
        private System.Windows.Forms.BindingSource tblMedlemBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Navn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn sagsnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}