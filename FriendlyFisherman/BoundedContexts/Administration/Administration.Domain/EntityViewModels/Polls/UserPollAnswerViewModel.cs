using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        public int AnswerIndex { get; set; }
    }
}
