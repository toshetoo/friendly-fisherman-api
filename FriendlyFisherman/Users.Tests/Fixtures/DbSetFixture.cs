using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Users.Domain.Entities;

namespace Users.Tests.Fixtures
{
    public class DbSetFixture
    {
        public Mock<DbSet<T>> CreateMockSet<T>(List<T> collection = null) where T : User
        {
            var mockSet = new Mock<DbSet<T>>();

            if (collection == null)
                return mockSet;

            mockSet.Setup(set => set.Add(It.IsAny<T>()))
                .Callback((T item) =>
                {
                    collection.Add(item);
                });

            mockSet.Setup(set => set.Remove(It.IsAny<T>()))
                .Callback((T item) =>
                {
                    collection.Remove(item);
                });

            return mockSet;
        }
    }

}
