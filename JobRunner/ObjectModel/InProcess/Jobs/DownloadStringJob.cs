using System;
using System.IO;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class DownloadStringJob : InProcessJob
    {
        private string SourceUrl { get; set; }
        private string TargetFile { get; set; }
        private string ExistsBehavour { get; set; }
        private Action<string, string> Action { get; set; }
        
        public override void Begin(ArgumentList args)
        {
            SourceUrl = args.GetAfter("-source");
            TargetFile = args.GetAfter("-target");
            ExistsBehavour = args.GetAfter("-existsbehaviour");

            switch (ExistsBehavour.ToLower())
            {
                case "skip":
                    if (File.Exists(TargetFile))
                    {
                        HasExited = true;
                        ExitCode = 0;
                        return;
                    }
                    break;
                case "fail":
                    if (File.Exists(TargetFile))
                    {
                        HasExited = true;
                        ExitCode = 1;
                        throw new SystemException("Target file exists.");
                    } 
                    break;
            }
            HasExited = false;
            Action = DownloadString;
            Action.BeginInvoke(SourceUrl, TargetFile, CompletedCallback, null);
        }

        private void DownloadString(string source, string target)
        {
            try
            {
                throw new SystemException("Error!!!!!!");
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