using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.EntityViewModels.Threads;
using Publishing.Services.Request.Threads;

namespace Publishing.Services.Abstraction.Threads
{
    public interface IThreadsService: IBaseCrudService<ThreadViewModel, Thread>
    {
        Task<ServiceResponseBase<ThreadViewModel>> MarkAsSeenAsync(MarkThreadAsSeenRequest request);
        Task<ServiceResponseBase<ThreadViewModel>> GetSeenCountAsync(ServiceRequestBase<ThreadViewModel> request);
        Task<ServiceResponseBase<ThreadReplyViewModel>> AddThreadReplyAsync(ServiceRequestBase<ThreadReplyViewModel> request);
        Task<ServiceResponseBase<ThreadReplyViewModel>> EditThreadReplyAsync(ServiceRequestBase<ThreadReplyViewModel> request);
        Task<ServiceResponseBase<ThreadReplyViewModel>> DeleteThreadReplyAsync(ServiceRequestBase<ThreadReplyViewModel> request);
        Task<ServiceResponseBase<LikeViewModel>> LikeReplyAsync(ServiceRequestBase<LikeViewModel> request);
    }
}
