using System;
using System.Collections.Generic;
using System.Text;
using Publishing.DataAccess.Repositories.Threads;
using Publishing.Domain.Entities.Threads;
using Publishing.Tests.Fixtures;
using Xunit;

namespace Publishing.Tests.RepositoryTests
{
    public class ThreadsRepositoryTests : IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public ThreadsRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithContext_BuildsCorrectly()
        {
            var repo = new ThreadsRepository(_contextFixture.CreateMockContext(_dbSetFixture.CreateMockSet(new List<Thread>())).Object);
            Assert.NotNull(repo);
        }

        [Fact]
        public void Repo_WithoutContext_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ThreadsRepository(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);
        }
    }
}
