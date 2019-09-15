using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using JobRunner.ObjectModel;

namespace JobRunner.Utils
{
    public static class Config
    {
        private static JobFileLocation? _jobFileLocation;
        private static bool? _isAdministrator;
        public static bool AutoStart { get; set; }
        public static bool AutoClose { get; set; }

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
                    _jobFileLocation = JobFileLocation.Application;
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

        private static JobFileLocation JobFileLocation
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
                    var folder = new DirectoryInfo(Path.Combine(profile, "JobRunnerJobListFile"));
                    if (!folder.Exists)
                        folder.Create();
                    return Path.Combine(folder.FullName, "jobs.xml");
                default:
                    throw new SystemException("Configuration error. Visit https://github.com/Anders-H/JobRunner for more information.");
            }
        }
        
        public static string GetVariableFilePath()
        {
            switch (JobFileLocation)
            {
                case JobFileLocation.Application:
                    var executingFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
                    return Path.Combine(executingFile.Directory?.FullName ?? "", "variables.xml");
                case JobFileLocation.UserSettings:
                    var profile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var folder = new DirectoryInfo(Path.Combine(profile, "JobRunnerJobListFile"));
                    if (!folder.Exists)
                        folder.Create();
                    return Path.Combine(folder.FullName, "variables.xml");
                default:
                    throw new SystemException("Configuration error. Visit https://github.com/Anders-H/JobRunner for more information.");
            }
        }

        public static bool IsAdministrator
        {
            get
            {
                if (_isAdministrator.HasValue)
                    return _isAdministrator.Value;
                using var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                _isAdministrator = principal.IsInRole(WindowsBuiltInRole.Administrator);
                return _isAdministrator.Value;
            }
        }
    }
}