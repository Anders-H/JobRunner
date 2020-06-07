using System;
using System.Windows.Forms;
using JobRunner.Services;

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
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using var x = new OpenFileDialog
            {
                Title = @"File to delete",
                Filter = @"All files (*.*)|*.*"
            };

            if (x.ShowDialog(this) == DialogResult.OK)
                txtTargetFile.Text = x.FileName;
        }
    }
}