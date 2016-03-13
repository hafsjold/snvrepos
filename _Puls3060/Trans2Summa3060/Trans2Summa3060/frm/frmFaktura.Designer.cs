namespace Trans2Summa3060
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
            Trans2Summa3060.Properties.Settings settings1 = new Trans2Summa3060.Properties.Settings();
            this.bnTblfak = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bsTblfak = new System.Windows.Forms.BindingSource(this.components);
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
            this.bsTblfaklin = new System.Windows.Forms.BindingSource(this.components);
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
            this.cmdKopier = new System.Windows.Forms.Button();
            this.cmdSog = new System.Windows.Forms.Button();
            this.tblfaklinDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuLineCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fakpidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regnskabidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.skDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fakidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.faklinnrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.varenrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tekstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kontoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momskodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.antalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enhedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rabatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.momsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nettobelobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bruttobelobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.omkostbelobDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblfakDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            skLabel = new System.Windows.Forms.Label();
            faknrLabel = new System.Windows.Forms.Label();
            datoLabel = new System.Windows.Forms.Label();
            kontoLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bnTblfak)).BeginInit();
            this.bnTblfak.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTblfak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTblfaklin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
            // bnTblfak
            // 
            this.bnTblfak.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bnTblfak.BindingSource = this.bsTblfak;
            this.bnTblfak.CountItem = this.bindingNavigatorCountItem;
            this.bnTblfak.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bnTblfak.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bnTblfak.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.bnTblfak.Location = new System.Drawing.Point(0, 235);
            this.bnTblfak.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnTblfak.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnTblfak.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnTblfak.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnTblfak.Name = "bnTblfak";
            this.bnTblfak.PositionItem = this.bindingNavigatorPositionItem;
            this.bnTblfak.Size = new System.Drawing.Size(825, 25);
            this.bnTblfak.TabIndex = 0;
            this.bnTblfak.Text = "bindingNavigator1";
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
            // bsTblfak
            // 
            this.bsTblfak.DataSource = typeof(Trans2Summa3060.tblfak);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
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
            // bsTblfaklin
            // 
            this.bsTblfaklin.DataMember = "tblfaklins";
            this.bsTblfaklin.DataSource = this.bsTblfak;
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
            this.splitContainer1.Panel1.Controls.Add(this.cmdKopier);
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
            this.skTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblfak, "Sk", true));
            this.skTextBox.Location = new System.Drawing.Point(48, 12);
            this.skTextBox.Name = "skTextBox";
            this.skTextBox.ReadOnly = true;
            this.skTextBox.Size = new System.Drawing.Size(22, 20);
            this.skTextBox.TabIndex = 3;
            // 
            // faknrTextBox
            // 
            this.faknrTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.faknrTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblfak, "Faknr", true));
            this.faknrTextBox.Location = new System.Drawing.Point(223, 12);
            this.faknrTextBox.Name = "faknrTextBox";
            this.faknrTextBox.ReadOnly = true;
            this.faknrTextBox.Size = new System.Drawing.Size(40, 20);
            this.faknrTextBox.TabIndex = 5;
            // 
            // datoTextBox
            // 
            this.datoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.datoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblfak, "Dato", true));
            this.datoTextBox.Location = new System.Drawing.Point(118, 12);
            this.datoTextBox.Name = "datoTextBox";
            this.datoTextBox.ReadOnly = true;
            this.datoTextBox.Size = new System.Drawing.Size(64, 20);
            this.datoTextBox.TabIndex = 6;
            // 
            // kontoTextBox
            // 
            this.kontoTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.kontoTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTblfak, "Konto", true));
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
            // cmdKopier
            // 
            this.cmdKopier.Location = new System.Drawing.Point(733, 11);
            this.cmdKopier.Name = "cmdKopier";
            this.cmdKopier.Size = new System.Drawing.Size(50, 23);
            this.cmdKopier.TabIndex = 2;
            this.cmdKopier.Text = "Kopier";
            this.cmdKopier.UseVisualStyleBackColor = true;
            this.cmdKopier.Click += new System.EventHandler(this.cmdKopier_Click);
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
            this.pidDataGridViewTextBoxColumn,
            this.fakpidDataGridViewTextBoxColumn,
            this.regnskabidDataGridViewTextBoxColumn,
            this.skDataGridViewTextBoxColumn,
            this.fakidDataGridViewTextBoxColumn,
            this.faklinnrDataGridViewTextBoxColumn,
            this.varenrDataGridViewTextBoxColumn,
            this.tekstDataGridViewTextBoxColumn,
            this.kontoDataGridViewTextBoxColumn,
            this.momskodeDataGridViewTextBoxColumn,
            this.antalDataGridViewTextBoxColumn,
            this.enhedDataGridViewTextBoxColumn,
            this.prisDataGridViewTextBoxColumn,
            this.rabatDataGridViewTextBoxColumn,
            this.momsDataGridViewTextBoxColumn,
            this.nettobelobDataGridViewTextBoxColumn,
            this.bruttobelobDataGridViewTextBoxColumn,
            this.omkostbelobDataGridViewTextBoxColumn,
            this.tblfakDataGridViewTextBoxColumn});
            this.tblfaklinDataGridView.ContextMenuStrip = this.contextMenuLineCopyPaste;
            this.tblfaklinDataGridView.DataSource = this.bsTblfaklin;
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
            // pidDataGridViewTextBoxColumn
            // 
            this.pidDataGridViewTextBoxColumn.DataPropertyName = "pid";
            this.pidDataGridViewTextBoxColumn.HeaderText = "pid";
            this.pidDataGridViewTextBoxColumn.Name = "pidDataGridViewTextBoxColumn";
            this.pidDataGridViewTextBoxColumn.ReadOnly = true;
            this.pidDataGridViewTextBoxColumn.Visible = false;
            // 
            // fakpidDataGridViewTextBoxColumn
            // 
            this.fakpidDataGridViewTextBoxColumn.DataPropertyName = "fakpid";
            this.fakpidDataGridViewTextBoxColumn.HeaderText = "fakpid";
            this.fakpidDataGridViewTextBoxColumn.Name = "fakpidDataGridViewTextBoxColumn";
            this.fakpidDataGridViewTextBoxColumn.ReadOnly = true;
            this.fakpidDataGridViewTextBoxColumn.Visible = false;
            // 
            // regnskabidDataGridViewTextBoxColumn
            // 
            this.regnskabidDataGridViewTextBoxColumn.DataPropertyName = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn.HeaderText = "regnskabid";
            this.regnskabidDataGridViewTextBoxColumn.Name = "regnskabidDataGridViewTextBoxColumn";
            this.regnskabidDataGridViewTextBoxColumn.ReadOnly = true;
            this.regnskabidDataGridViewTextBoxColumn.Visible = false;
            // 
            // skDataGridViewTextBoxColumn
            // 
            this.skDataGridViewTextBoxColumn.DataPropertyName = "sk";
            this.skDataGridViewTextBoxColumn.HeaderText = "sk";
            this.skDataGridViewTextBoxColumn.Name = "skDataGridViewTextBoxColumn";
            this.skDataGridViewTextBoxColumn.ReadOnly = true;
            this.skDataGridViewTextBoxColumn.Visible = false;
            // 
            // fakidDataGridViewTextBoxColumn
            // 
            this.fakidDataGridViewTextBoxColumn.DataPropertyName = "fakid";
            this.fakidDataGridViewTextBoxColumn.HeaderText = "fakid";
            this.fakidDataGridViewTextBoxColumn.Name = "fakidDataGridViewTextBoxColumn";
            this.fakidDataGridViewTextBoxColumn.ReadOnly = true;
            this.fakidDataGridViewTextBoxColumn.Visible = false;
            // 
            // faklinnrDataGridViewTextBoxColumn
            // 
            this.faklinnrDataGridViewTextBoxColumn.DataPropertyName = "faklinnr";
            this.faklinnrDataGridViewTextBoxColumn.HeaderText = "faklinnr";
            this.faklinnrDataGridViewTextBoxColumn.Name = "faklinnrDataGridViewTextBoxColumn";
            this.faklinnrDataGridViewTextBoxColumn.ReadOnly = true;
            this.faklinnrDataGridViewTextBoxColumn.Visible = false;
            // 
            // varenrDataGridViewTextBoxColumn
            // 
            this.varenrDataGridViewTextBoxColumn.DataPropertyName = "varenr";
            this.varenrDataGridViewTextBoxColumn.HeaderText = "Varenr";
            this.varenrDataGridViewTextBoxColumn.Name = "varenrDataGridViewTextBoxColumn";
            this.varenrDataGridViewTextBoxColumn.ReadOnly = true;
            this.varenrDataGridViewTextBoxColumn.Width = 80;
            // 
            // tekstDataGridViewTextBoxColumn
            // 
            this.tekstDataGridViewTextBoxColumn.DataPropertyName = "tekst";
            this.tekstDataGridViewTextBoxColumn.HeaderText = "Tekst";
            this.tekstDataGridViewTextBoxColumn.Name = "tekstDataGridViewTextBoxColumn";
            this.tekstDataGridViewTextBoxColumn.ReadOnly = true;
            this.tekstDataGridViewTextBoxColumn.Width = 200;
            // 
            // kontoDataGridViewTextBoxColumn
            // 
            this.kontoDataGridViewTextBoxColumn.DataPropertyName = "konto";
            this.kontoDataGridViewTextBoxColumn.HeaderText = "Konto";
            this.kontoDataGridViewTextBoxColumn.Name = "kontoDataGridViewTextBoxColumn";
            this.kontoDataGridViewTextBoxColumn.ReadOnly = true;
            this.kontoDataGridViewTextBoxColumn.Width = 60;
            // 
            // momskodeDataGridViewTextBoxColumn
            // 
            this.momskodeDataGridViewTextBoxColumn.DataPropertyName = "momskode";
            this.momskodeDataGridViewTextBoxColumn.HeaderText = "MK";
            this.momskodeDataGridViewTextBoxColumn.Name = "momskodeDataGridViewTextBoxColumn";
            this.momskodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.momskodeDataGridViewTextBoxColumn.Width = 40;
            // 
            // antalDataGridViewTextBoxColumn
            // 
            this.antalDataGridViewTextBoxColumn.DataPropertyName = "antal";
            this.antalDataGridViewTextBoxColumn.HeaderText = "Antal";
            this.antalDataGridViewTextBoxColumn.Name = "antalDataGridViewTextBoxColumn";
            this.antalDataGridViewTextBoxColumn.ReadOnly = true;
            this.antalDataGridViewTextBoxColumn.Width = 40;
            // 
            // enhedDataGridViewTextBoxColumn
            // 
            this.enhedDataGridViewTextBoxColumn.DataPropertyName = "enhed";
            this.enhedDataGridViewTextBoxColumn.HeaderText = "Enhed";
            this.enhedDataGridViewTextBoxColumn.Name = "enhedDataGridViewTextBoxColumn";
            this.enhedDataGridViewTextBoxColumn.ReadOnly = true;
            this.enhedDataGridViewTextBoxColumn.Width = 40;
            // 
            // prisDataGridViewTextBoxColumn
            // 
            this.prisDataGridViewTextBoxColumn.DataPropertyName = "pris";
            this.prisDataGridViewTextBoxColumn.HeaderText = "Pris";
            this.prisDataGridViewTextBoxColumn.Name = "prisDataGridViewTextBoxColumn";
            this.prisDataGridViewTextBoxColumn.ReadOnly = true;
            this.prisDataGridViewTextBoxColumn.Width = 60;
            // 
            // rabatDataGridViewTextBoxColumn
            // 
            this.rabatDataGridViewTextBoxColumn.DataPropertyName = "rabat";
            this.rabatDataGridViewTextBoxColumn.HeaderText = "rabat";
            this.rabatDataGridViewTextBoxColumn.Name = "rabatDataGridViewTextBoxColumn";
            this.rabatDataGridViewTextBoxColumn.ReadOnly = true;
            this.rabatDataGridViewTextBoxColumn.Visible = false;
            // 
            // momsDataGridViewTextBoxColumn
            // 
            this.momsDataGridViewTextBoxColumn.DataPropertyName = "moms";
            this.momsDataGridViewTextBoxColumn.HeaderText = "Moms";
            this.momsDataGridViewTextBoxColumn.Name = "momsDataGridViewTextBoxColumn";
            this.momsDataGridViewTextBoxColumn.ReadOnly = true;
            this.momsDataGridViewTextBoxColumn.Width = 60;
            // 
            // nettobelobDataGridViewTextBoxColumn
            // 
            this.nettobelobDataGridViewTextBoxColumn.DataPropertyName = "nettobelob";
            this.nettobelobDataGridViewTextBoxColumn.HeaderText = "Nettobeløb";
            this.nettobelobDataGridViewTextBoxColumn.Name = "nettobelobDataGridViewTextBoxColumn";
            this.nettobelobDataGridViewTextBoxColumn.ReadOnly = true;
            this.nettobelobDataGridViewTextBoxColumn.Width = 70;
            // 
            // bruttobelobDataGridViewTextBoxColumn
            // 
            this.bruttobelobDataGridViewTextBoxColumn.DataPropertyName = "bruttobelob";
            this.bruttobelobDataGridViewTextBoxColumn.HeaderText = "Bruttobeløb";
            this.bruttobelobDataGridViewTextBoxColumn.Name = "bruttobelobDataGridViewTextBoxColumn";
            this.bruttobelobDataGridViewTextBoxColumn.ReadOnly = true;
            this.bruttobelobDataGridViewTextBoxColumn.Width = 70;
            // 
            // omkostbelobDataGridViewTextBoxColumn
            // 
            this.omkostbelobDataGridViewTextBoxColumn.DataPropertyName = "omkostbelob";
            this.omkostbelobDataGridViewTextBoxColumn.HeaderText = "Omk";
            this.omkostbelobDataGridViewTextBoxColumn.Name = "omkostbelobDataGridViewTextBoxColumn";
            this.omkostbelobDataGridViewTextBoxColumn.ReadOnly = true;
            this.omkostbelobDataGridViewTextBoxColumn.Width = 50;
            // 
            // tblfakDataGridViewTextBoxColumn
            // 
            this.tblfakDataGridViewTextBoxColumn.DataPropertyName = "tblfak";
            this.tblfakDataGridViewTextBoxColumn.HeaderText = "tblfak";
            this.tblfakDataGridViewTextBoxColumn.Name = "tblfakDataGridViewTextBoxColumn";
            this.tblfakDataGridViewTextBoxColumn.ReadOnly = true;
            this.tblfakDataGridViewTextBoxColumn.Visible = false;
            // 
            // FrmFaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            settings1.BankafstemningSize = new System.Drawing.Size(829, 513);
            settings1.BankafstemningSplitterDistancr = 383;
            settings1.BetalingsFristiDageGamleMedlemmer = 31;
            settings1.BetalingsFristiDageNyeMedlemmer = 61;
            settings1.checkBoxMedsaldo = true;
            //settings1.dbDataTransSummaConnectionString = resources.GetString("settings1.dbDataTransSummaConnectionString");
            settings1.FrmActebisfakturaLocation = new System.Drawing.Point(0, 0);
            settings1.FrmActebisfakturaSize = new System.Drawing.Size(1081, 495);
            settings1.FrmBankkontoudtogLocation = new System.Drawing.Point(0, 0);
            settings1.frmBankkontoudtogSize = new System.Drawing.Size(393, 416);
            settings1.FrmBetalingsForslagPoint = new System.Drawing.Point(0, 0);
            settings1.FrmBetalingsForslagSize = new System.Drawing.Size(800, 600);
            settings1.frmBilagLoacation = new System.Drawing.Point(0, 0);
            settings1.frmBilagSize = new System.Drawing.Size(870, 346);
            settings1.frmBilagSplitDistance = 246;
            settings1.frmFakturaerLocation = new System.Drawing.Point(0, 0);
            settings1.frmFakturaerSize = new System.Drawing.Size(825, 260);
            settings1.frmKladderLocation = new System.Drawing.Point(0, 0);
            settings1.frmKladderSize = new System.Drawing.Size(530, 259);
            settings1.frmKontingentForslagPoint = new System.Drawing.Point(0, 0);
            settings1.frmKontingentForslagSize = new System.Drawing.Size(829, 513);
            settings1.frmKontoplanListCheckboxmedsaldo = true;
            settings1.frmKontoplanListLocation = new System.Drawing.Point(0, 0);
            settings1.frmKontoudtogLocation = new System.Drawing.Point(0, 0);
            settings1.frmKontoudtogSize = new System.Drawing.Size(324, 250);
            settings1.frmKreditorPoint = new System.Drawing.Point(10, 10);
            settings1.frmKreditorSize = new System.Drawing.Size(304, 300);
            settings1.frmKreditorState = System.Windows.Forms.FormWindowState.Normal;
            settings1.frmMainPoint = new System.Drawing.Point(100, 100);
            settings1.frmMainSize = new System.Drawing.Size(1024, 800);
            settings1.frmMedlemmerPoint = new System.Drawing.Point(100, 100);
            settings1.frmMedlemmerSize = new System.Drawing.Size(800, 600);
            settings1.frmMedlemmerSplitteDist = 280;
            settings1.frmNyeFakturaerLocation = new System.Drawing.Point(0, 0);
            settings1.frmNyeFakturaerSize = new System.Drawing.Size(803, 354);
            settings1.frmNyeKladderLocation = new System.Drawing.Point(0, 0);
            settings1.frmNyeKladderSize = new System.Drawing.Size(521, 259);
            settings1.frmPbsfilesPoint = new System.Drawing.Point(0, 0);
            settings1.frmPbsfilesSize = new System.Drawing.Size(707, 511);
            settings1.frmPbsnetdirPoint = new System.Drawing.Point(100, 100);
            settings1.frmPbsnetdirSize = new System.Drawing.Size(416, 211);
            settings1.frmRegnskabPoint = new System.Drawing.Point(0, 0);
            settings1.frmRegnskabSize = new System.Drawing.Size(531, 398);
            settings1.frmRegnskabState = System.Windows.Forms.FormWindowState.Normal;
            settings1.frmRykkerForslagClientSize = new System.Drawing.Size(829, 513);
            settings1.frmRykkerForslagPoint = new System.Drawing.Point(0, 0);
            settings1.frmTemplateLocation = new System.Drawing.Point(0, 0);
            settings1.frmTemplateSize = new System.Drawing.Size(730, 297);
            settings1.frmTemplateWinState = System.Windows.Forms.FormWindowState.Normal;
            settings1.FrmVarekontoTypeLocation = new System.Drawing.Point(0, 0);
            settings1.FrmVarekontoTypeSize = new System.Drawing.Size(302, 106);
            settings1.frmVareListLocation = new System.Drawing.Point(0, 0);
            settings1.SettingsKey = "";
            this.ClientSize = settings1.frmFakturaerSize;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bnTblfak);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", settings1, "frmFakturaerSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", settings1, "frmFakturaerLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = settings1.frmFakturaerLocation;
            this.Name = "FrmFaktura";
            this.Text = "Faktura";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFaktura_FormClosed);
            this.Load += new System.EventHandler(this.FrmFaktura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bnTblfak)).EndInit();
            this.bnTblfak.ResumeLayout(false);
            this.bnTblfak.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTblfak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTblfaklin)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblfaklinDataGridView)).EndInit();
            this.contextMenuLineCopyPaste.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsTblfak;
        private System.Windows.Forms.BindingNavigator bnTblfak;
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
        private System.Windows.Forms.BindingSource bsTblfaklin;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox kontoTextBox;
        private System.Windows.Forms.TextBox faknrTextBox;
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
        private System.Windows.Forms.Button cmdKopier;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fakpidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn regnskabidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn skDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fakidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn faklinnrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn varenrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tekstDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kontoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn momskodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn antalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enhedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rabatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn momsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nettobelobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bruttobelobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn omkostbelobDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tblfakDataGridViewTextBoxColumn;
    }
}