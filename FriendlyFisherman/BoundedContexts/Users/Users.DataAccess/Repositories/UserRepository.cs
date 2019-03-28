using FriendlyFisherman.SharedKernel.Repositories.Impl;
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

        /// <summary>
        /// Get all users in the Database
        /// </summary>
        /// <returns>An IEnumerable list of users</returns>
        public IEnumerable<User> GetAllUsers()
        {
            var repo = CreateRepo();
            return repo.GetAll();
        }

        /// <summary>
        /// Gets a single user by username from the database
        /// </summary>
        /// <param name="username">The username of the searched user</param>
        /// <returns>A user that matches the given username</returns>
        public User GetByUsername(string username)
        {
            var repo = CreateRepo();

            var user = repo.Get(x => x.UserName == username);
            if (user == null)
                return null;

            return new User { UserName = user.UserName, Id = user.Id, SecurityStamp = user.SecurityStamp, PasswordHash = user.PasswordHash };
        }

        /// <summary>
        /// Gets a single user by id from the database
        /// </summary>
        /// <param name="id">The id of the searched user</param>
        /// <returns>A user that matches the given id</returns>
        public User GetById(string id)
        {
            var repo = CreateRepo();

            var user = repo.Get(x => x.Id == id);
            return user;
        }

        /// <summary>
        /// Gets a single user by email from the database
        /// </summary>
        /// <param name="email">The email of the searched user</param>
        /// <returns>A user that matches the given email</returns>
        public User GetByEmail(string email)
        {
            var repo = CreateRepo();

            var user = repo.Get(x => x.Email == email);
            return user;
        }

        /// <summary>
        /// Saves or updates a user. If the User object in the parameter has an ID, an Update operation will be performed
        /// otherwise a new user will be created
        /// </summary>
        /// <param name="user">A user that should be modified or created</param>
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
