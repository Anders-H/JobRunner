namespace JobRunner.Dialogs
{
    partial class EditJobDialog
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.chkHidden = new System.Windows.Forms.CheckBox();
            this.cboTimeout = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.lblArguments = new System.Windows.Forms.Label();
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.lblProgram = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkBreakOnError = new System.Windows.Forms.CheckBox();
            this.lblVariables = new System.Windows.Forms.LinkLabel();
            this.txtArgsEvaluated = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.cboRetryCount = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(8, 24);
            this.txtName.MaxLength = 500;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(440, 20);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Job name:";
            // 
            // chkHidden
            // 
            this.chkHidden.AutoSize = true;
            this.chkHidden.Location = new System.Drawing.Point(8, 272);
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.Size = new System.Drawing.Size(81, 17);
            this.chkHidden.TabIndex = 15;
            this.chkHidden.Text = "Run hidden";
            this.chkHidden.UseVisualStyleBackColor = true;
            // 
            // cboTimeout
            // 
            this.cboTimeout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeout.FormattingEnabled = true;
            this.cboTimeout.Location = new System.Drawing.Point(8, 220);
            this.cboTimeout.Name = "cboTimeout";
            this.cboTimeout.Size = new System.Drawing.Size(112, 21);
            this.cboTimeout.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Timeout:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(8, 88);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(8, 140);
            this.txtArguments.MaxLength = 500;
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(440, 20);
            this.txtArguments.TabIndex = 7;
            this.txtArguments.TextChanged += new System.EventHandler(this.txtArguments_TextChanged);
            // 
            // lblArguments
            // 
            this.lblArguments.AutoSize = true;
            this.lblArguments.Location = new System.Drawing.Point(8, 124);
            this.lblArguments.Name = "lblArguments";
            this.lblArguments.Size = new System.Drawing.Size(173, 13);
            this.lblArguments.TabIndex = 5;
            this.lblArguments.Text = "Arguments (variables can be used):";
            // 
            // txtProgram
            // 
            this.txtProgram.Location = new System.Drawing.Point(8, 64);
            this.txtProgram.MaxLength = 500;
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.Size = new System.Drawing.Size(440, 20);
            this.txtProgram.TabIndex = 3;
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(8, 48);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(79, 13);
            this.lblProgram.TabIndex = 2;
            this.lblProgram.Text = "Program to run:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(288, 320);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(372, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // chkBreakOnError
            // 
            this.chkBreakOnError.AutoSize = true;
            this.chkBreakOnError.Location = new System.Drawing.Point(8, 292);
            this.chkBreakOnError.Name = "chkBreakOnError";
            this.chkBreakOnError.Size = new System.Drawing.Size(93, 17);
            this.chkBreakOnError.TabIndex = 16;
            this.chkBreakOnError.Text = "Break on error";
            this.chkBreakOnError.UseVisualStyleBackColor = true;
            // 
            // lblVariables
            // 
            this.lblVariables.AutoSize = true;
            this.lblVariables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVariables.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblVariables.LinkColor = System.Drawing.Color.Navy;
            this.lblVariables.Location = new System.Drawing.Point(388, 124);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(59, 13);
            this.lblVariables.TabIndex = 6;
            this.lblVariables.TabStop = true;
            this.lblVariables.Text = "Variables...";
            this.lblVariables.VisitedLinkColor = System.Drawing.Color.Navy;
            this.lblVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblVariables_LinkClicked);
            // 
            // txtArgsEvaluated
            // 
            this.txtArgsEvaluated.Location = new System.Drawing.Point(8, 180);
            this.txtArgsEvaluated.Name = "txtArgsEvaluated";
            this.txtArgsEvaluated.ReadOnly = true;
            this.txtArgsEvaluated.Size = new System.Drawing.Size(440, 20);
            this.txtArgsEvaluated.TabIndex = 9;
            this.txtArgsEvaluated.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Arguments evaluated:";
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(8, 252);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 14;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // cboRetryCount
            // 
            this.cboRetryCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRetryCount.FormattingEnabled = true;
            this.cboRetryCount.Location = new System.Drawing.Point(128, 220);
            this.cboRetryCount.Name = "cboRetryCount";
            this.cboRetryCount.Size = new System.Drawing.Size(112, 21);
            this.cboRetryCount.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(128, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Retry count:";
            // 
            // EditJobDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(456, 349);
            this.Controls.Add(this.cboRetryCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkEnabled);
            this.Controls.Add(this.chkBreakOnError);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkHidden);
            this.Controls.Add(this.cboTimeout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtArguments);
            this.Controls.Add(this.lblArguments);
            this.Controls.Add(this.txtProgram);
            this.Controls.Add(this.lblProgram);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtArgsEvaluated);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblVariables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditJobDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit job";
            this.Load += new System.EventHandler(this.EditJobDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox chkHidden;
        private System.Windows.Forms.ComboBox cboTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Label lblArguments;
        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkBreakOnError;
        private System.Windows.Forms.LinkLabel lblVariables;
        private System.Windows.Forms.TextBox txtArgsEvaluated;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.ComboBox cboRetryCount;
        private System.Windows.Forms.Label label3;
    }
}