using System.ComponentModel.DataAnnotations;

namespace Users.Domain.EntityViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordToken { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RePassword { get; set; }    
    }
}
