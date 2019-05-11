using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities;
using FriendlyFisherman.SharedKernel.Services.Abstraction;

namespace Administration.Services.Abstraction.Events
{
    public interface IEventsService: IBaseCrudService<Event>
    {
    }
}
