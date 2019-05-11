using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;
using Administration.Domain.EntityViewModels.Events;
using Administration.Domain.Repositories.Events;
using Administration.Services.Abstraction.Events;
using FriendlyFisherman.SharedKernel.Services.Impl;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Services.Implementation.Events
{
    public class EventsService: BaseCrudService<Event, IEventsRepository>, IEventsService
    {
        private readonly IEventParticipantsRepository _participantsRepository;
        private readonly IEventCommentsRepository _eventCommentsRepository;

        public EventsService(IEventsRepository repo, 
            IEventParticipantsRepository participantsRepository,
            IEventCommentsRepository eventCommentsRepository) : base(repo)
        {
            _participantsRepository = participantsRepository;
            _eventCommentsRepository = eventCommentsRepository;
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
                response.Items = _participantsRepository.GetWhere(p => p.EventId == request.ID).Select(par => new EventParticipantViewModel(par));
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
                var participant = new EventParticipant
                {
                    Id = request.Item.Id,
                    UserId = request.Item.UserId,
                    EventId = request.Item.EventId,
                    ParticipantStatus = request.Item.ParticipantStatus
                };

                _participantsRepository.Create(participant);
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
                var participant = new EventParticipant
                {
                    Id = request.Item.Id,
                    UserId = request.Item.UserId,
                    EventId = request.Item.EventId,
                    ParticipantStatus = request.Item.ParticipantStatus
                };

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

        private ServiceResponseBase<EventCommentViewModel> GetCommentsByEventId(
            ServiceRequestBase<EventCommentViewModel> request)
        {
            var response = new ServiceResponseBase<EventCommentViewModel>();

            try
            {
                response.Items = _eventCommentsRepository.GetWhere(comment => comment.EventId == request.ID)
                    .Select(c => new EventCommentViewModel(c));
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

        private ServiceResponseBase<EventCommentViewModel> AddComment(
            ServiceRequestBase<EventCommentViewModel> request)
        {
            var response = new ServiceResponseBase<EventCommentViewModel>();

            try
            {
                var comment = new EventComment
                {
                    Id = request.Item.Id,
                    CreatedOn = request.Item.CreatedOn,
                    EventId = request.Item.EventId,
                    Content = request.Item.Content,
                    CreatorId = request.Item.CreatorId
                };

                _eventCommentsRepository.Create(comment);
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
                var comment = new EventComment
                {
                    Id = request.Item.Id,
                    CreatedOn = request.Item.CreatedOn,
                    EventId = request.Item.EventId,
                    Content = request.Item.Content,
                    CreatorId = request.Item.CreatorId
                };

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
                var comment = new EventComment
                {
                    Id = request.Item.Id,
                    CreatedOn = request.Item.CreatedOn,
                    EventId = request.Item.EventId,
                    Content = request.Item.Content,
                    CreatorId = request.Item.CreatorId
                };

                _eventCommentsRepository.Delete(comment);
            }
            catch (Exception e)
            {
                response.Exception = e;
            }


            return response;
        }
    }
}
