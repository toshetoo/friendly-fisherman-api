﻿using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel;
using Publishing.Domain.Entities.Threads;

namespace Publishing.Domain.Repositories.Threads
{
    public interface ISeenCountRepository: IBaseRepository<SeenCount>
    {
        SeenCount GetByThreadId(string id);
    }
}
