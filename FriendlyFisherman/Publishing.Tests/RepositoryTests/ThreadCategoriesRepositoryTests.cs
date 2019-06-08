using System;
using System.Collections.Generic;
using System.Text;
using Publishing.DataAccess.Repositories.Categories;
using Publishing.Domain.Entities.Categories;
using Publishing.Tests.Fixtures;
using Xunit;

namespace Publishing.Tests.RepositoryTests
{
    public class ThreadCategoriesRepositoryTests : IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public ThreadCategoriesRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithContext_BuildsCorrectly()
        {
            var repo = new ThreadCategoriesRepository(_contextFixture.CreateMockContext(_dbSetFixture.CreateMockSet(new List<ThreadCategory>())).Object);
            Assert.NotNull(repo);
        }

        [Fact]
        public void Repo_WithoutContext_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ThreadCategoriesRepository(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);
        }
    }
}
