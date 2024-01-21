using JobRunner.ObjectModel;

namespace JobRunner.Dialogs.InProcess
{
    public interface IAddInProcess
    {
        IVariableList? Variables { get; set; }
        IJobList? Jobs { get; set; }
    }
}