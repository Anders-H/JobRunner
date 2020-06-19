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
            chkLogErrors.Enabled = Config.EnableLogging;

            if (!Config.IsAdministrator)
            {
                Text = @"Options (start JobRunner as administrator to edit)";
                chkAutoStart.Enabled = false;
                chkAutoExit.Enabled = false;
                chkLog.Enabled = false;
                chkLogErrors.Enabled = false;
                btnOk.Enabled = false;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;
            
            Config.AutoStart = chkAutoStart.Checked;
            Config.AutoClose = chkAutoExit.Checked;
            Config.EnableLogging = chkLog.Checked;
            Config.TreatLoggingErrorsAsStepErrors = chkLogErrors.Checked;
            DialogResult = DialogResult.OK;
        }

        private void chkLog_CheckedChanged(object sender, EventArgs e)
        {
            if (!Config.IsAdministrator)
                return;

            chkLogErrors.Enabled = chkLog.Checked;
        }
    }
}