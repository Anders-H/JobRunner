﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JobRunner.Dialogs;
using JobRunner.Dialogs.ViewList;
using JobRunner.Logging;
using JobRunner.ObjectModel;
using JobRunner.Utils;

namespace JobRunner
{
    public partial class MainWindow : Form
    {
        private IJobList Jobs { get; } = new JobList();
        private IVariableList Variables { get; } = new VariableList();
        private bool CleanExit { get; set; }
        private ILogger _logger;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            lblStatus.Text = @"Initializing...";
            Cursor = Cursors.WaitCursor;
            addJobToolStripMenuItem.Enabled
                = editJobToolStripMenuItem.Enabled
                = deleteJobToolStripMenuItem.Enabled
                = moveJobUpToolStripMenuItem.Enabled
                = moveJobDownToolStripMenuItem.Enabled
                = addVariableToolStripMenuItem.Enabled
                = editVariableToolStripMenuItem.Enabled
                = deleteVariableToolStripMenuItem.Enabled
                = Config.IsAdministrator;
            AssignLogger();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HasJobs())
                return;
            lblStatus.Text = @"Running...";
            EnableGui(false);
            Jobs.ResetJobs();
            grid1.RunSingle = -1;
            grid1.Running = true;
            grid1.Invalidate();
            var action = new Action(RunJobs);
            action.BeginInvoke(RunJobsCompleted, null);
        }

        private void RunSelectedJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HasJobs())
                return;
            if (grid1.SelectedJob == null)
            {
                MessageBox.Show(@"No job is selected.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lblStatus.Text = @"Running...";
            EnableGui(false);
            Jobs.ResetJobs();
            grid1.RunSingle = grid1.SelectedCells[0].RowIndex;
            grid1.Running = true;
            grid1.Invalidate();
            var action = new Action(RunJobs);
            action.BeginInvoke(RunJobsCompleted, null);
        }

        private bool HasJobs()
        {
            if (Jobs.Count > 0)
                return true;
            MessageBox.Show(@"Job list is empty.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        public void RunJobsCompleted(IAsyncResult ar) =>
            Invoke(new Action(RunJobsCompletedActions));

        private void RunJobsCompletedActions()
        {
            lblStatus.Text = CleanExit
                ? @"Done."
                : $@"Break. Completed: {Jobs.Completed}, error: {Jobs.Error}, no action: {Jobs.Pending}";
            EnableGui(true);
            grid1.Running = false;
            grid1.Invalidate();
            if (Config.AutoClose && Jobs.RunSuccess && AutoActionDialog.CheckAutoClose(this))
                Close();
        }

        private void EnableGui(bool enabled)
        {
            runToolStripMenuItem.Enabled = enabled;
            runSelectedJobToolStripMenuItem.Enabled = enabled;
        }

        private void RunJobs()
        {
            CleanExit = true;
            var jobIndex = 0;
            foreach (var job in Jobs.All)
            {
                if (grid1.RunSingle < 0 || grid1.RunSingle == jobIndex)
                {
                    job.Status = JobStatus.Running;
                    lblStatus.Text = grid1.RunSingle < 0
                        ? $@"Running step {job.RowIndex + 1} of {Jobs.Count}..."
                        : "Running single job...";
                    grid1.Invalidate();
                    job.Run(_logger, grid1, Variables);
                    grid1.Invalidate();
                    if (job.BreakOnError && job.Status != JobStatus.Completed)
                    {
                        CleanExit = false;
                        return;
                    }
                }
                jobIndex++;
            }
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            if (!Config.Validate())
            {
                Close();
                return;
            }
            Refresh();
            Jobs.Load();
            Variables.Load();
            if (!Jobs.LoadSuccess)
            {
                Cursor = Cursors.Default;
                lblStatus.Text = @"Load failed.";
                var failMessage = string.IsNullOrWhiteSpace(Jobs.LoadFailedMessage)
                    ? "An unknown error has occured."
                    : Jobs.LoadFailedMessage;
                var t = new StringBuilder();
                t.Append(failMessage);
                t.AppendLine();
                t.AppendLine();
                if (!Config.IsAdministrator)
                {
                    t.Append("The application will close.");
                    t.AppendLine();
                    t.AppendLine();
                    t.AppendLine("To be able to edit the job list, start JobRunner as administrator.");
                }
                MessageBox.Show(t.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!Config.IsAdministrator)
                    Close();
            }
            if (Config.IsAdministrator && !string.IsNullOrWhiteSpace(Jobs.LoadFailedMessage))
                MessageDisplayer.Yell(Jobs.LoadFailedMessage, Text);
            grid1.Initialize(Jobs);
            if (Config.IsAdministrator)
            {
                Text = @"JobRunner (Administrator)";
                lblStatus.Text = @"Done.";
            }
            else
            {
                Text = @"JobRunner";
                lblStatus.Text = @"Done (read only).";
            }
            Cursor = Cursors.Default;
            if (Config.AutoStart && AutoActionDialog.CheckAutoStart(this))
                runToolStripMenuItem_Click(this, new EventArgs());
        }

        private void AddJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;
            using var x = new AddJobDialog
            {
                Jobs = Jobs,
                Variables = Variables
            };
            if (x.ShowDialog(this) != DialogResult.OK)
                return;
            grid1.Initialize(Jobs);
            SaveJobs();
        }

        private void SaveJobs()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                using var sw = new StreamWriter(Config.GetJobFilePath(), false, Encoding.UTF8);
                sw.Write(Jobs.GetXml());
                sw.Flush();
                sw.Close();
                Cursor = Cursors.Default;
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageDisplayer.Yell($@"Failed to save the file ""{Config.GetJobFilePath()}"".", Text);
            }
        }

        private void SaveVariables()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                using var sw = new StreamWriter(Config.GetVariableFilePath(), false, Encoding.UTF8);
                sw.Write(Variables.GetXml());
                sw.Flush();
                sw.Close();
                Cursor = Cursors.Default;
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageDisplayer.Yell($@"Failed to save the file ""{Config.GetVariableFilePath()}"".", Text);
            }
        }
        
        private void DeleteJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;
            var job = grid1.SelectedJob;
            if (job == null)
            {
                MessageDisplayer.Tell("No job is selected.", "Delete job");
                return;
            }

            if (!MessageDisplayer.Ask($@"Are you sure you want to delete the job named ""{job.Name}""?", "Delete job"))
                return;

            grid1.RemoveJob(grid1.SelectedRow);
            Jobs.RemoveJob(job);
            grid1.Refresh();
            SaveJobs();
        }

        private void Grid1_EditJob(object sender, EventArgs e)
        {
            if (!runToolStripMenuItem.Enabled)
                return;

            if (Config.IsAdministrator)
            {
                EditJobToolStripMenuItem_Click(sender, e);
                return;
            }

            if (grid1.SelectedJob == null)
                return;

            ViewJob();
        }

        private void ViewJob()
        {
            using var x = new ViewJobDialog
            {
                Job = grid1.SelectedJob
            };
            var result = x.ShowDialog(this);

            if (result == DialogResult.OK)
                RunSelectedJobToolStripMenuItem_Click(grid1, new EventArgs());
        }
        
        private void EditJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;

            if (grid1.SelectedJob == null)
            {
                MessageDisplayer.Tell("No job is selected.", "Edit job");
                return;
            }

            using var x = new EditJobDialog
            {
                Job = grid1.SelectedJob,
                Variables = Variables,
                Jobs = Jobs,
                SaveVariables = SaveVariables
            };

            if (x.ShowDialog(this) != DialogResult.OK)
                return;
            grid1.Refresh();
            SaveJobs();
        }

        private void MoveJobUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;
            var job = grid1.SelectedJob;
            if (job == null)
            {
                MessageDisplayer.Tell("No job is selected.", "Move job up");
                return;
            }
            if (Jobs.FirstJob == job)
            {
                MessageDisplayer.Tell("Selected job is already the first job.", "Move job up");
                return;
            }
            grid1.MoveUp(grid1.SelectedRow);
            Jobs.MoveUp(job);
            grid1.Refresh();
            SaveJobs();
        }

        private void MoveJobDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;
            var job = grid1.SelectedJob;
            if (job == null)
            {
                MessageDisplayer.Tell("No job is selected.", "Move job down");
                return;
            }
            if (Jobs.LastJob == job)
            {
                MessageDisplayer.Tell("Selected job is already the last job.", "Move job down");
                return;
            }
            grid1.MoveDown(grid1.SelectedRow);
            Jobs.MoveDown(job);
            grid1.Refresh();
            SaveJobs();
        }

        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var x = new OptionsDialog();

            if (x.ShowDialog(this) != DialogResult.OK || !Config.IsAdministrator)
                return;
            
            SaveJobs();
        }

        private void jobsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var listDescriptor = new SimpleListDescriptor
            {
                WindowTitle = "Jobs",
                PrimaryColumnTitle = "Job",
                SecondaryColumnTitle = "Variables"
            };
            listDescriptor.AddRange(
                from job
                in Jobs.All
                let variables = Variables.GetVariables(job)
                select new SimpleListItem(
                    job.Name,
                    variables.GetVariableNames(job),
                    job.Name
                )
            );
            SimpleListDialog.ShowListDialog(this, listDescriptor, null);
        }

        private void variablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listDescriptor = new SimpleListDescriptor
            {
                WindowTitle = "Variables",
                PrimaryColumnTitle = "Variable",
                SecondaryColumnTitle = "Usage (job name)"
            };
            listDescriptor.AddRange(
                from variable
                in Variables.All
                let jobs = Jobs.GetVariableUsage(variable)
                select new SimpleListItem(
                    $"\"{variable.Name}\"=\"{variable.Value}\"",
                    jobs.Names,
                    $"[{variable.Name}]"
                )
            );
            SimpleListDialog.ShowListDialog(
                this,
                listDescriptor,
                Config.IsAdministrator ? (Action<SimpleListDescriptor>)AddVariable : null
            );
        }

        private void AddVariable(SimpleListDescriptor descriptor)
        {
            using var x = new AddVariableDialogSmall
            {
                Variables = Variables
            };
            x.ShowDialog(this);
            descriptor.Clear();
            descriptor.AddRange(
                from variable
                    in Variables.All
                let jobs = Jobs.GetVariableUsage(variable)
                select new SimpleListItem(
                    $"\"{variable.Name}\"=\"{variable.Value}\"",
                    jobs.Names,
                    $"[{variable.Name}]"
                )
            );
            SaveVariables();
        }
        
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new StringBuilder();
            about.AppendLine("Changes in version 1.3:");
            about.AppendLine("- Variables can be added on the fly, from any view.");
            about.AppendLine("- Two in-process tasks are added: \"Delete a file\" and \"Download text\".");
            about.AppendLine("- Non-administrators can view or start a job by double clicking on it.");
            about.AppendLine("- Context menu added.");
            about.AppendLine();
            about.AppendLine("Changes in version 1.2:");
            about.AppendLine("- Minor improvments in the user interface (added icons, more options in the Add job dialog).");
            about.AppendLine("- Non administrators can open the Options dialog in read only mode.");
            about.AppendLine("- Logging to file added.");
            about.AppendLine();
            about.AppendLine("Changes in version 1.1:");
            about.AppendLine("- Added support for variables in job configuration.");
            about.AppendLine("- Bug fix: Application crash when opening the Add job dialog.");
            about.AppendLine("- Bug fix: Browse button didn't work in the Edit job dialog.");
            MessageDisplayer.Tell(about.ToString(), $"About JobRunner {Application.ProductVersion}");
        }

        private void addVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;
            using var x = new AddVariableDialog
            {
                Variables = Variables,
                Jobs = Jobs
            };
            if (x.ShowDialog(this) != DialogResult.OK)
                return;
            SaveVariables();
        }

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var x = new ShowLogDialog
                {
                    Log = _logger
                };
                x.ShowDialog(this);
                AssignLogger();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Failed to open log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AssignLogger()
        {
            _logger = new Logger();
        }

        private void grid1_ShowContextMenu(object sender, GuiComponents.ContextMenuEventArgs e) =>
            contextMenuStrip1.Show(grid1, new System.Drawing.Point(e.X, e.Y));

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var job = grid1.SelectedJob;

            if (job == null)
            {
                viewJobToolStripMenuItem.Enabled = false;
                editJobToolStripMenuItem1.Enabled = false;
            }
            else
            {
                viewJobToolStripMenuItem.Enabled = true;
                editJobToolStripMenuItem1.Enabled = Config.IsAdministrator;
            }
        }

        private void viewJobToolStripMenuItem_Click(object sender, EventArgs e) =>
            ViewJob();

        private void editJobToolStripMenuItem1_Click(object sender, EventArgs e) =>
            EditJobToolStripMenuItem_Click(sender, e);
    }
}