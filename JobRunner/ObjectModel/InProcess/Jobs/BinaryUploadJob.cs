using System;
using System.IO;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.ObjectModel.InProcess.Jobs.Arguments;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class BinaryUploadJob : InProcessJob
    {
        private string SourceFile { get; set; }


        public override void Begin(ArgumentList args)
        {
            var uploadArguments = new BinaryUploadArguments(args);

            SourceFile = uploadArguments.GetSourceFile();
            Target = uploadArguments.GetTarget();
            Username = uploadArguments.GetUsername();
            Password = uploadArguments.GetPassword();

            switch (uploadArguments.GetFailedBehaviour())
            {
                case FileNotFoundBehaviour.Skip:
                    if (!File.Exists(SourceFile))
                    {
                        HasExited = true;
                        ExitCode = 0;
                        return;
                    }
                    break;
                case FileNotFoundBehaviour.Fail:
                    if (!File.Exists(SourceFile))
                    {
                        HasExited = true;
                        ExitCode = 1;
                        throw new SystemException("File not found.");
                    }
                    break;
            }
        }

        protected override void CompletedCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}