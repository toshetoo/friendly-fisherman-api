namespace Users.Services.Response
{
    using FriendlyFisherman.SharedKernel;
    using Users.Domain.EntityViewModels;

    public class GetUserResponse : ServiceResponseBase
    {
        public UserViewModel User { get; set; }
    }
}
