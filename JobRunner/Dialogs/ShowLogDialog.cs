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
            Cursor = Cursors.Default;
        }
    }
}