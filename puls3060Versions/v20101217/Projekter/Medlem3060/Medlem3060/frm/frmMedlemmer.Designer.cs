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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMedlemmer));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.kartotekBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsMedlem = new nsPuls3060.dsMedlem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.cmdSave_I_Record = new System.Windows.Forms.Button();
            this.cmdCancel_I_Record = new System.Windows.Forms.Button();
            this.I_Bynavn = new System.Windows.Forms.TextBox();
            this.I_Postnr = new System.Windows.Forms.MaskedTextBox();
            this.I_Email = new System.Windows.Forms.TextBox();
            this.I_Bank = new System.Windows.Forms.MaskedTextBox();
            this.I_Telefon = new System.Windows.Forms.MaskedTextBox();
            this.I_Adresse = new System.Windows.Forms.TextBox();
            this.I_Kaldenavn = new System.Windows.Forms.TextBox();
            this.I_Navn = new System.Windows.Forms.TextBox();
            this.I_Kon = new System.Windows.Forms.MaskedTextBox();
            this.I_Nr = new System.Windows.Forms.TextBox();
            this.label_I_label_Postnr_By = new System.Windows.Forms.Label();
            this.label_I_Email = new System.Windows.Forms.Label();
            this.label_I_Bank = new System.Windows.Forms.Label();
            this.label_I_Telefon = new System.Windows.Forms.Label();
            this.label_I_Adresse = new System.Windows.Forms.Label();
            this.label_I_Kaldenavn = new System.Windows.Forms.Label();
            this.label_I_Navn = new System.Windows.Forms.Label();
            this.label_I_Indmeldelsesdato = new System.Windows.Forms.Label();
            this.label_I_FodtDato = new System.Windows.Forms.Label();
            this.label_I_Kon = new System.Windows.Forms.Label();
            this.I_Overskrift = new System.Windows.Forms.Label();
            this.label_I_Nr = new System.Windows.Forms.Label();
            this.panelUpdate = new System.Windows.Forms.Panel();
            this.U_NyAktivitet = new System.Windows.Forms.ComboBox();
            this.cmdSave_U_Record = new System.Windows.Forms.Button();
            this.cmdCancel_U_Record = new System.Windows.Forms.Button();
            this.U_Bynavn = new System.Windows.Forms.TextBox();
            this.U_Postnr = new System.Windows.Forms.MaskedTextBox();
            this.U_Email = new System.Windows.Forms.TextBox();
            this.U_Bank = new System.Windows.Forms.MaskedTextBox();
            this.U_Telefon = new System.Windows.Forms.MaskedTextBox();
            this.U_Adresse = new System.Windows.Forms.TextBox();
            this.U_Kaldenavn = new System.Windows.Forms.TextBox();
            this.U_Navn = new System.Windows.Forms.TextBox();
            this.U_Kon = new System.Windows.Forms.MaskedTextBox();
            this.U_Nr = new System.Windows.Forms.TextBox();
            this.label_U_label_Postnr_By = new System.Windows.Forms.Label();
            this.label_U_Email = new System.Windows.Forms.Label();
            this.label_U_Bank = new System.Windows.Forms.Label();
            this.label_U_Telefon = new System.Windows.Forms.Label();
            this.label_U_Adresse = new System.Windows.Forms.Label();
            this.label_U_Kaldenavn = new System.Windows.Forms.Label();
            this.label_U_Navn = new System.Windows.Forms.Label();
            this.label_U_NyAktivitet = new System.Windows.Forms.Label();
            this.label_U_FodtDato = new System.Windows.Forms.Label();
            this.label_U_Kon = new System.Windows.Forms.Label();
            this.U_Overskrift = new System.Windows.Forms.Label();
            this.label_U_Nr = new System.Windows.Forms.Label();
            this.panelDisplay = new System.Windows.Forms.Panel();
            this.lvwLog = new System.Windows.Forms.ListView();
            this.columnHeaderDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAktivitet = new System.Windows.Forms.ColumnHeader();
            this.Bynavn = new System.Windows.Forms.TextBox();
            this.Postnr = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.Bank = new System.Windows.Forms.TextBox();
            this.Telefon = new System.Windows.Forms.TextBox();
            this.Adresse = new System.Windows.Forms.TextBox();
            this.Kaldenavn = new System.Windows.Forms.TextBox();
            this.Navn = new System.Windows.Forms.TextBox();
            this.FodtDato = new System.Windows.Forms.TextBox();
            this.Kon = new System.Windows.Forms.TextBox();
            this.Nr = new System.Windows.Forms.TextBox();
            this.label_Postnr_By = new System.Windows.Forms.Label();
            this.label_Email = new System.Windows.Forms.Label();
            this.label_Bank = new System.Windows.Forms.Label();
            this.label_Telefon = new System.Windows.Forms.Label();
            this.label_Adresse = new System.Windows.Forms.Label();
            this.label_Kaldenavn = new System.Windows.Forms.Label();
            this.label_Navn = new System.Windows.Forms.Label();
            this.label_FodtDato = new System.Windows.Forms.Label();
            this.label_Kon = new System.Windows.Forms.Label();
            this.Overskrift = new System.Windows.Forms.Label();
            this.label_Nr = new System.Windows.Forms.Label();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddUpdateItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripImportMedlem = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.I_DT_FodtDato = new ProjectMentor.Windows.Controls.NullableDateTimePicker();
            this.I_DT_Indmeldelsesdato = new ProjectMentor.Windows.Controls.NullableDateTimePicker();
            this.U_DT_NyAktivitetDato = new ProjectMentor.Windows.Controls.NullableDateTimePicker();
            this.U_DT_FodtDato = new ProjectMentor.Windows.Controls.NullableDateTimePicker();
            this.nrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.navnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kaldenavnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postnrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bynavnDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telefonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.konDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fodtDatoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kartotekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMedlem)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelAdd.SuspendLayout();
            this.panelUpdate.SuspendLayout();
            this.panelDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
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
            this.nrDataGridViewTextBoxColumn,
            this.navnDataGridViewTextBoxColumn,
            this.kaldenavnDataGridViewTextBoxColumn,
            this.adresseDataGridViewTextBoxColumn,
            this.postnrDataGridViewTextBoxColumn,
            this.bynavnDataGridViewTextBoxColumn,
            this.telefonDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.konDataGridViewTextBoxColumn,
            this.fodtDatoDataGridViewTextBoxColumn,
            this.bankDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.kartotekBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(516, 576);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
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
            // splitContainer1
            // 
            this.splitContainer1.DataBindings.Add(new System.Windows.Forms.Binding("SplitterDistance", global::nsPuls3060.Properties.Settings.Default, "frmMedlemmerSplitteDist", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelAdd);
            this.splitContainer1.Panel1.Controls.Add(this.panelUpdate);
            this.splitContainer1.Panel1.Controls.Add(this.panelDisplay);
            this.splitContainer1.Panel1MinSize = 280;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 576);
            this.splitContainer1.SplitterDistance = global::nsPuls3060.Properties.Settings.Default.frmMedlemmerSplitteDist;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.I_DT_FodtDato);
            this.panelAdd.Controls.Add(this.I_DT_Indmeldelsesdato);
            this.panelAdd.Controls.Add(this.cmdSave_I_Record);
            this.panelAdd.Controls.Add(this.cmdCancel_I_Record);
            this.panelAdd.Controls.Add(this.I_Bynavn);
            this.panelAdd.Controls.Add(this.I_Postnr);
            this.panelAdd.Controls.Add(this.I_Email);
            this.panelAdd.Controls.Add(this.I_Bank);
            this.panelAdd.Controls.Add(this.I_Telefon);
            this.panelAdd.Controls.Add(this.I_Adresse);
            this.panelAdd.Controls.Add(this.I_Kaldenavn);
            this.panelAdd.Controls.Add(this.I_Navn);
            this.panelAdd.Controls.Add(this.I_Kon);
            this.panelAdd.Controls.Add(this.I_Nr);
            this.panelAdd.Controls.Add(this.label_I_label_Postnr_By);
            this.panelAdd.Controls.Add(this.label_I_Email);
            this.panelAdd.Controls.Add(this.label_I_Bank);
            this.panelAdd.Controls.Add(this.label_I_Telefon);
            this.panelAdd.Controls.Add(this.label_I_Adresse);
            this.panelAdd.Controls.Add(this.label_I_Kaldenavn);
            this.panelAdd.Controls.Add(this.label_I_Navn);
            this.panelAdd.Controls.Add(this.label_I_Indmeldelsesdato);
            this.panelAdd.Controls.Add(this.label_I_FodtDato);
            this.panelAdd.Controls.Add(this.label_I_Kon);
            this.panelAdd.Controls.Add(this.I_Overskrift);
            this.panelAdd.Controls.Add(this.label_I_Nr);
            this.panelAdd.Location = new System.Drawing.Point(7, 399);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(258, 360);
            this.panelAdd.TabIndex = 1;
            this.panelAdd.Visible = false;
            // 
            // cmdSave_I_Record
            // 
            this.cmdSave_I_Record.Location = new System.Drawing.Point(91, 327);
            this.cmdSave_I_Record.Name = "cmdSave_I_Record";
            this.cmdSave_I_Record.Size = new System.Drawing.Size(55, 21);
            this.cmdSave_I_Record.TabIndex = 0;
            this.cmdSave_I_Record.TabStop = false;
            this.cmdSave_I_Record.Text = "Gem";
            this.cmdSave_I_Record.UseVisualStyleBackColor = true;
            this.cmdSave_I_Record.Click += new System.EventHandler(this.cmdSave_I_Record_Click);
            // 
            // cmdCancel_I_Record
            // 
            this.cmdCancel_I_Record.Location = new System.Drawing.Point(15, 327);
            this.cmdCancel_I_Record.Name = "cmdCancel_I_Record";
            this.cmdCancel_I_Record.Size = new System.Drawing.Size(55, 21);
            this.cmdCancel_I_Record.TabIndex = 0;
            this.cmdCancel_I_Record.TabStop = false;
            this.cmdCancel_I_Record.Text = "Fortryd";
            this.cmdCancel_I_Record.UseVisualStyleBackColor = true;
            this.cmdCancel_I_Record.Click += new System.EventHandler(this.cmdCancel_I_Record_Click);
            // 
            // I_Bynavn
            // 
            this.I_Bynavn.Location = new System.Drawing.Point(136, 122);
            this.I_Bynavn.Name = "I_Bynavn";
            this.I_Bynavn.Size = new System.Drawing.Size(112, 20);
            this.I_Bynavn.TabIndex = 5;
            // 
            // I_Postnr
            // 
            this.I_Postnr.Location = new System.Drawing.Point(91, 122);
            this.I_Postnr.Mask = "0000";
            this.I_Postnr.Name = "I_Postnr";
            this.I_Postnr.Size = new System.Drawing.Size(39, 20);
            this.I_Postnr.TabIndex = 4;
            this.I_Postnr.Leave += new System.EventHandler(this.I_Postnr_Leave);
            // 
            // I_Email
            // 
            this.I_Email.Location = new System.Drawing.Point(91, 170);
            this.I_Email.Name = "I_Email";
            this.I_Email.Size = new System.Drawing.Size(157, 20);
            this.I_Email.TabIndex = 7;
            // 
            // I_Bank
            // 
            this.I_Bank.Location = new System.Drawing.Point(91, 265);
            this.I_Bank.Mask = "0000 0000000000";
            this.I_Bank.Name = "I_Bank";
            this.I_Bank.Size = new System.Drawing.Size(95, 20);
            this.I_Bank.TabIndex = 11;
            // 
            // I_Telefon
            // 
            this.I_Telefon.Location = new System.Drawing.Point(91, 146);
            this.I_Telefon.Mask = "0000 0000";
            this.I_Telefon.Name = "I_Telefon";
            this.I_Telefon.Size = new System.Drawing.Size(78, 20);
            this.I_Telefon.TabIndex = 6;
            // 
            // I_Adresse
            // 
            this.I_Adresse.Location = new System.Drawing.Point(91, 98);
            this.I_Adresse.Name = "I_Adresse";
            this.I_Adresse.Size = new System.Drawing.Size(157, 20);
            this.I_Adresse.TabIndex = 3;
            // 
            // I_Kaldenavn
            // 
            this.I_Kaldenavn.Location = new System.Drawing.Point(91, 74);
            this.I_Kaldenavn.Name = "I_Kaldenavn";
            this.I_Kaldenavn.Size = new System.Drawing.Size(157, 20);
            this.I_Kaldenavn.TabIndex = 2;
            // 
            // I_Navn
            // 
            this.I_Navn.Location = new System.Drawing.Point(91, 50);
            this.I_Navn.Name = "I_Navn";
            this.I_Navn.Size = new System.Drawing.Size(157, 20);
            this.I_Navn.TabIndex = 1;
            this.I_Navn.Leave += new System.EventHandler(this.I_Navn_Leave);
            // 
            // I_Kon
            // 
            this.I_Kon.Location = new System.Drawing.Point(91, 218);
            this.I_Kon.Mask = ">L";
            this.I_Kon.Name = "I_Kon";
            this.I_Kon.Size = new System.Drawing.Size(39, 20);
            this.I_Kon.TabIndex = 9;
            // 
            // I_Nr
            // 
            this.I_Nr.Location = new System.Drawing.Point(91, 26);
            this.I_Nr.Name = "I_Nr";
            this.I_Nr.ReadOnly = true;
            this.I_Nr.Size = new System.Drawing.Size(39, 20);
            this.I_Nr.TabIndex = 0;
            this.I_Nr.TabStop = false;
            // 
            // label_I_label_Postnr_By
            // 
            this.label_I_label_Postnr_By.AutoSize = true;
            this.label_I_label_Postnr_By.Location = new System.Drawing.Point(12, 125);
            this.label_I_label_Postnr_By.Name = "label_I_label_Postnr_By";
            this.label_I_label_Postnr_By.Size = new System.Drawing.Size(61, 13);
            this.label_I_label_Postnr_By.TabIndex = 0;
            this.label_I_label_Postnr_By.Text = "Postnr + By";
            // 
            // label_I_Email
            // 
            this.label_I_Email.AutoSize = true;
            this.label_I_Email.Location = new System.Drawing.Point(12, 173);
            this.label_I_Email.Name = "label_I_Email";
            this.label_I_Email.Size = new System.Drawing.Size(32, 13);
            this.label_I_Email.TabIndex = 0;
            this.label_I_Email.Text = "Email";
            // 
            // label_I_Bank
            // 
            this.label_I_Bank.AutoSize = true;
            this.label_I_Bank.Location = new System.Drawing.Point(12, 268);
            this.label_I_Bank.Name = "label_I_Bank";
            this.label_I_Bank.Size = new System.Drawing.Size(32, 13);
            this.label_I_Bank.TabIndex = 0;
            this.label_I_Bank.Text = "Bank";
            // 
            // label_I_Telefon
            // 
            this.label_I_Telefon.AutoSize = true;
            this.label_I_Telefon.Location = new System.Drawing.Point(12, 149);
            this.label_I_Telefon.Name = "label_I_Telefon";
            this.label_I_Telefon.Size = new System.Drawing.Size(43, 13);
            this.label_I_Telefon.TabIndex = 0;
            this.label_I_Telefon.Text = "Telefon";
            // 
            // label_I_Adresse
            // 
            this.label_I_Adresse.AutoSize = true;
            this.label_I_Adresse.Location = new System.Drawing.Point(12, 101);
            this.label_I_Adresse.Name = "label_I_Adresse";
            this.label_I_Adresse.Size = new System.Drawing.Size(45, 13);
            this.label_I_Adresse.TabIndex = 0;
            this.label_I_Adresse.Text = "Adresse";
            // 
            // label_I_Kaldenavn
            // 
            this.label_I_Kaldenavn.AutoSize = true;
            this.label_I_Kaldenavn.Location = new System.Drawing.Point(12, 77);
            this.label_I_Kaldenavn.Name = "label_I_Kaldenavn";
            this.label_I_Kaldenavn.Size = new System.Drawing.Size(58, 13);
            this.label_I_Kaldenavn.TabIndex = 0;
            this.label_I_Kaldenavn.Text = "Kaldenavn";
            // 
            // label_I_Navn
            // 
            this.label_I_Navn.AutoSize = true;
            this.label_I_Navn.Location = new System.Drawing.Point(12, 53);
            this.label_I_Navn.Name = "label_I_Navn";
            this.label_I_Navn.Size = new System.Drawing.Size(33, 13);
            this.label_I_Navn.TabIndex = 0;
            this.label_I_Navn.Text = "Navn";
            // 
            // label_I_Indmeldelsesdato
            // 
            this.label_I_Indmeldelsesdato.AutoSize = true;
            this.label_I_Indmeldelsesdato.Location = new System.Drawing.Point(12, 293);
            this.label_I_Indmeldelsesdato.Name = "label_I_Indmeldelsesdato";
            this.label_I_Indmeldelsesdato.Size = new System.Drawing.Size(59, 13);
            this.label_I_Indmeldelsesdato.TabIndex = 0;
            this.label_I_Indmeldelsesdato.Text = "Medlem fra";
            // 
            // label_I_FodtDato
            // 
            this.label_I_FodtDato.AutoSize = true;
            this.label_I_FodtDato.Location = new System.Drawing.Point(12, 245);
            this.label_I_FodtDato.Name = "label_I_FodtDato";
            this.label_I_FodtDato.Size = new System.Drawing.Size(52, 13);
            this.label_I_FodtDato.TabIndex = 0;
            this.label_I_FodtDato.Text = "Født dato";
            // 
            // label_I_Kon
            // 
            this.label_I_Kon.AutoSize = true;
            this.label_I_Kon.Location = new System.Drawing.Point(12, 221);
            this.label_I_Kon.Name = "label_I_Kon";
            this.label_I_Kon.Size = new System.Drawing.Size(26, 13);
            this.label_I_Kon.TabIndex = 0;
            this.label_I_Kon.Text = "Køn";
            // 
            // I_Overskrift
            // 
            this.I_Overskrift.Dock = System.Windows.Forms.DockStyle.Top;
            this.I_Overskrift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.I_Overskrift.ForeColor = System.Drawing.SystemColors.ControlText;
            this.I_Overskrift.Location = new System.Drawing.Point(0, 0);
            this.I_Overskrift.Name = "I_Overskrift";
            this.I_Overskrift.Size = new System.Drawing.Size(258, 16);
            this.I_Overskrift.TabIndex = 17;
            this.I_Overskrift.Text = "Nyt medlem";
            this.I_Overskrift.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_I_Nr
            // 
            this.label_I_Nr.AutoSize = true;
            this.label_I_Nr.Location = new System.Drawing.Point(12, 29);
            this.label_I_Nr.Name = "label_I_Nr";
            this.label_I_Nr.Size = new System.Drawing.Size(18, 13);
            this.label_I_Nr.TabIndex = 0;
            this.label_I_Nr.Text = "Nr";
            // 
            // panelUpdate
            // 
            this.panelUpdate.Controls.Add(this.U_NyAktivitet);
            this.panelUpdate.Controls.Add(this.U_DT_NyAktivitetDato);
            this.panelUpdate.Controls.Add(this.U_DT_FodtDato);
            this.panelUpdate.Controls.Add(this.cmdSave_U_Record);
            this.panelUpdate.Controls.Add(this.cmdCancel_U_Record);
            this.panelUpdate.Controls.Add(this.U_Bynavn);
            this.panelUpdate.Controls.Add(this.U_Postnr);
            this.panelUpdate.Controls.Add(this.U_Email);
            this.panelUpdate.Controls.Add(this.U_Bank);
            this.panelUpdate.Controls.Add(this.U_Telefon);
            this.panelUpdate.Controls.Add(this.U_Adresse);
            this.panelUpdate.Controls.Add(this.U_Kaldenavn);
            this.panelUpdate.Controls.Add(this.U_Navn);
            this.panelUpdate.Controls.Add(this.U_Kon);
            this.panelUpdate.Controls.Add(this.U_Nr);
            this.panelUpdate.Controls.Add(this.label_U_label_Postnr_By);
            this.panelUpdate.Controls.Add(this.label_U_Email);
            this.panelUpdate.Controls.Add(this.label_U_Bank);
            this.panelUpdate.Controls.Add(this.label_U_Telefon);
            this.panelUpdate.Controls.Add(this.label_U_Adresse);
            this.panelUpdate.Controls.Add(this.label_U_Kaldenavn);
            this.panelUpdate.Controls.Add(this.label_U_Navn);
            this.panelUpdate.Controls.Add(this.label_U_NyAktivitet);
            this.panelUpdate.Controls.Add(this.label_U_FodtDato);
            this.panelUpdate.Controls.Add(this.label_U_Kon);
            this.panelUpdate.Controls.Add(this.U_Overskrift);
            this.panelUpdate.Controls.Add(this.label_U_Nr);
            this.panelUpdate.Location = new System.Drawing.Point(7, 757);
            this.panelUpdate.Name = "panelUpdate";
            this.panelUpdate.Size = new System.Drawing.Size(258, 385);
            this.panelUpdate.TabIndex = 18;
            this.panelUpdate.Visible = false;
            // 
            // U_NyAktivitet
            // 
            this.U_NyAktivitet.FormattingEnabled = true;
            this.U_NyAktivitet.Location = new System.Drawing.Point(16, 309);
            this.U_NyAktivitet.Name = "U_NyAktivitet";
            this.U_NyAktivitet.Size = new System.Drawing.Size(148, 21);
            this.U_NyAktivitet.TabIndex = 19;
            // 
            // cmdSave_U_Record
            // 
            this.cmdSave_U_Record.Location = new System.Drawing.Point(91, 347);
            this.cmdSave_U_Record.Name = "cmdSave_U_Record";
            this.cmdSave_U_Record.Size = new System.Drawing.Size(55, 21);
            this.cmdSave_U_Record.TabIndex = 0;
            this.cmdSave_U_Record.TabStop = false;
            this.cmdSave_U_Record.Text = "Gem";
            this.cmdSave_U_Record.UseVisualStyleBackColor = true;
            this.cmdSave_U_Record.Click += new System.EventHandler(this.cmdSave_U_Record_Click);
            // 
            // cmdCancel_U_Record
            // 
            this.cmdCancel_U_Record.Location = new System.Drawing.Point(15, 347);
            this.cmdCancel_U_Record.Name = "cmdCancel_U_Record";
            this.cmdCancel_U_Record.Size = new System.Drawing.Size(55, 21);
            this.cmdCancel_U_Record.TabIndex = 0;
            this.cmdCancel_U_Record.TabStop = false;
            this.cmdCancel_U_Record.Text = "Fortryd";
            this.cmdCancel_U_Record.UseVisualStyleBackColor = true;
            this.cmdCancel_U_Record.Click += new System.EventHandler(this.cmdCancel_U_Record_Click);
            // 
            // U_Bynavn
            // 
            this.U_Bynavn.Location = new System.Drawing.Point(136, 122);
            this.U_Bynavn.Name = "U_Bynavn";
            this.U_Bynavn.Size = new System.Drawing.Size(112, 20);
            this.U_Bynavn.TabIndex = 5;
            // 
            // U_Postnr
            // 
            this.U_Postnr.Location = new System.Drawing.Point(91, 122);
            this.U_Postnr.Mask = "0000";
            this.U_Postnr.Name = "U_Postnr";
            this.U_Postnr.Size = new System.Drawing.Size(39, 20);
            this.U_Postnr.TabIndex = 4;
            this.U_Postnr.Leave += new System.EventHandler(this.U_Postnr_Leave);
            // 
            // U_Email
            // 
            this.U_Email.Location = new System.Drawing.Point(91, 170);
            this.U_Email.Name = "U_Email";
            this.U_Email.Size = new System.Drawing.Size(157, 20);
            this.U_Email.TabIndex = 7;
            // 
            // U_Bank
            // 
            this.U_Bank.Location = new System.Drawing.Point(91, 266);
            this.U_Bank.Mask = "0000 0000000000";
            this.U_Bank.Name = "U_Bank";
            this.U_Bank.Size = new System.Drawing.Size(95, 20);
            this.U_Bank.TabIndex = 11;
            // 
            // U_Telefon
            // 
            this.U_Telefon.Location = new System.Drawing.Point(91, 146);
            this.U_Telefon.Mask = "0000 0000";
            this.U_Telefon.Name = "U_Telefon";
            this.U_Telefon.Size = new System.Drawing.Size(78, 20);
            this.U_Telefon.TabIndex = 6;
            // 
            // U_Adresse
            // 
            this.U_Adresse.Location = new System.Drawing.Point(91, 98);
            this.U_Adresse.Name = "U_Adresse";
            this.U_Adresse.Size = new System.Drawing.Size(157, 20);
            this.U_Adresse.TabIndex = 3;
            // 
            // U_Kaldenavn
            // 
            this.U_Kaldenavn.Location = new System.Drawing.Point(91, 74);
            this.U_Kaldenavn.Name = "U_Kaldenavn";
            this.U_Kaldenavn.Size = new System.Drawing.Size(157, 20);
            this.U_Kaldenavn.TabIndex = 2;
            // 
            // U_Navn
            // 
            this.U_Navn.Location = new System.Drawing.Point(91, 50);
            this.U_Navn.Name = "U_Navn";
            this.U_Navn.Size = new System.Drawing.Size(157, 20);
            this.U_Navn.TabIndex = 1;
            this.U_Navn.Leave += new System.EventHandler(this.U_Navn_Leave);
            // 
            // U_Kon
            // 
            this.U_Kon.Location = new System.Drawing.Point(91, 218);
            this.U_Kon.Mask = ">L";
            this.U_Kon.Name = "U_Kon";
            this.U_Kon.Size = new System.Drawing.Size(39, 20);
            this.U_Kon.TabIndex = 9;
            // 
            // U_Nr
            // 
            this.U_Nr.Location = new System.Drawing.Point(91, 26);
            this.U_Nr.Name = "U_Nr";
            this.U_Nr.ReadOnly = true;
            this.U_Nr.Size = new System.Drawing.Size(39, 20);
            this.U_Nr.TabIndex = 0;
            this.U_Nr.TabStop = false;
            // 
            // label_U_label_Postnr_By
            // 
            this.label_U_label_Postnr_By.AutoSize = true;
            this.label_U_label_Postnr_By.Location = new System.Drawing.Point(12, 125);
            this.label_U_label_Postnr_By.Name = "label_U_label_Postnr_By";
            this.label_U_label_Postnr_By.Size = new System.Drawing.Size(61, 13);
            this.label_U_label_Postnr_By.TabIndex = 0;
            this.label_U_label_Postnr_By.Text = "Postnr + By";
            // 
            // label_U_Email
            // 
            this.label_U_Email.AutoSize = true;
            this.label_U_Email.Location = new System.Drawing.Point(12, 173);
            this.label_U_Email.Name = "label_U_Email";
            this.label_U_Email.Size = new System.Drawing.Size(32, 13);
            this.label_U_Email.TabIndex = 0;
            this.label_U_Email.Text = "Email";
            // 
            // label_U_Bank
            // 
            this.label_U_Bank.AutoSize = true;
            this.label_U_Bank.Location = new System.Drawing.Point(12, 269);
            this.label_U_Bank.Name = "label_U_Bank";
            this.label_U_Bank.Size = new System.Drawing.Size(32, 13);
            this.label_U_Bank.TabIndex = 0;
            this.label_U_Bank.Text = "Bank";
            // 
            // label_U_Telefon
            // 
            this.label_U_Telefon.AutoSize = true;
            this.label_U_Telefon.Location = new System.Drawing.Point(12, 149);
            this.label_U_Telefon.Name = "label_U_Telefon";
            this.label_U_Telefon.Size = new System.Drawing.Size(43, 13);
            this.label_U_Telefon.TabIndex = 0;
            this.label_U_Telefon.Text = "Telefon";
            // 
            // label_U_Adresse
            // 
            this.label_U_Adresse.AutoSize = true;
            this.label_U_Adresse.Location = new System.Drawing.Point(12, 101);
            this.label_U_Adresse.Name = "label_U_Adresse";
            this.label_U_Adresse.Size = new System.Drawing.Size(45, 13);
            this.label_U_Adresse.TabIndex = 0;
            this.label_U_Adresse.Text = "Adresse";
            // 
            // label_U_Kaldenavn
            // 
            this.label_U_Kaldenavn.AutoSize = true;
            this.label_U_Kaldenavn.Location = new System.Drawing.Point(12, 77);
            this.label_U_Kaldenavn.Name = "label_U_Kaldenavn";
            this.label_U_Kaldenavn.Size = new System.Drawing.Size(58, 13);
            this.label_U_Kaldenavn.TabIndex = 0;
            this.label_U_Kaldenavn.Text = "Kaldenavn";
            // 
            // label_U_Navn
            // 
            this.label_U_Navn.AutoSize = true;
            this.label_U_Navn.Location = new System.Drawing.Point(12, 53);
            this.label_U_Navn.Name = "label_U_Navn";
            this.label_U_Navn.Size = new System.Drawing.Size(33, 13);
            this.label_U_Navn.TabIndex = 0;
            this.label_U_Navn.Text = "Navn";
            // 
            // label_U_NyAktivitet
            // 
            this.label_U_NyAktivitet.AutoSize = true;
            this.label_U_NyAktivitet.Location = new System.Drawing.Point(12, 289);
            this.label_U_NyAktivitet.Name = "label_U_NyAktivitet";
            this.label_U_NyAktivitet.Size = new System.Drawing.Size(60, 13);
            this.label_U_NyAktivitet.TabIndex = 0;
            this.label_U_NyAktivitet.Text = "Ny aktivitet";
            // 
            // label_U_FodtDato
            // 
            this.label_U_FodtDato.AutoSize = true;
            this.label_U_FodtDato.Location = new System.Drawing.Point(12, 245);
            this.label_U_FodtDato.Name = "label_U_FodtDato";
            this.label_U_FodtDato.Size = new System.Drawing.Size(52, 13);
            this.label_U_FodtDato.TabIndex = 0;
            this.label_U_FodtDato.Text = "Født dato";
            // 
            // label_U_Kon
            // 
            this.label_U_Kon.AutoSize = true;
            this.label_U_Kon.Location = new System.Drawing.Point(12, 221);
            this.label_U_Kon.Name = "label_U_Kon";
            this.label_U_Kon.Size = new System.Drawing.Size(26, 13);
            this.label_U_Kon.TabIndex = 0;
            this.label_U_Kon.Text = "Køn";
            // 
            // U_Overskrift
            // 
            this.U_Overskrift.Dock = System.Windows.Forms.DockStyle.Top;
            this.U_Overskrift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.U_Overskrift.ForeColor = System.Drawing.SystemColors.ControlText;
            this.U_Overskrift.Location = new System.Drawing.Point(0, 0);
            this.U_Overskrift.Name = "U_Overskrift";
            this.U_Overskrift.Size = new System.Drawing.Size(258, 16);
            this.U_Overskrift.TabIndex = 17;
            this.U_Overskrift.Text = "Opdater medlem";
            this.U_Overskrift.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_U_Nr
            // 
            this.label_U_Nr.AutoSize = true;
            this.label_U_Nr.Location = new System.Drawing.Point(12, 29);
            this.label_U_Nr.Name = "label_U_Nr";
            this.label_U_Nr.Size = new System.Drawing.Size(18, 13);
            this.label_U_Nr.TabIndex = 0;
            this.label_U_Nr.Text = "Nr";
            // 
            // panelDisplay
            // 
            this.panelDisplay.Controls.Add(this.lvwLog);
            this.panelDisplay.Controls.Add(this.Bynavn);
            this.panelDisplay.Controls.Add(this.Postnr);
            this.panelDisplay.Controls.Add(this.Email);
            this.panelDisplay.Controls.Add(this.Bank);
            this.panelDisplay.Controls.Add(this.Telefon);
            this.panelDisplay.Controls.Add(this.Adresse);
            this.panelDisplay.Controls.Add(this.Kaldenavn);
            this.panelDisplay.Controls.Add(this.Navn);
            this.panelDisplay.Controls.Add(this.FodtDato);
            this.panelDisplay.Controls.Add(this.Kon);
            this.panelDisplay.Controls.Add(this.Nr);
            this.panelDisplay.Controls.Add(this.label_Postnr_By);
            this.panelDisplay.Controls.Add(this.label_Email);
            this.panelDisplay.Controls.Add(this.label_Bank);
            this.panelDisplay.Controls.Add(this.label_Telefon);
            this.panelDisplay.Controls.Add(this.label_Adresse);
            this.panelDisplay.Controls.Add(this.label_Kaldenavn);
            this.panelDisplay.Controls.Add(this.label_Navn);
            this.panelDisplay.Controls.Add(this.label_FodtDato);
            this.panelDisplay.Controls.Add(this.label_Kon);
            this.panelDisplay.Controls.Add(this.Overskrift);
            this.panelDisplay.Controls.Add(this.label_Nr);
            this.panelDisplay.Location = new System.Drawing.Point(7, 7);
            this.panelDisplay.Name = "panelDisplay";
            this.panelDisplay.Size = new System.Drawing.Size(258, 394);
            this.panelDisplay.TabIndex = 0;
            // 
            // lvwLog
            // 
            this.lvwLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDato,
            this.columnHeaderAktivitet});
            this.lvwLog.Location = new System.Drawing.Point(15, 294);
            this.lvwLog.Name = "lvwLog";
            this.lvwLog.Size = new System.Drawing.Size(233, 87);
            this.lvwLog.TabIndex = 0;
            this.lvwLog.TabStop = false;
            this.lvwLog.UseCompatibleStateImageBehavior = false;
            this.lvwLog.View = System.Windows.Forms.View.Details;
            this.lvwLog.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwLog_ColumnClick);
            // 
            // columnHeaderDato
            // 
            this.columnHeaderDato.DisplayIndex = 1;
            this.columnHeaderDato.Text = "Dato";
            this.columnHeaderDato.Width = 66;
            // 
            // columnHeaderAktivitet
            // 
            this.columnHeaderAktivitet.DisplayIndex = 0;
            this.columnHeaderAktivitet.Text = "Aktivitet";
            this.columnHeaderAktivitet.Width = 154;
            // 
            // Bynavn
            // 
            this.Bynavn.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Bynavn", true));
            this.Bynavn.Location = new System.Drawing.Point(136, 122);
            this.Bynavn.Name = "Bynavn";
            this.Bynavn.ReadOnly = true;
            this.Bynavn.Size = new System.Drawing.Size(112, 20);
            this.Bynavn.TabIndex = 5;
            // 
            // Postnr
            // 
            this.Postnr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Postnr", true));
            this.Postnr.Location = new System.Drawing.Point(91, 122);
            this.Postnr.Name = "Postnr";
            this.Postnr.ReadOnly = true;
            this.Postnr.Size = new System.Drawing.Size(39, 20);
            this.Postnr.TabIndex = 4;
            // 
            // Email
            // 
            this.Email.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Email", true));
            this.Email.Location = new System.Drawing.Point(91, 170);
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Size = new System.Drawing.Size(157, 20);
            this.Email.TabIndex = 7;
            // 
            // Bank
            // 
            this.Bank.AcceptsReturn = true;
            this.Bank.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Bank", true));
            this.Bank.Location = new System.Drawing.Point(91, 268);
            this.Bank.Name = "Bank";
            this.Bank.ReadOnly = true;
            this.Bank.Size = new System.Drawing.Size(95, 20);
            this.Bank.TabIndex = 6;
            // 
            // Telefon
            // 
            this.Telefon.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Telefon", true));
            this.Telefon.Location = new System.Drawing.Point(91, 146);
            this.Telefon.Name = "Telefon";
            this.Telefon.ReadOnly = true;
            this.Telefon.Size = new System.Drawing.Size(78, 20);
            this.Telefon.TabIndex = 6;
            // 
            // Adresse
            // 
            this.Adresse.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Adresse", true));
            this.Adresse.Location = new System.Drawing.Point(91, 98);
            this.Adresse.Name = "Adresse";
            this.Adresse.ReadOnly = true;
            this.Adresse.Size = new System.Drawing.Size(157, 20);
            this.Adresse.TabIndex = 3;
            // 
            // Kaldenavn
            // 
            this.Kaldenavn.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Kaldenavn", true));
            this.Kaldenavn.Location = new System.Drawing.Point(91, 74);
            this.Kaldenavn.Name = "Kaldenavn";
            this.Kaldenavn.ReadOnly = true;
            this.Kaldenavn.Size = new System.Drawing.Size(157, 20);
            this.Kaldenavn.TabIndex = 2;
            // 
            // Navn
            // 
            this.Navn.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Navn", true));
            this.Navn.Location = new System.Drawing.Point(91, 50);
            this.Navn.Name = "Navn";
            this.Navn.ReadOnly = true;
            this.Navn.Size = new System.Drawing.Size(157, 20);
            this.Navn.TabIndex = 1;
            this.Navn.TextChanged += new System.EventHandler(this.Navn_TextChanged);
            // 
            // FodtDato
            // 
            this.FodtDato.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "FodtDato", true));
            this.FodtDato.Location = new System.Drawing.Point(91, 242);
            this.FodtDato.Name = "FodtDato";
            this.FodtDato.ReadOnly = true;
            this.FodtDato.Size = new System.Drawing.Size(78, 20);
            this.FodtDato.TabIndex = 10;
            // 
            // Kon
            // 
            this.Kon.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Kon", true));
            this.Kon.Location = new System.Drawing.Point(91, 218);
            this.Kon.Name = "Kon";
            this.Kon.ReadOnly = true;
            this.Kon.Size = new System.Drawing.Size(39, 20);
            this.Kon.TabIndex = 9;
            // 
            // Nr
            // 
            this.Nr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Nr", true));
            this.Nr.Location = new System.Drawing.Point(91, 26);
            this.Nr.Name = "Nr";
            this.Nr.ReadOnly = true;
            this.Nr.Size = new System.Drawing.Size(39, 20);
            this.Nr.TabIndex = 0;
            this.Nr.TabStop = false;
            this.Nr.TextChanged += new System.EventHandler(this.Nr_TextChanged);
            // 
            // label_Postnr_By
            // 
            this.label_Postnr_By.AutoSize = true;
            this.label_Postnr_By.Location = new System.Drawing.Point(12, 125);
            this.label_Postnr_By.Name = "label_Postnr_By";
            this.label_Postnr_By.Size = new System.Drawing.Size(61, 13);
            this.label_Postnr_By.TabIndex = 0;
            this.label_Postnr_By.Text = "Postnr + By";
            // 
            // label_Email
            // 
            this.label_Email.AutoSize = true;
            this.label_Email.Location = new System.Drawing.Point(12, 173);
            this.label_Email.Name = "label_Email";
            this.label_Email.Size = new System.Drawing.Size(32, 13);
            this.label_Email.TabIndex = 0;
            this.label_Email.Text = "Email";
            // 
            // label_Bank
            // 
            this.label_Bank.AutoSize = true;
            this.label_Bank.Location = new System.Drawing.Point(12, 271);
            this.label_Bank.Name = "label_Bank";
            this.label_Bank.Size = new System.Drawing.Size(32, 13);
            this.label_Bank.TabIndex = 0;
            this.label_Bank.Text = "Bank";
            // 
            // label_Telefon
            // 
            this.label_Telefon.AutoSize = true;
            this.label_Telefon.Location = new System.Drawing.Point(12, 149);
            this.label_Telefon.Name = "label_Telefon";
            this.label_Telefon.Size = new System.Drawing.Size(43, 13);
            this.label_Telefon.TabIndex = 0;
            this.label_Telefon.Text = "Telefon";
            // 
            // label_Adresse
            // 
            this.label_Adresse.AutoSize = true;
            this.label_Adresse.Location = new System.Drawing.Point(12, 101);
            this.label_Adresse.Name = "label_Adresse";
            this.label_Adresse.Size = new System.Drawing.Size(45, 13);
            this.label_Adresse.TabIndex = 0;
            this.label_Adresse.Text = "Adresse";
            // 
            // label_Kaldenavn
            // 
            this.label_Kaldenavn.AutoSize = true;
            this.label_Kaldenavn.Location = new System.Drawing.Point(12, 77);
            this.label_Kaldenavn.Name = "label_Kaldenavn";
            this.label_Kaldenavn.Size = new System.Drawing.Size(58, 13);
            this.label_Kaldenavn.TabIndex = 0;
            this.label_Kaldenavn.Text = "Kaldenavn";
            // 
            // label_Navn
            // 
            this.label_Navn.AutoSize = true;
            this.label_Navn.Location = new System.Drawing.Point(12, 53);
            this.label_Navn.Name = "label_Navn";
            this.label_Navn.Size = new System.Drawing.Size(33, 13);
            this.label_Navn.TabIndex = 0;
            this.label_Navn.Text = "Navn";
            // 
            // label_FodtDato
            // 
            this.label_FodtDato.AutoSize = true;
            this.label_FodtDato.Location = new System.Drawing.Point(12, 245);
            this.label_FodtDato.Name = "label_FodtDato";
            this.label_FodtDato.Size = new System.Drawing.Size(52, 13);
            this.label_FodtDato.TabIndex = 0;
            this.label_FodtDato.Text = "Født dato";
            // 
            // label_Kon
            // 
            this.label_Kon.AutoSize = true;
            this.label_Kon.Location = new System.Drawing.Point(12, 221);
            this.label_Kon.Name = "label_Kon";
            this.label_Kon.Size = new System.Drawing.Size(26, 13);
            this.label_Kon.TabIndex = 0;
            this.label_Kon.Text = "Køn";
            // 
            // Overskrift
            // 
            this.Overskrift.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Navn", true));
            this.Overskrift.Dock = System.Windows.Forms.DockStyle.Top;
            this.Overskrift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Overskrift.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Overskrift.Location = new System.Drawing.Point(0, 0);
            this.Overskrift.Name = "Overskrift";
            this.Overskrift.Size = new System.Drawing.Size(258, 16);
            this.Overskrift.TabIndex = 17;
            this.Overskrift.Text = "Vis Medlem";
            this.Overskrift.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_Nr
            // 
            this.label_Nr.AutoSize = true;
            this.label_Nr.Location = new System.Drawing.Point(12, 29);
            this.label_Nr.Name = "label_Nr";
            this.label_Nr.Size = new System.Drawing.Size(18, 13);
            this.label_Nr.TabIndex = 0;
            this.label_Nr.Text = "Nr";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.BindingSource = this.kartotekBindingSource;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.None;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddUpdateItem,
            this.bindingNavigatorAddNewItem,
            this.toolStripImportMedlem});
            this.bindingNavigator1.Location = new System.Drawing.Point(1, -1);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(309, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddUpdateItem
            // 
            this.bindingNavigatorAddUpdateItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddUpdateItem.Image = global::nsPuls3060.Properties.Resources.Upd;
            this.bindingNavigatorAddUpdateItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorAddUpdateItem.Name = "bindingNavigatorAddUpdateItem";
            this.bindingNavigatorAddUpdateItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddUpdateItem.Text = "Update";
            this.bindingNavigatorAddUpdateItem.Click += new System.EventHandler(this.bindingNavigatorUpdateItem_Click);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = global::nsPuls3060.Properties.Resources.Add;
            this.bindingNavigatorAddNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add New";
            this.bindingNavigatorAddNewItem.ToolTipText = "Add New";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // toolStripImportMedlem
            // 
            this.toolStripImportMedlem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripImportMedlem.Image = ((System.Drawing.Image)(resources.GetObject("toolStripImportMedlem.Image")));
            this.toolStripImportMedlem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripImportMedlem.Name = "toolStripImportMedlem";
            this.toolStripImportMedlem.Size = new System.Drawing.Size(53, 22);
            this.toolStripImportMedlem.Text = "Importer";
            this.toolStripImportMedlem.ToolTipText = "Importer medlemmer";
            this.toolStripImportMedlem.Click += new System.EventHandler(this.toolStripImportMedlem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // I_DT_FodtDato
            // 
            this.I_DT_FodtDato.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.I_DT_FodtDato.Location = new System.Drawing.Point(91, 241);
            this.I_DT_FodtDato.Name = "I_DT_FodtDato";
            this.I_DT_FodtDato.Size = new System.Drawing.Size(78, 20);
            this.I_DT_FodtDato.TabIndex = 10;
            this.I_DT_FodtDato.Value = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            this.I_DT_FodtDato.Enter += new System.EventHandler(this.I_DT_FodtDato_Enter);
            // 
            // I_DT_Indmeldelsesdato
            // 
            this.I_DT_Indmeldelsesdato.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.I_DT_Indmeldelsesdato.Location = new System.Drawing.Point(91, 289);
            this.I_DT_Indmeldelsesdato.Name = "I_DT_Indmeldelsesdato";
            this.I_DT_Indmeldelsesdato.Size = new System.Drawing.Size(78, 20);
            this.I_DT_Indmeldelsesdato.TabIndex = 12;
            this.I_DT_Indmeldelsesdato.Value = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            // 
            // U_DT_NyAktivitetDato
            // 
            this.U_DT_NyAktivitetDato.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.U_DT_NyAktivitetDato.Location = new System.Drawing.Point(170, 308);
            this.U_DT_NyAktivitetDato.Name = "U_DT_NyAktivitetDato";
            this.U_DT_NyAktivitetDato.Size = new System.Drawing.Size(78, 20);
            this.U_DT_NyAktivitetDato.TabIndex = 18;
            this.U_DT_NyAktivitetDato.Value = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            // 
            // U_DT_FodtDato
            // 
            this.U_DT_FodtDato.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.U_DT_FodtDato.Location = new System.Drawing.Point(91, 242);
            this.U_DT_FodtDato.Name = "U_DT_FodtDato";
            this.U_DT_FodtDato.Size = new System.Drawing.Size(78, 20);
            this.U_DT_FodtDato.TabIndex = 10;
            this.U_DT_FodtDato.Value = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            this.U_DT_FodtDato.Enter += new System.EventHandler(this.U_DT_FodtDato_Enter);
            // 
            // nrDataGridViewTextBoxColumn
            // 
            this.nrDataGridViewTextBoxColumn.DataPropertyName = "Nr";
            this.nrDataGridViewTextBoxColumn.HeaderText = "Nr";
            this.nrDataGridViewTextBoxColumn.Name = "nrDataGridViewTextBoxColumn";
            this.nrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // navnDataGridViewTextBoxColumn
            // 
            this.navnDataGridViewTextBoxColumn.DataPropertyName = "Navn";
            this.navnDataGridViewTextBoxColumn.HeaderText = "Navn";
            this.navnDataGridViewTextBoxColumn.Name = "navnDataGridViewTextBoxColumn";
            this.navnDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kaldenavnDataGridViewTextBoxColumn
            // 
            this.kaldenavnDataGridViewTextBoxColumn.DataPropertyName = "Kaldenavn";
            this.kaldenavnDataGridViewTextBoxColumn.HeaderText = "Kaldenavn";
            this.kaldenavnDataGridViewTextBoxColumn.Name = "kaldenavnDataGridViewTextBoxColumn";
            this.kaldenavnDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // adresseDataGridViewTextBoxColumn
            // 
            this.adresseDataGridViewTextBoxColumn.DataPropertyName = "Adresse";
            this.adresseDataGridViewTextBoxColumn.HeaderText = "Adresse";
            this.adresseDataGridViewTextBoxColumn.Name = "adresseDataGridViewTextBoxColumn";
            this.adresseDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // postnrDataGridViewTextBoxColumn
            // 
            this.postnrDataGridViewTextBoxColumn.DataPropertyName = "Postnr";
            this.postnrDataGridViewTextBoxColumn.HeaderText = "Postnr";
            this.postnrDataGridViewTextBoxColumn.Name = "postnrDataGridViewTextBoxColumn";
            this.postnrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bynavnDataGridViewTextBoxColumn
            // 
            this.bynavnDataGridViewTextBoxColumn.DataPropertyName = "Bynavn";
            this.bynavnDataGridViewTextBoxColumn.HeaderText = "By";
            this.bynavnDataGridViewTextBoxColumn.Name = "bynavnDataGridViewTextBoxColumn";
            this.bynavnDataGridViewTextBoxColumn.Visible = false;
            // 
            // telefonDataGridViewTextBoxColumn
            // 
            this.telefonDataGridViewTextBoxColumn.DataPropertyName = "Telefon";
            this.telefonDataGridViewTextBoxColumn.HeaderText = "Telefon";
            this.telefonDataGridViewTextBoxColumn.Name = "telefonDataGridViewTextBoxColumn";
            this.telefonDataGridViewTextBoxColumn.Visible = false;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.Visible = false;
            // 
            // konDataGridViewTextBoxColumn
            // 
            this.konDataGridViewTextBoxColumn.DataPropertyName = "Kon";
            this.konDataGridViewTextBoxColumn.HeaderText = "Køn";
            this.konDataGridViewTextBoxColumn.Name = "konDataGridViewTextBoxColumn";
            this.konDataGridViewTextBoxColumn.Visible = false;
            // 
            // fodtDatoDataGridViewTextBoxColumn
            // 
            this.fodtDatoDataGridViewTextBoxColumn.DataPropertyName = "FodtDato";
            this.fodtDatoDataGridViewTextBoxColumn.HeaderText = "Født Dato";
            this.fodtDatoDataGridViewTextBoxColumn.Name = "fodtDatoDataGridViewTextBoxColumn";
            this.fodtDatoDataGridViewTextBoxColumn.Visible = false;
            // 
            // bankDataGridViewTextBoxColumn
            // 
            this.bankDataGridViewTextBoxColumn.DataPropertyName = "Bank";
            this.bankDataGridViewTextBoxColumn.HeaderText = "Bank";
            this.bankDataGridViewTextBoxColumn.Name = "bankDataGridViewTextBoxColumn";
            this.bankDataGridViewTextBoxColumn.Visible = false;
            // 
            // FrmMedlemmer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmMedlemmerSize;
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmMedlemmerPoint", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmMedlemmerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmMedlemmerPoint;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMedlemmer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Medlemmer";
            this.Load += new System.EventHandler(this.frmMedlemmer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMedlemmer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kartotekBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMedlem)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            this.panelUpdate.ResumeLayout(false);
            this.panelUpdate.PerformLayout();
            this.panelDisplay.ResumeLayout(false);
            this.panelDisplay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource kartotekBindingSource;
        private dsMedlem dsMedlem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelDisplay;
        private System.Windows.Forms.TextBox Nr;
        private System.Windows.Forms.Label label_Nr;
        private System.Windows.Forms.TextBox Adresse;
        private System.Windows.Forms.TextBox Kaldenavn;
        private System.Windows.Forms.TextBox Navn;
        private System.Windows.Forms.Label label_Adresse;
        private System.Windows.Forms.Label label_Kaldenavn;
        private System.Windows.Forms.Label label_Navn;
        private System.Windows.Forms.TextBox Bynavn;
        private System.Windows.Forms.TextBox Postnr;
        private System.Windows.Forms.TextBox Telefon;
        private System.Windows.Forms.Label label_Postnr_By;
        private System.Windows.Forms.Label label_Telefon;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox FodtDato;
        private System.Windows.Forms.TextBox Kon;
        private System.Windows.Forms.Label label_Email;
        private System.Windows.Forms.Label label_FodtDato;
        private System.Windows.Forms.Label label_Kon;
        private System.Windows.Forms.Label Overskrift;
        private System.Windows.Forms.ListView lvwLog;
        private System.Windows.Forms.ColumnHeader columnHeaderDato;
        private System.Windows.Forms.ColumnHeader columnHeaderAktivitet;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Button cmdSave_I_Record;
        private System.Windows.Forms.Button cmdCancel_I_Record;
        private System.Windows.Forms.TextBox I_Bynavn;
        private System.Windows.Forms.MaskedTextBox I_Postnr;
        private System.Windows.Forms.TextBox I_Email;
        private System.Windows.Forms.MaskedTextBox I_Telefon;
        private System.Windows.Forms.TextBox I_Adresse;
        private System.Windows.Forms.TextBox I_Kaldenavn;
        private System.Windows.Forms.TextBox I_Navn;
        private System.Windows.Forms.MaskedTextBox I_Kon;
        private System.Windows.Forms.TextBox I_Nr;
        private System.Windows.Forms.Label label_I_label_Postnr_By;
        private System.Windows.Forms.Label label_I_Email;
        private System.Windows.Forms.Label label_I_Telefon;
        private System.Windows.Forms.Label label_I_Adresse;
        private System.Windows.Forms.Label label_I_Kaldenavn;
        private System.Windows.Forms.Label label_I_Navn;
        private System.Windows.Forms.Label label_I_FodtDato;
        private System.Windows.Forms.Label label_I_Kon;
        private System.Windows.Forms.Label I_Overskrift;
        private System.Windows.Forms.Label label_I_Nr;
        private System.Windows.Forms.Label label_I_Indmeldelsesdato;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.Panel panelUpdate;
        private System.Windows.Forms.Button cmdSave_U_Record;
        private System.Windows.Forms.Button cmdCancel_U_Record;
        private System.Windows.Forms.TextBox U_Bynavn;
        private System.Windows.Forms.MaskedTextBox U_Postnr;
        private System.Windows.Forms.TextBox U_Email;
        private System.Windows.Forms.MaskedTextBox U_Telefon;
        private System.Windows.Forms.TextBox U_Adresse;
        private System.Windows.Forms.TextBox U_Kaldenavn;
        private System.Windows.Forms.TextBox U_Navn;
        private System.Windows.Forms.MaskedTextBox U_Kon;
        private System.Windows.Forms.TextBox U_Nr;
        private System.Windows.Forms.Label label_U_label_Postnr_By;
        private System.Windows.Forms.Label label_U_Email;
        private System.Windows.Forms.Label label_U_Telefon;
        private System.Windows.Forms.Label label_U_Adresse;
        private System.Windows.Forms.Label label_U_Kaldenavn;
        private System.Windows.Forms.Label label_U_Navn;
        private System.Windows.Forms.Label label_U_NyAktivitet;
        private System.Windows.Forms.Label label_U_FodtDato;
        private System.Windows.Forms.Label label_U_Kon;
        private System.Windows.Forms.Label U_Overskrift;
        private System.Windows.Forms.Label label_U_Nr;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddUpdateItem;
        private ProjectMentor.Windows.Controls.NullableDateTimePicker U_DT_FodtDato;
        private ProjectMentor.Windows.Controls.NullableDateTimePicker I_DT_Indmeldelsesdato;
        private ProjectMentor.Windows.Controls.NullableDateTimePicker U_DT_NyAktivitetDato;
        private ProjectMentor.Windows.Controls.NullableDateTimePicker I_DT_FodtDato;
        private System.Windows.Forms.ComboBox U_NyAktivitet;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripButton toolStripImportMedlem;
        private System.Windows.Forms.TextBox Bank;
        private System.Windows.Forms.Label label_Bank;
        private System.Windows.Forms.MaskedTextBox I_Bank;
        private System.Windows.Forms.Label label_I_Bank;
        private System.Windows.Forms.MaskedTextBox U_Bank;
        private System.Windows.Forms.Label label_U_Bank;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn navnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kaldenavnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn postnrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bynavnDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telefonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn konDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fodtDatoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankDataGridViewTextBoxColumn;


    }
}