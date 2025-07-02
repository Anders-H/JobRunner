using System.Windows.Forms;

namespace JobRunner.Dialogs.ViewList;

public class SimpleListItem
{
    private string PrimaryValue { get; }
    private string SecondaryValue { get; }
    public string ValueToCopy { get; }
        
    public SimpleListItem(string primaryValue, string secondaryValue, string valueToCopy)
    {
        PrimaryValue = primaryValue;
        SecondaryValue = secondaryValue;
        ValueToCopy = valueToCopy;
    }

    public void Add(ListView listView)
    {
        var item = listView.Items.Add(PrimaryValue);
        item.Tag = ValueToCopy;
        item.SubItems.Add(SecondaryValue);
    }
}