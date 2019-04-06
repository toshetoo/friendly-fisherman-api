using Users.Domain.Entities;

namespace Users.Services.Response
{
    using FriendlyFisherman.SharedKernel;

    public class UserAuthenticationResponse : ServiceResponseBase<User>
    {
        public string AccessToken { get; set; }
    }
}
