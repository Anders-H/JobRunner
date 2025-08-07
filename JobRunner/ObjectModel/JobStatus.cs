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
        public static string GetStatusText(Job job)
        {
            var append = " (no condition)";

            switch (job.RunIf)
            {
                case JobRunConditionEnum.NoCondition:
                    break;
                case JobRunConditionEnum.RunIfFileExists:
                    append = " (if file exist)";
                    break;
                case JobRunConditionEnum.RunIfFileDoesNotExist:
                    append = " (if file don't exist)";
                    break;
                case JobRunConditionEnum.RubIfExistsAndChangedThreeHoursAgo:
                    append = " (if file exist and is edited)";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (job.Status)
            {
                case JobStatus.Pending:
                    return $"Pending{append}";
                case JobStatus.Running:
                    return job.RetryCount switch
                    {
                        1 => $"Running, 1st retry{append}",
                        2 => $"Running, 2nd retry{append}",
                        3 => $"Running, 3rd retry{append}",
                        4 => $"Running, 4th retry{append}",
                        5 => $"Running, 5th retry{append}",
                        _ => $"Running{append}"
                    };
                case JobStatus.Completed:
                    return job.RetryCount > 0 ? $"Completed after {job.RetryCount} retries{append}" : $"Completed{append}";
                case JobStatus.Failed:
                    return $"Failed{append}";
                case JobStatus.Timeout:
                    return $"Timeout{append}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(job.RetryCount), job.RetryCount, null);
            }
        }
    }
}