namespace Medlem3060uc
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
            this.DatoKontingentTil = new System.Windows.Forms.DateTimePicker();
            this.DatoKontingentForfald = new System.Windows.Forms.DateTimePicker();
            this.DatoBetaltKontingentTil = new System.Windows.Forms.DateTimePicker();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdFakturer = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pgmForslag = new System.Windows.Forms.ProgressBar();
            this.lvwMedlem = new System.Windows.Forms.ListView();
            this.columnHeaderMNavn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMAdresse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMPostnr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMFradato = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMKontingent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMTildato = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMIndmeldelse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderMTilmeldtpbs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Label_Forslagstekst = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdForslag = new System.Windows.Forms.Button();
            this.DelsystemBSH = new System.Windows.Forms.CheckBox();
            this.pgmFaktura = new System.Windows.Forms.ProgressBar();
            this.Label_Fakturatekst = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwKontingent = new System.Windows.Forms.ListView();
            this.columnHeaderKNnavn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKNR = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKAdresse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKPostnr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKFradato = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKKontingent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKTildato = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKIndmeldelse = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderKTilmeldtpbs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderNr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(23, 68);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(61, 25);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Fortryd";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdFakturer
            // 
            this.cmdFakturer.Location = new System.Drawing.Point(18, 68);
            this.cmdFakturer.Name = "cmdFakturer";
            this.cmdFakturer.Size = new System.Drawing.Size(61, 25);
            this.cmdFakturer.TabIndex = 3;
            this.cmdFakturer.Text = "Fakturer";
            this.cmdFakturer.UseVisualStyleBackColor = true;
            this.cmdFakturer.Visible = false;
            this.cmdFakturer.Click += new System.EventHandler(this.cmdRSMembership_Fakturer_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pgmForslag);
            this.splitContainer1.Panel1.Controls.Add(this.lvwMedlem);
            this.splitContainer1.Panel1.Controls.Add(this.Label_Forslagstekst);
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
            this.splitContainer1.Panel2.Controls.Add(this.DelsystemBSH);
            this.splitContainer1.Panel2.Controls.Add(this.pgmFaktura);
            this.splitContainer1.Panel2.Controls.Add(this.Label_Fakturatekst);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.lvwKontingent);
            this.splitContainer1.Panel2.Controls.Add(this.label_DatoBetaltKontingentTil);
            this.splitContainer1.Panel2.Controls.Add(this.cmdFakturer);
            this.splitContainer1.Panel2.Controls.Add(this.DatoBetaltKontingentTil);
            this.splitContainer1.Size = new System.Drawing.Size(1356, 513);
            this.splitContainer1.SplitterDistance = 669;
            this.splitContainer1.TabIndex = 4;
            // 
            // pgmForslag
            // 
            this.pgmForslag.Location = new System.Drawing.Point(165, 74);
            this.pgmForslag.Maximum = 325;
            this.pgmForslag.Name = "pgmForslag";
            this.pgmForslag.Size = new System.Drawing.Size(218, 15);
            this.pgmForslag.Step = 1;
            this.pgmForslag.TabIndex = 6;
            this.pgmForslag.Visible = false;
            // 
            // lvwMedlem
            // 
            this.lvwMedlem.AllowDrop = true;
            this.lvwMedlem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMNavn,
            this.columnHeaderMNr,
            this.columnHeaderMAdresse,
            this.columnHeaderMPostnr,
            this.columnHeaderMFradato,
            this.columnHeaderMKontingent,
            this.columnHeaderMTildato,
            this.columnHeaderMIndmeldelse,
            this.columnHeaderMTilmeldtpbs});
            this.lvwMedlem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwMedlem.FullRowSelect = true;
            this.lvwMedlem.Location = new System.Drawing.Point(0, 137);
            this.lvwMedlem.Name = "lvwMedlem";
            this.lvwMedlem.Size = new System.Drawing.Size(669, 376);
            this.lvwMedlem.TabIndex = 0;
            this.lvwMedlem.UseCompatibleStateImageBehavior = false;
            this.lvwMedlem.View = System.Windows.Forms.View.Details;
            this.lvwMedlem.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwMedlem_ColumnClick);
            this.lvwMedlem.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwMedlem_ItemDrag);
            this.lvwMedlem.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwMedlem_DragDrop);
            this.lvwMedlem.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwMedlem_DragEnter);
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
            // columnHeaderMFradato
            // 
            this.columnHeaderMFradato.Text = "Fra dato";
            // 
            // columnHeaderMKontingent
            // 
            this.columnHeaderMKontingent.Text = "Kontingent";
            // 
            // columnHeaderMTildato
            // 
            this.columnHeaderMTildato.Text = "Til dato";
            // 
            // columnHeaderMIndmeldelse
            // 
            this.columnHeaderMIndmeldelse.Text = "Indm";
            // 
            // columnHeaderMTilmeldtpbs
            // 
            this.columnHeaderMTilmeldtpbs.Text = "Pbs";
            // 
            // Label_Forslagstekst
            // 
            this.Label_Forslagstekst.AutoSize = true;
            this.Label_Forslagstekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Forslagstekst.ForeColor = System.Drawing.Color.Red;
            this.Label_Forslagstekst.Location = new System.Drawing.Point(20, 96);
            this.Label_Forslagstekst.Name = "Label_Forslagstekst";
            this.Label_Forslagstekst.Size = new System.Drawing.Size(33, 16);
            this.Label_Forslagstekst.TabIndex = 5;
            this.Label_Forslagstekst.Text = "test";
            this.Label_Forslagstekst.Visible = false;
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
            this.cmdForslag.Click += new System.EventHandler(this.cmdForslag_Click);
            // 
            // DelsystemBSH
            // 
            this.DelsystemBSH.AutoSize = true;
            this.DelsystemBSH.Location = new System.Drawing.Point(85, 73);
            this.DelsystemBSH.Name = "DelsystemBSH";
            this.DelsystemBSH.Size = new System.Drawing.Size(48, 17);
            this.DelsystemBSH.TabIndex = 7;
            this.DelsystemBSH.Text = "BSH";
            this.DelsystemBSH.UseVisualStyleBackColor = true;
            this.DelsystemBSH.Visible = false;
            this.DelsystemBSH.CheckStateChanged += new System.EventHandler(this.DelsystemBSH_CheckStateChanged);
            // 
            // pgmFaktura
            // 
            this.pgmFaktura.Location = new System.Drawing.Point(139, 74);
            this.pgmFaktura.Maximum = 325;
            this.pgmFaktura.Name = "pgmFaktura";
            this.pgmFaktura.Size = new System.Drawing.Size(218, 15);
            this.pgmFaktura.Step = 1;
            this.pgmFaktura.TabIndex = 6;
            this.pgmFaktura.Visible = false;
            // 
            // Label_Fakturatekst
            // 
            this.Label_Fakturatekst.AutoSize = true;
            this.Label_Fakturatekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Fakturatekst.ForeColor = System.Drawing.Color.Red;
            this.Label_Fakturatekst.Location = new System.Drawing.Point(15, 96);
            this.Label_Fakturatekst.Name = "Label_Fakturatekst";
            this.Label_Fakturatekst.Size = new System.Drawing.Size(33, 16);
            this.Label_Fakturatekst.TabIndex = 5;
            this.Label_Fakturatekst.Text = "test";
            this.Label_Fakturatekst.Visible = false;
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
            this.columnHeaderKFradato,
            this.columnHeaderKKontingent,
            this.columnHeaderKTildato,
            this.columnHeaderKIndmeldelse,
            this.columnHeaderKTilmeldtpbs,
            this.columnHeaderSid,
            this.columnHeaderNr});
            this.lvwKontingent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwKontingent.FullRowSelect = true;
            this.lvwKontingent.Location = new System.Drawing.Point(0, 137);
            this.lvwKontingent.Name = "lvwKontingent";
            this.lvwKontingent.Size = new System.Drawing.Size(683, 376);
            this.lvwKontingent.TabIndex = 0;
            this.lvwKontingent.UseCompatibleStateImageBehavior = false;
            this.lvwKontingent.View = System.Windows.Forms.View.Details;
            this.lvwKontingent.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwKontingent_ColumnClick);
            this.lvwKontingent.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwKontingent_ItemDrag);
            this.lvwKontingent.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwKontingent_DragDrop);
            this.lvwKontingent.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwKontingent_DragEnter);
            // 
            // columnHeaderKNnavn
            // 
            this.columnHeaderKNnavn.Text = "Navn";
            // 
            // columnHeaderKNR
            // 
            this.columnHeaderKNR.Text = "Uid";
            // 
            // columnHeaderKAdresse
            // 
            this.columnHeaderKAdresse.Text = "Adresse";
            // 
            // columnHeaderKPostnr
            // 
            this.columnHeaderKPostnr.Text = "Postnr";
            // 
            // columnHeaderKFradato
            // 
            this.columnHeaderKFradato.Text = "Fra dato";
            // 
            // columnHeaderKKontingent
            // 
            this.columnHeaderKKontingent.Text = "Kontingent";
            // 
            // columnHeaderKTildato
            // 
            this.columnHeaderKTildato.Text = "Til dato";
            // 
            // columnHeaderKIndmeldelse
            // 
            this.columnHeaderKIndmeldelse.Text = "Indm";
            // 
            // columnHeaderKTilmeldtpbs
            // 
            this.columnHeaderKTilmeldtpbs.Text = "Pbs";
            // 
            // columnHeaderSid
            // 
            this.columnHeaderSid.Text = "Sid";
            // 
            // columnHeaderNr
            // 
            this.columnHeaderNr.Text = "Nr";
            // 
            // FrmKontingentForslag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::Medlem3060uc.Properties.Settings.Default.frmKontingentForslagSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::Medlem3060uc.Properties.Settings.Default, "frmKontingentForslagSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Medlem3060uc.Properties.Settings.Default, "frmKontingentForslagPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::Medlem3060uc.Properties.Settings.Default.frmKontingentForslagPoint;
            this.Name = "FrmKontingentForslag";
            this.Text = "Kontingent Forslag";
            this.Load += new System.EventHandler(this.FrmKontingentForslag_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_DatoKontingentTil;
        private System.Windows.Forms.Label label_DatoKontingentForfald;
        private System.Windows.Forms.Label label_DatoBetaltKontingentTil;
        private System.Windows.Forms.DateTimePicker DatoKontingentTil;
        private System.Windows.Forms.DateTimePicker DatoKontingentForfald;
        private System.Windows.Forms.DateTimePicker DatoBetaltKontingentTil;
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
        private System.Windows.Forms.ColumnHeader columnHeaderMFradato;
        private System.Windows.Forms.ColumnHeader columnHeaderKNnavn;
        private System.Windows.Forms.ColumnHeader columnHeaderKNR;
        private System.Windows.Forms.ColumnHeader columnHeaderKAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderKPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderKFradato;
        private System.Windows.Forms.ProgressBar pgmForslag;
        private System.Windows.Forms.Label Label_Forslagstekst;
        private System.Windows.Forms.Label Label_Fakturatekst;
        private System.Windows.Forms.ProgressBar pgmFaktura;
        private System.Windows.Forms.ColumnHeader columnHeaderMKontingent;
        private System.Windows.Forms.ColumnHeader columnHeaderKKontingent;
        private System.Windows.Forms.ColumnHeader columnHeaderMTildato;
        private System.Windows.Forms.ColumnHeader columnHeaderKTildato;
        private System.Windows.Forms.CheckBox DelsystemBSH;
        private System.Windows.Forms.ColumnHeader columnHeaderMIndmeldelse;
        private System.Windows.Forms.ColumnHeader columnHeaderMTilmeldtpbs;
        private System.Windows.Forms.ColumnHeader columnHeaderKIndmeldelse;
        private System.Windows.Forms.ColumnHeader columnHeaderKTilmeldtpbs;
        private System.Windows.Forms.ColumnHeader columnHeaderSid;
        private System.Windows.Forms.ColumnHeader columnHeaderNr;
    }
}