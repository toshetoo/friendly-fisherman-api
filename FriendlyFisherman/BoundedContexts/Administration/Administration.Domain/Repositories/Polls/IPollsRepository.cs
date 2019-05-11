using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities.Polls;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;

namespace Administration.Domain.Repositories.Polls
{
    public interface IPollsRepository: IBaseRepository<Poll>
    {
    }
}
