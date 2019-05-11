using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities.Polls;
using FriendlyFisherman.SharedKernel.Services.Abstraction;

namespace Administration.Services.Abstraction.Polls
{
    public interface IPollsService: IBaseCrudService<Poll>
    {
    }
}
