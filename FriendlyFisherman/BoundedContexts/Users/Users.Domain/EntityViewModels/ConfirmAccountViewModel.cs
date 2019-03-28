using System.ComponentModel.DataAnnotations;

namespace Users.Domain.EntityViewModels
{
    public class ConfirmAccountViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
