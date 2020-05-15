using System;
using System.Windows.Forms;
using JobRunner.Logging;

namespace JobRunner.Dialogs
{
    public partial class ShowLogDialog : Form
    {
        public ILogger Log { get; set; }

        public ShowLogDialog()
        {
            InitializeComponent();
        }

        private void ShowLogDialog_Shown(object sender, EventArgs e)
        {
            Refresh();
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            var file = Log.GetFile();
            lblFolder.Text = file.Directory?.FullName ?? "";
            lblFile.Text = file.Name;
            lblStatus.Text = file.Exists
                ? $@"Status: Ok ({file.Length:n0} bytes)"
                : "Status: Missing";
            lblFile.Enabled = file.Exists;
            Cursor = Cursors.Default;
        }

        private void lblFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Log.GetFile().Directory?.FullName ?? "");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Failed to open folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Log.GetFile().FullName);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Failed to open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}