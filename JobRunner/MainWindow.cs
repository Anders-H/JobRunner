#nullable enable
using System;
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
        private readonly ILogger _log;
        private readonly MainWindowController _controller;
        private bool _cancelFlag;
        private IJobList Jobs { get; }
        private IVariableList Variables { get; } = new VariableList();
        private bool CleanExit { get; set; }
        
        public MainWindow()
        {
            _log = new Logger();
            Jobs = new JobList(_log);
            _cancelFlag = false;
            InitializeComponent();
            _controller = new MainWindowController();
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
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_controller.TellUserIfProgramHasJobs(Jobs, Text))
                return;

            _cancelFlag = false;
            lblStatus.Text = @"Running...";
            _controller.SetGuiToRunningState(runToolStripMenuItem, runSelectedJobToolStripMenuItem, stopToolStripMenuItem, true);
            Jobs.ResetJobs();
            grid1.RunSingle = -1;
            grid1.Running = true;
            grid1.Invalidate();
            var action = new Action(RunJobs);
            action.BeginInvoke(RunJobsCompleted, null);
        }

        private void RunSelectedJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_controller.TellUserIfProgramHasJobs(Jobs, Text))
                return;

            if (grid1.SelectedJob == null)
            {
                MessageDisplayer.Tell(@"No job is selected.", Text);
                return;
            }

            _cancelFlag = false;
            lblStatus.Text = @"Running...";
            _controller.SetGuiToRunningState(runToolStripMenuItem, runSelectedJobToolStripMenuItem, stopToolStripMenuItem, true);
            Jobs.ResetJobs();
            grid1.RunSingle = grid1.SelectedCells[0].RowIndex;
            grid1.Running = true;
            grid1.Invalidate();
            var action = new Action(RunJobs);
            action.BeginInvoke(RunJobsCompleted, null);
        }

        public void RunJobsCompleted(IAsyncResult ar) =>
            Invoke(new Action(RunJobsCompletedActions));

        private void RunJobsCompletedActions()
        {
            _controller.RunJobsCompleted(this, CleanExit, lblStatus, Jobs, grid1);
            _controller.SetGuiToRunningState(runToolStripMenuItem, runSelectedJobToolStripMenuItem, stopToolStripMenuItem, false);
        }

        private void RunJobs()
        {
            CleanExit = true;
            var jobIndex = 0;
            foreach (var job in Jobs.All)
            {
                if (_cancelFlag)
                {
                    CleanExit = false;
                    break;
                }

                if (grid1.RunSingle < 0 || grid1.RunSingle == jobIndex)
                {
                    job.Status = JobStatus.Running;
                    lblStatus.Text = grid1.RunSingle < 0
                        ? $@"Running step {job.RowIndex + 1} of {Jobs.Count}..."
                        : "Running single job...";
                    grid1.Invalidate();
                    job.Run(_log, grid1, Variables);
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
                    ? "An unknown error has occurred."
                    : Jobs.LoadFailedMessage;
                
                var t = new StringBuilder();
                var logTextProducer = new LogTextProducer();
                logTextProducer.AppendMessage(t, failMessage);

                if (!Config.IsAdministrator)
                    logTextProducer.AppendCloseMessage(t);

                MessageDisplayer.Yell(t.ToString(), Text);
                
                if (!Config.IsAdministrator)
                    Close();
            }

            if (Config.IsAdministrator && !string.IsNullOrWhiteSpace(Jobs.LoadFailedMessage))
                MessageDisplayer.Yell(Jobs.LoadFailedMessage, Text);
            
            grid1.Initialize(Jobs);
            _controller.InitializeStatus(this, lblStatus);

            Cursor = Cursors.Default;
            
            if (Config.AutoStart && !Config.IsAdministrator && AutoActionDialog.CheckAutoStart(this))
                runToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        private void AddJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;

            var suggestedSequenceNumber = Jobs.Count > 0 ? Jobs.LastJob.Number + 1 : 1;

            using var x = new AddJobDialog(this, Jobs, Variables, _log, suggestedSequenceNumber);

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
        
        private void DeleteJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var x = new DeleteJobDialog();
            x.Jobs = Jobs;
            x.InitiallySelectedJob = grid1.SelectedJob;

            if (x.ShowDialog(this) == DialogResult.OK)
            {
                grid1.RemoveJob(grid1.SelectedRow);
                Jobs.RemoveJob(x.SelectedJob!);
                grid1.Refresh();
                SaveJobs();
            }
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

            ViewJob();
        }

        private void ViewJob()
        {
            if (grid1.SelectedJob == null)
                return;

            using var x = new ViewJobDialog(grid1.SelectedJob);

            var result = x.ShowDialog(this);

            if (result == DialogResult.OK)
                RunSelectedJobToolStripMenuItem_Click(grid1, EventArgs.Empty);
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
            
            using var x = new EditJobDialog(this)
            {
                Job = grid1.SelectedJob,
                Variables = Variables,
                Jobs = Jobs,
                SaveVariables = _controller.SaveVariables
            };

            if (x.ShowDialog(this) != DialogResult.OK)
                return;

            grid1.Refresh();
            SaveJobs();
        }

        private void MoveJobUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.MoveJobUp(Jobs, grid1);
            SaveJobs();
        }

        private void MoveJobDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.MoveJobDown(Jobs, grid1);
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
            var listDescriptor = new SimpleListDescriptor(
                "Jobs",
                "Job",
                "Variables"
            );

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

            using var x = new SimpleListDialog(this, Variables, Jobs);

            x.ShowListDialog(this, listDescriptor, null);
        }

        private void variablesToolStripMenuItem_Click(object sender, EventArgs e) =>
            _controller.ShowVariables(this, Variables, Jobs);
        
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e) =>
            MessageDisplayer.Tell(
                VersionHistory.GetVersionHistory(),
                $"About JobRunner {Application.ProductVersion}"
            );

        private void addVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;

            using var x = new AddVariableDialog(Variables, Jobs);
            
            if (x.ShowDialog(this) != DialogResult.OK)
                return;

            _controller.SaveVariables(this, Variables);
        }

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using var x = new ShowLogDialog
                {
                    Log = _log
                };
                x.ShowDialog(this);
            }
            catch (Exception exception)
            {
                MessageDisplayer.Yell(exception.Message, @"Failed to open log");
            }
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
                runThisJobToolStripMenuItem.Enabled = false;
            }
            else
            {
                viewJobToolStripMenuItem.Enabled = true;
                editJobToolStripMenuItem1.Enabled = Config.IsAdministrator;
                runThisJobToolStripMenuItem.Enabled = true;
            }
        }

        private void viewJobToolStripMenuItem_Click(object sender, EventArgs e) =>
            ViewJob();

        private void editJobToolStripMenuItem1_Click(object sender, EventArgs e) =>
            EditJobToolStripMenuItem_Click(sender, e);

        private void runThisJobToolStripMenuItem_Click(object sender, EventArgs e) =>
            RunSelectedJobToolStripMenuItem_Click(sender, e);

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopToolStripMenuItem.Enabled = false;
            _cancelFlag = true;
            _controller.RunJobsCompleted(this, false, lblStatus, Jobs, grid1);
        }

        private void editVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var variable = SelectVariableDialog.GetVariable(this, "Select a variable to edit", Variables, Jobs);

            if (variable == null)
                return;
        }

        private void deleteVariableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var variable = SelectVariableDialog.GetVariable(this, "Select a variable to delete", Variables, Jobs);

            if (variable == null)
                return;

            if (!MessageDisplayer.Ask($@"Are you sure you want to delete the variable ""{variable.Name}""?", "Delete variable"))
                return;

            Variables.Delete(variable.Name);
            _controller.SaveVariables(this, Variables);
        }
    }
}