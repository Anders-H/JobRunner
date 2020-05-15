using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using JobRunner.Utils;

namespace JobRunner.Logging
{
    public class Logger : ILogger
    {
        public bool DeleteLog()
        {
            var file = GetFile();
            if (!file.Exists)
                return true;
            
            try
            {
                file.Delete();
                Application.DoEvents();
                file = GetFile();
                return !file.Exists;
            }
            catch
            {
                return false;
            }
        }

        public bool AppendLog(string content)
        {
            var file = GetFile();
            try
            {
                using var sw = new StreamWriter(file.FullName, true, Encoding.UTF8);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public FileInfo GetFile()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = new DirectoryInfo(Path.Combine(appData, Config.JobRunnerFolderName));
            if (!folder.Exists)
                folder.Create();
            return new FileInfo(Path.Combine(folder.FullName, "JobRunnerApplicationLog.log"));
        }
    }
}