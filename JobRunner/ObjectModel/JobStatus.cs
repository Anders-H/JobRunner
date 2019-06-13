using System;

namespace JobRunner.ObjectModel
{
    public enum JobStatus
    {
        Pending,
        Running,
        Completed,
        Failed,
        Timeout
    }

    public static class JobStatusHelper
    {
        public static string GetStatusText(JobStatus jobStatus)
        {
            switch (jobStatus)
            {
                case JobStatus.Pending:
                    return "Pending";
                case JobStatus.Running:
                    return "Running";
                case JobStatus.Completed:
                    return "Completed";
                case JobStatus.Failed:
                    return "Failed";
                case JobStatus.Timeout:
                    return "Timeout";
                default:
                    throw new ArgumentOutOfRangeException(nameof(jobStatus), jobStatus, null);
            }
        }
    }
}