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
            this.lvwKontoudtog = new System.Windows.Forms.ListView();
            this.colHdKDato = new System.Windows.Forms.ColumnHeader();
            this.colHdKTekst = new System.Windows.Forms.ColumnHeader();
            this.colHdKBeløb = new System.Windows.Forms.ColumnHeader();
            this.lvwKontoudtogAfstemt = new System.Windows.Forms.ListView();
            this.colHdKADato = new System.Windows.Forms.ColumnHeader();
            this.colHdKATekst = new System.Windows.Forms.ColumnHeader();
            this.colHdKABeløb = new System.Windows.Forms.ColumnHeader();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lvwBilag = new System.Windows.Forms.ListView();
            this.colHdBDato = new System.Windows.Forms.ColumnHeader();
            this.colHdBBilag = new System.Windows.Forms.ColumnHeader();
            this.colHdBTekst = new System.Windows.Forms.ColumnHeader();
            this.colHdBBeløb = new System.Windows.Forms.ColumnHeader();
            this.lvwBilagAfstemt = new System.Windows.Forms.ListView();
            this.colHdBADato = new System.Windows.Forms.ColumnHeader();
            this.colHdBABilag = new System.Windows.Forms.ColumnHeader();
            this.colHdBATekst = new System.Windows.Forms.ColumnHeader();
            this.colHdBABeløb = new System.Windows.Forms.ColumnHeader();
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
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
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
            this.splitContainer1.Size = new System.Drawing.Size(916, 469);
            this.splitContainer1.SplitterDistance = 448;
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
            this.splitContainer2.Panel1.Controls.Add(this.lvwKontoudtog);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lvwKontoudtogAfstemt);
            this.splitContainer2.Size = new System.Drawing.Size(448, 469);
            this.splitContainer2.SplitterDistance = 350;
            this.splitContainer2.TabIndex = 1;
            // 
            // lvwKontoudtog
            // 
            this.lvwKontoudtog.AllowDrop = true;
            this.lvwKontoudtog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHdKDato,
            this.colHdKTekst,
            this.colHdKBeløb});
            this.lvwKontoudtog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwKontoudtog.FullRowSelect = true;
            this.lvwKontoudtog.Location = new System.Drawing.Point(0, 0);
            this.lvwKontoudtog.Name = "lvwKontoudtog";
            this.lvwKontoudtog.Size = new System.Drawing.Size(448, 350);
            this.lvwKontoudtog.TabIndex = 0;
            this.lvwKontoudtog.UseCompatibleStateImageBehavior = false;
            this.lvwKontoudtog.View = System.Windows.Forms.View.Details;
            this.lvwKontoudtog.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwKontoudtog_DragDrop);
            this.lvwKontoudtog.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwKontoudtog_ColumnClick);
            this.lvwKontoudtog.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwKontoudtog_DragEnter);
            this.lvwKontoudtog.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwKontoudtog_ItemDrag);
            // 
            // colHdKDato
            // 
            this.colHdKDato.Text = "Dato";
            this.colHdKDato.Width = 95;
            // 
            // colHdKTekst
            // 
            this.colHdKTekst.Text = "Tekst";
            this.colHdKTekst.Width = 220;
            // 
            // colHdKBeløb
            // 
            this.colHdKBeløb.Text = "Beløb";
            this.colHdKBeløb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colHdKBeløb.Width = 100;
            // 
            // lvwKontoudtogAfstemt
            // 
            this.lvwKontoudtogAfstemt.AllowDrop = true;
            this.lvwKontoudtogAfstemt.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHdKADato,
            this.colHdKATekst,
            this.colHdKABeløb});
            this.lvwKontoudtogAfstemt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwKontoudtogAfstemt.FullRowSelect = true;
            this.lvwKontoudtogAfstemt.Location = new System.Drawing.Point(0, 0);
            this.lvwKontoudtogAfstemt.Name = "lvwKontoudtogAfstemt";
            this.lvwKontoudtogAfstemt.Size = new System.Drawing.Size(448, 115);
            this.lvwKontoudtogAfstemt.TabIndex = 0;
            this.lvwKontoudtogAfstemt.UseCompatibleStateImageBehavior = false;
            this.lvwKontoudtogAfstemt.View = System.Windows.Forms.View.Details;
            this.lvwKontoudtogAfstemt.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwKontoudtogAfstemt_DragDrop);
            this.lvwKontoudtogAfstemt.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwKontoudtogAfstemt_ColumnClick);
            this.lvwKontoudtogAfstemt.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwKontoudtogAfstemt_DragEnter);
            this.lvwKontoudtogAfstemt.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwKontoudtogAfstemt_ItemDrag);
            // 
            // colHdKADato
            // 
            this.colHdKADato.Text = "Dato";
            this.colHdKADato.Width = 95;
            // 
            // colHdKATekst
            // 
            this.colHdKATekst.Text = "Tekst";
            this.colHdKATekst.Width = 220;
            // 
            // colHdKABeløb
            // 
            this.colHdKABeløb.Text = "Beløb";
            this.colHdKABeløb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colHdKABeløb.Width = 100;
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
            this.splitContainer3.Panel1.Controls.Add(this.lvwBilag);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.lvwBilagAfstemt);
            this.splitContainer3.Size = new System.Drawing.Size(464, 469);
            this.splitContainer3.SplitterDistance = 350;
            this.splitContainer3.TabIndex = 1;
            // 
            // lvwBilag
            // 
            this.lvwBilag.AllowDrop = true;
            this.lvwBilag.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHdBDato,
            this.colHdBBilag,
            this.colHdBTekst,
            this.colHdBBeløb});
            this.lvwBilag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwBilag.FullRowSelect = true;
            this.lvwBilag.Location = new System.Drawing.Point(0, 0);
            this.lvwBilag.Name = "lvwBilag";
            this.lvwBilag.Size = new System.Drawing.Size(464, 350);
            this.lvwBilag.TabIndex = 1;
            this.lvwBilag.UseCompatibleStateImageBehavior = false;
            this.lvwBilag.View = System.Windows.Forms.View.Details;
            this.lvwBilag.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwBilag_DragDrop);
            this.lvwBilag.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwBilag_ColumnClick);
            this.lvwBilag.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwBilag_DragEnter);
            this.lvwBilag.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwBilag_ItemDrag);
            // 
            // colHdBDato
            // 
            this.colHdBDato.Text = "Dato";
            this.colHdBDato.Width = 95;
            // 
            // colHdBBilag
            // 
            this.colHdBBilag.Text = "Bilag";
            this.colHdBBilag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colHdBTekst
            // 
            this.colHdBTekst.Text = "Tekst";
            this.colHdBTekst.Width = 180;
            // 
            // colHdBBeløb
            // 
            this.colHdBBeløb.Text = "Beløb";
            this.colHdBBeløb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colHdBBeløb.Width = 100;
            // 
            // lvwBilagAfstemt
            // 
            this.lvwBilagAfstemt.AllowDrop = true;
            this.lvwBilagAfstemt.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHdBADato,
            this.colHdBABilag,
            this.colHdBATekst,
            this.colHdBABeløb});
            this.lvwBilagAfstemt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwBilagAfstemt.FullRowSelect = true;
            this.lvwBilagAfstemt.Location = new System.Drawing.Point(0, 0);
            this.lvwBilagAfstemt.Name = "lvwBilagAfstemt";
            this.lvwBilagAfstemt.Size = new System.Drawing.Size(464, 115);
            this.lvwBilagAfstemt.TabIndex = 1;
            this.lvwBilagAfstemt.UseCompatibleStateImageBehavior = false;
            this.lvwBilagAfstemt.View = System.Windows.Forms.View.Details;
            this.lvwBilagAfstemt.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwBilagAfstemt_DragDrop);
            this.lvwBilagAfstemt.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwBilagAfstemt_ColumnClick);
            this.lvwBilagAfstemt.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwBilagAfstemt_DragEnter);
            this.lvwBilagAfstemt.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwBilagAfstemt_ItemDrag);
            // 
            // colHdBADato
            // 
            this.colHdBADato.Text = "Dato";
            this.colHdBADato.Width = 95;
            // 
            // colHdBABilag
            // 
            this.colHdBABilag.Text = "Bilag";
            this.colHdBABilag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // colHdBATekst
            // 
            this.colHdBATekst.Text = "Tekst";
            this.colHdBATekst.Width = 180;
            // 
            // colHdBABeløb
            // 
            this.colHdBABeløb.Text = "Beløb";
            this.colHdBABeløb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colHdBABeløb.Width = 100;
            // 
            // FrmBankafstemning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 506);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmBankafstemning";
            this.Text = "Bankafstemning";
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
        private System.Windows.Forms.ListView lvwKontoudtog;
        private System.Windows.Forms.ListView lvwKontoudtogAfstemt;
        private System.Windows.Forms.ColumnHeader colHdKTekst;
        private System.Windows.Forms.ColumnHeader colHdKDato;
        private System.Windows.Forms.ColumnHeader colHdKBeløb;
        private System.Windows.Forms.ColumnHeader colHdKADato;
        private System.Windows.Forms.ColumnHeader colHdKATekst;
        private System.Windows.Forms.ColumnHeader colHdKABeløb;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView lvwBilag;
        private System.Windows.Forms.ColumnHeader colHdBDato;
        private System.Windows.Forms.ColumnHeader colHdBBilag;
        private System.Windows.Forms.ColumnHeader colHdBTekst;
        private System.Windows.Forms.ColumnHeader colHdBBeløb;
        private System.Windows.Forms.ListView lvwBilagAfstemt;
        private System.Windows.Forms.ColumnHeader colHdBADato;
        private System.Windows.Forms.ColumnHeader colHdBABilag;
        private System.Windows.Forms.ColumnHeader colHdBATekst;
        private System.Windows.Forms.ColumnHeader colHdBABeløb;
    }
}