using System;
using System.Globalization;
using System.Windows.Forms;

namespace JobRunner
{
    public partial class AddJobDialog : Form
    {
        public AddJobDialog()
        {
            InitializeComponent();
        }

        private void AddJobDialog_Shown(object sender, EventArgs e)
        {
            Refresh();
            tabControl1.Focus();
            txtNumber.Focus();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = ValidateForm(true);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex <= 0)
                return;
            tabControl1.SelectedIndex--;
            btnBack.Enabled = tabControl1.SelectedIndex > 0;
            btnNext.Enabled = tabControl1.SelectedIndex < tabControl1.TabCount - 1;
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex >= tabControl1.TabCount - 1)
                return;
            tabControl1.SelectedIndex++;
            btnBack.Enabled = tabControl1.SelectedIndex > 0;
            btnNext.Enabled = tabControl1.SelectedIndex < tabControl1.TabCount - 1;
        }

        private bool ValidateForm(bool quiet)
        {
            if (!ValidateSequenceNumber(quiet))
                return false;
            if (!ValidateJobName(quiet))
                return false;
            return true;
        }

        private bool ValidateSequenceNumber(bool quiet)
        {
            if (int.TryParse(txtNumber.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out _))
                return true;
            MessageDisplayer.ShowValidationError("The field Sequence number must be an integer.", Text, quiet);
            return false;
        }

        private bool ValidateJobName(bool quiet)
        {
            txtName.Text = txtName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(txtName.Text))
                return true;
            MessageDisplayer.ShowValidationError("The field Job name cannot be empty.", Text, quiet);
            return false;
        }
    }
}