﻿namespace JobRunner
{
    public class VersionHistory
    {
        public static string GetVersionHistory() =>
            @"Changes in version 1.7:

- Ability to set retry count
- Minor bug fixes

Changes in version 1.6.1:

- If a task name is nog given, it will be suggested
- Individual jobs can be disabled
- Dialogs for editing in process tasks are added

Changes in version 1.5:

- New jobs get a suggested sequence number
- The user interface allows for editing and deleting variables
- Lots of bug fixes

Changes in version 1.4:

- A bug in the job argument reader is fixed
- Incorrect use of failure behaviour is logged
- Ability to stop a running script is added
- When started as administrator, auto-run is always disabled

Changes in version 1.3:

- Variables can be added on the fly, from any view
- Three in-process tasks are added: ""Delete a file"", ""Download text"" and ""Binary upload""
- Non-administrators can view or start a job by double clicking on it
- Context menu added";
    }
}