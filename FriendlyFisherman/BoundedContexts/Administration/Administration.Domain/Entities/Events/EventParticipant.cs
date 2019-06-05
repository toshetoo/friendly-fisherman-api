using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities.Events
{
    public class EventParticipant: BaseEntity
    {
        public string EventId { get; set; }
        public string UserId { get; set; }
        public ParticipantStatus ParticipantStatus { get; set; }
    }

    public enum ParticipantStatus
    {
        Going, Interested, NotGoing
    }
}
