using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace JobRunner
{
    public class Job
    {
        public int Number { get; internal set; }
        public string Name { get; }
        public string Command { get; }
        public string Arguments { get; }
        public JobStatus Status { get; set; }
        public int RowIndex { get; set; }
        public int ExitCode { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public DateTime? AllJobsStartTime { get; private set; }
        public string FailMessage { get; private set; }
        public TimeSpan Timeout { get; }
        public bool Hidden { get; }

        public Job(int number, string name, string command, string arguments, TimeSpan timeout, bool hidden)
        {
            Number = number;
            Name = name;
            Command = command;
            Arguments = arguments;
            Timeout = timeout;
            Status = JobStatus.Pending;
            Hidden = hidden;
        }

        public void Reset(DateTime allJobsStartTime)
        {
            Status = JobStatus.Pending;
            ExitCode = 0;
            StartTime = null;
            AllJobsStartTime = allJobsStartTime;
            FailMessage = "";
        }

        public void Run(Grid grid)
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

        public string GetXml() =>
            $@"    <job>
        <name>{System.Net.WebUtility.HtmlEncode(Name)}</name>
    </job>";
    }
}