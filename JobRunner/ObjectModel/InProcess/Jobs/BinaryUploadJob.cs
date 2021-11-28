using System;
using System.IO;
using System.Net;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.ObjectModel.InProcess.Jobs.Arguments;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class BinaryUploadJob : InProcessJob
    {
        public string SourceFile { get; set; }
        public string Target { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private UploadFileDelegate Action { get; set; }

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

            HasExited = false;
            Action = UploadFile;
            Action.BeginInvoke(SourceFile, Target, Username, Password, CompletedCallback, null);
        }

        private void UploadFile(string sourceFile, string target, string username, string password)
        {
            try
            {
                using var client = new WebClient
                {
                    Credentials = new NetworkCredential(username, password)
                };
                client.UploadFile(target, WebRequestMethods.Ftp.UploadFile, sourceFile);
                
            }
            catch (SystemException e)
            {
                Exception = e;
            }
        }

        protected override void CompletedCallback(IAsyncResult result)
        {
            HasExited = true;
            ExitCode = Exception == null
                ? 0
                : -1;
        }
    }
}