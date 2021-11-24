#nullable enable
using System;
using System.Windows.Forms;
using JobRunner.ObjectModel;
using JobRunner.Utils;

namespace JobRunner
{
    public partial class DeleteJobDialog : Form
    {
        public IJobList? Jobs { get; set; }
        public Job? InitiallySelectedJob { get; set; }

        public DeleteJobDialog()
        {
            InitializeComponent();
        }

        private void DeleteJobDialog_Load(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                btnDelete.Enabled = false;

            if (Jobs!.Count <= 0)
            {
                btnDelete.Enabled = false;
                return;
            }

            for (var i = 0; i < Jobs.Count; i++)
                lstJobs.Items.Add(Jobs!.GetJob(i)!);

            SelectedJob = InitiallySelectedJob;
        }

        public Job? SelectedJob
        {
            get => lstJobs.SelectedItem as Job;
            private set => lstJobs.SelectedItem = value;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator || SelectedJob == null)
            {
                DialogResult = DialogResult.Cancel;
                return;
            }

            if (MessageDisplayer.Ask("Are you sure?", Text))
                DialogResult = DialogResult.OK;
        }

        private void lstJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = Config.IsAdministrator && SelectedJob != null;

            if (SelectedJob == null)
            {
                txtJob.Text = "";
                return;
            }

            txtJob.Text = SelectedJob.GetDescription();
        }
    }
}