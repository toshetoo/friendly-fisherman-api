using Microsoft.EntityFrameworkCore;
using Moq;
using Users.DataAccess;
using Users.Domain.Entities;

namespace Users.Tests.Fixtures
{
    public class ContextFixture
    {
        public Mock<UsersDbContext> CreateMockContext<T>(Mock<DbSet<T>> mockSet) where T: User
        {
            var mockContext = new Mock<UsersDbContext>();

            mockContext.Setup(context => context.Set<T>())
                .Returns(mockSet.Object);

            return mockContext;
        }
    }

}
