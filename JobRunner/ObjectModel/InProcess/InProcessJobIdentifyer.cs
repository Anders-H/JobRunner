using System;
using JobRunner.ObjectModel.InProcess.Jobs;

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
                _ => throw new SystemException($@"Unknown in-process job identifyer: {j}")
            };

        public InProcessJobIdentifyer GetIdentifyerFromString(string j) =>
            (j ?? "").ToLower().Trim() switch
            {
                "@downloadstring" => InProcessJobIdentifyer.DownloadString,
                "@deletefile" => InProcessJobIdentifyer.DeleteFile,
                _ => throw new SystemException($@"Unknown in-process job identifyer: {j}")
            };
        
        public InProcessJob GetJob(InProcessJobIdentifyer j) =>
            j switch
            {
                InProcessJobIdentifyer.DownloadString => new DownloadStringJob(),
                InProcessJobIdentifyer.DeleteFile => null,
                _ => throw new SystemException($@"Failed to get in-process job: {j}")
            };
    }
}