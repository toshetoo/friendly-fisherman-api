using System;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities.Events
{
    public class EventComment: BaseEntity
    {
        public string EventId { get; set; }
        public string CreatorId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
