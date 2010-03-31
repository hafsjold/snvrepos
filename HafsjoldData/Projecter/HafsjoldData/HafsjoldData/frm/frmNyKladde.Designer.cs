namespace nsHafsjoldData
{
    partial class FrmNyKladde
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNyKladde));
            this.dtDato = new System.Windows.Forms.DateTimePicker();
            this.labelDato = new System.Windows.Forms.Label();
            this.tbBilag = new System.Windows.Forms.TextBox();
            this.labelBilag = new System.Windows.Forms.Label();
            this.dgwkladder = new System.Windows.Forms.DataGridView();
            this.bindingNavigatorwbilag = new System.Windows.Forms.BindingNavigator(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgwkladder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorwbilag)).BeginInit();
            this.bindingNavigatorwbilag.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtDato
            // 
            this.dtDato.Location = new System.Drawing.Point(48, 34);
            this.dtDato.Name = "dtDato";
            this.dtDato.Size = new System.Drawing.Size(123, 20);
            this.dtDato.TabIndex = 0;
            // 
            // labelDato
            // 
            this.labelDato.AutoSize = true;
            this.labelDato.Location = new System.Drawing.Point(12, 38);
            this.labelDato.Name = "labelDato";
            this.labelDato.Size = new System.Drawing.Size(30, 13);
            this.labelDato.TabIndex = 1;
            this.labelDato.Text = "Dato";
            // 
            // tbBilag
            // 
            this.tbBilag.Location = new System.Drawing.Point(230, 35);
            this.tbBilag.Name = "tbBilag";
            this.tbBilag.Size = new System.Drawing.Size(65, 20);
            this.tbBilag.TabIndex = 2;
            // 
            // labelBilag
            // 
            this.labelBilag.AutoSize = true;
            this.labelBilag.Location = new System.Drawing.Point(194, 38);
            this.labelBilag.Name = "labelBilag";
            this.labelBilag.Size = new System.Drawing.Size(30, 13);
            this.labelBilag.TabIndex = 1;
            this.labelBilag.Text = "Bilag";
            // 
            // dgwkladder
            // 
            this.dgwkladder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwkladder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgwkladder.Location = new System.Drawing.Point(0, 61);
            this.dgwkladder.Name = "dgwkladder";
            this.dgwkladder.Size = new System.Drawing.Size(571, 258);
            this.dgwkladder.TabIndex = 3;
            // 
            // bindingNavigatorwbilag
            // 
            this.bindingNavigatorwbilag.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigatorwbilag.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigatorwbilag.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigatorwbilag.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.bindingNavigatorDeleteItem});
            this.bindingNavigatorwbilag.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigatorwbilag.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigatorwbilag.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigatorwbilag.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigatorwbilag.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigatorwbilag.Name = "bindingNavigatorwbilag";
            this.bindingNavigatorwbilag.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigatorwbilag.Size = new System.Drawing.Size(571, 25);
            this.bindingNavigatorwbilag.TabIndex = 4;
            this.bindingNavigatorwbilag.Text = "bindingNavigator1";
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
            // FrmNyKladde
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 319);
            this.Controls.Add(this.bindingNavigatorwbilag);
            this.Controls.Add(this.dgwkladder);
            this.Controls.Add(this.tbBilag);
            this.Controls.Add(this.labelBilag);
            this.Controls.Add(this.labelDato);
            this.Controls.Add(this.dtDato);
            this.Name = "FrmNyKladde";
            this.Text = "frmNyKladde";
            ((System.ComponentModel.ISupportInitialize)(this.dgwkladder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorwbilag)).EndInit();
            this.bindingNavigatorwbilag.ResumeLayout(false);
            this.bindingNavigatorwbilag.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtDato;
        private System.Windows.Forms.Label labelDato;
        private System.Windows.Forms.TextBox tbBilag;
        private System.Windows.Forms.Label labelBilag;
        private System.Windows.Forms.DataGridView dgwkladder;
        private System.Windows.Forms.BindingNavigator bindingNavigatorwbilag;
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
    }
}