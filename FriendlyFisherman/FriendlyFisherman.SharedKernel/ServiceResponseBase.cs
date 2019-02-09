using System;

namespace FriendlyFisherman.SharedKernel
{
    public class ServiceResponseBase
    {
        public ServiceResponseBase()
        {
            this.Exception = null;
        }

        public Exception Exception { get; set; }
    }
}
