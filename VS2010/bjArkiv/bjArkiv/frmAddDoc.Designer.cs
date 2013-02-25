namespace bjArkiv
{
    partial class frmAddDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddDoc));
            this.labelVirksomhed = new System.Windows.Forms.Label();
            this.labelÅr = new System.Windows.Forms.Label();
            this.labelBeskrivelse = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.labelDokument = new System.Windows.Forms.Label();
            this.labelEmne = new System.Windows.Forms.Label();
            this.labelDokument_type = new System.Windows.Forms.Label();
            this.labelEkstern_ref = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.labelRef_nr = new System.Windows.Forms.Label();
            this.xmldocsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.xmldocsBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
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
            this.xmldocsBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.txtBoxRef_nr = new System.Windows.Forms.TextBox();
            this.txtBoxDokument = new System.Windows.Forms.TextBox();
            this.txtBoxVirksomhed = new System.Windows.Forms.TextBox();
            this.txtBoxEmne = new System.Windows.Forms.TextBox();
            this.txtBoxDokument_type = new System.Windows.Forms.TextBox();
            this.txtBoxÅr = new System.Windows.Forms.TextBox();
            this.txtBoxEkstern_kilde = new System.Windows.Forms.TextBox();
            this.txtBoxBeskrivelse = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.xmldocsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmldocsBindingNavigator)).BeginInit();
            this.xmldocsBindingNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelVirksomhed
            // 
            this.labelVirksomhed.AutoSize = true;
            this.labelVirksomhed.Location = new System.Drawing.Point(8, 71);
            this.labelVirksomhed.Name = "labelVirksomhed";
            this.labelVirksomhed.Size = new System.Drawing.Size(62, 13);
            this.labelVirksomhed.TabIndex = 10;
            this.labelVirksomhed.Text = "Virksomhed";
            // 
            // labelÅr
            // 
            this.labelÅr.AutoSize = true;
            this.labelÅr.Location = new System.Drawing.Point(8, 149);
            this.labelÅr.Name = "labelÅr";
            this.labelÅr.Size = new System.Drawing.Size(17, 13);
            this.labelÅr.TabIndex = 13;
            this.labelÅr.Text = "År";
            // 
            // labelBeskrivelse
            // 
            this.labelBeskrivelse.AutoSize = true;
            this.labelBeskrivelse.Location = new System.Drawing.Point(8, 201);
            this.labelBeskrivelse.Name = "labelBeskrivelse";
            this.labelBeskrivelse.Size = new System.Drawing.Size(61, 13);
            this.labelBeskrivelse.TabIndex = 15;
            this.labelBeskrivelse.Text = "Beskrivelse";
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(79, 232);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(92, 23);
            this.butOK.TabIndex = 6;
            this.butOK.Text = "Indsæt";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // labelDokument
            // 
            this.labelDokument.AutoSize = true;
            this.labelDokument.Location = new System.Drawing.Point(8, 43);
            this.labelDokument.Name = "labelDokument";
            this.labelDokument.Size = new System.Drawing.Size(56, 13);
            this.labelDokument.TabIndex = 9;
            this.labelDokument.Text = "Dokument";
            // 
            // labelEmne
            // 
            this.labelEmne.AutoSize = true;
            this.labelEmne.Location = new System.Drawing.Point(8, 97);
            this.labelEmne.Name = "labelEmne";
            this.labelEmne.Size = new System.Drawing.Size(34, 13);
            this.labelEmne.TabIndex = 11;
            this.labelEmne.Text = "Emne";
            // 
            // labelDokument_type
            // 
            this.labelDokument_type.AutoSize = true;
            this.labelDokument_type.Location = new System.Drawing.Point(8, 123);
            this.labelDokument_type.Name = "labelDokument_type";
            this.labelDokument_type.Size = new System.Drawing.Size(50, 13);
            this.labelDokument_type.TabIndex = 12;
            this.labelDokument_type.Text = "Dok type";
            // 
            // labelEkstern_ref
            // 
            this.labelEkstern_ref.AutoSize = true;
            this.labelEkstern_ref.Location = new System.Drawing.Point(8, 175);
            this.labelEkstern_ref.Name = "labelEkstern_ref";
            this.labelEkstern_ref.Size = new System.Drawing.Size(58, 13);
            this.labelEkstern_ref.TabIndex = 14;
            this.labelEkstern_ref.Text = "Ekstern ref";
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(265, 232);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(92, 23);
            this.butCancel.TabIndex = 7;
            this.butCancel.Text = "Fortryd";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // labelRef_nr
            // 
            this.labelRef_nr.AutoSize = true;
            this.labelRef_nr.Location = new System.Drawing.Point(8, 17);
            this.labelRef_nr.Name = "labelRef_nr";
            this.labelRef_nr.Size = new System.Drawing.Size(36, 13);
            this.labelRef_nr.TabIndex = 8;
            this.labelRef_nr.Text = "Ref nr";
            // 
            // xmldocsBindingSource
            // 
            this.xmldocsBindingSource.DataSource = typeof(bjArkiv.xmldoc);
            // 
            // xmldocsBindingNavigator
            // 
            this.xmldocsBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.xmldocsBindingNavigator.BindingSource = this.xmldocsBindingSource;
            this.xmldocsBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.xmldocsBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.xmldocsBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xmldocsBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.xmldocsBindingNavigatorSaveItem});
            this.xmldocsBindingNavigator.Location = new System.Drawing.Point(0, 270);
            this.xmldocsBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.xmldocsBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.xmldocsBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.xmldocsBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.xmldocsBindingNavigator.Name = "xmldocsBindingNavigator";
            this.xmldocsBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.xmldocsBindingNavigator.Size = new System.Drawing.Size(408, 25);
            this.xmldocsBindingNavigator.TabIndex = 18;
            this.xmldocsBindingNavigator.Text = "bindingNavigator1";
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
            // xmldocsBindingNavigatorSaveItem
            // 
            this.xmldocsBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xmldocsBindingNavigatorSaveItem.Enabled = false;
            this.xmldocsBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("xmldocsBindingNavigatorSaveItem.Image")));
            this.xmldocsBindingNavigatorSaveItem.Name = "xmldocsBindingNavigatorSaveItem";
            this.xmldocsBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.xmldocsBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // txtBoxRef_nr
            // 
            this.txtBoxRef_nr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "ref_nr", true));
            this.txtBoxRef_nr.Location = new System.Drawing.Point(79, 10);
            this.txtBoxRef_nr.Name = "txtBoxRef_nr";
            this.txtBoxRef_nr.ReadOnly = true;
            this.txtBoxRef_nr.Size = new System.Drawing.Size(100, 20);
            this.txtBoxRef_nr.TabIndex = 20;
            // 
            // txtBoxDokument
            // 
            this.txtBoxDokument.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "kilde_sti", true));
            this.txtBoxDokument.Location = new System.Drawing.Point(79, 40);
            this.txtBoxDokument.Name = "txtBoxDokument";
            this.txtBoxDokument.ReadOnly = true;
            this.txtBoxDokument.Size = new System.Drawing.Size(278, 20);
            this.txtBoxDokument.TabIndex = 21;
            // 
            // txtBoxVirksomhed
            // 
            this.txtBoxVirksomhed.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "virksomhed", true));
            this.txtBoxVirksomhed.Location = new System.Drawing.Point(79, 68);
            this.txtBoxVirksomhed.Name = "txtBoxVirksomhed";
            this.txtBoxVirksomhed.Size = new System.Drawing.Size(278, 20);
            this.txtBoxVirksomhed.TabIndex = 22;
            // 
            // txtBoxEmne
            // 
            this.txtBoxEmne.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "emne", true));
            this.txtBoxEmne.Location = new System.Drawing.Point(79, 94);
            this.txtBoxEmne.Name = "txtBoxEmne";
            this.txtBoxEmne.Size = new System.Drawing.Size(278, 20);
            this.txtBoxEmne.TabIndex = 23;
            // 
            // txtBoxDokument_type
            // 
            this.txtBoxDokument_type.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "dokument_type", true));
            this.txtBoxDokument_type.Location = new System.Drawing.Point(79, 120);
            this.txtBoxDokument_type.Name = "txtBoxDokument_type";
            this.txtBoxDokument_type.Size = new System.Drawing.Size(278, 20);
            this.txtBoxDokument_type.TabIndex = 24;
            // 
            // txtBoxÅr
            // 
            this.txtBoxÅr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "år", true));
            this.txtBoxÅr.Location = new System.Drawing.Point(79, 146);
            this.txtBoxÅr.Name = "txtBoxÅr";
            this.txtBoxÅr.Size = new System.Drawing.Size(100, 20);
            this.txtBoxÅr.TabIndex = 25;
            // 
            // txtBoxEkstern_kilde
            // 
            this.txtBoxEkstern_kilde.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "ekstern_kilde", true));
            this.txtBoxEkstern_kilde.Location = new System.Drawing.Point(79, 172);
            this.txtBoxEkstern_kilde.Name = "txtBoxEkstern_kilde";
            this.txtBoxEkstern_kilde.Size = new System.Drawing.Size(278, 20);
            this.txtBoxEkstern_kilde.TabIndex = 26;
            // 
            // txtBoxBeskrivelse
            // 
            this.txtBoxBeskrivelse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.xmldocsBindingSource, "beskrivelse", true));
            this.txtBoxBeskrivelse.Location = new System.Drawing.Point(79, 198);
            this.txtBoxBeskrivelse.Name = "txtBoxBeskrivelse";
            this.txtBoxBeskrivelse.Size = new System.Drawing.Size(278, 20);
            this.txtBoxBeskrivelse.TabIndex = 27;
            // 
            // frmAddDoc2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 295);
            this.Controls.Add(this.txtBoxBeskrivelse);
            this.Controls.Add(this.txtBoxEkstern_kilde);
            this.Controls.Add(this.txtBoxÅr);
            this.Controls.Add(this.txtBoxDokument_type);
            this.Controls.Add(this.txtBoxEmne);
            this.Controls.Add(this.txtBoxVirksomhed);
            this.Controls.Add(this.txtBoxDokument);
            this.Controls.Add(this.txtBoxRef_nr);
            this.Controls.Add(this.xmldocsBindingNavigator);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labelEmne);
            this.Controls.Add(this.labelDokument_type);
            this.Controls.Add(this.labelEkstern_ref);
            this.Controls.Add(this.labelBeskrivelse);
            this.Controls.Add(this.labelRef_nr);
            this.Controls.Add(this.labelÅr);
            this.Controls.Add(this.labelDokument);
            this.Controls.Add(this.labelVirksomhed);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::bjArkiv.Properties.Settings.Default, "frmAddDocLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::bjArkiv.Properties.Settings.Default.frmAddDocLocation;
            this.Name = "frmAddDoc2";
            this.Text = "Indsæt dokument";
            this.Load += new System.EventHandler(this.frmAddDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xmldocsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmldocsBindingNavigator)).EndInit();
            this.xmldocsBindingNavigator.ResumeLayout(false);
            this.xmldocsBindingNavigator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelVirksomhed;
        private System.Windows.Forms.Label labelÅr;
        private System.Windows.Forms.Label labelBeskrivelse;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Label labelDokument;
        private System.Windows.Forms.Label labelEmne;
        private System.Windows.Forms.Label labelDokument_type;
        private System.Windows.Forms.Label labelEkstern_ref;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Label labelRef_nr;
        private System.Windows.Forms.BindingSource xmldocsBindingSource;
        private System.Windows.Forms.BindingNavigator xmldocsBindingNavigator;
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
        private System.Windows.Forms.ToolStripButton xmldocsBindingNavigatorSaveItem;
        private System.Windows.Forms.TextBox txtBoxRef_nr;
        private System.Windows.Forms.TextBox txtBoxDokument;
        private System.Windows.Forms.TextBox txtBoxVirksomhed;
        private System.Windows.Forms.TextBox txtBoxEmne;
        private System.Windows.Forms.TextBox txtBoxDokument_type;
        private System.Windows.Forms.TextBox txtBoxÅr;
        private System.Windows.Forms.TextBox txtBoxEkstern_kilde;
        private System.Windows.Forms.TextBox txtBoxBeskrivelse;
    }
}