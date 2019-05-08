using System.ComponentModel.DataAnnotations;

namespace Users.Domain.EntityViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
