using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Publishing.Domain.EntityViewModels.Categories
{
    public class ThreadCategoryViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
