using System.Collections.Generic;

namespace JobRunner.ObjectModel
{
    public interface IVariableList
    {
        List<Variable> All { get; }
        int Count { get; }
        string LoadFailedMessage { get; }
        bool LoadSuccess { get; }
        IVariableList GetVariables(Job job);
        string GetXml();
        string GetVariableNames(Job job);
        Variable? GetVariable(string name);
        void Load();
        bool HasVariable(string name);
        void Add(string name, string value);
        void Delete(string name);
    }
}