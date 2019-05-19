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
        [Required]
        public int? IsLiked { get; set; }
    }
}
