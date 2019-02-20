using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.Entities;

namespace Users.Domain.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetByUsername(string username);
        User GetById(string id);
        void Save(User user);
    }
}
