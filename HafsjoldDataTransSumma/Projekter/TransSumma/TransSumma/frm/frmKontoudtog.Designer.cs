namespace nsPuls3060
{
    partial class FrmKontoudtog
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
            System.Windows.Forms.Label pidLabel;
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label savefileLabel;
            System.Windows.Forms.Label bogfkontoLabel;
            System.Windows.Forms.Label afstemningskontoLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKontoudtog));
            this.bsTblkontoudtog = new System.Windows.Forms.BindingSource(this.components);
            this.bnTblkontoudtog = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.tblkontoudtogBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tbPid = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbSavefile = new System.Windows.Forms.TextBox();
            this.tbBogfkonto = new System.Windows.Forms.TextBox();
            this.tbAfstemningskonto = new System.Windows.Forms.TextBox();
            pidLabel = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            savefileLabel = new System.Windows.Forms.Label();
            bogfkontoLabel = new System.Windows.Forms.Label();
            afstemningskontoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsTblkontoudtog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnTblkontoudtog)).BeginInit();
            this.bnTblkontoudtog.SuspendLayout();
            this.SuspendLayout();
            // 
            // pidLabel
            // 
            pidLabel.AutoSize = true;
            pidLabel.Location = new System.Drawing.Point(12, 25);
            pidLabel.Name = "pidLabel";
            pidLabel.Size = new System.Drawing.Size(18, 13);
            pidLabel.TabIndex = 1;
            pidLabel.Text = "Nr";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(12, 64);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(33, 13);
            nameLabel.TabIndex = 3;
            nameLabel.Text = "Navn";
            // 
            // savefileLabel
            // 
            savefileLabel.AutoSize = true;
            savefileLabel.Location = new System.Drawing.Point(12, 103);
            savefileLabel.Name = "savefileLabel";
            savefileLabel.Size = new System.Drawing.Size(45, 13);
            savefileLabel.TabIndex = 5;
            savefileLabel.Text = "Savefile";
            // 
            // bogfkontoLabel
            // 
            bogfkontoLabel.AutoSize = true;
            bogfkontoLabel.Location = new System.Drawing.Point(12, 142);
            bogfkontoLabel.Name = "bogfkontoLabel";
            bogfkontoLabel.Size = new System.Drawing.Size(35, 13);
            bogfkontoLabel.TabIndex = 7;
            bogfkontoLabel.Text = "Konto";
            // 
            // afstemningskontoLabel
            // 
            afstemningskontoLabel.AutoSize = true;
            afstemningskontoLabel.Location = new System.Drawing.Point(12, 181);
            afstemningskontoLabel.Name = "afstemningskontoLabel";
            afstemningskontoLabel.Size = new System.Drawing.Size(59, 13);
            afstemningskontoLabel.TabIndex = 9;
            afstemningskontoLabel.Text = "Afstemning";
            // 
            // bsTblkontoudtog
            // 
            this.bsTblkontoudtog.DataSource = typeof(nsPuls3060.Tblkontoudtog);
            // 
            // bnTblkontoudtog
            // 
            this.bnTblkontoudtog.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnTblkontoudtog.BindingSource = this.bsTblkontoudtog;
            this.bnTblkontoudtog.CountItem = this.bindingNavigatorCountItem;
            this.bnTblkontoudtog.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnTblkontoudtog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnTblkontoudtog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tblkontoudtogBindingNavigatorSaveItem});
            this.bnTblkontoudtog.Location = new System.Drawing.Point(0, 225);
            this.bnTblkontoudtog.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnTblkontoudtog.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnTblkontoudtog.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnTblkontoudtog.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnTblkontoudtog.Name = "bnTblkontoudtog";
            this.bnTblkontoudtog.PositionItem = this.bindingNavigatorPositionItem;
            this.bnTblkontoudtog.Size = new System.Drawing.Size(324, 25);
            this.bnTblkontoudtog.TabIndex = 0;
            this.bnTblkontoudtog.Text = "bindingNavigator1";
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
            // tblkontoudtogBindingNavigatorSaveItem
            // 
            this.tblkontoudtogBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblkontoudtogBindingNavigatorSaveItem.Enabled = false;
            this.tblkontoudtogBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblkontoudtogBindingNavigatorSaveItem.Image")));
            this.tblkontoudtogBindingNavigatorSaveItem.Name = "tblkontoudtogBindingNavigatorSaveItem";
            this.tblkontoudtogBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblkontoudtogBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tbPid
            // 
            this.tbPid.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblkontoudtog, "Pid", true));
            this.tbPid.Location = new System.Drawing.Point(85, 22);
            this.tbPid.Name = "tbPid";
            this.tbPid.Size = new System.Drawing.Size(36, 20);
            this.tbPid.TabIndex = 2;
            // 
            // tbName
            // 
            this.tbName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblkontoudtog, "Name", true));
            this.tbName.Location = new System.Drawing.Point(85, 61);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(171, 20);
            this.tbName.TabIndex = 4;
            // 
            // tbSavefile
            // 
            this.tbSavefile.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblkontoudtog, "Savefile", true));
            this.tbSavefile.Location = new System.Drawing.Point(85, 100);
            this.tbSavefile.Name = "tbSavefile";
            this.tbSavefile.Size = new System.Drawing.Size(171, 20);
            this.tbSavefile.TabIndex = 6;
            // 
            // tbBogfkonto
            // 
            this.tbBogfkonto.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblkontoudtog, "Bogfkonto", true));
            this.tbBogfkonto.Location = new System.Drawing.Point(85, 139);
            this.tbBogfkonto.Name = "tbBogfkonto";
            this.tbBogfkonto.Size = new System.Drawing.Size(49, 20);
            this.tbBogfkonto.TabIndex = 8;
            // 
            // tbAfstemningskonto
            // 
            this.tbAfstemningskonto.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblkontoudtog, "Afstemningskonto", true));
            this.tbAfstemningskonto.Location = new System.Drawing.Point(85, 178);
            this.tbAfstemningskonto.Name = "tbAfstemningskonto";
            this.tbAfstemningskonto.Size = new System.Drawing.Size(100, 20);
            this.tbAfstemningskonto.TabIndex = 10;
            // 
            // FrmKontoudtog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmKontoudtogSize;
            this.Controls.Add(afstemningskontoLabel);
            this.Controls.Add(this.tbAfstemningskonto);
            this.Controls.Add(bogfkontoLabel);
            this.Controls.Add(this.tbBogfkonto);
            this.Controls.Add(savefileLabel);
            this.Controls.Add(this.tbSavefile);
            this.Controls.Add(nameLabel);
            this.Controls.Add(this.tbName);
            this.Controls.Add(pidLabel);
            this.Controls.Add(this.tbPid);
            this.Controls.Add(this.bnTblkontoudtog);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmKontoudtogSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmKontoudtogLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmKontoudtogLocation;
            this.Name = "FrmKontoudtog";
            this.Text = "Kontoudtog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmKontoudtog_FormClosing);
            this.Load += new System.EventHandler(this.FrmKontoudtog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsTblkontoudtog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnTblkontoudtog)).EndInit();
            this.bnTblkontoudtog.ResumeLayout(false);
            this.bnTblkontoudtog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsTblkontoudtog;
        private System.Windows.Forms.BindingNavigator bnTblkontoudtog;
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
        private System.Windows.Forms.ToolStripButton tblkontoudtogBindingNavigatorSaveItem;
        private System.Windows.Forms.TextBox tbPid;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbSavefile;
        private System.Windows.Forms.TextBox tbBogfkonto;
        private System.Windows.Forms.TextBox tbAfstemningskonto;
    }
}