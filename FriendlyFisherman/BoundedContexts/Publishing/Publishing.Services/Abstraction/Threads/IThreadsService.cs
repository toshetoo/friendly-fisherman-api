using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using Publishing.Domain.Entities.Threads;
using Publishing.Services.Request.Threads;

namespace Publishing.Services.Abstraction.Threads
{
    public interface IThreadsService: IBaseCrudService<Thread>
    {
        Task<ServiceResponseBase<Thread>> MarkAsSeenAsync(MarkThreadAsSeenRequest request);
        Task<ServiceResponseBase<Thread>> GetSeenCountAsync(ServiceRequestBase<Thread> request);
    }
}
