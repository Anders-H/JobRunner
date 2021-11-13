using System;
using System.Windows.Forms;
using JobRunner.ObjectModel.InProcess;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.ObjectModel.InProcess.Jobs.Arguments;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs.InProcess
{
    public partial class DeleteFileDialog : Form
    {
        public string JobIdentiftyerString { get; set; }
        public string Arguments { get; set; }

        public DeleteFileDialog()
        {
            InitializeComponent();
        }

        private void DeleteFileDialog_Load(object sender, EventArgs e)
        {
            cboFileNotFoundBehaviour.Items.Add("Skip - do not delete anything");
            cboFileNotFoundBehaviour.Items.Add("Fail");
            cboFileNotFoundBehaviour.SelectedIndex = 0;

            var args = new ArgumentList(Arguments ?? "");
            if (args.Count <= 0)
                return;

            var deleteFileArguments = new DeleteFileArguments(args);

            txtTargetFile.Text = deleteFileArguments.GetFilename();

            cboFileNotFoundBehaviour.SelectedIndex = deleteFileArguments.TryGetFileNotFoundBehaviour() switch
            {
                FileNotFoundBehaviour.Fail => 1,
                _ => 0
            };
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var x = new OpenFileDialog
            {
                Title = @"File to delete",
                Filter = @"All files (*.*)|*.*",
                FileName = txtTargetFile.Text
            };

            if (x.ShowDialog(this) == DialogResult.OK)
                txtTargetFile.Text = x.FileName;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var file = txtTargetFile.Text.Trim();
            txtTargetFile.Text = file;
            if (file == "")
            {
                MessageDisplayer.Tell("File to delete is mandatory.", Text);
                txtTargetFile.Focus();
                return;
            }
            if (file.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell("File to delete cannot contain quotes.", Text);
                txtTargetFile.Focus();
                return;
            }
            if (file.StartsWith("-"))
            {
                MessageDisplayer.Tell("File to delete cannot start with \"-\".", Text);
                txtTargetFile.Focus();
                return;
            }

            var helper = new InProcessJobIdentifyerHelper();
            JobIdentiftyerString = helper.GetIdentifyerString(InProcessJobIdentifyer.DeleteFile);
            Arguments = DeleteFileArguments.CreateArgumentString(file, GetFileNotFoundBehaviour());
            DialogResult = DialogResult.OK;
        }

        public FileNotFoundBehaviour GetFileNotFoundBehaviour() =>
            cboFileNotFoundBehaviour.SelectedIndex switch
            {
                0 => FileNotFoundBehaviour.Skip,
                _ => FileNotFoundBehaviour.Fail
            };
    }
}