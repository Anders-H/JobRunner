using System;
using System.Windows.Forms;
using JobRunner.Utils;

namespace JobRunner.Dialogs.ViewList
{
    public partial class SimpleListDialog : Form
    {
        private SimpleListDescriptor _descriptor;
        private Action<SimpleListDescriptor> _addAction; 
        
        private SimpleListDialog()
        {
            InitializeComponent();
        }

        public static DialogResult ShowListDialog(IWin32Window owner, SimpleListDescriptor descriptor, Action<SimpleListDescriptor> addAction)
        {
            using var x = new SimpleListDialog
            {
                _descriptor = descriptor,
                _addAction = addAction
            };
            return x.ShowDialog(owner);
        }

        private void SimpleListDialog_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = _addAction != null;
            Text = _descriptor.WindowTitle;
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
            _addAction.Invoke(_descriptor);
            RefreshList();
        }

        private void RefreshList()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            listView1.Columns[0].Text = _descriptor.PrimaryColumnTitle;
            listView1.Columns[1].Text = _descriptor.SecondaryColumnTitle;
            foreach (var item in _descriptor)
                item.Add(listView1);
            listView1.EndUpdate();
        }
    }
}