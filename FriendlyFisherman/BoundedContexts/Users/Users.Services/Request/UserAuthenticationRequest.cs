using FriendlyFisherman.SharedKernel;

namespace Users.Services.Request
{
    public class UserAuthenticationRequest : ServiceRequestBase
    {
        public UserAuthenticationRequest(string username)
        {
            Username = username;
        }
        public string Username { get; set; }
    }
}
