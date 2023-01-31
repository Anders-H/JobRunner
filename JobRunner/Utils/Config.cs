using System.Configuration;
using System.Security.Principal;
using JobRunner.ObjectModel;

namespace JobRunner.Utils
{
    public static class Config
    {
        private static JobFileLocation? _jobFileLocation;
        private static bool? _isAdministrator;
        public static bool AutoStart { get; set; }
        public static bool AutoClose { get; set; }
        public static bool EnableLogging { get; set; }
        public static bool TreatLoggingErrorsAsStepErrors { get; set; }
        
        
        public static bool Validate()
        {
            var x = ConfigurationManager.AppSettings.Get("JobFileLocation");
            if (string.IsNullOrWhiteSpace(x))
            {
                MessageDisplayer.Yell(
                    @"Missing configuration setting: JobFileLocation. Visit https://github.com/Anders-H/JobRunner for more information.",
                    @"Configuration error");
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
                    MessageDisplayer.Yell(
                        @"Configuration setting JobFileLocation has an invalid value. Visit https://github.com/Anders-H/JobRunner for more information.",
                        @"Configuration error");
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

        public static string GetJobFilePath() =>
            new PathGenerator(JobFileLocation).GetDataFile("jobs.xml");

        public static string GetVariableFilePath() =>
            new PathGenerator(JobFileLocation).GetDataFile("variables.xml");

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