using System;
using System.Linq;
using System.Threading.Tasks;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Messages;
using FriendlyFisherman.SharedKernel.Services.Impl;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.EntityViewModels.Threads;
using Publishing.Domain.Repositories.Threads;
using Publishing.Services.Abstraction.Threads;
using Publishing.Services.Request.Threads;
using Users.Services.Abstraction;
using Users.Services.Request;

namespace Publishing.Services.Implementation.Threads
{
    public class ThreadsService: BaseCrudService<ThreadViewModel, Thread, IThreadsRepository>, IThreadsService
    {
        private readonly ISeenCountRepository _seenCountRepo;
        private readonly IThreadReplyRepository _replyRepository;
        private readonly ILikesRepository _likesRepository;
        private readonly IUserService _userService;

        public ThreadsService(IThreadsRepository repo, 
            ISeenCountRepository seenCountRepo, 
            IThreadReplyRepository replyRepository, 
            ILikesRepository likesRepository,
            IUserService userService) : base(repo)
        {
            _seenCountRepo = seenCountRepo;
            _replyRepository = replyRepository;
            _likesRepository = likesRepository;
            _userService = userService;
        }

        protected override ServiceResponseBase<ThreadViewModel> GetAll(ServiceRequestBase<Thread> request)
        {
            var response = new ServiceResponseBase<ThreadViewModel>();

            try
            {
                var allThreads = _repo.GetAll().ToList();
                var threads = Mapper<ThreadViewModel, Thread>.MapList(allThreads);

                foreach (var thread in threads)
                {
                    thread.SeenCount = _seenCountRepo.GetWhere(seen => seen.ThreadId == thread.Id).Count();
                    thread.AnswersCount = _replyRepository.GetWhere(rep => rep.ThreadId == thread.Id).Count();
                    thread.AuthorImageUrl = _userService
                        .GetUserByIdAsync(new GetUserRequest {Id = thread.AuthorId}).Result.Item.ImagePath;
                }

                response.Items = threads;
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
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

                response.Item.SeenCount = _seenCountRepo.GetWhere(t => t.ThreadId == request.ID).Count();
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

        public async Task<ServiceResponseBase<LikeViewModel>> LikeReplyAsync(ServiceRequestBase<LikeViewModel> request)
        {
            return await Task.Run(() => LikeReply(request));
        }

        private ServiceResponseBase<LikeViewModel> LikeReply(ServiceRequestBase<LikeViewModel> request)
        {
            var response = new ServiceResponseBase<LikeViewModel>();

            try
            {
                var like = Mapper<Like, LikeViewModel>.Map(request.Item);

                // If we remove our vote
                if (request.Item.IsLiked == null)
                {
                    _likesRepository.Delete(like);

                    return response;
                }

                if (request.Item.Id != null)
                {
                    _likesRepository.Update(like);
                }
                else
                {
                    _likesRepository.Create(like);
                }
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
