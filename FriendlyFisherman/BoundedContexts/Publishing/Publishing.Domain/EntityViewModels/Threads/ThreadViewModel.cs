using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Publishing.Domain.Entities.Threads;

namespace Publishing.Domain.EntityViewModels.Threads
{
    public class ThreadViewModel
    {
        public string Id { get; set; }

        [Required] public string CategoryId { get; set; }

        [Required] public string AuthorId { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Content { get; set; }

        [Required] public DateTime CreatedOn { get; set; }

        public int SeenCount { get; set; }
        public int AnswersCount { get; set; }
        public string AuthorImageUrl { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public LikeViewModel UserLike { get; set; }

        public List<ThreadReplyViewModel> Replies { get; set; }
    }
}