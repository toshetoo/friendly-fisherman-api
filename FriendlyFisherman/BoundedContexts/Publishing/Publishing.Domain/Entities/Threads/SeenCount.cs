using FriendlyFisherman.SharedKernel.Services.Models;

namespace Publishing.Domain.Entities.Threads
{
    public class SeenCount: BaseEntity
    {
        public string ThreadId { get; set; }
        public string UserId { get; set; }

        public Thread Thread;
    }
}
