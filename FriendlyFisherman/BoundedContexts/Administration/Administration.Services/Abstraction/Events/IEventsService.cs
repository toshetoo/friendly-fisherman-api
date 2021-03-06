﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;
using Administration.Domain.EntityViewModels.Events;
using Administration.Services.Request;
using Administration.Services.Response;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Services.Abstraction.Events
{
    public interface IEventsService: IBaseCrudService<EventViewModel, Event>
    {
        Task<ServiceResponseBase<EventViewModel>> GetLatestEventsAsync(ServiceRequestBase<EventViewModel> request);
        Task<ServiceResponseBase<EventParticipantViewModel>> GetParticipantsByEventIdAsync(ServiceRequestBase<EventParticipantViewModel> request);
        Task<ServiceResponseBase<EventParticipantViewModel>> GetParticipantByEventIdAndUserIdAsync(ServiceRequestBase<EventParticipantViewModel> request);
        Task<ServiceResponseBase<EventParticipantViewModel>> AddParticipantAsync(ServiceRequestBase<EventParticipantViewModel> request);
        Task<ServiceResponseBase<EventParticipantViewModel>> DeleteParticipantAsync(ServiceRequestBase<EventParticipantViewModel> request);

        Task<ServiceResponseBase<EventCommentViewModel>> GetCommentsByEventIdAsync(ServiceRequestBase<EventCommentViewModel> request);
        Task<ServiceResponseBase<EventCommentViewModel>> AddCommentAsync(ServiceRequestBase<EventCommentViewModel> request);
        Task<ServiceResponseBase<EventCommentViewModel>> EditCommentAsync(ServiceRequestBase<EventCommentViewModel> request);
        Task<ServiceResponseBase<EventCommentViewModel>> DeleteCommentAsync(ServiceRequestBase<EventCommentViewModel> request);
        Task<SaveImageCoverResponse> SaveImageCoverAsync(SaveImageCoverRequest request);
    }
}
