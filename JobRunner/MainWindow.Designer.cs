namespace JobRunner
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.moveJobUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveJobDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runSelectedJobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jobsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.variablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.grid1 = new JobRunner.GuiComponents.Grid();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) (this.grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.fileToolStripMenuItem, this.editToolStripMenuItem, this.jobsToolStripMenuItem,
                this.viewToolStripMenuItem, this.helpToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(933, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.addJobToolStripMenuItem, this.editJobToolStripMenuItem, this.deleteJobToolStripMenuItem,
                this.toolStripMenuItem1, this.addVariableToolStripMenuItem, this.editVariableToolStripMenuItem,
                this.deleteVariableToolStripMenuItem, this.toolStripMenuItem3, this.moveJobUpToolStripMenuItem,
                this.moveJobDownToolStripMenuItem, this.toolStripMenuItem2, this.optionsToolStripMenuItem
            });
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // addJobToolStripMenuItem
            // 
            this.addJobToolStripMenuItem.Name = "addJobToolStripMenuItem";
            this.addJobToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.addJobToolStripMenuItem.Text = "Add job...";
            this.addJobToolStripMenuItem.Click += new System.EventHandler(this.AddJobToolStripMenuItem_Click);
            // 
            // editJobToolStripMenuItem
            // 
            this.editJobToolStripMenuItem.Name = "editJobToolStripMenuItem";
            this.editJobToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.editJobToolStripMenuItem.Text = "Edit job...";
            this.editJobToolStripMenuItem.Click += new System.EventHandler(this.EditJobToolStripMenuItem_Click);
            // 
            // deleteJobToolStripMenuItem
            // 
            this.deleteJobToolStripMenuItem.Name = "deleteJobToolStripMenuItem";
            this.deleteJobToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.deleteJobToolStripMenuItem.Text = "Delete job";
            this.deleteJobToolStripMenuItem.Click += new System.EventHandler(this.DeleteJobToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // addVariableToolStripMenuItem
            // 
            this.addVariableToolStripMenuItem.Name = "addVariableToolStripMenuItem";
            this.addVariableToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.addVariableToolStripMenuItem.Text = "Add variable...";
            this.addVariableToolStripMenuItem.Click += new System.EventHandler(this.addVariableToolStripMenuItem_Click);
            // 
            // editVariableToolStripMenuItem
            // 
            this.editVariableToolStripMenuItem.Name = "editVariableToolStripMenuItem";
            this.editVariableToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.editVariableToolStripMenuItem.Text = "Edit variable...";
            // 
            // deleteVariableToolStripMenuItem
            // 
            this.deleteVariableToolStripMenuItem.Name = "deleteVariableToolStripMenuItem";
            this.deleteVariableToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.deleteVariableToolStripMenuItem.Text = "Delete variable";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(154, 6);
            // 
            // moveJobUpToolStripMenuItem
            // 
            this.moveJobUpToolStripMenuItem.Name = "moveJobUpToolStripMenuItem";
            this.moveJobUpToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.moveJobUpToolStripMenuItem.Text = "Move job up";
            this.moveJobUpToolStripMenuItem.Click += new System.EventHandler(this.MoveJobUpToolStripMenuItem_Click);
            // 
            // moveJobDownToolStripMenuItem
            // 
            this.moveJobDownToolStripMenuItem.Name = "moveJobDownToolStripMenuItem";
            this.moveJobDownToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.moveJobDownToolStripMenuItem.Text = "Move job down";
            this.moveJobDownToolStripMenuItem.Click += new System.EventHandler(this.MoveJobDownToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // jobsToolStripMenuItem
            // 
            this.jobsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.runToolStripMenuItem, this.runSelectedJobToolStripMenuItem});
            this.jobsToolStripMenuItem.Name = "jobsToolStripMenuItem";
            this.jobsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.jobsToolStripMenuItem.Text = "&Jobs";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.runToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // runSelectedJobToolStripMenuItem
            // 
            this.runSelectedJobToolStripMenuItem.Name = "runSelectedJobToolStripMenuItem";
            this.runSelectedJobToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.runSelectedJobToolStripMenuItem.Text = "Run selected job";
            this.runSelectedJobToolStripMenuItem.Click +=
                new System.EventHandler(this.RunSelectedJobToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.jobsToolStripMenuItem1, this.variablesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // jobsToolStripMenuItem1
            // 
            this.jobsToolStripMenuItem1.Name = "jobsToolStripMenuItem1";
            this.jobsToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.jobsToolStripMenuItem1.Text = "Jobs...";
            this.jobsToolStripMenuItem1.Click += new System.EventHandler(this.jobsToolStripMenuItem1_Click);
            // 
            // variablesToolStripMenuItem
            // 
            this.variablesToolStripMenuItem.Name = "variablesToolStripMenuItem";
            this.variablesToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.variablesToolStripMenuItem.Text = "Variables...";
            this.variablesToolStripMenuItem.Click += new System.EventHandler(this.variablesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
                {this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 497);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(933, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 17);
            this.lblStatus.Text = "   ";
            // 
            // grid1
            // 
            this.grid1.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid1.CursorBlink = false;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.Location = new System.Drawing.Point(0, 24);
            this.grid1.Name = "grid1";
            this.grid1.Running = false;
            this.grid1.RunSingle = 0;
            this.grid1.Size = new System.Drawing.Size(933, 473);
            this.grid1.TabIndex = 2;
            this.grid1.EditJob += new System.EventHandler(this.Grid1_EditJob);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "JobRunner";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize) (this.grid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private JobRunner.GuiComponents.Grid grid1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jobsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runSelectedJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteJobToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem moveJobUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveJobDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jobsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem deleteVariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editVariableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addVariableToolStripMenuItem;
    }
}

