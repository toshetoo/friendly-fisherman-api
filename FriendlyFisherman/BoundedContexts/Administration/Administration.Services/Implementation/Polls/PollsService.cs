using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities.Polls;
using Administration.Domain.Repositories.Polls;
using Administration.Services.Abstraction.Polls;
using FriendlyFisherman.SharedKernel.Services.Impl;

namespace Administration.Services.Implementation.Polls
{
    public class PollsService: BaseCrudService<Poll, IPollsRepository>, IPollsService
    {
        public PollsService(IPollsRepository repo) : base(repo)
        {
        }
    }
}
