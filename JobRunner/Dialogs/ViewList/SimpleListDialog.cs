using System.Windows.Forms;

namespace JobRunner.Dialogs.ViewList
{
    public partial class SimpleListDialog : Form
    {
        private SimpleListDescriptor _descriptor;

        public SimpleListDialog()
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

        private void SimpleListDialog_Load(object sender, System.EventArgs e)
        {
            Text = _descriptor.WindowTitle; 
            listView1.BeginUpdate();
            listView1.Columns[0].Text = _descriptor.PrimaryColumnTitle;
            listView1.Columns[1].Text = _descriptor.SecondaryColumnTitle;
            foreach (var item in _descriptor)
                item.Add(listView1);
            listView1.EndUpdate();
        }
    }
}