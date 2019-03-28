using System.Collections.Generic;
using Users.Domain.Entities;

namespace Users.Domain.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetByUsername(string username);
        User GetById(string id);
        User GetByEmail(string email);
        void Save(User user);
    }
}
