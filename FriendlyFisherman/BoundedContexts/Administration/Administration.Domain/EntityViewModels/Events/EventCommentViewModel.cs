using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Administration.Domain.Entities;

namespace Administration.Domain.EntityViewModels.Events
{
    public class EventCommentViewModel
    {
        public string Id { get; set; }
        [Required]
        public string EventId { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        public EventCommentViewModel(EventComment c)
        {
            Id = c.Id;
            Content = c.Content;
            CreatedOn = c.CreatedOn;
            CreatorId = c.CreatorId;
            EventId = c.EventId;
        }
    }
}
