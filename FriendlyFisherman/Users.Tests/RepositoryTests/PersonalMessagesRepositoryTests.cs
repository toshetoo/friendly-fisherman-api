using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using Users.DataAccess.Repositories;
using Users.Domain.Entities;
using Users.Tests.Fixtures;
using Users.Tests.TestData;
using Xunit;

namespace Users.Tests.RepositoryTests
{
    public class PersonalMessagesRepositoryTests: IClassFixture<ContextFixture>, IClassFixture<DbSetFixture>
    {
        private readonly ContextFixture _contextFixture;
        private readonly DbSetFixture _dbSetFixture;

        public PersonalMessagesRepositoryTests(ContextFixture contextFixture, DbSetFixture dbSetFixture)
        {
            _contextFixture = contextFixture;
            _dbSetFixture = dbSetFixture;
        }

        [Fact]
        public void Repo_BuildsCorrectly()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            Assert.NotNull(new PersonalMessagesRepository(null));
            Assert.NotNull(GetRepo(data));
        }

        [Fact]
        public void GetAllMessagesBySenderId_ReturnsCollectionWithCorrectId()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var result = repo.GetAllMessagesBySenderId(Constants.AdminId);
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetAllMessagesBySenderId_ReturnsEmptyListWithWrongId()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var result = repo.GetAllMessagesBySenderId(Guid.NewGuid().ToString());
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllMessagesBySenderId_ReturnsEmptyListWithoutId()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var res = repo.GetAllMessagesBySenderId(null);

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void GetAllMessagesByReceiverId_ReturnsCollectionWithCorrectId()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var result = repo.GetAllMessagesByReceiverId(Constants.AdminId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllMessagesByReceiverId_ReturnsEmptyListWithWrongId()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var result = repo.GetAllMessagesByReceiverId(Guid.NewGuid().ToString());
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllMessagesByReceiverId_ReturnsEmptyListWithoutId()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);
            var res = repo.GetAllMessagesByReceiverId(null);

            Assert.NotNull(res);
            Assert.Empty(res);
        }

        [Fact]
        public void GetById_ReturnsCorrectMessage()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var result = repo.GetMessageById(data[0].Id);

            Assert.NotNull(result);
            Assert.Equal(data[0].Id, result.Id);
        }

        [Fact]
        public void GetById_WithWrongId_ReturnsError()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var result = repo.GetMessageById(Guid.NewGuid().ToString());

            Assert.Null(result);
        }

        [Fact]
        public void GetById_WithoutId_ReturnsException()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var result = repo.GetMessageById(null);

            Assert.Null(result);
        }

        [Fact(Skip = "Entry of EF not mocked yet")]
        public void SaveMessage_WithCorrectData_SavesMessage()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            var m = repo.GetMessageById(Constants.PersonalMessageID);
            Assert.NotNull(m);

            m.Title = "Edited title";

            repo = GetRepo(data);
            repo.SaveMessage(m);

            var res = repo.GetMessageById(Constants.PersonalMessageID);

            Assert.NotNull(res);
            Assert.Equal("Edited title", res.Title);
        }

        [Fact]
        public void SaveMessage_WithoutData_ThrowsException()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);

            Assert.Throws<NullReferenceException>(() => repo.SaveMessage(null));
        }

        [Fact]
        public void SaveMessage_WithoutId_CreatesMessage()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);
            var count = data.Count();
            var message = new Fixture().Build<PersonalMessage>().Without(m => m.Id).Create();

            repo.SaveMessage(message);

            Assert.Equal(count + 1, data.Count);
        }

        [Fact]
        public void DeleteMessage_WithCorrectId_DeletesMessage()
        {
            var data = new TestData.TestData().GetPersonalMessagesData();
            var repo = GetRepo(data);
            repo.DeleteMessage(Constants.PersonalMessageID);

            var res = repo.GetMessageById(Constants.PersonalMessageID);
            Assert.Null(res);
        }


        private PersonalMessagesRepository GetRepo(List<PersonalMessage> data)
        {
            var mockSet = _dbSetFixture.CreateMockSet<PersonalMessage>(data);
            var mockContext = _contextFixture.CreateMockContext<PersonalMessage>(mockSet).Object;

            return new PersonalMessagesRepository(mockContext);
        }
    }
}
