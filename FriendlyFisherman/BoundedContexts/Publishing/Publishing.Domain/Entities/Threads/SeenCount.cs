﻿using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Publishing.Domain.Entities.Threads
{
    public class SeenCount: BaseEntity
    {
        public string ThreadId { get; set; }
        public string UserId { get; set; }
    }
}