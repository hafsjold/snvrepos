namespace pipeClient
{
    partial class FrmStatus
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
            System.Windows.Forms.Label bdbPuls3060Medlem_OnlineLabel;
            System.Windows.Forms.Label bGigaHostImap_OnlineLabel;
            System.Windows.Forms.Label bpuls3060_dk_OnlineLabel;
            System.Windows.Forms.Label bSFTP_OnlineLabel;
            System.Windows.Forms.Label bUniConta_OnlineLabel;
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dsStatusData = new System.Windows.Forms.BindingSource(this.components);
            this.btnServerStatus = new System.Windows.Forms.Button();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.bdbPuls3060Medlem_OnlineCheckBox = new System.Windows.Forms.CheckBox();
            this.bGigaHostImap_OnlineCheckBox = new System.Windows.Forms.CheckBox();
            this.bpuls3060_dk_OnlineCheckBox = new System.Windows.Forms.CheckBox();
            this.bSFTP_OnlineCheckBox = new System.Windows.Forms.CheckBox();
            this.bUniConta_OnlineCheckBox = new System.Windows.Forms.CheckBox();
            bdbPuls3060Medlem_OnlineLabel = new System.Windows.Forms.Label();
            bGigaHostImap_OnlineLabel = new System.Windows.Forms.Label();
            bpuls3060_dk_OnlineLabel = new System.Windows.Forms.Label();
            bSFTP_OnlineLabel = new System.Windows.Forms.Label();
            bUniConta_OnlineLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsStatusData)).BeginInit();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(bdbPuls3060Medlem_OnlineLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bdbPuls3060Medlem_OnlineCheckBox);
            this.splitContainer1.Panel1.Controls.Add(bGigaHostImap_OnlineLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bGigaHostImap_OnlineCheckBox);
            this.splitContainer1.Panel1.Controls.Add(bpuls3060_dk_OnlineLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bpuls3060_dk_OnlineCheckBox);
            this.splitContainer1.Panel1.Controls.Add(bSFTP_OnlineLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bSFTP_OnlineCheckBox);
            this.splitContainer1.Panel1.Controls.Add(bUniConta_OnlineLabel);
            this.splitContainer1.Panel1.Controls.Add(this.bUniConta_OnlineCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.btnServerStatus);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxMessages);
            this.splitContainer1.Size = new System.Drawing.Size(358, 366);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.TabIndex = 0;
            // 
            // dsStatusData
            // 
            this.dsStatusData.DataSource = typeof(nsPbs3060.clsStatusData);
            // 
            // btnServerStatus
            // 
            this.btnServerStatus.Location = new System.Drawing.Point(117, 197);
            this.btnServerStatus.Name = "btnServerStatus";
            this.btnServerStatus.Size = new System.Drawing.Size(101, 23);
            this.btnServerStatus.TabIndex = 0;
            this.btnServerStatus.Text = "Server Status";
            this.btnServerStatus.UseVisualStyleBackColor = true;
            this.btnServerStatus.Click += new System.EventHandler(this.btnServerStatus_Click);
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMessages.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.ReadOnly = true;
            this.richTextBoxMessages.Size = new System.Drawing.Size(358, 120);
            this.richTextBoxMessages.TabIndex = 1;
            this.richTextBoxMessages.Text = "";
            // 
            // bdbPuls3060Medlem_OnlineLabel
            // 
            bdbPuls3060Medlem_OnlineLabel.AutoSize = true;
            bdbPuls3060Medlem_OnlineLabel.Location = new System.Drawing.Point(23, 96);
            bdbPuls3060Medlem_OnlineLabel.Name = "bdbPuls3060Medlem_OnlineLabel";
            bdbPuls3060Medlem_OnlineLabel.Size = new System.Drawing.Size(173, 13);
            bdbPuls3060Medlem_OnlineLabel.TabIndex = 1;
            bdbPuls3060Medlem_OnlineLabel.Text = "Database Puls3060Medlem Online:";
            // 
            // bdbPuls3060Medlem_OnlineCheckBox
            // 
            this.bdbPuls3060Medlem_OnlineCheckBox.AutoSize = true;
            this.bdbPuls3060Medlem_OnlineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsStatusData, "bdbPuls3060Medlem_Online", true));
            this.bdbPuls3060Medlem_OnlineCheckBox.Location = new System.Drawing.Point(203, 94);
            this.bdbPuls3060Medlem_OnlineCheckBox.Name = "bdbPuls3060Medlem_OnlineCheckBox";
            this.bdbPuls3060Medlem_OnlineCheckBox.Size = new System.Drawing.Size(15, 14);
            this.bdbPuls3060Medlem_OnlineCheckBox.TabIndex = 2;
            this.bdbPuls3060Medlem_OnlineCheckBox.UseVisualStyleBackColor = true;
            // 
            // bGigaHostImap_OnlineLabel
            // 
            bGigaHostImap_OnlineLabel.AutoSize = true;
            bGigaHostImap_OnlineLabel.Location = new System.Drawing.Point(23, 63);
            bGigaHostImap_OnlineLabel.Name = "bGigaHostImap_OnlineLabel";
            bGigaHostImap_OnlineLabel.Size = new System.Drawing.Size(113, 13);
            bGigaHostImap_OnlineLabel.TabIndex = 3;
            bGigaHostImap_OnlineLabel.Text = "GigaHost Imap Online:";
            // 
            // bGigaHostImap_OnlineCheckBox
            // 
            this.bGigaHostImap_OnlineCheckBox.AutoSize = true;
            this.bGigaHostImap_OnlineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsStatusData, "bGigaHostImap_Online", true));
            this.bGigaHostImap_OnlineCheckBox.Location = new System.Drawing.Point(203, 56);
            this.bGigaHostImap_OnlineCheckBox.Name = "bGigaHostImap_OnlineCheckBox";
            this.bGigaHostImap_OnlineCheckBox.Size = new System.Drawing.Size(15, 14);
            this.bGigaHostImap_OnlineCheckBox.TabIndex = 4;
            this.bGigaHostImap_OnlineCheckBox.UseVisualStyleBackColor = true;
            // 
            // bpuls3060_dk_OnlineLabel
            // 
            bpuls3060_dk_OnlineLabel.AutoSize = true;
            bpuls3060_dk_OnlineLabel.Location = new System.Drawing.Point(23, 129);
            bpuls3060_dk_OnlineLabel.Name = "bpuls3060_dk_OnlineLabel";
            bpuls3060_dk_OnlineLabel.Size = new System.Drawing.Size(153, 13);
            bpuls3060_dk_OnlineLabel.TabIndex = 5;
            bpuls3060_dk_OnlineLabel.Text = "Database puls3060_dk Online:";
            // 
            // bpuls3060_dk_OnlineCheckBox
            // 
            this.bpuls3060_dk_OnlineCheckBox.AutoSize = true;
            this.bpuls3060_dk_OnlineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsStatusData, "bpuls3060_dk_Online", true));
            this.bpuls3060_dk_OnlineCheckBox.Location = new System.Drawing.Point(203, 129);
            this.bpuls3060_dk_OnlineCheckBox.Name = "bpuls3060_dk_OnlineCheckBox";
            this.bpuls3060_dk_OnlineCheckBox.Size = new System.Drawing.Size(15, 14);
            this.bpuls3060_dk_OnlineCheckBox.TabIndex = 6;
            this.bpuls3060_dk_OnlineCheckBox.UseVisualStyleBackColor = true;
            // 
            // bSFTP_OnlineLabel
            // 
            bSFTP_OnlineLabel.AutoSize = true;
            bSFTP_OnlineLabel.Location = new System.Drawing.Point(23, 162);
            bSFTP_OnlineLabel.Name = "bSFTP_OnlineLabel";
            bSFTP_OnlineLabel.Size = new System.Drawing.Size(95, 13);
            bSFTP_OnlineLabel.TabIndex = 7;
            bSFTP_OnlineLabel.Text = "Nets SFTP Online:";
            // 
            // bSFTP_OnlineCheckBox
            // 
            this.bSFTP_OnlineCheckBox.AutoSize = true;
            this.bSFTP_OnlineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsStatusData, "bSFTP_Online", true));
            this.bSFTP_OnlineCheckBox.Location = new System.Drawing.Point(203, 159);
            this.bSFTP_OnlineCheckBox.Name = "bSFTP_OnlineCheckBox";
            this.bSFTP_OnlineCheckBox.Size = new System.Drawing.Size(15, 14);
            this.bSFTP_OnlineCheckBox.TabIndex = 8;
            this.bSFTP_OnlineCheckBox.UseVisualStyleBackColor = true;
            // 
            // bUniConta_OnlineLabel
            // 
            bUniConta_OnlineLabel.AutoSize = true;
            bUniConta_OnlineLabel.Location = new System.Drawing.Point(23, 30);
            bUniConta_OnlineLabel.Name = "bUniConta_OnlineLabel";
            bUniConta_OnlineLabel.Size = new System.Drawing.Size(87, 13);
            bUniConta_OnlineLabel.TabIndex = 9;
            bUniConta_OnlineLabel.Text = "UniConta Online:";
            // 
            // bUniConta_OnlineCheckBox
            // 
            this.bUniConta_OnlineCheckBox.AutoSize = true;
            this.bUniConta_OnlineCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.dsStatusData, "bUniConta_Online", true));
            this.bUniConta_OnlineCheckBox.Location = new System.Drawing.Point(203, 25);
            this.bUniConta_OnlineCheckBox.Name = "bUniConta_OnlineCheckBox";
            this.bUniConta_OnlineCheckBox.Size = new System.Drawing.Size(15, 14);
            this.bUniConta_OnlineCheckBox.TabIndex = 10;
            this.bUniConta_OnlineCheckBox.UseVisualStyleBackColor = true;
            // 
            // FrmStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 366);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmStatus";
            this.Text = "frmStatus";
            this.Load += new System.EventHandler(this.FrmStatus_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsStatusData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.Button btnServerStatus;
        private System.Windows.Forms.BindingSource dsStatusData;
        private System.Windows.Forms.CheckBox bdbPuls3060Medlem_OnlineCheckBox;
        private System.Windows.Forms.CheckBox bGigaHostImap_OnlineCheckBox;
        private System.Windows.Forms.CheckBox bpuls3060_dk_OnlineCheckBox;
        private System.Windows.Forms.CheckBox bSFTP_OnlineCheckBox;
        private System.Windows.Forms.CheckBox bUniConta_OnlineCheckBox;
    }
}