﻿using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Publishing.Domain.Entities.Threads;
using Publishing.Domain.Repositories.Threads;

namespace Publishing.DataAccess.Repositories.Threads
{
    public class ThreadReplyRepository: BaseRepository<ThreadReply>, IThreadReplyRepository
    {
        public ThreadReplyRepository(PublishingDbContext context) : base(context)
        {
        }
    }
}
