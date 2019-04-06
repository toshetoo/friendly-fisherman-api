using Users.Domain.EntityViewModels;

namespace Users.Services.Request
{
    using FriendlyFisherman.SharedKernel;

    public class GetUserRequest : ServiceRequestBase<UserViewModel>
    {
        public GetUserRequest()
        {
            
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
