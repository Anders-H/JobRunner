using System;
using JobRunner.Logging;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs.Arguments;

public class DownloadStringArguments : TypedArgumentsBase
{
    public const string Source = "-source";
    public const string Target = "-target";
    public const string ExistsBehaviour = "-existsbehaviour";
    public const string ExistsBehaviourOverwrite = "overwrite";
    public const string ExistsBehaviourSkip = "skip";
    public const string ExistsBehaviourFail = "fail";

    public DownloadStringArguments(ArgumentList args) : base(args)
    {
    }

    public static string CreateArgumentString(string sourceUrl, string targetFile, FileExistsBehaviour fileExistsBehaviour) =>
        $@"{Source} ""{sourceUrl}"" {Target} ""{targetFile}"" {ExistsBehaviour} {fileExistsBehaviour.ToString().ToLower()}";

    public string SourceUrl =>
        Arguments.GetAfter(Source);

    public string TargetFile =>
        Arguments.GetAfter(Target);

    public FileExistsBehaviour GetFileExistsBehaviour()
    {
        var v = Arguments.GetAfter(ExistsBehaviour);

        return v.ToLower() switch
        {
            ExistsBehaviourOverwrite => FileExistsBehaviour.Overwrite,
            ExistsBehaviourSkip => FileExistsBehaviour.Skip,
            ExistsBehaviourFail => FileExistsBehaviour.Fail,
            _ => throw new SystemException($"Value out of range after {ExistsBehaviour}.")
        };
    }

    public FileExistsBehaviour TryGetFileExistsBehaviour(ILogger log)
    {
        try
        {
            return GetFileExistsBehaviour();
        }
        catch
        {
            log.AppendLog($"Using default behaviour (skip) on {ExistsBehaviour}.");
            return FileExistsBehaviour.Skip;
        }
    }
}