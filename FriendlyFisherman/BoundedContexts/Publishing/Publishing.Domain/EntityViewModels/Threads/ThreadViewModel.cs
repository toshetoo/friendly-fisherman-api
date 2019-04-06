using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Publishing.Domain.EntityViewModels.Threads
{
    public class ThreadViewModel
    {
        public string Id { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Subtitle { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
