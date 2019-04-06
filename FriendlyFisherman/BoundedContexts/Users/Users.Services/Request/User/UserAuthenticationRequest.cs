using FriendlyFisherman.SharedKernel;
using Users.Domain.Entities;

namespace Users.Services.Request
{
    public class UserAuthenticationRequest : ServiceRequestBase<User>
    {
        public UserAuthenticationRequest(string username)
        {
            Username = username;
        }
        public string Username { get; set; }
    }
}
