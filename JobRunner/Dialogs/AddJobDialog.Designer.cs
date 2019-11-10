namespace JobRunner.Dialogs
{
    partial class AddJobDialog
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.tabPageProcess = new System.Windows.Forms.TabPage();
            this.chkHidden = new System.Windows.Forms.CheckBox();
            this.cboTimeout = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.lblArguments = new System.Windows.Forms.Label();
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.lblProgram = new System.Windows.Forms.Label();
            this.tabFlowControl = new System.Windows.Forms.TabPage();
            this.chkBreakOnError = new System.Windows.Forms.CheckBox();
            this.tabPageOverview = new System.Windows.Forms.TabPage();
            this.tvOverview = new System.Windows.Forms.TreeView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageBasic.SuspendLayout();
            this.tabPageProcess.SuspendLayout();
            this.tabFlowControl.SuspendLayout();
            this.tabPageOverview.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageBasic);
            this.tabControl1.Controls.Add(this.tabPageProcess);
            this.tabControl1.Controls.Add(this.tabFlowControl);
            this.tabControl1.Controls.Add(this.tabPageOverview);
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(546, 305);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.txtName);
            this.tabPageBasic.Controls.Add(this.lblName);
            this.tabPageBasic.Controls.Add(this.txtNumber);
            this.tabPageBasic.Controls.Add(this.lblNumber);
            this.tabPageBasic.Location = new System.Drawing.Point(4, 24);
            this.tabPageBasic.Name = "tabPageBasic";
            this.tabPageBasic.Size = new System.Drawing.Size(538, 277);
            this.tabPageBasic.TabIndex = 0;
            this.tabPageBasic.Text = "Basic information";
            this.tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(9, 78);
            this.txtName.MaxLength = 500;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(513, 23);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(9, 60);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(61, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Job name:";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(9, 28);
            this.txtNumber.MaxLength = 6;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(135, 23);
            this.txtNumber.TabIndex = 1;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(9, 9);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(106, 15);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "Sequence number:";
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.chkHidden);
            this.tabPageProcess.Controls.Add(this.cboTimeout);
            this.tabPageProcess.Controls.Add(this.label1);
            this.tabPageProcess.Controls.Add(this.btnBrowse);
            this.tabPageProcess.Controls.Add(this.txtArguments);
            this.tabPageProcess.Controls.Add(this.lblArguments);
            this.tabPageProcess.Controls.Add(this.txtProgram);
            this.tabPageProcess.Controls.Add(this.lblProgram);
            this.tabPageProcess.Location = new System.Drawing.Point(4, 24);
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.Size = new System.Drawing.Size(538, 277);
            this.tabPageProcess.TabIndex = 1;
            this.tabPageProcess.Text = "Process";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // chkHidden
            // 
            this.chkHidden.AutoSize = true;
            this.chkHidden.Location = new System.Drawing.Point(9, 249);
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.Size = new System.Drawing.Size(87, 19);
            this.chkHidden.TabIndex = 7;
            this.chkHidden.Text = "Run hidden";
            this.chkHidden.UseVisualStyleBackColor = true;
            // 
            // cboTimeout
            // 
            this.cboTimeout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeout.FormattingEnabled = true;
            this.cboTimeout.Location = new System.Drawing.Point(9, 162);
            this.cboTimeout.Name = "cboTimeout";
            this.cboTimeout.Size = new System.Drawing.Size(130, 23);
            this.cboTimeout.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Timeout:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(9, 55);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(87, 27);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(9, 115);
            this.txtArguments.MaxLength = 500;
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(513, 23);
            this.txtArguments.TabIndex = 4;
            // 
            // lblArguments
            // 
            this.lblArguments.AutoSize = true;
            this.lblArguments.Location = new System.Drawing.Point(9, 97);
            this.lblArguments.Name = "lblArguments";
            this.lblArguments.Size = new System.Drawing.Size(192, 15);
            this.lblArguments.TabIndex = 3;
            this.lblArguments.Text = "Arguments (variables can be used):";
            // 
            // txtProgram
            // 
            this.txtProgram.Location = new System.Drawing.Point(9, 28);
            this.txtProgram.MaxLength = 500;
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.Size = new System.Drawing.Size(513, 23);
            this.txtProgram.TabIndex = 1;
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(9, 9);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(91, 15);
            this.lblProgram.TabIndex = 0;
            this.lblProgram.Text = "Program to run:";
            // 
            // tabFlowControl
            // 
            this.tabFlowControl.Controls.Add(this.chkBreakOnError);
            this.tabFlowControl.Location = new System.Drawing.Point(4, 24);
            this.tabFlowControl.Name = "tabFlowControl";
            this.tabFlowControl.Size = new System.Drawing.Size(538, 277);
            this.tabFlowControl.TabIndex = 3;
            this.tabFlowControl.Text = "Flow control";
            this.tabFlowControl.UseVisualStyleBackColor = true;
            // 
            // chkBreakOnError
            // 
            this.chkBreakOnError.AutoSize = true;
            this.chkBreakOnError.Location = new System.Drawing.Point(9, 9);
            this.chkBreakOnError.Name = "chkBreakOnError";
            this.chkBreakOnError.Size = new System.Drawing.Size(100, 19);
            this.chkBreakOnError.TabIndex = 0;
            this.chkBreakOnError.Text = "Break on error";
            this.chkBreakOnError.UseVisualStyleBackColor = true;
            // 
            // tabPageOverview
            // 
            this.tabPageOverview.Controls.Add(this.tvOverview);
            this.tabPageOverview.Location = new System.Drawing.Point(4, 24);
            this.tabPageOverview.Name = "tabPageOverview";
            this.tabPageOverview.Size = new System.Drawing.Size(538, 277);
            this.tabPageOverview.TabIndex = 2;
            this.tabPageOverview.Text = "Overview";
            this.tabPageOverview.UseVisualStyleBackColor = true;
            // 
            // tvOverview
            // 
            this.tvOverview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvOverview.Location = new System.Drawing.Point(5, 5);
            this.tvOverview.Name = "tvOverview";
            this.tvOverview.ShowPlusMinus = false;
            this.tvOverview.Size = new System.Drawing.Size(527, 263);
            this.tvOverview.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Enabled = false;
            this.btnBack.Location = new System.Drawing.Point(177, 314);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(87, 27);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(271, 314);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(87, 27);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(364, 314);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(87, 27);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Finish";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(457, 314);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AddJobDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(554, 347);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddJobDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add job";
            this.Load += new System.EventHandler(this.AddJobDialog_Load);
            this.Shown += new System.EventHandler(this.AddJobDialog_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPageBasic.ResumeLayout(false);
            this.tabPageBasic.PerformLayout();
            this.tabPageProcess.ResumeLayout(false);
            this.tabPageProcess.PerformLayout();
            this.tabFlowControl.ResumeLayout(false);
            this.tabFlowControl.PerformLayout();
            this.tabPageOverview.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabPageBasic;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TabPage tabPageProcess;
        private System.Windows.Forms.TabPage tabPageOverview;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtArguments;
        private System.Windows.Forms.Label lblArguments;
        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.TreeView tvOverview;
        private System.Windows.Forms.CheckBox chkHidden;
        private System.Windows.Forms.ComboBox cboTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabFlowControl;
        private System.Windows.Forms.CheckBox chkBreakOnError;
    }
}