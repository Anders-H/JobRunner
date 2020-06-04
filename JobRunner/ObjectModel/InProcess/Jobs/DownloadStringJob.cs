using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs
{
    public class DownloadStringJob : InProcessJob
    {
        public override bool HasExited { get; }
        public override int ExitCode { get; }

        public override void Begin(ArgumentList args)
        {
            throw new System.NotImplementedException();
        }
        
        public override void Kill()
        {
            throw new System.NotImplementedException();
        }
    }
}