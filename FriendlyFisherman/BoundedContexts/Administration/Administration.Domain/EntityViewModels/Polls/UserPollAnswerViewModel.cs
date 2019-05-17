using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Administration.Domain.Entities.Polls;

namespace Administration.Domain.EntityViewModels.Polls
{
    public class UserPollAnswerViewModel
    {
        public string Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string PollId { get; set; }
        [Required]
        public string AnswerId { get; set; }

        public PollAnswer Answer;
    }
}
