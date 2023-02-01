#nullable enable
using System;
using System.Linq;
using System.Windows.Forms;
using JobRunner.Dialogs.ViewList;
using JobRunner.ObjectModel;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.Dialogs
{
    public partial class AddVariableDialog : Form
    {
        private readonly IVariableList _variables;
        private readonly IJobList _jobs;

        public AddVariableDialog(IVariableList variables, IJobList jobs)
        {
            _variables = variables;
            _jobs = jobs;
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
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
        
        private void AddVariableDialog_Shown(object sender, EventArgs e)
        {
            var listDescriptor = new SimpleListDescriptor(
                "Variables",
                "Variable",
                "Usage (job name)"
            );

            listDescriptor.AddRange(
                from variable
                in _variables.All
                let jobs = _jobs.GetVariableUsage(variable)
                select new SimpleListItem(
                    $"\"{variable.Name}\"=\"{variable.Value}\"",
                    jobs.Names,
                    $"[{variable.Name}]"
                )
            );

            listView1.BeginUpdate();
            listView1.Columns[0].Text = listDescriptor.PrimaryColumnTitle;
            listView1.Columns[1].Text = listDescriptor.SecondaryColumnTitle;
            
            foreach (var item in listDescriptor)
                item.Add(listView1);
            
            listView1.EndUpdate();
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