using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobRunner
{
    public class VersionHistory
    {
        public static string GetVersionHistory() =>
            @"Changes in version 1.3:

- Variables can be added on the fly, from any view.
- Two in-process tasks are added: ""Delete a file"", ""Download text"" and ""Binary upload"".
- Non-administrators can view or start a job by double clicking on it.
- Context menu added.

Changes in version 1.2:

- Minor improvments in the user interface (added icons, more options in the Add job dialog).
- Non administrators can open the Options dialog in read only mode.
- Logging to file added.

Changes in version 1.1:

- Added support for variables in job configuration.
- Bug fix: Application crash when opening the Add job dialog.
- Bug fix: Browse button didn't work in the Edit job dialog.";
    }
}