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
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.lvwLog = new System.Windows.Forms.ListView();
            this.columnHeaderDato = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAktivitet = new System.Windows.Forms.ColumnHeader();
            this.Bynavn = new System.Windows.Forms.TextBox();
            this.Postnr = new System.Windows.Forms.TextBox();
            this.Email = new System.Windows.Forms.TextBox();
            this.Telefon = new System.Windows.Forms.TextBox();
            this.Adresse = new System.Windows.Forms.TextBox();
            this.Kaldenavn = new System.Windows.Forms.TextBox();
            this.Navn = new System.Windows.Forms.TextBox();
            this.FodtDato = new System.Windows.Forms.TextBox();
            this.Kon = new System.Windows.Forms.TextBox();
            this.Knr = new System.Windows.Forms.TextBox();
            this.Nr = new System.Windows.Forms.TextBox();
            this.label_Postnr_By = new System.Windows.Forms.Label();
            this.label_Email = new System.Windows.Forms.Label();
            this.label_Telefon = new System.Windows.Forms.Label();
            this.label_Adresse = new System.Windows.Forms.Label();
            this.label_Kaldenavn = new System.Windows.Forms.Label();
            this.label_Navn = new System.Windows.Forms.Label();
            this.label_FodtDato = new System.Windows.Forms.Label();
            this.label_Kon = new System.Windows.Forms.Label();
            this.label_Knr = new System.Windows.Forms.Label();
            this.Overskrift = new System.Windows.Forms.Label();
            this.label_Nr = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kartotekBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsMedlem)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.knrDataGridViewTextBoxColumn,
            this.konDataGridViewTextBoxColumn,
            this.fodtDatoDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.kartotekBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(523, 600);
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
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.bindingNavigator1);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 600);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.BindingSource = this.kartotekBindingSource;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.bindingNavigatorAddNewItem});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 575);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(273, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdSave);
            this.panel1.Controls.Add(this.cmdUpdate);
            this.panel1.Controls.Add(this.lvwLog);
            this.panel1.Controls.Add(this.Bynavn);
            this.panel1.Controls.Add(this.Postnr);
            this.panel1.Controls.Add(this.Email);
            this.panel1.Controls.Add(this.Telefon);
            this.panel1.Controls.Add(this.Adresse);
            this.panel1.Controls.Add(this.Kaldenavn);
            this.panel1.Controls.Add(this.Navn);
            this.panel1.Controls.Add(this.FodtDato);
            this.panel1.Controls.Add(this.Kon);
            this.panel1.Controls.Add(this.Knr);
            this.panel1.Controls.Add(this.Nr);
            this.panel1.Controls.Add(this.label_Postnr_By);
            this.panel1.Controls.Add(this.label_Email);
            this.panel1.Controls.Add(this.label_Telefon);
            this.panel1.Controls.Add(this.label_Adresse);
            this.panel1.Controls.Add(this.label_Kaldenavn);
            this.panel1.Controls.Add(this.label_Navn);
            this.panel1.Controls.Add(this.label_FodtDato);
            this.panel1.Controls.Add(this.label_Kon);
            this.panel1.Controls.Add(this.label_Knr);
            this.panel1.Controls.Add(this.Overskrift);
            this.panel1.Controls.Add(this.label_Nr);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 436);
            this.panel1.TabIndex = 0;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(91, 376);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(55, 21);
            this.cmdSave.TabIndex = 0;
            this.cmdSave.TabStop = false;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(15, 376);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(55, 21);
            this.cmdUpdate.TabIndex = 0;
            this.cmdUpdate.TabStop = false;
            this.cmdUpdate.Text = "Opdater";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // lvwLog
            // 
            this.lvwLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDato,
            this.columnHeaderAktivitet});
            this.lvwLog.Location = new System.Drawing.Point(15, 271);
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
            this.columnHeaderDato.Text = "Dato";
            this.columnHeaderDato.Width = 66;
            // 
            // columnHeaderAktivitet
            // 
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
            // Knr
            // 
            this.Knr.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.kartotekBindingSource, "Knr", true));
            this.Knr.Location = new System.Drawing.Point(91, 194);
            this.Knr.Name = "Knr";
            this.Knr.ReadOnly = true;
            this.Knr.Size = new System.Drawing.Size(39, 20);
            this.Knr.TabIndex = 8;
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
            // label_Knr
            // 
            this.label_Knr.AutoSize = true;
            this.label_Knr.Location = new System.Drawing.Point(12, 197);
            this.label_Knr.Name = "label_Knr";
            this.label_Knr.Size = new System.Drawing.Size(54, 13);
            this.label_Knr.TabIndex = 0;
            this.label_Knr.Text = "Kommune";
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
            this.Overskrift.Text = "SSSSS";
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
            // knrDataGridViewTextBoxColumn
            // 
            this.knrDataGridViewTextBoxColumn.DataPropertyName = "Knr";
            this.knrDataGridViewTextBoxColumn.HeaderText = "Knr";
            this.knrDataGridViewTextBoxColumn.Name = "knrDataGridViewTextBoxColumn";
            this.knrDataGridViewTextBoxColumn.Visible = false;
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
            // FrmMedlemmer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmMedlemmerSize;
            this.Controls.Add(this.splitContainer1);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource kartotekBindingSource;
        private dsMedlem dsMedlem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.TextBox Knr;
        private System.Windows.Forms.Label label_Email;
        private System.Windows.Forms.Label label_FodtDato;
        private System.Windows.Forms.Label label_Kon;
        private System.Windows.Forms.Label label_Knr;
        private System.Windows.Forms.Label Overskrift;
        private System.Windows.Forms.ListView lvwLog;
        private System.Windows.Forms.ColumnHeader columnHeaderDato;
        private System.Windows.Forms.ColumnHeader columnHeaderAktivitet;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Button cmdSave;
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


    }
}