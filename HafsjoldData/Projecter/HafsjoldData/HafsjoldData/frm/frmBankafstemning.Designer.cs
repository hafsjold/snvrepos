namespace nsHafsjoldData
{
    partial class FrmBankafstemning
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lvwKontoudtogBank = new System.Windows.Forms.ListView();
            this.columnHeaderMNavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMNr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMFradato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMKontingent = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMTildato = new System.Windows.Forms.ColumnHeader();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lvwBilagBankkonto = new System.Windows.Forms.ListView();
            this.columnHeaderKNnavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKNR = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKAdresse = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKPostnr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKFradato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKKontingent = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKTildato = new System.Windows.Forms.ColumnHeader();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(916, 527);
            this.splitContainer1.SplitterDistance = 450;
            this.splitContainer1.TabIndex = 4;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lvwKontoudtogBank);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listView1);
            this.splitContainer2.Size = new System.Drawing.Size(450, 527);
            this.splitContainer2.SplitterDistance = 263;
            this.splitContainer2.TabIndex = 1;
            // 
            // lvwKontoudtogBank
            // 
            this.lvwKontoudtogBank.AllowDrop = true;
            this.lvwKontoudtogBank.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMNavn,
            this.columnHeaderMNr,
            this.columnHeaderMAdresse,
            this.columnHeaderMPostnr,
            this.columnHeaderMFradato,
            this.columnHeaderMKontingent,
            this.columnHeaderMTildato});
            this.lvwKontoudtogBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwKontoudtogBank.FullRowSelect = true;
            this.lvwKontoudtogBank.Location = new System.Drawing.Point(0, 0);
            this.lvwKontoudtogBank.Name = "lvwKontoudtogBank";
            this.lvwKontoudtogBank.Size = new System.Drawing.Size(450, 263);
            this.lvwKontoudtogBank.TabIndex = 0;
            this.lvwKontoudtogBank.UseCompatibleStateImageBehavior = false;
            this.lvwKontoudtogBank.View = System.Windows.Forms.View.Details;
            this.lvwKontoudtogBank.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwKontoudtogBank_DragDrop);
            this.lvwKontoudtogBank.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwKontoudtogBank_ColumnClick);
            this.lvwKontoudtogBank.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwKontoudtogBank_DragEnter);
            this.lvwKontoudtogBank.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwKontoudtogBank_ItemDrag);
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
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lvwBilagBankkonto);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.listView2);
            this.splitContainer3.Size = new System.Drawing.Size(462, 527);
            this.splitContainer3.SplitterDistance = 263;
            this.splitContainer3.TabIndex = 1;
            // 
            // lvwBilagBankkonto
            // 
            this.lvwBilagBankkonto.AllowDrop = true;
            this.lvwBilagBankkonto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderKNnavn,
            this.columnHeaderKNR,
            this.columnHeaderKAdresse,
            this.columnHeaderKPostnr,
            this.columnHeaderKFradato,
            this.columnHeaderKKontingent,
            this.columnHeaderKTildato});
            this.lvwBilagBankkonto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwBilagBankkonto.FullRowSelect = true;
            this.lvwBilagBankkonto.Location = new System.Drawing.Point(0, 0);
            this.lvwBilagBankkonto.Name = "lvwBilagBankkonto";
            this.lvwBilagBankkonto.Size = new System.Drawing.Size(462, 263);
            this.lvwBilagBankkonto.TabIndex = 0;
            this.lvwBilagBankkonto.UseCompatibleStateImageBehavior = false;
            this.lvwBilagBankkonto.View = System.Windows.Forms.View.Details;
            this.lvwBilagBankkonto.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwBilagBankkonto_DragDrop);
            this.lvwBilagBankkonto.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwBilagBankkonto_ColumnClick);
            this.lvwBilagBankkonto.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwBilagBankkonto_DragEnter);
            this.lvwBilagBankkonto.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwBilagBankkonto_ItemDrag);
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
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(450, 260);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Navn";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nr";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Adresse";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Postnr";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Fra dato";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Kontingent";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Til dato";
            // 
            // listView2
            // 
            this.listView2.AllowDrop = true;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.FullRowSelect = true;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(462, 260);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Navn";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Nr";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Adresse";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Postnr";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Fra dato";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Kontingent";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Til dato";
            // 
            // FrmBankafstemning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 527);
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsHafsjoldData.Properties.Settings.Default, "frmKontingentForslagSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsHafsjoldData.Properties.Settings.Default, "frmKontingentForslagPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Name = "FrmBankafstemning";
            this.Text = "Kontingent Forslag";
            this.Load += new System.EventHandler(this.FrmBankafstemning_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwKontoudtogBank;
        private System.Windows.Forms.ListView lvwBilagBankkonto;
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
        private System.Windows.Forms.ColumnHeader columnHeaderMKontingent;
        private System.Windows.Forms.ColumnHeader columnHeaderKKontingent;
        private System.Windows.Forms.ColumnHeader columnHeaderMTildato;
        private System.Windows.Forms.ColumnHeader columnHeaderKTildato;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
    }
}