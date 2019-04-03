using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.Tests.Fixtures
{
    public class RepositoryFixture
    {
        public IUserRepository CreateUsersRepository(DbContext context)
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

        public IPersonalMessagesRepository CreatePersonalMessagesRepository(DbContext context)
        {
            var dbSet = context.Set<PersonalMessage>();
            var repositoryMock = new Mock<IPersonalMessagesRepository>();

            repositoryMock.Setup(repo => repo.GetMessageById(It.IsAny<string>()))
                .Returns((string id) => dbSet.FirstOrDefault(m => m.Id == id));

            repositoryMock.Setup(repo => repo.GetAllMessagesBySenderId(It.IsAny<string>()))
                .Returns((string email) => dbSet.Where(m => m.SenderId == email));

            repositoryMock.Setup(repo => repo.GetAllMessagesByReceiverId(It.IsAny<string>()))
                .Returns((string email) => dbSet.Where(m => m.ReceiverId == email));

            repositoryMock.Setup(repo => repo.SaveMessage(It.IsAny<PersonalMessage>()))
                .Callback((PersonalMessage message) =>
                {
                    if (message.Id == null)
                    {
                        dbSet.Add(message);
                    }
                    else
                    {
                        dbSet.Update(message);
                    }
                });

            repositoryMock.Setup(repo => repo.DeleteMessage(It.IsAny<string>()))
                .Callback((string id) => { dbSet.Remove(dbSet.Find(id)); });

            return repositoryMock.Object;
        }
    }
}
