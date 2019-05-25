using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace JobRunner
{
    public static class Config
    {
        private static JobFileLocation? _jobFileLocation;

        public static bool Validate()
        {
            var x = ConfigurationManager.AppSettings.Get("JobFileLocation");
            if (string.IsNullOrWhiteSpace(x))
            {
                MessageBox.Show(
                    @"Missing configuration setting: JobFileLocation. Visit https://github.com/Anders-H/JobRunner for more information.",
                    @"Configuration error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            switch (x)
            {
                case "UserSettings":
                    _jobFileLocation = JobFileLocation.UserSettings;
                    return true;
                case "Application":
                    _jobFileLocation = JobFileLocation.UserSettings;
                    return true;
                default:
                    MessageBox.Show(
                        @"Configuration setting JobFileLocation has an invalid value. Visit https://github.com/Anders-H/JobRunner for more information.",
                        @"Configuration error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
            }
        }

        public static JobFileLocation JobFileLocation
        {
            get
            {
                if (_jobFileLocation == null)
                    Validate();
                return _jobFileLocation ?? JobFileLocation.UserSettings;
            }
        }

        public static string GetJobFilePath()
        {
            switch (JobFileLocation)
            {
                case JobFileLocation.Application:
                    var executingFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
                    return Path.Combine(executingFile.Directory?.FullName ?? "", "jobs.xml");
                case JobFileLocation.UserSettings:
                    var profile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var folder = new DirectoryInfo(Path.Combine(profile, "WinsoftJobRunnerJobSettingsFile"));
                    if (!folder.Exists)
                        folder.Create();
                    return Path.Combine(folder.FullName, "jobs.xml");
                default:
                    throw new SystemException("Configuration error. Visit https://github.com/Anders-H/JobRunner for more information.");
            }
        }
    }
}