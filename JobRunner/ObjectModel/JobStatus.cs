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
        public static string GetStatusText(JobStatus jobStatus, int retryCount)
        {
            switch (jobStatus)
            {
                case JobStatus.Pending:
                    return "Pending";
                case JobStatus.Running:
                    return retryCount switch
                    {
                        1 => "Running, 1st retry",
                        2 => "Running, 2nd retry",
                        3 => "Running, 3rd retry",
                        4 => "Running, 4th retry",
                        5 => "Running, 5th retry",
                        _ => "Running"
                    };
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