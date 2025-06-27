using System;
using System.Linq;
using System.Windows.Forms;
using JobRunner.Dialogs.ViewList;
using JobRunner.GuiComponents;
using JobRunner.Logging;
using JobRunner.ObjectModel;
using JobRunner.ObjectModel.InProcess;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.ObjectModel.InProcess.Jobs.Arguments;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs.InProcess;

public partial class AddBinaryUploadDialog : Form, IAddInProcess
{
    private readonly ILogger _log;
    public string JobIdentifierString { get; private set; }
    public string Arguments { get; private set; }
    public IVariableList? Variables { get; set; }
    public IJobList? Jobs { get; set; }

    public AddBinaryUploadDialog(ILogger log, string jobIdentifierString, string arguments)
    {
        _log = log;
        JobIdentifierString = jobIdentifierString;
        Arguments = arguments;
        InitializeComponent();
    }

    private void BinaryUploadDialog_Load(object sender, EventArgs e)
    {
        cboUploadFailedBehaviour.Items.Add("Skip - do not upload anything");
        cboUploadFailedBehaviour.Items.Add("Fail");
        cboUploadFailedBehaviour.SelectedIndex = 0;

        var args = new ArgumentList(Arguments);

        if (args.Count <= 0)
            return;

        var binaryUploadArguments = new BinaryUploadArguments(args);

        txtSourceFile.Text = binaryUploadArguments.GetSourceFile();
        txtTarget.Text = binaryUploadArguments.GetTarget();
        txtUsername.Text = binaryUploadArguments.GetUsername();
        txtPassword.Text = binaryUploadArguments.GetPassword();

        cboUploadFailedBehaviour.SelectedIndex = binaryUploadArguments.TryGetFailedBehaviour() switch
        {
            FileNotFoundBehaviour.Fail => 1,
            _ => 0
        };
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var x = new OpenFileDialog();
        x.Title = @"File to upload";
        x.Filter = @"All files (*.*)|*.*";
        x.FileName = txtSourceFile.Text;

        if (x.ShowDialog(this) == DialogResult.OK)
            txtSourceFile.Text = x.FileName;
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        txtSourceFile.Text = txtSourceFile.Text.Trim();
        if (string.IsNullOrEmpty(txtSourceFile.Text))
        {
            MessageDisplayer.Tell(this, "Source file is mandatory.", Text);
            txtSourceFile.Focus();
            return;
        }

        if (!txtSourceFile.ValidateDashAndQuotes(this, "Source file", Text))
            return;

        txtTarget.Text = txtTarget.Text.Trim();
        if (!txtTarget.ValidateDashAndQuotes(this, "Target", Text))
            return;

        if (!txtUsername.ValidateDashAndQuotes(this, "Username", Text))
            return;

        if (!txtPassword.ValidateDashAndQuotes(this, "Password", Text))
            return;

        JobIdentifierString = new InProcessJobIdentifierHelper(_log)
            .GetIdentifierString(
                InProcessJobIdentifier.BinaryUpload
            );
            
        Arguments = BinaryUploadArguments
            .CreateArgumentString(
                txtSourceFile.Text,
                txtTarget.Text,
                txtUsername.Text,
                txtPassword.Text,
                GetBinaryUploadFailedBehaviour()
            );
            
        DialogResult = DialogResult.OK;
    }

    private FileNotFoundBehaviour GetBinaryUploadFailedBehaviour() =>
        cboUploadFailedBehaviour.SelectedIndex switch
        {
            0 => FileNotFoundBehaviour.Skip,
            _ => FileNotFoundBehaviour.Fail
        };

    private void lblUsernameVariables_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Jobs ??= new JobList(_log);

        var listDescriptor = new SimpleListDescriptor(
            "Variables",
            "Variable",
            "Usage (job name)"
        );

            

        listDescriptor.AddRange(
            from variable in Variables!.All
            let jobs = Jobs.GetVariableUsage(variable)
            select new SimpleListItem(
                $"\"{variable.Name}\"=\"{variable.Value}\"",
                jobs.Names,
                $"[{variable.Name}]"
            )
        );

        using var x = new SimpleListDialog((MainWindow)Parent.Parent, Variables, Jobs);
        x.ShowListDialog(this, listDescriptor, null);
    }
}