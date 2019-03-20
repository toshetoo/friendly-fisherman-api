namespace Users.Services.Abstraction
{
    using System.Threading.Tasks;
    using Users.Services.Request;
    using Users.Services.Response;

    public interface IUserService
    {
        Task<UserAuthenticationResponse> GetUserAuthenticationAsync(UserAuthenticationRequest request);
        Task<GetAllUsersResponse> GetAllUsersAsync(GetAllUsersRequest request);
        Task<GetUserResponse> GetUserByIdAsync(GetUserRequest request);
        Task<GetUserResponse> GetUserByEmailAsync(GetUserRequest request);
        Task<EditUserResponse> EditUserAsync(EditUserRequest request);
    }
}
