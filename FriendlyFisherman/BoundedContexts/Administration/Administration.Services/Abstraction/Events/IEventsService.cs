using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Administration.Domain.Entities;
using Administration.Domain.EntityViewModels.Events;
using FriendlyFisherman.SharedKernel.Services.Abstraction;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Services.Abstraction.Events
{
    public interface IEventsService: IBaseCrudService<Event>
    {
        Task<ServiceResponseBase<EventParticipantViewModel>> GetParticipantsByEventIdAsync(ServiceRequestBase<EventParticipantViewModel> request);
        Task<ServiceResponseBase<EventParticipantViewModel>> AddParticipantAsync(ServiceRequestBase<EventParticipantViewModel> request);
        Task<ServiceResponseBase<EventParticipantViewModel>> DeleteParticipantAsync(ServiceRequestBase<EventParticipantViewModel> request);
    }
}
