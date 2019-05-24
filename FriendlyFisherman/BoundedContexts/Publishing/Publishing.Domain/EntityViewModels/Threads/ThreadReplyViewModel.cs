using System;
using System.ComponentModel.DataAnnotations;

namespace Publishing.Domain.EntityViewModels.Threads
{
    public class ThreadReplyViewModel
    {
        public string Id { get; set; }
        [Required]
        public string ThreadId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }
        public int Dislikes { get; set; }

        public LikeViewModel UserLike { get; set; }
        public string AuthorImageUrl { get; set; }
    }
}
