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
    public class ThreadsService : BaseCrudService<ThreadViewModel, Thread, IThreadsRepository>, IThreadsService
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
                        .GetUserByIdAsync(new GetUserRequest { Id = thread.AuthorId }).Result.Item.ImagePath;
                }

                response.Items = threads;
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        protected override ServiceResponseBase<ThreadViewModel> GetById(ServiceRequestBase<Thread> request)
        {
            var mappedRequest = Mapper<GetByIdRequest, ServiceRequestBase<Thread>>.Map(request);
            var response = new ServiceResponseBase<ThreadViewModel>();

            try
            {
                var thread = Mapper<ThreadViewModel, Thread>.Map(_repo.Get(t => t.Id == request.ID));
                thread.Replies = Mapper<ThreadReplyViewModel, ThreadReply>.MapList(_replyRepository.GetWhere(rep => rep.ThreadId == thread.Id).ToList()).ToList();
                thread.AnswersCount = thread.Replies.Count();

                var threadReactions = _likesRepository.GetWhere(l => l.ThreadReplyId == thread.Id);
                thread.Likes = threadReactions.Count(l => l.IsLiked.HasValue && l.IsLiked == 1);
                thread.Dislikes = threadReactions.Count(l => l.IsLiked.HasValue && l.IsLiked == 0);
                thread.SeenCount = _seenCountRepo.GetWhere(seen => seen.ThreadId == thread.Id).Count();
                thread.AuthorImageUrl = _userService
                    .GetUserByIdAsync(new GetUserRequest { Id = thread.AuthorId }).Result.Item.ImagePath;

                var userLikeAnswer = threadReactions.FirstOrDefault(tr => tr.UserId == mappedRequest.UserId & tr.ThreadReplyId == thread.Id);
                if (userLikeAnswer != null)
                {
                    thread.UserLike = Mapper<LikeViewModel, Like>.Map(userLikeAnswer);
                }

                foreach (var reply in thread.Replies)
                {
                    reply.AuthorImageUrl = _userService
                        .GetUserByIdAsync(new GetUserRequest { Id = reply.UserId }).Result.Item.ImagePath;

                    var reactions = _likesRepository.GetWhere(l => l.ThreadReplyId == reply.Id);
                    reply.Likes = reactions.Count(l => l.IsLiked.HasValue && l.IsLiked == 1);
                    reply.Dislikes = reactions.Count(l => l.IsLiked.HasValue && l.IsLiked == 0);

                    var userLikeReply = reactions.FirstOrDefault(tr => tr.UserId == mappedRequest.UserId && tr.ThreadReplyId == reply.Id);
                    if (userLikeReply != null)
                    {
                        reply.UserLike = Mapper<LikeViewModel, Like>.Map(userLikeReply);
                    }
                }

                response.Item = thread;
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

                var existingSeen = _seenCountRepo.Get(s =>
                    s.UserId == request.SeenBy && s.ThreadId == request.ThreadId);

                if (existingSeen != null)
                {
                    return response;
                }

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
                var thread = _repo.Get(t => t.Id == request.Item.ThreadId);

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

                _replyRepository.Delete(reply);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<ThreadLikeViewModel>> LikeReplyAsync(ServiceRequestBase<LikeViewModel> request)
        {
            return await Task.Run(() => LikeReply(request));
        }

        private ServiceResponseBase<ThreadLikeViewModel> LikeReply(ServiceRequestBase<LikeViewModel> request)
        {
            var response = new ServiceResponseBase<ThreadLikeViewModel>();

            try
            {
                var like = Mapper<Like, LikeViewModel>.Map(request.Item);

                var existingLike =
                    _likesRepository.Get(l => l.ThreadReplyId == like.ThreadReplyId && l.UserId == like.UserId);

                if (existingLike != null)
                {
                    if (like.IsLiked == existingLike.IsLiked) like.IsLiked = null;

                    existingLike.IsLiked = like.IsLiked;
                }

                // If we remove our vote
                if (like.IsLiked == null)
                {
                    _likesRepository.Delete(existingLike);

                    response.Item = new ThreadLikeViewModel()
                    {
                        Likes = _likesRepository.GetWhere(l => l.ThreadReplyId == request.Item.ThreadReplyId)
                            .GroupBy(l => l.IsLiked)
                            .ToDictionary(e => e.Key, e => e.Count())
                    };

                    var userLikeReply = _likesRepository.Get(tr => tr.UserId == like.UserId && tr.ThreadReplyId == like.ThreadReplyId);
                    if (userLikeReply != null)
                    {
                        response.Item.UserLike = Mapper<LikeViewModel, Like>.Map(userLikeReply);
                    }

                    return response;
                }

                if (existingLike != null && existingLike.Id != null)
                {
                    _likesRepository.Update(existingLike);
                }
                else
                {
                    _likesRepository.Create(like);
                }

                response.Item = new ThreadLikeViewModel
                {
                    Likes = _likesRepository.GetWhere(l => l.ThreadReplyId == request.Item.ThreadReplyId)
                        .GroupBy(l => l.IsLiked)
                        .ToDictionary(e => e.Key, e => e.Count())
                };

                var userLike = _likesRepository.Get(tr => tr.UserId == like.UserId && tr.ThreadReplyId == like.ThreadReplyId);
                if (userLike != null)
                {
                    response.Item.UserLike = Mapper<LikeViewModel, Like>.Map(userLike);
                }
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<LikeViewModel>> GetUserLikeAsync(ServiceRequestBase<LikeViewModel> request)
        {
            return await Task.Run(() => GetUserLike(request));
        }

        private ServiceResponseBase<LikeViewModel> GetUserLike(ServiceRequestBase<LikeViewModel> request)
        {
            var response = new ServiceResponseBase<LikeViewModel>();

            try
            {
                var userLike = _likesRepository.Get(like =>
                    like.ThreadReplyId == request.Item.ThreadReplyId && like.UserId == request.Item.UserId);

                if(userLike != null)
                    response.Item = Mapper<LikeViewModel, Like>.Map(userLike);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }
    }
}
