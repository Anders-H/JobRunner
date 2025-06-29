using System;
using System.Collections.Generic;
using System.Linq;

namespace JobRunner.ObjectModel;

public enum JobRunConditionEnum
{
    NoCondition,
    RunIfFileExists,
    RunIfFileDoesNotExist,
    RubIfExistsAndChangedThreeHoursAgo
}

public static class JobRunConditionEnumHelper
{
    public static string ToFriendlyString(this JobRunConditionEnum condition)
    {
        return condition switch
        {
            JobRunConditionEnum.NoCondition => "No Condition",
            JobRunConditionEnum.RunIfFileExists => "Run if file exists",
            JobRunConditionEnum.RunIfFileDoesNotExist => "Run if file does not exist",
            JobRunConditionEnum.RubIfExistsAndChangedThreeHoursAgo => "Run if file exists and is recently changed",
            _ => throw new ArgumentOutOfRangeException(nameof(condition), condition, null)
        };
    }

    public static JobRunConditionEnum FromFriendlyString(string condition)
    {
        return condition switch
        {
            "No Condition" => JobRunConditionEnum.NoCondition,
            "Run if file exists" => JobRunConditionEnum.RunIfFileExists,
            "Run if file does not exist" => JobRunConditionEnum.RunIfFileDoesNotExist,
            "Run if file exists and is recently changed" => JobRunConditionEnum.RubIfExistsAndChangedThreeHoursAgo,
            _ => throw new ArgumentException($@"Unknown condition: {condition}", nameof(condition))
        };
    }

    public static List<JobRunConditionEnum> GetAllEnumItems() =>
        Enum.GetValues(typeof(JobRunConditionEnum)).Cast<JobRunConditionEnum>().ToList();

    public static List<string> GetAllFriendlyStrings() =>
        GetAllEnumItems().Select(item => item.ToFriendlyString()).ToList();
}