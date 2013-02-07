namespace docdblite
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
            this.txtBoxVirksomhed = new System.Windows.Forms.TextBox();
            this.labelVirksomhed = new System.Windows.Forms.Label();
            this.txtBoxÅr = new System.Windows.Forms.TextBox();
            this.labelÅr = new System.Windows.Forms.Label();
            this.txtBoxBeskrivelse = new System.Windows.Forms.TextBox();
            this.labelBeskrivelse = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.txtBoxDokument = new System.Windows.Forms.TextBox();
            this.labelDokument = new System.Windows.Forms.Label();
            this.txtBoxEmne = new System.Windows.Forms.TextBox();
            this.labelEmne = new System.Windows.Forms.Label();
            this.txtBoxDokument_type = new System.Windows.Forms.TextBox();
            this.labelDokument_type = new System.Windows.Forms.Label();
            this.txtBoxEkstern_kilde = new System.Windows.Forms.TextBox();
            this.labelEkstern_ref = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBoxVirksomhed
            // 
            this.txtBoxVirksomhed.Location = new System.Drawing.Point(92, 72);
            this.txtBoxVirksomhed.MaxLength = 50;
            this.txtBoxVirksomhed.Name = "txtBoxVirksomhed";
            this.txtBoxVirksomhed.Size = new System.Drawing.Size(278, 20);
            this.txtBoxVirksomhed.TabIndex = 3;
            // 
            // labelVirksomhed
            // 
            this.labelVirksomhed.AutoSize = true;
            this.labelVirksomhed.Location = new System.Drawing.Point(21, 79);
            this.labelVirksomhed.Name = "labelVirksomhed";
            this.labelVirksomhed.Size = new System.Drawing.Size(62, 13);
            this.labelVirksomhed.TabIndex = 2;
            this.labelVirksomhed.Text = "Virksomhed";
            // 
            // txtBoxÅr
            // 
            this.txtBoxÅr.Location = new System.Drawing.Point(92, 150);
            this.txtBoxÅr.MaxLength = 4;
            this.txtBoxÅr.Name = "txtBoxÅr";
            this.txtBoxÅr.Size = new System.Drawing.Size(92, 20);
            this.txtBoxÅr.TabIndex = 9;
            // 
            // labelÅr
            // 
            this.labelÅr.AutoSize = true;
            this.labelÅr.Location = new System.Drawing.Point(21, 157);
            this.labelÅr.Name = "labelÅr";
            this.labelÅr.Size = new System.Drawing.Size(17, 13);
            this.labelÅr.TabIndex = 8;
            this.labelÅr.Text = "År";
            // 
            // txtBoxBeskrivelse
            // 
            this.txtBoxBeskrivelse.Location = new System.Drawing.Point(92, 202);
            this.txtBoxBeskrivelse.MaxLength = 100;
            this.txtBoxBeskrivelse.Name = "txtBoxBeskrivelse";
            this.txtBoxBeskrivelse.Size = new System.Drawing.Size(278, 20);
            this.txtBoxBeskrivelse.TabIndex = 13;
            // 
            // labelBeskrivelse
            // 
            this.labelBeskrivelse.AutoSize = true;
            this.labelBeskrivelse.Location = new System.Drawing.Point(21, 209);
            this.labelBeskrivelse.Name = "labelBeskrivelse";
            this.labelBeskrivelse.Size = new System.Drawing.Size(61, 13);
            this.labelBeskrivelse.TabIndex = 12;
            this.labelBeskrivelse.Text = "Beskrivelse";
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(278, 240);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(92, 23);
            this.butOK.TabIndex = 18;
            this.butOK.Text = "Indsæt";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // txtBoxDokument
            // 
            this.txtBoxDokument.Location = new System.Drawing.Point(92, 32);
            this.txtBoxDokument.Name = "txtBoxDokument";
            this.txtBoxDokument.ReadOnly = true;
            this.txtBoxDokument.Size = new System.Drawing.Size(278, 20);
            this.txtBoxDokument.TabIndex = 1;
            // 
            // labelDokument
            // 
            this.labelDokument.AutoSize = true;
            this.labelDokument.Location = new System.Drawing.Point(21, 39);
            this.labelDokument.Name = "labelDokument";
            this.labelDokument.Size = new System.Drawing.Size(56, 13);
            this.labelDokument.TabIndex = 0;
            this.labelDokument.Text = "Dokument";
            // 
            // txtBoxEmne
            // 
            this.txtBoxEmne.Location = new System.Drawing.Point(92, 98);
            this.txtBoxEmne.MaxLength = 50;
            this.txtBoxEmne.Name = "txtBoxEmne";
            this.txtBoxEmne.Size = new System.Drawing.Size(278, 20);
            this.txtBoxEmne.TabIndex = 5;
            // 
            // labelEmne
            // 
            this.labelEmne.AutoSize = true;
            this.labelEmne.Location = new System.Drawing.Point(21, 105);
            this.labelEmne.Name = "labelEmne";
            this.labelEmne.Size = new System.Drawing.Size(34, 13);
            this.labelEmne.TabIndex = 4;
            this.labelEmne.Text = "Emne";
            // 
            // txtBoxDokument_type
            // 
            this.txtBoxDokument_type.Location = new System.Drawing.Point(92, 124);
            this.txtBoxDokument_type.MaxLength = 50;
            this.txtBoxDokument_type.Name = "txtBoxDokument_type";
            this.txtBoxDokument_type.Size = new System.Drawing.Size(278, 20);
            this.txtBoxDokument_type.TabIndex = 7;
            // 
            // labelDokument_type
            // 
            this.labelDokument_type.AutoSize = true;
            this.labelDokument_type.Location = new System.Drawing.Point(21, 131);
            this.labelDokument_type.Name = "labelDokument_type";
            this.labelDokument_type.Size = new System.Drawing.Size(50, 13);
            this.labelDokument_type.TabIndex = 6;
            this.labelDokument_type.Text = "Dok type";
            // 
            // txtBoxEkstern_kilde
            // 
            this.txtBoxEkstern_kilde.Location = new System.Drawing.Point(92, 176);
            this.txtBoxEkstern_kilde.MaxLength = 50;
            this.txtBoxEkstern_kilde.Name = "txtBoxEkstern_kilde";
            this.txtBoxEkstern_kilde.Size = new System.Drawing.Size(278, 20);
            this.txtBoxEkstern_kilde.TabIndex = 11;
            // 
            // labelEkstern_ref
            // 
            this.labelEkstern_ref.AutoSize = true;
            this.labelEkstern_ref.Location = new System.Drawing.Point(21, 183);
            this.labelEkstern_ref.Name = "labelEkstern_ref";
            this.labelEkstern_ref.Size = new System.Drawing.Size(58, 13);
            this.labelEkstern_ref.TabIndex = 10;
            this.labelEkstern_ref.Text = "Ekstern ref";
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(92, 240);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(92, 23);
            this.butCancel.TabIndex = 19;
            this.butCancel.Text = "Fortryd";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // frmAddDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 289);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labelEmne);
            this.Controls.Add(this.labelDokument_type);
            this.Controls.Add(this.labelEkstern_ref);
            this.Controls.Add(this.labelBeskrivelse);
            this.Controls.Add(this.labelÅr);
            this.Controls.Add(this.labelDokument);
            this.Controls.Add(this.labelVirksomhed);
            this.Controls.Add(this.txtBoxEmne);
            this.Controls.Add(this.txtBoxDokument_type);
            this.Controls.Add(this.txtBoxEkstern_kilde);
            this.Controls.Add(this.txtBoxBeskrivelse);
            this.Controls.Add(this.txtBoxDokument);
            this.Controls.Add(this.txtBoxÅr);
            this.Controls.Add(this.txtBoxVirksomhed);
            this.Name = "frmAddDoc";
            this.Text = "Indsæt dokument";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxVirksomhed;
        private System.Windows.Forms.Label labelVirksomhed;
        private System.Windows.Forms.TextBox txtBoxÅr;
        private System.Windows.Forms.Label labelÅr;
        private System.Windows.Forms.TextBox txtBoxBeskrivelse;
        private System.Windows.Forms.Label labelBeskrivelse;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.TextBox txtBoxDokument;
        private System.Windows.Forms.Label labelDokument;
        private System.Windows.Forms.TextBox txtBoxEmne;
        private System.Windows.Forms.Label labelEmne;
        private System.Windows.Forms.TextBox txtBoxDokument_type;
        private System.Windows.Forms.Label labelDokument_type;
        private System.Windows.Forms.TextBox txtBoxEkstern_kilde;
        private System.Windows.Forms.Label labelEkstern_ref;
        private System.Windows.Forms.Button butCancel;
    }
}