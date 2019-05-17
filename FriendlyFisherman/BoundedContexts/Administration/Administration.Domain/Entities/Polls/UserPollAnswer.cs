using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities.Polls
{
    public class UserPollAnswer: BaseEntity
    {
        public string UserId { get; set; }
        public string PollId { get; set; }
        public string AnswerId { get; set; }

        public PollAnswer Answer { get; set; }
    }
}
