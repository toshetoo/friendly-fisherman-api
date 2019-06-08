using System.Collections.Generic;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.Repositories.Threads;

namespace Publishing.DataAccess.Repositories.Threads
{
    public class ThreadsRepository: BaseRepository<Thread>, IThreadsRepository
    {
        public ThreadsRepository(PublishingDbContext context) : base(context)
        {
        }
    }
}
