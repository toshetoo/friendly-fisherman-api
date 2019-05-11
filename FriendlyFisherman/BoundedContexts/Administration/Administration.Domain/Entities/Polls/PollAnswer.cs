using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities.Polls
{
    public class PollAnswer: BaseEntity
    {
        public string PollId { get; set; }
        public string Content { get; set; }

        public Poll Poll { get; set; }
    }
}
