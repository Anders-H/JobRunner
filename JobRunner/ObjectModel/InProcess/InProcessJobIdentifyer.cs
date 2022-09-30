using System;
using JobRunner.Logging;
using JobRunner.ObjectModel.InProcess.Jobs;

namespace JobRunner.ObjectModel.InProcess
{
    public enum InProcessJobIdentifyer
    {
        BinaryUpload,
        DeleteFile,
        DownloadString
    }

    public class InProcessJobIdentifyerHelper
    {
        private readonly ILogger _log;

        public InProcessJobIdentifyerHelper(ILogger log)
        {
            _log = log;
        }

        public string GetIdentifyerString(InProcessJobIdentifyer j) =>
            j switch
            {
                InProcessJobIdentifyer.DeleteFile => "@deletefile",
                InProcessJobIdentifyer.BinaryUpload => "@binaryupload",
                InProcessJobIdentifyer.DownloadString => "@downloadstring",
                _ => throw new SystemException($@"Unknown in-process job identifyer: {j}")
            };

        public InProcessJobIdentifyer GetIdentifyerFromString(string j) =>
            (j ?? "").ToLower().Trim() switch
            {
                "@binaryupload" => InProcessJobIdentifyer.BinaryUpload,
                "@deletefile" => InProcessJobIdentifyer.DeleteFile,
                "@downloadstring" => InProcessJobIdentifyer.DownloadString,
                _ => throw new SystemException($@"Unknown in-process job identifyer: {j}")
            };
        
        public InProcessJob GetJob(InProcessJobIdentifyer j) =>
            j switch
            {
                InProcessJobIdentifyer.BinaryUpload => new BinaryUploadJob(_log),
                InProcessJobIdentifyer.DeleteFile => new DeleteFileJob(_log),
                InProcessJobIdentifyer.DownloadString => new DownloadStringJob(_log),
                _ => throw new SystemException($@"Failed to get in-process job: {j}")
            };
    }
}