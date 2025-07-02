using System;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs.Arguments;

public class DeleteFileArguments : TypedArgumentsBase
{
    public const string File = "-file";
    public const string NotFoundBehaviour = "-notfoundbehaviour";
    public const string NotFoundBehaviourSkip = "skip";
    public const string NotFoundBehaviourFail = "fail";

    public DeleteFileArguments(ArgumentList args) : base(args)
    {
    }

    public static string CreateArgumentString(string filename, FileNotFoundBehaviour fileNotFoundBehaviour) =>
        $@"{File} ""{filename}"" {NotFoundBehaviour} {fileNotFoundBehaviour.ToString().ToLower()}";

    public string GetFilename() =>
        Arguments.GetAfter(File);

    public FileNotFoundBehaviour GetFileNotFoundBehaviour()
    {
        var v = Arguments.GetAfter(NotFoundBehaviour);

        return v.ToLower() switch
        {
            NotFoundBehaviourSkip => FileNotFoundBehaviour.Skip,
            NotFoundBehaviourFail => FileNotFoundBehaviour.Fail,
            _ => throw new SystemException("Value out of range.")
        };
    }

    public FileNotFoundBehaviour TryGetFileNotFoundBehaviour()
    {
        try
        {
            return GetFileNotFoundBehaviour();
        }
        catch
        {
            return FileNotFoundBehaviour.Skip;
        }
    }
}