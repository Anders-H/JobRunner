using JobRunner.Dialogs.ViewList;
using JobRunner.ObjectModel;

namespace JobRunner
{
    public delegate void AddVariableDelegate(MainWindow parent, IVariableList variables, IJobList jobList, SimpleListDescriptor descriptor);
    public delegate void SaveVariablesDelegate(MainWindow parent, IVariableList variables);
    public delegate void UploadFileDelegate(string sourceFile, string target, string username, string password);
    public delegate void DeleteFileDelegate(string fileToDelete);
    public delegate void DownloadStringDelegate(string source, string target);
}