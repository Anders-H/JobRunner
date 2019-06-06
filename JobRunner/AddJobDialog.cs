using System;
using System.Globalization;
using System.Windows.Forms;

namespace JobRunner
{
    public partial class AddJobDialog : Form
    {
        public JobList Jobs { private get; set; }

        public AddJobDialog()
        {
            InitializeComponent();
        }

        private void AddJobDialog_Shown(object sender, EventArgs e)
        {
            Refresh();
            tabControl1.Focus();
            txtNumber.Focus();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = ValidateForm(true);
            if (tabControl1.SelectedTab == tabPageOverview)
                ConstructOverview();
            btnBack.Enabled = tabControl1.SelectedIndex > 0;
            btnNext.Enabled = tabControl1.SelectedIndex < tabControl1.TabCount - 1;
        }

        private void ConstructOverview()
        {
            tvOverview.BeginUpdate();
            tvOverview.Nodes.Clear();
            AddItemToOverview("Sequence:",
                ValidateSequenceNumber(true)
                    ? SequenceAsString(CalculateSequence())
                    : "(Invalid value - must be corrected)");
            AddItemToOverview("Name:", 
                ValidateJobName(true)
                    ? txtName.Text
                    : "(Invalid name - must be corrected)");
            AddItemToOverview("Program:", txtProgram.Text);
            AddItemToOverview("Arguments:",
                string.IsNullOrWhiteSpace(txtArguments.Text)
                    ? "(No arguments)"
                    : txtArguments.Text);
            AddItemToOverview("Timeout:",
                ValidateTimeout(true)
                    ? ((TimeSpan)cboTimeout.SelectedItem).ToString()
                    : "(Invalid value - must be corrected)");
            AddItemToOverview("Window:", chkHidden.Checked ? "Hidden" : "Visible");
            tvOverview.ExpandAll();
            tvOverview.EndUpdate();
        }

        private void AddItemToOverview(string text, string value) =>
            tvOverview.Nodes.Add(text).Nodes.Add(value);

        private int CalculateSequence()
        {
            int.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var s);
            if (s <= Jobs.FirstSequenceNumber)
                s = 1;
            else if (s > Jobs.LastSequenceNumber)
                s = Jobs.Count + 1;
            return s;
        }

        private string SequenceAsString(int s)
        {
            if (Jobs.Count <= 0 || s == 1)
                return "First job";
            return s > Jobs.Count
                ? "Last job"
                : $"Position {s}";
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex <= 0)
                return;
            tabControl1.SelectedIndex--;
            btnBack.Enabled = tabControl1.SelectedIndex > 0;
            btnNext.Enabled = tabControl1.SelectedIndex < tabControl1.TabCount - 1;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex >= tabControl1.TabCount - 1)
                return;
            tabControl1.SelectedIndex++;
            btnBack.Enabled = tabControl1.SelectedIndex > 0;
            btnNext.Enabled = tabControl1.SelectedIndex < tabControl1.TabCount - 1;
        }

        private bool ValidateForm(bool quiet)
        {
            if (!ValidateSequenceNumber(quiet))
                return false;
            if (!ValidateJobName(quiet))
                return false;
            if (!ValidateProgram(quiet))
                return false;
            if (!ValidateTimeout(quiet))
                return false;
            return true;
        }

        private bool ValidateSequenceNumber(bool quiet)
        {
            if (int.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out _))
                return true;
            MessageDisplayer.ShowValidationError(@"The field ""Sequence number"" must be an integer.", Text, quiet);
            return false;
        }

        private bool ValidateJobName(bool quiet)
        {
            txtName.Text = txtName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(txtName.Text))
                return true;
            MessageDisplayer.ShowValidationError(@"The field ""Job name"" cannot be empty.", Text, quiet);
            return false;
        }

        private bool ValidateProgram(bool quiet)
        {
            txtProgram.Text = txtProgram.Text.Trim();
            if (!string.IsNullOrWhiteSpace(txtProgram.Text))
                return true;
            MessageDisplayer.ShowValidationError(@"The field ""Program to run"" cannot be empty.", Text, quiet);
            return false;
        }

        private bool ValidateTimeout(bool quiet)
        {
            if (cboTimeout.SelectedItem != null)
                return true;
            MessageDisplayer.ShowValidationError(@"The field ""Timeout"" cannot be empty.", Text, quiet);
            return false;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(false))
                return;
            Jobs.InsertJob(CreateJob());
            DialogResult = DialogResult.OK;
        }

        private Job CreateJob()
        {
            int.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var nr);
            return new Job(
                nr,
                txtName.Text,
                txtProgram.Text,
                txtArguments.Text,
                (TimeSpan)cboTimeout.SelectedItem,
                chkHidden.Checked);
        }

        private void AddJobDialog_Load(object sender, EventArgs e)
        {
            var timeout = new TimeSpan(0, 0, 0, 30, 0);
            for (var i = 0; i < 30; i++)
            {
                cboTimeout.Items.Add(timeout);
                if (i < 29)
                    timeout = timeout.Add(new TimeSpan(0, 0, 0, 30, 0));
            }
            for (var i = 0; i < 30; i++)
            {
                timeout = timeout.Add(new TimeSpan(0, 0, 1, 0, 0));
                cboTimeout.Items.Add(timeout);
            }
            timeout = new TimeSpan(0, 1, 0, 0, 0);
            for (var i = 0; i < 20; i++)
            {
                cboTimeout.Items.Add(timeout);
                timeout = timeout.Add(new TimeSpan(0, 1, 0, 0, 0));
            }
            cboTimeout.SelectedIndex = 1;
        }
    }
}