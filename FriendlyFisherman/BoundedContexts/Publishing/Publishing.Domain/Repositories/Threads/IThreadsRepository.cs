﻿using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Repositories.Abstraction;
using Publishing.Domain.Entities.Threads;

namespace Publishing.Domain.Repositories.Threads
{
    public interface IThreadsRepository: IBaseRepository<Thread>
    {
    }
}
