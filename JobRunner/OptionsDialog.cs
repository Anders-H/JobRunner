using System;
using System.Windows.Forms;

namespace JobRunner
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
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Config.AutoStart = chkAutoStart.Checked;
            Config.AutoClose = chkAutoExit.Checked;
            DialogResult = DialogResult.OK;
        }
    }
}