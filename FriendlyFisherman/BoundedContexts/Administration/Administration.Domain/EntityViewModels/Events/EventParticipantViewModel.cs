using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;

namespace Administration.Domain.EntityViewModels.Events
{
    public class EventParticipantViewModel
    {
        public string Id { get; set; }
        [Required]
        public string EventId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public ParticipantStatus ParticipantStatus { get; set; }
    }
}
