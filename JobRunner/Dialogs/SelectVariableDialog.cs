using System.Linq;
using JobRunner.ObjectModel;
using System.Windows.Forms;
using JobRunner.Dialogs.ViewList;
using JobRunner.Utils;

namespace JobRunner.Dialogs;

public partial class SelectVariableDialog : Form
{
    private readonly IVariableList _variables;
    private readonly IJobList _jobs;
    private Variable? _selectedVariable;

    public SelectVariableDialog(IVariableList variables, IJobList jobs)
    {
        _variables = variables;
        _jobs = jobs;
        InitializeComponent();
    }

    public static Variable? GetVariable(IWin32Window owner, string text, IVariableList variables, IJobList jobs)
    {
        var x = new SelectVariableDialog(variables, jobs);
        x.Text = text;

        if (x.ShowDialog(owner) != DialogResult.OK)
            return null;

        return x._selectedVariable;
    }

    private void SelectVariableDialog_Shown(object sender, System.EventArgs e)
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
                variable.Name
            )
        );

        listView1.BeginUpdate();
        listView1.Columns[0].Text = listDescriptor.PrimaryColumnTitle;
        listView1.Columns[1].Text = listDescriptor.SecondaryColumnTitle;

        foreach (var item in listDescriptor)
            item.Add(listView1);

        listView1.EndUpdate();
    }

    private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        listView1.SelectedItems.Clear();
        var li = listView1.GetItemAt(e.X, e.Y);

        if (li == null)
            return;

        li.Selected = true;

        ReturnSelectedVariable();
    }

    private void btnOk_Click(object sender, System.EventArgs e)
    {
        if (!ReturnSelectedVariable())
            MessageDisplayer.Tell(this, "No variable selected.", Text);
    }

    private bool ReturnSelectedVariable()
    {
        if (listView1.SelectedItems.Count == 0)
            return false;

        var variableName = listView1.SelectedItems[0].Tag as string;

        if (string.IsNullOrWhiteSpace(variableName))
            return false;

        _selectedVariable = _variables.GetVariable(variableName ?? "");

        if (_selectedVariable == null)
            return false;

        DialogResult = DialogResult.OK;

        return true;
    }
}