using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;

namespace Administration.Domain.EntityViewModels.Events
{
    public class EventCommentViewModel
    {
        public string Id { get; set; }
        [Required]
        public string EventId { get; set; }
        [Required]
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string CreatorProfileImagePath { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
