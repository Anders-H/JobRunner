using System;
using System.Windows.Forms;
using JobRunner.Utils;

namespace JobRunner.Dialogs.ViewList
{
    public partial class SimpleListDialog : Form
    {
        private SimpleListDescriptor _descriptor;

        private SimpleListDialog()
        {
            InitializeComponent();
        }

        public static DialogResult ShowListDialog(IWin32Window owner, SimpleListDescriptor descriptor)
        {
            using var x = new SimpleListDialog
            {
                _descriptor = descriptor
            };
            return x.ShowDialog(owner);
        }

        private void SimpleListDialog_Load(object sender, EventArgs e)
        {
            Text = _descriptor.WindowTitle; 
            listView1.BeginUpdate();
            listView1.Columns[0].Text = _descriptor.PrimaryColumnTitle;
            listView1.Columns[1].Text = _descriptor.SecondaryColumnTitle;
            foreach (var item in _descriptor)
                item.Add(listView1);
            listView1.EndUpdate();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
                return;
            var valueToCopy = listView1.SelectedItems[0].Tag as string ?? "";
            Clipboard.SetText(valueToCopy);
            MessageDisplayer.Tell($"Copied to clipboard: {valueToCopy}", Text);
        }
    }
}