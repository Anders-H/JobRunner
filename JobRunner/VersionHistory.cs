namespace JobRunner
{
    public class VersionHistory
    {
        public static string GetVersionHistory() =>
            @"Changes in version 1.8:

- Ability to set run conditions for jobs

Changes in version 1.7:

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
- When started as administrator, auto-run is always disabled";
    }
}