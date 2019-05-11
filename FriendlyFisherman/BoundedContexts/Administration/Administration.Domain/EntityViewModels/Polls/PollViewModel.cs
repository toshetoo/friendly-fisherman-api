using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Administration.Domain.Entities.Polls;

namespace Administration.Domain.EntityViewModels.Polls
{
    public class PollViewModel
    {
        public string Id { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime EndOn { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public string Question { get; set; }
        [Required]
        public List<PollAnswer> Answers { get; set; }
    }
}
