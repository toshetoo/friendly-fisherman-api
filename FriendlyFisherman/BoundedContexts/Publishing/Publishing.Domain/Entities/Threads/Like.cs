using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Publishing.Domain.Entities.Threads
{
    public class Like: BaseEntity
    {
        public string UserId { get; set; }
        public string ThreadReplyId { get; set; }
        public int? IsLiked { get; set; }
    }
}
