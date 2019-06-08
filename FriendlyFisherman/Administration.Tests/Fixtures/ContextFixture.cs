using Administration.DataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Administration.Tests.Fixtures
{
    public class ContextFixture
    {
        public Mock<AdministrationDbContext> CreateMockContext<T>(Mock<DbSet<T>> mockSet) where T: class
        {
            var mockContext = new Mock<AdministrationDbContext>();

            mockContext.Setup(context => context.Set<T>())
                .Returns(mockSet.Object);

            return mockContext;
        }
    }

}
