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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageBasic = new System.Windows.Forms.TabPage();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.tabPageProcess = new System.Windows.Forms.TabPage();
            this.txtArgsEvaluated = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.lblArguments = new System.Windows.Forms.Label();
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.lblProgram = new System.Windows.Forms.Label();
            this.lblVariables = new System.Windows.Forms.LinkLabel();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.chkHidden = new System.Windows.Forms.CheckBox();
            this.cboTimeout = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabFlowControl = new System.Windows.Forms.TabPage();
            this.chkBreakOnError = new System.Windows.Forms.CheckBox();
            this.tabPageOverview = new System.Windows.Forms.TabPage();
            this.tvOverview = new System.Windows.Forms.TreeView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnInProcess = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteAFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPageBasic.SuspendLayout();
            this.tabPageProcess.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabFlowControl.SuspendLayout();
            this.tabPageOverview.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageBasic);
            this.tabControl1.Controls.Add(this.tabPageProcess);
            this.tabControl1.Controls.Add(this.tabOptions);
            this.tabControl1.Controls.Add(this.tabFlowControl);
            this.tabControl1.Controls.Add(this.tabPageOverview);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 264);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPageBasic
            // 
            this.tabPageBasic.Controls.Add(this.txtName);
            this.tabPageBasic.Controls.Add(this.lblName);
            this.tabPageBasic.Controls.Add(this.txtNumber);
            this.tabPageBasic.Controls.Add(this.lblNumber);
            this.tabPageBasic.Location = new System.Drawing.Point(4, 22);
            this.tabPageBasic.Name = "tabPageBasic";
            this.tabPageBasic.Size = new System.Drawing.Size(460, 238);
            this.tabPageBasic.TabIndex = 0;
            this.tabPageBasic.Text = "Basic information";
            this.tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(8, 68);
            this.txtName.MaxLength = 500;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(440, 20);
            this.txtName.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 52);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(56, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Job name:";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(8, 24);
            this.txtNumber.MaxLength = 6;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(116, 20);
            this.txtNumber.TabIndex = 1;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(8, 8);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(97, 13);
            this.lblNumber.TabIndex = 0;
            this.lblNumber.Text = "Sequence number:";
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.btnInProcess);
            this.tabPageProcess.Controls.Add(this.txtArgsEvaluated);
            this.tabPageProcess.Controls.Add(this.label2);
            this.tabPageProcess.Controls.Add(this.btnBrowse);
            this.tabPageProcess.Controls.Add(this.txtArguments);
            this.tabPageProcess.Controls.Add(this.lblArguments);
            this.tabPageProcess.Controls.Add(this.txtProgram);
            this.tabPageProcess.Controls.Add(this.lblProgram);
            this.tabPageProcess.Controls.Add(this.lblVariables);
            this.tabPageProcess.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.Size = new System.Drawing.Size(460, 238);
            this.tabPageProcess.TabIndex = 1;
            this.tabPageProcess.Text = "Process";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // txtArgsEvaluated
            // 
            this.txtArgsEvaluated.Location = new System.Drawing.Point(8, 208);
            this.txtArgsEvaluated.Name = "txtArgsEvaluated";
            this.txtArgsEvaluated.ReadOnly = true;
            this.txtArgsEvaluated.Size = new System.Drawing.Size(440, 20);
            this.txtArgsEvaluated.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Arguments evaluated:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(8, 48);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(84, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // txtArguments
            // 
            this.txtArguments.Location = new System.Drawing.Point(8, 164);
            this.txtArguments.MaxLength = 500;
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.Size = new System.Drawing.Size(440, 20);
            this.txtArguments.TabIndex = 6;
            this.txtArguments.TextChanged += new System.EventHandler(this.txtArguments_TextChanged);
            // 
            // lblArguments
            // 
            this.lblArguments.AutoSize = true;
            this.lblArguments.Location = new System.Drawing.Point(8, 148);
            this.lblArguments.Name = "lblArguments";
            this.lblArguments.Size = new System.Drawing.Size(173, 13);
            this.lblArguments.TabIndex = 4;
            this.lblArguments.Text = "Arguments (variables can be used):";
            // 
            // txtProgram
            // 
            this.txtProgram.Location = new System.Drawing.Point(8, 24);
            this.txtProgram.MaxLength = 500;
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.Size = new System.Drawing.Size(440, 20);
            this.txtProgram.TabIndex = 1;
            // 
            // lblProgram
            // 
            this.lblProgram.AutoSize = true;
            this.lblProgram.Location = new System.Drawing.Point(8, 8);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Size = new System.Drawing.Size(79, 13);
            this.lblProgram.TabIndex = 0;
            this.lblProgram.Text = "Program to run:";
            // 
            // lblVariables
            // 
            this.lblVariables.AutoSize = true;
            this.lblVariables.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVariables.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lblVariables.LinkColor = System.Drawing.Color.Navy;
            this.lblVariables.Location = new System.Drawing.Point(388, 150);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(59, 13);
            this.lblVariables.TabIndex = 5;
            this.lblVariables.TabStop = true;
            this.lblVariables.Text = "Variables...";
            this.lblVariables.VisitedLinkColor = System.Drawing.Color.Navy;
            this.lblVariables.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblVariables_LinkClicked);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.chkHidden);
            this.tabOptions.Controls.Add(this.cboTimeout);
            this.tabOptions.Controls.Add(this.label1);
            this.tabOptions.Location = new System.Drawing.Point(4, 22);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(460, 238);
            this.tabOptions.TabIndex = 4;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // chkHidden
            // 
            this.chkHidden.AutoSize = true;
            this.chkHidden.Location = new System.Drawing.Point(8, 216);
            this.chkHidden.Name = "chkHidden";
            this.chkHidden.Size = new System.Drawing.Size(81, 17);
            this.chkHidden.TabIndex = 2;
            this.chkHidden.Text = "Run hidden";
            this.chkHidden.UseVisualStyleBackColor = true;
            // 
            // cboTimeout
            // 
            this.cboTimeout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeout.FormattingEnabled = true;
            this.cboTimeout.Location = new System.Drawing.Point(8, 24);
            this.cboTimeout.Name = "cboTimeout";
            this.cboTimeout.Size = new System.Drawing.Size(112, 21);
            this.cboTimeout.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Timeout:";
            // 
            // tabFlowControl
            // 
            this.tabFlowControl.Controls.Add(this.chkBreakOnError);
            this.tabFlowControl.Location = new System.Drawing.Point(4, 22);
            this.tabFlowControl.Name = "tabFlowControl";
            this.tabFlowControl.Size = new System.Drawing.Size(460, 238);
            this.tabFlowControl.TabIndex = 3;
            this.tabFlowControl.Text = "Flow control";
            this.tabFlowControl.UseVisualStyleBackColor = true;
            // 
            // chkBreakOnError
            // 
            this.chkBreakOnError.AutoSize = true;
            this.chkBreakOnError.Location = new System.Drawing.Point(8, 8);
            this.chkBreakOnError.Name = "chkBreakOnError";
            this.chkBreakOnError.Size = new System.Drawing.Size(93, 17);
            this.chkBreakOnError.TabIndex = 0;
            this.chkBreakOnError.Text = "Break on error";
            this.chkBreakOnError.UseVisualStyleBackColor = true;
            // 
            // tabPageOverview
            // 
            this.tabPageOverview.Controls.Add(this.tvOverview);
            this.tabPageOverview.Location = new System.Drawing.Point(4, 22);
            this.tabPageOverview.Name = "tabPageOverview";
            this.tabPageOverview.Size = new System.Drawing.Size(460, 238);
            this.tabPageOverview.TabIndex = 2;
            this.tabPageOverview.Text = "Overview";
            this.tabPageOverview.UseVisualStyleBackColor = true;
            // 
            // tvOverview
            // 
            this.tvOverview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvOverview.Location = new System.Drawing.Point(4, 4);
            this.tvOverview.Name = "tvOverview";
            this.tvOverview.ShowPlusMinus = false;
            this.tvOverview.Size = new System.Drawing.Size(452, 228);
            this.tvOverview.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Enabled = false;
            this.btnBack.Location = new System.Drawing.Point(152, 272);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "< Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(232, 272);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(312, 272);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Finish";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(392, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnInProcess
            // 
            this.btnInProcess.Location = new System.Drawing.Point(96, 48);
            this.btnInProcess.Name = "btnInProcess";
            this.btnInProcess.Size = new System.Drawing.Size(84, 23);
            this.btnInProcess.TabIndex = 3;
            this.btnInProcess.Text = "In progress...";
            this.btnInProcess.UseVisualStyleBackColor = true;
            this.btnInProcess.Click += new System.EventHandler(this.btnInProcess_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteAFileToolStripMenuItem,
            this.downloadTextToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // deleteAFileToolStripMenuItem
            // 
            this.deleteAFileToolStripMenuItem.Name = "deleteAFileToolStripMenuItem";
            this.deleteAFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteAFileToolStripMenuItem.Text = "Delete a file...";
            this.deleteAFileToolStripMenuItem.Click += new System.EventHandler(this.deleteAFileToolStripMenuItem_Click);
            // 
            // downloadTextToolStripMenuItem
            // 
            this.downloadTextToolStripMenuItem.Name = "downloadTextToolStripMenuItem";
            this.downloadTextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.downloadTextToolStripMenuItem.Text = "Download text...";
            this.downloadTextToolStripMenuItem.Click += new System.EventHandler(this.downloadTextToolStripMenuItem_Click);
            // 
            // AddJobDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(475, 301);
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
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.tabFlowControl.ResumeLayout(false);
            this.tabFlowControl.PerformLayout();
            this.tabPageOverview.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage tabFlowControl;
        private System.Windows.Forms.CheckBox chkBreakOnError;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.CheckBox chkHidden;
        private System.Windows.Forms.ComboBox cboTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtArgsEvaluated;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel lblVariables;
        private System.Windows.Forms.Button btnInProcess;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteAFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadTextToolStripMenuItem;
    }
}