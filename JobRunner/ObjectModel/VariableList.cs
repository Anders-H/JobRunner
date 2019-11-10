using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using JobRunner.Utils;

namespace JobRunner.ObjectModel
{
    public class VariableList : IVariableList
    {
        public List<Variable> All { get; } = new List<Variable>();
        public bool LoadSuccess { get; private set; }
        
        public IVariableList GetVariables(Job job)
        {
            IVariableList vl = new VariableList();
            foreach (var v in All.Where(job.UsesVariable))
                vl.All.Add(v);
            return vl;
        }

        public string GetVariableNames(Job job)
        {
            var vl = GetVariables(job);
            var names = from v 
                in vl.All
                select v.Name;
            return string.Join(", ", names);
        }

        public string LoadFailedMessage { get; private set; }

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

        public int Count =>
            All.Count;
    }
}