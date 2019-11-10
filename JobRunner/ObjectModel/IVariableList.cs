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
        string GetVariableNames(Job job);
        void Load();
    }
}