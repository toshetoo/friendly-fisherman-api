using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.Entities.Polls;

namespace Administration.Domain.EntityViewModels.Polls
{
    public class PollAnswerViewModel
    {
        public string Id { get; set; }
        public string PollId { get; set; }
        public string Content { get; set; }

        public double Percentage { get; set; }

        public PollAnswerViewModel()
        {
            
        }

        public PollAnswerViewModel(PollAnswer pollAnswer)
        {
            Id = pollAnswer.Id;
            PollId = pollAnswer.PollId;
            Content = pollAnswer.Content;
        }
    }
}
