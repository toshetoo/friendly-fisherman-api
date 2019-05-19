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

        [Required] public string Subtitle { get; set; }

        [Required] public DateTime CreatedOn { get; set; }

        public int SeenCount { get; set; }
        public int AnswersCount { get; set; }
        public string AuthorImageUrl { get; set; }

        public List<ThreadReply> Replies;


        public ThreadViewModel()
        {
            
        }
        public ThreadViewModel(Thread t)
        {
            Id = t.Id;
            AuthorId = t.AuthorId;
            CategoryId = t.CategoryId;
            CreatedOn = t.CreatedOn;
            Subtitle = t.Subtitle;
            Title = t.Title;
            Replies = t.Replies;
            
        }
    }
}