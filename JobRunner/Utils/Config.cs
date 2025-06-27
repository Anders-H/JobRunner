using System.Configuration;
using System.Security.Principal;
using System.Windows.Forms;
using JobRunner.ObjectModel;

namespace JobRunner.Utils;

public static class Config
{
    private static JobFileLocation _jobFileLocation;
    private static bool? _isAdministrator;
    public static bool AutoStart { get; set; }
    public static bool AutoClose { get; set; }
    public static bool EnableLogging { get; set; }
    public static bool TreatLoggingErrorsAsStepErrors { get; set; }
        
        
    public static bool Validate(IWin32Window owner)
    {
        var x = ConfigurationManager.AppSettings.Get("JobFileLocation");
        if (string.IsNullOrWhiteSpace(x))
        {
            MessageDisplayer.Yell(owner,
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
                MessageDisplayer.Yell(owner,
                    @"Configuration setting JobFileLocation has an invalid value. Visit https://github.com/Anders-H/JobRunner for more information.",
                    @"Configuration error");
                return false;
        }
    }

    public static string GetJobFilePath() =>
        new PathGenerator(_jobFileLocation).GetDataFile("jobs.xml");

    public static string GetVariableFilePath() =>
        new PathGenerator(_jobFileLocation).GetDataFile("variables.xml");

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