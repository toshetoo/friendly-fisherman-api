using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Users.DataAccess;
using Users.Domain.Entities;

namespace Users.Tests.Fixtures
{
    public class ContextFixture
    {
        public Mock<UsersDbContext> CreateMockContext<T>(Mock<DbSet<User>> mockSet)
        {
            var mockContext = new Mock<UsersDbContext>();

            mockContext.Setup(context => context.Set<User>())
                .Returns(mockSet.Object);

            return mockContext;
        }
    }

}
