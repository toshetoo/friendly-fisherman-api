using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities.Polls;
using Administration.Domain.Repositories.Polls;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace Administration.DataAccess.Repositories.Polls
{
    public class PollsRepository: BaseRepository<Poll>, IPollsRepository
    {
        public PollsRepository(AdministrationDbContext context) : base(context)
        {
        }
    }
}
