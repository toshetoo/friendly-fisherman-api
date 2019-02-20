namespace Users.Services.Request
{
    using FriendlyFisherman.SharedKernel;

    public class GetUserRequest : ServiceRequestBase
    {
        public GetUserRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
