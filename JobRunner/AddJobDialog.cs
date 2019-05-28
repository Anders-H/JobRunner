using System;
using System.Windows.Forms;

namespace JobRunner
{
    public partial class AddJobDialog : Form
    {
        public AddJobDialog()
        {
            InitializeComponent();
        }

        private void AddJobDialog_Load(object sender, EventArgs e)
        {
            tabControl1.Focus();
            txtNumber.Focus();
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Hello!");
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPageBasic)
            {
                btnBack.Enabled = false;
                btnNext.Enabled = true;
                btnOk.Enabled = ValidateForm(true);
            }
            if (tabControl1.SelectedTab == tabPageProcess)
            {
                btnBack.Enabled = true;
                btnNext.Enabled = true;
                btnOk.Enabled = ValidateForm(true);
            }
            if (tabControl1.SelectedTab == tabPageOverview)
            {
                btnBack.Enabled = true;
                btnNext.Enabled = false;
                btnOk.Enabled = ValidateForm(true);
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (!ValidateForm(false))
                return;
        }
    }
}