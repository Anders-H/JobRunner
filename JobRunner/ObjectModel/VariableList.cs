#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using JobRunner.Utils;

namespace JobRunner.ObjectModel
{
    public class VariableList : IVariableList
    {
        public List<Variable> All { get; } = new List<Variable>();
        public bool LoadSuccess { get; private set; }
        public string LoadFailedMessage { get; private set; }

        public VariableList() : this(false, "")
        {
        }

        public VariableList(bool loadSuccess, string loadFailedMessage)
        {
            LoadSuccess = loadSuccess;
            LoadFailedMessage = loadFailedMessage;
        }

        public IVariableList GetVariables(Job job)
        {
            IVariableList vl = new VariableList();
            foreach (var v in All.Where(job.UsesVariable))
                vl.All.Add(v);
            return vl;
        }

        public string GetXml()
        {
            var s = new StringBuilder();
            s.AppendLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
            s.AppendLine($@"<jobs AutoStart=""{(Config.AutoStart ? "1" : "0")}"" AutoClose=""{(Config.AutoClose ? "1" : "0")}"">");
            All.ForEach(x => s.AppendLine(x.GetXml()));
            s.AppendLine("</jobs>");
            return s.ToString();
        }

        public string GetVariableNames(Job job)
        {
            var vl = GetVariables(job);
            var names = from v 
                in vl.All
                select v.Name;
            return string.Join(", ", names);
        }

        public void Load()
        {
            LoadSuccess = false;
            All.Clear();
            var filename = Config.GetVariableFilePath();
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
            var variablesXml = doc?.SelectNodes("variable");
            if (variablesXml == null)
                return;
            foreach (XmlElement variableXml in variablesXml)
            {
                var name = variableXml.GetChildString("name");
                var value = variableXml.GetChildString("value");
                All.Add(new Variable(name, value));
            }
            LoadSuccess = true;
        }

        public bool HasVariable(string name) =>
            All.Exists(
                x => string.Compare(x.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0
            );

        public void Add(string name, string value) =>
            All.Add(new Variable(name, value));

        public void Delete(string name)
        {
            bool again;
            do
            {
                again = false;
                foreach (var v in All.Where(v => v.Is(name)))
                {
                    All.Remove(v);
                    again = true;
                    break;
                }
            } while (again);
        }

        public int Count =>
            All.Count;

        public Variable? GetVariable(string name) =>
            All.FirstOrDefault(
                x => string.Compare(x.Name, name, StringComparison.InvariantCultureIgnoreCase) == 0
            );
    }
}