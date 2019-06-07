using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace JobRunner
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
            var jobsXml = dom.DocumentElement?.SelectNodes("job");
            if (jobsXml == null)
                return;
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
                Add(new Job(number, name, command, arguments, t, display == "hidden"));
            }
            LoadSuccess = true;
        }

        public string GetXml()
        {
            var s = new StringBuilder();
            s.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            s.AppendLine("<jobs>");
            ForEach(x => s.AppendLine(x.GetXml()));
            s.AppendLine("</jobs>");
            return s.ToString();
        }

        public void Reset()
        {
            var startTime = DateTime.Now;
            ForEach(x => x.Reset(startTime));
        }

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