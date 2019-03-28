using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Users.Domain.EntityViewModels
{
    public class ImageUploadViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ImageSource { get; set; }
    }
}
