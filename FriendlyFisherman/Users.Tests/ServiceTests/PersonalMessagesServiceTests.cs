using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebSockets.Internal;
using Users.Domain.Entities;
using Users.Domain.EntityViewModels.PersonalMessage;
using Users.Domain.Repositories;
using Users.Services.Abstraction;
using Users.Services.Implementation;
using Users.Services.Request.PersonalMessage;
using Users.Tests.Fixtures;
using Xunit;
using Constants = Users.Tests.TestData.Constants;

namespace Users.Tests.ServiceTests
{
    public class PersonalMessagesServiceTests: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>, IClassFixture<RepositoryFixture>
    {
        private readonly IPersonalMessagesService _service;
        private readonly IPersonalMessagesRepository _repositoryMock;
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public PersonalMessagesServiceTests(RepositoryFixture repositoryFixture, ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;

            var data = new TestData.TestData().GetPersonalMessagesData();
            var mockSet = _dbSetFixture.CreateMockSet<PersonalMessage>(data);
            var mockContext = _contextFixture.CreateMockContext<PersonalMessage>(mockSet).Object;
            _repositoryMock = repositoryFixture.CreatePersonalMessagesRepository(mockContext);

            _service = new PersonalMessagesService(_repositoryMock);
        }

        [Fact]
        public void GetAllMessagesBySenderIdAsync_ThrowsException_WithNullAsParam()
        {
            Assert.ThrowsAsync<Exception>(() => _service.GetAllMessagesBySenderIdAsync(null));
        }

        [Fact]
        public async Task GetAllMessagesBySenderIdAsync_ReturnsCollectionOfMessages()
        {
            var res = await _service.GetAllMessagesBySenderIdAsync(new GetAllMessagesRequest {SenderId = Constants.AdminId});
            Assert.NotNull(res);
            Assert.NotNull(res.Items);
            Assert.Null(res.Exception);
            Assert.Equal(3, res.Items.Count());
        }

        [Fact]
        public async Task GetAllMessagesBySenderIdAsync_ReturnsEmptyCollection_WithWrongId()
        {
            var res = await _service.GetAllMessagesBySenderIdAsync(new GetAllMessagesRequest { SenderId = Guid.NewGuid().ToString() });
            Assert.NotNull(res);
            Assert.NotNull(res.Items);
            Assert.Null(res.Exception);
            Assert.Empty(res.Items);
        }

        [Fact]
        public void GetAllMessagesByReceiverIdAsync_ThrowsException_WithNullAsParam()
        {
            Assert.ThrowsAsync<Exception>(() => _service.GetAllMessagesByReceiverIdAsync(null));
        }

        [Fact]
        public async Task GetAllMessagesByReceiverIdAsync_ReturnsCollectionOfMessages()
        {
            var res = await _service.GetAllMessagesByReceiverIdAsync(new GetAllMessagesRequest { ReceiverId = Constants.AdminId });
            Assert.NotNull(res);
            Assert.NotNull(res.Items);
            Assert.Null(res.Exception);
            Assert.Equal(2, res.Items.Count());
        }

        [Fact]
        public async Task GetAllMessagesByReceiverIdAsync_ReturnsEmptyCollection_WithWrongId()
        {
            var res = await _service.GetAllMessagesByReceiverIdAsync(new GetAllMessagesRequest { ReceiverId = Guid.NewGuid().ToString() });
            Assert.NotNull(res);
            Assert.NotNull(res.Items);
            Assert.Null(res.Exception);
            Assert.Empty(res.Items);
        }

        [Fact]
        public async Task GetMessageById_ReturnsException_WithWrongId()
        {
            var request = new GetMessagesRequest() { MessageId = Guid.NewGuid().ToString() };
            var result = await _service.GetMessageByIdAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task GetMessageById_ReturnsUser_WithCorrectId()
        {
            var request = new GetMessagesRequest() { MessageId = Constants.PersonalMessageID };
            var result = await _service.GetMessageByIdAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Item);
            Assert.Equal(Constants.PersonalMessageID, result.Item.Id);
        }

        [Fact]
        public async Task SaveMessage_ReturnsException_WithWrongUser()
        {
            var request = new EditMessageRequest(null);
            var result = await _service.SaveMessageAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task SaveMessage_ReturnsException_WithWrongMessageId()
        {
            var request = new EditMessageRequest(new PersonalMessageViewModel() { Id = Guid.NewGuid().ToString() });
            var result = await _service.SaveMessageAsync(request);

            Assert.NotNull(result);
            Assert.NotNull(result.Exception);
        }

        [Fact]
        public async Task SaveMessage_UpdatesMessage_WithCorrectMessage()
        {
            var messageToEdit = await _service.GetMessageByIdAsync(new GetMessagesRequest() { MessageId = Constants.PersonalMessageID });

            messageToEdit.Item.Title = "Test CHANGED";
            var request = new EditMessageRequest(messageToEdit.Item);
            await _service.SaveMessageAsync(request);

            var result = await _service.GetMessageByIdAsync(new GetMessagesRequest() { MessageId = Constants.PersonalMessageID });

            Assert.NotNull(result);
            Assert.NotNull(result.Item);
            Assert.Equal("Test CHANGED", result.Item.Title);
        }

        [Fact]
        public async Task DeleteMessage_WithoutModel_ThrowsException()
        {
            Assert.ThrowsAsync<Exception>(() => _service.DeleteMessageAsync(null));
        }

        [Fact]
        public async Task DeleteMessage_WithoutId_ThrowsException()
        {
            Assert.ThrowsAsync<Exception>(() => _service.DeleteMessageAsync(new GetMessagesRequest()));
        }

        [Fact]
        public async Task DeleteMessage_WithCorrectId_DeletesMessage()
        {
            await _service.DeleteMessageAsync(new GetMessagesRequest {MessageId = Constants.PersonalMessageID});
            var result = await _service.GetMessageByIdAsync(new GetMessagesRequest {MessageId = Constants.PersonalMessageID});

            Assert.NotNull(result);
            Assert.Null(result.Item);
        }
    }
}
