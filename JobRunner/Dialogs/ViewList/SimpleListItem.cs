using System.Windows.Forms;

namespace JobRunner.Dialogs.ViewList
{
    public class SimpleListItem
    {
        private string PrimaryValue { get; }
        private string SecondaryValue { get; }

        public SimpleListItem(string primaryValue, string secondaryValue)
        {
            PrimaryValue = primaryValue;
            SecondaryValue = secondaryValue;
        }

        public void Add(ListView listView)
        {
            var item = listView.Items.Add(PrimaryValue);
            item.SubItems.Add(SecondaryValue);
        }
    }
}