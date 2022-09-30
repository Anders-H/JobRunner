#nullable enable
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using JobRunner.Logging;
using JobRunner.ObjectModel.InProcess.Jobs.ArgumentOptions;
using JobRunner.ObjectModel.InProcess.Jobs.Arguments;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class DownloadStringJob : InProcessJob
    {
        private string? TargetFile { get; set; }
        private DownloadStringDelegate? Action { get; set; }

        public DownloadStringJob(ILogger log) : base(log)
        {
        }

        public override void Begin(ArgumentList args)
        {
            var downloadStringArguments = new DownloadStringArguments(args);

            TargetFile = downloadStringArguments.TargetFile;

            switch (downloadStringArguments.TryGetFileExistsBehaviour(_log))
            {
                case FileExistsBehaviour.Skip:
                    if (File.Exists(TargetFile))
                    {
                        HasExited = true;
                        ExitCode = 0;
                        return;
                    }
                    break;
                case FileExistsBehaviour.Fail:
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
            Action.BeginInvoke(downloadStringArguments.SourceUrl, TargetFile, CompletedCallback, null);
        }

        private void DownloadString(string source, string target)
        {
            if (string.IsNullOrWhiteSpace(target))
                throw new SystemException("Target is empty.");

            try
            {
                using var client = new HttpClient();
                var request = client.GetStringAsync(source);
                do
                {
                    Thread.Sleep(500);
                    if (request.IsCanceled || request.IsFaulted)
                        throw new SystemException("Download failed.");
                    if (request.IsCompleted)
                        break;
                } while (true);

                var result = request.Result;

                using var sw = new StreamWriter(TargetFile!, false, Encoding.UTF8);
                sw.Write(result);
                sw.Flush();
                sw.Close();
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