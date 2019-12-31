namespace Trans2SummaHDC
{
    partial class FrmPassword
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
            System.Windows.Forms.Label imapPWLabel;
            System.Windows.Forms.Label imapUserLabel;
            System.Windows.Forms.Label puls3060_dkPWLabel;
            System.Windows.Forms.Label puls3060_dkUserLabel;
            System.Windows.Forms.Label uniContaCompanyIdLabel;
            System.Windows.Forms.Label uniContaPWLabel;
            System.Windows.Forms.Label uniContaUserLabel;
            this.bImapPWCheckBox = new System.Windows.Forms.CheckBox();
            this.bImapUserCheckBox = new System.Windows.Forms.CheckBox();
            this.bpuls3060_dkPWCheckBox = new System.Windows.Forms.CheckBox();
            this.bpuls3060_dkUserCheckBox = new System.Windows.Forms.CheckBox();
            this.bUniContaCompanyIdCheckBox = new System.Windows.Forms.CheckBox();
            this.bUniContaPWCheckBox = new System.Windows.Forms.CheckBox();
            this.bUniContaUserCheckBox = new System.Windows.Forms.CheckBox();
            this.imapPWTextBox = new System.Windows.Forms.TextBox();
            this.imapUserTextBox = new System.Windows.Forms.TextBox();
            this.puls3060_dkPWTextBox = new System.Windows.Forms.TextBox();
            this.puls3060_dkUserTextBox = new System.Windows.Forms.TextBox();
            this.uniContaCompanyIdTextBox = new System.Windows.Forms.TextBox();
            this.uniContaPWTextBox = new System.Windows.Forms.TextBox();
            this.uniContaUserTextBox = new System.Windows.Forms.TextBox();
            this.EncryptAppconfig = new System.Windows.Forms.Button();
            this.Opdatermarkerededata = new System.Windows.Forms.Button();
            this.dsAppData = new System.Windows.Forms.BindingSource(this.components);
            imapPWLabel = new System.Windows.Forms.Label();
            imapUserLabel = new System.Windows.Forms.Label();
            puls3060_dkPWLabel = new System.Windows.Forms.Label();
            puls3060_dkUserLabel = new System.Windows.Forms.Label();
            uniContaCompanyIdLabel = new System.Windows.Forms.Label();
            uniContaPWLabel = new System.Windows.Forms.Label();
            uniContaUserLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dsAppData)).BeginInit();
            this.SuspendLayout();
            // 
            // imapPWLabel
            // 
            imapPWLabel.AutoSize = true;
            imapPWLabel.Location = new System.Drawing.Point(12, 113);
            imapPWLabel.Name = "imapPWLabel";
            imapPWLabel.Size = new System.Drawing.Size(57, 13);
            imapPWLabel.TabIndex = 19;
            imapPWLabel.Text = "Imap_PW:";
            // 
            // imapUserLabel
            // 
            imapUserLabel.AutoSize = true;
            imapUserLabel.Location = new System.Drawing.Point(12, 87);
            imapUserLabel.Name = "imapUserLabel";
            imapUserLabel.Size = new System.Drawing.Size(61, 13);
            imapUserLabel.TabIndex = 21;
            imapUserLabel.Text = "Imap_User:";
            // 
            // puls3060_dkPWLabel
            // 
            puls3060_dkPWLabel.AutoSize = true;
            puls3060_dkPWLabel.Location = new System.Drawing.Point(12, 165);
            puls3060_dkPWLabel.Name = "puls3060_dkPWLabel";
            puls3060_dkPWLabel.Size = new System.Drawing.Size(89, 13);
            puls3060_dkPWLabel.TabIndex = 25;
            puls3060_dkPWLabel.Text = "puls3060 dk PW:";
            puls3060_dkPWLabel.Visible = false;
            // 
            // puls3060_dkUserLabel
            // 
            puls3060_dkUserLabel.AutoSize = true;
            puls3060_dkUserLabel.Location = new System.Drawing.Point(12, 139);
            puls3060_dkUserLabel.Name = "puls3060_dkUserLabel";
            puls3060_dkUserLabel.Size = new System.Drawing.Size(93, 13);
            puls3060_dkUserLabel.TabIndex = 27;
            puls3060_dkUserLabel.Text = "puls3060 dk User:";
            puls3060_dkUserLabel.Visible = false;
            // 
            // uniContaCompanyIdLabel
            // 
            uniContaCompanyIdLabel.AutoSize = true;
            uniContaCompanyIdLabel.Location = new System.Drawing.Point(12, 61);
            uniContaCompanyIdLabel.Name = "uniContaCompanyIdLabel";
            uniContaCompanyIdLabel.Size = new System.Drawing.Size(113, 13);
            uniContaCompanyIdLabel.TabIndex = 29;
            uniContaCompanyIdLabel.Text = "UniConta_CompanyId:";
            // 
            // uniContaPWLabel
            // 
            uniContaPWLabel.AutoSize = true;
            uniContaPWLabel.Location = new System.Drawing.Point(12, 35);
            uniContaPWLabel.Name = "uniContaPWLabel";
            uniContaPWLabel.Size = new System.Drawing.Size(81, 13);
            uniContaPWLabel.TabIndex = 31;
            uniContaPWLabel.Text = "UniConta _WP:";
            // 
            // uniContaUserLabel
            // 
            uniContaUserLabel.AutoSize = true;
            uniContaUserLabel.Location = new System.Drawing.Point(12, 9);
            uniContaUserLabel.Name = "uniContaUserLabel";
            uniContaUserLabel.Size = new System.Drawing.Size(82, 13);
            uniContaUserLabel.TabIndex = 33;
            uniContaUserLabel.Text = "UniConta_User:";
            // 
            // bImapPWCheckBox
            // 
            this.bImapPWCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsAppData, "bImapPW", true));
            this.bImapPWCheckBox.Location = new System.Drawing.Point(142, 112);
            this.bImapPWCheckBox.Name = "bImapPWCheckBox";
            this.bImapPWCheckBox.Size = new System.Drawing.Size(14, 14);
            this.bImapPWCheckBox.TabIndex = 4;
            this.bImapPWCheckBox.UseVisualStyleBackColor = true;
            // 
            // bImapUserCheckBox
            // 
            this.bImapUserCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsAppData, "bImapUser", true));
            this.bImapUserCheckBox.Location = new System.Drawing.Point(142, 87);
            this.bImapUserCheckBox.Name = "bImapUserCheckBox";
            this.bImapUserCheckBox.Size = new System.Drawing.Size(14, 14);
            this.bImapUserCheckBox.TabIndex = 6;
            this.bImapUserCheckBox.UseVisualStyleBackColor = true;
            // 
            // bpuls3060_dkPWCheckBox
            // 
            this.bpuls3060_dkPWCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsAppData, "bpuls3060_dkPW", true));
            this.bpuls3060_dkPWCheckBox.Location = new System.Drawing.Point(142, 164);
            this.bpuls3060_dkPWCheckBox.Name = "bpuls3060_dkPWCheckBox";
            this.bpuls3060_dkPWCheckBox.Size = new System.Drawing.Size(14, 14);
            this.bpuls3060_dkPWCheckBox.TabIndex = 8;
            this.bpuls3060_dkPWCheckBox.UseVisualStyleBackColor = true;
            this.bpuls3060_dkPWCheckBox.Visible = false;
            // 
            // bpuls3060_dkUserCheckBox
            // 
            this.bpuls3060_dkUserCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsAppData, "bpuls3060_dkUser", true));
            this.bpuls3060_dkUserCheckBox.Location = new System.Drawing.Point(142, 138);
            this.bpuls3060_dkUserCheckBox.Name = "bpuls3060_dkUserCheckBox";
            this.bpuls3060_dkUserCheckBox.Size = new System.Drawing.Size(14, 14);
            this.bpuls3060_dkUserCheckBox.TabIndex = 10;
            this.bpuls3060_dkUserCheckBox.UseVisualStyleBackColor = true;
            this.bpuls3060_dkUserCheckBox.Visible = false;
            // 
            // bUniContaCompanyIdCheckBox
            // 
            this.bUniContaCompanyIdCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsAppData, "bUniContaCompanyId", true));
            this.bUniContaCompanyIdCheckBox.Location = new System.Drawing.Point(142, 60);
            this.bUniContaCompanyIdCheckBox.Name = "bUniContaCompanyIdCheckBox";
            this.bUniContaCompanyIdCheckBox.Size = new System.Drawing.Size(14, 14);
            this.bUniContaCompanyIdCheckBox.TabIndex = 12;
            this.bUniContaCompanyIdCheckBox.UseVisualStyleBackColor = true;
            // 
            // bUniContaPWCheckBox
            // 
            this.bUniContaPWCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsAppData, "bUniContaPW", true));
            this.bUniContaPWCheckBox.Location = new System.Drawing.Point(142, 35);
            this.bUniContaPWCheckBox.Name = "bUniContaPWCheckBox";
            this.bUniContaPWCheckBox.Size = new System.Drawing.Size(14, 14);
            this.bUniContaPWCheckBox.TabIndex = 14;
            this.bUniContaPWCheckBox.UseVisualStyleBackColor = true;
            // 
            // bUniContaUserCheckBox
            // 
            this.bUniContaUserCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsAppData, "bUniContaUser", true));
            this.bUniContaUserCheckBox.Location = new System.Drawing.Point(142, 9);
            this.bUniContaUserCheckBox.Name = "bUniContaUserCheckBox";
            this.bUniContaUserCheckBox.Size = new System.Drawing.Size(14, 14);
            this.bUniContaUserCheckBox.TabIndex = 16;
            this.bUniContaUserCheckBox.UseVisualStyleBackColor = true;
            // 
            // imapPWTextBox
            // 
            this.imapPWTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsAppData, "ImapPW", true));
            this.imapPWTextBox.Location = new System.Drawing.Point(162, 110);
            this.imapPWTextBox.Name = "imapPWTextBox";
            this.imapPWTextBox.Size = new System.Drawing.Size(143, 20);
            this.imapPWTextBox.TabIndex = 20;
            // 
            // imapUserTextBox
            // 
            this.imapUserTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsAppData, "ImapUser", true));
            this.imapUserTextBox.Location = new System.Drawing.Point(162, 84);
            this.imapUserTextBox.Name = "imapUserTextBox";
            this.imapUserTextBox.Size = new System.Drawing.Size(143, 20);
            this.imapUserTextBox.TabIndex = 22;
            // 
            // puls3060_dkPWTextBox
            // 
            this.puls3060_dkPWTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsAppData, "puls3060_dkPW", true));
            this.puls3060_dkPWTextBox.Location = new System.Drawing.Point(162, 162);
            this.puls3060_dkPWTextBox.Name = "puls3060_dkPWTextBox";
            this.puls3060_dkPWTextBox.Size = new System.Drawing.Size(143, 20);
            this.puls3060_dkPWTextBox.TabIndex = 26;
            this.puls3060_dkPWTextBox.Visible = false;
            // 
            // puls3060_dkUserTextBox
            // 
            this.puls3060_dkUserTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsAppData, "puls3060_dkUser", true));
            this.puls3060_dkUserTextBox.Location = new System.Drawing.Point(162, 136);
            this.puls3060_dkUserTextBox.Name = "puls3060_dkUserTextBox";
            this.puls3060_dkUserTextBox.Size = new System.Drawing.Size(143, 20);
            this.puls3060_dkUserTextBox.TabIndex = 28;
            this.puls3060_dkUserTextBox.Visible = false;
            // 
            // uniContaCompanyIdTextBox
            // 
            this.uniContaCompanyIdTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsAppData, "UniContaCompanyId", true));
            this.uniContaCompanyIdTextBox.Location = new System.Drawing.Point(162, 58);
            this.uniContaCompanyIdTextBox.Name = "uniContaCompanyIdTextBox";
            this.uniContaCompanyIdTextBox.Size = new System.Drawing.Size(143, 20);
            this.uniContaCompanyIdTextBox.TabIndex = 30;
            // 
            // uniContaPWTextBox
            // 
            this.uniContaPWTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsAppData, "UniContaPW", true));
            this.uniContaPWTextBox.Location = new System.Drawing.Point(162, 32);
            this.uniContaPWTextBox.Name = "uniContaPWTextBox";
            this.uniContaPWTextBox.Size = new System.Drawing.Size(143, 20);
            this.uniContaPWTextBox.TabIndex = 32;
            // 
            // uniContaUserTextBox
            // 
            this.uniContaUserTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsAppData, "UniContaUser", true));
            this.uniContaUserTextBox.Location = new System.Drawing.Point(162, 6);
            this.uniContaUserTextBox.Name = "uniContaUserTextBox";
            this.uniContaUserTextBox.Size = new System.Drawing.Size(143, 20);
            this.uniContaUserTextBox.TabIndex = 34;
            // 
            // EncryptAppconfig
            // 
            this.EncryptAppconfig.Location = new System.Drawing.Point(15, 194);
            this.EncryptAppconfig.Name = "EncryptAppconfig";
            this.EncryptAppconfig.Size = new System.Drawing.Size(125, 23);
            this.EncryptAppconfig.TabIndex = 35;
            this.EncryptAppconfig.Text = "Encrypt App.config";
            this.EncryptAppconfig.UseVisualStyleBackColor = true;
            this.EncryptAppconfig.Click += new System.EventHandler(this.EncryptAppconfig_Click);
            // 
            // Opdatermarkerededata
            // 
            this.Opdatermarkerededata.Location = new System.Drawing.Point(162, 194);
            this.Opdatermarkerededata.Name = "Opdatermarkerededata";
            this.Opdatermarkerededata.Size = new System.Drawing.Size(143, 23);
            this.Opdatermarkerededata.TabIndex = 36;
            this.Opdatermarkerededata.Text = "Opdater markerede data";
            this.Opdatermarkerededata.UseVisualStyleBackColor = true;
            this.Opdatermarkerededata.Click += new System.EventHandler(this.Opdatermarkerededata_Click);
            // 
            // dsAppData
            // 
            this.dsAppData.DataSource = typeof(Trans2SummaHDC.clsAppData);
            // 
            // FrmPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 227);
            this.Controls.Add(this.Opdatermarkerededata);
            this.Controls.Add(this.EncryptAppconfig);
            this.Controls.Add(this.bImapPWCheckBox);
            this.Controls.Add(this.bImapUserCheckBox);
            this.Controls.Add(this.bpuls3060_dkPWCheckBox);
            this.Controls.Add(this.bpuls3060_dkUserCheckBox);
            this.Controls.Add(this.bUniContaCompanyIdCheckBox);
            this.Controls.Add(this.bUniContaPWCheckBox);
            this.Controls.Add(this.bUniContaUserCheckBox);
            this.Controls.Add(imapPWLabel);
            this.Controls.Add(this.imapPWTextBox);
            this.Controls.Add(imapUserLabel);
            this.Controls.Add(this.imapUserTextBox);
            this.Controls.Add(puls3060_dkPWLabel);
            this.Controls.Add(this.puls3060_dkPWTextBox);
            this.Controls.Add(puls3060_dkUserLabel);
            this.Controls.Add(this.puls3060_dkUserTextBox);
            this.Controls.Add(uniContaCompanyIdLabel);
            this.Controls.Add(this.uniContaCompanyIdTextBox);
            this.Controls.Add(uniContaPWLabel);
            this.Controls.Add(this.uniContaPWTextBox);
            this.Controls.Add(uniContaUserLabel);
            this.Controls.Add(this.uniContaUserTextBox);
            this.Name = "FrmPassword";
            this.Text = "Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPassword_FormClosing);
            this.Load += new System.EventHandler(this.frmPassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsAppData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource dsAppData;
        private System.Windows.Forms.CheckBox bImapPWCheckBox;
        private System.Windows.Forms.CheckBox bImapUserCheckBox;
        private System.Windows.Forms.CheckBox bpuls3060_dkPWCheckBox;
        private System.Windows.Forms.CheckBox bpuls3060_dkUserCheckBox;
        private System.Windows.Forms.CheckBox bUniContaCompanyIdCheckBox;
        private System.Windows.Forms.CheckBox bUniContaPWCheckBox;
        private System.Windows.Forms.CheckBox bUniContaUserCheckBox;
        private System.Windows.Forms.TextBox imapPWTextBox;
        private System.Windows.Forms.TextBox imapUserTextBox;
        private System.Windows.Forms.TextBox puls3060_dkPWTextBox;
        private System.Windows.Forms.TextBox puls3060_dkUserTextBox;
        private System.Windows.Forms.TextBox uniContaCompanyIdTextBox;
        private System.Windows.Forms.TextBox uniContaPWTextBox;
        private System.Windows.Forms.TextBox uniContaUserTextBox;
        private System.Windows.Forms.Button EncryptAppconfig;
        private System.Windows.Forms.Button Opdatermarkerededata;
    }
}