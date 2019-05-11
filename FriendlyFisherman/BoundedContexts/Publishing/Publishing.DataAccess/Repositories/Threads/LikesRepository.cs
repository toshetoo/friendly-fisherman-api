using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.Repositories.Threads;

namespace Publishing.DataAccess.Repositories.Threads
{
    public class LikesRepository : BaseRepository<Like>, ILikesRepository
    {
        public LikesRepository(PublishingDbContext context) : base(context)
        {

        }
    }
}
