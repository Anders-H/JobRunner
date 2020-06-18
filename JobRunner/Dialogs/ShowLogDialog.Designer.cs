namespace JobRunner.Dialogs
{
    partial class ShowLogDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.LinkLabel();
            this.lblFile = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder:";
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFolder.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblFolder.LinkColor = System.Drawing.Color.Navy;
            this.lblFolder.Location = new System.Drawing.Point(12, 28);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(16, 13);
            this.lblFolder.TabIndex = 1;
            this.lblFolder.TabStop = true;
            this.lblFolder.Text = "   ";
            this.lblFolder.VisitedLinkColor = System.Drawing.Color.Navy;
            this.lblFolder.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFolder_LinkClicked);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFile.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblFile.LinkColor = System.Drawing.Color.Navy;
            this.lblFile.Location = new System.Drawing.Point(12, 68);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(16, 13);
            this.lblFile.TabIndex = 3;
            this.lblFile.TabStop = true;
            this.lblFile.Text = "   ";
            this.lblFile.VisitedLinkColor = System.Drawing.Color.Navy;
            this.lblFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblFile_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "File:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 92);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(400, 116);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // ShowLogDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(488, 149);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowLogDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log";
            this.Shown += new System.EventHandler(this.ShowLogDialog_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblFile;
        private System.Windows.Forms.LinkLabel lblFolder;
        private System.Windows.Forms.Label lblStatus;

        #endregion
    }
}