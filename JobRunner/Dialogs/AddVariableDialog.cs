using System;
using System.Linq;
using System.Windows.Forms;
using JobRunner.Dialogs.ViewList;
using JobRunner.ObjectModel;
using JobRunner.Services;

namespace JobRunner.Dialogs
{
    public partial class AddVariableDialog : Form
    {
        public IVariableList Variables { get; set; }
        public IJobList Jobs { get; set; }

        public AddVariableDialog()
        {
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
        
        private void AddVariableDialog_Shown(object sender, EventArgs e)
        {
            var listDescriptor = new SimpleListDescriptor
            {
                WindowTitle = "Variables",
                PrimaryColumnTitle = "Variable",
                SecondaryColumnTitle = "Usage (job name)"
            };
            listDescriptor.AddRange(
                from variable
                in Variables.All
                let jobs = Jobs.GetVariableUsage(variable)
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