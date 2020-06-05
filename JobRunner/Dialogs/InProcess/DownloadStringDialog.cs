using JobRunner.ObjectModel.InProcess;
using JobRunner.Utils;
using System;
using System.Windows.Forms;
using JobRunner.Services;

namespace JobRunner.Dialogs.InProcess
{
    public partial class DownloadStringDialog : Form
    {
        public string JobIdentiftyerString { get; set; }
        public string Arguments { get; set; }

        public DownloadStringDialog()
        {
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
            txtSourceUrl.Text = args.GetAfter("-source");
            txtTargetFile.Text = args.GetAfter("-target");
            var existsBehaviour = args.GetAfter("-existsbehaviour");
            cboFileExistsBehaviour.SelectedIndex = existsBehaviour.ToLower() switch
            {
                "overwrite" => 1,
                "fail" => 2,
                _ => 0
            };
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var sourceUrl = txtSourceUrl.Text.Trim();
            txtSourceUrl.Text = sourceUrl;
            if (sourceUrl == "")
            {
                MessageDisplayer.Tell("Source URL is mandatory.", Text);
                txtSourceUrl.Focus();
                return;
            }
            if (sourceUrl.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell("Source URL cannot contain quotes.", Text);
                txtSourceUrl.Focus();
                return;
            }
            if (sourceUrl.StartsWith("-"))
            {
                MessageDisplayer.Tell("Source URL cannot start with \"-\".", Text);
                txtSourceUrl.Focus();
                return;
            }

            var targetFile = txtTargetFile.Text.Trim();
            txtTargetFile.Text = targetFile;
            if (targetFile == "")
            {
                MessageDisplayer.Tell("Target file is mandatory.", Text);
                txtTargetFile.Focus();
                return;
            }
            if (targetFile.IndexOf("\"", StringComparison.Ordinal) >= 0)
            {
                MessageDisplayer.Tell("Target file cannot contain quotes.", Text);
                txtTargetFile.Focus();
                return;
            }

            var helper = new InProcessJobIdentifyerHelper();
            JobIdentiftyerString = helper.GetIdentifyerString(InProcessJobIdentifyer.DownloadString);
            Arguments = $@"-source ""{sourceUrl}"" -target ""{targetFile}"" -existsbehaviour {GetExistsBehaviour()}";
            DialogResult = DialogResult.OK;
        }

        private string GetExistsBehaviour() =>
            cboFileExistsBehaviour.SelectedIndex switch
            {
                0 => "skip",
                1 => "overwrite",
                _ => "fail"
            };

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var x = new OpenFileDialog();
            x.Filter = "All files (*.*)|*.*";
            if (x.ShowDialog(this) == DialogResult.OK)
                txtTargetFile.Text = x.FileName;
        }
    }
}