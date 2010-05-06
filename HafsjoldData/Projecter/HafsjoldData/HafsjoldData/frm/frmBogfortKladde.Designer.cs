namespace nsHafsjoldData
{
    partial class FrmBogfortKladde
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBogfortKladde));
            System.Windows.Forms.Label findLlabel;
            this.tblbilagBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblbilagBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bilagTextBox = new System.Windows.Forms.TextBox();
            this.tblkladderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblkladderDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.datoTextBox = new System.Windows.Forms.TextBox();
            this.FindTextBox = new System.Windows.Forms.TextBox();
            this.findBotton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            datoLabel = new System.Windows.Forms.Label();
            bilagLabel = new System.Windows.Forms.Label();
            findLlabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingNavigator)).BeginInit();
            this.tblbilagBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(10, 37);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(33, 13);
            datoLabel.TabIndex = 1;
            datoLabel.Text = "Dato:";
            // 
            // bilagLabel
            // 
            bilagLabel.AutoSize = true;
            bilagLabel.Location = new System.Drawing.Point(122, 37);
            bilagLabel.Name = "bilagLabel";
            bilagLabel.Size = new System.Drawing.Size(33, 13);
            bilagLabel.TabIndex = 3;
            bilagLabel.Text = "Bilag:";
            // 
            // tblbilagBindingSource
            // 
            this.tblbilagBindingSource.DataSource = typeof(nsHafsjoldData.Tblbilag);
            // 
            // tblbilagBindingNavigator
            // 
            this.tblbilagBindingNavigator.AddNewItem = null;
            this.tblbilagBindingNavigator.BindingSource = this.tblbilagBindingSource;
            this.tblbilagBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblbilagBindingNavigator.DeleteItem = null;
            this.tblbilagBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem});
            this.tblbilagBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.tblbilagBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblbilagBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblbilagBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblbilagBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblbilagBindingNavigator.Name = "tblbilagBindingNavigator";
            this.tblbilagBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblbilagBindingNavigator.Size = new System.Drawing.Size(562, 25);
            this.tblbilagBindingNavigator.TabIndex = 0;
            this.tblbilagBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.AutoToolTip = false;
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.AutoToolTip = false;
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
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(25, 21);
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
            this.bindingNavigatorMoveNextItem.AutoToolTip = false;
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.AutoToolTip = false;
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bilagTextBox
            // 
            this.bilagTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.bilagTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblbilagBindingSource, "Bilag", true));
            this.bilagTextBox.Location = new System.Drawing.Point(156, 34);
            this.bilagTextBox.Name = "bilagTextBox";
            this.bilagTextBox.ReadOnly = true;
            this.bilagTextBox.Size = new System.Drawing.Size(35, 20);
            this.bilagTextBox.TabIndex = 4;
            // 
            // tblkladderBindingSource
            // 
            this.tblkladderBindingSource.DataMember = "Tblkladder";
            this.tblkladderBindingSource.DataSource = this.tblbilagBindingSource;
            // 
            // tblkladderDataGridView
            // 
            this.tblkladderDataGridView.AllowUserToAddRows = false;
            this.tblkladderDataGridView.AllowUserToDeleteRows = false;
            this.tblkladderDataGridView.AutoGenerateColumns = false;
            this.tblkladderDataGridView.BackgroundColor = System.Drawing.SystemColors.Info;
            this.tblkladderDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblkladderDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.tblkladderDataGridView.DataSource = this.tblkladderBindingSource;
            this.tblkladderDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblkladderDataGridView.Location = new System.Drawing.Point(0, 60);
            this.tblkladderDataGridView.Name = "tblkladderDataGridView";
            this.tblkladderDataGridView.ReadOnly = true;
            this.tblkladderDataGridView.Size = new System.Drawing.Size(562, 261);
            this.tblkladderDataGridView.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Pid";
            this.dataGridViewTextBoxColumn1.HeaderText = "Pid";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Bilagpid";
            this.dataGridViewTextBoxColumn2.HeaderText = "Bilagpid";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Tekst";
            this.dataGridViewTextBoxColumn3.HeaderText = "Tekst";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Afstemningskonto";
            this.dataGridViewTextBoxColumn4.HeaderText = "Afstem";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Belob";
            this.dataGridViewTextBoxColumn5.HeaderText = "Beløb";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Konto";
            this.dataGridViewTextBoxColumn6.HeaderText = "Konto";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 50;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Momskode";
            this.dataGridViewTextBoxColumn7.HeaderText = "MK";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 50;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Faktura";
            this.dataGridViewTextBoxColumn8.HeaderText = "Faknr";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 50;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn9.HeaderText = "Id";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Tblbilag";
            this.dataGridViewTextBoxColumn10.HeaderText = "Tblbilag";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // datoTextBox
            // 
            this.datoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.datoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblbilagBindingSource, "Dato", true));
            this.datoTextBox.Location = new System.Drawing.Point(45, 34);
            this.datoTextBox.Name = "datoTextBox";
            this.datoTextBox.ReadOnly = true;
            this.datoTextBox.Size = new System.Drawing.Size(71, 20);
            this.datoTextBox.TabIndex = 6;
            // 
            // FindTextBox
            // 
            this.FindTextBox.AcceptsReturn = true;
            this.FindTextBox.Location = new System.Drawing.Point(228, 34);
            this.FindTextBox.Name = "FindTextBox";
            this.FindTextBox.Size = new System.Drawing.Size(114, 20);
            this.FindTextBox.TabIndex = 7;
            // 
            // findLlabel
            // 
            findLlabel.AutoSize = true;
            findLlabel.Location = new System.Drawing.Point(197, 37);
            findLlabel.Name = "findLlabel";
            findLlabel.Size = new System.Drawing.Size(29, 13);
            findLlabel.TabIndex = 3;
            findLlabel.Text = "Søg:";
            // 
            // findBotton
            // 
            this.findBotton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findBotton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.findBotton.Location = new System.Drawing.Point(342, 34);
            this.findBotton.Name = "findBotton";
            this.findBotton.Size = new System.Drawing.Size(26, 20);
            this.findBotton.TabIndex = 8;
            this.findBotton.Text = "...";
            this.findBotton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.findBotton.UseVisualStyleBackColor = true;
            this.findBotton.Click += new System.EventHandler(this.findBotton_Click);
            // 
            // AddButton
            // 
            this.AddButton.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.tblbilagBindingSource, "Pid", true));
            this.AddButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AddButton.Location = new System.Drawing.Point(495, 34);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(37, 20);
            this.AddButton.TabIndex = 8;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // FrmBogfortKladde
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 321);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.findBotton);
            this.Controls.Add(this.FindTextBox);
            this.Controls.Add(this.datoTextBox);
            this.Controls.Add(this.tblkladderDataGridView);
            this.Controls.Add(findLlabel);
            this.Controls.Add(bilagLabel);
            this.Controls.Add(this.bilagTextBox);
            this.Controls.Add(datoLabel);
            this.Controls.Add(this.tblbilagBindingNavigator);
            this.Name = "FrmBogfortKladde";
            this.Text = "Bogført Kladde";
            this.Load += new System.EventHandler(this.frmBogfortKladde_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblbilagBindingNavigator)).EndInit();
            this.tblbilagBindingNavigator.ResumeLayout(false);
            this.tblbilagBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblkladderDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tblbilagBindingSource;
        private System.Windows.Forms.BindingNavigator tblbilagBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.TextBox bilagTextBox;
        private System.Windows.Forms.BindingSource tblkladderBindingSource;
        private System.Windows.Forms.DataGridView tblkladderDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.TextBox datoTextBox;
        private System.Windows.Forms.TextBox FindTextBox;
        private System.Windows.Forms.Button findBotton;
        private System.Windows.Forms.Button AddButton;

    }
}