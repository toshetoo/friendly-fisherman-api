using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Messages;
using FriendlyFisherman.SharedKernel.Services.Impl;
using Publishing.DataAccess.Repositories.Threads;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.EntityViewModels.Threads;
using Publishing.Domain.Repositories.Threads;
using Publishing.Services.Abstraction.Threads;
using Publishing.Services.Request.Threads;

namespace Publishing.Services.Implementation.Threads
{
    public class ThreadsService: BaseCrudService<Thread, IThreadsRepository>, IThreadsService
    {
        private readonly IThreadsRepository _repo;
        private readonly ISeenCountRepository _seenCountRepo;
        private readonly IThreadReplyRepository _replyRepository;

        public ThreadsService(IThreadsRepository repo, ISeenCountRepository seenCountRepo, IThreadReplyRepository replyRepository) : base(repo)
        {
            _repo = repo;
            _seenCountRepo = seenCountRepo;
            _replyRepository = replyRepository;
        }
        

        public async Task<ServiceResponseBase<ThreadViewModel>> MarkAsSeenAsync(MarkThreadAsSeenRequest request)
        {
            return await Task.Run(() => MarkAsSeen(request));
        }

        private ServiceResponseBase<ThreadViewModel> MarkAsSeen(MarkThreadAsSeenRequest request)
        {
            var response = new ServiceResponseBase<ThreadViewModel>();

            try
            {
                var thread = _repo.Get(t => t.Id == request.ThreadId);

                if (thread == null)
                    throw new Exception(ErrorMessages.InvalidId);

                var seen = new SeenCount()
                {
                    ThreadId = request.ThreadId,
                    UserId = request.SeenBy
                };

                _seenCountRepo.Create(seen);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<ThreadViewModel>> GetSeenCountAsync(ServiceRequestBase<ThreadViewModel> request)
        {
            return await Task.Run(() => GetSeenCount(request));
        }

        private ServiceResponseBase<ThreadViewModel> GetSeenCount(ServiceRequestBase<ThreadViewModel> request)
        {
            var response = new ServiceResponseBase<ThreadViewModel>();

            try
            {
                var thread = _repo.Get(t => t.Id == request.ID);

                if (thread == null)
                    throw new Exception(ErrorMessages.InvalidId);

                var res = _seenCountRepo.Get(t => t.ThreadId == request.ID);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<ThreadReplyViewModel>> AddThreadReplyAsync(ServiceRequestBase<ThreadReplyViewModel> request)
        {
            return await Task.Run(() => AddThreadReply(request));
        }

        private ServiceResponseBase<ThreadReplyViewModel> AddThreadReply(ServiceRequestBase<ThreadReplyViewModel> request)
        {
            var response = new ServiceResponseBase<ThreadReplyViewModel>();

            try
            {
                var thread = _repo.Get(t => t.Id == request.ID);

                if (thread == null)
                    throw new Exception(ErrorMessages.InvalidId);

                _replyRepository.Create(new ThreadReply(request.Item));
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<ThreadReplyViewModel>> EditThreadReplyAsync(ServiceRequestBase<ThreadReplyViewModel> request)
        {
            return await Task.Run(() => EditThreadReply(request));
        }

        private ServiceResponseBase<ThreadReplyViewModel> EditThreadReply(
            ServiceRequestBase<ThreadReplyViewModel> request)
        {
            var response = new ServiceResponseBase<ThreadReplyViewModel>();

            try
            {
                var reply = _replyRepository.Get(t => t.Id == request.ID);

                if (reply == null)
                    throw new Exception(ErrorMessages.InvalidId);

                _replyRepository.Update(new ThreadReply(request.Item));
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<ThreadReplyViewModel>> DeleteThreadReplyAsync(ServiceRequestBase<ThreadReplyViewModel> request)
        {
            return await Task.Run(() => DeleteThreadReply(request));
        }

        private ServiceResponseBase<ThreadReplyViewModel> DeleteThreadReply(
            ServiceRequestBase<ThreadReplyViewModel> request)
        {
            var response = new ServiceResponseBase<ThreadReplyViewModel>();

            try
            {
                var reply = _replyRepository.Get(t => t.Id == request.ID);

                if (reply == null)
                    throw new Exception(ErrorMessages.InvalidId);

                _replyRepository.Delete(new ThreadReply(request.Item));
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
