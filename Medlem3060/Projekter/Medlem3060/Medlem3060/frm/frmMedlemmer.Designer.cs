namespace nsPuls3060
{
    partial class FrmMedlemmer
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
            this.nrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.navnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kaldenavnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postnrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bynavnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.knrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.konDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fodtDatoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kartotekBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsMedlem = new nsPuls3060.dsMedlem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kartotekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMedlem)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nrDataGridViewTextBoxColumn,
            this.navnDataGridViewTextBoxColumn,
            this.kaldenavnDataGridViewTextBoxColumn,
            this.adresseDataGridViewTextBoxColumn,
            this.postnrDataGridViewTextBoxColumn,
            this.bynavnDataGridViewTextBoxColumn,
            this.telefonDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.knrDataGridViewTextBoxColumn,
            this.konDataGridViewTextBoxColumn,
            this.fodtDatoDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.kartotekBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(800, 600);
            this.dataGridView1.TabIndex = 0;
            // 
            // nrDataGridViewTextBoxColumn
            // 
            this.nrDataGridViewTextBoxColumn.DataPropertyName = "Nr";
            this.nrDataGridViewTextBoxColumn.HeaderText = "Nr";
            this.nrDataGridViewTextBoxColumn.Name = "nrDataGridViewTextBoxColumn";
            // 
            // navnDataGridViewTextBoxColumn
            // 
            this.navnDataGridViewTextBoxColumn.DataPropertyName = "Navn";
            this.navnDataGridViewTextBoxColumn.HeaderText = "Navn";
            this.navnDataGridViewTextBoxColumn.Name = "navnDataGridViewTextBoxColumn";
            // 
            // kaldenavnDataGridViewTextBoxColumn
            // 
            this.kaldenavnDataGridViewTextBoxColumn.DataPropertyName = "Kaldenavn";
            this.kaldenavnDataGridViewTextBoxColumn.HeaderText = "Kaldenavn";
            this.kaldenavnDataGridViewTextBoxColumn.Name = "kaldenavnDataGridViewTextBoxColumn";
            // 
            // adresseDataGridViewTextBoxColumn
            // 
            this.adresseDataGridViewTextBoxColumn.DataPropertyName = "Adresse";
            this.adresseDataGridViewTextBoxColumn.HeaderText = "Adresse";
            this.adresseDataGridViewTextBoxColumn.Name = "adresseDataGridViewTextBoxColumn";
            // 
            // postnrDataGridViewTextBoxColumn
            // 
            this.postnrDataGridViewTextBoxColumn.DataPropertyName = "Postnr";
            this.postnrDataGridViewTextBoxColumn.HeaderText = "Postnr";
            this.postnrDataGridViewTextBoxColumn.Name = "postnrDataGridViewTextBoxColumn";
            // 
            // bynavnDataGridViewTextBoxColumn
            // 
            this.bynavnDataGridViewTextBoxColumn.DataPropertyName = "Bynavn";
            this.bynavnDataGridViewTextBoxColumn.HeaderText = "Bynavn";
            this.bynavnDataGridViewTextBoxColumn.Name = "bynavnDataGridViewTextBoxColumn";
            // 
            // telefonDataGridViewTextBoxColumn
            // 
            this.telefonDataGridViewTextBoxColumn.DataPropertyName = "Telefon";
            this.telefonDataGridViewTextBoxColumn.HeaderText = "Telefon";
            this.telefonDataGridViewTextBoxColumn.Name = "telefonDataGridViewTextBoxColumn";
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            // 
            // knrDataGridViewTextBoxColumn
            // 
            this.knrDataGridViewTextBoxColumn.DataPropertyName = "Knr";
            this.knrDataGridViewTextBoxColumn.HeaderText = "Knr";
            this.knrDataGridViewTextBoxColumn.Name = "knrDataGridViewTextBoxColumn";
            // 
            // konDataGridViewTextBoxColumn
            // 
            this.konDataGridViewTextBoxColumn.DataPropertyName = "Kon";
            this.konDataGridViewTextBoxColumn.HeaderText = "Kon";
            this.konDataGridViewTextBoxColumn.Name = "konDataGridViewTextBoxColumn";
            // 
            // fodtDatoDataGridViewTextBoxColumn
            // 
            this.fodtDatoDataGridViewTextBoxColumn.DataPropertyName = "FodtDato";
            this.fodtDatoDataGridViewTextBoxColumn.HeaderText = "FodtDato";
            this.fodtDatoDataGridViewTextBoxColumn.Name = "fodtDatoDataGridViewTextBoxColumn";
            // 
            // kartotekBindingSource
            // 
            this.kartotekBindingSource.DataMember = "Kartotek";
            this.kartotekBindingSource.DataSource = this.dsMedlem;
            // 
            // dsMedlem
            // 
            this.dsMedlem.DataSetName = "dsMedlem";
            this.dsMedlem.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FrmMedlemmer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmMedlemmerSize;
            this.Controls.Add(this.dataGridView1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmMedlemmerPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmMedlemmerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmMedlemmerPoint;
            this.Name = "FrmMedlemmer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Medlemmer";
            this.Load += new System.EventHandler(this.frmMedlemmer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMedlemmer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kartotekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMedlem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn navnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kaldenavnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn postnrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bynavnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn knrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn konDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fodtDatoDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource kartotekBindingSource;
        private dsMedlem dsMedlem;


    }
}