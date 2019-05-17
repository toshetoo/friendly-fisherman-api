using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities.Polls
{
    public class Poll : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime EndOn { get; set; }
        public string CreatedBy { get; set; }
        public string Question { get; set; }

        public bool IsPollOfTheWeek { get; set; }
        public List<PollAnswer> Answers { get; set; }
    }
}
