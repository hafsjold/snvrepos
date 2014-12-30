namespace nsPuls3060v2
{
    partial class FrmBetalingsForslag
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
            this.cmdBetal = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pgmForslag = new System.Windows.Forms.ProgressBar();
            this.lvwKreditor = new System.Windows.Forms.ListView();
            this.columnHeaderMNavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMNr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMFaknr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMBelob = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMBank = new System.Windows.Forms.ColumnHeader();
            this.Label_Forslagstekst = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdForslag = new System.Windows.Forms.Button();
            this.pgmBetal = new System.Windows.Forms.ProgressBar();
            this.Label_Betaltekst = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwKrdFaktura = new System.Windows.Forms.ListView();
            this.columnHeaderKNnavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKNR = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKFaknr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKBelob = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKBank = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(23, 38);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(61, 25);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Fortryd";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdBetal
            // 
            this.cmdBetal.Location = new System.Drawing.Point(18, 38);
            this.cmdBetal.Name = "cmdBetal";
            this.cmdBetal.Size = new System.Drawing.Size(61, 25);
            this.cmdBetal.TabIndex = 3;
            this.cmdBetal.Text = "Betal";
            this.cmdBetal.UseVisualStyleBackColor = true;
            this.cmdBetal.Visible = false;
            this.cmdBetal.Click += new System.EventHandler(this.cmdBetal_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.lvwKreditor);
            this.splitContainer1.Panel1.Controls.Add(this.Label_Forslagstekst);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cmdForslag);
            this.splitContainer1.Panel1.Controls.Add(this.cmdCancel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pgmBetal);
            this.splitContainer1.Panel2.Controls.Add(this.Label_Betaltekst);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.lvwKrdFaktura);
            this.splitContainer1.Panel2.Controls.Add(this.cmdBetal);
            this.splitContainer1.Size = new System.Drawing.Size(879, 490);
            this.splitContainer1.SplitterDistance = 430;
            this.splitContainer1.TabIndex = 4;
            // 
            // pgmForslag
            // 
            this.pgmForslag.Location = new System.Drawing.Point(165, 44);
            this.pgmForslag.Maximum = 325;
            this.pgmForslag.Name = "pgmForslag";
            this.pgmForslag.Size = new System.Drawing.Size(218, 15);
            this.pgmForslag.Step = 1;
            this.pgmForslag.TabIndex = 6;
            this.pgmForslag.Visible = false;
            // 
            // lvwKreditor
            // 
            this.lvwKreditor.AllowDrop = true;
            this.lvwKreditor.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMNavn,
            this.columnHeaderMNr,
            this.columnHeaderMAdresse,
            this.columnHeaderMPostnr,
            this.columnHeaderMFaknr,
            this.columnHeaderMBelob,
            this.columnHeaderMBank});
            this.lvwKreditor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwKreditor.FullRowSelect = true;
            this.lvwKreditor.Location = new System.Drawing.Point(0, 114);
            this.lvwKreditor.Name = "lvwKreditor";
            this.lvwKreditor.Size = new System.Drawing.Size(430, 376);
            this.lvwKreditor.TabIndex = 0;
            this.lvwKreditor.UseCompatibleStateImageBehavior = false;
            this.lvwKreditor.View = System.Windows.Forms.View.Details;
            this.lvwKreditor.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwKreditor_DragDrop);
            this.lvwKreditor.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwKreditor_ColumnClick);
            this.lvwKreditor.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwKreditor_DragEnter);
            this.lvwKreditor.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwKreditor_ItemDrag);
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
            // columnHeaderMFaknr
            // 
            this.columnHeaderMFaknr.Text = "Faknr";
            // 
            // columnHeaderMBelob
            // 
            this.columnHeaderMBelob.Text = "Beløb";
            // 
            // columnHeaderMBank
            // 
            this.columnHeaderMBank.Text = "Bank";
            // 
            // Label_Forslagstekst
            // 
            this.Label_Forslagstekst.AutoSize = true;
            this.Label_Forslagstekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Forslagstekst.ForeColor = System.Drawing.Color.Red;
            this.Label_Forslagstekst.Location = new System.Drawing.Point(20, 66);
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
            this.label1.Location = new System.Drawing.Point(20, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vælg til Betaling";
            // 
            // cmdForslag
            // 
            this.cmdForslag.Location = new System.Drawing.Point(90, 38);
            this.cmdForslag.Name = "cmdForslag";
            this.cmdForslag.Size = new System.Drawing.Size(61, 25);
            this.cmdForslag.TabIndex = 3;
            this.cmdForslag.Text = "Forslag";
            this.cmdForslag.UseVisualStyleBackColor = true;
            this.cmdForslag.Click += new System.EventHandler(this.cmdForslag_Click);
            // 
            // pgmBetal
            // 
            this.pgmBetal.Location = new System.Drawing.Point(96, 44);
            this.pgmBetal.Maximum = 325;
            this.pgmBetal.Name = "pgmBetal";
            this.pgmBetal.Size = new System.Drawing.Size(218, 15);
            this.pgmBetal.Step = 1;
            this.pgmBetal.TabIndex = 6;
            this.pgmBetal.Visible = false;
            // 
            // Label_Betaltekst
            // 
            this.Label_Betaltekst.AutoSize = true;
            this.Label_Betaltekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Betaltekst.ForeColor = System.Drawing.Color.Red;
            this.Label_Betaltekst.Location = new System.Drawing.Point(15, 66);
            this.Label_Betaltekst.Name = "Label_Betaltekst";
            this.Label_Betaltekst.Size = new System.Drawing.Size(33, 16);
            this.Label_Betaltekst.TabIndex = 5;
            this.Label_Betaltekst.Text = "test";
            this.Label_Betaltekst.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Betal";
            // 
            // lvwKrdFaktura
            // 
            this.lvwKrdFaktura.AllowDrop = true;
            this.lvwKrdFaktura.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderKNnavn,
            this.columnHeaderKNR,
            this.columnHeaderKAdresse,
            this.columnHeaderKPostnr,
            this.columnHeaderKFaknr,
            this.columnHeaderKBelob,
            this.columnHeaderKBank});
            this.lvwKrdFaktura.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvwKrdFaktura.FullRowSelect = true;
            this.lvwKrdFaktura.Location = new System.Drawing.Point(0, 114);
            this.lvwKrdFaktura.Name = "lvwKrdFaktura";
            this.lvwKrdFaktura.Size = new System.Drawing.Size(445, 376);
            this.lvwKrdFaktura.TabIndex = 0;
            this.lvwKrdFaktura.UseCompatibleStateImageBehavior = false;
            this.lvwKrdFaktura.View = System.Windows.Forms.View.Details;
            this.lvwKrdFaktura.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwKrdFaktura_DragDrop);
            this.lvwKrdFaktura.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwKrdFaktura_ColumnClick);
            this.lvwKrdFaktura.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwKrdFaktura_DragEnter);
            this.lvwKrdFaktura.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwKrdFaktura_ItemDrag);
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
            // columnHeaderKFaknr
            // 
            this.columnHeaderKFaknr.Text = "Faknr";
            // 
            // columnHeaderKBelob
            // 
            this.columnHeaderKBelob.Text = "Beløb";
            // 
            // columnHeaderKBank
            // 
            this.columnHeaderKBank.Text = "Bank";
            // 
            // FrmBetalingsForslag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060v2.Properties.Settings.Default.FrmBetalingsForslagSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060v2.Properties.Settings.Default, "FrmBetalingsForslagSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060v2.Properties.Settings.Default, "FrmBetalingsForslagPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060v2.Properties.Settings.Default.FrmBetalingsForslagPoint;
            this.Name = "FrmBetalingsForslag";
            this.Text = "Betalings Forslag";
            this.Load += new System.EventHandler(this.FrmBetalingsForslag_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdBetal;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwKreditor;
        private System.Windows.Forms.ListView lvwKrdFaktura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdForslag;
        private System.Windows.Forms.ColumnHeader columnHeaderMNr;
        private System.Windows.Forms.ColumnHeader columnHeaderMNavn;
        private System.Windows.Forms.ColumnHeader columnHeaderMAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderMPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderMFaknr;
        private System.Windows.Forms.ColumnHeader columnHeaderKNnavn;
        private System.Windows.Forms.ColumnHeader columnHeaderKNR;
        private System.Windows.Forms.ColumnHeader columnHeaderKAdresse;
        private System.Windows.Forms.ColumnHeader columnHeaderKPostnr;
        private System.Windows.Forms.ColumnHeader columnHeaderKFaknr;
        private System.Windows.Forms.ProgressBar pgmForslag;
        private System.Windows.Forms.Label Label_Forslagstekst;
        private System.Windows.Forms.Label Label_Betaltekst;
        private System.Windows.Forms.ProgressBar pgmBetal;
        private System.Windows.Forms.ColumnHeader columnHeaderMBelob;
        private System.Windows.Forms.ColumnHeader columnHeaderKBelob;
        private System.Windows.Forms.ColumnHeader columnHeaderMBank;
        private System.Windows.Forms.ColumnHeader columnHeaderKBank;
    }
}