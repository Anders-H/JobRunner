﻿using System;
using System.Text;
using System.Diagnostics;
using System.Threading;
using JobRunner.GuiComponents;
using JobRunner.Logging;
using JobRunner.ObjectModel.InProcess;
using JobRunner.Services;
using JobRunner.Utils;

namespace JobRunner.ObjectModel
{
    public class Job
    {
        private readonly ILogger _log;
        public int Number { get; internal set; }
        public string Name { get; internal set; }
        public bool Enabled { get; internal set; }
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
        public int RetryCount { get; internal set; }

        public Job(ILogger log, int number, string name, bool enabled, string command, string arguments, TimeSpan timeout, bool hidden, bool breakOnError, int retryCount)
        {
            _log = log;
            Number = number;
            Name = name;
            Enabled = enabled;
            Command = command;
            Arguments = arguments;
            Timeout = timeout;
            Status = JobStatus.Pending;
            Hidden = hidden;
            BreakOnError = breakOnError;
            FailMessage = "";
            RetryCount = retryCount;
        }

        public string GetDescription()
        {
            var s = new StringBuilder();
            s.AppendLine($"Command: {Command}");
            s.AppendLine($"Arguments: {Arguments}");
            s.AppendLine($"Timeout: {Timeout}");
            return s.ToString();
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

        public void Run(ILogger log, IGridVisualFeedback grid, IVariableList variableList)
        {
            if (!Enabled)
            {
                Status = JobStatus.Completed;
                log.AppendLog($@"Job ""{Name}"" ({Number}) is disabled.");
                grid.Invalidate();
                return;
            }

            for (var retry = 0; retry <= RetryCount; retry++)
            {
                if (retry > 0)
                    log.AppendLog($"Retry {retry} of {RetryCount}.");
                try
                {
                    StartTime = DateTime.Now;

                    if (Config.EnableLogging)
                    {
                        var s = StartTime.Value;
                        var h = Hidden ? " (hidden)" : "";
                        var n = Name.Trim();

                        if (string.IsNullOrEmpty(n))
                            n = Command;

                        var result = log.AppendLog($"{s.Year:0000}-{s.Month:00}-{s.Day:00} {s.Hour:00}:{s.Minute:00}:{s.Second:00}{h}: {n}");

                        if (!result && Config.TreatLoggingErrorsAsStepErrors)
                            throw new SystemException("Logging failed.");
                    }

                    if (Command.StartsWith("@"))
                        InProcess(grid, variableList);
                    else
                        OutOfProcess(grid, variableList);

                    EndTime = DateTime.Now;

                    if (Config.EnableLogging)
                    {
                        var s = EndTime.Value;
                        var result = log.AppendLog($"Ended at {s.Year:0000}-{s.Month:00}-{s.Day:00} {s.Hour:00}:{s.Minute:00}:{s.Second:00} with exit code: {ExitCode}.");
                        if (!result && Config.TreatLoggingErrorsAsStepErrors)
                            throw new SystemException("Logging failed.");
                    }

                    if (!(Status == JobStatus.Failed || Status == JobStatus.Timeout))
                        break;
                }
                catch (Exception e)
                {
                    EndTime = DateTime.Now;
                    Status = JobStatus.Failed;
                    FailMessage = e.Message;

                    if (string.IsNullOrWhiteSpace(FailMessage))
                        FailMessage = e.GetType().Name;

                    if (Config.EnableLogging)
                    {
                        var s = EndTime.Value;
                        log.AppendLog($"System error at {s.Year:0000}-{s.Month:00}-{s.Day:00} {s.Hour:00}:{s.Minute:00}:{s.Second:00}: {FailMessage}");
                    }
                }
            }
        }

        private void OutOfProcess(IGridVisualFeedback grid, IVariableList variableList)
        {
            var start = new ProcessStartInfo(Command)
            {
                Arguments = new ArgumentDecoder(variableList).GetDecodedText(Arguments),
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
                if (DateTime.Now.Subtract(StartTime!.Value) <= Timeout)
                    continue;
                process.Kill();
                EndTime = DateTime.Now;
                Status = JobStatus.Timeout;
                FailMessage = "Timeout";
                return;
            }

            ExitCode = process.ExitCode;

            Status = ExitCode == 0
                ? JobStatus.Completed
                : JobStatus.Failed;
        }

        private void InProcess(IGridVisualFeedback grid, IVariableList variableList)
        {
            var inProcessJobIdentifierHelper = new InProcessJobIdentifierHelper(_log);
            var jobId = inProcessJobIdentifierHelper.GetIdentifierFromString(Command);
            var job = inProcessJobIdentifierHelper.GetJob(jobId);
            var args = new ArgumentList(Arguments);
            args = args.Decode(variableList);
            job.Begin(args);

            while (!job.HasExited)
            {
                grid.CursorBlink = !grid.CursorBlink;
                grid.Invalidate();
                Thread.Sleep(1000);
                if (DateTime.Now.Subtract(StartTime!.Value) <= Timeout)
                    continue;
                EndTime = DateTime.Now;
                Status = JobStatus.Timeout;
                FailMessage = "Timeout";
                ExitCode = -1;
                return;
            }

            ExitCode = job.ExitCode;

            Status = ExitCode == 0
                ? JobStatus.Completed
                : JobStatus.Failed;

            if (Status == JobStatus.Failed && job.Exception != null)
                FailMessage = job.Exception.Message;
        }

        public bool UsesVariable(Variable variable) =>
            Arguments.IndexOf($"[{variable.Name}]", StringComparison.Ordinal) > -1;

        public string GetXml() =>
            $@"    <job>
        <name>{System.Net.WebUtility.HtmlEncode(Name)}</name>
        <enabled>{(Enabled ? "true" : "false")}</enabled>
        <command>{System.Net.WebUtility.HtmlEncode(Command)}</command>
        <arguments>{System.Net.WebUtility.HtmlEncode(Arguments)}</arguments>
        <timeout>{System.Net.WebUtility.HtmlEncode(Timeout.ToString())}</timeout>
        <display>{(Hidden ? "Hidden" : "Visible")}</display>
        <breakOnError>{(BreakOnError ? "true" : "false")}</breakOnError>
        <RetryCount>{RetryCount}</RetryCount>
    </job>";

        public override string ToString() =>
            $"{Number}. {Name}";
    }
}