using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs.Arguments
{
    public class BinaryUploadArguments : TypedArgumentsBase
    {
        public const string SourceFile = "-source";
        public const string Target = "-target";
        public const string Username = "-username";
        public const string Password = "-password";
        public const string FailedBehaviour = "-failedbehaviour";
        public const string FailedBehaviourSkip = "skip";
        public const string FailedBehaviourFail = "fail";

        public BinaryUploadArguments(ArgumentList args) : base(args)
        {
        }

        public static string CreateArgumentString(string sourceFile, string target, string username, string password, FileNotFoundBehaviour failedBehaviour) =>
            $@"{SourceFile} ""{sourceFile}"" {Target} ""{target}"" {Username} ""{username}"" {Password} ""{password}"" {FailedBehaviour} {failedBehaviour.ToString().ToLower()}";

        public string GetSourceFile() =>
            Arguments.GetAfter(SourceFile);

    }
}