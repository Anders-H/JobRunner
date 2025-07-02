using System.IO;
using System.Text;
using System.Windows.Forms;

namespace JobRunner.Logging;

public class Logger : ILogger
{
    public bool DeleteLog()
    {
        var file = PathGenerator.GetLogFile();

        if (!file.Exists)
            return true;
            
        try
        {
            file.Delete();
            Application.DoEvents();
            file = PathGenerator.GetLogFile();
            return !file.Exists;
        }
        catch
        {
            return false;
        }
    }

    public bool AppendLog(string content)
    {
        var file = PathGenerator.GetLogFile();

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
}