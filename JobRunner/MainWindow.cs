using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JobRunner
{
    public partial class MainWindow : Form
    {
        private JobList Jobs { get; } = new JobList();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            lblStatus.Text = @"Initializing...";
            Cursor = Cursors.WaitCursor;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!HasJobs())
                return;
            lblStatus.Text = @"Running...";
            EnableGui(false);
            Jobs.Reset();
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
            if (grid1.SelectedCells.Count <= 0)
            {
                MessageBox.Show(@"No job is selected.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            lblStatus.Text = @"Running...";
            EnableGui(false);
            Jobs.Reset();
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
            lblStatus.Text = @"Done.";
            EnableGui(true);
            grid1.Running = false;
            grid1.Invalidate();
        }

        private void EnableGui(bool enabled)
        {
            runToolStripMenuItem.Enabled = enabled;
            runSelectedJobToolStripMenuItem.Enabled = enabled;
        }

        private void RunJobs()
        {
            var jobIndex = 0;
            foreach (var job in Jobs)
            {
                if (grid1.RunSingle < 0 || grid1.RunSingle == jobIndex)
                {
                    job.Status = JobStatus.Running;
                    lblStatus.Text = grid1.RunSingle < 0
                        ? $@"Running step {job.RowIndex + 1} of {Jobs.Count}..."
                        : "Running single job...";
                    grid1.Invalidate();
                    job.Run(grid1);
                    grid1.Invalidate();
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
            Jobs.Load();
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
                t.Append("The application will close.");
                if (!Config.IsAdministrator)
                {
                    t.AppendLine();
                    t.AppendLine();
                    t.AppendLine("To be able to edit the job list, start JobRunner as administrator.");
                }
                MessageBox.Show(t.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
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
        }

        private void AddJobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var x = new AddJobDialog
            {
                Jobs = Jobs
            };
            if (x.ShowDialog(this) != DialogResult.OK)
                return;
            grid1.Initialize(Jobs);
            SaveJobs();
        }

        private void SaveJobs()
        {
            try
            {
                using(var sw = new StreamWriter(Config.GetJobFilePath(), false, Encoding.UTF8))
                {
                    sw.Write(Jobs.GetXml());
                    sw.Flush();
                    sw.Close();
                }
            }
            catch
            {
                MessageDisplayer.Yell($@"Failed to save the file ""{Config.GetJobFilePath()}"".", Text);
            }
        }
    }
}