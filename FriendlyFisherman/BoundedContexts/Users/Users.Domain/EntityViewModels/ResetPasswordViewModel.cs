namespace Users.Domain.EntityViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        public string PasswordToken { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }    
    }
}
