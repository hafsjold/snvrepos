namespace nsPuls3060
{
    partial class FrmKreditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKreditor));
            this.bsKreditor = new System.Windows.Forms.BindingSource(this.components);
            this.bNavKreditor = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem1 = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem1 = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem1 = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.Datalevnr = new System.Windows.Forms.TextBox();
            this.label_Datalevnr = new System.Windows.Forms.Label();
            this.Datalevnavn = new System.Windows.Forms.TextBox();
            this.label_Datalevnavn = new System.Windows.Forms.Label();
            this.Pbsnr = new System.Windows.Forms.TextBox();
            this.label_Pbsnr = new System.Windows.Forms.Label();
            this.Delsystem = new System.Windows.Forms.TextBox();
            this.label_Delsystem = new System.Windows.Forms.Label();
            this.Regnr = new System.Windows.Forms.TextBox();
            this.label_Regnr = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Kontonr = new System.Windows.Forms.TextBox();
            this.label_Kontonr = new System.Windows.Forms.Label();
            this.Debgrpnr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Sektionnr = new System.Windows.Forms.TextBox();
            this.label_Sektionnr = new System.Windows.Forms.Label();
            this.Transkodebetaling = new System.Windows.Forms.TextBox();
            this.label_Transkodebetaling = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsKreditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bNavKreditor)).BeginInit();
            this.bNavKreditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsKreditor
            // 
            this.bsKreditor.DataSource = typeof(nsPuls3060.MemKreditor);
            // 
            // bNavKreditor
            // 
            this.bNavKreditor.AddNewItem = this.bindingNavigatorAddNewItem1;
            this.bNavKreditor.BindingSource = this.bsKreditor;
            this.bNavKreditor.CountItem = this.bindingNavigatorCountItem1;
            this.bNavKreditor.DeleteItem = this.bindingNavigatorDeleteItem1;
            this.bNavKreditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bNavKreditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem1,
            this.bindingNavigatorMovePreviousItem1,
            this.bindingNavigatorSeparator3,
            this.bindingNavigatorPositionItem1,
            this.bindingNavigatorCountItem1,
            this.bindingNavigatorSeparator4,
            this.bindingNavigatorMoveNextItem1,
            this.bindingNavigatorMoveLastItem1,
            this.bindingNavigatorSeparator5,
            this.bindingNavigatorAddNewItem1,
            this.bindingNavigatorDeleteItem1,
            this.toolStripSeparator});
            this.bNavKreditor.Location = new System.Drawing.Point(0, 275);
            this.bNavKreditor.MoveFirstItem = this.bindingNavigatorMoveFirstItem1;
            this.bNavKreditor.MoveLastItem = this.bindingNavigatorMoveLastItem1;
            this.bNavKreditor.MoveNextItem = this.bindingNavigatorMoveNextItem1;
            this.bNavKreditor.MovePreviousItem = this.bindingNavigatorMovePreviousItem1;
            this.bNavKreditor.Name = "bNavKreditor";
            this.bNavKreditor.PositionItem = this.bindingNavigatorPositionItem1;
            this.bNavKreditor.Size = new System.Drawing.Size(304, 25);
            this.bNavKreditor.TabIndex = 1;
            this.bNavKreditor.Text = "bindingNavigator2";
            // 
            // bindingNavigatorAddNewItem1
            // 
            this.bindingNavigatorAddNewItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem1.Image")));
            this.bindingNavigatorAddNewItem1.Name = "bindingNavigatorAddNewItem1";
            this.bindingNavigatorAddNewItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem1.Text = "Add new";
            // 
            // bindingNavigatorCountItem1
            // 
            this.bindingNavigatorCountItem1.Name = "bindingNavigatorCountItem1";
            this.bindingNavigatorCountItem1.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem1.Text = "of {0}";
            this.bindingNavigatorCountItem1.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem1
            // 
            this.bindingNavigatorDeleteItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem1.Image")));
            this.bindingNavigatorDeleteItem1.Name = "bindingNavigatorDeleteItem1";
            this.bindingNavigatorDeleteItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem1.Text = "Delete";
            // 
            // bindingNavigatorMoveFirstItem1
            // 
            this.bindingNavigatorMoveFirstItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem1.Image")));
            this.bindingNavigatorMoveFirstItem1.Name = "bindingNavigatorMoveFirstItem1";
            this.bindingNavigatorMoveFirstItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem1.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem1
            // 
            this.bindingNavigatorMovePreviousItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem1.Image")));
            this.bindingNavigatorMovePreviousItem1.Name = "bindingNavigatorMovePreviousItem1";
            this.bindingNavigatorMovePreviousItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem1.Text = "Move previous";
            // 
            // bindingNavigatorSeparator3
            // 
            this.bindingNavigatorSeparator3.Name = "bindingNavigatorSeparator3";
            this.bindingNavigatorSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem1
            // 
            this.bindingNavigatorPositionItem1.AccessibleName = "Position";
            this.bindingNavigatorPositionItem1.AutoSize = false;
            this.bindingNavigatorPositionItem1.Name = "bindingNavigatorPositionItem1";
            this.bindingNavigatorPositionItem1.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem1.Text = "0";
            this.bindingNavigatorPositionItem1.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator4
            // 
            this.bindingNavigatorSeparator4.Name = "bindingNavigatorSeparator4";
            this.bindingNavigatorSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem1
            // 
            this.bindingNavigatorMoveNextItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem1.Image")));
            this.bindingNavigatorMoveNextItem1.Name = "bindingNavigatorMoveNextItem1";
            this.bindingNavigatorMoveNextItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem1.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem1
            // 
            this.bindingNavigatorMoveLastItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem1.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem1.Image")));
            this.bindingNavigatorMoveLastItem1.Name = "bindingNavigatorMoveLastItem1";
            this.bindingNavigatorMoveLastItem1.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem1.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem1.Text = "Move last";
            // 
            // bindingNavigatorSeparator5
            // 
            this.bindingNavigatorSeparator5.Name = "bindingNavigatorSeparator5";
            this.bindingNavigatorSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // Datalevnr
            // 
            this.Datalevnr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Datalevnr", true));
            this.Datalevnr.Location = new System.Drawing.Point(127, 35);
            this.Datalevnr.MaxLength = 8;
            this.Datalevnr.Name = "Datalevnr";
            this.Datalevnr.Size = new System.Drawing.Size(83, 20);
            this.Datalevnr.TabIndex = 2;
            // 
            // label_Datalevnr
            // 
            this.label_Datalevnr.AutoSize = true;
            this.label_Datalevnr.Location = new System.Drawing.Point(14, 38);
            this.label_Datalevnr.Name = "label_Datalevnr";
            this.label_Datalevnr.Size = new System.Drawing.Size(18, 13);
            this.label_Datalevnr.TabIndex = 3;
            this.label_Datalevnr.Text = "Nr";
            // 
            // Datalevnavn
            // 
            this.Datalevnavn.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Datalevnavn", true));
            this.Datalevnavn.Location = new System.Drawing.Point(127, 61);
            this.Datalevnavn.MaxLength = 15;
            this.Datalevnavn.Name = "Datalevnavn";
            this.Datalevnavn.Size = new System.Drawing.Size(156, 20);
            this.Datalevnavn.TabIndex = 3;
            // 
            // label_Datalevnavn
            // 
            this.label_Datalevnavn.AutoSize = true;
            this.label_Datalevnavn.Location = new System.Drawing.Point(14, 64);
            this.label_Datalevnavn.Name = "label_Datalevnavn";
            this.label_Datalevnavn.Size = new System.Drawing.Size(33, 13);
            this.label_Datalevnavn.TabIndex = 3;
            this.label_Datalevnavn.Text = "Navn";
            // 
            // Pbsnr
            // 
            this.Pbsnr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Pbsnr", true));
            this.Pbsnr.Location = new System.Drawing.Point(127, 87);
            this.Pbsnr.MaxLength = 8;
            this.Pbsnr.Name = "Pbsnr";
            this.Pbsnr.Size = new System.Drawing.Size(83, 20);
            this.Pbsnr.TabIndex = 4;
            // 
            // label_Pbsnr
            // 
            this.label_Pbsnr.AutoSize = true;
            this.label_Pbsnr.Location = new System.Drawing.Point(14, 90);
            this.label_Pbsnr.Name = "label_Pbsnr";
            this.label_Pbsnr.Size = new System.Drawing.Size(68, 13);
            this.label_Pbsnr.TabIndex = 3;
            this.label_Pbsnr.Text = "PBS nummer";
            // 
            // Delsystem
            // 
            this.Delsystem.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Delsystem", true));
            this.Delsystem.Location = new System.Drawing.Point(127, 113);
            this.Delsystem.MaxLength = 3;
            this.Delsystem.Name = "Delsystem";
            this.Delsystem.Size = new System.Drawing.Size(83, 20);
            this.Delsystem.TabIndex = 5;
            // 
            // label_Delsystem
            // 
            this.label_Delsystem.AutoSize = true;
            this.label_Delsystem.Location = new System.Drawing.Point(14, 116);
            this.label_Delsystem.Name = "label_Delsystem";
            this.label_Delsystem.Size = new System.Drawing.Size(55, 13);
            this.label_Delsystem.TabIndex = 3;
            this.label_Delsystem.Text = "Delsystem";
            // 
            // Regnr
            // 
            this.Regnr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Regnr", true));
            this.Regnr.Location = new System.Drawing.Point(127, 139);
            this.Regnr.MaxLength = 4;
            this.Regnr.Name = "Regnr";
            this.Regnr.Size = new System.Drawing.Size(83, 20);
            this.Regnr.TabIndex = 6;
            // 
            // label_Regnr
            // 
            this.label_Regnr.AutoSize = true;
            this.label_Regnr.Location = new System.Drawing.Point(14, 142);
            this.label_Regnr.Name = "label_Regnr";
            this.label_Regnr.Size = new System.Drawing.Size(36, 13);
            this.label_Regnr.TabIndex = 3;
            this.label_Regnr.Text = "Regnr";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(124, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "Dataleverandør";
            // 
            // Kontonr
            // 
            this.Kontonr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Kontonr", true));
            this.Kontonr.Location = new System.Drawing.Point(127, 165);
            this.Kontonr.MaxLength = 10;
            this.Kontonr.Name = "Kontonr";
            this.Kontonr.Size = new System.Drawing.Size(83, 20);
            this.Kontonr.TabIndex = 7;
            // 
            // label_Kontonr
            // 
            this.label_Kontonr.AutoSize = true;
            this.label_Kontonr.Location = new System.Drawing.Point(14, 168);
            this.label_Kontonr.Name = "label_Kontonr";
            this.label_Kontonr.Size = new System.Drawing.Size(44, 13);
            this.label_Kontonr.TabIndex = 3;
            this.label_Kontonr.Text = "Kontonr";
            // 
            // Debgrpnr
            // 
            this.Debgrpnr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Debgrpnr", true));
            this.Debgrpnr.Location = new System.Drawing.Point(127, 191);
            this.Debgrpnr.MaxLength = 5;
            this.Debgrpnr.Name = "Debgrpnr";
            this.Debgrpnr.Size = new System.Drawing.Size(83, 20);
            this.Debgrpnr.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Debitor gruppe nr";
            // 
            // Sektionnr
            // 
            this.Sektionnr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Sektionnr", true));
            this.Sektionnr.Location = new System.Drawing.Point(127, 217);
            this.Sektionnr.MaxLength = 4;
            this.Sektionnr.Name = "Sektionnr";
            this.Sektionnr.Size = new System.Drawing.Size(83, 20);
            this.Sektionnr.TabIndex = 9;
            // 
            // label_Sektionnr
            // 
            this.label_Sektionnr.AutoSize = true;
            this.label_Sektionnr.Location = new System.Drawing.Point(14, 220);
            this.label_Sektionnr.Name = "label_Sektionnr";
            this.label_Sektionnr.Size = new System.Drawing.Size(52, 13);
            this.label_Sektionnr.TabIndex = 3;
            this.label_Sektionnr.Text = "Sektionnr";
            // 
            // Transkodebetaling
            // 
            this.Transkodebetaling.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsKreditor, "Transkodebetaling", true));
            this.Transkodebetaling.Location = new System.Drawing.Point(127, 243);
            this.Transkodebetaling.MaxLength = 4;
            this.Transkodebetaling.Name = "Transkodebetaling";
            this.Transkodebetaling.Size = new System.Drawing.Size(83, 20);
            this.Transkodebetaling.TabIndex = 10;
            // 
            // label_Transkodebetaling
            // 
            this.label_Transkodebetaling.AutoSize = true;
            this.label_Transkodebetaling.Location = new System.Drawing.Point(14, 246);
            this.label_Transkodebetaling.Name = "label_Transkodebetaling";
            this.label_Transkodebetaling.Size = new System.Drawing.Size(101, 13);
            this.label_Transkodebetaling.TabIndex = 3;
            this.label_Transkodebetaling.Text = "Trans kode betaling";
            // 
            // FrmKreditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmKreditorSize;
            this.Controls.Add(this.label_Transkodebetaling);
            this.Controls.Add(this.label_Sektionnr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_Kontonr);
            this.Controls.Add(this.label_Regnr);
            this.Controls.Add(this.label_Delsystem);
            this.Controls.Add(this.label_Pbsnr);
            this.Controls.Add(this.label_Datalevnavn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_Datalevnr);
            this.Controls.Add(this.Transkodebetaling);
            this.Controls.Add(this.Sektionnr);
            this.Controls.Add(this.Debgrpnr);
            this.Controls.Add(this.Kontonr);
            this.Controls.Add(this.Regnr);
            this.Controls.Add(this.Delsystem);
            this.Controls.Add(this.Pbsnr);
            this.Controls.Add(this.Datalevnavn);
            this.Controls.Add(this.Datalevnr);
            this.Controls.Add(this.bNavKreditor);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmKreditorPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmKreditorSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::nsPuls3060.Properties.Settings.Default, "frmKreditorState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmKreditorPoint;
            this.Name = "FrmKreditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Kreditor";
            this.WindowState = global::nsPuls3060.Properties.Settings.Default.frmKreditorState;
            this.Load += new System.EventHandler(this.FrmKreditor_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmKreditor_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bsKreditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bNavKreditor)).EndInit();
            this.bNavKreditor.ResumeLayout(false);
            this.bNavKreditor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsKreditor;
        private System.Windows.Forms.BindingNavigator bNavKreditor;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem1;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator3;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem1;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator4;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem1;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.TextBox Datalevnr;
        private System.Windows.Forms.Label label_Datalevnr;
        private System.Windows.Forms.TextBox Datalevnavn;
        private System.Windows.Forms.Label label_Datalevnavn;
        private System.Windows.Forms.TextBox Pbsnr;
        private System.Windows.Forms.Label label_Pbsnr;
        private System.Windows.Forms.TextBox Delsystem;
        private System.Windows.Forms.Label label_Delsystem;
        private System.Windows.Forms.TextBox Regnr;
        private System.Windows.Forms.Label label_Regnr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Kontonr;
        private System.Windows.Forms.Label label_Kontonr;
        private System.Windows.Forms.TextBox Debgrpnr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Sektionnr;
        private System.Windows.Forms.Label label_Sektionnr;
        private System.Windows.Forms.TextBox Transkodebetaling;
        private System.Windows.Forms.Label label_Transkodebetaling;

    }
}