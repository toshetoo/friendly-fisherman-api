namespace Users.Services.Request
{
    using FriendlyFisherman.SharedKernel;

    public class GetUserRequest : ServiceRequestBase
    {
        public GetUserRequest(string id, string email = "")
        {
            Id = id;
            Email = email;
        }

        public string Id { get; set; }
        public string Email { get; set; }
    }
}
