namespace FriendlyFisherman.SharedKernel
{
    using System;

    public class ServiceResponseBase
    {
        public ServiceResponseBase()
        {
            this.Exception = null;
        }

        public Exception Exception { get; set; }
    }
}
