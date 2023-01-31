#nullable enable
using System;
using System.Windows.Forms;
using JobRunner.Logging;
using JobRunner.Utils;

namespace JobRunner.Dialogs
{
    public partial class ShowLogDialog : Form
    {
        public ILogger Log { get; set; }

        public ShowLogDialog()
        {
            if (Log == null)
                throw new SystemException($"Uninitialized property: {nameof(Log)}");

            InitializeComponent();
        }

        private void ShowLogDialog_Shown(object sender, EventArgs e)
        {
            Refresh();
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            var file = PathGenerator.GetLogFile();
            
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
                System.Diagnostics.Process.Start(PathGenerator.GetLogFileDirectoryName());
            }
            catch (Exception exception)
            {
                MessageDisplayer.Yell(exception.Message, @"Failed to open folder");
            }
        }

        private void lblFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(PathGenerator.GetLogFile().FullName);
            }
            catch (Exception exception)
            {
                MessageDisplayer.Yell(exception.Message, @"Failed to open file");
            }
        }
    }
}