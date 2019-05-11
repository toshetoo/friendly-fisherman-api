﻿using System;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Administration.Domain.Entities
{
    public class Event: BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageCover { get; set; }
        public EventStatus EventStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public enum EventStatus
    {
        Active, Pending, Completed
    }
}
