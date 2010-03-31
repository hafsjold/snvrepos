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
            System.Windows.Forms.Label datoLabel;
            System.Windows.Forms.Label bilagLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNyKladde));
            this.bnXWbilag = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dgwXWkladder = new System.Windows.Forms.DataGridView();
            this.datoDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.bilagTextBox = new System.Windows.Forms.TextBox();
            this.bsXWbilag = new System.Windows.Forms.BindingSource(this.components);
            this.bsXWkladder = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.tblwbilagBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            datoLabel = new System.Windows.Forms.Label();
            bilagLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bnXWbilag)).BeginInit();
            this.bnXWbilag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwXWkladder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsXWbilag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsXWkladder)).BeginInit();
            this.SuspendLayout();
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(10, 37);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(33, 13);
            datoLabel.TabIndex = 2;
            datoLabel.Text = "Dato:";
            // 
            // bilagLabel
            // 
            bilagLabel.AutoSize = true;
            bilagLabel.Location = new System.Drawing.Point(200, 37);
            bilagLabel.Name = "bilagLabel";
            bilagLabel.Size = new System.Drawing.Size(33, 13);
            bilagLabel.TabIndex = 4;
            bilagLabel.Text = "Bilag:";
            // 
            // bnXWbilag
            // 
            this.bnXWbilag.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnXWbilag.BindingSource = this.bsXWbilag;
            this.bnXWbilag.CountItem = this.bindingNavigatorCountItem;
            this.bnXWbilag.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnXWbilag.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tblwbilagBindingNavigatorSaveItem});
            this.bnXWbilag.Location = new System.Drawing.Point(0, 0);
            this.bnXWbilag.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnXWbilag.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnXWbilag.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnXWbilag.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnXWbilag.Name = "bnXWbilag";
            this.bnXWbilag.PositionItem = this.bindingNavigatorPositionItem;
            this.bnXWbilag.Size = new System.Drawing.Size(567, 25);
            this.bnXWbilag.TabIndex = 0;
            this.bnXWbilag.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
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
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // dgwXWkladder
            // 
            this.dgwXWkladder.AutoGenerateColumns = false;
            this.dgwXWkladder.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dgwXWkladder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwXWkladder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
            this.dgwXWkladder.DataSource = this.bsXWkladder;
            this.dgwXWkladder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgwXWkladder.Location = new System.Drawing.Point(0, 59);
            this.dgwXWkladder.Name = "dgwXWkladder";
            this.dgwXWkladder.Size = new System.Drawing.Size(567, 240);
            this.dgwXWkladder.TabIndex = 2;
            // 
            // datoDateTimePicker
            // 
            this.datoDateTimePicker.CustomFormat = "";
            this.datoDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsXWbilag, "Dato", true));
            this.datoDateTimePicker.Location = new System.Drawing.Point(49, 33);
            this.datoDateTimePicker.MaxDate = new System.DateTime(2012, 12, 31, 0, 0, 0, 0);
            this.datoDateTimePicker.MinDate = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            this.datoDateTimePicker.Name = "datoDateTimePicker";
            this.datoDateTimePicker.Size = new System.Drawing.Size(145, 20);
            this.datoDateTimePicker.TabIndex = 3;
            this.datoDateTimePicker.Value = new System.DateTime(2010, 3, 31, 0, 0, 0, 0);
            // 
            // bilagTextBox
            // 
            this.bilagTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsXWbilag, "Bilag", true));
            this.bilagTextBox.Location = new System.Drawing.Point(239, 34);
            this.bilagTextBox.Name = "bilagTextBox";
            this.bilagTextBox.Size = new System.Drawing.Size(64, 20);
            this.bilagTextBox.TabIndex = 5;
            // 
            // bsXWbilag
            // 
            this.bsXWbilag.DataSource = typeof(nsHafsjoldData.Tblwbilag);
            // 
            // bsXWkladder
            // 
            this.bsXWkladder.DataMember = "Tblwkladder";
            this.bsXWkladder.DataSource = this.bsXWbilag;
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
            // tblwbilagBindingNavigatorSaveItem
            // 
            this.tblwbilagBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblwbilagBindingNavigatorSaveItem.Enabled = false;
            this.tblwbilagBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblwbilagBindingNavigatorSaveItem.Image")));
            this.tblwbilagBindingNavigatorSaveItem.Name = "tblwbilagBindingNavigatorSaveItem";
            this.tblwbilagBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblwbilagBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Pid";
            this.dataGridViewTextBoxColumn4.HeaderText = "Pid";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Bilagpid";
            this.dataGridViewTextBoxColumn5.HeaderText = "Bilagpid";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Tekst";
            this.dataGridViewTextBoxColumn6.HeaderText = "Tekst";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Afstemningskonto";
            this.dataGridViewTextBoxColumn7.HeaderText = "Afstem";
            this.dataGridViewTextBoxColumn7.Items.AddRange(new object[] {
            "",
            "Bank",
            "A conto-udbetaling af løn"});
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Belob";
            this.dataGridViewTextBoxColumn8.HeaderText = "Beløb";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 70;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Konto";
            this.dataGridViewTextBoxColumn9.HeaderText = "Konto";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 50;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Momskode";
            this.dataGridViewTextBoxColumn10.HeaderText = "MK";
            this.dataGridViewTextBoxColumn10.Items.AddRange(new object[] {
            "",
            "K25",
            "S25",
            "U25"});
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewTextBoxColumn10.Width = 50;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Faktura";
            this.dataGridViewTextBoxColumn11.HeaderText = "Faknr";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 50;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Tblwbilag";
            this.dataGridViewTextBoxColumn12.HeaderText = "Tblwbilag";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Visible = false;
            // 
            // FrmNyKladde
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 299);
            this.Controls.Add(bilagLabel);
            this.Controls.Add(this.bilagTextBox);
            this.Controls.Add(datoLabel);
            this.Controls.Add(this.datoDateTimePicker);
            this.Controls.Add(this.dgwXWkladder);
            this.Controls.Add(this.bnXWbilag);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "FrmNyKladde";
            this.Text = "Ny Kladde";
            this.Load += new System.EventHandler(this.TestMD_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNyKladde_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bnXWbilag)).EndInit();
            this.bnXWbilag.ResumeLayout(false);
            this.bnXWbilag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwXWkladder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsXWbilag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsXWkladder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsXWbilag;
        private System.Windows.Forms.BindingNavigator bnXWbilag;
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
        private System.Windows.Forms.BindingSource bsXWkladder;
        private System.Windows.Forms.DataGridView dgwXWkladder;
        private System.Windows.Forms.DateTimePicker datoDateTimePicker;
        private System.Windows.Forms.TextBox bilagTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    }
}