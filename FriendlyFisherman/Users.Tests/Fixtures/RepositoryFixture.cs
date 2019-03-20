using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using Users.DataAccess.Repositories;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.Tests.Fixtures
{
    public class RepositoryFixture
    {
        public IUserRepository CreateRepository(DbContext context)
        {
            var dbSet = context.Set<User>();
            var repositoryMock = new Mock<IUserRepository>();

            repositoryMock.Setup(repo => repo.GetAllUsers()).Returns(dbSet.ToList());

            repositoryMock.Setup(repo => repo.GetById(It.IsAny<string>()))
                .Returns((string id) => dbSet.FirstOrDefault(u => u.Id == id));

            repositoryMock.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                .Returns((string email) => dbSet.FirstOrDefault(u => u.Email == email));

            repositoryMock.Setup(repo => repo.GetByUsername(It.IsAny<string>()))
                .Returns((string username) => dbSet.FirstOrDefault(u => u.UserName == username));

            repositoryMock.Setup(repo => repo.Save(It.IsAny<User>()))
                .Callback((User user) =>
                {
                    if (user.Id == null)
                    {
                        dbSet.Add(user);
                    }
                    else
                    {
                        dbSet.Update(user);
                    }
                });

            return repositoryMock.Object;
        }
    }
}
