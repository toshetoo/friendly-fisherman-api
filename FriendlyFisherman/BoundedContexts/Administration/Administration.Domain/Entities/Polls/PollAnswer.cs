﻿using System;
using System.Collections.Generic;
using System.Text;
using Administration.Domain.EntityViewModels.Polls;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities.Polls
{
    public class PollAnswer: BaseEntity
    {
        public string PollId { get; set; }
        public string Content { get; set; }

        public Poll Poll { get; set; }

        public PollAnswer()
        {
            
        }

        public PollAnswer(PollAnswerViewModel pollAnswer)
        {
            Id = pollAnswer.Id;
            PollId = pollAnswer.PollId;
            Content = pollAnswer.Content;
        }
    }

    
}
