using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            return CreateRepo().GetAll();
        }

        public User GetByUsername(string username)
        {
            var user = CreateRepo().Get(x => x.UserName == username);
            return new User { UserName = user.UserName, Id = user.Id, SecurityStamp = user.SecurityStamp, PasswordHash = user.PasswordHash };
        }
    }
}
