namespace JobRunner
{
    public class VersionHistory
    {
        public static string GetVersionHistory() =>
            @"Changes in version 1.6:

- Individual jobs can be disabled.

Changes in version 1.5:

- New jobs get a suggested sequence number.
- The user interface allows for editing and deleting variables.
- Lots of bug fixes.

Changes in version 1.4:

- A bug in the job argument reader is fixed.
- Incorrect use of failure behaviour is logged.
- Ability to stop a running script is added.
- When started as administrator, auto-run is always disabled.

Changes in version 1.3:

- Variables can be added on the fly, from any view.
- Three in-process tasks are added: ""Delete a file"", ""Download text"" and ""Binary upload"".
- Non-administrators can view or start a job by double clicking on it.
- Context menu added.";
    }
}