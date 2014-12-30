namespace nsPuls3060v2
{
    partial class FrmRykkerForslag
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdRykkere = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.RykketTidligere = new System.Windows.Forms.CheckBox();
            this.pgmForslag = new System.Windows.Forms.ProgressBar();
            this.lvwMedlem = new System.Windows.Forms.ListView();
            this.columnHeaderMNavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMNr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMKontingent = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMFaknr = new System.Windows.Forms.ColumnHeader();
            this.Label_Forslagstekst = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdForslag = new System.Windows.Forms.Button();
            this.DelsystemBSH = new System.Windows.Forms.CheckBox();
            this.pgmRykker = new System.Windows.Forms.ProgressBar();
            this.Label_Rykkertekst = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwRykker = new System.Windows.Forms.ListView();
            this.columnHeaderKNnavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKNR = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKKontingent = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKFaknr = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            // cmdRykkere
            // 
            this.cmdRykkere.Location = new System.Drawing.Point(18, 68);
            this.cmdRykkere.Name = "cmdRykkere";
            this.cmdRykkere.Size = new System.Drawing.Size(61, 25);
            this.cmdRykkere.TabIndex = 3;
            this.cmdRykkere.Text = "Rykkere";
            this.cmdRykkere.UseVisualStyleBackColor = true;
            this.cmdRykkere.Visible = false;
            this.cmdRykkere.Click += new System.EventHandler(this.cmdRykkere_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.RykketTidligere);
            this.splitContainer1.Panel1.Controls.Add(this.pgmForslag);
            this.splitContainer1.Panel1.Controls.Add(this.lvwMedlem);
            this.splitContainer1.Panel1.Controls.Add(this.Label_Forslagstekst);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cmdForslag);
            this.splitContainer1.Panel1.Controls.Add(this.cmdCancel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DelsystemBSH);
            this.splitContainer1.Panel2.Controls.Add(this.pgmRykker);
            this.splitContainer1.Panel2.Controls.Add(this.Label_Rykkertekst);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.lvwRykker);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRykkere);
            this.splitContainer1.Size = new System.Drawing.Size(829, 513);
            this.splitContainer1.SplitterDistance = 409;
            this.splitContainer1.TabIndex = 4;
            // 
            // RykketTidligere
            // 
            this.RykketTidligere.AutoSize = true;
            this.RykketTidligere.Location = new System.Drawing.Point(23, 12);
            this.RykketTidligere.Name = "RykketTidligere";
            this.RykketTidligere.Size = new System.Drawing.Size(99, 17);
            this.RykketTidligere.TabIndex = 8;
            this.RykketTidligere.Text = "Rykket tidligere";
            this.RykketTidligere.UseVisualStyleBackColor = true;
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
            this.columnHeaderMDato,
            this.columnHeaderMKontingent,
            this.columnHeaderMFaknr});
            this.lvwMedlem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwMedlem.FullRowSelect = true;
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
            // columnHeaderMDato
            // 
            this.columnHeaderMDato.Text = "Dato";
            // 
            // columnHeaderMKontingent
            // 
            this.columnHeaderMKontingent.Text = "Kontingent";
            // 
            // columnHeaderMFaknr
            // 
            this.columnHeaderMFaknr.Text = "Faknr";
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
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vælg til Rykning";
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
            this.DelsystemBSH.Location = new System.Drawing.Point(85, 74);
            this.DelsystemBSH.Name = "DelsystemBSH";
            this.DelsystemBSH.Size = new System.Drawing.Size(48, 17);
            this.DelsystemBSH.TabIndex = 8;
            this.DelsystemBSH.Text = "BSH";
            this.DelsystemBSH.UseVisualStyleBackColor = true;
            this.DelsystemBSH.Visible = false;
            // 
            // pgmRykker
            // 
            this.pgmRykker.Location = new System.Drawing.Point(139, 74);
            this.pgmRykker.Maximum = 325;
            this.pgmRykker.Name = "pgmRykker";
            this.pgmRykker.Size = new System.Drawing.Size(218, 15);
            this.pgmRykker.Step = 1;
            this.pgmRykker.TabIndex = 6;
            this.pgmRykker.Visible = false;
            // 
            // Label_Rykkertekst
            // 
            this.Label_Rykkertekst.AutoSize = true;
            this.Label_Rykkertekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Rykkertekst.ForeColor = System.Drawing.Color.Red;
            this.Label_Rykkertekst.Location = new System.Drawing.Point(15, 96);
            this.Label_Rykkertekst.Name = "Label_Rykkertekst";
            this.Label_Rykkertekst.Size = new System.Drawing.Size(33, 16);
            this.Label_Rykkertekst.TabIndex = 5;
            this.Label_Rykkertekst.Text = "test";
            this.Label_Rykkertekst.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rykker E-mail";
            // 
            // lvwRykker
            // 
            this.lvwRykker.AllowDrop = true;
            this.lvwRykker.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderKNnavn,
            this.columnHeaderKNR,
            this.columnHeaderKAdresse,
            this.columnHeaderKPostnr,
            this.columnHeaderKDato,
            this.columnHeaderKKontingent,
            this.columnHeaderKFaknr});
            this.lvwRykker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwRykker.FullRowSelect = true;
            this.lvwRykker.Location = new System.Drawing.Point(0, 137);
            this.lvwRykker.Name = "lvwRykker";
            this.lvwRykker.Size = new System.Drawing.Size(416, 376);
            this.lvwRykker.TabIndex = 0;
            this.lvwRykker.UseCompatibleStateImageBehavior = false;
            this.lvwRykker.View = System.Windows.Forms.View.Details;
            this.lvwRykker.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwRykker_DragDrop);
            this.lvwRykker.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwRykker_ColumnClick);
            this.lvwRykker.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwRykker_DragEnter);
            this.lvwRykker.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwRykker_ItemDrag);
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
            // columnHeaderKDato
            // 
            this.columnHeaderKDato.Text = "Dato";
            // 
            // columnHeaderKKontingent
            // 
            this.columnHeaderKKontingent.Text = "Kontingent";
            // 
            // columnHeaderKFaknr
            // 
            this.columnHeaderKFaknr.Text = "Faknr";
            // 
            // FrmRykkerForslag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060v2.Properties.Settings.Default.frmRykkerForslagClientSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060v2.Properties.Settings.Default, "frmRykkerForslagClientSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060v2.Properties.Settings.Default, "frmRykkerForslagPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060v2.Properties.Settings.Default.frmRykkerForslagPoint;
            this.Name = "FrmRykkerForslag";
            this.Text = "Rykker Forslag";
            this.Load += new System.EventHandler(this.FrmRykkerForslag_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdRykkere;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwMedlem;
        private System.Windows.Forms.ListView lvwRykker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdForslag;
        private System.Windows.Forms.ColumnHeader columnHeaderMNr;
        private System.Windows.Forms.ColumnHeader columnHeaderMNavn;
        private System.Windows.Forms.ColumnHeader columnHeaderMAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderMPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderMDato;
        private System.Windows.Forms.ColumnHeader columnHeaderKNnavn;
        private System.Windows.Forms.ColumnHeader columnHeaderKNR;
        private System.Windows.Forms.ColumnHeader columnHeaderKAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderKPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderKDato;
        private System.Windows.Forms.ProgressBar pgmForslag;
        private System.Windows.Forms.Label Label_Forslagstekst;
        private System.Windows.Forms.Label Label_Rykkertekst;
        private System.Windows.Forms.ProgressBar pgmRykker;
        private System.Windows.Forms.ColumnHeader columnHeaderMKontingent;
        private System.Windows.Forms.ColumnHeader columnHeaderKKontingent;
        private System.Windows.Forms.ColumnHeader columnHeaderMFaknr;
        private System.Windows.Forms.ColumnHeader columnHeaderKFaknr;
        private System.Windows.Forms.CheckBox DelsystemBSH;
        private System.Windows.Forms.CheckBox RykketTidligere;
    }
}