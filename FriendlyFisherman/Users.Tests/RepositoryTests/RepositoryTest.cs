using AutoFixture;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using Users.Domain.Entities;
using Users.Tests.Fixtures;
using Users.Tests.TestData;
using Xunit;

namespace Users.Tests.RepositoryTests
{
    public class RepositoryTest : IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public RepositoryTest(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_WithoutContext_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new BaseRepository<User>(null));

            Assert.NotNull(exception);
            Assert.Equal("context", exception.ParamName);
        }

        [Fact]
        public void Repo_WithCorrectParams_BuildsOk()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            Assert.NotNull(repo);
        }

        [Fact]
        public void Create_WithNull_ThrowsArgumentNullException()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.Create(null));

            Assert.NotNull(exception);
            Assert.Equal("entity", exception.ParamName);
        }

        [Fact]
        public void Create_WithCorrectObject_ReturnsOK()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var user = new Fixture().Build<User>().Create();
            repo.Create(user);

            repo = GetRepo(data);
            Assert.NotNull(repo.Get(u => u.Id == user.Id));
        }

        [Fact]
        public void Get_WithNullAsFilter_ThrowsArgumentNullException()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.Get(null));

            Assert.NotNull(exception);
            Assert.Equal("whereClause", exception.ParamName);
        }

        [Fact]
        public void GetAll_ReturnsList()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var result = repo.GetAll();

            Assert.NotNull(result);
            Assert.Equal(typeof(List<User>), result.GetType());
        }

        [Fact]
        public void GetAll_WithOrderByNull_ReturnsDefaultOrder()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var result = repo.GetAll<User>(false, null).ToList();

            Assert.NotNull(result);
            for (int i = 0; i < data.Count; i++)
            {
                Assert.Equal(result[i].Id, data[i].Id);
            }
        }

        [Fact]
        public void Update_WithNull_ThrowsArgumentNullException()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.Update(null));

            Assert.NotNull(exception);
            Assert.Equal("newEntity", exception.ParamName);
        }

        [Fact]
        public void Delete_WithNull_ThrowsArgumentNullException()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.Delete((User)null));

            Assert.NotNull(exception);
            Assert.Equal("object", exception.ParamName);
        }

        [Fact]
        public void DeleteRange_WithNull_ThrowsArgumentNullException()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var exception = Assert.Throws<ArgumentNullException>(() => repo.DeleteRange(null));

            Assert.NotNull(exception);
            Assert.Equal("objects", exception.ParamName);
        }

        [Fact]
        public void GetAll_WithoutParameters_ReturnsAll()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var collection = repo.GetAll();

            Assert.NotNull(collection);
            Assert.NotEmpty(collection);
            Assert.Equal(3, collection.Count());
        }

        [Fact]
        public void GetAll_WithOrderByAscParam_ReturnsOrderedByAsc()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var collection = repo.GetAll(false, user => user.FirstName);

            Assert.NotNull(collection);
            Assert.Equal("Admin", collection.First().FirstName);
            Assert.Equal("User", collection.Last().FirstName);
        }

        [Fact]
        public void GetAll_WithOrderByDescParam_ReturnsOrderedByDesc()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var collection = repo.GetAll(true, user => user.FirstName);

            Assert.NotNull(collection);
            Assert.Equal("User", collection.First().FirstName);
            Assert.Equal("Admin", collection.Last().FirstName);
        }

        [Fact]
        public void GetWhere_WithNull_ThrowsNullReferenceException()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            Assert.Throws<NullReferenceException>(() => repo.GetWhere(null, null));
        }

        [Fact]
        public void GetWhere_WithWhereClause_ReturnsFilteredCollection()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var collection = repo.GetWhere(user => user.Email.Contains("admin"));

            Assert.NotNull(collection);
            Assert.Single(collection);
        }

        [Fact]
        public void GetWhere_WithWhereClauseAndSorting_ReturnsFilteredSortedAscCollection()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var collection = repo.GetWhere(
                user => user.Email.EndsWith("user.com"), user => user.Email);

            Assert.NotNull(collection);
            Assert.Equal("john@user.com", collection.First().Email);
            Assert.Equal("user@user.com", collection.Last().Email);
        }

        [Fact]
        public void GetWhere_WithWhereClauseAndSorting_ReturnsFilteredSortedDescCollection()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var collection = repo.GetWhere(
                user => user.Email.EndsWith("user.com"), user => user.Email, true);

            Assert.NotNull(collection);
            Assert.Equal("user@user.com", collection.First().Email);
            Assert.Equal("john@user.com", collection.Last().Email);
        }

        [Fact]
        public void Delete_WithNotExistingWhereClause_ReturnsWithoutDeleting()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            Assert.Equal(3, repo.GetAll().Count());

            repo = GetRepo(data);
            repo.Delete(user => user.Id == Guid.NewGuid().ToString());

            repo = GetRepo(data);
            Assert.Equal(3, repo.GetAll().Count());
        }

        [Fact]
        public void Delete_WithValidWhereClause_RemovesUser()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            Assert.Equal(3, repo.GetAll().Count());

            repo = GetRepo(data);
            repo.Delete(user => user.Id == Constants.AdminId);

            repo = GetRepo(data);
            Assert.Equal(2, repo.GetAll().Count());

            repo = GetRepo(data);
            Assert.Null(repo.Get(u => u.Id == Constants.AdminId));
        }

        [Fact(Skip = "Delete does not work")]
        public void DeleteRange_WithValidCollection_RemovesTheItems()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            Assert.Equal(3, repo.GetAll().Count());
            
            var collectionToDelete = data.Where(user => user.Email.Contains("user.com"));

            repo = GetRepo(data);
            repo.DeleteRange(collectionToDelete);

            repo = GetRepo(data);
            Assert.Single(repo.GetAll());
        }

        private BaseRepository<User> GetRepo(List<User> data)
        {
            var mockSet = _dbSetFixture.CreateMockSet<User>(data);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;

            return new BaseRepository<User>(mockContext);
        }
    }
}
