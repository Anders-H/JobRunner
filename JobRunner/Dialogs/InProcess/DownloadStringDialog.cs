using JobRunner.ObjectModel.InProcess;
using JobRunner.Utils;
using System;
using System.Windows.Forms;
using JobRunner.GuiComponents;
using JobRunner.Logging;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.ObjectModel.InProcess.Jobs.Arguments;
using JobRunner.Services;

namespace JobRunner.Dialogs.InProcess
{
    public partial class DownloadStringDialog : Form
    {
        private readonly ILogger _log;
        public string JobIdentifierString { get; private set; }
        public string Arguments { get; private set; }

        public DownloadStringDialog(ILogger log, string jobIdentifierString, string arguments)
        {
            _log = log;
            JobIdentifierString = jobIdentifierString;
            Arguments = arguments;
            InitializeComponent();
        }

        private void DownloadStringDialog_Load(object sender, EventArgs e)
        {
            cboFileExistsBehaviour.Items.Add("Skip - do not download anything");
            cboFileExistsBehaviour.Items.Add("Overwrite the existing file");
            cboFileExistsBehaviour.Items.Add("Fail");
            cboFileExistsBehaviour.SelectedIndex = 0;

            var args = new ArgumentList(Arguments ?? "");
            if (args.Count <= 0)
                return;

            var deleteFileArguments = new DownloadStringArguments(args);

            txtSourceUrl.Text = deleteFileArguments.SourceUrl;
            txtTargetFile.Text = deleteFileArguments.TargetFile;
            var existsBehaviour = args.GetAfter(DownloadStringArguments.ExistsBehaviour);
            cboFileExistsBehaviour.SelectedIndex = existsBehaviour.ToLower() switch
            {
                DownloadStringArguments.ExistsBehaviourOverwrite => 1,
                DownloadStringArguments.ExistsBehaviourFail => 2,
                _ => 0
            };
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtSourceUrl.Text = txtSourceUrl.Text.Trim();

            if (string.IsNullOrEmpty(txtSourceUrl.Text))
            {
                MessageDisplayer.Tell("Source URL is mandatory.", Text);
                txtSourceUrl.Focus();
                return;
            }

            if (!txtSourceUrl.ValidateDashAndQuotes("Source URL", Text))
                return;

            txtTargetFile.Text = txtTargetFile.Text.Trim();

            if (string.IsNullOrEmpty(txtTargetFile.Text))
            {
                MessageDisplayer.Tell("Target file is mandatory.", Text);
                txtTargetFile.Focus();
                return;
            }

            if (!txtTargetFile.ValidateDashAndQuotes("Target file", Text))
                return;

            JobIdentifierString = new InProcessJobIdentifierHelper(_log)
                .GetIdentifierString(
                    InProcessJobIdentifier.DownloadString
                );

            Arguments = DownloadStringArguments.CreateArgumentString(txtSourceUrl.Text, txtTargetFile.Text, GetExistsBehaviour());
            
            DialogResult = DialogResult.OK;
        }

        private FileExistsBehaviour GetExistsBehaviour() =>
            cboFileExistsBehaviour.SelectedIndex switch
            {
                0 => FileExistsBehaviour.Skip,
                1 => FileExistsBehaviour.Overwrite,
                _ => FileExistsBehaviour.Fail
            };

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var x = new OpenFileDialog
            {
                Title = @"Target file",
                Filter = @"All files (*.*)|*.*",
                FileName = txtTargetFile.Text
            };

            if (x.ShowDialog(this) == DialogResult.OK)
                txtTargetFile.Text = x.FileName;
        }
    }
}