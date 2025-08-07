using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using JobRunner.Logging;
using JobRunner.ObjectModel.Xml;
using JobRunner.Utils;

namespace JobRunner.ObjectModel;

public class JobList : IJobList
{
    private readonly ILogger _log;
    public List<Job> All { get; } = [];
    public bool LoadSuccess { get; private set; }
    public string LoadFailedMessage { get; private set; }

    public JobList(ILogger log)
    {
        _log = log;
        LoadFailedMessage = "";
    }

    public void Load()
    {
        LoadSuccess = false;
        All.Clear();
        var filename = Config.GetJobFilePath();

        if (!File.Exists(filename))
        {
            if (Config.IsAdministrator)
            {
                LoadSuccess = true;
                return;
            }
            LoadFailedMessage = $"The file {filename} does not exist.";
            return;
        }
            
        var dom = new XmlDocument();

        try
        {
            dom.Load(filename);
        }
        catch
        {
            LoadSuccess = true; // To avoid an extra messagebox.
            var fi = new FileInfo(filename);

            if (MessageBox.Show($@"The XML file ""{fi.FullName}"" is corrupt. You should restore a backup version or start from scratch. Do you want to open the working folder ""{(fi.Directory?.FullName ?? "")}""?", @"Load failed", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("explorer.exe", fi.Directory?.FullName ?? "");
                return;
            }
        }

        var doc = dom.DocumentElement;

        if (doc == null)
            return;

        var jobsXml = doc.SelectNodes("job");

        if (jobsXml == null)
            return;
            
        Config.AutoStart = doc.GetBoolFromAttribute("AutoStart");
        Config.AutoClose = doc.GetBoolFromAttribute("AutoClose");
        Config.EnableLogging = doc.GetBoolFromAttribute("EnableLogging");
        Config.TreatLoggingErrorsAsStepErrors = doc.GetBoolFromAttribute("TreatLoggingErrorsAsStepErrors");
        var number = 0;

        foreach (XmlElement jobXml in jobsXml)
        {
            number++;

            var command = jobXml.GetChildString("command");

            if (string.IsNullOrWhiteSpace(command))
            {
                LoadFailedMessage = "At least one job is missing <command> value.";
                if (!Config.IsAdministrator)
                    return;
            }

            var timeoutString = jobXml.GetChildString("timeout");

            if (string.IsNullOrWhiteSpace(timeoutString))
            {
                LoadFailedMessage = "At least one job is missing <timeout> value.";
                if (!Config.IsAdministrator)
                    return;
            }

            if (!TimeSpan.TryParse(timeoutString, CultureInfo.CurrentCulture, out var timeout))
            {
                LoadFailedMessage = $"Failed to parse timeout: {timeoutString}";
                if (!Config.IsAdministrator)
                    return;
            }

            var display = jobXml.GetChildString("display").ToLower();

            if (!(display == "visible" || display == "hidden"))
            {
                LoadFailedMessage = "At least one job is missing a correct <display> value. Possible values are Visible or Hidden.";
                if (!Config.IsAdministrator)
                    return;
            }

            var arguments = jobXml.GetChildString("arguments");
            var breakOnError = jobXml.GetChildString("breakOnError").ToLower();
            var enabled = jobXml.GetChildString("enabled").ToLower();
            var retryCountString = jobXml.GetChildString("retryCount");
            var retryCount = 0;
            var runIf = jobXml.GetChildString("RunIf");
            JobRunConditionEnum runIfEnum;

            try
            {
                runIfEnum = JobRunConditionEnumHelper.FromFriendlyString(runIf);
            }
            catch
            {
                runIfEnum = JobRunConditionEnum.NoCondition;
            }

            var runIfArg = jobXml.GetChildString("RunIfArgument");

            if (!string.IsNullOrEmpty(retryCountString))
                int.TryParse(retryCountString, NumberStyles.Any, CultureInfo.CurrentCulture, out retryCount);

            if (retryCount < 0)
                retryCount = 0;
            else if (retryCount > 5)
                retryCount = 5;

            Add(
                number,
                jobXml.GetChildString("name"),
                enabled,
                command,
                arguments,
                timeout,
                display,
                breakOnError,
                retryCount,
                runIfEnum,
                runIfArg);
        }
        LoadSuccess = true;
    }

    private void Add(
        int number,
        string name,
        string enabled,
        string command,
        string arguments,
        TimeSpan timeout,
        string display,
        string breakOnError,
        int retryCount,
        JobRunConditionEnum runIf,
        string runIfArgument) =>
        All.Add(new Job(
            _log,
            number,
            name,
            enabled == "true" || enabled == "1" || enabled == "",
            command,
            arguments,
            timeout,
            display == "hidden",
            breakOnError == "true" || breakOnError == "1",
            retryCount,
            runIf,
            runIfArgument
        ));

    public Job FirstJob =>
        All.FirstOrDefault()!;

    public Job LastJob =>
        All.LastOrDefault()!;

    public int Count =>
        All.Count;

    public string GetXml() =>
        new XmlBuilder().GetXml(All);

    public void ResetJobs()
    {
        var startTime = DateTime.Now;
        All.ForEach(x => x.Reset(startTime));
    }

    public bool RunSuccess
    {
        get
        {
            foreach (var job in All)
                if (job.Status != JobStatus.Completed)
                    return false;
            return true;
        }
    }

    public int Completed =>
        All.Count(x => x.Status == JobStatus.Completed);

    public int Error =>
        All.Count(x => x.Status == JobStatus.Failed || x.Status == JobStatus.Timeout);

    public int Pending =>
        All.Count(x => x.Status == JobStatus.Pending);

    public void InsertJob(Job job)
    {
        foreach (var j in All)
        {
            if (j.Number < job.Number)
                continue;
            All.Insert(All.IndexOf(j), job);
            Renumber();
            return;
        }
        All.Add(job);
        Renumber();
    }

    public void RemoveJob(Job job)
    {
        All.Remove(job);
        Renumber();
    }

    public void MoveUp(Job job) =>
        Move(job, -1);

    public void MoveDown(Job job) =>
        Move(job, 1);

    private void Move(Job job, int offset)
    {
        var currentIndex = All.IndexOf(job);
        All.Remove(job);
        All.Insert(currentIndex + offset, job);
        Renumber();
    }

    private void Renumber()
    {
        for (var i = 0; i < All.Count; i++)
            All[i].Number = i + 1;
    }

    public int FirstSequenceNumber =>
        All.FirstOrDefault()?.Number ?? 1;

    public int LastSequenceNumber =>
        All.LastOrDefault()?.Number ?? All.Count + 1;
        
    public IJobList GetVariableUsage(Variable variable)
    {
        IJobList jobs = new JobList(_log);
        foreach (var job in All.Where(job => job.UsesVariable(variable)))
            jobs.All.Add(job);
        return jobs;
    }

    public Job? GetJob(int index)
    {
        if (Count <= 0)
            return null;

        if (index >= 0 && index < Count)
            return All[index];

        return null;
    }

    public string Names =>
        string.Join(", ", All.Select(job => job.Name).ToList());
}