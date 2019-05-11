using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities;
using Administration.Domain.Repositories.Events;
using Administration.Services.Abstraction.Events;
using FriendlyFisherman.SharedKernel.Services.Impl;

namespace Administration.Services.Implementation.Events
{
    public class EventsService: BaseCrudService<Event, IEventsRepository>, IEventsService
    {
        public EventsService(IEventsRepository repo) : base(repo)
        {
        }
    }
}
