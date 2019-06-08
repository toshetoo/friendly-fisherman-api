using System.ComponentModel.DataAnnotations;

namespace FriendlyFisherman.SharedKernel.ViewModels
{
    public class ImageUploadViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ImageSource { get; set; }

        public string ImageName { get; set; }
    }
}
