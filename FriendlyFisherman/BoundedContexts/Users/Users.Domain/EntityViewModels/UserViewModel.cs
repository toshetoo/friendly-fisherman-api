using System.ComponentModel.DataAnnotations;

namespace Users.Domain.EntityViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string ImagePath { get; set; }
    }
}
