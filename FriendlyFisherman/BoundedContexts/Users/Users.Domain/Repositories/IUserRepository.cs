using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.Entities;

namespace Users.Domain.Repositories
{
    public interface IUserRepository
    {
        void GetAllUsers();
        User GetByUsername(string username);
    }
}
