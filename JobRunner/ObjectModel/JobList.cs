using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using JobRunner.Utils;

namespace JobRunner.ObjectModel
{
    public class JobList : List<Job>
    {
        public bool LoadSuccess { get; private set; }
        public string LoadFailedMessage { get; private set; }

        public void Load()
        {
            LoadSuccess = false;
            Clear();
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
            dom.Load(filename);
            var doc = dom.DocumentElement;
            if (doc == null)
                return;
            var jobsXml = doc.SelectNodes("job");
            if (jobsXml == null)
                return;
            Config.AutoStart = GetBoolFromAttribute(doc, "AutoStart");
            Config.AutoClose = GetBoolFromAttribute(doc, "AutoClose");
            var number = 0;
            foreach (XmlElement jobXml in jobsXml)
            {
                number++;
                var name = jobXml.SelectSingleNode("name")?.InnerText ?? "";
                var command = jobXml.SelectSingleNode("command")?.InnerText ?? "";
                if (string.IsNullOrWhiteSpace(command))
                {
                    LoadFailedMessage = "At least one job is missing <command> value.";
                    if (!Config.IsAdministrator)
                        return;
                }
                var timeout = jobXml.SelectSingleNode("timeout")?.InnerText ?? "";
                if (string.IsNullOrWhiteSpace(timeout))
                {
                    LoadFailedMessage = "At least one job is missing <timeout> value.";
                    if (!Config.IsAdministrator)
                        return;
                }
                if (!TimeSpan.TryParse(timeout, CultureInfo.CurrentCulture, out var t))
                {
                    LoadFailedMessage = $"Failed to parse timeout: {timeout}";
                    if (!Config.IsAdministrator)
                        return;
                }
                var display = (jobXml.SelectSingleNode("display")?.InnerText ?? "").ToLower();
                if (!(display == "visible" || display == "hidden"))
                {
                    LoadFailedMessage = "At least one job is missing a correct <display> value. Possible values are Visible or Hidden.";
                    if (!Config.IsAdministrator)
                        return;
                }
                var arguments = jobXml.SelectSingleNode("arguments")?.InnerText ?? "";
                var breakOnError = (jobXml.SelectSingleNode("breakOnError")?.InnerText ?? "").ToLower();
                Add(new Job(number, name, command, arguments, t, display == "hidden", breakOnError == "true" || breakOnError == "1"));
            }
            LoadSuccess = true;
        }

        public string GetXml()
        {
            var s = new StringBuilder();
            s.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            s.AppendLine($@"<jobs AutoStart=""{(Config.AutoStart ? "1" : "0")}"" AutoClose=""{(Config.AutoClose ? "1" : "0")}"">");
            ForEach(x => s.AppendLine(x.GetXml()));
            s.AppendLine("</jobs>");
            return s.ToString();
        }

        private bool GetBoolFromAttribute(XmlElement e, string attributeName)
        {
            if (e == null)
                return false;
            var att = e.Attributes.GetNamedItem(attributeName);
            var v = att?.Value?.ToLower() ?? "";
            return v == "1" || v == "true";
        }

        public void Reset()
        {
            var startTime = DateTime.Now;
            ForEach(x => x.Reset(startTime));
        }

        public bool RunSuccess
        {
            get
            {
                foreach (var job in this)
                    if (job.Status != JobStatus.Completed)
                        return false;
                return true;
            }
        }

        public int Completed =>
            this.Count(x => x.Status == JobStatus.Completed);

        public int Error =>
            this.Count(x => x.Status == JobStatus.Failed || x.Status == JobStatus.Timeout);

        public int Pending =>
            this.Count(x => x.Status == JobStatus.Pending);

        public void InsertJob(Job job)
        {
            foreach (var j in this)
            {
                if (j.Number < job.Number)
                    continue;
                Insert(IndexOf(j), job);
                Renumber();
                return;
            }
            Add(job);
            Renumber();
        }

        public void RemoveJob(Job job)
        {
            Remove(job);
            Renumber();
        }

        public void MoveUp(Job job)
        {
            var currentIndex = IndexOf(job);
            Remove(job);
            Insert(currentIndex - 1, job);
            Renumber();
        }

        public void MoveDown(Job job)
        {
            var currentIndex = IndexOf(job);
            Remove(job);
            Insert(currentIndex + 1, job);
            Renumber();
        }

        private void Renumber()
        {
            for (var i = 0; i < Count; i++)
                this[i].Number = i + 1;
        }

        public int FirstSequenceNumber =>
            this.FirstOrDefault()?.Number ?? 1;

        public int LastSequenceNumber =>
            this.LastOrDefault()?.Number ?? Count + 1;
    }
}