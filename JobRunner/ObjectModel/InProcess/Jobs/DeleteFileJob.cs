using System;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class DeleteFileJob : InProcessJob
    {
        private string FileToDelete { get; set; }
        private string FileNotFoundBehavour { get; set; }

        public override void Begin(ArgumentList args)
        {

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