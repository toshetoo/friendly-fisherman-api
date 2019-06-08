using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Administration.Tests.Fixtures
{
    public class DbSetFixture
    {
        public Mock<DbSet<T>> CreateMockSet<T>(List<T> collection = null) where T: class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(collection.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(collection.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(collection.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(collection.AsQueryable().GetEnumerator());

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
