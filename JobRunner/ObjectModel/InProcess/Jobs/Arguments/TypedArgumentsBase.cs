using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess.Jobs.Arguments;

public abstract class TypedArgumentsBase
{
    protected ArgumentList Arguments { get; }

    protected TypedArgumentsBase(ArgumentList arguments)
    {
        Arguments = arguments;
    }
}