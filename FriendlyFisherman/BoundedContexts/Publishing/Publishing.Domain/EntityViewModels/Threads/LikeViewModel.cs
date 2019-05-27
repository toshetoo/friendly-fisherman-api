using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Publishing.Domain.EntityViewModels.Threads
{
    public class LikeViewModel
    {
        public string Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public string ThreadReplyId { get; set; }
        public int? IsLiked { get; set; }

        public Dictionary<int?, int> Likes { get; set; }
    }
}
