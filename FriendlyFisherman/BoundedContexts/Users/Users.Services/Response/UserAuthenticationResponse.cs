namespace Users.Services.Response
{
    using FriendlyFisherman.SharedKernel;

    public class UserAuthenticationResponse : ServiceResponseBase
    {
        public string AccessToken { get; set; }
    }
}
