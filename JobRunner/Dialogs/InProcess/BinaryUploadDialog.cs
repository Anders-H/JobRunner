using System;
using System.Windows.Forms;
using JobRunner.GuiComponents;
using JobRunner.ObjectModel.InProcess;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.ObjectModel.InProcess.Jobs.Arguments;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs.InProcess
{
    public partial class BinaryUploadDialog : Form
    {
        public string JobIdentiftyerString { get; set; }
        public string Arguments { get; set; }

        public BinaryUploadDialog()
        {
            InitializeComponent();
        }

        private void BinaryUploadDialog_Load(object sender, EventArgs e)
        {
            cboUploadFailedBehaviour.Items.Add("Skip - do not upload anything");
            cboUploadFailedBehaviour.Items.Add("Fail");
            cboUploadFailedBehaviour.SelectedIndex = 0;

            var args = new ArgumentList(Arguments ?? "");
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
            using var x = new OpenFileDialog
            {
                Title = @"File to upload",
                Filter = @"All files (*.*)|*.*",
                FileName = txtSourceFile.Text
            };

            if (x.ShowDialog(this) == DialogResult.OK)
                txtSourceFile.Text = x.FileName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtSourceFile.Text = txtSourceFile.Text.Trim();
            if (string.IsNullOrEmpty(txtSourceFile.Text))
            {
                MessageDisplayer.Tell("Source file is mandatory.", Text);
                txtSourceFile.Focus();
                return;
            }

            if (!txtSourceFile.ValidateDashAndQuotes("Source file", Text))
                return;

            txtTarget.Text = txtTarget.Text.Trim();
            if (!txtTarget.ValidateDashAndQuotes("Target", Text))
                return;

            if (!txtUsername.ValidateDashAndQuotes("Username", Text))
                return;

            if (!txtPassword.ValidateDashAndQuotes("Password", Text))
                return;

            JobIdentiftyerString = new InProcessJobIdentifyerHelper()
                .GetIdentifyerString(
                    InProcessJobIdentifyer.BinaryUpload
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
    }
}