using System;
using System.IO;
#if !DEBUG
using System.Reflection;
#endif
using JobRunner.ObjectModel;

namespace JobRunner
{
    public class PathGenerator
    {
        private const string JobRunnerFolderName = "JobRunnerJobListFile";
#if !DEBUG
        private readonly JobFileLocation _location;
#endif

        public PathGenerator(JobFileLocation location)
        {
#if !DEBUG
            _location = location;
#endif
        }

        public string GetDataFile(string name)
        {
#if DEBUG
            return $@"..\..\{name}";
#else
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
#endif
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