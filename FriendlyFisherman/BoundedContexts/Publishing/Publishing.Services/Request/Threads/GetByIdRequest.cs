using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.Entities.Threads;

namespace Publishing.Services.Request.Threads
{
    public class GetByIdRequest: ServiceRequestBase<Thread>
    {
        public string UserId { get; set; }
    }
}
