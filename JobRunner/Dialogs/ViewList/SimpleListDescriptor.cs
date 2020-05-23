using System.Collections.Generic;

namespace JobRunner.Dialogs.ViewList
{
    public class SimpleListDescriptor : List<SimpleListItem>
    {
        public string WindowTitle { get; set; }
        public string PrimaryColumnTitle { get; set; }
        public string SecondaryColumnTitle { get; set; }
    }
}