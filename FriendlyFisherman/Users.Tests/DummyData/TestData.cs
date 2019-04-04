using System;
using System.Collections.Generic;
using Users.Domain.Entities;
using Users.Tests.TestData;

namespace Users.Tests.TestData
{
    public class TestData
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
                    UserName = "admin",
                    ImagePath = "TEST"
                },
                new User
                {
                    Id = Constants.UserId,
                    Email = "user@user.com",
                    FirstName = "User",
                    LastName = "Userov",
                    UserName = "user",
                    ImagePath = "TEST"
                },
                new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "john@user.com",
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "john",
                    ImagePath = "TEST"
                }
            };
        }

        public List<PersonalMessage> GetPersonalMessagesData()
        {
            return new List<PersonalMessage>
            {
                new PersonalMessage
                {
                    Id = Constants.PersonalMessageID,
                    ReceiverId = Constants.UserId,
                    SenderId = Constants.AdminId,
                    Seen = true,
                    SentOn = DateTime.Now,
                    Content = "Test dummy content",
                    Title = "Test dummy title"
                },
                new PersonalMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    ReceiverId = Constants.AdminId,
                    SenderId = Constants.UserId,
                    Seen = true,
                    SentOn = DateTime.Now,
                    Content = "Test dummy content",
                    Title = "Test dummy title"
                },
                new PersonalMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    ReceiverId = Constants.AdminId,
                    SenderId = Constants.UserId,
                    Seen = true,
                    SentOn = DateTime.Now,
                    Content = "Test dummy content",
                    Title = "Test dummy title"
                },
                new PersonalMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    ReceiverId = Constants.UserId,
                    SenderId = Constants.AdminId,
                    Seen = true,
                    SentOn = DateTime.Now,
                    Content = "Test dummy content",
                    Title = "Test dummy title"
                },
                new PersonalMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    ReceiverId = Constants.UserId,
                    SenderId = Constants.AdminId,
                    Seen = true,
                    SentOn = DateTime.Now,
                    Content = "Test dummy content",
                    Title = "Test dummy title"
                },
            };
        }
    }
}
