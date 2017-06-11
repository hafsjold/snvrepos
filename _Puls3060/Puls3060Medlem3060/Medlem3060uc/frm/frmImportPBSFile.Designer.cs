namespace Medlem3060uc
{
    partial class FrmImportPBSFile
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
            this.cmdImport = new System.Windows.Forms.Button();
            this.textFilePath = new System.Windows.Forms.TextBox();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdImport
            // 
            this.cmdImport.Location = new System.Drawing.Point(247, 61);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(129, 24);
            this.cmdImport.TabIndex = 0;
            this.cmdImport.Text = "Impoerte file";
            this.cmdImport.UseVisualStyleBackColor = true;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // textFilePath
            // 
            this.textFilePath.Location = new System.Drawing.Point(66, 35);
            this.textFilePath.Name = "textFilePath";
            this.textFilePath.Size = new System.Drawing.Size(540, 20);
            this.textFilePath.TabIndex = 1;
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Location = new System.Drawing.Point(13, 35);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(47, 13);
            this.labelFilePath.TabIndex = 2;
            this.labelFilePath.Text = "File path";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(66, 99);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(50, 13);
            this.labelMessage.TabIndex = 3;
            this.labelMessage.Text = "Message";
            this.labelMessage.Visible = false;
            // 
            // FrmImportPBSFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 130);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.textFilePath);
            this.Controls.Add(this.cmdImport);
            this.Name = "FrmImportPBSFile";
            this.Text = "Importer file fra PBS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.TextBox textFilePath;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Label labelMessage;
    }
}