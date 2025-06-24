# JobRunner
**JobRunner** is a program that provide an interface for running and monitoring other processes. This can be used for downloading and uploading RSS feeds, for generating and uploading static web pages or anything that need to be run in a specific order.

![JobRunner screenshot](https://raw.githubusercontent.com/Anders-H/JobRunner/refs/heads/master/screenshot.jpg "JobRunner screenshot")

When **JobRunner** is started as administrator, the job list is editable from within the application. The application cannot run without a job list. If you don't want to start **JobRunner** as administrator, the job list can still be edited manually.

The default location for the job list file is:

`C:\Users\[Username]\AppData\Roaming\JobRunnerJobListFile\jobs.xml`

Or:

`%APPDATA%\JobRunnerJobListFile\jobs.xml`

This can be changed by editing the `JobRunner.exe.config` file.

Download setup program for version 1.7: [JobRunner.exe](http://winsoft.se/files/JobRunner.exe) (right click, Save as...)

## Version history:

**Changes in version 1.8:**

- Ability to set run conditions for jobs

**Changes in version 1.7:**

- Ability to set retry count
- Minor bug fixes

**Changes in version 1.6.1:**

- If a task name is nog given, it will be suggested
- Individual jobs can be disabled
- Dialogs for editing in process tasks are added

**Changes in version 1.5:**

- New jobs get a suggested sequence number
- The user interface allows for editing and deleting variables
- Lots of bug fixes

**Changes in version 1.4:**

- A bug in the job argument reader is fixed
- Incorrect use of failure behaviour is logged
- Ability to stop a running script is added
- When started as administrator, auto-run is always disabled

**Changes in version 1.3:**

- Variables can be added on the fly, from any view
- Three in-process tasks are added: "Delete a file", "Download text" and "Binary upload"
- Non-administrators can view or start a job by double clicking on it
- Context menu added

**Changes in version 1.2:**

- Minor improvments in the user interface (added icons, more options in the Add job dialog)
- Non administrators can open the Options dialog in read only mode
- Logging to file added

**Changes in version 1.1:**

- Added support for variables in job configuration
- Bug fix: Application crash when opening the Add job dialog
- Bug fix: Browse button didn't work in the Edit job dialog
