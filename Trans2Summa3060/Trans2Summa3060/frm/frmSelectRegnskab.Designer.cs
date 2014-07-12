namespace Trans2Summa3060
{
    partial class frmSelectRegnskab
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
            this.cmdOpenRegnskab = new System.Windows.Forms.Button();
            this.cmdSidstAnventeRegnskab = new System.Windows.Forms.Button();
            this.Regnskab = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colRegnskab = new System.Windows.Forms.ColumnHeader();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label_listview = new System.Windows.Forms.Label();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // cmdOpenRegnskab
            // 
            this.cmdOpenRegnskab.Location = new System.Drawing.Point(57, 30);
            this.cmdOpenRegnskab.Name = "cmdOpenRegnskab";
            this.cmdOpenRegnskab.Size = new System.Drawing.Size(221, 40);
            this.cmdOpenRegnskab.TabIndex = 0;
            this.cmdOpenRegnskab.Text = "Åbn eksisterende regnskab >";
            this.cmdOpenRegnskab.UseVisualStyleBackColor = true;
            this.cmdOpenRegnskab.Click += new System.EventHandler(this.cmdOpenRegnskab_Click);
            // 
            // cmdSidstAnventeRegnskab
            // 
            this.cmdSidstAnventeRegnskab.Location = new System.Drawing.Point(57, 95);
            this.cmdSidstAnventeRegnskab.Name = "cmdSidstAnventeRegnskab";
            this.cmdSidstAnventeRegnskab.Size = new System.Drawing.Size(221, 40);
            this.cmdSidstAnventeRegnskab.TabIndex = 0;
            this.cmdSidstAnventeRegnskab.Text = "Åbn det sidst anvendte regnskab:";
            this.cmdSidstAnventeRegnskab.UseVisualStyleBackColor = true;
            this.cmdSidstAnventeRegnskab.Click += new System.EventHandler(this.cmdSidstAnventeRegnskab_Click);
            // 
            // Regnskab
            // 
            this.Regnskab.BackColor = System.Drawing.Color.White;
            this.Regnskab.Location = new System.Drawing.Point(28, 160);
            this.Regnskab.Name = "Regnskab";
            this.Regnskab.ReadOnly = true;
            this.Regnskab.Size = new System.Drawing.Size(276, 20);
            this.Regnskab.TabIndex = 1;
            this.Regnskab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colRegnskab,
            this.colStatus});
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(28, 30);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(276, 150);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Visible = false;
            this.listView1.DoubleClick += new System.EventHandler(this.cmdOK_Click);
            // 
            // colRegnskab
            // 
            this.colRegnskab.Text = "Regnskaber";
            this.colRegnskab.Width = 200;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(237, 188);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(67, 24);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Annuler";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Visible = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(164, 188);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(67, 24);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Visible = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label_listview
            // 
            this.label_listview.AutoSize = true;
            this.label_listview.Location = new System.Drawing.Point(25, 14);
            this.label_listview.Name = "label_listview";
            this.label_listview.Size = new System.Drawing.Size(59, 13);
            this.label_listview.TabIndex = 4;
            this.label_listview.Text = "Regnskab:";
            this.label_listview.Visible = false;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            // 
            // frmSelectRegnskab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(332, 224);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Regnskab);
            this.Controls.Add(this.label_listview);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSidstAnventeRegnskab);
            this.Controls.Add(this.cmdOpenRegnskab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectRegnskab";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medlem 3060 - Vælg regnskab";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSelectRegnskab_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOpenRegnskab;
        private System.Windows.Forms.Button cmdSidstAnventeRegnskab;
        private System.Windows.Forms.TextBox Regnskab;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colRegnskab;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label_listview;
        private System.Windows.Forms.ColumnHeader colStatus;
    }
}