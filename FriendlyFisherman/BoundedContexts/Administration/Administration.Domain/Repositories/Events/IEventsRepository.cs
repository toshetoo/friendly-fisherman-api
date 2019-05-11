using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;

namespace Administration.Domain.Repositories.Events
{
    public interface IEventsRepository: IBaseRepository<Event>
    {
    }
}
