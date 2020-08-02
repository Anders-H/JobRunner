using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs.Arguments
{
    public class DownloadStringArguments : TypedArgumentsBase
    {
        public const string Source = "-source";
        public const string Target = "-target";
        public const string ExistsBehaviour = "-existsbehaviour";
        public const string ExistsBehaviourOverwrite = "overwrite";
        public const string ExistsBehaviourFail = "fail";

        public DownloadStringArguments(ArgumentList args) : base(args)
        {
        }

        public static string CreateArgumentString(string sourceUrl, string targetFile, FileExistsBehaviour fileExistsBehaviour) =>
            $@"-source ""{sourceUrl}"" -target ""{targetFile}"" -existsbehaviour {fileExistsBehaviour.ToString().ToLower()}";

        public string SourceUrl =>
            Arguments.GetAfter(Source);

        public string TargetFile =>
            Arguments.GetAfter(Target);
    }
}