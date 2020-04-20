using System.IO;

namespace JobRunner.Logging
{
    public class Logger : ILogger
    {
        public void DeleteLog(string filename)
        {
            
        }

        public void AppendLog(string filename, string content)
        {
            
        }

        public FileInfo GetFile()
        {
            return new FileInfo("Hellpo");
        }
    }
}