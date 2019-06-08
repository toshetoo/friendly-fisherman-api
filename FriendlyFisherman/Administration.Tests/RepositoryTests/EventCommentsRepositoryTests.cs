using System;
using System.Collections.Generic;
using System.Text;
using Administration.DataAccess.Repositories.Events;
using Administration.Domain.Entities.Events;
using Administration.Tests.Fixtures;
using Xunit;

namespace Administration.Tests.RepositoryTests
{
    public class EventCommentsRepositoryTests: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public EventCommentsRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithContext_BuildsCorrectly()
        {
            var repo = new EventCommentsRepository(_contextFixture.CreateMockContext(_dbSetFixture.CreateMockSet(new List<EventComment>())).Object);
            Assert.NotNull(repo);
        }

        [Fact]
        public void Repo_WithoutContext_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new EventCommentsRepository(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);
        }
    }
}
