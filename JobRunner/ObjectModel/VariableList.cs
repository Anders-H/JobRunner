using System.Collections.Generic;

namespace JobRunner.ObjectModel
{
    public class VariableList : IVariableList
    {
        public List<Variable> All { get; } = new List<Variable>();
        public bool LoadSuccess { get; private set; }
        public string LoadFailedMessage { get; private set; }

        public void Load()
        {
            
        }

        public int Count =>
            All.Count;
    }
}