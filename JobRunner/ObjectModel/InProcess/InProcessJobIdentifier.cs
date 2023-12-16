using System;
using JobRunner.Logging;
using JobRunner.ObjectModel.InProcess.Jobs;

namespace JobRunner.ObjectModel.InProcess
{
    public enum InProcessJobIdentifier
    {
        BinaryUpload,
        DeleteFile,
        DownloadString
    }

    public class InProcessJobIdentifierHelper
    {
        private readonly ILogger _log;

        public InProcessJobIdentifierHelper(ILogger log)
        {
            _log = log;
        }

        public string GetIdentifierString(InProcessJobIdentifier j) =>
            j switch
            {
                InProcessJobIdentifier.DeleteFile => "@deletefile",
                InProcessJobIdentifier.BinaryUpload => "@binaryupload",
                InProcessJobIdentifier.DownloadString => "@downloadstring",
                _ => throw new SystemException($@"Unknown in-process job identifier: {j}")
            };

        public InProcessJobIdentifier GetIdentifierFromString(string j) =>
            (j ?? "").ToLower().Trim() switch
            {
                "@binaryupload" => InProcessJobIdentifier.BinaryUpload,
                "@deletefile" => InProcessJobIdentifier.DeleteFile,
                "@downloadstring" => InProcessJobIdentifier.DownloadString,
                _ => throw new SystemException($@"Unknown in-process job identifier: {j}")
            };
        
        public InProcessJob GetJob(InProcessJobIdentifier j) =>
            j switch
            {
                InProcessJobIdentifier.BinaryUpload => new BinaryUploadJob(_log),
                InProcessJobIdentifier.DeleteFile => new DeleteFileJob(_log),
                InProcessJobIdentifier.DownloadString => new DownloadStringJob(_log),
                _ => throw new SystemException($@"Failed to get in-process job: {j}")
            };
    }
}