namespace nsPuls3060
{
    partial class FrmKontoplanList
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderKontonr = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderKontonavn = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderMoms = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderKontonr,
            this.columnHeaderKontonavn,
            this.columnHeaderMoms});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(335, 273);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderKontonr
            // 
            this.columnHeaderKontonr.Text = "Kontonr";
            // 
            // columnHeaderKontonavn
            // 
            this.columnHeaderKontonavn.Text = "Kontonavn";
            this.columnHeaderKontonavn.Width = 194;
            // 
            // columnHeaderMoms
            // 
            this.columnHeaderMoms.Text = "Moms";
            // 
            // FrmKontoplanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 273);
            this.Controls.Add(this.listView1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmKontoplanListLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmKontoplanListLocation;
            this.Name = "FrmKontoplanList";
            this.Text = "Kontoplan";
            this.Load += new System.EventHandler(this.FrmKontoplanList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderKontonr;
        private System.Windows.Forms.ColumnHeader columnHeaderKontonavn;
        private System.Windows.Forms.ColumnHeader columnHeaderMoms;
    }
}