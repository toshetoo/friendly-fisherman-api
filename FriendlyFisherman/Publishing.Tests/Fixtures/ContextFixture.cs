using Microsoft.EntityFrameworkCore;
using Moq;
using Publishing.DataAccess;

namespace Publishing.Tests.Fixtures
{
    public class ContextFixture
    {
        public Mock<PublishingDbContext> CreateMockContext<T>(Mock<DbSet<T>> mockSet) where T: class
        {
            var mockContext = new Mock<PublishingDbContext>();

            mockContext.Setup(context => context.Set<T>())
                .Returns(mockSet.Object);

            return mockContext;
        }
    }

}
