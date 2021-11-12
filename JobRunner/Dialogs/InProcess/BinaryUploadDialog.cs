using System;
using System.Windows.Forms;
using JobRunner.Services;

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
            cboUploadFailedBehaviour.Items.Add("Skip - do not download anything");
            cboUploadFailedBehaviour.Items.Add("Fail");
            cboUploadFailedBehaviour.SelectedIndex = 0;

            var args = new ArgumentList(Arguments ?? "");
            if (args.Count <= 0)
                return;

            var binaryUploadArguments = new BinaryUploadArguments(args);


        }
    }
}
