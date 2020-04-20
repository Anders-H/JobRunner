using System.IO;

namespace JobRunner.Logging
{
    public interface ILogger
    {
        void DeleteLog(string filename);
        void AppendLog(string filename, string content);
        FileInfo GetFile();
    }
}