namespace nsHafsjoldData
{
    partial class FrmRegnskab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegnskab));
            this.label_Rid = new System.Windows.Forms.Label();
            this.label_Navn = new System.Windows.Forms.Label();
            this.label_Oprettet = new System.Windows.Forms.Label();
            this.label_Start = new System.Windows.Forms.Label();
            this.label_Slut = new System.Windows.Forms.Label();
            this.label_DatoLaas = new System.Windows.Forms.Label();
            this.label_Placering = new System.Windows.Forms.Label();
            this.label_Eksportmappe = new System.Windows.Forms.Label();
            this.label_TilPBS = new System.Windows.Forms.Label();
            this.label_FraPBS = new System.Windows.Forms.Label();
            this.label1_Afsluttet = new System.Windows.Forms.Label();
            this.Rid = new System.Windows.Forms.TextBox();
            this.bsRegnskab = new System.Windows.Forms.BindingSource(this.components);
            this.Navn = new System.Windows.Forms.TextBox();
            this.Oprettet = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.TextBox();
            this.Slut = new System.Windows.Forms.TextBox();
            this.DatoLaas = new System.Windows.Forms.TextBox();
            this.Placering = new System.Windows.Forms.TextBox();
            this.Eksportmappe = new System.Windows.Forms.TextBox();
            this.TilPBS = new System.Windows.Forms.TextBox();
            this.FraPBS = new System.Windows.Forms.TextBox();
            this.Afsluttet = new System.Windows.Forms.TextBox();
            this.bnRegnskab = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.epRegnskab = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bsRegnskab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnRegnskab)).BeginInit();
            this.bnRegnskab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epRegnskab)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Rid
            // 
            this.label_Rid.AutoSize = true;
            this.label_Rid.Location = new System.Drawing.Point(12, 14);
            this.label_Rid.Name = "label_Rid";
            this.label_Rid.Size = new System.Drawing.Size(56, 13);
            this.label_Rid.TabIndex = 0;
            this.label_Rid.Text = "Regnskab";
            // 
            // label_Navn
            // 
            this.label_Navn.AutoSize = true;
            this.label_Navn.Location = new System.Drawing.Point(12, 47);
            this.label_Navn.Name = "label_Navn";
            this.label_Navn.Size = new System.Drawing.Size(33, 13);
            this.label_Navn.TabIndex = 0;
            this.label_Navn.Text = "Navn";
            // 
            // label_Oprettet
            // 
            this.label_Oprettet.AutoSize = true;
            this.label_Oprettet.Location = new System.Drawing.Point(13, 80);
            this.label_Oprettet.Name = "label_Oprettet";
            this.label_Oprettet.Size = new System.Drawing.Size(55, 13);
            this.label_Oprettet.TabIndex = 0;
            this.label_Oprettet.Text = "Oprettelse";
            // 
            // label_Start
            // 
            this.label_Start.AutoSize = true;
            this.label_Start.Location = new System.Drawing.Point(13, 113);
            this.label_Start.Name = "label_Start";
            this.label_Start.Size = new System.Drawing.Size(50, 13);
            this.label_Start.TabIndex = 0;
            this.label_Start.Text = "Startdato";
            // 
            // label_Slut
            // 
            this.label_Slut.AutoSize = true;
            this.label_Slut.Location = new System.Drawing.Point(12, 146);
            this.label_Slut.Name = "label_Slut";
            this.label_Slut.Size = new System.Drawing.Size(46, 13);
            this.label_Slut.TabIndex = 0;
            this.label_Slut.Text = "Slutdato";
            // 
            // label_DatoLaas
            // 
            this.label_DatoLaas.AutoSize = true;
            this.label_DatoLaas.Location = new System.Drawing.Point(13, 179);
            this.label_DatoLaas.Name = "label_DatoLaas";
            this.label_DatoLaas.Size = new System.Drawing.Size(63, 13);
            this.label_DatoLaas.TabIndex = 0;
            this.label_DatoLaas.Text = "Lås før dato";
            // 
            // label_Placering
            // 
            this.label_Placering.AutoSize = true;
            this.label_Placering.Location = new System.Drawing.Point(12, 212);
            this.label_Placering.Name = "label_Placering";
            this.label_Placering.Size = new System.Drawing.Size(51, 13);
            this.label_Placering.TabIndex = 0;
            this.label_Placering.Text = "Placering";
            // 
            // label_Eksportmappe
            // 
            this.label_Eksportmappe.AutoSize = true;
            this.label_Eksportmappe.Location = new System.Drawing.Point(12, 245);
            this.label_Eksportmappe.Name = "label_Eksportmappe";
            this.label_Eksportmappe.Size = new System.Drawing.Size(75, 13);
            this.label_Eksportmappe.TabIndex = 0;
            this.label_Eksportmappe.Text = "Eksportmappe";
            // 
            // label_TilPBS
            // 
            this.label_TilPBS.AutoSize = true;
            this.label_TilPBS.Location = new System.Drawing.Point(13, 278);
            this.label_TilPBS.Name = "label_TilPBS";
            this.label_TilPBS.Size = new System.Drawing.Size(42, 13);
            this.label_TilPBS.TabIndex = 0;
            this.label_TilPBS.Text = "Til PBS";
            // 
            // label_FraPBS
            // 
            this.label_FraPBS.AutoSize = true;
            this.label_FraPBS.Location = new System.Drawing.Point(12, 311);
            this.label_FraPBS.Name = "label_FraPBS";
            this.label_FraPBS.Size = new System.Drawing.Size(46, 13);
            this.label_FraPBS.TabIndex = 0;
            this.label_FraPBS.Text = "Fra PBS";
            // 
            // label1_Afsluttet
            // 
            this.label1_Afsluttet.AutoSize = true;
            this.label1_Afsluttet.Location = new System.Drawing.Point(12, 341);
            this.label1_Afsluttet.Name = "label1_Afsluttet";
            this.label1_Afsluttet.Size = new System.Drawing.Size(45, 13);
            this.label1_Afsluttet.TabIndex = 0;
            this.label1_Afsluttet.Text = "Afsluttet";
            // 
            // Rid
            // 
            this.Rid.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Rid", true));
            this.Rid.Location = new System.Drawing.Point(94, 11);
            this.Rid.Name = "Rid";
            this.Rid.Size = new System.Drawing.Size(43, 20);
            this.Rid.TabIndex = 1;
            // 
            // bsRegnskab
            // 
            this.bsRegnskab.DataSource = typeof(nsHafsjoldData.TblRegnskab);
            // 
            // Navn
            // 
            this.Navn.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Navn", true));
            this.Navn.Location = new System.Drawing.Point(94, 44);
            this.Navn.Name = "Navn";
            this.Navn.Size = new System.Drawing.Size(390, 20);
            this.Navn.TabIndex = 2;
            this.Navn.Validated += new System.EventHandler(this.Navn_Validated);
            this.Navn.Validating += new System.ComponentModel.CancelEventHandler(this.Navn_Validating);
            // 
            // Oprettet
            // 
            this.Oprettet.AcceptsReturn = true;
            this.Oprettet.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Oprettet", true));
            this.Oprettet.Location = new System.Drawing.Point(94, 77);
            this.Oprettet.Name = "Oprettet";
            this.Oprettet.Size = new System.Drawing.Size(145, 20);
            this.Oprettet.TabIndex = 2;
            // 
            // Start
            // 
            this.Start.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Start", true));
            this.Start.Location = new System.Drawing.Point(94, 113);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(95, 20);
            this.Start.TabIndex = 2;
            // 
            // Slut
            // 
            this.Slut.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Slut", true));
            this.Slut.Location = new System.Drawing.Point(94, 143);
            this.Slut.Name = "Slut";
            this.Slut.Size = new System.Drawing.Size(145, 20);
            this.Slut.TabIndex = 2;
            // 
            // DatoLaas
            // 
            this.DatoLaas.AcceptsReturn = true;
            this.DatoLaas.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "DatoLaas", true));
            this.DatoLaas.Location = new System.Drawing.Point(94, 176);
            this.DatoLaas.Name = "DatoLaas";
            this.DatoLaas.Size = new System.Drawing.Size(95, 20);
            this.DatoLaas.TabIndex = 2;
            // 
            // Placering
            // 
            this.Placering.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Placering", true));
            this.Placering.Location = new System.Drawing.Point(94, 209);
            this.Placering.Name = "Placering";
            this.Placering.Size = new System.Drawing.Size(390, 20);
            this.Placering.TabIndex = 2;
            // 
            // Eksportmappe
            // 
            this.Eksportmappe.AcceptsTab = true;
            this.Eksportmappe.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Eksportmappe", true));
            this.Eksportmappe.Location = new System.Drawing.Point(94, 242);
            this.Eksportmappe.Name = "Eksportmappe";
            this.Eksportmappe.Size = new System.Drawing.Size(390, 20);
            this.Eksportmappe.TabIndex = 2;
            // 
            // TilPBS
            // 
            this.TilPBS.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "TilPBS", true));
            this.TilPBS.Location = new System.Drawing.Point(94, 275);
            this.TilPBS.Name = "TilPBS";
            this.TilPBS.Size = new System.Drawing.Size(390, 20);
            this.TilPBS.TabIndex = 2;
            // 
            // FraPBS
            // 
            this.FraPBS.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "FraPBS", true));
            this.FraPBS.Location = new System.Drawing.Point(94, 308);
            this.FraPBS.Name = "FraPBS";
            this.FraPBS.Size = new System.Drawing.Size(390, 20);
            this.FraPBS.TabIndex = 2;
            // 
            // Afsluttet
            // 
            this.Afsluttet.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsRegnskab, "Afsluttet", true));
            this.Afsluttet.Location = new System.Drawing.Point(94, 341);
            this.Afsluttet.Name = "Afsluttet";
            this.Afsluttet.Size = new System.Drawing.Size(95, 20);
            this.Afsluttet.TabIndex = 2;
            // 
            // bnRegnskab
            // 
            this.bnRegnskab.AddNewItem = null;
            this.bnRegnskab.BindingSource = this.bsRegnskab;
            this.bnRegnskab.CountItem = this.bindingNavigatorCountItem;
            this.bnRegnskab.DeleteItem = null;
            this.bnRegnskab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnRegnskab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.bnRegnskab.Location = new System.Drawing.Point(0, 373);
            this.bnRegnskab.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnRegnskab.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnRegnskab.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnRegnskab.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnRegnskab.Name = "bnRegnskab";
            this.bnRegnskab.PositionItem = this.bindingNavigatorPositionItem;
            this.bnRegnskab.Size = new System.Drawing.Size(531, 25);
            this.bnRegnskab.TabIndex = 3;
            this.bnRegnskab.Text = "bindingNavigator1";
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
            // epRegnskab
            // 
            this.epRegnskab.ContainerControl = this;
            this.epRegnskab.DataSource = this.bsRegnskab;
            // 
            // FrmRegnskab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsHafsjoldData.Properties.Settings.Default.frmRegnskabSize;
            this.Controls.Add(this.bnRegnskab);
            this.Controls.Add(this.Afsluttet);
            this.Controls.Add(this.FraPBS);
            this.Controls.Add(this.TilPBS);
            this.Controls.Add(this.Eksportmappe);
            this.Controls.Add(this.Placering);
            this.Controls.Add(this.DatoLaas);
            this.Controls.Add(this.Slut);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Oprettet);
            this.Controls.Add(this.Navn);
            this.Controls.Add(this.Rid);
            this.Controls.Add(this.label1_Afsluttet);
            this.Controls.Add(this.label_FraPBS);
            this.Controls.Add(this.label_TilPBS);
            this.Controls.Add(this.label_Eksportmappe);
            this.Controls.Add(this.label_Placering);
            this.Controls.Add(this.label_DatoLaas);
            this.Controls.Add(this.label_Slut);
            this.Controls.Add(this.label_Start);
            this.Controls.Add(this.label_Oprettet);
            this.Controls.Add(this.label_Navn);
            this.Controls.Add(this.label_Rid);
            this.DataBindings.Add(new System.Windows.Forms.Binding("WindowState", global::nsHafsjoldData.Properties.Settings.Default, "frmRegnskabState", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsHafsjoldData.Properties.Settings.Default, "frmRegnskabSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsHafsjoldData.Properties.Settings.Default, "frmRegnskabPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsHafsjoldData.Properties.Settings.Default.frmRegnskabPoint;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegnskab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Regnskab";
            this.WindowState = global::nsHafsjoldData.Properties.Settings.Default.frmRegnskabState;
            this.Load += new System.EventHandler(this.FrmRegnskab_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRegnskab_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bsRegnskab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bnRegnskab)).EndInit();
            this.bnRegnskab.ResumeLayout(false);
            this.bnRegnskab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epRegnskab)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Rid;
        private System.Windows.Forms.Label label_Navn;
        private System.Windows.Forms.Label label_Oprettet;
        private System.Windows.Forms.Label label_Start;
        private System.Windows.Forms.Label label_Slut;
        private System.Windows.Forms.Label label_DatoLaas;
        private System.Windows.Forms.Label label_Placering;
        private System.Windows.Forms.Label label_Eksportmappe;
        private System.Windows.Forms.Label label_TilPBS;
        private System.Windows.Forms.Label label_FraPBS;
        private System.Windows.Forms.Label label1_Afsluttet;
        private System.Windows.Forms.TextBox Rid;
        private System.Windows.Forms.TextBox Navn;
        private System.Windows.Forms.TextBox Oprettet;
        private System.Windows.Forms.TextBox Start;
        private System.Windows.Forms.TextBox Slut;
        private System.Windows.Forms.TextBox DatoLaas;
        private System.Windows.Forms.TextBox Placering;
        private System.Windows.Forms.TextBox Eksportmappe;
        private System.Windows.Forms.TextBox TilPBS;
        private System.Windows.Forms.TextBox FraPBS;
        private System.Windows.Forms.TextBox Afsluttet;
        private System.Windows.Forms.BindingNavigator bnRegnskab;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource bsRegnskab;
        private System.Windows.Forms.ErrorProvider epRegnskab;
    }
}