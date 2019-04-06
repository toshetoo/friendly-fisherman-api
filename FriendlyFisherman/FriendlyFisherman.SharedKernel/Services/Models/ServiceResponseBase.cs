using System.Collections.Generic;

namespace FriendlyFisherman.SharedKernel
{
    using System;

    public class ServiceResponseBase<T>
    {
        public ServiceResponseBase()
        {
            this.Exception = null;
        }

        public IEnumerable<T> Items { get; set; }
        public T Item { get; set; }
        public Exception Exception { get; set; }
    }
}
