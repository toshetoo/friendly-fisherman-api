using System;
using System.Collections.Generic;
using Administration.Domain.Entities.Polls;

namespace Administration.Tests.DummyData
{
    public class TestData
    {
        // TODO fix ids
        public List<Poll> GetPollsData()
        {
            return new List<Poll>()
            {
                new Poll()
                {
                    Id = Constants.PollID,
                    Question = "Test",
                    CreatedBy = Constants.UserId,
                    CreatedOn = DateTime.Now,
                    EndOn = DateTime.Now.AddDays(2),
                    IsPollOfTheWeek = true,
                    Answers = new TestData().GetPollAnswers()
                },
                new Poll()
                {
                    Id = Guid.NewGuid().ToString(),
                    Question = "Test",
                    CreatedBy = Constants.AdminId,
                    CreatedOn = DateTime.Now,
                    EndOn = DateTime.Now.AddDays(2),
                    IsPollOfTheWeek = true,
                    Answers = new TestData().GetPollAnswers()
                },
                new Poll()
                {
                    Id = Constants.PollID,
                    Question = "Test",
                    CreatedBy = Constants.AdminId,
                    CreatedOn = DateTime.Now,
                    EndOn = DateTime.Now.AddDays(2),
                    IsPollOfTheWeek = true,
                    Answers = new TestData().GetPollAnswers()
                },
                new Poll()
                {
                    Id = Constants.PollID,
                    Question = "Test",
                    CreatedBy = Constants.AdminId,
                    CreatedOn = DateTime.Now,
                    EndOn = DateTime.Now.AddDays(2),
                    IsPollOfTheWeek = true,
                    Answers = new TestData().GetPollAnswers()
                }
            };
        }

        public List<PollAnswer> GetPollAnswers()
        {
            return new List<PollAnswer>()
            {
                new PollAnswer
                {
                    Id = Constants.PollAnswerId,
                    Content = "fdfsd",
                    PollId = Constants.PollID
                },
                new PollAnswer
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "fdfsd",
                    PollId = Constants.PollID
                },
                new PollAnswer
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "fdfsd",
                    PollId = Constants.PollID
                },
                new PollAnswer
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "fdfsd",
                    PollId = Constants.PollID
                },
                new PollAnswer
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = "fdfsd",
                    PollId = Constants.PollID
                }
            };
        }

        public List<UserPollAnswer> GetUserPollAnswers()
        {
            return new List<UserPollAnswer>
            {
                new UserPollAnswer()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Constants.UserId,
                    PollId = Constants.PollID,
                    AnswerId = Constants.PollAnswerId
                },
                new UserPollAnswer()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Constants.AdminId,
                    PollId = Constants.PollID,
                    AnswerId = Constants.PollAnswerId
                },
                new UserPollAnswer()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    PollId = Constants.PollID,
                    AnswerId = Constants.PollAnswerId
                },
                new UserPollAnswer()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    PollId = Constants.PollID,
                    AnswerId = Constants.PollAnswerId
                },
                new UserPollAnswer()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    PollId = Constants.PollID,
                    AnswerId = Constants.PollAnswerId
                },
                new UserPollAnswer()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    PollId = Constants.PollID,
                    AnswerId = Constants.PollAnswerId
                },
            };
        }
    }
}
