using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using JobRunner.Dialogs.InProcess;
using JobRunner.Dialogs.ViewList;
using JobRunner.ObjectModel;
using JobRunner.ObjectModel.InProcess;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs
{
    public partial class AddJobDialog : Form
    {
        private readonly MainWindow _parent;
        private readonly IJobList _jobs;
        private readonly IVariableList _variables;

        public AddJobDialog(MainWindow parent, IJobList jobs, IVariableList variables)
        {
            _parent = parent;
            _jobs = jobs;
            _variables = variables;
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
            AddItemToOverview("Program:",
                ValidateProgram(true)
                    ? txtProgram.Text
                    : "(Invalid value - must be corrected)");
            AddItemToOverview("Arguments:",
                string.IsNullOrWhiteSpace(txtArguments.Text)
                    ? "(No arguments)"
                    : txtArguments.Text);
            AddItemToOverview("Timeout:",
                ValidateTimeout(true)
                    ? ((TimeSpan)cboTimeout.SelectedItem).ToString()
                    : "(Invalid value - must be corrected)");
            AddItemToOverview("Window:", chkHidden.Checked ? "Hidden" : "Visible");
            AddItemToOverview("Flow control:", chkBreakOnError.Checked ? "Break on error" : "Continue on error");
            tvOverview.ExpandAll();
            tvOverview.EndUpdate();
        }

        private void AddItemToOverview(string text, string value) =>
            tvOverview.Nodes.Add(text).Nodes.Add(value);

        private int CalculateSequence()
        {
            int.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var s);
            if (s <= _jobs.FirstSequenceNumber)
                s = 1;
            else if (s > _jobs.LastSequenceNumber)
                s = _jobs.Count + 1;
            return s;
        }

        private string SequenceAsString(int s)
        {
            if (_jobs.Count <= 0 || s == 1)
                return "First job";
            return s > _jobs.Count
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
            _jobs.InsertJob(CreateJob());
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
                chkHidden.Checked,
                chkBreakOnError.Checked);
        }

        private void AddJobDialog_Load(object sender, EventArgs e)
        {
            var p = tvOverview.Parent.BackColor;
            tvOverview.BackColor = Color.FromArgb(p.R, p.G, p.B);
            foreach (var x in new TimeSpanList())
                cboTimeout.Items.Add(x);
            cboTimeout.SelectedIndex = 1;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using var x = new OpenFileDialog
            {
                Filter = @"Executables (*.exe)|*.exe|All files (*.*)|*.*",
                Title = @"Select an executable file"
            };
            if (x.ShowDialog(this) != DialogResult.OK)
                return;
            txtProgram.Text = x.FileName;
        }

        private void txtArguments_TextChanged(object sender, EventArgs e)
        {
            if (_variables == null)
            {
                txtArgsEvaluated.Text = @"(Missing variable list.)";
                return;
            }

            if (_variables.Count <= 0)
            {
                txtArgsEvaluated.Text = txtArguments.Text.Trim();
                return;
            }

            txtArgsEvaluated.Text = new ArgumentDecoder(_variables)
                .GetDecodedText(txtArguments.Text);
        }

        private void lblVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var listDescriptor = new SimpleListDescriptor(
                "Variables",
                "Variable",
                "Usage (job name)"
            );

            listDescriptor.AddRange(
                from variable
                in _variables.All
                let jobs = _jobs.GetVariableUsage(variable)
                select new SimpleListItem(
                    $"\"{variable.Name}\"=\"{variable.Value}\"",
                    jobs.Names,
                    $"[{variable.Name}]"
                )
            );

            using var x = new SimpleListDialog(_parent, _variables, _jobs);

            x.ShowListDialog(this, listDescriptor, null);
        }

        private void btnInProcess_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnInProcess, 0, btnInProcess.Height);
        }

        private void deleteAFileToolStripMenuItem_Click(object sender, EventArgs e) =>
            ShowInProcessDialog(typeof(DeleteFileDialog));

        private void downloadTextToolStripMenuItem_Click(object sender, EventArgs e) =>
            ShowInProcessDialog(typeof(DownloadStringDialog));

        private void binaryUploadToolStripMenuItem_Click(object sender, EventArgs e) =>
            ShowInProcessDialog(typeof(BinaryUploadDialog));

        private void ShowInProcessDialog(Type dialogType)
        {
            var helper = new InProcessJobIdentifyerHelper();
            var jobIdentiftyerString = helper.GetIdentifyerString(InProcessJobIdentifyer.DownloadString);
            var program = txtProgram.Text.Trim();

            var x = (Form)Activator.CreateInstance(dialogType);

            var argumentsProperty = x.GetType().GetProperty("Arguments");

            if (string.Compare(jobIdentiftyerString, program, StringComparison.InvariantCultureIgnoreCase) == 0)
                argumentsProperty!.SetValue(x, txtArguments.Text, null);

            if (x.ShowDialog(this) != DialogResult.OK)
                return;

            var jobIdentiftyerStringProperty = x.GetType().GetProperty("JobIdentiftyerString");
            txtProgram.Text = (jobIdentiftyerStringProperty!.GetValue(x, null) as string ?? "").Trim();
            txtArguments.Text = (argumentsProperty!.GetValue(x, null) as string ?? "").Trim();
        }
    }
}