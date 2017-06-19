namespace Trans2Summa3060
{
    partial class FrmNyKontoplan
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
            this.components = new System.ComponentModel.Container();
            this.karNyKontoplanDataGridView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.OpretNyeKonti = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.karNyKontoplanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.karNyKontoplanDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.karNyKontoplanBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // karNyKontoplanDataGridView
            // 
            this.karNyKontoplanDataGridView.AllowUserToAddRows = false;
            this.karNyKontoplanDataGridView.AllowUserToDeleteRows = false;
            this.karNyKontoplanDataGridView.AutoGenerateColumns = false;
            this.karNyKontoplanDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.karNyKontoplanDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewCheckBoxColumn1});
            this.karNyKontoplanDataGridView.DataSource = this.karNyKontoplanBindingSource;
            this.karNyKontoplanDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.karNyKontoplanDataGridView.Location = new System.Drawing.Point(0, 0);
            this.karNyKontoplanDataGridView.Name = "karNyKontoplanDataGridView";
            this.karNyKontoplanDataGridView.Size = new System.Drawing.Size(664, 601);
            this.karNyKontoplanDataGridView.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.OpretNyeKonti);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.karNyKontoplanDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(664, 639);
            this.splitContainer1.SplitterDistance = 34;
            this.splitContainer1.TabIndex = 2;
            // 
            // OpretNyeKonti
            // 
            this.OpretNyeKonti.Location = new System.Drawing.Point(543, 6);
            this.OpretNyeKonti.Name = "OpretNyeKonti";
            this.OpretNyeKonti.Size = new System.Drawing.Size(95, 23);
            this.OpretNyeKonti.TabIndex = 0;
            this.OpretNyeKonti.Text = "Opret Nye Konti";
            this.OpretNyeKonti.UseVisualStyleBackColor = true;
            this.OpretNyeKonti.Click += new System.EventHandler(this.OpretNyeKonti_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Kontonr";
            this.dataGridViewTextBoxColumn1.HeaderText = "Kontonr";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "NytKontonr";
            this.dataGridViewTextBoxColumn2.HeaderText = "NytKontonr";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Kontonavn";
            this.dataGridViewTextBoxColumn3.HeaderText = "Kontonavn";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "SkalOprettes";
            this.dataGridViewCheckBoxColumn1.HeaderText = "SkalOprettes";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            // 
            // karNyKontoplanBindingSource
            // 
            this.karNyKontoplanBindingSource.DataSource = typeof(Trans2Summa3060.recNyKontoplan);
            // 
            // FrmNyKontoplan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 639);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmNyKontoplan";
            this.Text = "NyKontoplan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNyKontoplan_FormClosing);
            this.Load += new System.EventHandler(this.frmNyKontoplan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.karNyKontoplanDataGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.karNyKontoplanBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource karNyKontoplanBindingSource;
        private System.Windows.Forms.DataGridView karNyKontoplanDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button OpretNyeKonti;
    }
}