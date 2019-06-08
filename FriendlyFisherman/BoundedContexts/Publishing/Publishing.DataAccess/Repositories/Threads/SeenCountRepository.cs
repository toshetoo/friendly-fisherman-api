using System.Collections.Generic;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.Repositories.Threads;

namespace Publishing.DataAccess.Repositories.Threads
{
    public class SeenCountRepository : BaseRepository<SeenCount>, ISeenCountRepository
    {
        public SeenCountRepository(PublishingDbContext context) : base(context)
        {
        }
    }
}
