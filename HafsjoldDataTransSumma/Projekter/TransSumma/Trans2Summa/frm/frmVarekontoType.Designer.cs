namespace Trans2Summa
{
    partial class FrmVarekontoType
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
            System.Windows.Forms.Label kontonrLabel;
            System.Windows.Forms.Label omktypeLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVarekontoType));
            this.tblvareomkostningerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblvareomkostningerBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.tblvareomkostningerBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.kontonrTextBox = new System.Windows.Forms.TextBox();
            this.omktypeComboBox = new System.Windows.Forms.ComboBox();
            this.labelKontotekst = new System.Windows.Forms.Label();
            kontonrLabel = new System.Windows.Forms.Label();
            omktypeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblvareomkostningerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblvareomkostningerBindingNavigator)).BeginInit();
            this.tblvareomkostningerBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // kontonrLabel
            // 
            kontonrLabel.AutoSize = true;
            kontonrLabel.Location = new System.Drawing.Point(9, 17);
            kontonrLabel.Name = "kontonrLabel";
            kontonrLabel.Size = new System.Drawing.Size(47, 13);
            kontonrLabel.TabIndex = 1;
            kontonrLabel.Text = "Kontonr:";
            // 
            // omktypeLabel
            // 
            omktypeLabel.AutoSize = true;
            omktypeLabel.Location = new System.Drawing.Point(6, 44);
            omktypeLabel.Name = "omktypeLabel";
            omktypeLabel.Size = new System.Drawing.Size(52, 13);
            omktypeLabel.TabIndex = 3;
            omktypeLabel.Text = "Omktype:";
            // 
            // tblvareomkostningerBindingSource
            // 
            this.tblvareomkostningerBindingSource.DataSource = typeof(Trans2Summa.Tblvareomkostninger);
            // 
            // tblvareomkostningerBindingNavigator
            // 
            this.tblvareomkostningerBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.tblvareomkostningerBindingNavigator.BindingSource = this.tblvareomkostningerBindingSource;
            this.tblvareomkostningerBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblvareomkostningerBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.tblvareomkostningerBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblvareomkostningerBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.tblvareomkostningerBindingNavigatorSaveItem});
            this.tblvareomkostningerBindingNavigator.Location = new System.Drawing.Point(0, 81);
            this.tblvareomkostningerBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblvareomkostningerBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblvareomkostningerBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblvareomkostningerBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblvareomkostningerBindingNavigator.Name = "tblvareomkostningerBindingNavigator";
            this.tblvareomkostningerBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblvareomkostningerBindingNavigator.Size = new System.Drawing.Size(370, 25);
            this.tblvareomkostningerBindingNavigator.TabIndex = 0;
            this.tblvareomkostningerBindingNavigator.Text = "bindingNavigator1";
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
            // tblvareomkostningerBindingNavigatorSaveItem
            // 
            this.tblvareomkostningerBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblvareomkostningerBindingNavigatorSaveItem.Enabled = false;
            this.tblvareomkostningerBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblvareomkostningerBindingNavigatorSaveItem.Image")));
            this.tblvareomkostningerBindingNavigatorSaveItem.Name = "tblvareomkostningerBindingNavigatorSaveItem";
            this.tblvareomkostningerBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblvareomkostningerBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // kontonrTextBox
            // 
            this.kontonrTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblvareomkostningerBindingSource, "Kontonr", true));
            this.kontonrTextBox.Location = new System.Drawing.Point(64, 14);
            this.kontonrTextBox.Name = "kontonrTextBox";
            this.kontonrTextBox.Size = new System.Drawing.Size(53, 20);
            this.kontonrTextBox.TabIndex = 2;
            this.kontonrTextBox.TextChanged += new System.EventHandler(this.kontonrTextBox_TextChanged);
            this.kontonrTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kontonrTextBox_MouseDown);
            // 
            // omktypeComboBox
            // 
            this.omktypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblvareomkostningerBindingSource, "Omktype", true));
            this.omktypeComboBox.FormattingEnabled = true;
            this.omktypeComboBox.Items.AddRange(new object[] {
            "vareomk",
            "vareforb"});
            this.omktypeComboBox.Location = new System.Drawing.Point(64, 41);
            this.omktypeComboBox.Name = "omktypeComboBox";
            this.omktypeComboBox.Size = new System.Drawing.Size(76, 21);
            this.omktypeComboBox.TabIndex = 4;
            // 
            // labelKontotekst
            // 
            this.labelKontotekst.AutoSize = true;
            this.labelKontotekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKontotekst.Location = new System.Drawing.Point(123, 16);
            this.labelKontotekst.Name = "labelKontotekst";
            this.labelKontotekst.Size = new System.Drawing.Size(11, 15);
            this.labelKontotekst.TabIndex = 5;
            this.labelKontotekst.Text = " ";
            // 
            // FrmVarekontoType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Trans2Summa.Properties.Settings.Default.FrmVarekontoTypeSize;
            this.Controls.Add(this.labelKontotekst);
            this.Controls.Add(omktypeLabel);
            this.Controls.Add(this.omktypeComboBox);
            this.Controls.Add(kontonrLabel);
            this.Controls.Add(this.kontonrTextBox);
            this.Controls.Add(this.tblvareomkostningerBindingNavigator);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Trans2Summa.Properties.Settings.Default, "FrmVarekontoTypeLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Trans2Summa.Properties.Settings.Default, "FrmVarekontoTypeSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Trans2Summa.Properties.Settings.Default.FrmVarekontoTypeLocation;
            this.Name = "FrmVarekontoType";
            this.Text = "VarekontoType";
            this.Load += new System.EventHandler(this.FrmVarekontoType_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmVarekontoType_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.tblvareomkostningerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblvareomkostningerBindingNavigator)).EndInit();
            this.tblvareomkostningerBindingNavigator.ResumeLayout(false);
            this.tblvareomkostningerBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tblvareomkostningerBindingSource;
        private System.Windows.Forms.BindingNavigator tblvareomkostningerBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton tblvareomkostningerBindingNavigatorSaveItem;
        private System.Windows.Forms.TextBox kontonrTextBox;
        private System.Windows.Forms.ComboBox omktypeComboBox;
        private System.Windows.Forms.Label labelKontotekst;
    }
}