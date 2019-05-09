using FriendlyFisherman.SharedKernel;
using FriendlyFisherman.SharedKernel.Services.Models;
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
