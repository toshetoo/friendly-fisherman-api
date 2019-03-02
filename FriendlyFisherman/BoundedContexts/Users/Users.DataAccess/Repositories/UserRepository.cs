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
            var repo = CreateRepo();
            return repo.GetAll();
        }

        public User GetByUsername(string username)
        {
            var repo = CreateRepo();

            var user = repo.Get(x => x.UserName == username);
            if (user == null)
                return null;

            return new User { UserName = user.UserName, Id = user.Id, SecurityStamp = user.SecurityStamp, PasswordHash = user.PasswordHash };
        }

        public User GetById(string id)
        {
            var repo = CreateRepo();

            var user = repo.Get(x => x.Id == id);
            return user;
        }

        public void Save(User user)
        {
            var repo = CreateRepo();

            if(user.Id == null)
            {
                repo.Create(user);
            } else
            {
                repo.Update(user);
            }
        }
    }
}
