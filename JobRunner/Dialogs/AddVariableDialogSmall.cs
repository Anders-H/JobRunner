using System.Windows.Forms;
using JobRunner.ObjectModel;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs
{
    public partial class AddVariableDialogSmall : Form
    {
        private readonly IVariableList _variables;
        
        public AddVariableDialogSmall(IVariableList variable)
        {
            _variables = variable;
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
                MessageDisplayer.Tell(@"The variable must have a valid name.", Text);
                return;
            }

            if (_variables.HasVariable(txtVariableName.Text))
            {
                MessageDisplayer.Tell(@"The variable name already exists.", Text);
                return;
            }

            _variables.Add(txtVariableName.Text, txtVariableValue.Text);
            
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