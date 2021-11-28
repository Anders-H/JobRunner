using JobRunner.Utils;
using System.Collections.Generic;
using System.Text;

namespace JobRunner.ObjectModel.Xml
{
    internal class XmlBuilder
    {
        public string GetXml(List<Job> jobList)
        {
            var s = new StringBuilder();
            s.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            s.AppendLine($@"<jobs
   AutoStart=""{(Config.AutoStart ? "1" : "0")}""
   AutoClose=""{(Config.AutoClose ? "1" : "0")}""
   EnableLogging=""{(Config.EnableLogging ? "1" : "0")}""
   TreatLoggingErrorsAsStepErrors=""{(Config.TreatLoggingErrorsAsStepErrors ? "1" : "0")}"">");
            jobList.ForEach(x => s.AppendLine(x.GetXml()));
            s.AppendLine("</jobs>");
            return s.ToString();
        }
    }
}