﻿using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel;
using Publishing.Domain.Entities.Threads;

namespace Publishing.Services.Request.Threads
{
    public class MarkThreadAsSeenRequest: ServiceRequestBase<Thread>
    {
        public string SeenBy { get; set; }
        public bool IsSeen { get; set; }
    }
}