using System;
using System.Windows.Forms;

namespace JobRunner.Dialogs.InProcess
{
    public partial class DownloadStringDialog : Form
    {
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
        }
    }
}