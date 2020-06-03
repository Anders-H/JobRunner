using System;

namespace JobRunner.ObjectModel.InProcess
{
    public enum InProcessJobIdentifyer
    {
        DownloadString,
        DeleteFile
    }

    public class InProcessJobIdentifyerHelper
    {
        public string GetIdentifyerString(InProcessJobIdentifyer j) =>
            j switch
            {
                InProcessJobIdentifyer.DownloadString => "@downloadstring",
                InProcessJobIdentifyer.DeleteFile => "@deletefile",
                _ => throw new ArgumentOutOfRangeException(nameof(j), j, null)
            };
    }
}