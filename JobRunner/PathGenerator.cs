using System;
using System.IO;
using System.Reflection;
using JobRunner.ObjectModel;

namespace JobRunner
{
    public class PathGenerator
    {
        private const string JobRunnerFolderName = "JobRunnerJobListFile";
        private readonly JobFileLocation _location;

        public PathGenerator(JobFileLocation location)
        {
            _location = location;
        }

        public string GetDataFile(string name)
        {
            switch (_location)
            {
                case JobFileLocation.Application:
                    var executingFile = new FileInfo(Assembly.GetExecutingAssembly().Location);
                    return Path.Combine(executingFile.Directory?.FullName ?? "", name);
                case JobFileLocation.UserSettings:
                    var profile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    var folder = new DirectoryInfo(Path.Combine(profile, JobRunnerFolderName));

                    if (!folder.Exists)
                        folder.Create();

                    return Path.Combine(folder.FullName, name);
                default:
                    throw new SystemException("Configuration error. Visit https://github.com/Anders-H/JobRunner for more information.");
            }
        }

        public static FileInfo GetLogFile()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = new DirectoryInfo(Path.Combine(appData, JobRunnerFolderName));
            
            if (!folder.Exists)
                folder.Create();

            return new FileInfo(Path.Combine(folder.FullName, "JobRunnerApplicationLog.log"));
        }

        public static string GetLogFileDirectoryName() =>
            GetLogFile().Directory?.FullName ?? "";
    }
}