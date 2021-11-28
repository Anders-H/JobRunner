#nullable enable
using System;
using System.Windows.Forms;
using JobRunner.ObjectModel;
using JobRunner.Utils;

namespace JobRunner.Dialogs.ViewList
{
    public partial class SimpleListDialog : Form
    {
        private readonly MainWindow _parent;
        private readonly IVariableList _variables;
        private readonly IJobList _jobs;
        private SimpleListDescriptor? _descriptor;
        private AddVariableDelegate? _addAction;

        public SimpleListDialog(MainWindow parent, IVariableList variables, IJobList jobs)
        {
            _parent = parent;
            _variables = variables;
            _jobs = jobs;
            InitializeComponent();
        }

        public DialogResult ShowListDialog(IWin32Window owner, SimpleListDescriptor descriptor, AddVariableDelegate addAction)
        {
            using var x = new SimpleListDialog(_parent, _variables, _jobs)
            {
                _descriptor = descriptor,
                _addAction = addAction
            };
            return x.ShowDialog(owner);
        }

        private void SimpleListDialog_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = _addAction != null;
            Text = _descriptor!.WindowTitle ?? "";
            RefreshList();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            var valueToCopy = listView1.SelectedItems[0].Tag as string ?? "";
            Clipboard.SetText(valueToCopy);
            MessageDisplayer.Tell($"Copied to clipboard: {valueToCopy}", Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_addAction == null)
                return;
            _addAction.Invoke(_parent, _variables, _jobs, _descriptor);
            RefreshList();
        }

        private void RefreshList()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.Columns[0].Text = _descriptor?.PrimaryColumnTitle ?? "";
            listView1.Columns[1].Text = _descriptor?.SecondaryColumnTitle ?? "";

            if (_descriptor != null)
                foreach (var item in _descriptor)
                    item.Add(listView1);

            listView1.EndUpdate();
        }
    }
}