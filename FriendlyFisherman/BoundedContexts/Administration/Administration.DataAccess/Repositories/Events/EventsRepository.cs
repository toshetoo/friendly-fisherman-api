using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;
using Administration.Domain.Repositories.Events;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccess.Repositories.Events
{
    public class EventsRepository: BaseRepository<Event>, IEventsRepository
    {
        public EventsRepository(AdministrationDbContext context) : base(context)
        {
        }
    }
}
