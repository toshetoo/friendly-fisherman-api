using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Users.Domain.Repositories;
using Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Users.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UsersDbContext _context;

        public UserRepository(UsersDbContext context)
        {
            _context = context;
        }

        public void GetAllUsers()
        {
            _context.Users.FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserName == username);
            return new User { Username = user.UserName, Id = user.Id, Token = user.SecurityStamp, Password = user.PasswordHash };
        }
    }
}
