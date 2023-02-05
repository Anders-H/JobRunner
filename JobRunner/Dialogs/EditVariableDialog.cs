#nullable enable
using System;
using System.Linq;
using System.Windows.Forms;
using JobRunner.ObjectModel;
using JobRunner.Utils;

namespace JobRunner.Dialogs
{
    public partial class EditVariableDialog : Form
    {
        private readonly Variable _variable;
        private readonly IVariableList _variables;

        public EditVariableDialog(Variable variable, IVariableList variables)
        {
            _variable = variable;
            _variables = variables;
            InitializeComponent();
        }

        private void EditVariableDialog_Load(object sender, EventArgs e)
        {
            txtVariableName.Text = _variable.Name;
            txtVariableValue.Text = _variable.Value;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtVariableName.Text = txtVariableName.Text.Trim();
            txtVariableValue.Text = txtVariableValue.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtVariableName.Text))
            {
                MessageDisplayer.Tell("The variable must have a name.", Text);
                txtVariableName.Focus();
                return;
            }

            if (!NameIsUnique())
            {
                MessageDisplayer.Tell("The variable name is not unique.", Text);
                txtVariableName.Focus();
                return;
            }

            _variable.Name = txtVariableName.Text;
            _variable.Value = txtVariableValue.Text;
            DialogResult = DialogResult.OK;
        }

        private bool NameIsUnique() =>
            _variables.All.Where(v => v != _variable).All(v => !v.Is(txtVariableName.Text));
    }
}