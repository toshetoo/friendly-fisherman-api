using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.Entities;

namespace Users.Tests.TestData
{
    public static class UsersTestData
    {
        public static List<User> UsersData = new List<User>()
        {
            new User()
            {
                Id = Constants.AdminId,
                Email = "admin@admin.com",
                FirstName = "Admin",
                LastName = "Adminov",
                UserName = "admin"                
            },
            new User()
            {
                Id = Constants.UserId,
                Email = "user@user.com",
                FirstName = "User",
                LastName = "Userov",
                UserName = "user"
            }
        };
    }
}
