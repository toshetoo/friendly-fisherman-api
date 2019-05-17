using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Administration.Domain.Entities.Polls;

namespace Administration.Domain.EntityViewModels.Polls
{
    public class PollViewModel
    {
        public string Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime EndOn { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string Question { get; set; }

        public bool IsPollOfTheWeek { get; set; }
        [Required]
        public List<PollAnswerViewModel> Answers { get; set; }

        public PollViewModel()
        {
            
        }

        public PollViewModel(Poll poll)
        {
            Id = poll.Id;
            CreatedBy = poll.CreatedBy;
            CreatedOn = poll.CreatedOn;
            EndOn = poll.EndOn;
            IsPollOfTheWeek = poll.IsPollOfTheWeek;
            Question = poll.Question;

            if (poll.Answers != null)
            {
                Answers = poll.Answers.Select(p => new PollAnswerViewModel(p)).ToList();
            }
        }
    }
}
