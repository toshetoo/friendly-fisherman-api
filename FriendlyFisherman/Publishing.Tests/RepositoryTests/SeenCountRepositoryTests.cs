using System;
using System.Collections.Generic;
using System.Text;
using Publishing.DataAccess.Repositories.Threads;
using Publishing.Domain.Entities.Threads;
using Publishing.Tests.Fixtures;
using Xunit;

namespace Publishing.Tests.RepositoryTests
{
    public class SeenCountRepositoryTests : IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public SeenCountRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithContext_BuildsCorrectly()
        {
            var repo = new SeenCountRepository(_contextFixture.CreateMockContext(_dbSetFixture.CreateMockSet(new List<SeenCount>())).Object);
            Assert.NotNull(repo);
        }

        [Fact]
        public void Repo_WithoutContext_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new SeenCountRepository(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);
        }
    }
}
