using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner.Dialogs.ViewList
{
    public class SimpleListDescriptor : List<SimpleListItem>
    {
        public string WindowTitle { get; set; }
        public string PrimaryColumnTitle { get; set; }
        public string SecondaryColumnTitle { get; set; }
    }
}