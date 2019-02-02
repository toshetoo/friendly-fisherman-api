namespace Users.Services.Abstraction
{
    using Users.Domain.Entities;

    public interface IUserService
    {
        void GetAllUsersAsync();
        User GetAuth(string username);
    }
}
