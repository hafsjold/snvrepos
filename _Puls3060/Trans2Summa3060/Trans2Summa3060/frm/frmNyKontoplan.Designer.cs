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
            this.karNyKontoplanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.karNyKontoplanDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.karNyKontoplanBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // karNyKontoplanDataGridView
            // 
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
            this.karNyKontoplanDataGridView.Size = new System.Drawing.Size(689, 639);
            this.karNyKontoplanDataGridView.TabIndex = 1;
            // 
            // karNyKontoplanBindingSource
            // 
            this.karNyKontoplanBindingSource.DataSource = typeof(Trans2Summa3060.recNyKontoplan);
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
            // frmKarNyKontoplan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 639);
            this.Controls.Add(this.karNyKontoplanDataGridView);
            this.Name = "frmKarNyKontoplan";
            this.Text = "NyKontoplan";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNyKontoplan_FormClosing);
            this.Load += new System.EventHandler(this.frmNyKontoplan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.karNyKontoplanDataGridView)).EndInit();
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
    }
}