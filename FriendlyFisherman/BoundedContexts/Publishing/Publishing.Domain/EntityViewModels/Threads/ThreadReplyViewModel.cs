using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Publishing.Domain.EntityViewModels.Threads
{
    public class ThreadReplyViewModel
    {
        public string Id { get; set; }
        [Required]
        public string ThreadId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
