using System;
using System.Windows.Forms;
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
            var sourceFile = txtSourceFile.Text.Trim();
            txtSourceFile.Text = sourceFile;
            if (sourceFile == "")
            {
                MessageDisplayer.Tell("Source file is mandatory.", Text);
                txtSourceFile.Focus();
                return;
            }
            if (sourceFile.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell("Source file cannot contain quotes.", Text);
                txtSourceFile.Focus();
                return;
            }
            if (sourceFile.StartsWith("-"))
            {
                MessageDisplayer.Tell("Source file cannot start with \"-\".", Text);
                txtSourceFile.Focus();
                return;
            }

            txtTarget.Text = txtTarget.Text.Trim();
            if (txtTarget.Text.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell("Target cannot contain quotes.", Text);
                txtTarget.Focus();
                return;
            }
            if (txtTarget.Text.StartsWith("-"))
            {
                MessageDisplayer.Tell("Target cannot start with \"-\".", Text);
                txtTarget.Focus();
                return;
            }

            if (txtUsername.Text.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell("Username cannot contain quotes.", Text);
                txtUsername.Focus();
                return;
            }
            if (txtUsername.Text.StartsWith("-"))
            {
                MessageDisplayer.Tell("Username cannot start with \"-\".", Text);
                txtUsername.Focus();
                return;
            }

            if (txtPassword.Text.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell("Password cannot contain quotes.", Text);
                txtPassword.Focus();
                return;
            }
            if (txtPassword.Text.StartsWith("-"))
            {
                MessageDisplayer.Tell("Password cannot start with \"-\".", Text);
                txtPassword.Focus();
                return;
            }

            var helper = new InProcessJobIdentifyerHelper();
            JobIdentiftyerString = helper.GetIdentifyerString(InProcessJobIdentifyer.BinaryUpload);
            Arguments = BinaryUploadArguments.CreateArgumentString(sourceFile, txtTarget.Text, txtUsername.Text, txtPassword.Text, GetBinaryUploadFailedBehaviour());
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