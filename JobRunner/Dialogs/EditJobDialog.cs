using System;
using System.Linq;
using System.Windows.Forms;
using JobRunner.Dialogs.ViewList;
using JobRunner.ObjectModel;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs;

public partial class EditJobDialog : Form
{
    private readonly MainWindow _parent;
    private readonly EditJobDialogController _controller;
    public Job? Job { get; set; }
    public IVariableList? Variables { get; set; }
    public IJobList? Jobs { get; set; }
    public SaveVariablesDelegate? SaveVariables { get; set; }
        
    public EditJobDialog(MainWindow parent)
    {
        _parent = parent;
        _controller = new EditJobDialogController(this);
        InitializeComponent();
    }

    private void EditJobDialog_Load(object sender, EventArgs e)
    {
        _controller.ThrowIfRequiredPropertiesAreNull();
        _controller.PopulateTimeout(cboTimeout);
        _controller.PopulateRunningConditions(cboRunIf, Job?.RunIf ?? JobRunConditionEnum.NoCondition);

        txtName.Text = Job!.Name;
        txtProgram.Text = Job.Command;
        txtArguments.Text = Job.Arguments;
        chkEnabled.Checked = Job.Enabled;
        chkHidden.Checked = Job.Hidden;
        chkBreakOnError.Checked = Job.BreakOnError;
        txtRunningConditionArgumentFile.Text = Job.RunIfArgument;

        cboRetryCount.Items.Add(0);
        cboRetryCount.Items.Add(1);
        cboRetryCount.Items.Add(2);
        cboRetryCount.Items.Add(3);
        cboRetryCount.Items.Add(4);
        cboRetryCount.Items.Add(5);

        var retryCountIndex = Job.RetryCount;

        if (retryCountIndex < 0)
            retryCountIndex = 0;
        else if (retryCountIndex > 5)
            retryCountIndex = 5;

        cboRetryCount.SelectedIndex = retryCountIndex;
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

        MessageDisplayer.ShowValidationError(this, @"The field ""Job name"" cannot be empty.", Text, quiet);
        return false;
    }

    private bool ValidateProgram(bool quiet)
    {
        txtProgram.Text = txtProgram.Text.Trim();

        if (!string.IsNullOrWhiteSpace(txtProgram.Text))
            return true;
            
        MessageDisplayer.ShowValidationError(this, @"The field ""Program to run"" cannot be empty.", Text, quiet);
        return false;
    }

    private bool ValidateTimeout(bool quiet)
    {
        if (cboTimeout.SelectedItem != null)
            return true;

        MessageDisplayer.ShowValidationError(this, @"The field ""Timeout"" cannot be empty.", Text, quiet);
        return false;
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (!ValidateForm(false))
            return;

        Job!.Name = txtName.Text;
        Job.Command = txtProgram.Text;
        Job.Arguments = txtArguments.Text;
        Job.Timeout = (TimeSpan)cboTimeout.SelectedItem;
        Job.Enabled = chkEnabled.Checked;
        Job.Hidden = chkHidden.Checked;
        Job.BreakOnError = chkBreakOnError.Checked;
        Job.RetryCount = cboRetryCount.SelectedIndex;
        Job.RunIf = JobRunConditionEnumHelper.FromFriendlyString((string)cboRunIf.SelectedItem);
        Job.RunIfArgument = txtRunningConditionArgumentFile.Text.Trim();
        DialogResult = DialogResult.OK;
    }

    private void BtnBrowse_Click(object sender, EventArgs e)
    {
        using var x = new OpenFileDialog();
        x.Filter = @"Executables (*.exe)|*.exe|All files (*.*)|*.*";
        x.Title = @"Select an executable file";

        if (x.ShowDialog(this) != DialogResult.OK)
            return;

        txtProgram.Text = x.FileName;
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
                in Variables!.All
            let jobs = Jobs!.GetVariableUsage(variable)
            select new SimpleListItem(
                $"\"{variable.Name}\"=\"{variable.Value}\"",
                jobs.Names,
                $"[{variable.Name}]"
            )
        );

        using var x = new SimpleListDialog(_parent, Variables, Jobs!);

        x.ShowListDialog(
            this,
            listDescriptor,
            Config.IsAdministrator ? (AddVariableDelegate)AddVariable : null
        );
    }

    private string AddVariable(MainWindow parent, IVariableList variables, IJobList jobList, SimpleListDescriptor descriptor)
    {
        using var x = new AddVariableDialogSmall(Variables!);

        x.ShowDialog(this);

        descriptor.Clear();

        descriptor.AddRange(
            from variable
                in Variables!.All
            let jobs = Jobs!.GetVariableUsage(variable)
            select new SimpleListItem(
                $"\"{variable.Name}\"=\"{variable.Value}\"",
                jobs.Names,
                $"[{variable.Name}]"
            )
        );

        SaveVariables!(_parent, Variables);

        return x.NewVariableName;
    }
        
    private void txtArguments_TextChanged(object sender, EventArgs e)
    {
        if (Variables == null)
        {
            txtArgsEvaluated.Text = @"(Missing variable list.)";
            return;
        }

        if (Variables.Count <= 0)
        {
            txtArgsEvaluated.Text = txtArguments.Text.Trim();
            return;
        }

        txtArgsEvaluated.Text = new ArgumentDecoder(Variables)
            .GetDecodedText(txtArguments.Text);
    }

    private void btnBrowseConditionArg_Click(object sender, EventArgs e)
    {
        using var x = new OpenFileDialog();
        x.Title = @"Select a file for the running condition";
        x.Filter = @"All files (*.*)|*.*";
        x.FileName = txtRunningConditionArgumentFile.Text;

        if (x.ShowDialog(this) == DialogResult.OK)
            txtRunningConditionArgumentFile.Text = x.FileName.Trim();
    }

    private void cboRunIf_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRunningConditionArgumentFile.Enabled = cboRunIf.SelectedIndex > 0;
        btnBrowseConditionArg.Enabled = cboRunIf.SelectedIndex > 0;
    }
}