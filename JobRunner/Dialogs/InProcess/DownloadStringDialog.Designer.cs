namespace JobRunner.Dialogs.InProcess
{
    partial class DownloadStringDialog
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
            this.txtSourceUrl = new System.Windows.Forms.TextBox();
            this.txtTargetFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboFileExistsBehaviour = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source URL:";
            // 
            // txtSourceUrl
            // 
            this.txtSourceUrl.Location = new System.Drawing.Point(8, 24);
            this.txtSourceUrl.Name = "txtSourceUrl";
            this.txtSourceUrl.Size = new System.Drawing.Size(528, 20);
            this.txtSourceUrl.TabIndex = 1;
            // 
            // txtTargetFile
            // 
            this.txtTargetFile.Location = new System.Drawing.Point(8, 64);
            this.txtTargetFile.Name = "txtTargetFile";
            this.txtTargetFile.Size = new System.Drawing.Size(488, 20);
            this.txtTargetFile.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Target file:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(500, 63);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(35, 22);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Local file exists behaviour:";
            // 
            // cboFileExistsBehaviour
            // 
            this.cboFileExistsBehaviour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileExistsBehaviour.FormattingEnabled = true;
            this.cboFileExistsBehaviour.Location = new System.Drawing.Point(8, 104);
            this.cboFileExistsBehaviour.Name = "cboFileExistsBehaviour";
            this.cboFileExistsBehaviour.Size = new System.Drawing.Size(528, 21);
            this.cboFileExistsBehaviour.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(380, 168);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(460, 168);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // DownloadStringDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(545, 199);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cboFileExistsBehaviour);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtTargetFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSourceUrl);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadStringDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "In process: Download string";
            this.Load += new System.EventHandler(this.DownloadStringDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSourceUrl;
        private System.Windows.Forms.TextBox txtTargetFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboFileExistsBehaviour;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}