using System;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess
{
    public abstract class InProcessJob
    {
        public bool HasExited { get; protected set; }
        public int ExitCode { get; protected set; }
        public abstract void Begin(ArgumentList args);
        public abstract void Kill();
        protected IAsyncResult AsyncResult { get; set; }
    }
}