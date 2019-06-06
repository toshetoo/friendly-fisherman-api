using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;
using Administration.Domain.EntityViewModels.Events;
using Administration.Domain.Repositories.Events;
using Administration.Services.Abstraction.Events;
using Administration.Services.Request;
using Administration.Services.Response;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Helpers;
using FriendlyFisherman.SharedKernel.Services.Impl;
using FriendlyFisherman.SharedKernel.Services.Models;
using Users.Domain.Repositories;

namespace Administration.Services.Implementation.Events
{
    public class EventsService : BaseCrudService<EventViewModel, Event, IEventsRepository>, IEventsService
    {
        private readonly IEventParticipantsRepository _participantsRepository;
        private readonly IEventCommentsRepository _eventCommentsRepository;
        private readonly IUserRepository _usersUserRepository;
        private readonly AppSettings _appSettings;

        public EventsService(IEventsRepository repo,
            IEventParticipantsRepository participantsRepository,
            IEventCommentsRepository eventCommentsRepository,
            IUserRepository userRepository,
            AppSettings appSettings) : base(repo)
        {
            _participantsRepository = participantsRepository;
            _eventCommentsRepository = eventCommentsRepository;
            _usersUserRepository = userRepository;
            _appSettings = appSettings;
        }
        public async Task<ServiceResponseBase<EventViewModel>> GetLatestEventsAsync(ServiceRequestBase<EventViewModel> request)
        {
            return await Task.Run(() => GetLatestEvents(request));
        }

