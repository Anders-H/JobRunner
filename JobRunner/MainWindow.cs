using System;
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
            runToolStripMenuItem.Enabled = false;
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
            runToolStripMenuItem.Enabled = false;
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
            runToolStripMenuItem.Enabled = true;
            grid1.Running = false;
            grid1.Invalidate();
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
            Jobs.Load();
            if (!Jobs.LoadSuccess)
            {
                Cursor = Cursors.Default;
                lblStatus.Text = @"Load failed.";
                var failMessage = string.IsNullOrWhiteSpace(Jobs.LoadFailedMessage)
                    ? "An unknown error has occured."
                    : Jobs.LoadFailedMessage;
                MessageBox.Show($@"{failMessage}

The application will close.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            grid1.Initialize(Jobs);
            lblStatus.Text = @"Done.";
            Cursor = Cursors.Default;
        }
    }
}