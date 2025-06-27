#nullable enable
using System;
using JobRunner.Logging;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess;

public abstract class InProcessJob
{
    protected readonly ILogger Log;
    public bool HasExited { get; protected set; }
    public int ExitCode { get; protected set; }
    public SystemException? Exception { get; protected set; }
    public abstract void Begin(ArgumentList args);
    protected abstract void CompletedCallback(IAsyncResult result);

    protected InProcessJob(ILogger log)
    {
        Log = log;
    }
}