        private ServiceResponseBase<EventViewModel> GetLatestEvents(
            ServiceRequestBase<EventViewModel> request)
        {
            var response = new ServiceResponseBase<EventViewModel>();

            try
            {
                response.Items = Mapper<EventViewModel, Event>.MapList(_repo.GetAll(true, ev => ev.StartDate).Take(3).ToList());
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<EventParticipantViewModel>> GetParticipantsByEventIdAsync(ServiceRequestBase<EventParticipantViewModel> request)
        {
            return await Task.Run(() => GetParticipantsByEventId(request));
        }

        private ServiceResponseBase<EventParticipantViewModel> GetParticipantsByEventId(
            ServiceRequestBase<EventParticipantViewModel> request)
        {
            var response = new ServiceResponseBase<EventParticipantViewModel>();

            try
            {
                response.Items = _participantsRepository.GetWhere(p => p.EventId == request.ID && p.ParticipantStatus != ParticipantStatus.NotGoing).Select(par => new EventParticipantViewModel()
                {
                    Id = par.Id,
                    UserId = par.UserId,
                    EventId = par.EventId,
                    ParticipantStatus = par.ParticipantStatus
                });
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<EventParticipantViewModel>> GetParticipantByEventIdAndUserIdAsync(ServiceRequestBase<EventParticipantViewModel> request)
        {
            return await Task.Run(() => GetParticipantByEventIdAndUserId(request));
        }

        private ServiceResponseBase<EventParticipantViewModel> GetParticipantByEventIdAndUserId(
            ServiceRequestBase<EventParticipantViewModel> request)
        {
            var response = new ServiceResponseBase<EventParticipantViewModel>();

            try
            {
                response.Item = _participantsRepository.GetWhere(p => p.EventId == request.Item.EventId && p.UserId == request.Item.UserId).Select(par => new EventParticipantViewModel()
                {
                    Id = par.Id,
                    UserId = par.UserId,
                    EventId = par.EventId,
                    ParticipantStatus = par.ParticipantStatus
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<EventParticipantViewModel>> AddParticipantAsync(ServiceRequestBase<EventParticipantViewModel> request)
        {
            return await Task.Run(() => AddParticipant(request));
        }

        private ServiceResponseBase<EventParticipantViewModel> AddParticipant(
            ServiceRequestBase<EventParticipantViewModel> request)
        {
            var response = new ServiceResponseBase<EventParticipantViewModel>();

            try
            {
                var participant = _participantsRepository.Get(x => x.UserId == request.Item.UserId && x.EventId == request.Item.EventId);
                if (participant == null)
                {
                    var newParticipant = Mapper<EventParticipant, EventParticipantViewModel>.Map(request.Item);
                    newParticipant.Id = Guid.NewGuid().ToString();
                    _participantsRepository.Create(newParticipant);

                    response.Item = Mapper<EventParticipantViewModel, EventParticipant>.Map(newParticipant);
                }
                else
                {
                    request.Item.Id = participant.Id;
                    var updateParticipant = Mapper<EventParticipant, EventParticipantViewModel>.Map(request.Item);
                    updateParticipant.Id = participant.Id;
                    _participantsRepository.Update(updateParticipant);

                    response.Item = request.Item;
                }
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        public async Task<ServiceResponseBase<EventParticipantViewModel>> DeleteParticipantAsync(ServiceRequestBase<EventParticipantViewModel> request)
        {
            return await Task.Run(() => DeleteParticipant(request));
        }

        private ServiceResponseBase<EventParticipantViewModel> DeleteParticipant(
            ServiceRequestBase<EventParticipantViewModel> request)
        {
            var response = new ServiceResponseBase<EventParticipantViewModel>();

            try
            {
                var participant = Mapper<EventParticipant, EventParticipantViewModel>.Map(request.Item);
                _participantsRepository.Delete(participant);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }


        public async Task<ServiceResponseBase<EventCommentViewModel>> GetCommentsByEventIdAsync(ServiceRequestBase<EventCommentViewModel> request)
        {
            return await Task.Run(() => GetCommentsByEventId(request));
        }

        private ServiceResponseBase<EventCommentViewModel> GetCommentsByEventId(ServiceRequestBase<EventCommentViewModel> request)
        {
            var response = new ServiceResponseBase<EventCommentViewModel>();

            try
            {
                var comments = _eventCommentsRepository.GetWhere(comment => comment.EventId == request.ID)
                     .Select(c => new EventCommentViewModel
                     {
                         Id = c.Id,
                         Content = c.Content,
                         EventId = c.EventId,
                         CreatedOn = c.CreatedOn,
                         CreatorId = c.CreatorId,
                     }).ToList();

                foreach (var comment in comments)
                {
                    var creator = _usersUserRepository.GetById(comment.CreatorId);

                    comment.CreatorProfileImagePath = creator.ImagePath;
                    comment.CreatorName = creator.FirstName + " " + creator.LastName;
                }

                response.Items = comments;
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<ServiceResponseBase<EventCommentViewModel>> AddCommentAsync(ServiceRequestBase<EventCommentViewModel> request)
        {
            return await Task.Run(() => AddComment(request));
        }

        private ServiceResponseBase<EventCommentViewModel> AddComment(ServiceRequestBase<EventCommentViewModel> request)
        {
            var response = new ServiceResponseBase<EventCommentViewModel>();

            try
            {
                if (string.IsNullOrEmpty(request.Item.Id))
                {
                    var comment = Mapper<EventComment, EventCommentViewModel>.Map(request.Item);
                    comment.Id = Guid.NewGuid().ToString();

                    _eventCommentsRepository.Create(comment);
                }
                else
                {
                    var comment = Mapper<EventComment, EventCommentViewModel>.Map(request.Item);
                    _eventCommentsRepository.Update(comment);
                }
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<ServiceResponseBase<EventCommentViewModel>> EditCommentAsync(ServiceRequestBase<EventCommentViewModel> request)
        {
            return await Task.Run(() => EditComment(request));
        }

        private ServiceResponseBase<EventCommentViewModel> EditComment(
            ServiceRequestBase<EventCommentViewModel> request)
        {
            var response = new ServiceResponseBase<EventCommentViewModel>();

            try
            {
                var comment = Mapper<EventComment, EventCommentViewModel>.Map(request.Item);
                _eventCommentsRepository.Update(comment);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<ServiceResponseBase<EventCommentViewModel>> DeleteCommentAsync(ServiceRequestBase<EventCommentViewModel> request)
        {
            return await Task.Run(() => DeleteComment(request));
        }

        private ServiceResponseBase<EventCommentViewModel> DeleteComment(
            ServiceRequestBase<EventCommentViewModel> request)
        {
            var response = new ServiceResponseBase<EventCommentViewModel>();

            try
            {
                var comment = _eventCommentsRepository.Get(x => x.Id == request.ID);
                _eventCommentsRepository.Delete(comment);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }

        public async Task<SaveImageCoverResponse> SaveImageCoverAsync(SaveImageCoverRequest request)
        {
            return await Task.Run(() => SaveImageCover(request));
        }

        private SaveImageCoverResponse SaveImageCover(SaveImageCoverRequest request)
        {
            var response = new SaveImageCoverResponse();

            try
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(request.ImageName);
                string filePath = FileHelper.BuildFilePath(_appSettings.FileUploadSettings.FilesUploadFolder, fileName);

                FileHelper.CreateFile(request.ImageData.Split(',').Last(), filePath);

                response.ImageCoverFilePath = filePath.Replace(filePath.Split('/').First(), string.Empty);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }
    }
}
