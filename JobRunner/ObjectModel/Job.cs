using System;
using System.Diagnostics;
using System.Threading;
using JobRunner.GuiComponents;

namespace JobRunner.ObjectModel
{
    public class Job
    {
        public int Number { get; internal set; }
        public string Name { get; internal set; }
        public string Command { get; internal set; }
        public string Arguments { get; internal set; }
        public JobStatus Status { get; set; }
        public int RowIndex { get; set; }
        public int ExitCode { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public DateTime? AllJobsStartTime { get; private set; }
        public string FailMessage { get; private set; }
        public TimeSpan Timeout { get; internal set; }
        public bool Hidden { get; internal set; }
        public bool BreakOnError { get; internal set; }

        public Job(int number, string name, string command, string arguments, TimeSpan timeout, bool hidden, bool breakOnError)
        {
            Number = number;
            Name = name;
            Command = command;
            Arguments = arguments;
            Timeout = timeout;
            Status = JobStatus.Pending;
            Hidden = hidden;
            BreakOnError = breakOnError;
        }

        public void Reset(DateTime allJobsStartTime)
        {
            Status = JobStatus.Pending;
            ExitCode = 0;
            StartTime = null;
            EndTime = null;
            AllJobsStartTime = allJobsStartTime;
            FailMessage = "";
        }

        public void Run(IGridVisualFeedback grid)
        {
            try
            {
                StartTime = DateTime.Now;
                var start = new ProcessStartInfo(Command)
                {
                    Arguments = Arguments,
                    WindowStyle = Hidden ? ProcessWindowStyle.Hidden : ProcessWindowStyle.Normal
                };
                var process = Process.Start(start);
                if (process == null)
                {
                    Status = JobStatus.Failed;
                    return;
                }
                while (!process.HasExited)
                {
                    grid.CursorBlink = !grid.CursorBlink;
                    grid.Invalidate();
                    Thread.Sleep(1000);
                    if (DateTime.Now.Subtract(StartTime.Value) <= Timeout)
                        continue;
                    process.Kill();
                    EndTime = DateTime.Now;
                    Status = JobStatus.Timeout;
                    FailMessage = "Timeout";
                    break;
                }

                EndTime = DateTime.Now;
                ExitCode = process.ExitCode;
                Status = ExitCode == 0
                    ? JobStatus.Completed
                    : JobStatus.Failed;
            }
            catch (Exception e)
            {
                EndTime = DateTime.Now;
                Status = JobStatus.Failed;
                FailMessage = e.Message;
            }
        }

        public bool UsesVariable(Variable variable) =>
            Arguments.IndexOf($"[{variable.Name}]", StringComparison.Ordinal) > -1;
        
        public string GetXml() =>
            $@"    <job>
        <name>{System.Net.WebUtility.HtmlEncode(Name)}</name>
        <command>{System.Net.WebUtility.HtmlEncode(Command)}</command>
        <arguments>{System.Net.WebUtility.HtmlEncode(Arguments)}</arguments>
        <timeout>{System.Net.WebUtility.HtmlEncode(Timeout.ToString())}</timeout>
        <display>{(Hidden ? "Hidden" : "Visible")}</display>
        <breakOnError>{(BreakOnError ? "true" : "false")}</breakOnError>
    </job>";
    }
}