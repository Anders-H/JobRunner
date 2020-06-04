using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess
{
    public abstract class InProcessJob
    {
        public abstract bool HasExited { get; }
        public abstract int ExitCode { get; }
        public abstract void Begin(ArgumentList args);
        public abstract void Kill();
    }
}