namespace docdb
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
            this.txtBoxSelskab = new System.Windows.Forms.TextBox();
            this.labelSelskab = new System.Windows.Forms.Label();
            this.txtBoxÅr = new System.Windows.Forms.TextBox();
            this.labelÅr = new System.Windows.Forms.Label();
            this.txtBoxProdukt = new System.Windows.Forms.TextBox();
            this.labelProdukt = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.txtBoxDokument = new System.Windows.Forms.TextBox();
            this.labelDokument = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBoxSelskab
            // 
            this.txtBoxSelskab.Location = new System.Drawing.Point(92, 72);
            this.txtBoxSelskab.Name = "txtBoxSelskab";
            this.txtBoxSelskab.Size = new System.Drawing.Size(223, 20);
            this.txtBoxSelskab.TabIndex = 0;
            // 
            // labelSelskab
            // 
            this.labelSelskab.AutoSize = true;
            this.labelSelskab.Location = new System.Drawing.Point(21, 79);
            this.labelSelskab.Name = "labelSelskab";
            this.labelSelskab.Size = new System.Drawing.Size(45, 13);
            this.labelSelskab.TabIndex = 1;
            this.labelSelskab.Text = "Selskab";
            // 
            // txtBoxÅr
            // 
            this.txtBoxÅr.Location = new System.Drawing.Point(92, 102);
            this.txtBoxÅr.Name = "txtBoxÅr";
            this.txtBoxÅr.Size = new System.Drawing.Size(67, 20);
            this.txtBoxÅr.TabIndex = 0;
            // 
            // labelÅr
            // 
            this.labelÅr.AutoSize = true;
            this.labelÅr.Location = new System.Drawing.Point(21, 109);
            this.labelÅr.Name = "labelÅr";
            this.labelÅr.Size = new System.Drawing.Size(17, 13);
            this.labelÅr.TabIndex = 1;
            this.labelÅr.Text = "År";
            // 
            // txtBoxProdukt
            // 
            this.txtBoxProdukt.Location = new System.Drawing.Point(92, 139);
            this.txtBoxProdukt.Name = "txtBoxProdukt";
            this.txtBoxProdukt.Size = new System.Drawing.Size(223, 20);
            this.txtBoxProdukt.TabIndex = 0;
            // 
            // labelProdukt
            // 
            this.labelProdukt.AutoSize = true;
            this.labelProdukt.Location = new System.Drawing.Point(21, 146);
            this.labelProdukt.Name = "labelProdukt";
            this.labelProdukt.Size = new System.Drawing.Size(44, 13);
            this.labelProdukt.TabIndex = 1;
            this.labelProdukt.Text = "Produkt";
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(240, 181);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "OK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // txtBoxDokument
            // 
            this.txtBoxDokument.Location = new System.Drawing.Point(92, 32);
            this.txtBoxDokument.Name = "txtBoxDokument";
            this.txtBoxDokument.ReadOnly = true;
            this.txtBoxDokument.Size = new System.Drawing.Size(223, 20);
            this.txtBoxDokument.TabIndex = 0;
            // 
            // labelDokument
            // 
            this.labelDokument.AutoSize = true;
            this.labelDokument.Location = new System.Drawing.Point(21, 39);
            this.labelDokument.Name = "labelDokument";
            this.labelDokument.Size = new System.Drawing.Size(56, 13);
            this.labelDokument.TabIndex = 1;
            this.labelDokument.Text = "Dokument";
            // 
            // frmAddDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 223);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labelProdukt);
            this.Controls.Add(this.labelÅr);
            this.Controls.Add(this.labelDokument);
            this.Controls.Add(this.labelSelskab);
            this.Controls.Add(this.txtBoxProdukt);
            this.Controls.Add(this.txtBoxDokument);
            this.Controls.Add(this.txtBoxÅr);
            this.Controls.Add(this.txtBoxSelskab);
            this.Name = "frmAddDoc";
            this.Text = "Indsæt dokument";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxSelskab;
        private System.Windows.Forms.Label labelSelskab;
        private System.Windows.Forms.TextBox txtBoxÅr;
        private System.Windows.Forms.Label labelÅr;
        private System.Windows.Forms.TextBox txtBoxProdukt;
        private System.Windows.Forms.Label labelProdukt;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.TextBox txtBoxDokument;
        private System.Windows.Forms.Label labelDokument;
    }
}