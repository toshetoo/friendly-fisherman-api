using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Users.DataAccess.Repositories;
using Users.Domain.Entities;
using Users.Domain.EntityViewModels;
using Users.Domain.Repositories;
using Users.Services.Abstraction;
using Users.Services.Implementation;
using Users.Services.Request;
using Users.Tests.Fixtures;
using Users.Tests.TestData;
using Xunit;

namespace Users.Tests.ServiceTests
{
    public class UserServiceTests: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>, IClassFixture<RepositoryFixture>, IClassFixture<AppSettingsFixture>
    {
        private readonly IUserService _service;
        private readonly IUserRepository _repositoryMock;
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;
        private readonly AppSettingsFixture _settingsFixture;

        public UserServiceTests(ContextFixture contextFixture, DbSetFixture dbSetFixture, RepositoryFixture repositoryFixture, AppSettingsFixture settingsFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
            _settingsFixture = settingsFixture;

            var data = new TestData.TestData().GetUsersData();
            var mockSet = _dbSetFixture.CreateMockSet<User>(data);
            var mockContext = _contextFixture.CreateMockContext<User>(mockSet).Object;
            _repositoryMock = repositoryFixture.CreateUsersRepository(mockContext);

            var mockSettings = _settingsFixture.CreateMockSettings();
            _service = new UserService(_repositoryMock, mockSettings);
        }

        [Fact]
        public void GetUserAuthenticationAsync_ThrowsException_IfParamIsNUll()
        {
            Assert.ThrowsAsync<NullReferenceException>(() => _service.GetUserAuthenticationAsync(null));
        }

        [Fact]
        public async Task GetUserAuthenticationAsync_WithWrongUsername_ReturnsNull()
        {
            UserAuthenticationRequest req = new UserAuthenticationRequest(Guid.NewGuid().ToString());
            Assert.Null(await _service.GetUserAuthenticationAsync(req));
        }

        [Fact]
        public async Task GetUserAuthenticationAsync_WithCorrectData_ReturnsToken()
        {
            UserAuthenticationRequest req = new UserAuthenticationRequest(_repositoryMock.GetAllUsers().First().UserName);
            var result = await _service.GetUserAuthenticationAsync(req);
            Assert.NotNull(result);
            Assert.NotNull(result.AccessToken);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsCollectionOfUsers()
        {
            var result = await _service.GetAllUsersAsync(new GetAllUsersRequest());
            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.Equal(3, result.Items.Count());
        }

        [Fact]
        public async Task GetUserById_ReturnsException_WithWrongId()
        {
            var request = new GetUserRequest() { Id = Guid.NewGuid().ToString() };
            var result = await _service.GetUserByIdAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser_WithCorrectId()
        {
            var request = new GetUserRequest() { Id = Constants.AdminId };
            var result = await _service.GetUserByIdAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Item);
            Assert.Equal(Constants.AdminId, result.Item.Id);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsException_WithWrongEmail()
        {
            var request = new GetUserRequest() { Email = Guid.NewGuid().ToString() };
            var result = await _service.GetUserByEmailAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task GetUserByEmail_ReturnsUser_WithCorrectEmail()
        {
            var userRequest = new GetUserRequest() { Id = Constants.AdminId };
            var user = await _service.GetUserByIdAsync(userRequest);
            var emailRequest = new GetUserRequest() { Id = user.Item.Id, Email = user.Item.Email};
            var result = await _service.GetUserByEmailAsync(emailRequest);

            Assert.NotNull(result);
            Assert.NotNull(result.Item);
            Assert.Equal(user.Item.Email, result.Item.Email);
        }

        [Fact]
        public async Task EditUser_ReturnsException_WithWrongUser()
        {
            var request = new EditUserRequest(null);
            var result = await _service.EditUserAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task EditUser_ReturnsException_WithWrongUserId()
        {
            var request = new EditUserRequest(new UserViewModel(){Id = Guid.NewGuid().ToString()});
            var result = await _service.EditUserAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task EditUser_UpdatesUser_WithCorrectUser()
        {
            var userToEdit = await _service.GetUserByIdAsync(new GetUserRequest() {Id = Constants.UserId });

            userToEdit.Item.FirstName = "Test";
            var request = new EditUserRequest(userToEdit.Item);
            await _service.EditUserAsync(request);

            var result = await _service.GetUserByIdAsync(new GetUserRequest() { Id = Constants.UserId });

            Assert.NotNull(result);
            Assert.NotNull(result.Item);
            Assert.Equal("Test", result.Item.FirstName);
        }

    }
}
