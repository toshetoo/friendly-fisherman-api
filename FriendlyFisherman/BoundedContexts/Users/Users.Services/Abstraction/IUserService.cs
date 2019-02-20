namespace Users.Services.Abstraction
{
    using Users.Services.Request;
    using Users.Services.Response;

    public interface IUserService
    {
        GetAllUsersResponse GetAllUsersAsync(GetAllUsersRequest request);
        UserAuthenticationResponse GetAuth(UserAuthenticationRequest request);
    }
}
