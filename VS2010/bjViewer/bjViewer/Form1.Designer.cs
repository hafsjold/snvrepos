namespace bjViewer
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refnrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.virksomhedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dokumenttypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.årDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eksternkildeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beskrivelseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oprettesafDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oprettetdatoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kildestiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xmldocsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmldocsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.refnrDataGridViewTextBoxColumn,
            this.virksomhedDataGridViewTextBoxColumn,
            this.emneDataGridViewTextBoxColumn,
            this.dokumenttypeDataGridViewTextBoxColumn,
            this.årDataGridViewTextBoxColumn,
            this.eksternkildeDataGridViewTextBoxColumn,
            this.beskrivelseDataGridViewTextBoxColumn,
            this.oprettesafDataGridViewTextBoxColumn,
            this.oprettetdatoDataGridViewTextBoxColumn,
            this.kildestiDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.xmldocsBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(802, 382);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // refnrDataGridViewTextBoxColumn
            // 
            this.refnrDataGridViewTextBoxColumn.DataPropertyName = "ref_nr";
            this.refnrDataGridViewTextBoxColumn.HeaderText = "Nr";
            this.refnrDataGridViewTextBoxColumn.Name = "refnrDataGridViewTextBoxColumn";
            this.refnrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // virksomhedDataGridViewTextBoxColumn
            // 
            this.virksomhedDataGridViewTextBoxColumn.DataPropertyName = "virksomhed";
            this.virksomhedDataGridViewTextBoxColumn.HeaderText = "Virksomhed";
            this.virksomhedDataGridViewTextBoxColumn.Name = "virksomhedDataGridViewTextBoxColumn";
            this.virksomhedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emneDataGridViewTextBoxColumn
            // 
            this.emneDataGridViewTextBoxColumn.DataPropertyName = "emne";
            this.emneDataGridViewTextBoxColumn.HeaderText = "Emne";
            this.emneDataGridViewTextBoxColumn.Name = "emneDataGridViewTextBoxColumn";
            this.emneDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dokumenttypeDataGridViewTextBoxColumn
            // 
            this.dokumenttypeDataGridViewTextBoxColumn.DataPropertyName = "dokument_type";
            this.dokumenttypeDataGridViewTextBoxColumn.HeaderText = "Dokument type";
            this.dokumenttypeDataGridViewTextBoxColumn.Name = "dokumenttypeDataGridViewTextBoxColumn";
            this.dokumenttypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // årDataGridViewTextBoxColumn
            // 
            this.årDataGridViewTextBoxColumn.DataPropertyName = "år";
            this.årDataGridViewTextBoxColumn.HeaderText = "År";
            this.årDataGridViewTextBoxColumn.Name = "årDataGridViewTextBoxColumn";
            this.årDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // eksternkildeDataGridViewTextBoxColumn
            // 
            this.eksternkildeDataGridViewTextBoxColumn.DataPropertyName = "ekstern_kilde";
            this.eksternkildeDataGridViewTextBoxColumn.HeaderText = "Ekstern kilde";
            this.eksternkildeDataGridViewTextBoxColumn.Name = "eksternkildeDataGridViewTextBoxColumn";
            this.eksternkildeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // beskrivelseDataGridViewTextBoxColumn
            // 
            this.beskrivelseDataGridViewTextBoxColumn.DataPropertyName = "beskrivelse";
            this.beskrivelseDataGridViewTextBoxColumn.HeaderText = "Beskrivelse";
            this.beskrivelseDataGridViewTextBoxColumn.Name = "beskrivelseDataGridViewTextBoxColumn";
            this.beskrivelseDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oprettesafDataGridViewTextBoxColumn
            // 
            this.oprettesafDataGridViewTextBoxColumn.DataPropertyName = "oprettes_af";
            this.oprettesafDataGridViewTextBoxColumn.HeaderText = "Oprettet af";
            this.oprettesafDataGridViewTextBoxColumn.Name = "oprettesafDataGridViewTextBoxColumn";
            this.oprettesafDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // oprettetdatoDataGridViewTextBoxColumn
            // 
            this.oprettetdatoDataGridViewTextBoxColumn.DataPropertyName = "oprettet_dato";
            this.oprettetdatoDataGridViewTextBoxColumn.HeaderText = "Oprettet dato";
            this.oprettetdatoDataGridViewTextBoxColumn.Name = "oprettetdatoDataGridViewTextBoxColumn";
            this.oprettetdatoDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kildestiDataGridViewTextBoxColumn
            // 
            this.kildestiDataGridViewTextBoxColumn.DataPropertyName = "kilde_sti";
            this.kildestiDataGridViewTextBoxColumn.HeaderText = "Kilde sti";
            this.kildestiDataGridViewTextBoxColumn.Name = "kildestiDataGridViewTextBoxColumn";
            this.kildestiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // xmldocsBindingSource
            // 
            this.xmldocsBindingSource.DataSource = typeof(bjViewer.xmldocs);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 382);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmldocsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn refnrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn virksomhedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dokumenttypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn årDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eksternkildeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn beskrivelseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oprettesafDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oprettetdatoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kildestiDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource xmldocsBindingSource;

    }
}

