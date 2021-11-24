# JobRunner
**JobRunner** is a program that provide an interface for running and monitoring other processes. This can be used for downloading and uploading RSS feeds, for generating and uploading static web pages or anything that need to be run in a specific order.

![JobRunner screenshot](http://imghost.winsoft.se/upload/857971586771823jobrunner.jpg "JobRunner screenshot")

When **JobRunner** is started as administrator, the job list is editable from within the application. The application cannot run without a job list. If you don't want to start **JobRunner** as administrator, the job list can still be edited manually.

The default location for the job list file is:

C:\Users\\*username*\AppData\Roaming\JobRunnerJobListFile\jobs.xml

This can be changed by editing the `JobRunner.exe.config` file.

Download version 1.2: [JobRunner.exe](http://winsoft.se/files/JobRunner.exe)

**Version history:**

Changes in version 1.3:

- Variables can be added on the fly, from any view.
- Three in-process tasks are added: "Delete a file", "Download text" and "Binary upload".
- Non-administrators can view or start a job by double clicking on it.
- Context menu added.

Changes in version 1.2:

- Minor improvments in the user interface (added icons, more options in the Add job dialog).
- Non administrators can open the Options dialog in read only mode.
- Logging to file added.

Changes in version 1.1:

- Added support for variables in job configuration.
- Bug fix: Application crash when opening the Add job dialog.
- Bug fix: Browse button didn't work in the Edit job dialog.
