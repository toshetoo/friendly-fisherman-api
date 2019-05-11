using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities;
using Administration.Domain.Repositories.Events;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccess.Repositories.Events
{
    public class EventParticipantsRepository : BaseRepository<EventParticipant>, IEventParticipantsRepository
    {
        public EventParticipantsRepository(AdministrationDbContext context) : base(context)
        {
        }
    }
}
