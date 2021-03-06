﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Administration.Domain.Entities;
using Administration.Domain.Entities.Events;

namespace Administration.Domain.EntityViewModels.Events
{
    public class EventViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageCover { get; set; }
        public EventStatus EventStatus { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string ImageData { get; set; }
        public string ImageName { get; set; }
    }
}
