using JobRunner.ObjectModel;
using System;
using System.Windows.Forms;

namespace JobRunner.Dialogs
{
    public partial class ViewJobDialog : Form
    {
        public bool Running { get; set; }
        public Job Job { get; }

        public ViewJobDialog(Job job)
        {
            Job = job;
            InitializeComponent();
        }

        private void ViewJobDialog_Load(object sender, EventArgs e)
        {
            tvOverview.BeginUpdate();
            tvOverview.Nodes.Clear();
            btnRun.Enabled = !Running;

            AddItemToOverview("Sequence:", Job.Number.ToString());
            AddItemToOverview("Name:", Job.Name);
            AddItemToOverview("Program:", Job.Command);
            AddItemToOverview("Arguments:", Job.Arguments);
            AddItemToOverview("Timeout:", $"{Job.Timeout.TotalMinutes:00}:{Job.Timeout.Seconds:00}{(Job.RetryCount > 0 ? $" (retry count: {Job.RetryCount})" : " (without retries)")}");
            AddItemToOverview("Window:", Job.Hidden ? "Hidden" : "Visible");
            AddItemToOverview("Flow control:", Job.BreakOnError ? "Break on error" : "Continue on error");
            AddItemToOverview("Start time:", Job.StartTime.HasValue ? Job.StartTime.ToString() : "[Not run]");
            AddItemToOverview("End time:", Job.StartTime.HasValue ? Job.EndTime.ToString() : "[Not run]");
            AddItemToOverview("Exit code:", Job.StartTime.HasValue ? Job.ExitCode.ToString() : "[Not run]");
            AddItemToOverview("Fail message:", Job.StartTime.HasValue ? Job.FailMessage : "[Not run]");

            tvOverview.ExpandAll();
            tvOverview.EndUpdate();
        }

        private void AddItemToOverview(string text, string value) =>
            tvOverview.Nodes.Add(text).Nodes.Add(value);

        private void btnRun_Click(object sender, EventArgs e) =>
            DialogResult = DialogResult.OK;
    }
}