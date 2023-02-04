#nullable enable
using System;
using System.Windows.Forms;
using JobRunner.ObjectModel;

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

        }
    }
}