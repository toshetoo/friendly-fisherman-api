using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FriendlyFisherman.SharedKernel.Repositories.Impl;
using Users.DataAccess.Repositories;
using Users.Domain.Entities;
using Users.Tests.Fixtures;
using Users.Tests.TestData;
using Xunit;

namespace Users.Tests.RepositoryTests
{
    public class UsersRepositoryTests: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public UsersRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_BuildsCorrectly()
        {
            var data = new UsersTestData().GetUsersData();
            Assert.NotNull(new UserRepository(null));
            Assert.NotNull(GetRepo(data));
        }

        [Fact]
        public void GetAll_ReturnsCollection()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var result = repo.GetAllUsers();
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetUserByUsername_ReturnsCorrectUser()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var result = repo.GetByUsername(data[0].UserName);

            Assert.NotNull(result);
            Assert.Equal(data[0].Id, result.Id);
        }

        [Fact]
        public void GetUserByUsername_WithWrongUser_ReturnsError()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var result = repo.GetByUsername(Guid.NewGuid().ToString());

            Assert.Null(result);
        }

        [Fact]
        public void GetById_ReturnsCorrectUser()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var result = repo.GetById(data[0].Id);

            Assert.NotNull(result);
            Assert.Equal(data[0].Id, result.Id);
        }

        [Fact]
        public void GetById_WithWrongUser_ReturnsError()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var result = repo.GetById(Guid.NewGuid().ToString());

            Assert.Null(result);
        }

        [Fact]
        public void Save_WithNoId_AddsNewUser()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            var newUser = new User()
            {
                UserName = "Test 1"
            };

            repo.Save(newUser);

            repo = GetRepo(data);
            Assert.Equal(4, repo.GetAllUsers().Count());
        }

        [Fact]
        public void Save_WithNull_ThrowsException()
        {
            var data = new UsersTestData().GetUsersData();
            var repo = GetRepo(data);

            Assert.Throws<NullReferenceException>(() => repo.Save(null));
        }

        private UserRepository GetRepo(List<User> data)
        {
            var mockSet = _dbSetFixture.CreateMockSet<User>(data);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;

            return new UserRepository(mockContext);
        }
    }
}
