namespace nsPuls3060
{
    partial class FrmFaktura
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
            System.Windows.Forms.Label skLabel;
            System.Windows.Forms.Label faknrLabel;
            System.Windows.Forms.Label datoLabel;
            System.Windows.Forms.Label kontoLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFaktura));
            this.tblfakBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.tblfakBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tblfakBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.tblfaklinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelKontonavn = new System.Windows.Forms.Label();
            this.checkBoxSalg = new System.Windows.Forms.CheckBox();
            this.checkBoxVareforbrug = new System.Windows.Forms.CheckBox();
            this.checkBoxKøb = new System.Windows.Forms.CheckBox();
            this.skTextBox = new System.Windows.Forms.TextBox();
            this.faknrTextBox = new System.Windows.Forms.TextBox();
            this.datoTextBox = new System.Windows.Forms.TextBox();
            this.kontoTextBox = new System.Windows.Forms.TextBox();
            this.Sogeordlabel = new System.Windows.Forms.Label();
            this.textBoxSogeord = new System.Windows.Forms.TextBox();
            this.cmdSog = new System.Windows.Forms.Button();
            this.tblfaklinDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxPid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxFakpid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxRegnskabid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxSk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxFakid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxFaklinnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxVarenr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxTekst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxKonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxMK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxAntal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxEnhed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxPris = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxRabat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxMoms = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxNettobelob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxBruttobelob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxOmkostbelob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxTblfak = new System.Windows.Forms.DataGridViewTextBoxColumn();
            skLabel = new System.Windows.Forms.Label();
            faknrLabel = new System.Windows.Forms.Label();
            datoLabel = new System.Windows.Forms.Label();
            kontoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingNavigator)).BeginInit();
            this.tblfakBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinBindingSource)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinDataGridView)).BeginInit();
            this.contextMenuLineCopyPaste.SuspendLayout();
            this.SuspendLayout();
            // 
            // skLabel
            // 
            skLabel.AutoSize = true;
            skLabel.Location = new System.Drawing.Point(19, 15);
            skLabel.Name = "skLabel";
            skLabel.Size = new System.Drawing.Size(26, 13);
            skLabel.TabIndex = 1;
            skLabel.Text = "K/S";
            // 
            // faknrLabel
            // 
            faknrLabel.AutoSize = true;
            faknrLabel.Location = new System.Drawing.Point(189, 15);
            faknrLabel.Name = "faknrLabel";
            faknrLabel.Size = new System.Drawing.Size(34, 13);
            faknrLabel.TabIndex = 5;
            faknrLabel.Text = "Faknr";
            // 
            // datoLabel
            // 
            datoLabel.AutoSize = true;
            datoLabel.Location = new System.Drawing.Point(82, 15);
            datoLabel.Name = "datoLabel";
            datoLabel.Size = new System.Drawing.Size(30, 13);
            datoLabel.TabIndex = 7;
            datoLabel.Text = "Dato";
            // 
            // kontoLabel
            // 
            kontoLabel.AutoSize = true;
            kontoLabel.Location = new System.Drawing.Point(10, 42);
            kontoLabel.Name = "kontoLabel";
            kontoLabel.Size = new System.Drawing.Size(35, 13);
            kontoLabel.TabIndex = 9;
            kontoLabel.Text = "Konto";
            // 
            // tblfakBindingNavigator
            // 
            this.tblfakBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.tblfakBindingNavigator.BindingSource = this.tblfakBindingSource;
            this.tblfakBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.tblfakBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.tblfakBindingNavigator.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblfakBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.tblfakBindingNavigatorSaveItem});
            this.tblfakBindingNavigator.Location = new System.Drawing.Point(0, 235);
            this.tblfakBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.tblfakBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.tblfakBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.tblfakBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.tblfakBindingNavigator.Name = "tblfakBindingNavigator";
            this.tblfakBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.tblfakBindingNavigator.Size = new System.Drawing.Size(825, 25);
            this.tblfakBindingNavigator.TabIndex = 0;
            this.tblfakBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // tblfakBindingSource
            // 
            this.tblfakBindingSource.DataSource = typeof(nsPuls3060.Tblfak);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
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
            // tblfakBindingNavigatorSaveItem
            // 
            this.tblfakBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tblfakBindingNavigatorSaveItem.Enabled = false;
            this.tblfakBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("tblfakBindingNavigatorSaveItem.Image")));
            this.tblfakBindingNavigatorSaveItem.Name = "tblfakBindingNavigatorSaveItem";
            this.tblfakBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.tblfakBindingNavigatorSaveItem.Text = "Save Data";
            // 
            // tblfaklinBindingSource
            // 
            this.tblfaklinBindingSource.DataMember = "Tblfaklin";
            this.tblfaklinBindingSource.DataSource = this.tblfakBindingSource;
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
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.labelKontonavn);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxSalg);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxVareforbrug);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxKøb);
            this.splitContainer1.Panel1.Controls.Add(skLabel);
            this.splitContainer1.Panel1.Controls.Add(this.skTextBox);
            this.splitContainer1.Panel1.Controls.Add(faknrLabel);
            this.splitContainer1.Panel1.Controls.Add(this.faknrTextBox);
            this.splitContainer1.Panel1.Controls.Add(datoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.datoTextBox);
            this.splitContainer1.Panel1.Controls.Add(kontoLabel);
            this.splitContainer1.Panel1.Controls.Add(this.kontoTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.Sogeordlabel);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSogeord);
            this.splitContainer1.Panel1.Controls.Add(this.cmdSog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.tblfaklinDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(825, 235);
            this.splitContainer1.SplitterDistance = 65;
            this.splitContainer1.TabIndex = 3;
            // 
            // labelKontonavn
            // 
            this.labelKontonavn.AutoSize = true;
            this.labelKontonavn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKontonavn.Location = new System.Drawing.Point(104, 39);
            this.labelKontonavn.Name = "labelKontonavn";
            this.labelKontonavn.Size = new System.Drawing.Size(11, 15);
            this.labelKontonavn.TabIndex = 17;
            this.labelKontonavn.Text = " ";
            // 
            // checkBoxSalg
            // 
            this.checkBoxSalg.AutoSize = true;
            this.checkBoxSalg.Checked = true;
            this.checkBoxSalg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSalg.Location = new System.Drawing.Point(585, 41);
            this.checkBoxSalg.Name = "checkBoxSalg";
            this.checkBoxSalg.Size = new System.Drawing.Size(47, 17);
            this.checkBoxSalg.TabIndex = 16;
            this.checkBoxSalg.Text = "Salg";
            this.checkBoxSalg.UseVisualStyleBackColor = true;
            this.checkBoxSalg.CheckedChanged += new System.EventHandler(this.checkBoxSalg_CheckedChanged);
            // 
            // checkBoxVareforbrug
            // 
            this.checkBoxVareforbrug.AutoSize = true;
            this.checkBoxVareforbrug.Location = new System.Drawing.Point(636, 15);
            this.checkBoxVareforbrug.Name = "checkBoxVareforbrug";
            this.checkBoxVareforbrug.Size = new System.Drawing.Size(81, 17);
            this.checkBoxVareforbrug.TabIndex = 15;
            this.checkBoxVareforbrug.Text = "Vareforbrug";
            this.checkBoxVareforbrug.UseVisualStyleBackColor = true;
            this.checkBoxVareforbrug.CheckedChanged += new System.EventHandler(this.checkBoxVareforbrug_CheckedChanged);
            // 
            // checkBoxKøb
            // 
            this.checkBoxKøb.AutoSize = true;
            this.checkBoxKøb.Checked = true;
            this.checkBoxKøb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxKøb.Location = new System.Drawing.Point(585, 16);
            this.checkBoxKøb.Name = "checkBoxKøb";
            this.checkBoxKøb.Size = new System.Drawing.Size(45, 17);
            this.checkBoxKøb.TabIndex = 15;
            this.checkBoxKøb.Text = "Køb";
            this.checkBoxKøb.UseVisualStyleBackColor = true;
            this.checkBoxKøb.CheckedChanged += new System.EventHandler(this.checkBoxKøb_CheckedChanged);
            // 
            // skTextBox
            // 
            this.skTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.skTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Sk", true));
            this.skTextBox.Location = new System.Drawing.Point(48, 12);
            this.skTextBox.Name = "skTextBox";
            this.skTextBox.ReadOnly = true;
            this.skTextBox.Size = new System.Drawing.Size(22, 20);
            this.skTextBox.TabIndex = 3;
            // 
            // faknrTextBox
            // 
            this.faknrTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.faknrTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Faknr", true));
            this.faknrTextBox.Location = new System.Drawing.Point(223, 12);
            this.faknrTextBox.Name = "faknrTextBox";
            this.faknrTextBox.ReadOnly = true;
            this.faknrTextBox.Size = new System.Drawing.Size(40, 20);
            this.faknrTextBox.TabIndex = 5;
            // 
            // datoTextBox
            // 
            this.datoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.datoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Dato", true));
            this.datoTextBox.Location = new System.Drawing.Point(118, 12);
            this.datoTextBox.Name = "datoTextBox";
            this.datoTextBox.ReadOnly = true;
            this.datoTextBox.Size = new System.Drawing.Size(64, 20);
            this.datoTextBox.TabIndex = 6;
            // 
            // kontoTextBox
            // 
            this.kontoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.kontoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.tblfakBindingSource, "Konto", true));
            this.kontoTextBox.Location = new System.Drawing.Point(48, 38);
            this.kontoTextBox.Name = "kontoTextBox";
            this.kontoTextBox.ReadOnly = true;
            this.kontoTextBox.Size = new System.Drawing.Size(50, 20);
            this.kontoTextBox.TabIndex = 7;
            this.kontoTextBox.TextChanged += new System.EventHandler(this.kontoTextBox_TextChanged);
            // 
            // Sogeordlabel
            // 
            this.Sogeordlabel.AutoSize = true;
            this.Sogeordlabel.Location = new System.Drawing.Point(370, 17);
            this.Sogeordlabel.Name = "Sogeordlabel";
            this.Sogeordlabel.Size = new System.Drawing.Size(47, 13);
            this.Sogeordlabel.TabIndex = 14;
            this.Sogeordlabel.Text = "Søgeord";
            // 
            // textBoxSogeord
            // 
            this.textBoxSogeord.Location = new System.Drawing.Point(419, 13);
            this.textBoxSogeord.Name = "textBoxSogeord";
            this.textBoxSogeord.Size = new System.Drawing.Size(102, 20);
            this.textBoxSogeord.TabIndex = 1;
            // 
            // cmdSog
            // 
            this.cmdSog.Location = new System.Drawing.Point(529, 12);
            this.cmdSog.Name = "cmdSog";
            this.cmdSog.Size = new System.Drawing.Size(43, 23);
            this.cmdSog.TabIndex = 2;
            this.cmdSog.Text = "Søg";
            this.cmdSog.UseVisualStyleBackColor = true;
            this.cmdSog.Click += new System.EventHandler(this.cmdSog_Click);
            // 
            // tblfaklinDataGridView
            // 
            this.tblfaklinDataGridView.AllowUserToAddRows = false;
            this.tblfaklinDataGridView.AllowUserToDeleteRows = false;
            this.tblfaklinDataGridView.AutoGenerateColumns = false;
            this.tblfaklinDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblfaklinDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxPid,
            this.dataGridViewTextBoxFakpid,
            this.dataGridViewTextBoxRegnskabid,
            this.dataGridViewTextBoxSk,
            this.dataGridViewTextBoxFakid,
            this.dataGridViewTextBoxFaklinnr,
            this.dataGridViewTextBoxVarenr,
            this.dataGridViewTextBoxTekst,
            this.dataGridViewTextBoxKonto,
            this.dataGridViewTextBoxMK,
            this.dataGridViewTextBoxAntal,
            this.dataGridViewTextBoxEnhed,
            this.dataGridViewTextBoxPris,
            this.dataGridViewTextBoxRabat,
            this.dataGridViewTextBoxMoms,
            this.dataGridViewTextBoxNettobelob,
            this.dataGridViewTextBoxBruttobelob,
            this.dataGridViewTextBoxOmkostbelob,
            this.dataGridViewTextBoxTblfak});
            this.tblfaklinDataGridView.ContextMenuStrip = this.contextMenuLineCopyPaste;
            this.tblfaklinDataGridView.DataSource = this.tblfaklinBindingSource;
            this.tblfaklinDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblfaklinDataGridView.Location = new System.Drawing.Point(0, 0);
            this.tblfaklinDataGridView.Name = "tblfaklinDataGridView";
            this.tblfaklinDataGridView.ReadOnly = true;
            this.tblfaklinDataGridView.Size = new System.Drawing.Size(825, 166);
            this.tblfaklinDataGridView.TabIndex = 99;
            this.tblfaklinDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tblfaklinDataGridView_KeyDown);
            // 
            // contextMenuLineCopyPaste
            // 
            this.contextMenuLineCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuLineCopyPaste.Name = "contextMenuLineCopyPaste";
            this.contextMenuLineCopyPaste.Size = new System.Drawing.Size(111, 26);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // dataGridViewTextBoxPid
            // 
            this.dataGridViewTextBoxPid.DataPropertyName = "Pid";
            this.dataGridViewTextBoxPid.HeaderText = "Pid";
            this.dataGridViewTextBoxPid.Name = "dataGridViewTextBoxPid";
            this.dataGridViewTextBoxPid.ReadOnly = true;
            this.dataGridViewTextBoxPid.Visible = false;
            // 
            // dataGridViewTextBoxFakpid
            // 
            this.dataGridViewTextBoxFakpid.DataPropertyName = "Fakpid";
            this.dataGridViewTextBoxFakpid.HeaderText = "Fakpid";
            this.dataGridViewTextBoxFakpid.Name = "dataGridViewTextBoxFakpid";
            this.dataGridViewTextBoxFakpid.ReadOnly = true;
            this.dataGridViewTextBoxFakpid.Visible = false;
            // 
            // dataGridViewTextBoxRegnskabid
            // 
            this.dataGridViewTextBoxRegnskabid.DataPropertyName = "Regnskabid";
            this.dataGridViewTextBoxRegnskabid.HeaderText = "Regnskabid";
            this.dataGridViewTextBoxRegnskabid.Name = "dataGridViewTextBoxRegnskabid";
            this.dataGridViewTextBoxRegnskabid.ReadOnly = true;
            this.dataGridViewTextBoxRegnskabid.Visible = false;
            // 
            // dataGridViewTextBoxSk
            // 
            this.dataGridViewTextBoxSk.DataPropertyName = "Sk";
            this.dataGridViewTextBoxSk.HeaderText = "Sk";
            this.dataGridViewTextBoxSk.Name = "dataGridViewTextBoxSk";
            this.dataGridViewTextBoxSk.ReadOnly = true;
            this.dataGridViewTextBoxSk.Visible = false;
            // 
            // dataGridViewTextBoxFakid
            // 
            this.dataGridViewTextBoxFakid.DataPropertyName = "Fakid";
            this.dataGridViewTextBoxFakid.HeaderText = "Fakid";
            this.dataGridViewTextBoxFakid.Name = "dataGridViewTextBoxFakid";
            this.dataGridViewTextBoxFakid.ReadOnly = true;
            this.dataGridViewTextBoxFakid.Visible = false;
            // 
            // dataGridViewTextBoxFaklinnr
            // 
            this.dataGridViewTextBoxFaklinnr.DataPropertyName = "Faklinnr";
            this.dataGridViewTextBoxFaklinnr.HeaderText = "Faklinnr";
            this.dataGridViewTextBoxFaklinnr.Name = "dataGridViewTextBoxFaklinnr";
            this.dataGridViewTextBoxFaklinnr.ReadOnly = true;
            this.dataGridViewTextBoxFaklinnr.Visible = false;
            // 
            // dataGridViewTextBoxVarenr
            // 
            this.dataGridViewTextBoxVarenr.DataPropertyName = "Varenr";
            this.dataGridViewTextBoxVarenr.HeaderText = "Varenr";
            this.dataGridViewTextBoxVarenr.Name = "dataGridViewTextBoxVarenr";
            this.dataGridViewTextBoxVarenr.ReadOnly = true;
            this.dataGridViewTextBoxVarenr.Width = 80;
            // 
            // dataGridViewTextBoxTekst
            // 
            this.dataGridViewTextBoxTekst.DataPropertyName = "Tekst";
            this.dataGridViewTextBoxTekst.HeaderText = "Tekst";
            this.dataGridViewTextBoxTekst.Name = "dataGridViewTextBoxTekst";
            this.dataGridViewTextBoxTekst.ReadOnly = true;
            this.dataGridViewTextBoxTekst.Width = 200;
            // 
            // dataGridViewTextBoxKonto
            // 
            this.dataGridViewTextBoxKonto.DataPropertyName = "Konto";
            this.dataGridViewTextBoxKonto.HeaderText = "Konto";
            this.dataGridViewTextBoxKonto.Name = "dataGridViewTextBoxKonto";
            this.dataGridViewTextBoxKonto.ReadOnly = true;
            this.dataGridViewTextBoxKonto.Width = 60;
            // 
            // dataGridViewTextBoxMK
            // 
            this.dataGridViewTextBoxMK.DataPropertyName = "Momskode";
            this.dataGridViewTextBoxMK.HeaderText = "MK";
            this.dataGridViewTextBoxMK.Name = "dataGridViewTextBoxMK";
            this.dataGridViewTextBoxMK.ReadOnly = true;
            this.dataGridViewTextBoxMK.Width = 40;
            // 
            // dataGridViewTextBoxAntal
            // 
            this.dataGridViewTextBoxAntal.DataPropertyName = "Antal";
            this.dataGridViewTextBoxAntal.HeaderText = "Antal";
            this.dataGridViewTextBoxAntal.Name = "dataGridViewTextBoxAntal";
            this.dataGridViewTextBoxAntal.ReadOnly = true;
            this.dataGridViewTextBoxAntal.Width = 40;
            // 
            // dataGridViewTextBoxEnhed
            // 
            this.dataGridViewTextBoxEnhed.DataPropertyName = "Enhed";
            this.dataGridViewTextBoxEnhed.HeaderText = "Enhed";
            this.dataGridViewTextBoxEnhed.Name = "dataGridViewTextBoxEnhed";
            this.dataGridViewTextBoxEnhed.ReadOnly = true;
            this.dataGridViewTextBoxEnhed.Width = 40;
            // 
            // dataGridViewTextBoxPris
            // 
            this.dataGridViewTextBoxPris.DataPropertyName = "Pris";
            this.dataGridViewTextBoxPris.HeaderText = "Pris";
            this.dataGridViewTextBoxPris.Name = "dataGridViewTextBoxPris";
            this.dataGridViewTextBoxPris.ReadOnly = true;
            this.dataGridViewTextBoxPris.Width = 60;
            // 
            // dataGridViewTextBoxRabat
            // 
            this.dataGridViewTextBoxRabat.DataPropertyName = "Rabat";
            this.dataGridViewTextBoxRabat.HeaderText = "Rabat";
            this.dataGridViewTextBoxRabat.Name = "dataGridViewTextBoxRabat";
            this.dataGridViewTextBoxRabat.ReadOnly = true;
            this.dataGridViewTextBoxRabat.Visible = false;
            // 
            // dataGridViewTextBoxMoms
            // 
            this.dataGridViewTextBoxMoms.DataPropertyName = "Moms";
            this.dataGridViewTextBoxMoms.HeaderText = "Moms";
            this.dataGridViewTextBoxMoms.Name = "dataGridViewTextBoxMoms";
            this.dataGridViewTextBoxMoms.ReadOnly = true;
            this.dataGridViewTextBoxMoms.Width = 60;
            // 
            // dataGridViewTextBoxNettobelob
            // 
            this.dataGridViewTextBoxNettobelob.DataPropertyName = "Nettobelob";
            this.dataGridViewTextBoxNettobelob.HeaderText = "Nettobelob";
            this.dataGridViewTextBoxNettobelob.Name = "dataGridViewTextBoxNettobelob";
            this.dataGridViewTextBoxNettobelob.ReadOnly = true;
            this.dataGridViewTextBoxNettobelob.Width = 70;
            // 
            // dataGridViewTextBoxBruttobelob
            // 
            this.dataGridViewTextBoxBruttobelob.DataPropertyName = "Bruttobelob";
            this.dataGridViewTextBoxBruttobelob.HeaderText = "Bruttobelob";
            this.dataGridViewTextBoxBruttobelob.Name = "dataGridViewTextBoxBruttobelob";
            this.dataGridViewTextBoxBruttobelob.ReadOnly = true;
            this.dataGridViewTextBoxBruttobelob.Width = 70;
            // 
            // dataGridViewTextBoxOmkostbelob
            // 
            this.dataGridViewTextBoxOmkostbelob.DataPropertyName = "Omkostbelob";
            this.dataGridViewTextBoxOmkostbelob.HeaderText = "Omk";
            this.dataGridViewTextBoxOmkostbelob.Name = "dataGridViewTextBoxOmkostbelob";
            this.dataGridViewTextBoxOmkostbelob.ReadOnly = true;
            this.dataGridViewTextBoxOmkostbelob.Width = 50;
            // 
            // dataGridViewTextBoxTblfak
            // 
            this.dataGridViewTextBoxTblfak.DataPropertyName = "Tblfak";
            this.dataGridViewTextBoxTblfak.HeaderText = "Tblfak";
            this.dataGridViewTextBoxTblfak.Name = "dataGridViewTextBoxTblfak";
            this.dataGridViewTextBoxTblfak.ReadOnly = true;
            this.dataGridViewTextBoxTblfak.Visible = false;
            // 
            // FrmFaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::nsPuls3060.Properties.Settings.Default.frmFakturaerSize;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tblfakBindingNavigator);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::nsPuls3060.Properties.Settings.Default, "frmFakturaerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::nsPuls3060.Properties.Settings.Default, "frmFakturaerLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::nsPuls3060.Properties.Settings.Default.frmFakturaerLocation;
            this.Name = "FrmFaktura";
            this.Text = "Faktura";
            this.Load += new System.EventHandler(this.FrmFaktura_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFaktura_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingNavigator)).EndInit();
            this.tblfakBindingNavigator.ResumeLayout(false);
            this.tblfakBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblfakBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinBindingSource)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinDataGridView)).EndInit();
            this.contextMenuLineCopyPaste.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource tblfakBindingSource;
        private System.Windows.Forms.BindingNavigator tblfakBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton tblfakBindingNavigatorSaveItem;
        private System.Windows.Forms.BindingSource tblfaklinBindingSource;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox kontoTextBox;
        private System.Windows.Forms.TextBox faknrTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        private System.Windows.Forms.DataGridView tblfaklinDataGridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuLineCopyPaste;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button cmdSog;
        private System.Windows.Forms.Label Sogeordlabel;
        private System.Windows.Forms.TextBox textBoxSogeord;
        private System.Windows.Forms.TextBox datoTextBox;
        private System.Windows.Forms.TextBox skTextBox;
        private System.Windows.Forms.CheckBox checkBoxSalg;
        private System.Windows.Forms.CheckBox checkBoxKøb;
        private System.Windows.Forms.Label labelKontonavn;
        private System.Windows.Forms.CheckBox checkBoxVareforbrug;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxPid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxFakpid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxRegnskabid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxSk;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxFakid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxFaklinnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxVarenr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxTekst;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxKonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxMK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxAntal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxEnhed;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxPris;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxRabat;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxMoms;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxNettobelob;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxBruttobelob;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxOmkostbelob;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxTblfak;
    }
}