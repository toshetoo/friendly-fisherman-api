using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities
{
    public class EventComment: BaseEntity
    {
        public string EventId { get; set; }
        public string CreatorId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
