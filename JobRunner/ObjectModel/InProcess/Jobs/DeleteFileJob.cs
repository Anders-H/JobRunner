using System;
using System.IO;
using System.Threading;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class DeleteFileJob : InProcessJob
    {
        private string FileToDelete { get; set; }
        private string FileNotFoundBehavour { get; set; }
        private Action<string> Action { get; set; }

        public override void Begin(ArgumentList args)
        {
            FileToDelete = args.GetAfter("-file");
            FileNotFoundBehavour = args.GetAfter("-notfoundbehaviour");

            switch (FileNotFoundBehavour.ToLower())
            {
                case "skip":
                    if (!File.Exists(FileToDelete))
                    {
                        HasExited = true;
                        ExitCode = 0;
                        return;
                    }
                    break;
                case "fail":
                    if (!File.Exists(FileToDelete))
                    {
                        HasExited = true;
                        ExitCode = 1;
                        throw new SystemException("File not found.");
                    }
                    break;
            }

            HasExited = false;
            Action = DeleteFile;
            Action.BeginInvoke(FileToDelete, CompletedCallback, null);
        }

        private void DeleteFile(string fileToDelete)
        {
            try
            {
                File.Delete(fileToDelete);
                Thread.Sleep(500);
                if (File.Exists(fileToDelete))
                    throw new SystemException($"Failed to delete file: {fileToDelete}");
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