using System.Windows.Forms;
using JobRunner.ObjectModel;
using JobRunner.Services;

namespace JobRunner.Dialogs
{
    public partial class AddVariableDialogSmall : Form
    {
        public IVariableList Variables { get; set; }
        
        public AddVariableDialogSmall()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            txtVariableName.Text = new VariableName(txtVariableName.Text)
                .GetCleanName();
            
            txtVariableValue.Text = new VariableName(txtVariableValue.Text)
                .GetCleanValue();

            if (string.IsNullOrWhiteSpace(txtVariableName.Text))
            {
                MessageBox.Show(
                    @"The variable must have a valid name.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            if (Variables.HasVariable(txtVariableName.Text))
            {
                MessageBox.Show(
                    @"The variable name already exists.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }

            Variables.Add(txtVariableName.Text, txtVariableValue.Text);
            
            DialogResult = DialogResult.OK;
        }

        private void txtVariableName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtVariableName.Text = new VariableName(txtVariableName.Text)
                .GetCleanName();
        }

        private void txtVariableValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtVariableValue.Text = new VariableName(txtVariableValue.Text)
                .GetCleanValue();
        }
    }
}