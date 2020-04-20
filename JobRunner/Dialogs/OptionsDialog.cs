using System;
using System.Windows.Forms;
using JobRunner.Utils;

namespace JobRunner.Dialogs
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            chkAutoStart.Checked = Config.AutoStart;
            chkAutoExit.Checked = Config.AutoClose;
            chkLog.Checked = Config.EnableLogging;
            chkLogErrors.Checked = Config.TreatLoggingErrorsAsStepErrors;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Config.AutoStart = chkAutoStart.Checked;
            Config.AutoClose = chkAutoExit.Checked;
            Config.EnableLogging = chkLog.Checked;
            Config.TreatLoggingErrorsAsStepErrors = chkLogErrors.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}