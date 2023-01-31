namespace JobRunner.Logging
{
    public interface ILogger
    {
        bool DeleteLog();
        bool AppendLog(string content);
    }
}