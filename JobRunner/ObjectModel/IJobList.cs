using System.Collections.Generic;

namespace JobRunner.ObjectModel;

public interface IJobList
{
    List<Job> All { get; }
    int Completed { get; }
    int Count { get; }
    int Error { get; }
    Job FirstJob { get; }
    int FirstSequenceNumber { get; }
    Job LastJob { get; }
    int LastSequenceNumber { get; }
    Job? GetJob(int index);
    string LoadFailedMessage { get; }
    bool LoadSuccess { get; }
    int Pending { get; }
    bool RunSuccess { get; }
    string GetXml();
    void InsertJob(Job job);
    void Load();
    void MoveDown(Job job);
    void MoveUp(Job job);
    void RemoveJob(Job job);
    void ResetJobs();
    IJobList GetVariableUsage(Variable variable);
    string Names { get; }
}