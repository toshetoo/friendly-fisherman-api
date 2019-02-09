using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(UsersDbContext context) : base(context)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            var repo = CreateRepo();
            return repo.GetAll();
        }

        public User GetByUsername(string username)
        {
            var repo = CreateRepo();
            var user = repo.Get(x => x.UserName == username);
            return new User { UserName = user.UserName, Id = user.Id, SecurityStamp = user.SecurityStamp, PasswordHash = user.PasswordHash };
        }
    }
}
