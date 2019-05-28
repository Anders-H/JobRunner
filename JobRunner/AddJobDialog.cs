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
    }
}