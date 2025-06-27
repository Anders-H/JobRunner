using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using JobRunner.Dialogs.InProcess;
using JobRunner.Dialogs.ViewList;
using JobRunner.Logging;
using JobRunner.ObjectModel;
using JobRunner.ObjectModel.InProcess;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs;

public partial class AddJobDialog : Form
{
    private readonly MainWindow _parent;
    private readonly IJobList _jobs;
    private readonly IVariableList? _variables;
    private readonly ILogger _log;
    private readonly int _suggestedSequenceNumber;

    public AddJobDialog(MainWindow parent, IJobList jobs, IVariableList variables, ILogger log, int suggestedSequenceNumber)
    {
        _parent = parent;
        _jobs = jobs;
        _variables = variables;
        _log = log;
        _suggestedSequenceNumber = suggestedSequenceNumber;
        InitializeComponent();
    }

    private void AddJobDialog_Shown(object sender, EventArgs e)
    {
        Refresh();
        tabControl1.Focus();
        txtNumber.Focus();
        txtNumber.Text = _suggestedSequenceNumber.ToString();

        foreach (var allFriendlyString in JobRunConditionEnumHelper.GetAllFriendlyStrings())
            cboRunIf.Items.Add(allFriendlyString);

        cboRunIf.SelectedIndex = 0;
    }

    private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabControl1.SelectedIndex >= tabControl1.TabCount - 1)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
                txtName.Text = SuggestName();
        }

        btnOk.Enabled = ValidateForm(true);

        if (tabControl1.SelectedTab == tabPageOverview)
            ConstructOverview();
            
        btnBack.Enabled = tabControl1.SelectedIndex > 0;
        btnNext.Enabled = tabControl1.SelectedIndex < tabControl1.TabCount - 1;
    }

    private string SuggestName()
    {
        var p = txtProgram.Text.Trim();

        txtProgram.Text = p;

        if (string.IsNullOrWhiteSpace(p))
            return "";

        if (p.StartsWith("@"))
        {
            switch (p)
            {
                case InProcessNames.DeleteFile:
                    return $"Delete a file: {txtArguments.Text}";
                case InProcessNames.DownloadString:
                    return $"Download a string of text: {txtArguments.Text}";
                case InProcessNames.BinaryUpload:
                    return $"Upload a binary file: {txtArguments.Text}";
                default:
                    return p;
            }
        }

        return $@"Run process: {p}";
    }

    private void ConstructOverview()
    {
        if (string.IsNullOrWhiteSpace(txtName.Text))
            txtName.Text = SuggestName();

        const string noGood = "(Invalid value - must be corrected)";
        tvOverview.BeginUpdate();
        tvOverview.Nodes.Clear();
        AddItemToOverview("Sequence:", ValidateSequenceNumber(true) ? SequenceAsString(CalculateSequence()) : noGood);
        AddItemToOverview("Name:", ValidateJobName(true) ? txtName.Text : noGood);
        AddItemToOverview("Program:", ValidateProgram(true) ? txtProgram.Text : noGood);
        AddItemToOverview("Arguments:", string.IsNullOrWhiteSpace(txtArguments.Text) ? "(No arguments)" : txtArguments.Text);
        AddItemToOverview("Timeout:", ValidateTimeout(true) ? ((TimeSpan)cboTimeout.SelectedItem).ToString() : noGood);
        AddItemToOverview("Window:", chkHidden.Checked ? "Hidden" : "Visible");
        var flowControlNode = AddItemToOverview("Flow control:", chkBreakOnError.Checked ? "Break on error" : "Continue on error");

        if (cboRunIf.SelectedIndex > 0)
            flowControlNode.Nodes.Add(cboRunIf.Text);

        tvOverview.ExpandAll();
        tvOverview.EndUpdate();
    }

    private TreeNode AddItemToOverview(string text, string value)
    {
        var n = tvOverview.Nodes.Add(text);
        n.Nodes.Add(value);
        return n;
    }

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
            
        MessageDisplayer.ShowValidationError(this, @"The field ""Sequence number"" must be an integer.", Text, quiet);
        return false;
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
        _jobs.InsertJob(CreateJob());
        DialogResult = DialogResult.OK;
    }

    private Job CreateJob()
    {
        int.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var nr);

        return new Job(
            _log,
            nr,
            txtName.Text,
            chkEnabled.Checked,
            txtProgram.Text,
            txtArguments.Text,
            (TimeSpan)cboTimeout.SelectedItem,
            chkHidden.Checked,
            chkBreakOnError.Checked,
            cboRetryCount.SelectedIndex,
            JobRunConditionEnumHelper.FromFriendlyString(cboRunIf.Text),
            txtRunningConditionArgumentFile.Text);
    }

    private void AddJobDialog_Load(object sender, EventArgs e)
    {
        var p = tvOverview.Parent.BackColor;
        tvOverview.BackColor = Color.FromArgb(p.R, p.G, p.B);
            
        foreach (var x in new TimeSpanList())
            cboTimeout.Items.Add(x);

        cboTimeout.SelectedIndex = 1;

        cboRetryCount.Items.Add(0);
        cboRetryCount.Items.Add(1);
        cboRetryCount.Items.Add(2);
        cboRetryCount.Items.Add(3);
        cboRetryCount.Items.Add(4);
        cboRetryCount.Items.Add(5);
        cboRetryCount.SelectedIndex = 0;
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
                in _variables!.All
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
        ShowInProcessDialog(typeof(AddDeleteFileDialog));

    private void downloadTextToolStripMenuItem_Click(object sender, EventArgs e) =>
        ShowInProcessDialog(typeof(AddDownloadStringDialog));

    private void binaryUploadToolStripMenuItem_Click(object sender, EventArgs e) =>
        ShowInProcessDialog(typeof(AddBinaryUploadDialog));

    private void ShowInProcessDialog(Type dialogType)
    {
        var helper = new InProcessJobIdentifierHelper(_log);
        var jobIdentifierString = helper.GetIdentifierString(InProcessJobIdentifier.DownloadString);
        var program = txtProgram.Text.Trim();

        var x = (Form)Activator.CreateInstance(dialogType, _log, jobIdentifierString, "");

        ((IAddInProcess)x).Jobs = _jobs;
        ((IAddInProcess)x).Variables = _variables;

        var argumentsProperty = x.GetType().GetProperty("Arguments");

        if (string.Compare(jobIdentifierString, program, StringComparison.InvariantCultureIgnoreCase) == 0)
            argumentsProperty!.SetValue(x, txtArguments.Text, null);

        if (x.ShowDialog(this) != DialogResult.OK)
            return;

        var jobIdentifierStringProperty = x.GetType().GetProperty("JobIdentifierString");
        txtProgram.Text = (jobIdentifierStringProperty!.GetValue(x, null) as string ?? "").Trim();
        txtArguments.Text = (argumentsProperty!.GetValue(x, null) as string ?? "").Trim();
    }

    private void cboRunIf_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtRunningConditionArgumentFile.Enabled = cboRunIf.SelectedIndex > 0;
        btnBrowseConditionArg.Enabled = txtRunningConditionArgumentFile.Enabled;
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
}