namespace Users.Services.Request
{
    using FriendlyFisherman.SharedKernel;
    using Users.Domain.EntityViewModels;

    public class EditUserRequest : ServiceRequestBase
    {
        public EditUserRequest(UserViewModel user)
        {
            User = user;
        }

        public UserViewModel User { get; set; }
    }
}
