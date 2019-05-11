using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;

namespace Administration.Domain.Repositories.Events
{
    public interface IEventCommentsRepository: IBaseRepository<EventComment>
    {
    }
}
