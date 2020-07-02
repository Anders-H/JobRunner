using System;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class DeleteFileArguments
    {
        public const string File = "-file";
        public const string NotFoundBehaviour = "-notfoundbehaviour";
        private readonly ArgumentList _args;

        public DeleteFileArguments(ArgumentList args)
        {
            _args = args;
        }

        public string GetFilename() =>
            _args.GetAfter(File);

        public FileNotFoundBehaviour GetFileNotFoundBehaviour()
        {
            var v = _args.GetAfter(NotFoundBehaviour);

            return v.ToLower() switch
            {
                "skip" => FileNotFoundBehaviour.Skip,
                "fail" => FileNotFoundBehaviour.Fail,
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

    public enum FileNotFoundBehaviour
    {
        Skip,
        Fail
    }
}