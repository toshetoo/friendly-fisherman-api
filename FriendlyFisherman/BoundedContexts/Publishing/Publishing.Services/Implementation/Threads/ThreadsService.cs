using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Services.Impl;
using Publishing.DataAccess.Repositories.Threads;
using Publishing.Domain.Entities.Threads;
using Publishing.Services.Abstraction.Threads;
using Publishing.Services.Request.Threads;

namespace Publishing.Services.Implementation.Threads
{
    public class ThreadsService: BaseCrudService<Thread, ThreadsRepository>, IThreadsService
    {
        private readonly ThreadsRepository _repo;
        private readonly SeenCountRepository _seenCountRepo;

        public ThreadsService(ThreadsRepository repo, SeenCountRepository seenCountRepo) : base(repo)
        {
            _repo = repo;
            _seenCountRepo = seenCountRepo;
        }

        public async Task<ServiceResponseBase<Thread>> MarkAsSeenAsync(MarkThreadAsSeenRequest request)
        {
            return await Task.Run(() => MarkAsSeen(request));
        }

        private ServiceResponseBase<Thread> MarkAsSeen(MarkThreadAsSeenRequest request)
        {
            var response = new ServiceResponseBase<Thread>();

            try
            {

            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public Task<ServiceResponseBase<Thread>> GetSeenCountAsync(ServiceRequestBase<Thread> request)
        {
            throw new NotImplementedException();
        }
    }
}
