using System;
using System.Collections.Generic;
using Users.Domain.Entities;

namespace Users.Tests.TestData
{
    public class UsersTestData
    {
        public List<User> GetUsersData()
        {
            return new List<User>
            {
                new User
                {
                    Id = Constants.AdminId,
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Adminov",
                    UserName = "admin"
                },
                new User
                {
                    Id = Constants.UserId,
                    Email = "user@user.com",
                    FirstName = "User",
                    LastName = "Userov",
                    UserName = "user"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "john@user.com",
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "john"
                }
            };
        }
    }
}
