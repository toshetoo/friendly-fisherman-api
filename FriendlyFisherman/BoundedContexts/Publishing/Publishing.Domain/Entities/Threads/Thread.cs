using System;
using System.Collections.Generic;
using System.Text;
using FriendlyFisherman.SharedKernel.Services.Models;

namespace Publishing.Domain.Entities.Threads
{
    public class Thread: BaseEntity
    {
        public string CategoryId { get; set; }  
        public string AuthorId { get; set; }  
        public string Title { get; set; }  
        public string Subtitle { get; set; }  
        public DateTime CreatedOn { get; set; }  
    }
}
