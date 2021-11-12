using System;
using JobRunner.Services;

namespace JobRunner.ObjectModel.InProcess
{
    public class BinaryUploadJob : InProcessJob
    {
        public override void Begin(ArgumentList args)
        {
            throw new NotImplementedException();
        }

        protected override void CompletedCallback(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
    }
}