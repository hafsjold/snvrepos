namespace nsPuls3060
{
    partial class FrmKontingentForslag
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
            this.label_DatoKontingentTil = new System.Windows.Forms.Label();
            this.label_DatoKontingentForfald = new System.Windows.Forms.Label();
            this.label_DatoBetaltKontingentTil = new System.Windows.Forms.Label();
            this.label_Aarskontingent = new System.Windows.Forms.Label();
            this.DatoKontingentTil = new System.Windows.Forms.DateTimePicker();
            this.DatoKontingentForfald = new System.Windows.Forms.DateTimePicker();
            this.DatoBetaltKontingentTil = new System.Windows.Forms.DateTimePicker();
            this.Aarskontingent = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdFakturer = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvwMedlem = new System.Windows.Forms.ListView();
            this.columnHeaderMNavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMNr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMBynavn = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdForslag = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwKontingent = new System.Windows.Forms.ListView();
            this.columnHeaderKNnavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKNR = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKBynavn = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_DatoKontingentTil
            // 
            this.label_DatoKontingentTil.AutoSize = true;
            this.label_DatoKontingentTil.Location = new System.Drawing.Point(20, 10);
            this.label_DatoKontingentTil.Name = "label_DatoKontingentTil";
            this.label_DatoKontingentTil.Size = new System.Drawing.Size(95, 13);
            this.label_DatoKontingentTil.TabIndex = 0;
            this.label_DatoKontingentTil.Text = "Kontingent til dato:";
            // 
            // label_DatoKontingentForfald
            // 
            this.label_DatoKontingentForfald.AutoSize = true;
            this.label_DatoKontingentForfald.Location = new System.Drawing.Point(20, 40);
            this.label_DatoKontingentForfald.Name = "label_DatoKontingentForfald";
            this.label_DatoKontingentForfald.Size = new System.Drawing.Size(122, 13);
            this.label_DatoKontingentForfald.TabIndex = 0;
            this.label_DatoKontingentForfald.Text = "Kontingent forfalds dato:";
            // 
            // label_DatoBetaltKontingentTil
            // 
            this.label_DatoBetaltKontingentTil.AutoSize = true;
            this.label_DatoBetaltKontingentTil.Location = new System.Drawing.Point(15, 9);
            this.label_DatoBetaltKontingentTil.Name = "label_DatoBetaltKontingentTil";
            this.label_DatoBetaltKontingentTil.Size = new System.Drawing.Size(213, 13);
            this.label_DatoBetaltKontingentTil.TabIndex = 0;
            this.label_DatoBetaltKontingentTil.Text = "Medlemmer som har betalt kontingent indtil::";
            // 
            // label_Aarskontingent
            // 
            this.label_Aarskontingent.AutoSize = true;
            this.label_Aarskontingent.Location = new System.Drawing.Point(15, 39);
            this.label_Aarskontingent.Name = "label_Aarskontingent";
            this.label_Aarskontingent.Size = new System.Drawing.Size(135, 13);
            this.label_Aarskontingent.TabIndex = 0;
            this.label_Aarskontingent.Text = "Kontingent for 12 måneder:";
            // 
            // DatoKontingentTil
            // 
            this.DatoKontingentTil.Location = new System.Drawing.Point(195, 10);
            this.DatoKontingentTil.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.DatoKontingentTil.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DatoKontingentTil.Name = "DatoKontingentTil";
            this.DatoKontingentTil.Size = new System.Drawing.Size(121, 20);
            this.DatoKontingentTil.TabIndex = 1;
            // 
            // DatoKontingentForfald
            // 
            this.DatoKontingentForfald.Location = new System.Drawing.Point(195, 36);
            this.DatoKontingentForfald.Name = "DatoKontingentForfald";
            this.DatoKontingentForfald.Size = new System.Drawing.Size(121, 20);
            this.DatoKontingentForfald.TabIndex = 1;
            // 
            // DatoBetaltKontingentTil
            // 
            this.DatoBetaltKontingentTil.Location = new System.Drawing.Point(248, 9);
            this.DatoBetaltKontingentTil.Name = "DatoBetaltKontingentTil";
            this.DatoBetaltKontingentTil.Size = new System.Drawing.Size(124, 20);
            this.DatoBetaltKontingentTil.TabIndex = 1;
            // 
            // Aarskontingent
            // 
            this.Aarskontingent.Location = new System.Drawing.Point(248, 36);
            this.Aarskontingent.Name = "Aarskontingent";
            this.Aarskontingent.Size = new System.Drawing.Size(47, 20);
            this.Aarskontingent.TabIndex = 2;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(23, 68);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(61, 25);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Fortryd";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdFakturer
            // 
            this.cmdFakturer.Location = new System.Drawing.Point(18, 68);
            this.cmdFakturer.Name = "cmdFakturer";
            this.cmdFakturer.Size = new System.Drawing.Size(61, 25);
            this.cmdFakturer.TabIndex = 3;
            this.cmdFakturer.Text = "Fakturer";
            this.cmdFakturer.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvwMedlem);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label_DatoKontingentTil);
            this.splitContainer1.Panel1.Controls.Add(this.label_DatoKontingentForfald);
            this.splitContainer1.Panel1.Controls.Add(this.DatoKontingentTil);
            this.splitContainer1.Panel1.Controls.Add(this.cmdForslag);
            this.splitContainer1.Panel1.Controls.Add(this.DatoKontingentForfald);
            this.splitContainer1.Panel1.Controls.Add(this.cmdCancel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.lvwKontingent);
            this.splitContainer1.Panel2.Controls.Add(this.label_DatoBetaltKontingentTil);
            this.splitContainer1.Panel2.Controls.Add(this.cmdFakturer);
            this.splitContainer1.Panel2.Controls.Add(this.label_Aarskontingent);
            this.splitContainer1.Panel2.Controls.Add(this.Aarskontingent);
            this.splitContainer1.Panel2.Controls.Add(this.DatoBetaltKontingentTil);
            this.splitContainer1.Size = new System.Drawing.Size(829, 513);
            this.splitContainer1.SplitterDistance = 409;
            this.splitContainer1.TabIndex = 4;
            // 
            // lvwMedlem
            // 
            this.lvwMedlem.AllowDrop = true;
            this.lvwMedlem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMNavn,
            this.columnHeaderMNr,
            this.columnHeaderMAdresse,
            this.columnHeaderMPostnr,
            this.columnHeaderMBynavn});
            this.lvwMedlem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwMedlem.Location = new System.Drawing.Point(0, 137);
            this.lvwMedlem.Name = "lvwMedlem";
            this.lvwMedlem.Size = new System.Drawing.Size(409, 376);
            this.lvwMedlem.TabIndex = 0;
            this.lvwMedlem.UseCompatibleStateImageBehavior = false;
            this.lvwMedlem.View = System.Windows.Forms.View.Details;
            this.lvwMedlem.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwMedlem_DragDrop);
            this.lvwMedlem.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwMedlem_ColumnClick);
            this.lvwMedlem.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwMedlem_DragEnter);
            this.lvwMedlem.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwMedlem_ItemDrag);
            // 
            // columnHeaderMNavn
            // 
            this.columnHeaderMNavn.Text = "Navn";
            // 
            // columnHeaderMNr
            // 
            this.columnHeaderMNr.Text = "Nr";
            // 
            // columnHeaderMAdresse
            // 
            this.columnHeaderMAdresse.Text = "Adresse";
            // 
            // columnHeaderMPostnr
            // 
            this.columnHeaderMPostnr.Text = "Postnr";
            // 
            // columnHeaderMBynavn
            // 
            this.columnHeaderMBynavn.Text = "By";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vælg til Betaling";
            // 
            // cmdForslag
            // 
            this.cmdForslag.Location = new System.Drawing.Point(90, 68);
            this.cmdForslag.Name = "cmdForslag";
            this.cmdForslag.Size = new System.Drawing.Size(61, 25);
            this.cmdForslag.TabIndex = 3;
            this.cmdForslag.Text = "Forslag";
            this.cmdForslag.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Kontingent Betaling";
            // 
            // lvwKontingent
            // 
            this.lvwKontingent.AllowDrop = true;
            this.lvwKontingent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderKNnavn,
            this.columnHeaderKNR,
            this.columnHeaderKAdresse,
            this.columnHeaderKPostnr,
            this.columnHeaderKBynavn});
            this.lvwKontingent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwKontingent.Location = new System.Drawing.Point(0, 137);
            this.lvwKontingent.Name = "lvwKontingent";
            this.lvwKontingent.Size = new System.Drawing.Size(416, 376);
            this.lvwKontingent.TabIndex = 0;
            this.lvwKontingent.UseCompatibleStateImageBehavior = false;
            this.lvwKontingent.View = System.Windows.Forms.View.Details;
            this.lvwKontingent.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwKontingent_DragDrop);
            this.lvwKontingent.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwKontingent_ColumnClick);
            this.lvwKontingent.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwKontingent_DragEnter);
            this.lvwKontingent.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwKontingent_ItemDrag);
            // 
            // columnHeaderKNnavn
            // 
            this.columnHeaderKNnavn.Text = "Navn";
            // 
            // columnHeaderKNR
            // 
            this.columnHeaderKNR.Text = "Nr";
            // 
            // columnHeaderKAdresse
            // 
            this.columnHeaderKAdresse.Text = "Adresse";
            // 
            // columnHeaderKPostnr
            // 
            this.columnHeaderKPostnr.Text = "Postnr";
            // 
            // columnHeaderKBynavn
            // 
            this.columnHeaderKBynavn.Text = "By";
            // 
            // FrmKontingentForslag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmKontingentForslagSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmKontingentForslagSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmKontingentForslagPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmKontingentForslagPoint;
            this.Name = "FrmKontingentForslag";
            this.Text = "Kontingent Forslag";
            this.Load += new System.EventHandler(this.FrmKontingentForslag_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_DatoKontingentTil;
        private System.Windows.Forms.Label label_DatoKontingentForfald;
        private System.Windows.Forms.Label label_DatoBetaltKontingentTil;
        private System.Windows.Forms.Label label_Aarskontingent;
        private System.Windows.Forms.DateTimePicker DatoKontingentTil;
        private System.Windows.Forms.DateTimePicker DatoKontingentForfald;
        private System.Windows.Forms.DateTimePicker DatoBetaltKontingentTil;
        private System.Windows.Forms.TextBox Aarskontingent;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdFakturer;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwMedlem;
        private System.Windows.Forms.ListView lvwKontingent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdForslag;
        private System.Windows.Forms.ColumnHeader columnHeaderMNr;
        private System.Windows.Forms.ColumnHeader columnHeaderMNavn;
        private System.Windows.Forms.ColumnHeader columnHeaderMAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderMPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderMBynavn;
        private System.Windows.Forms.ColumnHeader columnHeaderKNnavn;
        private System.Windows.Forms.ColumnHeader columnHeaderKNR;
        private System.Windows.Forms.ColumnHeader columnHeaderKAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderKPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderKBynavn;
    }
}