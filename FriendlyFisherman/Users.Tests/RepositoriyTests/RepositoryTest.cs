using FriendlyFisherman.SharedKernel.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using Users.Domain.Entities;
using Users.Tests.Fixtures;
using Users.Tests.TestData;
using Xunit;

namespace Users.Tests.RepositoriyTests
{
    public class RepositoryTest: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public RepositoryTest(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithoutContext_ThorowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new BaseRepository<User>(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);

        }

        [Fact]
        public void Repo_WithCorrectParams_BuildsOk()
        {
            var mockSet = _dbSetFixture.CreateMockSet<User>(UsersTestData.UsersData);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;
            var repo = new BaseRepository<User>(mockContext);

            Assert.NotNull(repo);
        }

        [Fact]
        public void Create_WithNull_ThorowsArgumentNullException()
        {            
            var mockSet = _dbSetFixture.CreateMockSet<User>(UsersTestData.UsersData);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;
            var repo = new BaseRepository<User>(mockContext);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.Create(null));

            Assert.NotNull(exception);
            Assert.Equal("entity", exception.ParamName);

        }

        [Fact]
        public void Update_WithNull_ThorowsArgumentNullException()
        {
            var mockSet = _dbSetFixture.CreateMockSet<User>(UsersTestData.UsersData);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;
            var repo = new BaseRepository<User>(mockContext);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.Update(null));

            Assert.NotNull(exception);
            Assert.Equal("newEntity", exception.ParamName);

        }

        [Fact]
        public void Delete_WithNull_ThorowsArgumentNullException()
        {
            var mockSet = _dbSetFixture.CreateMockSet<User>(UsersTestData.UsersData);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;
            var repo = new BaseRepository<User>(mockContext);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.Delete((User)null));

            Assert.NotNull(exception);
            Assert.Equal("object", exception.ParamName);

        }

        [Fact]
        public void DeleteRange_WithNull_ThorowsArgumentNullException()
        {
            var mockSet = _dbSetFixture.CreateMockSet<User>(UsersTestData.UsersData);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;
            var repo = new BaseRepository<User>(mockContext);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.DeleteRange(null));

            Assert.NotNull(exception);
            Assert.Equal("objects", exception.ParamName);

        }

    }
}
