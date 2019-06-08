using System;
using System.Collections.Generic;
using Administration.DataAccess.Repositories.Polls;
using Administration.Domain.Entities.Polls;
using Administration.Tests.Fixtures;
using Xunit;

namespace Administration.Tests.RepositoryTests
{
    public class PollsRepositoryTests : IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public PollsRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithContext_BuildsCorrectly()
        {
            var repo = new PollsRepository(_contextFixture.CreateMockContext(_dbSetFixture.CreateMockSet(new List<Poll>())).Object);
            Assert.NotNull(repo);
        }

        [Fact]
        public void Repo_WithoutContext_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new PollsRepository(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);
        }
    }
}
