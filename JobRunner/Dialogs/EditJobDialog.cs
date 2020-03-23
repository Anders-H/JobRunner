using System;
using System.Windows.Forms;
using JobRunner.ObjectModel;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs
{
    public partial class EditJobDialog : Form
    {
        public Job Job { get; set; }
        public IVariableList Variables { get; set; }

        public EditJobDialog()
        {
            InitializeComponent();
        }

        private void EditJobDialog_Load(object sender, EventArgs e)
        {
            foreach (var x in new TimeSpanList())
                cboTimeout.Items.Add(x);
            foreach (TimeSpan i in cboTimeout.Items)
            {
                if (i != Job.Timeout)
                    continue;
                cboTimeout.SelectedItem = i;
                break;
            }
            if (cboTimeout.SelectedIndex < 0)
            {
                cboTimeout.Items.Add(Job.Timeout);
                cboTimeout.SelectedIndex = cboTimeout.Items.Count - 1;
            }
            txtName.Text = Job.Name;
            txtProgram.Text = Job.Command;
            txtArguments.Text = Job.Arguments;
            chkHidden.Checked = Job.Hidden;
            chkBreakOnError.Checked = Job.BreakOnError;
        }

        private bool ValidateForm(bool quiet)
        {
            if (!ValidateJobName(quiet))
                return false;
            if (!ValidateProgram(quiet))
                return false;
            if (!ValidateTimeout(quiet))
                return false;
            return true;
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
            Job.Name = txtName.Text;
            Job.Command = txtProgram.Text;
            Job.Arguments = txtArguments.Text;
            Job.Timeout = (TimeSpan)cboTimeout.SelectedItem;
            Job.Hidden = chkHidden.Checked;
            Job.BreakOnError = chkBreakOnError.Checked;
            DialogResult = DialogResult.OK;
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

        private void lblVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void txtArguments_TextChanged(object sender, EventArgs e)
        {
            if (Variables == null)
                return;
            if (Variables.Count <= 0)
                return;
            txtArgsEvaluated.Text = new ArgumentDecoder(Variables)
                .GetDecodedText(txtArguments.Text);
        }
    }
}