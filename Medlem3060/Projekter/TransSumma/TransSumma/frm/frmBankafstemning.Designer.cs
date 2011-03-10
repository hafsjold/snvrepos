﻿namespace nsPuls3060
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
            this.lvwBank = new System.Windows.Forms.ListView();
            this.columnHeaderBDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBTekst = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderBBelob = new System.Windows.Forms.ColumnHeader();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lvwAfstemBank = new System.Windows.Forms.ListView();
            this.columnHeaderABDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderABTekst = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderABBelob = new System.Windows.Forms.ColumnHeader();
            this.RykketTidligere = new System.Windows.Forms.CheckBox();
            this.cmdForslag = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lvwTrans = new System.Windows.Forms.ListView();
            this.columnHeaderTDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTBilag = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTTekst = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTBelob = new System.Windows.Forms.ColumnHeader();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.lvwAfstemTrans = new System.Windows.Forms.ListView();
            this.columnHeaderATDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderATBilag = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderATTekst = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderATBelob = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(813, 512);
            this.splitContainer1.SplitterDistance = 328;
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
            this.splitContainer2.Panel1.Controls.Add(this.lvwBank);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(328, 512);
            this.splitContainer2.SplitterDistance = 233;
            this.splitContainer2.TabIndex = 0;
            // 
            // lvwBank
            // 
            this.lvwBank.AllowDrop = true;
            this.lvwBank.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderBDato,
            this.columnHeaderBTekst,
            this.columnHeaderBBelob});
            this.lvwBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwBank.FullRowSelect = true;
            this.lvwBank.Location = new System.Drawing.Point(0, 0);
            this.lvwBank.Name = "lvwBank";
            this.lvwBank.Size = new System.Drawing.Size(328, 233);
            this.lvwBank.TabIndex = 0;
            this.lvwBank.UseCompatibleStateImageBehavior = false;
            this.lvwBank.View = System.Windows.Forms.View.Details;
            this.lvwBank.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwBank_DragDrop);
            this.lvwBank.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwMedlem_ColumnClick);
            this.lvwBank.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwBank_DragEnter);
            this.lvwBank.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwBank_ItemDrag);
            // 
            // columnHeaderBDato
            // 
            this.columnHeaderBDato.Text = "Dato";
            this.columnHeaderBDato.Width = 62;
            // 
            // columnHeaderBTekst
            // 
            this.columnHeaderBTekst.Text = "Tekst";
            this.columnHeaderBTekst.Width = 192;
            // 
            // columnHeaderBBelob
            // 
            this.columnHeaderBBelob.Text = "Beløb";
            this.columnHeaderBBelob.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.lvwAfstemBank);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.RykketTidligere);
            this.splitContainer4.Panel2.Controls.Add(this.cmdForslag);
            this.splitContainer4.Size = new System.Drawing.Size(328, 275);
            this.splitContainer4.SplitterDistance = 136;
            this.splitContainer4.TabIndex = 0;
            // 
            // lvwAfstemBank
            // 
            this.lvwAfstemBank.AllowDrop = true;
            this.lvwAfstemBank.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderABDato,
            this.columnHeaderABTekst,
            this.columnHeaderABBelob});
            this.lvwAfstemBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwAfstemBank.FullRowSelect = true;
            this.lvwAfstemBank.Location = new System.Drawing.Point(0, 0);
            this.lvwAfstemBank.Name = "lvwAfstemBank";
            this.lvwAfstemBank.Size = new System.Drawing.Size(328, 136);
            this.lvwAfstemBank.TabIndex = 1;
            this.lvwAfstemBank.UseCompatibleStateImageBehavior = false;
            this.lvwAfstemBank.View = System.Windows.Forms.View.Details;
            this.lvwAfstemBank.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwAfstemBank_DragDrop);
            this.lvwAfstemBank.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwAfstemBank_ColumnClick);
            this.lvwAfstemBank.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwAfstemBank_DragEnter);
            this.lvwAfstemBank.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwAfstemBank_ItemDrag);
            // 
            // columnHeaderABDato
            // 
            this.columnHeaderABDato.Text = "Dato";
            // 
            // columnHeaderABTekst
            // 
            this.columnHeaderABTekst.Text = "Tekst";
            this.columnHeaderABTekst.Width = 192;
            // 
            // columnHeaderABBelob
            // 
            this.columnHeaderABBelob.Text = "Beløb";
            this.columnHeaderABBelob.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // RykketTidligere
            // 
            this.RykketTidligere.AutoSize = true;
            this.RykketTidligere.Location = new System.Drawing.Point(57, 88);
            this.RykketTidligere.Name = "RykketTidligere";
            this.RykketTidligere.Size = new System.Drawing.Size(99, 17);
            this.RykketTidligere.TabIndex = 8;
            this.RykketTidligere.Text = "Rykket tidligere";
            this.RykketTidligere.UseVisualStyleBackColor = true;
            // 
            // cmdForslag
            // 
            this.cmdForslag.Location = new System.Drawing.Point(202, 80);
            this.cmdForslag.Name = "cmdForslag";
            this.cmdForslag.Size = new System.Drawing.Size(61, 25);
            this.cmdForslag.TabIndex = 3;
            this.cmdForslag.Text = "Forslag";
            this.cmdForslag.UseVisualStyleBackColor = true;
            this.cmdForslag.Click += new System.EventHandler(this.cmdForslag_Click);
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
            this.splitContainer3.Panel1.Controls.Add(this.lvwTrans);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer3.Size = new System.Drawing.Size(481, 512);
            this.splitContainer3.SplitterDistance = 233;
            this.splitContainer3.TabIndex = 0;
            // 
            // lvwTrans
            // 
            this.lvwTrans.AllowDrop = true;
            this.lvwTrans.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTDato,
            this.columnHeaderTBilag,
            this.columnHeaderTTekst,
            this.columnHeaderTBelob});
            this.lvwTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwTrans.FullRowSelect = true;
            this.lvwTrans.Location = new System.Drawing.Point(0, 0);
            this.lvwTrans.Name = "lvwTrans";
            this.lvwTrans.Size = new System.Drawing.Size(481, 233);
            this.lvwTrans.TabIndex = 0;
            this.lvwTrans.UseCompatibleStateImageBehavior = false;
            this.lvwTrans.View = System.Windows.Forms.View.Details;
            this.lvwTrans.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwTrans_DragDrop);
            this.lvwTrans.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwTrans_ColumnClick);
            this.lvwTrans.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwTrans_DragEnter);
            this.lvwTrans.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwTrans_ItemDrag);
            // 
            // columnHeaderTDato
            // 
            this.columnHeaderTDato.Text = "Dato";
            // 
            // columnHeaderTBilag
            // 
            this.columnHeaderTBilag.Text = "Bilag";
            this.columnHeaderTBilag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeaderTTekst
            // 
            this.columnHeaderTTekst.Text = "Tekst";
            // 
            // columnHeaderTBelob
            // 
            this.columnHeaderTBelob.Text = "Beløb";
            this.columnHeaderTBelob.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.lvwAfstemTrans);
            this.splitContainer5.Size = new System.Drawing.Size(481, 275);
            this.splitContainer5.SplitterDistance = 136;
            this.splitContainer5.TabIndex = 0;
            // 
            // lvwAfstemTrans
            // 
            this.lvwAfstemTrans.AllowDrop = true;
            this.lvwAfstemTrans.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderATDato,
            this.columnHeaderATBilag,
            this.columnHeaderATTekst,
            this.columnHeaderATBelob});
            this.lvwAfstemTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwAfstemTrans.FullRowSelect = true;
            this.lvwAfstemTrans.Location = new System.Drawing.Point(0, 0);
            this.lvwAfstemTrans.Name = "lvwAfstemTrans";
            this.lvwAfstemTrans.Size = new System.Drawing.Size(481, 136);
            this.lvwAfstemTrans.TabIndex = 2;
            this.lvwAfstemTrans.UseCompatibleStateImageBehavior = false;
            this.lvwAfstemTrans.View = System.Windows.Forms.View.Details;
            this.lvwAfstemTrans.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwAfstemTrans_DragDrop);
            this.lvwAfstemTrans.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwAfstemTrans_ColumnClick);
            this.lvwAfstemTrans.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwAfstemTrans_DragEnter);
            this.lvwAfstemTrans.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwAfstemTrans_ItemDrag);
            // 
            // columnHeaderATDato
            // 
            this.columnHeaderATDato.Text = "Dato";
            // 
            // columnHeaderATBilag
            // 
            this.columnHeaderATBilag.Text = "Bilag";
            this.columnHeaderATBilag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderATBilag.Width = 53;
            // 
            // columnHeaderATTekst
            // 
            this.columnHeaderATTekst.Text = "Tekst";
            this.columnHeaderATTekst.Width = 192;
            // 
            // columnHeaderATBelob
            // 
            this.columnHeaderATBelob.Text = "Beløb";
            this.columnHeaderATBelob.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FrmBankafstemning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmRykkerForslagClientSize;
            this.Controls.Add(this.splitContainer1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmRykkerForslagClientSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmRykkerForslagPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmRykkerForslagPoint;
            this.Name = "FrmBankafstemning";
            this.Text = "Bankafstemning";
            this.Load += new System.EventHandler(this.FrmBankafstemning_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwBank;
        private System.Windows.Forms.ListView lvwTrans;
        private System.Windows.Forms.Button cmdForslag;
        private System.Windows.Forms.ColumnHeader columnHeaderBDato;
        private System.Windows.Forms.ColumnHeader columnHeaderBTekst;
        private System.Windows.Forms.ColumnHeader columnHeaderTDato;
        private System.Windows.Forms.ColumnHeader columnHeaderTBilag;
        private System.Windows.Forms.ColumnHeader columnHeaderTTekst;
        private System.Windows.Forms.ColumnHeader columnHeaderTBelob;
        private System.Windows.Forms.ColumnHeader columnHeaderBBelob;
        private System.Windows.Forms.CheckBox RykketTidligere;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView lvwAfstemBank;
        private System.Windows.Forms.ColumnHeader columnHeaderABDato;
        private System.Windows.Forms.ColumnHeader columnHeaderABTekst;
        private System.Windows.Forms.ColumnHeader columnHeaderABBelob;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.ListView lvwAfstemTrans;
        private System.Windows.Forms.ColumnHeader columnHeaderATDato;
        private System.Windows.Forms.ColumnHeader columnHeaderATBilag;
        private System.Windows.Forms.ColumnHeader columnHeaderATTekst;
        private System.Windows.Forms.ColumnHeader columnHeaderATBelob;
    }
}