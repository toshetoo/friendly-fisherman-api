using System;
using System.Collections.Generic;
using FriendlyFisherman.SharedKernel.Services.Models;
using Publishing.Domain.EntityViewModels.Threads;

namespace Publishing.Domain.Entities.Threads
{
    public class ThreadReply: BaseEntity
    {
        public string ThreadId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }

        public Thread Thread { get; set; }

        public ThreadReply()
        {
            
        }

        public ThreadReply(ThreadReplyViewModel model)
        {
            Id = model.Id;
            ThreadId = model.ThreadId;
            UserId = model.UserId;
            Content = model.Content;
            CreatedOn = model.CreatedOn;
        }
    }
}
